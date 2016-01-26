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
    public sealed partial class GoalWeight2 : Page
    {
        SQLiteAsyncConnection conn;
        public GoalWeight2()
        {
            this.InitializeComponent();
          NavigationCacheMode = NavigationCacheMode.Required;
            List<Programclass> l = new List<Programclass>();
           
                l.Add(new Programclass() { prog = "  Easy" });
                l.Add(new Programclass() { prog = "Average" });
                l.Add(new Programclass() { prog = " Intense" });
            

            programList.DataContext = l;
            
        }

      

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            TempGoal.program = "average";
        }

    
        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {

            bool dbExist = await CheckDbAsync("Prog.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }
            else
            {

                conn = new SQLiteAsyncConnection("Prog.db3");
            }

            Prog p = new Prog();
            p.goalW = TempGoal.goalweight;
            p.program = TempGoal.program;
            await conn.InsertAsync(p);
            Frame.Navigate(typeof(Home));

        }


        private void programList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int index = programList.SelectedIndex;
            switch (index)
            {

                case 0:
                
                    TempGoal.program = "easy";

                    break;
                case 1:
                    TempGoal.program = "average";

                    break;
                case 2:
                      TempGoal.program = "intense";

                    break;
            }


       


    }
        private async Task CreateDatabaseAsync()
        {
            conn = new SQLiteAsyncConnection("Prog.db3");
            await conn.CreateTableAsync<Prog>();
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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(GoalWeight));
        }
    }
}
