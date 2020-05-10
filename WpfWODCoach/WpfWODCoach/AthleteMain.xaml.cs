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
        private Athlete selectedAthlete;
        private DateTime dateTime;
        private Wod selectedWod;
        private Rate selectedRate;

        public AthleteMain()
        {
            InitializeComponent();
            InitCoach();
        }


        private void InitGrid()
        {
            try
            {
                dgAthleteGrid.ItemsSource = ViewModel.LoadAthletes();

               


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }



        }

        private void InitCoach()
        {
            try
            {
                dgAthleteGrid.ItemsSource = ViewModel.LoadWods();

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
            dgAthleteGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);
        }

        // if DATE value changes
        private void dpWod_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            cbAthleteName_SelectionChanged(sender, e);
        }

        /*
        // if Done checkbox is unchecked  
        private void cbDone_Unchecked(object sender, RoutedEventArgs e)
        {
            int id = selectedWod.idWod;
            ViewModel.SaveDoneWod(id, false);
            
            string message = $"Movement no. {selectedWod.idWod} of athlete {selectedWod.Athlete.fullname} marked UNDONE";
            tbMessage.Text = message;                           // Update bottom message row
        }


        // if DONE checkbox is checked
        private void cbDone_Checked(object sender, RoutedEventArgs e)
        {
            int id = selectedWod.idWod;
            ViewModel.SaveDoneWod(id, true);
            
            string message = $"Movement no. {selectedWod.idWod} of athlete {selectedWod.Athlete.fullname} marked as DONE";
            tbMessage.Text = message;                           // Update bottom message row
        }
        */

        // Athlete Datagrid  
        private void dgAthleteGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(dgAthleteGrid.SelectedIndex > -1)                    // selected index is in datagrid
                {
                    selectedWod = dgAthleteGrid.SelectedItem as Wod;    // casting selected item from datagrid as Wod
                    string message = $"Movement no. {selectedWod.idWod} of athlete {selectedWod.Athlete.fullname} chosen";
                    tbMessage.Text = message;                           // Update bottom message row
                    selectedAthlete = selectedWod.Athlete;              // update selected Athlete
                    tbRatedMovement.Text = selectedWod.movementName;
                    // selectedRate = (Rate)dgAthleteGrid.SelectedItem;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbratingValue.Text = slider.Value.ToString("0.#");
        }


        // tämä pitää vielä viewmodeliin jossa tehdään tallennus? 

        private void btnRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedRate.rating = (float)slider.Value;
                selectedRate.wod_id = selectedWod.idWod;
                selectedRate.athlete_id = selectedWod.Athlete.idAthlete;
                selectedRate.comment = tbRatingComment.Text;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
