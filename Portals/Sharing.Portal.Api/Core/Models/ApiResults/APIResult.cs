
namespace Sharing.Portal.Api.Models {
	public class APIResult<T>  {
		public bool Success { get; set; }
		public T Data { get; set; }
		public string Message { get; set; }
	}
}
