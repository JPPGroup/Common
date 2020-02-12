using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jpp.Common.Razor.Services
{
    public class ModalService
    {
        public event Action<string, RenderFragment> OnShow;
        public event Action OnClose;

        private TaskCompletionSource<bool> _awaitFlag;

        public void Show(string title, Type contentType, params KeyValuePair<string, object>[] attributes)
        {
            if (contentType.BaseType != typeof(ComponentBase))
            {
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
            }

            //Pass parameters
            var content = new RenderFragment(x => {
                x.OpenComponent(1, contentType);
                foreach(KeyValuePair<string, object> attribute in attributes)
                {
                    x.AddAttribute(1, attribute.Key, attribute.Value);
                }
                x.CloseComponent(); });
            OnShow?.Invoke(title, content);
        }

        public async Task<bool> ShowAsync(string title, Type contentType, params KeyValuePair<string, object>[] attributes)
        {
            Show(title, contentType, attributes);
            _awaitFlag = new TaskCompletionSource<bool>();
            return await _awaitFlag.Task;
        }

        public void Close(bool success = false)
        {
            OnClose?.Invoke();
            _awaitFlag.SetResult(success);
        }
    }
}
