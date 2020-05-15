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
using System.Data;
using System.Runtime.Remoting.Contexts;

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
        private Rate rating;

       

        public AthleteMain()
        {
            InitializeComponent();
            InitAthleteGrid();
        }

        private void InitAthleteGrid()
        {
            try
            {
                var wods = ViewModel.LoadWods();    // ladataan wodit muuttujaan
                dgAthleteGrid.DataContext = wods;   // asetetaan datagridin datacontextiksi Wod olio

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

        // Athlete Datagrid  
        private void dgAthleteGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgAthleteGrid.SelectedIndex > -1)                    // selected index is in datagrid
                {
                    selectedWod = dgAthleteGrid.SelectedItem as Wod;    // casting selected item from datagrid as Wod
                    string message = $"Movement no. {selectedWod.idWod} of athlete {selectedWod.Athlete.fullname} chosen";
                    tbMessage.Text = message;                           // Update bottom message row
                    selectedAthlete = selectedWod.Athlete;              // update selected Athlete
                    tbRatedMovement.Text = selectedWod.movementName;
                    
                    //bool done = selectedWod.done.Value;
                    //ViewModel.SaveDoneWod(selectedWod.idWod, done);

                   
                    if (selectedWod.Rate.FirstOrDefault() == null)
                    {
                        tbRatingComment.Text = "";
                        slider.Value = 0;
                    }
                    else
                    {
                        rating = selectedWod.Rate.FirstOrDefault();
                        tbRatingComment.Text = rating.comment;
                        slider.Value = (float)rating.rating;
                    }

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
                int idWod = selectedWod.idWod;



                if (selectedWod.Rate.FirstOrDefault() == null)
                {
                    ViewModel.SaveRating(0, selectedWod.idWod, selectedWod.Athlete.idAthlete, (float)slider.Value, tbRatingComment.Text);
                }
                else
                {

                    ViewModel.SaveRating(selectedWod.idWod, selectedWod.idWod, selectedWod.Athlete.idAthlete, (float)slider.Value, tbRatingComment.Text);
                }
                tbMessage.Text = $"Rating for movement {selectedWod.idWod} is saved";
                dgAthleteGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

  

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (dgAthleteGrid.SelectedIndex > -1)
            {
                selectedWod.done = false;
                ViewModel.SaveDoneWod(selectedWod);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dgAthleteGrid.SelectedIndex > -1)
            {
                selectedWod.done = true;
                ViewModel.SaveDoneWod(selectedWod);
            }
        }
    }
}
