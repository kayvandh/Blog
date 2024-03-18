using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.Api.Tools

{
    public class ApiResult<T> : IActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        public bool HasError;
        /// <summary>
        /// 
        /// </summary>
        public string Message;
        /// <summary>
        /// result
        /// </summary>
        public T Result { get; set; }

        private readonly HttpStatusCode httpStatusCode;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="successMessage"></param>
        public ApiResult(T result)
        {
            HasError = false;
            Result = result;
            httpStatusCode = HttpStatusCode.OK;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="httpStatusCode"></param>
        public ApiResult(HttpStatusCode httpStatusCode, string? errorMessage = null)
        {
            this.httpStatusCode = httpStatusCode;
            HasError = true;
            Message = errorMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)httpStatusCode;

            await new ObjectResult(new { Result, HasError, Message }).ExecuteResultAsync(context);
        }
    }
}
