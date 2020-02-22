using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharing.Portal.Api.Models {
	public class APIResults<T> where T : class {
		public bool Success { get; set; }
		public T[] Data { get; set; }
		public string Message { get; set; }
	}
}
