


namespace Sharing.Portal.Api.Filters {
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Sharing.Core;
	public class ExceptionFilter : IExceptionFilter {
		public void OnException(ExceptionContext context) {
			context.ExceptionHandled = true;
			context.HttpContext.Response.StatusCode = 200;
			context.HttpContext.Response.ContentType = "application/json";
			var result = new {
				Sucess = false,
				Message = $"'{context.Exception.GetType().Name}':{context.Exception.Message}",
				StatusCodes = 500,
			};
			context.HttpContext.Response.WriteAsync(result.SerializeToJson());
		}
	}
}
