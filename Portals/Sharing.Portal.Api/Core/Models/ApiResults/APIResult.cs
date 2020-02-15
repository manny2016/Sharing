
namespace Sharing.Portal.Api.Models {
	public class APIResult<T> where T : class {
		public bool Sucess { get; set; }
		public T Data { get; set; }
		public string Message { get; set; }
	}
}
