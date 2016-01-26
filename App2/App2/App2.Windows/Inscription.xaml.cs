using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public sealed partial class Inscription : Page
    {
        string gender="Male";
        SQLiteAsyncConnection conn;
        public Inscription()
        {
            this.InitializeComponent();
          
        }

        public async void InscriptionButton()
        {

            if (r1.IsChecked == true)
            {

                gender = "Male";
            }
            if (r2.IsChecked == true)
            {

                gender = "Female";
            }
            string JsonString = "{\"username\":\"" + Username.Text + "\",\"password\":\"" + Password.Password  +"\",\"Gender\":\"" + gender + "\",\"email\":\"" + Email.Text + "\",\"Age\":" + Age.Text + ",\"Weight\":" + Weight.Text + ",\"Height\":" + Height.Text + "}";

                        Uri uri = new Uri("https://api.parse.com/1/users");
                        Debug.WriteLine(JsonString);
                        HttpClient cl = new HttpClient();
                        cl.BaseAddress = new Uri("https://api.parse.com/");
                        cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                        request.Headers.Add("X-Parse-Application-Id", "7EjTEqzbb1zfFWz6BJArdZVKkPag6qUuKTCE7rz3");
                        request.Headers.Add("X-Parse-REST-API-Key", "L3IEKFEazzTxzWVZim6oSVwCTvMPWwiI7IyQYnz3");
                        request.Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await cl.SendAsync(request);
                        var data = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(data);
                        if (data.Contains("error"))
                        {
                            ringprog2.Visibility = Visibility.Collapsed;
                          
                            var parsedObject = JsonConvert.DeserializeObject<JObject>(data);
                            var messageDialog = new Windows.UI.Popups.MessageDialog((String)parsedObject["error"]);
                            await messageDialog.ShowAsync();
                        }
                        else
                        {
                           
                       
                bool dbExist = await CheckDbAsync("User.db3");
                if (!dbExist)
                {
                    await CreateDatabaseAsync4();
                }
                else
                {

                    conn = new SQLiteAsyncConnection("User.db3");
                }
                // await DropTableAsync3();
                var listT = new List<User>();
                ringprog2.Visibility = Visibility.Collapsed;
            
                listT.Add(new User() { username = Username.Text, Age = Age.Text, Height = Height.Text, Weight = Weight.Text, email = Email.Text, Gender = gender });
                // {"Age":4,"Gender":"male","Height":174,"Weight":45,"createdAt":"2015-02-12T12:33:06.494Z","email":"a@a.com","objectId":"277GytFjup","sessionToken":"zCxelN4dq22FAEAOk3SK99oTP","updatedAt":"2015-02-12T12:33:06.494Z","username":"a"}
                await conn.InsertAllAsync(listT);
                foreach (User t in listT)
                {

                    Debug.WriteLine(t.username);

                    Debug.WriteLine(t.Weight);

                    Debug.WriteLine(t.Height);

                }

                Frame.Navigate(typeof(Home));

            
                        }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
      

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ConnectionActivity));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ringprog2.Visibility = Visibility.Visible;
            InscriptionButton();
           
        }
        private async Task CreateDatabaseAsync4()
        {
            conn = new SQLiteAsyncConnection("User.db3");
            await conn.CreateTableAsync<User>();
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
