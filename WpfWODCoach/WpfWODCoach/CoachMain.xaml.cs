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
    /// Interaction logic for CoachMain.xaml
    /// </summary>
    public partial class CoachMain : Page
    {
        private int selected = 0;
        private Athlete selectedAthlete;
        private DateTime dateTime;

        public CoachMain()
        {
            InitializeComponent();
            InitCoach();
        }


        private void InitCoach()
        {
            try
            {
                dgCoachGrid.ItemsSource = ViewModel.LoadWods();

                var athletes = ViewModel.LoadAthletes();
                cbAthleteName.ItemsSource = athletes;
                cbAthleteName.DisplayMemberPath = "fullname";
                dpWod.SelectedDate = DateTime.Today;
                dpWod.DisplayDate = DateTime.Today;
                
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
            
            dgCoachGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime) ;

        }
        // if datepicker value changes
        private void dpWod_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            cbAthleteName_SelectionChanged(sender, e);
        }

        // eventhandler add Wod to athlete
        private void btnAddWod_Click(object sender, RoutedEventArgs e)
        {
           
            Athlete athlete = selectedAthlete;
            int id = athlete.idAthlete;
            
            int.TryParse(tbReps.Text, out int reps);
            int.TryParse(tbRounds.Text, out int rounds);

            ViewModel.AddWodToAthlete(id, dateTime, tbMovement.Text, reps, rounds, tbComment.Text);
            // athId = athlete.idAthlete;
            dgCoachGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);
            tbMessage.Text = $"Movement saved to athlete {athlete.fullname} on date {dateTime}";


        }

        private void dgCoachGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
