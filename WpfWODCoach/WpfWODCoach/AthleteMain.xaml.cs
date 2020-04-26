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
using System.Data.Entity;

namespace WpfWODCoach
{
    /// <summary>
    /// Interaction logic for AthleteMain.xaml
    /// </summary>
    public partial class AthleteMain : Page
    {
        WODCoachEntities ctx;

        private int selected = 0;
        private int sel;

        public AthleteMain()
        {
            InitializeComponent();
            InitGrid();
        }

        
        private void InitGrid()
        {
            try
            {
                dgCoachGrid.ItemsSource = ViewModel.LoadAthletes();

                var coaches = ViewModel.LoadCoaches();
                cbCoachName.ItemsSource = coaches;
                cbCoachName.DisplayMemberPath = "fullName";
                
                
            }
            catch (Exception ex) 
            {

                MessageBox.Show(ex.ToString());
            }
            
            
            
            
            



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dgCoachGrid.ItemsSource = ViewModel.LoadAthletesByCoach(selected);
        }

        private void cbCoachName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = cbCoachName.SelectedItem;
            var fee = sender as Coach;
            selected = fee.idCoach;
            
            dgCoachGrid.ItemsSource = ViewModel.LoadAthletesByCoach(selected);
            
            
           
        }
    }
}
