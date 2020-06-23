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

        public async Task<ModalResult> ShowAsync(string title, Type contentType, params KeyValuePair<string, object>[] attributes)
        {
            Show(title, contentType, attributes);
            _awaitFlag = new TaskCompletionSource<bool>();
            
            bool modalSuccess = await _awaitFlag.Task;
            _result.Success = modalSuccess; 
            return _result;
        }

        private ModalResult _result;
        

        public void Close(bool success = false, params KeyValuePair<string,object>[] Results)
        {
            _result = new ModalResult();
            foreach (KeyValuePair<string, object> keyValuePair in Results)
            {
                _result.Results.Add(keyValuePair.Key, keyValuePair.Value);
            }
            OnClose?.Invoke();
            _awaitFlag?.SetResult(success);
        }
    }

    public class ModalResult
    {
        public bool Success { get; set; }
        public Dictionary<string, object> Results { get; set; } = new Dictionary<string, object>();
    }
}
