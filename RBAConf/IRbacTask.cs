using System.Collections.Generic;

namespace RBAConf
{
    public interface IRbacTask: IRbacEntity
    {
        bool CheckAccess(string name, IDictionary<string, string> parameters);
    }
}