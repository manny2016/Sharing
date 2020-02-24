

namespace Sharing.Core
{
    public interface IWorkItemState
    {
        string Name { get;  }
        void Update();
    }
}
