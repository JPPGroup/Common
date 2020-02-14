using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jpp.Common.Razor
{
    public class FileUploadBase : ComponentBase
    {
        public const int BUFFER_SIZE = 4096;

        [Parameter]
        public byte[] File { get; set; }

        [Parameter]
        public EventCallback<FileUploadEventArgs> OnUpload { get; set; }

        [Inject]
        public IJSRuntime Runtime { get; set; }

        [Parameter]
        public bool Uploading { get; set; } = false;

        public bool NotUploading { get { return !Uploading; } }

        public int ProgressPercentage { get; set; }

        public bool UploadComplete { get; set; }

        public bool Paused { get; set; }

        public bool UploadButtonHidden { get; set; } = true;

        private CancellationTokenSource _cts;
        private CancellationToken _ct;

        private int _currentByte, _fileSize;

        private string _filename;

        public FileUploadBase()
        {            
        }

        public async void UploadFile()
        {
            UploadButtonHidden = true;
            UploadComplete = false;
            _cts = new CancellationTokenSource();
            _fileSize = await Runtime.InvokeAsync<int>("getFileSize", "fileUploadPicker");
            _filename = await Runtime.InvokeAsync<string>("getFileName", "fileUploadPicker");
            File = new byte[_fileSize];
            _currentByte = 0;
            Uploading = true;

            Upload();
        }

        public void Pause()
        {
            Paused = true;
            _cts.Cancel();
        }

        public void Resume()
        {
            Paused = false;
            _cts = new CancellationTokenSource();
            Upload();
        }

        public async void Cancel()
        {
            _cts.Cancel();
            Uploading = false;
            await Runtime.InvokeAsync<object>("clearInput", "fileUploadPicker");
        }

        private void Upload()
        {
            _ct = _cts.Token;
            Task.Run(async () =>
            {                                
                while (Uploading)
                {
                    if (_ct.IsCancellationRequested)
                    {
                        return;
                    }

                    await ReadChunk();

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
                        FileUploadEventArgs args = new FileUploadEventArgs()
                        {
                            Filename = _filename,
                            Data = File
                        };
                        OnUpload.InvokeAsync(args);
                    }                    
                });
            });
        }

        private async Task ReadChunk()
        {
            int toBeRead = BUFFER_SIZE;

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
        }

        public void FileChanged()
        {
            UploadComplete = false;
            UploadButtonHidden = false;
        }
    }

    public struct FileUploadEventArgs
    {
        public string Filename { get; set; }
        public byte[] Data { get; set; }
    }
}
