using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfWODCoach
{
    /// <summary>
    /// Interaction logic for Selection.xaml
    /// </summary>
    public partial class UserSelection : Window
    {
        public static bool show;

        public UserSelection()
        {
            InitializeComponent();
        }


        // ---------------------------------------------------------
        // Button Athlete eventhandler
        // ---------------------------------------------------------
        private void btnAthlete_Click(object sender, RoutedEventArgs e)
        {
            show = false;                                   // set bool variable to false -> certain menu items are not enabled
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // ---------------------------------------------------------
        // Button Coach eventhandler
        // ---------------------------------------------------------
        private void btnCoach_Click(object sender, RoutedEventArgs e)
        {
            show = true;                                    // set bool variable to true -> all menu items are enabled
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
    
}
