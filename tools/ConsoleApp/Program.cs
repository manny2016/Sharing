using Sharing.Core.Configuration;
using System;
using System.Configuration;
using Sharing.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Sharing.WeChat.Models;
using System.Threading;
using Sharing.Core.Models;
using Sharing.Core.Services;
using Sharing.Core.CMQ;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp {
	class Program {
		static ServiceCollection collection = new ServiceCollection();
		static void Main(string[] args) {
			var sorted = new SortedDictionary<int, string>();
			var text = "ABCDEFGHIJKLMN";
			for ( int i = 0; i < text.Length; i++ ) {
				sorted.Add(i, text[text.Length - i - 1].ToString());

			}
			foreach ( var x in sorted.Select(x => x.Value) ) {
				Console.WriteLine(x);
			}

		}

		static ProductModel GenernateProductModel(int id, string productName, int categoryId, int defaultPrice, Specification[] specifications) {
			return new ProductModel() {
				Name = productName,
				Id = id,
				MerchantId = 1,
				CategoryId = categoryId,
				Price = defaultPrice,
				ImageUrl = string.Empty,
				SalesVol = 0,
				Options = (new {
					banners = new string[] { },
					specifications = specifications
				}).SerializeToJson(),
				SortNo = 1,
				Enabled = true
			};
		}
	}
}
