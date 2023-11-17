using Microsoft.AspNetCore.Components;

namespace Steeltoe.Client.Components.UIInterfaces
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
