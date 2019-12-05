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

        public async void Show(string title, Type contentType, params KeyValuePair<string, string>[] attributes)
        {
            if (contentType.BaseType != typeof(ComponentBase))
            {
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
            }

            //Pass parameters
            var content = new RenderFragment(x => { 
                x.OpenComponent(1, contentType);
                foreach(KeyValuePair<string, string> attribute in attributes)
                {
                    x.AddAttribute(1, attribute.Key, attribute.Value);
                }
                x.CloseComponent(); });
            OnShow?.Invoke(title, content);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}
