using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;
using ModelContextProtocol.NET.Core.Transport.Base;

namespace ModelContextProtocol.NET.Server.Features.Resources;

/// <summary>
/// Manages resource subscriptions and notifications.
/// </summary>
internal class ResourceSubscriptionManager(IMcpTransportBase transport)
{
    private readonly ConcurrentDictionary<string, HashSet<string>> subscriptions = new();
    private readonly ConcurrentDictionary<string, IReadOnlyList<ResourceContents>> lastKnownValues =
        new();

    /// <summary>
    /// Subscribes to a resource.
    /// </summary>
    public void Subscribe(string uri, string subscriptionId)
    {
        var subs = subscriptions.GetOrAdd(uri, _ => []);
        lock (subs)
        {
            subs.Add(subscriptionId);
        }
    }

    /// <summary>
    /// Unsubscribes from a resource.
    /// </summary>
    public void Unsubscribe(string uri, string subscriptionId)
    {
        if (subscriptions.TryGetValue(uri, out var subs))
        {
            lock (subs)
            {
                subs.Remove(subscriptionId);
                if (subs.Count == 0)
                {
                    subscriptions.TryRemove(uri, out _);
                    lastKnownValues.TryRemove(uri, out _);
                }
            }
        }
    }

    /// <summary>
    /// Notifies subscribers of a resource update.
    /// </summary>
    public async Task NotifyUpdateAsync(
        string uri,
        IReadOnlyList<ResourceContents> contents,
        CancellationToken cancellationToken = default
    )
    {
        if (!subscriptions.TryGetValue(uri, out var subs) || subs.Count == 0)
            return;

        // Check if the value has actually changed
        if (
            lastKnownValues.TryGetValue(uri, out var lastValue)
            && ContentsEqual(lastValue, contents)
        )
            return;

        // Update last known value
        lastKnownValues[uri] = contents;

        // Send notifications to all subscribers
        var notification = new ResourceUpdatedNotification
        {
            Params = new ResourceUpdatedNotification.Parameters { Uri = uri },
        };

        await transport.WriteMessageAsync(notification, cancellationToken);
    }

    /// <summary>
    /// Gets all subscription IDs for a resource.
    /// </summary>
    public IReadOnlySet<string> GetSubscriptions(string uri)
    {
        if (subscriptions.TryGetValue(uri, out var subs))
        {
            lock (subs)
            {
                return new HashSet<string>(subs);
            }
        }
        return new HashSet<string>();
    }

    /// <summary>
    /// Checks if a resource has any subscribers.
    /// </summary>
    public bool HasSubscribers(string uri) =>
        subscriptions.TryGetValue(uri, out var subs) && subs.Count > 0;

    private static bool ContentsEqual(
        IReadOnlyList<ResourceContents> a,
        IReadOnlyList<ResourceContents> b
    ) => a.Count == b.Count && a.Select((content, i) => ContentEqual(content, b[i])).All(x => x);

    private static bool ContentEqual(ResourceContents a, ResourceContents b) =>
        a.MimeType == b.MimeType && a.Uri == a.Uri;
}
