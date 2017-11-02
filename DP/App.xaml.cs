using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DP
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SplashScreen _splashScreen;
        public static void SceenOn(string resourcePath = "/Resources/wpf-banner.png")
        {
            _splashScreen = new SplashScreen(resourcePath);
            _splashScreen.Show(false, false);
        }
        public static void ScreenOff(int timerMs = 300)
        {
            _splashScreen.Close(TimeSpan.FromMilliseconds(timerMs));
        }
    }
}
