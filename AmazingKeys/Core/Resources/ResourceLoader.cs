using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace AmazingKeys.Core.Resources
{
    public class ResourceLoader
    {
        #region Public Methods

        public event System.EventHandler onProxyChanged;

        private LoadedProxy tempProxy { get; set; }

        public LoadedProxy Proxy
        {
            get
            {
                return tempProxy;
            }

            set
            {
                if (tempProxy != value)
                {
                    tempProxy = value;
                    onProxyChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public static bool Load(ResourceLoader loader, MainWindow window)
        {
            bool success = false;
            using (OpenFileDialog ofd = new OpenFileDialog() { Title = "Select Proxylist (Text File)" ,Filter = "Text Files | *.txt" })
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (File.Exists(ofd.FileName))
                    {
                        loader.onProxyChanged += (s, g) =>
                        {
                            // On
                            Action act1 = new Action(() =>
                            {
                                window.btnStartScrape.IsEnabled = true;
                                window.LB_TotalProxies.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                window.LB_TotalProxies.Content = $"Proxies: {loader.Proxy.ProxyLength}";
                            });
                            window.BeginInvoke(act1, System.Windows.Threading.DispatcherPriority.Normal);
                        };

                        loader.Proxy = new ResourceLoader.LoadedProxy(ofd.FileName);
                        {
                            success = true;
                        }
                    }
                }
            }
            return success;
        }

        public class LoadedProxy
        {
            #region Public Constructors

            public LoadedProxy(string path) => Path = path;

            #endregion Public Constructors

            #region Public Properties

            private string _path { get; set; }

            public string Path
            {
                get
                {
                    return _path;
                }

                set
                {
                    if (value != _path)
                    {
                        _path = value;
                        if (!string.IsNullOrEmpty(Path) && File.Exists(Path))
                        {
                            string[] lines = File.ReadAllLines(Path);
                            ProxyLength = lines.Length;
                            ProxyLines = lines;
                        }
                    }
                }
            }

            public int ProxyLength { get; private set; }

            public string[] ProxyLines { get; set; }

            public WebProxy GetProxyByIndex(int index)
            {
                if (ProxyLength < index) { return null; }
                string IP = ProxyLines.ElementAt(index).Split(':')[0];
                int Port = -1;
                int.TryParse(ProxyLines.ElementAt(index).Split(':')[1], out Port);
                return new WebProxy(IP, Port);
            }

            #endregion Public Properties
        }

        #endregion Public Methods
    }
}