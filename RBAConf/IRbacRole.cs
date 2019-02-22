using System.Collections.Generic;

namespace RBAConf
{
    public interface IRbacRole: IRbacEntity
    {
        bool CheckAccess(string name, IDictionary<string, string> parameters);
        bool CheckAccess(string name);
    }
}
