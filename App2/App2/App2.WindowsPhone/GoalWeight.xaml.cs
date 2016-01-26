using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GoalWeight : Page
    {
        SQLiteAsyncConnection conn;
        List<User> user = new List<User>();
        string heights;
        string weights;
        int index1;
        int interpret;
        public GoalWeight()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            List<Weightclass> l = new List<Weightclass>();
           
            for (int i = 30; i <= 200; i++)
            {
                l.Add(new Weightclass() { Weight = i + " Kg" });
              
            }
           

            WeightList.DataContext = l;
          
           
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            bool dbExist = await CheckDbAsync("User.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }

            // Get personnes
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("User.db3");
            var query = conn.Table<User>();
            user = await query.ToListAsync();

            heights = user[user.Count - 1].Height;
            weights = user[user.Count - 1].Weight;
           
            TempGoal.goalweight =weights;


           
            float heigh1 = float.Parse(heights, CultureInfo.InvariantCulture.NumberFormat); 
            float wished = float.Parse(TempGoal.goalweight, CultureInfo.InvariantCulture.NumberFormat);
            float result = calculateBMI(wished, heigh1);

            float resultfinal = result * 10000;
            
            interpret = interpretBMI(resultfinal);
          
            switch (interpret)
            {
                case 1:
                    BitmapImage bp = new BitmapImage(new Uri("ms-appx:///a1.png"));

                    bmipic.Source = bp;

                    break;
                case 2:
                    BitmapImage bp2 = new BitmapImage(new Uri("ms-appx:///a2.png"));

                    bmipic.Source = bp2;
                    break;
                case 3:
                    BitmapImage bp3 = new BitmapImage(new Uri("ms-appx:///a3.png"));

                    bmipic.Source = bp3;
                    break;
                case 4:
                    BitmapImage bp4 = new BitmapImage(new Uri("ms-appx:///a4.png"));

                    bmipic.Source = bp4;
                    break;
                case 5:
                    BitmapImage bp5 = new BitmapImage(new Uri("ms-appx:///a5.png"));

                    bmipic.Source = bp5;
                    break;
                case 6:
                    BitmapImage bp6 = new BitmapImage(new Uri("ms-appx:///a6.png"));

                    bmipic.Source = bp6;
                    break;
                case 7:
                    BitmapImage bp7 = new BitmapImage(new Uri("ms-appx:///a7.png"));

                    bmipic.Source = bp7;
                    break;
                case 8:
                    BitmapImage bp8 = new BitmapImage(new Uri("ms-appx:///a8.png"));

                    bmipic.Source = bp8;
                    break;
                case 9:
                    BitmapImage bp9 = new BitmapImage(new Uri("ms-appx:///a9.png"));

                    bmipic.Source = bp9;
                    break;
            }
        }

      

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GoalWeight2));
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

        private void WeightList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            

            Weightclass temp = (Weightclass)WeightList.SelectedItem;
            string goal = temp.Weight.ToString();
            string[] mot = goal.Split(' ');
            TempGoal.goalweight = mot[0];
            Debug.WriteLine(TempGoal.goalweight);
            float heigh1 = float.Parse(heights, CultureInfo.InvariantCulture.NumberFormat); ;
          float wished=  float.Parse(TempGoal.goalweight, CultureInfo.InvariantCulture.NumberFormat);
          float result = calculateBMI(wished, heigh1);

            float resultfinal = result * 10000;
            interpret = interpretBMI(resultfinal);
            switch (interpret)
            {
                case 1:
                     BitmapImage bp = new BitmapImage(new Uri("ms-appx:///a1.png"));

            bmipic.Source = bp;
                  
                    break;
                case 2:
                    BitmapImage bp2 = new BitmapImage(new Uri("ms-appx:///a2.png"));

            bmipic.Source = bp2;
                    break;
                case 3:
                    BitmapImage bp3 = new BitmapImage(new Uri("ms-appx:///a3.png"));

            bmipic.Source = bp3;
                    break;
                case 4:
                    BitmapImage bp4 = new BitmapImage(new Uri("ms-appx:///a4.png"));

            bmipic.Source = bp4;
                    break;
                case 5:
                  BitmapImage bp5 = new BitmapImage(new Uri("ms-appx:///a5.png"));

            bmipic.Source = bp5;
                    break;
                case 6:
                    BitmapImage bp6 = new BitmapImage(new Uri("ms-appx:///a6.png"));

            bmipic.Source = bp6;
                    break;
                case 7:
                   BitmapImage bp7 = new BitmapImage(new Uri("ms-appx:///a7.png"));

            bmipic.Source = bp7;
                    break;
                case 8:
                    BitmapImage bp8 = new BitmapImage(new Uri("ms-appx:///a8.png"));

            bmipic.Source = bp8;
                    break;
                case 9:
                   BitmapImage bp9 = new BitmapImage(new Uri("ms-appx:///a9.png"));

            bmipic.Source = bp9;
                    break;
            }

           
            
           
        }

        private float calculateBMI(float weight, float height)
        {

            return (float)(weight / (height * height));

        }

        // interpret what BMI means
        private int interpretBMI(double resultfinal)
        {

            if (resultfinal <= 16.0)
            {
                index1 = 1;

            }
            else if (resultfinal > 16.0 && resultfinal <= 17.0)
            {
                index1 = 2;

            }
            else if (resultfinal > 17.0 && resultfinal <= 18.5)
            {
                index1 = 3;
                // return "Normal weight";
            }
            else if (resultfinal > 18.5 && resultfinal <= 20.9)
            {
                index1 = 4;

            }
            else if (resultfinal > 21.0 && resultfinal <= 22.9)
            {
                index1 = 5;

            }
            else if (resultfinal > 23.9 && resultfinal <= 24.9)
            {
                index1 = 6;

            }
            else if (resultfinal > 25 && resultfinal <= 29.9)
            {
                index1 = 7;

            }
            else if (resultfinal > 30 && resultfinal <= 34.9)
            {
                index1 = 8;

            }
            else if (resultfinal >= 35)
            {
                index1 = 9;

            }
            return index1;

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
            conn = new SQLiteAsyncConnection("User.db3");
            await conn.CreateTableAsync<User>();
        }

        private void WeightList_DragLeave(object sender, DragEventArgs e)
        {
         
        }

        private void WeightList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("aaa");
        }

        private void WeightList_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Debug.WriteLine("aaa");
        }
    }

}
