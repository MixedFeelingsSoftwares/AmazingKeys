using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AmazingKeys.Core.Scrape;
using AmazingKeys.Core.Resources;
using AmazingKeys.Core.Objects;
using AmazingKeys.Core.Information;
using System.Threading;
using System.ComponentModel;
using System.Net;

namespace AmazingKeys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Public Constructors

        public static ResourceLoader loader = new ResourceLoader();

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void BtnStartScrape_Click(object sender, RoutedEventArgs e)
        {
            Scraper.StartScrape(this);
        }

        #endregion Private Methods

        private void BtnLoadProxies_Click(object sender, RoutedEventArgs e)
        {
            ResourceLoader.Load(loader, this);
        }

        private void MainFrm_Loaded(object sender, RoutedEventArgs e)
        {
            StatusInformation.Status.window = this;
        }
    }
}