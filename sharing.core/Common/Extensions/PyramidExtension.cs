
namespace Sharing.Core {
	using Sharing.Core.Models;
	using System.Collections.Generic;
	using System.Linq;

	public static class PyramidExtension {

		public static ISharedPyramid BuildSharedPyramid(
			this IList<ISharedContext> context,
			IWxUserKey basic,
			out long basicWxUserId) {

			var levelLimit = IoC.GetService<WeChatConstant>().AllowSharedPyramidLevel;

			basicWxUserId = 0;
			var basicSharedContext = context.FirstOrDefault(o => o.Id.Equals(basic.Id));
			if ( basicSharedContext == null || basicSharedContext.InvitedBy == null )
				return default(ISharedPyramid);

			basicWxUserId = basicSharedContext.Id;
			//构建第一级 basicSharedContext 本身
			var level = 1;
			var pyramid = new SharedPyramid() {
				Id = basicSharedContext.Id,
				Level = level,
				Parent = null,
				MchId = basic.MerchantId
			};
			var lastInvitedBy = basicSharedContext.InvitedBy ?? 0;
			var lastPyramid = pyramid;
			while ( level <= levelLimit ) {
				level++;
				var sharedContext = context.FirstOrDefault(o => o.Id.Equals(lastInvitedBy));
				if ( sharedContext == null ) {
					lastPyramid.Parent = new SharedPyramid() { Id = lastInvitedBy, Level = level, MchId = basic.MerchantId, Parent = null };
					break;
				}
				var parent = new SharedPyramid() { Id = sharedContext.Id, Level = level, MchId = basic.MerchantId, Parent = null };
				lastPyramid.Parent = parent;
				lastInvitedBy = sharedContext.InvitedBy ?? 0;
				lastPyramid = parent;

			}
			return pyramid;

		}

		public static ISharedPyramid BuildSharedPyramid(
			this IList<ISharedContext> context,
			IWxUserKey basic) {
			return BuildSharedPyramid(context, basic, out long basicWxUserId);
		}
	}
}
