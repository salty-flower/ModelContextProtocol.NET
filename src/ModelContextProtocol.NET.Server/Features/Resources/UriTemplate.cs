using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ModelContextProtocol.NET.Server.Features.Resources;

/// <summary>
/// Handles URI template parsing and matching according to RFC 6570 Level 1.
/// </summary>
internal static partial class UriTemplate
{
    private static readonly Regex parameterRegex = ParameterRegex();
    private static readonly Regex validTemplateRegex = ValidTemplateRegex();

    /// <summary>
    /// Validates a URI template.
    /// </summary>
    public static bool IsValidTemplate(string template)
    {
        if (string.IsNullOrWhiteSpace(template))
            return false;

        // Check for valid URI template format
        if (!validTemplateRegex.IsMatch(template))
            return false;

        // Check for duplicate parameter names
        var paramNames = new HashSet<string>();
        foreach (Match match in parameterRegex.Matches(template))
        {
            if (!paramNames.Add(match.Groups[1].Value))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Gets parameter names from a template.
    /// </summary>
    public static IReadOnlyList<string> GetParameterNames(string template)
    {
        var names = new List<string>();
        foreach (Match match in parameterRegex.Matches(template))
        {
            names.Add(match.Groups[1].Value);
        }
        return names;
    }

    /// <summary>
    /// Checks if a URI matches a template pattern.
    /// </summary>
    public static bool IsMatch(string template, string uri)
    {
        if (!IsValidTemplate(template))
            throw new ArgumentException("Invalid URI template", nameof(template));

        var pattern = parameterRegex.Replace(template, "([^/]+)");
        pattern = $"^{Regex.Escape(pattern).Replace("\\([^/]+\\)", "([^/]+)")}$";
        return Regex.IsMatch(uri, pattern);
    }

    /// <summary>
    /// Extracts parameter values from a URI based on a template pattern.
    /// </summary>
    public static Dictionary<string, string> ExtractParameters(string template, string uri)
    {
        if (!IsValidTemplate(template))
            throw new ArgumentException("Invalid URI template", nameof(template));

        var parameters = new Dictionary<string, string>();
        var parameterNames = GetParameterNames(template);

        // Create regex pattern with capturing groups
        var pattern = parameterRegex.Replace(template, "([^/]+)");
        pattern = $"^{Regex.Escape(pattern).Replace("\\([^/]+\\)", "([^/]+)")}$";

        // Extract values using regex
        var match = Regex.Match(uri, pattern);
        if (match.Success)
        {
            for (int i = 0; i < parameterNames.Count; i++)
            {
                parameters[parameterNames[i]] = Uri.UnescapeDataString(match.Groups[i + 1].Value);
            }
        }

        return parameters;
    }

    /// <summary>
    /// Expands a URI template with the given parameters.
    /// </summary>
    public static string Expand(string template, IDictionary<string, string> parameters)
    {
        if (!IsValidTemplate(template))
            throw new ArgumentException("Invalid URI template", nameof(template));

        // Check for required parameters
        var requiredParams = GetParameterNames(template);
        foreach (var param in requiredParams)
        {
            if (!parameters.ContainsKey(param))
                throw new ArgumentException(
                    $"Missing required parameter: {param}",
                    nameof(parameters)
                );
        }

        return parameterRegex.Replace(
            template,
            match =>
                parameters.TryGetValue(match.Groups[1].Value, out var value)
                    ? Uri.EscapeDataString(value)
                    : match.Value
        );
    }

    [GeneratedRegex(@"{([^{}./]+)}", RegexOptions.Compiled)]
    private static partial Regex ParameterRegex();

    [GeneratedRegex(@"^[^{}]+(\{[^{}./]+\}[^{}]*)*$", RegexOptions.Compiled)]
    private static partial Regex ValidTemplateRegex();
}
