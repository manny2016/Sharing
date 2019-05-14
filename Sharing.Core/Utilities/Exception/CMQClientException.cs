using System;


namespace Sharing.Core
{
    using System;
    public class ClientException : ApplicationException
    {
        public ClientException(string message) : base(message) { }

        public override string Message
        {
            get { return base.Message; }

        }
    }
}
