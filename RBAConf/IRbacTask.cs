using System.Collections.Generic;

namespace RBAConf
{
    public interface IRbacTask
    {
        bool CheckAccess(string name, IDictionary<string, string> parameters);
    }
}