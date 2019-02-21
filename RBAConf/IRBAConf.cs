using System.Collections.Generic;

namespace RBAConf
{
    public interface IRbaConf
    {
        bool CheckAccess(string name);
        bool CheckAccess(string name, IDictionary<string, string> parameters);
    }
}
