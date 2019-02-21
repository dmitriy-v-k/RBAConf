namespace RBAConf
{
    public sealed class RbacOperation : IRbacOperation
    {
        private readonly string operationName;

        public RbacOperation(string operationName)
        {
            this.operationName = operationName;
        }

        public bool CheckAccess(string name)
        {
            return operationName == name;
        }
    }
}
