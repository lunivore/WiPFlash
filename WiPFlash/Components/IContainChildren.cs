#region

using WiPFlash.Framework;

#endregion

namespace WiPFlash.Components
{
    public interface IContainChildren
    {
        FailureToFindHandler HandlerForFailingToFind { get; set; }
    }
}