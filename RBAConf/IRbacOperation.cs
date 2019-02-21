namespace RBAConf
{
    public interface IRbacOperation
    {
        bool CheckAccess(string name);
    }
}