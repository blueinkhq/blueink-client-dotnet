using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Helper
{
    public static class FileHelper
    {
        public static string ConvertFileToBase64Encode(string filepath)
        {
            if (String.IsNullOrWhiteSpace(filepath))
                return String.Empty;

            var bytes = System.IO.File.ReadAllBytes(filepath);
            return Convert.ToBase64String(bytes);
        }

        public static async Task DownloadFile(string url)
        {
            using (var client = new HttpClient())
            {
                using (var s = client.GetStreamAsync(url))
                {
                    using (var fs = new FileStream("localfile.jpg", FileMode.OpenOrCreate))
                    {
                        await s.Result.CopyToAsync(fs);
                    }
                }
            }
        }

    } 
}
