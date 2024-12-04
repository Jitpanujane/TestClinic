using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Exceptions
{
    public abstract class _BaseException : Exception
    {
        protected _BaseException()
        {
        }

        protected _BaseException(string message) : base(message)
        {
        }

        protected _BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected _BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
