using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Api.Models
{
    public class ApiResponseModel : ApiResponseModel<object>
    {
        public ApiResponseModel()
        {
        }

        public ApiResponseModel(object data) : base(data)
        {
        }

        public ApiResponseModel(int code, string message) : base(code, message)
        {
        }

        public ApiResponseModel(int code, string message, object data) : base(code, message, data)
        {
        }
    }

    public class ApiResponseModel<T>
    {
        public ApiResponseModel()
        {
        }

        public ApiResponseModel(T data)
        {
            this.Data = data;
        }

        public ApiResponseModel(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public ApiResponseModel(int code, string message, T data) : this(code, message)
        {
            this.Data = data;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public DateTime CurrentTime => DateTime.Now;
    }
}