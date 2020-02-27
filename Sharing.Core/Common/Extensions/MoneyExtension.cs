using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core {
	public static class MoneyExtension {
		public static string MoneyDisplay(this int money) {
			return ((decimal)money / 100).ToString("¥ 0.00 元");
		}
	}
}
