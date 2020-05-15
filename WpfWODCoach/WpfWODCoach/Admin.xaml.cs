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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {

        private Athlete selectedAthlete;
        private Coach selectedCoach;
        private int selectedCoachId = 0;

        public Admin()
        {
            InitializeComponent();
            InitAdminGrid();
        }

        private void InitAdminGrid()
        {
            try
            {
                var athletes = ViewModel.LoadAthletes();    // load athletes to variable
                dgAdminGrid.DataContext = athletes ;   // set datagrid datacontext as athlete object

                cbCoachName.ItemsSource = ViewModel.LoadCoaches();
                cbCoachName.DisplayMemberPath = "fullName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAddWod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Athlete athlete = new Athlete();
                int.TryParse(tbCoachId.Text, out int coachId);
                if (selectedAthlete == null)        // if Athlete is not selected  => new Athlete
                {
                    athlete.fullname = tbAthlete.Text;
                    athlete.telNumber = tbTel.Text;
                    athlete.Coach_idCoach = coachId;
                    ViewModel.SaveAthlete(0, athlete);
                }
                else                            // if not new then selected Athlete is modified
                {
                    athlete = selectedAthlete;
                    athlete.fullname = tbAthlete.Text;
                    athlete.telNumber = tbTel.Text;
                    athlete.Coach_idCoach = coachId;
                    ViewModel.SaveAthlete(athlete.idAthlete, athlete);
                }

                tbAthlete.Text = "";       // empty textboxes after create / modify
                cbCoachName.Text = "";
                tbCoachId.Text = "";
                tbTel.Text = "";

                dgAdminGrid.ItemsSource = ViewModel.LoadAthletes();
                tbMessage.Text = $"Athlete {athlete.fullname} saved on date {DateTime.Today}";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void dgAdminGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dgAdminGrid.SelectedIndex > -1)
                {
                    selectedAthlete = dgAdminGrid.SelectedItem as Athlete;                  // WOD selection

                   
                    tbAthlete.Text = Convert.ToString(selectedAthlete.fullname);   // update selectedWod properties to textboxea
                    tbTel.Text = Convert.ToString(selectedAthlete.telNumber);
                    cbCoachName.Text = Convert.ToString(selectedAthlete.Coach.fullName);
                    tbCoachId.Text = Convert.ToString(selectedAthlete.Coach.idCoach);

                    string message = $"Athlete {selectedAthlete.idAthlete} {selectedAthlete.fullname} selected";
                    tbMessage.Text = message;                                       // Update bottom message row
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cbCoachName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            {
                Coach selectedItem = (Coach)cbCoachName.SelectedItem;
                
                if (selectedItem != null)
                {
                    selectedCoachId = selectedItem.idCoach;
                    tbCoachId.Text = selectedItem.idCoach.ToString();
                }

                selectedCoachId = 99;
                
                // DateTime dateTime = DateTime.Today;
                dgAdminGrid.ItemsSource = ViewModel.LoadAthletes();

                //dgAdminGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selectedAthlete.Coach_idCoach, dateTime);
            }

            /*
            Athlete selectedItem = (Athlete)cbCoachName.SelectedItem;
            if (selectedItem != null)
            {
                selected = selectedItem.idAthlete;
            }

            selectedAthlete = selectedItem;

            // selectedItem.Coach.fullName


            
            */
        }
    }
    
}
