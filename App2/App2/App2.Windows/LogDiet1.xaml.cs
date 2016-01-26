using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
    public sealed partial class LogDiet1 : Page
    {
        public LogDiet1()
        {
            this.InitializeComponent();
            DateTime thisDay = DateTime.Today;
            // Display the date in the default (general) format.

            //  Console.WriteLine(thisDay.ToString("d"));
            // Console.WriteLine(thisDay.ToString("D"));
            // Console.WriteLine(thisDay.ToString("g"));
            // today.Text = thisDay.ToString("g");
            String[] année = thisDay.ToString("D").Split(',');

            string ann = année[1]+année[2];
            Debug.WriteLine(ann);
            today.Text =ann;

            List<DietsClass> l = new List<DietsClass>();
           
            l.Add(new DietsClass() { diets = "Breakfast" });
            l.Add(new DietsClass() { diets = "Morning Snack" });
            l.Add(new DietsClass() { diets = "Lunch" });
            l.Add(new DietsClass() { diets = "Afternoon Snack" });
            l.Add(new DietsClass() { diets = "Dinner" });
            l.Add(new DietsClass() { diets = "Evening Snack" });

          

            DietsList.DataContext = l;
           
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DietsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Debug.WriteLine("iktiiib");
            DietsClass d = (DietsClass)DietsList.SelectedItem;
            TempDiets.dietSelected = d.diets;


            Frame.Navigate(typeof(LogDiet2));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(Home));
        }
     
    }
}
