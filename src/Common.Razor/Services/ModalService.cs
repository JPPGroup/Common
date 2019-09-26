using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Jpp.Common.Razor.Services
{
    public class ModalService
    {
        public event Action<string, RenderFragment> OnShow;
        public event Action OnClose;

        public async void Show(string title, Type contentType)
        {
            if (contentType.BaseType != typeof(ComponentBase))
            {
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
            }

            var content = new RenderFragment(x => { x.OpenComponent(1, contentType); x.CloseComponent(); });
            OnShow?.Invoke(title, content);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}
