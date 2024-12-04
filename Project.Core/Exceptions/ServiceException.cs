using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Exceptions
{
    public class ServiceException : _BaseException
    {
        public ServiceException() : base("An error occurred")
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
