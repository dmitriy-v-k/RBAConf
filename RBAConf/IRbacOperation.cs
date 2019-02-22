namespace RBAConf
{
    public interface IRbacOperation: IRbacEntity
    {
        bool CheckAccess(string name);
    }
}