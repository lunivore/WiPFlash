using WiPFlash.Framework;

namespace WiPFlash.Components
{
    public interface IHandleFailureToFindChildren
    {
        FailureToFindHandler HandlerForFailingToFind { get; set; }
    }
}