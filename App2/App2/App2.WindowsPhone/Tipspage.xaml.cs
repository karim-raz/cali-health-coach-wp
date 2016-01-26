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
    public sealed partial class Tipspage : Page
    {
        SQLiteAsyncConnection conn;
        List<Tips> tips = new List<Tips>();
        List<Tips> t = new List<Tips>();
        public Tipspage()
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

            bool dbExist = await CheckDbAsync("Tips.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }

         
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Tips.db3");
            var query = conn.Table<Tips>();
            tips = await query.ToListAsync();

            string title1res = tips[tips.Count - 1].title1;
            string title2res = tips[tips.Count - 1].title2;
            string title3res = tips[tips.Count - 1].title3;
            
            string desc1res = tips[tips.Count - 1].desc1;
            string desc2res = tips[tips.Count - 1].desc2;
            string desc3res = tips[tips.Count - 1].desc3;


            Tips l = new Tips { title1 = title1res, title2 = title2res, title3 = title3res, desc1 = desc1res, desc2 = desc2res, desc3 = desc3res };
            t.Add(l);
            TipsList.ItemsSource = t;
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
            conn = new SQLiteAsyncConnection("Tips.db3");
            await conn.CreateTableAsync<Tips>();
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

        private void TipsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
