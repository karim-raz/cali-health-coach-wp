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
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class statistique : Page
    {
      
        public class NameValueItem
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
             SQLiteAsyncConnection conn;
        List<ouzen> ouzen = new List<ouzen>();
        public statistique()
        {
            this.InitializeComponent();
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
        }


        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }
        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d’événement décrivant la manière dont l’utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: préparer la page pour affichage ici.

            // TODO: si votre application comporte plusieurs pages, assurez-vous que vous
            // gérez le bouton Retour physique en vous inscrivant à l’événement
            // Événement Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si vous utilisez le NavigationHelper fourni par certains modèles,
            // cet événement est géré automatiquement.
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(Home));
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



      
    }
}





 
 











 





     




      