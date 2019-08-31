using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jpp.Common.Razor
{
    public class FileUploadBase : ComponentBase
    {
        public const int BufferSize = 4096;

        [Parameter]
        public byte[] File { get; set; }

        [Inject]
        public IJSRuntime Runtime { get; set; }

        [Parameter]
        public bool Uploading { get; set; } = false;

        public bool NotUploading { get { return !Uploading; } }

        public int ProgressPercentage { get; set; }

        public bool UploadComplete { get; set; }

        public FileUploadBase()
        {            
        }

        public void UploadFile()
        {
            UploadComplete = false;
            Task.Run(async () =>
            {
                //File = await _runtime.InvokeAsync<byte[]>("readFile", "");
                int fileSize = await Runtime.InvokeAsync<int>("getFileSize", "fileUploadPicker");
                File = new byte[fileSize];

                int currentByte = 0;

                Uploading = true;

                while (Uploading)
                {
                    int toBeRead = BufferSize;

                    if (toBeRead > fileSize - currentByte)
                    {
                        toBeRead = fileSize - currentByte;
                        Uploading = false;
                    }

                    string buffer = await Runtime.InvokeAsync<string>("getSlice", "fileUploadPicker", currentByte, toBeRead);
                    buffer = buffer.Replace("data:application/octet-stream;base64,", "");
                    byte[] byteBuffer = Convert.FromBase64String(buffer);

                    byteBuffer.CopyTo(File, currentByte);

                    currentByte = currentByte + toBeRead;

                    _ = InvokeAsync(() =>
                      {
                          double p = (double)currentByte / (double)fileSize * 100d;
                          ProgressPercentage = (int)Math.Round(p);
                          StateHasChanged();
                      });
                }                     

                _ = InvokeAsync(() =>
                {
                    UploadComplete = true;
                    StateHasChanged();
                });
            });            
        }
    }
}
