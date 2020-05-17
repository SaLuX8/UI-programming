using Google.Protobuf.WellKnownTypes;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfWODCoach
{
    /// <summary>
    /// Interaction logic for CoachMain.xaml
    /// </summary>
    public partial class CoachMain : Page
    {
        private int selected = 0;
        private Athlete selectedAthlete;
        private DateTime dateTime;
        private Wod selectedWod;

        public CoachMain()
        {
            InitializeComponent();
            InitCoach();
        }

        private void InitCoach()
        {
            try
            {
                var wods = ViewModel.LoadWods();            // Load Wod objects as datagrid datacontext
                dgCoachGrid.DataContext = wods;

                var athletes = ViewModel.LoadAthletes();    // Load Athlete objects as ComboBox itemssource
                cbAthleteName.ItemsSource = athletes;
                cbAthleteName.DisplayMemberPath = "fullname";   // Show athlete fullname in combobox

                cbMovementName.ItemsSource = wods;          // set Movements ComboBox items from Wods object
                cbMovementName.DisplayMemberPath = "movementName";

                dpWod.SelectedDate = DateTime.Today;        // Set DatePicker selected and displayed date as current date
                dpWod.DisplayDate = DateTime.Today;
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Combobox Printing to datagrid wods by athlete and date
        private void cbAthleteName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Athlete selectedItem = (Athlete)cbAthleteName.SelectedItem;     // Load Combobox selecteditem to Athlete object selectedItem
                if (selectedItem != null)
                {
                    selected = selectedItem.idAthlete;
                }

                selectedAthlete = selectedItem;

                dateTime = (DateTime)dpWod.SelectedDate;
                if (dateTime == null)
                {
                    dateTime = DateTime.Today;
                }
                dgCoachGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // if DATE value changes
        private void dpWod_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbAthleteName_SelectionChanged(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // eventhandler for SAVE button
        private void btnSaveWod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(tbReps.Text, out int reps);
                int.TryParse(tbRounds.Text, out int rounds);
                Athlete athlete1 = (Athlete)cbAthleteName.SelectedItem;

                if (dgCoachGrid.SelectedIndex < 0)        // if wod (movement) is not selected  => new wod
                {
                    ViewModel.AddWodToAthlete(0, selectedAthlete.idAthlete, dateTime, cbMovementName.Text, reps, rounds, tbComment.Text, starsLevel.Value);
                }
                else                            // if not new then selected wod (movement) is modified
                {
                    ViewModel.AddWodToAthlete(selectedWod.idWod, selectedAthlete.idAthlete, dateTime, cbMovementName.Text, reps, rounds, tbComment.Text, starsLevel.Value);
                }

                cbMovementName.Text = "";       // empty textboxes after create / modify
                tbReps.Text = "";
                tbRounds.Text = "";
                tbComment.Text = "";
                starsLevel.Value = 0;

                dgCoachGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);
                tbMessage.Text = $"Movement saved to athlete {selectedAthlete.fullname} on date {dateTime.ToString("dd.MM.yyyy")}";
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.RemoveWodFromAthlete(selectedWod.idWod, (int)selectedWod.idAthlete);

                if (selectedWod != null)
                {
                    selectedWod.idAthlete = 0;
                    tbMessage.Text = $"Movement {selectedWod.movementName} Deleted on date {DateTime.Today.ToString("dd.MM.yyyy")}";
                }
                cbMovementName.Text = "";       // empty textboxes after delete
                tbReps.Text = "";
                tbRounds.Text = "";
                tbComment.Text = "";
                dgCoachGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);
            }
            catch (SystemException)
            {
                MessageBox.Show("No Movement selected or Movement is in use and can't be deleted","Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Updates values in textboxes when selection is changed
        private void dgCoachGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgCoachGrid.SelectedIndex > -1)
                {
                    selectedWod = dgCoachGrid.SelectedItem as Wod;                  // WOD selection

                    cbMovementName.Text = Convert.ToString(selectedWod.movementName);   // update selectedWod properties to textboxea
                    tbComment.Text = Convert.ToString(selectedWod.comment);
                    tbReps.Text = Convert.ToString(selectedWod.repsCount);
                    tbRounds.Text = Convert.ToString(selectedWod.roundCount);
                    starsLevel.Value = Convert.ToInt32(selectedWod.level);

                    string message = $"Movement no. {selectedWod.idWod} of athlete {selectedWod.Athlete.fullname} chosen";
                    tbMessage.Text = message;                                       // Update bottom message row
                    selectedAthlete = selectedWod.Athlete;                          // update selected Athlete
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
