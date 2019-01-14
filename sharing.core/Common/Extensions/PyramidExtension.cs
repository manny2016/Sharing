
namespace Sharing.Core
{
    using Sharing.Core.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class PyramidExtension
    {

        public static ISharedPyramid BuildSharedPyramid(
            this IList<ISharedContext> context,
            IWxUserKey basic,
            out long basicWxUserId,
            int levelLimit = WeChatConstant.AllowSharedPyramidLevel)
        {
            var basicSharedContext = context.FirstOrDefault(o => o.Id.Equals(basic.Id));
            var invitedBy = basicSharedContext.InvitedBy;
            basicWxUserId = basicSharedContext.Id;
            if (invitedBy == null)
                return null;
            else
            {
                var pyramid = new SharedPyramid() { Id = invitedBy ?? 0, Level = 1, Parent = null };
                int level = 1;
                var lastPyramid = pyramid;
                while (level < levelLimit)
                {
                    level++;
                    var parent = context.FirstOrDefault(o => o.InvitedBy == lastPyramid.Id);
                    if (parent == null)
                        break;
                    else
                    {
                        var parentPyramid = new SharedPyramid() { Id = parent.Id, Level = level, Parent = null };
                        lastPyramid.Parent = parentPyramid;
                        lastPyramid = parentPyramid;
                    }
                }
                return pyramid;
            }

        }

        public static ISharedPyramid BuildSharedPyramid(
            this IList<ISharedContext> context,
            IWxUserKey basic)
        {
            return BuildSharedPyramid(context, basic, out long basicWxUserId);
        }
    }
}
