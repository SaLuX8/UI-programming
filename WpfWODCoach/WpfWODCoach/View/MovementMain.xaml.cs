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
using WpfWODCoach.Model;
using WpfWODCoach.Viewmodel;

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
            try
            {
                var wods = ViewModel.LoadWods();                    // ladataan wodit muuttujaan
                dgMovementGrid.DataContext = wods;                  // asetetaan datagridin datacontextiksi Wod olio
            }
            catch (Exception)
            {
                MessageBox.Show("No Movement selected or Movement is in use and can't be deleted");
            }
        }
        // ---------------------------------------------------------
        // DELETE button Eventhandler
        // ---------------------------------------------------------
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedWod != null)                                // if movement is selected
                {
                    ViewModel.DeleteWod(selectedWod.idWod);             // call method to delete movement (wod) from entity model and database
                    tbMessage.Text = $"Movement {selectedWod.idWod} - {selectedWod.movementName} Deleted on date {DateTime.Today.ToString("dd.MM.yyyy")}"; // update message to bottom inforow
                }
                tbMovement.Text = "";                                   // empty textboxes after create / modify
                starsLevel.Value = 0;
                dgMovementGrid.ItemsSource = ViewModel.LoadWods();      // update datagrid
            }
            catch (SystemException)
            {
                MessageBox.Show("No Movement selected or Movement is in use and can't be deleted");
            }

            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // SAVE button Eventhandler
        // ---------------------------------------------------------
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedWod == null)                                        // if wod (movement) is not selected  => new wod
                {
                    selectedWod = new Wod();                                    // create new movement (wod)
                    selectedWod.movementName = tbMovement.Text;
                    selectedWod.level = starsLevel.Value;
                    ViewModel.SaveWod(0, selectedWod);                          // call method to save movement to entity model and database
                }
                else                                                            // if not new then selected wod (movement) is modified
                {
                    selectedWod.movementName = tbMovement.Text;
                    selectedWod.level = starsLevel.Value;
                    ViewModel.SaveWod(selectedWod.idWod, selectedWod);          // call method to save movement to entity model and database
                }
                tbMovement.Text = "";                                           // empty textboxes after create / modify
                starsLevel.Value = 0;

                dgMovementGrid.ItemsSource = ViewModel.LoadWods();              // update datagrid
                tbMessage.Text = $"Movement {selectedWod.movementName} saved on date {DateTime.Today.ToString("dd.MM.yyyy")}";  // update message to bottom inforow
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // Datagrid selection changed Eventhandler
        // ---------------------------------------------------------
        private void dgAthleteGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgMovementGrid.SelectedIndex > -1)                         // if something is seleceted from datagrid
                {
                    selectedWod = dgMovementGrid.SelectedItem as Wod;           // set datagrid selected item to selectedWod -object
                    tbMovement.Text = selectedWod.movementName;                 // Movement textbox text update 
                    tbMessage.Text = $"Movement {selectedWod.idWod} selected";  // update message to bottom inforow
                    starsLevel.Value = Convert.ToInt32(selectedWod.level);      // update stars value update
                }

                dgMovementGrid.DataContext = ViewModel.LoadWods();               // update datagrid

                tbMessage.Text = $"Movement {selectedWod.idWod} - {selectedWod.movementName} selected";     // update message to bottom inforow
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
