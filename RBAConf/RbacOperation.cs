namespace RBAConf
{
    public sealed class RbacOperation : IRbacOperation
    {
        public string Name { get; }

        public RbacOperation(string operationName)
        {
            Name = operationName;
        }

        public bool CheckAccess(string name)
        {
            return Name == name;
        }
    }
}
