using System.Collections.Generic;

namespace RBAConf
{
    public interface IBizRule
    {
        bool Check(IDictionary<string,string> parameters);
    }
}
