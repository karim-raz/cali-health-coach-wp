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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogDiet2 : Page
    {
        SQLiteAsyncConnection conn;
        List<Nutrition> nutritions = new List<Nutrition>();
       
        public LogDiet2()
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

            bool dbExist = await CheckDbAsync("Nut.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }

            // Get personnes
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Nut.db3");
            var query = conn.Table<Nutrition>();
            nutritions = await query.ToListAsync();
          /*  foreach(var n in nutritions)
            {
                DietsList2.ItemsSource = n.name;

            }*/
            // Show users
            DietsList2.ItemsSource = nutritions;
        }

     

        private void DietsList2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("clickiit");
            Nutrition d = (Nutrition)DietsList2.SelectedItem;
            Debug.WriteLine(d.name);
            TempDiets.NutSelected = d.name;
            TempDiets.NutCategory = d.category;
            TempDiets.IDNutSelected = d.Id;
            Frame.Navigate(typeof(LogDiet3));

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
            conn = new SQLiteAsyncConnection("Nut.db3");
            await conn.CreateTableAsync<Nutrition>();
        }

        private void textBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Nutrition> nutritions2 = new List<Nutrition>();
            Debug.WriteLine(searchtxt.Text.ToString());
            
            foreach (var n in nutritions)
            {
                if (n.name.ToLower().Contains(searchtxt.Text.ToString().ToLower()))
                {
                    nutritions2.Add(n);

                }

            }
            if (nutritions2.Count() == 0)
            {
                DietsList2.ItemsSource = nutritions;
            }
            else
            {
                DietsList2.ItemsSource = nutritions2;
            }

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
                frame.Navigate(typeof(LogDiet1));
                e.Handled = true;
            }
        }
    }
}
