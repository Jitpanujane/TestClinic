using Project.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Project.Api.Controllers.Api
{
    public abstract class _BaseController : ApiController
    {
        protected _BaseController()
        {
            if (User?.Identity != null)
            {
                CurrentUser = User.Identity as IdentityModel;
            }
        }

        protected IdentityModel CurrentUser { get; }

        new protected OkNegotiatedContentResult<ApiResponseModel<object>> Ok()
        {
            return Ok(default(object));
        }

        new protected OkNegotiatedContentResult<ApiResponseModel<T>> Ok<T>(T content)
        {
            return base.Ok(new ApiResponseModel<T>(0, "Success", content));
        }
    }
}
