using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Stat : Page
    {

        public class NameValueItem
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
        SQLiteAsyncConnection conn;
        List<ouzen> ouzen = new List<ouzen>();
        public Stat()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            this.Loaded += MainPage_Loaded;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }







        private async void LoadChartContents()
        {


            bool dbExist = await CheckDbAsync("ouzen.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }

            // Get personnes
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("ouzen.db3");
            var query = conn.Table<ouzen>();
            ouzen = await query.ToListAsync();


            for (int i = 0; i < ouzen.Count; i++) Debug.WriteLine(ouzen[i].date1);

            //    items.Add(new ouzen { Name =q , Value = _random.Next(35, 150) });

            ((LineSeries)this.LineChart.Series[0]).ItemsSource = ouzen;
            ((LineSeries)this.LineChart.Series[0]).DependentRangeAxis =
              new LinearAxis
              {
                  Minimum = 35,
                  Maximum = 150,
                  Orientation = AxisOrientation.Y,
                  Interval = 5,
                  ShowGridLines = true
              };
            ((LineSeries)this.LineChart.Series[0]).Refresh();
            /* Random _random = new Random();
             List<NameValueItem> items = new List<NameValueItem>();
             items.Add(new NameValueItem { Name = , Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test2", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test3", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test4", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test5", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test6", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test7", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test8", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test9", Value = _random.Next(35, 150) });
             items.Add(new NameValueItem { Name = "Test10", Value = _random.Next(35, 150) });



             ((LineSeries)this.LineChart.Series[0]).ItemsSource = items;*/

        }







        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }









        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

        }







        private async Task CreateDatabaseAsync()
        {
            conn = new SQLiteAsyncConnection("ouzen.db3");
            await conn.CreateTableAsync<Nutrition>();
        }




        private async Task<bool> CheckDbAsync(string dbName)
        {
            bool dbExist = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }




        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.Navigate(typeof(Home));
                e.Handled = true;
            }
        }
    }
}
