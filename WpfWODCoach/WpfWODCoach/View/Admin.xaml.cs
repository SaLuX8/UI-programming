using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {

        private Athlete selectedAthlete;
        // private Coach selectedCoach;
        // private int selectedCoachId = 0;

        public Admin()
        {
            InitializeComponent();
            InitAdminGrid();
        }

        // ---------------------------------------------------------
        // Initialize datagrid 
        // ---------------------------------------------------------
        private void InitAdminGrid()
        {
            try
            {
                var athletes = ViewModel.LoadAthletes();    // load athletes to variable
                dgAdminGrid.DataContext = athletes;   // set datagrid datacontext as athlete object
                cbCoachName.ItemsSource = ViewModel.LoadCoaches();
                cbCoachName.DisplayMemberPath = "fullName";
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // If selection changes in admin datagrid
        // ---------------------------------------------------------
        private void dgAdminGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgAdminGrid.SelectedIndex > -1)
                {
                    selectedAthlete = dgAdminGrid.SelectedItem as Athlete;                  // Athlete selection
                    tbAthlete.Text = Convert.ToString(selectedAthlete.fullname);            // update selectedAthlete properties to textboxea
                    tbTel.Text = Convert.ToString(selectedAthlete.telNumber);
                    cbCoachName.Text = Convert.ToString(selectedAthlete.Coach.fullName);
                    tbCoachId.Text = Convert.ToString(selectedAthlete.Coach.idCoach);

                    string message = $"Athlete {selectedAthlete.idAthlete} {selectedAthlete.fullname} selected";
                    tbMessage.Text = message;                                               // Update bottom message row
                    dgAdminGrid.UnselectAll();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // Combobox Coach selection changed eventhandler
        // ---------------------------------------------------------
        private void cbCoachName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Coach selectedItem = (Coach)cbCoachName.SelectedItem;
                if (selectedItem != null)
                {
                    tbCoachId.Text = selectedItem.idCoach.ToString();       // change the coachId number in the textbox right side
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // SAVE button Eventhandler
        // ---------------------------------------------------------
        private void btnAddWod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Athlete athlete = new Athlete();
                int.TryParse(tbCoachId.Text, out int coachId);
                if (selectedAthlete == null)                    // if Athlete is selected from datagrid  => new Athlete
                {
                    athlete.fullname = tbAthlete.Text;
                    athlete.telNumber = tbTel.Text;
                    athlete.Coach_idCoach = coachId;
                    ViewModel.SaveAthlete(0, athlete);
                }
                else                                    // if not new then selected Athlete is modified -> requires Save button
                {
                    athlete = selectedAthlete;
                    athlete.fullname = tbAthlete.Text;
                    athlete.telNumber = tbTel.Text;
                    athlete.Coach_idCoach = coachId;
                    ViewModel.SaveAthlete(athlete.idAthlete, athlete);
                }

                tbAthlete.Text = "";                    // empty textboxes after create / modify
                cbCoachName.Text = "";
                tbCoachId.Text = "99";
                tbTel.Text = "";

                dgAdminGrid.ItemsSource = ViewModel.LoadAthletes();                                                     // update of datagrid
                tbMessage.Text = $"Athlete {athlete.fullname} saved on date {DateTime.Today.ToString("dd.MM.yyyy")}";   // update message to bottom inforow
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        // ---------------------------------------------------------
        // DELETE button Eventhandler
        // ---------------------------------------------------------
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAdminGrid.SelectedIndex > -1)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);    // Create Confirmation box "Are you sure?"

                    if (messageBoxResult == MessageBoxResult.Yes)                                       // if answer is yes
                    {
                        ViewModel.DeleteAthlete(selectedAthlete);
                        tbAthlete.Text = "";                                                            // empty textboxes after create / modify
                        cbCoachName.Text = "";
                        tbCoachId.Text = "99";
                        tbTel.Text = "";
                        tbMessage.Text = $"Athlete {selectedAthlete.fullname} deleted";                 // update message to bottom inforow
                    }
                }
                dgAdminGrid.ItemsSource = ViewModel.LoadAthletes();                                     // update of datagrid
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }

}
