namespace Sharing.Core
{
    using System;

    public class ExceptionTransientFaultDetecter
        : ITransientFaultDetecter<Exception>
    {
        protected Func<Exception, bool> DetectFunction;
        public ExceptionTransientFaultDetecter(
            Func<Exception, bool> detect)
        {
            this.DetectFunction = detect;
        }

        public virtual bool Detect(Exception condition)
        {
            if (this.DetectFunction == null)
            {
                return true;
            }
            else
            {
                return this.DetectFunction(condition);
            }
        }
    }
}