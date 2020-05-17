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

namespace WpfWODCoach
{
    /// <summary>
    /// Interaction logic for MovementMain.xaml
    /// </summary>
    public partial class MovementMain : Page
    {
        private Wod selectedWod;

        public MovementMain()
        {
            InitializeComponent();
            InitMovementGrid();
        }

        private void InitMovementGrid()
        {
            var wods = ViewModel.LoadWods();    // ladataan wodit muuttujaan
            dgMovementGrid.DataContext = wods;   // asetetaan datagridin datacontextiksi Wod olio
           

           
            


            /*var athletes = ViewModel.LoadAthletes();
            cbAthleteName.ItemsSource = athletes;
            cbAthleteName.DisplayMemberPath = "fullname";

            dpWod.SelectedDate = DateTime.Today;
            dpWod.DisplayDate = DateTime.Today;*/
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedWod != null)
                {
                    ViewModel.DeleteWod(selectedWod.idWod);
                    tbMessage.Text = $"Movement {selectedWod.movementName} Deleted on date {DateTime.Today}";
                }
                tbMovement.Text = "";       // empty textboxes after create / modify
                starsLevel.Value = 0;
                dgMovementGrid.ItemsSource = ViewModel.LoadWods();
            }
            catch (SystemException) 
            {
                MessageBox.Show("No Movement selected or Movement is in use and can't be deleted");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedWod == null)        // if wod (movement) is not selected  => new wod
                {
                    selectedWod.movementName = tbMovement.Text;
                    selectedWod.level = starsLevel.Value;
                    ViewModel.SaveWod(0, selectedWod);
                }
                else                            // if not new then selected wod (movement) is modified
                {
                    selectedWod.movementName = tbMovement.Text;
                    selectedWod.level = starsLevel.Value;
                    ViewModel.SaveWod(selectedWod.idWod, selectedWod);
                }

                tbMovement.Text = "";       // empty textboxes after create / modify
                starsLevel.Value = 0;

                dgMovementGrid.ItemsSource = ViewModel.LoadWods();
                tbMessage.Text = $"Movement {selectedWod.movementName} saved on date {DateTime.Today}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgAthleteGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgMovementGrid.SelectedIndex > -1 )
                {
                    selectedWod = dgMovementGrid.SelectedItem as Wod;
                    tbMovement.Text = selectedWod.movementName;
                    tbMessage.Text = $"Movement {selectedWod.idWod} selected";
                    starsLevel.Value = Convert.ToInt32(selectedWod.level);
                }
                dgMovementGrid.DataContext = ViewModel.LoadWods();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
