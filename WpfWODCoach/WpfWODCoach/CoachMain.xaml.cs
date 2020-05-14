using Google.Protobuf.WellKnownTypes;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
                // dgCoachGrid.ItemsSource = ViewModel.LoadWods();
                
                

                var wods = ViewModel.LoadWods(); // ladataan datagridin datacontextiksi Wod olio
                dgCoachGrid.DataContext = wods; 

                var athletes = ViewModel.LoadAthletes();
                cbAthleteName.ItemsSource = athletes;
                cbAthleteName.DisplayMemberPath = "fullname";

                dpWod.SelectedDate = DateTime.Today;
                dpWod.DisplayDate = DateTime.Today;

                var rates = ViewModel.LoadRating();
               
                float i;
                Binding binding = new Binding();
                binding.Source = rates;
                ratingColumn.Binding = binding;

                
                foreach (var item in rates)
                {
                    i = (float)item.rating;
                    
                    
                }
                /*string c;
                foreach (var item in rates)
                {
                    c = item.comment;
                    binding.Source = c;
                    ratingComment.Binding = binding;
                }*/
                

                
               
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        // Combobox Printing to datagrid wods by athlete and date
        private void cbAthleteName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Athlete selectedItem = (Athlete)cbAthleteName.SelectedItem;
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

        // if DATE value changes
        private void dpWod_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            cbAthleteName_SelectionChanged(sender, e);
        }

        // eventhandler for SAVE button
        private void btnAddWod_Click(object sender, RoutedEventArgs e)
        {
            Athlete athlete = selectedAthlete;
            int idAthlete = athlete.idAthlete;
            int.TryParse(tbReps.Text, out int reps);
            int.TryParse(tbRounds.Text, out int rounds);

            if (selectedWod == null)        // if wod (movement) is not selected  => new wod
            {
                ViewModel.AddWodToAthlete(0, idAthlete, dateTime, tbMovement.Text, reps, rounds, tbComment.Text);
            }
            else                            // if not new then selected wod (movement) is modified
            {
                ViewModel.AddWodToAthlete(selectedWod.idWod, idAthlete, dateTime, tbMovement.Text, reps, rounds, tbComment.Text);
            }

            tbMovement.Text = "";       // empty textboxes after create / modify
            tbReps.Text = "";
            tbRounds.Text = "";
            tbComment.Text = "";
            
            dgCoachGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);
            tbMessage.Text = $"Movement saved to athlete {athlete.fullname} on date {dateTime}";


        }

        // Updates values in textboxes when selection is changed
        private void dgCoachGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgCoachGrid.SelectedIndex > -1)
                {
                    selectedWod = dgCoachGrid.SelectedItem as Wod;                  // WOD selection
                    
                    tbMovement.Text = Convert.ToString(selectedWod.movementName);   // update selectedWod properties to textboxea
                    tbComment.Text = Convert.ToString(selectedWod.comment);
                    tbReps.Text = Convert.ToString(selectedWod.repsCount);
                    tbRounds.Text = Convert.ToString(selectedWod.roundCount);
                    
                    string message = $"Movement no. {selectedWod.idWod} of athlete {selectedWod.Athlete.fullname} chosen";
                    tbMessage.Text = message;                                       // Update bottom message row
                    selectedAthlete = selectedWod.Athlete;                          // update selected Athlete

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

       

        
    }
}
