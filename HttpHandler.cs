using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace TestBunifu
{
    internal class HttpHandler
    {
        private HttpClient client;

        public HttpHandler()
        {
            client = new HttpClient();
        }

        public async Task<byte[]> DownloadFileAsync(string url, IProgress<Tuple<int, string>> progress = null)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadProgressChanged += (sender, e) =>
                    {
                        progress?.Report(new Tuple<int, string>(e.ProgressPercentage, e.UserState.ToString()));
                    };

                    // Download the file from the specified URL
                    byte[] fileBytes = await webClient.DownloadDataTaskAsync(url);

                    // Return the downloaded file as byte array
                    return fileBytes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file from {url}: {ex.Message}");
                return null;
            }
        }


    }
}
