using AmazingKeys.Core.Information;
using AmazingKeys.Core.Objects;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKeys.Core.Scrape
{
    public class Scraper
    {
        #region Public Properties

        public static string GenerateRandomPastebin
        {
            get
            {
                return $@"http://pastebin.com/{RandomIdGenerator.GetBase62(8)}";
            }
        }

        private static void onTotalChanged(object sender, EventArgs e)
        {
            MainWindow window = StatusInformation.Status.window;
            Action act = new Action(() =>
            {
                int num = StatusInformation.Status.Success;
                window.LB_TotalSuccess.Content = $"Success: {num}";

                int num1 = StatusInformation.Status.Errors;
                window.LB_TotalFailed.Content = $"Failed: {num1}";
            });

            window.BeginInvoke(act, System.Windows.Threading.DispatcherPriority.Normal);
        }

        private static void WebRequestCallback(IAsyncResult result)
        {
            try
            {
                Classes.RequestCallback callback = (Classes.RequestCallback)result.AsyncState;
                WebResponse resp = callback.result.EndGetResponse(result);
                Console.WriteLine(resp.ResponseUri.AbsoluteUri);
                StatusInformation.Status.Success++;
            }
            catch
            {
                StatusInformation.Status.Errors++;
            }
        }

        public static void StartScrape(MainWindow window)
        {
            // Disable Blur Effect on Listview when proxy is selected
            window.Canvas_Listview.Effect = null;

            StatusInformation.Status.onTotalChanged += onTotalChanged;
            window.btnStartScrape.Content = "Stop";
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += async (s, g) =>
            {
                for (int i = 0; i < MainWindow.loader.Proxy.ProxyLength; i++)
                {
                    string url = AmazingKeys.Core.Scrape.Scraper.GenerateRandomPastebin;
                    WebRequest request = WebRequest.Create(url);
                    request.Method = "GET";
                    request.Timeout = 5000;
                    WebProxy prox = MainWindow.loader.Proxy.GetProxyByIndex(i);
                    request.Proxy = prox;
                    await Task.Delay(100);
                    Classes.RequestCallback cb = new Classes.RequestCallback()
                    {
                        result = request,
                        usedProxy = prox,
                        URL = url,
                    };
                    request.BeginGetResponse(WebRequestCallback, cb);
                }
            };

            worker.RunWorkerCompleted += (s, g) =>
            {
                Action act = new Action(() =>
                {
                    window.btnStartScrape.Content = "Start";
                });
                window.btnStartScrape.BeginInvoke(act, System.Windows.Threading.DispatcherPriority.Normal);
            };
            worker.RunWorkerAsync();
        }

        #endregion Public Properties

        #region Public Classes

        public static class RandomIdGenerator
        {
            #region Private Fields

            private static char[] _base62chars =
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
                .ToCharArray();

            private static Random _random = new Random();

            #endregion Private Fields

            #region Public Methods

            public static string GetBase36(int length)
            {
                var sb = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                    sb.Append(_base62chars[_random.Next(36)]);

                return sb.ToString();
            }

            public static string GetBase62(int length)
            {
                var sb = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                    sb.Append(_base62chars[_random.Next(62)]);

                return sb.ToString();
            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}