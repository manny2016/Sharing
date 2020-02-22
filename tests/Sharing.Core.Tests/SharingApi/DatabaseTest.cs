

using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharing.Core;
using Sharing.Core.Models;
using Sharing.WeChat.Models;
using Microsoft.Extensions.DependencyInjection;
using Sharing.Core.Services;
using System.Linq;
namespace Sharing.Core.Tests {
	[TestClass]
	public class DatabaseTest {

		private readonly Guid scenarioId = Guid.NewGuid();
		private readonly string AppId = "e999292b01fa4258b6ead9ec8bf27d2e";
		private readonly string mCode = "5cafd148bde3466fa586bae1ddb3d50b";
		private readonly string originalId = Guid.NewGuid().ToString().Replace("-", string.Empty);
		
	}
}
