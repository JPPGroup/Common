using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jpp.Common.Razor.Services
{
    public class DownloadService
    {
        private Dictionary<string, MemoryStream> _registeredFiles;

        public DownloadService()
        {
            _registeredFiles = new Dictionary<string, MemoryStream>();
        }

        public void Register(string filename, byte[] data)
        {
            MemoryStream ms;

            if (filename.Contains(".zip"))
            {
                ms = new MemoryStream(data);
            }
            else
            {
                byte[] header = Encoding.UTF8.GetBytes("base64,");
                IEnumerable<byte> rv = header.Concat(data);

                ms = new MemoryStream(rv.ToArray());
            }

            _registeredFiles[filename] = ms;
        }

        public MemoryStream Get(string filename)
        {
            MemoryStream result = _registeredFiles[filename];
            _registeredFiles.Remove(filename);
            return result;
        }
    }
}
