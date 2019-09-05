﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestSharp;
using Newtonsoft.Json;


namespace LuisaKatrinaReyes.Bingmaps.windows
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            KathMap.Focus();
            KathMap.Mode = new AerialMode(true);
            KathMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(MyMap_ViewChangeOnFrame);


        }

        private void KathMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = KathMap.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            pin.MouseDoubleClick += new MouseButtonEventHandler(KathMap_MouseDoubleClick);


            KathMap.Children.Add(pin);
        }

        private void KathMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            var pushPinContent = 0;
            var pushPin = sender as Pushpin;
            if (pushPin != null && pushPin.GetType() == typeof(Pushpin))
                pushPinContent = Convert.ToInt32(pushPin.Content);

            KathMap.Heading = (double)pushPinContent;
        }

        private void KathMap_ViewChangeStart(object sender, MapEventArgs e)
        {
            KathMap.ViewChangeStart +=
              new EventHandler<MapEventArgs>(KathMap_ViewChangeStart);
        }

        void MyMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            //Gets the map that raised this event
            Map map = (Map)sender;
            //Gets the bounded rectangle for the current frame
            LocationRect bounds = map.BoundingRectangle;
            //Update the current latitude and longitude
            CurrentPosition.Text += String.Format("Northwest: {0:F5}, Southeast: {1:F5} (Current)",
                        bounds.Northwest, bounds.Southeast);
        }
    }
}

