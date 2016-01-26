using SQLite;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        SQLiteAsyncConnection conn;
        List<Prog> p = new List<Prog>();
        List<calories> cal = new List<calories>();
        public Home()
        {
           
            this.InitializeComponent();
          Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
      
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            bool dbExist = await CheckDbAsync("Prog.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }

            // Get personnes
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Prog.db3");
            var query = conn.Table<Prog>();
            p = await query.ToListAsync();
            if (p.Count == 0)
            {
                
                grid2.Visibility = Visibility.Collapsed;
                grid1.Visibility = Visibility.Visible;
               
            }
            if (p.Count != 0)
            {
                grid1.Visibility = Visibility.Collapsed;
                grid2.Visibility = Visibility.Visible;
            }

            bool dbExist1 = await CheckDbAsync("calories.db3");
            if (!dbExist1)
            {
                await CreateDatabaseAsync1();
            }

            // Get personnes
            SQLiteAsyncConnection conn1 = new SQLiteAsyncConnection("calories.db3");
            var query1 = conn1.Table<calories>();
            cal = await query1.ToListAsync();

            if (cal.Count == 0)
            {

                messagediet.Message = "you logged 0 from 2500 Calories";
            }
            if (cal.Count != 0)
            {

                messagediet.Message = "you logged " + cal[cal.Count-1].totalcalorie +" from 2500 Calories";
            }




        }






        private void MediumTileIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogDiet1));
        }

        private void MediumTileIcon_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RunPage));
        }

        private void MediumTileIcon_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Tipspage));
        }

        private void MediumTileIcon_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GoalWeight));
        }

        private void MediumTileIcon_Tapped_4(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(WeightIn));
        }

        private void MediumTileIcon_Tapped_5(object sender, TappedRoutedEventArgs e)
        {
            //
            Frame.Navigate(typeof(Stat));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));


        }

        
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (!e.Handled && Frame.CurrentSourcePageType.FullName == "App2.Home")
                Application.Current.Exit();

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

        private async Task CreateDatabaseAsync()
        {
            conn = new SQLiteAsyncConnection("Prog.db3");
            await conn.CreateTableAsync<Prog>();
        }
        private async Task CreateDatabaseAsync1()
        {
            conn = new SQLiteAsyncConnection("calories.db3");
            await conn.CreateTableAsync<calories>();
        }

       
    }
}
