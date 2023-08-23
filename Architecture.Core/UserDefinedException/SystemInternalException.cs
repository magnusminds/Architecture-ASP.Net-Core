namespace Architecture.Core.UserDefinedException
{
    public class SystemInternalException : Exception
    {
        public SystemInternalException()
        {
        }

        public SystemInternalException(string message)
            : base(message)
        {
        }

        public SystemInternalException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}