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
using Windows.UI;
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
    public sealed partial class LogDiet3 : Page
    {
        SQLiteAsyncConnection conn;
        List<Details> details = new List<Details>();
        List<DetailsParts> l = new List<DetailsParts>();
        Details resultat;
        public LogDiet3()
        {
            this.InitializeComponent();
             }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            bool dbExist = await CheckDbAsync("Details.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }

            name_diet.Text = TempDiets.NutSelected;
            name_category.Text = TempDiets.NutCategory;
            switch (name_category.Text)
            {
                case "Yellow food":

                    name_category.Foreground = new SolidColorBrush(Colors.LightYellow);
                    break;
                case "Green Food":

                    name_category.Foreground = new SolidColorBrush(Colors.LightGreen);
                    break;
                case "Red Food":

                    name_category.Foreground = new SolidColorBrush(Colors.Red);
                    break;


            }
           
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Details.db3");
            var query = conn.Table<Details>().Where(x => x.Id.Equals(TempDiets.IDNutSelected));
             resultat = await query.FirstOrDefaultAsync();
            
            l.Add(new DetailsParts() { part = resultat.part1, val = resultat.val1+" Calories"  });
            l.Add(new DetailsParts() { part = resultat.part2, val = resultat.val2 + " Calories" });
            l.Add(new DetailsParts() { part = resultat.part3, val = resultat.val3 + " Calories" });
            l.Add(new DetailsParts() { part = resultat.part4, val = resultat.val4 + " Calories" });
           


           
            DietsList3.ItemsSource = l;
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
            conn = new SQLiteAsyncConnection("Details.db3");
            await conn.CreateTableAsync<Nutrition>();
        }

        private void DietsList3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int index = DietsList3.SelectedIndex;
            
            switch(index)
            {

                case 0:
                    TempDiets.DetSelected += Convert.ToInt32(resultat.val1);
                    TempDiets.NutsSelected.Add(TempDiets.NutSelected);
                    TempDiets.DetsSelected.Add(resultat.val1);

                break;
                case 1:
                    TempDiets.DetSelected += Convert.ToInt32(resultat.val2);
                    Debug.WriteLine(TempDiets.NutSelected);
                    TempDiets.NutsSelected.Add(TempDiets.NutSelected);
                    TempDiets.DetsSelected.Add(resultat.val2);
                    break;
                case 2:
                    TempDiets.DetSelected += Convert.ToInt32(resultat.val3);
                    TempDiets.NutsSelected.Add(TempDiets.NutSelected);
                    TempDiets.DetsSelected.Add(resultat.val3);
                    break;
                case 3:
                    TempDiets.DetSelected += Convert.ToInt32(resultat.val4);
                    TempDiets.NutsSelected.Add(TempDiets.NutSelected);
                    TempDiets.DetsSelected.Add(resultat.val4);
                    break;
            }
            Frame.Navigate(typeof(LogDietFinal));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(LogDiet2));

        }

         
    
    }
}
