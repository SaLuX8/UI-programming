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
        

        private int selected = 0;
      

        public AthleteMain()
        {
            InitializeComponent();
            InitGrid();
        }

        
        private void InitGrid()
        {
            try
            {
                dgAthleteGrid.ItemsSource = ViewModel.LoadAthletes();

                var coaches = ViewModel.LoadCoaches();
                cbCoachName.ItemsSource = coaches;
                cbCoachName.DisplayMemberPath = "fullName";
                
                
            }
            catch (Exception ex) 
            {

                MessageBox.Show(ex.ToString());
            }



        }

       


    

        //Comboboc Coach selection to view athletes by coach
        private void cbCoachName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Coach selectedItem = (Coach)cbCoachName.SelectedItem;
            selected = selectedItem.idCoach;
            
            
            dgAthleteGrid.ItemsSource = ViewModel.LoadAthletesByCoach(selected);
            
            
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
