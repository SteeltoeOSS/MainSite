using Microsoft.AspNetCore.Components;

namespace Steeltoe.Client.UIInterfaces
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}