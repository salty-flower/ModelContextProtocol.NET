using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Server.Contexts;

namespace ModelContextProtocol.NET.Server.Session;

public interface ISessionFacade
{
    public ISessionContext Context { get; }
}
