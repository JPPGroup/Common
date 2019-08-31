using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jpp.Common.Razor
{
    public class FileUploadBase : ComponentBase
    {
        public const int BufferSize = 4096;

        [Parameter]
        public byte[] File { get; set; }

        [Parameter] 
        protected EventCallback<byte[]> OnUpload { get; set; }

        [Inject]
        public IJSRuntime Runtime { get; set; }

        [Parameter]
        public bool Uploading { get; set; } = false;

        public bool NotUploading { get { return !Uploading; } }

        public int ProgressPercentage { get; set; }

        public bool UploadComplete { get; set; }

        public bool Paused { get; set; }

        public bool UploadButtonHidden { get; set; } = true;

        private CancellationTokenSource cts;
        private CancellationToken ct;

        private int _currentByte, _fileSize;

        public FileUploadBase()
        {            
        }

        public async void UploadFile()
        {
            UploadButtonHidden = true;
            UploadComplete = false;
            cts = new CancellationTokenSource();
            _fileSize = await Runtime.InvokeAsync<int>("getFileSize", "fileUploadPicker");
            File = new byte[_fileSize];
            _currentByte = 0;
            Uploading = true;

            Upload();
        }

        public void Pause()
        {
            Paused = true;
            cts.Cancel();
        }

        public void Resume()
        {
            Paused = false;
            cts = new CancellationTokenSource();
            Upload();
        }

        public async void Cancel()
        {
            cts.Cancel();
            Uploading = false;
            await Runtime.InvokeAsync<object>("clearInput", "fileUploadPicker");
        }

        private void Upload()
        {
            ct = cts.Token;
            Task.Run(async () =>
            {                                
                while (Uploading)
                {
                    if(ct.IsCancellationRequested)
                    {
                        return;
                    }

                    int toBeRead = BufferSize;

                    if (toBeRead > _fileSize - _currentByte)
                    {
                        toBeRead = _fileSize - _currentByte;
                        Uploading = false;
                        UploadComplete = true;
                    }

                    string buffer = await Runtime.InvokeAsync<string>("getSlice", "fileUploadPicker", _currentByte, toBeRead);
                    buffer = buffer.Replace("data:application/octet-stream;base64,", "");
                    byte[] byteBuffer = Convert.FromBase64String(buffer);

                    byteBuffer.CopyTo(File, _currentByte);

                    _currentByte = _currentByte + toBeRead;

                    _ = InvokeAsync(() =>
                    {
                        double p = (double)_currentByte / (double)_fileSize * 100d;
                        ProgressPercentage = (int)Math.Round(p);
                        StateHasChanged();
                    });
                }               

                _ = InvokeAsync(() =>
                {                    
                    if(UploadComplete)
                    {
                        OnUpload.InvokeAsync(File);
                    }
                    StateHasChanged();
                });
            });
        }

        public void FileChanged()
        {
            UploadComplete = false;
            UploadButtonHidden = false;
        }
    }
}
