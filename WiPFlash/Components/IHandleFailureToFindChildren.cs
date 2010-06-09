#region

using WiPFlash.Framework;

#endregion

namespace WiPFlash.Components
{
    public interface IHandleFailureToFindChildren
    {
        FailureToFindHandler HandlerForFailingToFind { get; set; }
    }
}