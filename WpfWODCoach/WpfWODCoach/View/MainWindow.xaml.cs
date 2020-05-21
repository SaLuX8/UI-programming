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
using System.Data.Entity; // for load();
using WpfWODCoach.Model;
using WpfWODCoach.Viewmodel;


namespace WpfWODCoach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new StartPage();
            menuCoach.IsEnabled = UserSelection.show;
            menuAdmin.IsEnabled = UserSelection.show;
            menuMovements.IsEnabled = UserSelection.show;
        }
      
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CoachMain();
            PageHeader.Content = "Coach";
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AthleteMain();
            PageHeader.Content = "Athlete";
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Admin();
            PageHeader.Content = "Manage Athletes";
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new StartPage();
            PageHeader.Content = "";
        }

        // Closes MainWindow
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Main.Content = new MovementMain();
            PageHeader.Content = "Manage Movements";
        }
    }
}

