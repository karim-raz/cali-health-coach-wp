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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogDietFinal : Page
    {
        SQLiteAsyncConnection conn;
        List<calories> cal = new List<calories>();
        public LogDietFinal()
        {
            this.InitializeComponent();
                  }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DateTime thisDay = DateTime.Today;

            String[] année = thisDay.ToString("D").Split(',');

            string ann = année[1] + année[2];
            Debug.WriteLine(ann);
            today_value.Text = ann;
            total_value.Text = TempDiets.DetSelected.ToString() + " Calories";
            nut_name.Text = TempDiets.dietSelected.ToString();
            List<FinalList> l = new List<FinalList>();
            for (int i =0; i<TempDiets.NutsSelected.Count();i++) { 
            l.Add(new FinalList() { diet = TempDiets.NutsSelected[i],val = TempDiets.DetsSelected[i]+ " Calories" });

        }
            DietsFinalList.DataContext = l;
        
    }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogDiet2));
        }


        //submit
        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            bool dbExist = await CheckDbAsync("calories.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync1();
            }
            else
            {

                conn = new SQLiteAsyncConnection("calories.db3");
            }


            var query1 = conn.Table<calories>();
            cal = await query1.ToListAsync();
            if (cal.Count == 0)
            {
                calories cal1 = new calories();
                cal1.totalcalorie = TempDiets.DetSelected;
                await conn.InsertAsync(cal1);

            }
            if (cal.Count != 0)
            {
                calories cal1 = new calories();
                cal1.totalcalorie = TempDiets.DetSelected + cal[cal.Count - 1].totalcalorie;
                await conn.InsertAsync(cal1);

            }
           

            Frame.Navigate(typeof(Home));
            TempDiets.DetSelected = 0;
            TempDiets.DetsSelected = new List<string>();
            TempDiets.dietSelected = "";
            TempDiets.IDNutSelected = "";
            TempDiets.NutCategory = "";
            TempDiets.NutSelected = "";
            TempDiets.NutsSelected = new List<string>();

        }
        //remove items
        private void DietsFinalList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
   /*     private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.Navigate(typeof(Home));
                TempDiets.DetSelected =0;
                TempDiets.DetsSelected = new List<string>();
                TempDiets.dietSelected = "";
                TempDiets.IDNutSelected = "";
                TempDiets.NutCategory = "";
                TempDiets.NutSelected = "";
                TempDiets.NutsSelected = new List<string>();
                e.Handled = true;
            }
        }*/

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

       
        private async Task CreateDatabaseAsync1()
        {
            conn = new SQLiteAsyncConnection("calories.db3");
            await conn.CreateTableAsync<calories>();
        }

 

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(Home));
            TempDiets.DetSelected = 0;
            TempDiets.DetsSelected = new List<string>();
            TempDiets.dietSelected = "";
            TempDiets.IDNutSelected = "";
            TempDiets.NutCategory = "";
            TempDiets.NutSelected = "";
            TempDiets.NutsSelected = new List<string>();
        }

    }
}
