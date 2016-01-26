using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConnectionActivity : Page
    {
      //  NutritionDatabaseHelperClass Db_HelperNut = new NutritionDatabaseHelperClass();
       // DetailsDatabaseHelperClass Db_HelperDet = new DetailsDatabaseHelperClass();
       // TipsDatabaseHelperClass Db_HelperTip = new TipsDatabaseHelperClass();
        SQLiteAsyncConnection conn;
      
      
        
        public ConnectionActivity()
        {

          
            InitializeComponent();
        
         
        }

        public async void Parse()
        {
            
            Uri uri = new Uri("https://api.parse.com/1/classes/Nutrition");
            Debug.WriteLine(uri);
            HttpClient cl = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Parse-Application-Id", "7EjTEqzbb1zfFWz6BJArdZVKkPag6qUuKTCE7rz3");
            request.Headers.Add("X-Parse-REST-API-Key", "L3IEKFEazzTxzWVZim6oSVwCTvMPWwiI7IyQYnz3");

            HttpResponseMessage response = await cl.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();
            var parsedObject = JsonConvert.DeserializeObject<JObject>(data);

            JObject jsonObject = new JObject();
            jsonObject["results"] = parsedObject["results"];
        
          bool dbExist = await CheckDbAsync("Nut.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }
            else
            {

                conn = new SQLiteAsyncConnection("Nut.db3");
            }
            var listN = new List<Nutrition>();
       
           

            foreach (var dataset in jsonObject["results"])
            {

                string Category = Convert.ToString(dataset["Category"]);
                string name = Convert.ToString(dataset["Name"]);
                string Id_N = Convert.ToString(dataset["Id_N"]);

             
             Nutrition n = new Nutrition();
               n.name = name;
               n.category = Category;
               n.Id = Id_N;

              listN.Add(n);
               

               // Debug.WriteLine(name);
            }




          //  await  DropTableAsync();
             await conn.InsertAllAsync(listN);
            // IsolatedStorageHelper.SaveObject<List<Nutrition>>("nutrition", listN);
            int i = 0;
            foreach (Nutrition n in listN)
            {

                Debug.WriteLine("Compteur =" + i);
                i++;
                Debug.WriteLine(n.name);

            }
            if (listN.Count == 0)
            {
                popupStructure();
            }






        }

        public async void Parse2()


        {


            Uri uri = new Uri("https://api.parse.com/1/classes/Details");
            Debug.WriteLine(uri);
            HttpClient cl = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Parse-Application-Id", "7EjTEqzbb1zfFWz6BJArdZVKkPag6qUuKTCE7rz3");
            request.Headers.Add("X-Parse-REST-API-Key", "L3IEKFEazzTxzWVZim6oSVwCTvMPWwiI7IyQYnz3");

            HttpResponseMessage response = await cl.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();
            var parsedObject = JsonConvert.DeserializeObject<JObject>(data);

            JObject jsonObject = new JObject();
            jsonObject["results"] = parsedObject["results"];
            bool dbExist = await CheckDbAsync("Details.db3");
            if (!dbExist)
            {
                await CreateDatabaseAsync2();
            }
            else
            {

                conn = new SQLiteAsyncConnection("Details.db3");
            }
            //await DropTableAsync2();
         
            var listD = new List<Details>();
            foreach (var dataset in jsonObject["results"])
            {

                string Part1 = Convert.ToString(dataset["Part1"]);
                string Part2 = Convert.ToString(dataset["Part2"]);
                string Part3 = Convert.ToString(dataset["Part3"]);
                string Part4 = Convert.ToString(dataset["Part4"]);
                string Val1 = Convert.ToString(dataset["Val1"]);
                string Val2 = Convert.ToString(dataset["Val2"]);
                string Val3 = Convert.ToString(dataset["Val3"]);
                string Val4 = Convert.ToString(dataset["Val4"]);
                string id_DN = Convert.ToString(dataset["Id_DN"]);
            
                Details d = new Details();
                d.Id = id_DN;
                d.part1 = Part1;
                d.part2 = Part2;
                d.part3 = Part3;
                d.part4 = Part4;
                d.val1 = Val1;
                d.val2 = Val2;
                d.val3 = Val3;
                d.val4 = Val4;





                listD.Add(d);
                //Debug.WriteLine(Part1);
            }
           // await DropTableAsync2();
            await conn.InsertAllAsync(listD);
            int i = 0;
            foreach (Details d in listD)
            {

                Debug.WriteLine("Compteur Details =" + i);
                i++;
                Debug.WriteLine(d.part1);

            }
        }
        public async void Parse3()
        {
            Uri uri = new Uri("https://api.parse.com/1/classes/Tips");
            Debug.WriteLine(uri);
            HttpClient cl = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Parse-Application-Id", "7EjTEqzbb1zfFWz6BJArdZVKkPag6qUuKTCE7rz3");
            request.Headers.Add("X-Parse-REST-API-Key", "L3IEKFEazzTxzWVZim6oSVwCTvMPWwiI7IyQYnz3");

            HttpResponseMessage response = await cl.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();
            var parsedObject = JsonConvert.DeserializeObject<JObject>(data);

            JObject jsonObject = new JObject();
            jsonObject["results"] = parsedObject["results"];
          
             bool dbExist = await CheckDbAsync("Tips.db3");
                if (!dbExist)
                {
                    await CreateDatabaseAsync3();
                }
                else
                {

                    conn = new SQLiteAsyncConnection("Tips.db3");
                }
           // await DropTableAsync3();
            var listT = new List<Tips>();
            foreach (var dataset in jsonObject["results"])
            {

                string title1 = Convert.ToString(dataset["Title1"]);
                string title2 = Convert.ToString(dataset["Title2"]);
                string title3 = Convert.ToString(dataset["Title3"]);
                string Desc1 = Convert.ToString(dataset["Desc1"]);
                string Desc2 = Convert.ToString(dataset["Desc2"]);
                string Desc3 = Convert.ToString(dataset["Desc3"]);
                string Id = Convert.ToString(dataset["objectId"]);
              
               Tips t = new Tips();
                t.desc1 = Desc1;
                t.desc2 = Desc2;
                t.desc3 = Desc3;
                t.title1 = title1;
                t.title2 = title2;
                t.title3 = title3;
                t.Id= Id;


                listT.Add(t);
              
               
               
            }
           // await DropTableAsync3();
            await conn.InsertAllAsync(listT);
            int i = 0;
            foreach (Tips t in listT)
            {

                Debug.WriteLine("Compteur =" + i);
                i++;
                Debug.WriteLine(t.desc1);

            }
        }

        public async void Authentifiction()
        {
            Uri uri = new Uri("https://api.parse.com/1/login?username=" + Username.Text + "&password=" + Password.Password);
            Debug.WriteLine(uri);
            HttpClient cl = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Parse-Application-Id", "7EjTEqzbb1zfFWz6BJArdZVKkPag6qUuKTCE7rz3");
            request.Headers.Add("X-Parse-REST-API-Key", "L3IEKFEazzTxzWVZim6oSVwCTvMPWwiI7IyQYnz3");
            HttpResponseMessage response = await cl.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(data);

            var parsedObject = JsonConvert.DeserializeObject<JObject>(data);
            if (data.Contains("error"))
            {
                //await progresBar.HideAsync();
                var messageDialog = new Windows.UI.Popups.MessageDialog(" Check your login and password and retry again");
                await messageDialog.ShowAsync();
                ringprog.Visibility = Visibility.Collapsed;
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
                ringprog.Visibility = Visibility.Collapsed;
                listT.Add(new User() { username = parsedObject["username"].ToString(), Age = parsedObject["Age"].ToString(), Height = parsedObject["Height"].ToString(), Weight = parsedObject["Weight"].ToString(), email = parsedObject["email"].ToString(), Gender = parsedObject["Gender"].ToString() });
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

       

     
        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {

          

               Task.Run(()=>{
                   Parse();
                   Parse2();
                   Parse3();
               });
           
           
           
            

          
           

          
        }

        private void signin_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(Inscription));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ringprog.Visibility = Visibility.Visible;
            Authentifiction();
           
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
        private async Task CreateDatabaseAsync2()
        {
            conn = new SQLiteAsyncConnection("Details.db3");
            await conn.CreateTableAsync<Details>();
        }
        private async Task CreateDatabaseAsync3()
        {
            conn = new SQLiteAsyncConnection("Tips.db3");
            await conn.CreateTableAsync<Tips>();
        }
        private async Task CreateDatabaseAsync4()
        {
            conn = new SQLiteAsyncConnection("User.db3");
            await conn.CreateTableAsync<User>();
        }
        private async Task DropTableAsync()
        {

            Nutrition n = await RecupererPremiereRecette();
            await conn.DeleteAsync(n);
            //await conn.QueryAsync<Nutrition>("Delete from Nut");
            //await conn.DeleteAsync
            //Debug.WriteLine("drop1");
            //conn = new SQLiteAsyncConnection("Nut.db3");
            //await conn.DropTableAsync<Nutrition>();
        }
        private async Task<Nutrition> RecupererPremiereRecette()
        {
            return await conn.Table<Nutrition>()
                .FirstOrDefaultAsync();
        }
        private async Task DropTableAsync2()
        {

            Details n = await RecupererPremiereRecette2();
            await conn.DeleteAsync(n);
            //await conn.QueryAsync<Nutrition>("Delete from Nut");
            //await conn.DeleteAsync
            //Debug.WriteLine("drop1");
            //conn = new SQLiteAsyncConnection("Nut.db3");
            //await conn.DropTableAsync<Nutrition>();
        }
        private async Task<Details> RecupererPremiereRecette2()
        {
            return await conn.Table<Details>()
                .FirstOrDefaultAsync();
        }

        private async Task DropTableAsync3()
        {

            Tips n = await RecupererPremiereRecette3();
            await conn.DeleteAsync(n);
            //await conn.QueryAsync<Nutrition>("Delete from Nut");
            //await conn.DeleteAsync
            //Debug.WriteLine("drop1");
            //conn = new SQLiteAsyncConnection("Nut.db3");
            //await conn.DropTableAsync<Nutrition>();
        }
        private async Task<Tips> RecupererPremiereRecette3()
        {
            return await conn.Table<Tips>()
                .FirstOrDefaultAsync();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        public void CommandHandlers(IUICommand commandLabel)
        {
            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Ok":
                    this.Focus(Windows.UI.Xaml.FocusState.Pointer);
                    break;
                case "Quit":
                    Application.Current.Exit();
                    break;
                case "Return":
                    Frame rootFrame = Window.Current.Content as Frame;
                    rootFrame.GoBack();
                    break;
            }
        }
        async void popupStructure()
        {
            MessageDialog msg = new MessageDialog("Check your connection and reboot application.", "No data avaible !");

            // msg.Commands.Add(new UICommand("Ok", new UICommandInvokedHandler(CommandHandlers)));
            msg.Commands.Add(new UICommand("Return", new UICommandInvokedHandler(CommandHandlers)));

            await msg.ShowAsync();
        }







    }


}
