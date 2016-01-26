using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class RunPage : Page
    {
        private Accelerometer _accelerometer;
        private uint _desiredReportInterval;
        private DispatcherTimer _dispatcherTimer;
        int i = 0;
  
        public RunPage()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            _accelerometer = Accelerometer.GetDefault();
           
                // Select a report interval that is both suitable for the purposes of the app and supported by the sensor.
                // This value will be used later to activate the sensor.
                uint minReportInterval = _accelerometer.MinimumReportInterval;
                _desiredReportInterval = minReportInterval > 16 ? minReportInterval : 16;

                // Set up a DispatchTimer
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Tick += DisplayCurrentReading;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)_desiredReportInterval);
                

        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d’événement décrivant la manière dont l’utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
    

        }
       
        private void VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (runimage.IsEnabled)
            {
                if (e.Visible)
                {
                    // Re-enable sensor input (no need to restore the desired reportInterval... it is restored for us upon app resume)
                    _dispatcherTimer.Start();
                }
                else
                {
                    // Disable sensor input (no need to restore the default reportInterval... resources will be released upon app suspension)
                    _dispatcherTimer.Stop();
                }
            }
        }



        private Boolean tharrek()
        {
            Boolean a = false;
            AccelerometerReading reading = _accelerometer.GetCurrentReading();
            if (reading.AccelerationZ <= -0.9)
            {
                a = true;
            }

            return a;

        }
        private Boolean wakher()
        {
            Boolean a = true;
            AccelerometerReading reading = _accelerometer.GetCurrentReading();
            if (reading.AccelerationZ <= 0.9)
            {
                a = false;
            }

            return a;

        }
        private void DisplayCurrentReading(object sender, object args)
        {
            AccelerometerReading reading = _accelerometer.GetCurrentReading();
            if (reading != null)
            {
               

                if (tharrek() == true && wakher() == false)
                {
                    Steps.Text = i.ToString();
                    i++;
                }
                if (tharrek() == false && wakher() == true)
                {
                    Steps.Text = i.ToString();
                    i++;
                }



            }
        }



        private void ScenarioEnable(object sender, RoutedEventArgs e)
        {
          
               
        }

      

        private void runimage_Click(object sender, RoutedEventArgs e)
        {
            // Set the report interval to enable the sensor for polling
            _accelerometer.ReportInterval = _desiredReportInterval;
            runimage_Copy.Visibility = Visibility.Collapsed;
            Window.Current.VisibilityChanged += new WindowVisibilityChangedEventHandler(VisibilityChanged);
            _dispatcherTimer.Start();
            pauseimage.Visibility = Visibility.Visible;
           runimage.Visibility = Visibility.Collapsed;


        }

        private void pauseimage_Click(object sender, RoutedEventArgs e)
        {
            runimage_Copy.Visibility = Visibility.Visible;
            Window.Current.VisibilityChanged -= new WindowVisibilityChangedEventHandler(VisibilityChanged);

            // Stop the dispatcher
            _dispatcherTimer.Stop();

            // Restore the default report interval to release resources while the sensor is not in use
            _accelerometer.ReportInterval = 0;
          

           pauseimage.Visibility = Visibility.Collapsed;
            runimage.Visibility = Visibility.Visible;
        }

    

        private void runimage_Copy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            i = 0;
            Steps.Text = "0";
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


    }
}
