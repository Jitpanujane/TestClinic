using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Extensions
{
    public static class ExceptionExtension
    {
        public static Exception GetInnerException(this Exception exception)
        {
            return exception.InnerException?.GetInnerException() ?? exception;
        }
    }
}
