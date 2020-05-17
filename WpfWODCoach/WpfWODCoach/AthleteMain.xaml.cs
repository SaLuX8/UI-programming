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
        private DateTime dateTime;
        private Wod selectedWod;
        private Rate rating;

        public AthleteMain()
        {
            InitializeComponent();
            InitAthleteGrid();
        }

        // ---------------------------------------------------------
        // Initialize datagrid 
        // ---------------------------------------------------------
        private void InitAthleteGrid()
        {
            try
            {
                var wods = ViewModel.LoadWods();                    // ladataan wodit muuttujaan
                dgAthleteGrid.DataContext = wods;                   // asetetaan datagridin datacontextiksi Wod olio

                var athletes = ViewModel.LoadAthletes();
                cbAthleteName.ItemsSource = athletes;
                cbAthleteName.DisplayMemberPath = "fullname";
                
                dpWod.SelectedDate = DateTime.Today;
                dpWod.DisplayDate = DateTime.Today;
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right... ", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // Combobox Printing to datagrid wods by athlete and date
        // ---------------------------------------------------------
        private void cbAthleteName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Athlete selectedItem = (Athlete)cbAthleteName.SelectedItem;
                if (selectedItem != null)                                               // if nothing is selected from datagrid
                {
                    selected = selectedItem.idAthlete;                                  // set int selected value from athtlete chosen in combobox. 
                }

                dateTime = (DateTime)dpWod.SelectedDate;

                if (dateTime == null)                                                   // if nothing is selected from datepicker, current date is chosen
                {
                    dateTime = DateTime.Today;
                }

                dgAthleteGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);    // update datagrid
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right... ", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // if DATE value changes
        // ---------------------------------------------------------
        private void dpWod_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbAthleteName_SelectionChanged(sender, e);                              // if Date value changes Combobox Athletename eventhandler is called
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right... ", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // Athlete Datagrid  
        // ---------------------------------------------------------
        private void dgAthleteGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgAthleteGrid.SelectedIndex > -1)                                   // selected index is in datagrid
                {
                    selectedWod = dgAthleteGrid.SelectedItem as Wod;                    // casting selected item from datagrid as Wod
                    string message = $"Movement no. {selectedWod.idWod} of athlete {selectedWod.Athlete.fullname} chosen";
                    tbMessage.Text = message;                                           // Update bottom message row
                    tbRatedMovement.Text = selectedWod.movementName;

                    if (selectedWod.Rate.FirstOrDefault() == null)                      // check if selected wod has Rating
                    {
                        tbRatingComment.Text = "";                                      // if not set comment and value empty
                        slider.Value = 0;
                    }
                    else
                    {
                        rating = selectedWod.Rate.FirstOrDefault();                     // Rating that is made for selected Wod
                        tbRatingComment.Text = rating.comment;                          // update textbox and slider with selected wod rating values
                        slider.Value = (float)rating.rating;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // If slider is moved, update the number value in the textbox beside it
        // ---------------------------------------------------------
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                tbratingValue.Text = slider.Value.ToString("0.#");
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // Rate button eventhandler
        // ---------------------------------------------------------
        private void btnRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedWod.Rate.FirstOrDefault() == null)              // if chosen movement (wod) doesn't have Rate
                {
                    ViewModel.SaveRating(0, selectedWod.idWod, selectedWod.Athlete.idAthlete, (float)slider.Value, tbRatingComment.Text);   // method SaveRating is called with parameter value 0
                }
                else
                {
                    ViewModel.SaveRating(selectedWod.idWod, selectedWod.idWod, selectedWod.Athlete.idAthlete, (float)slider.Value, tbRatingComment.Text); // otherwise wod id is given to method
                }
                tbMessage.Text = $"Rating for movement {selectedWod.idWod} - {selectedWod.movementName} is saved";                          // Update bottom message row
                dgAthleteGrid.ItemsSource = ViewModel.LoadWodsByAthlete(selected, dateTime);
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right... Check what are you rating", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        // ---------------------------------------------------------
        // Eventhandler for Done-checkbox in datagrid - Uncheck
        // ---------------------------------------------------------
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAthleteGrid.SelectedIndex > -1)                                               // if something is chosen from datagrid
                {
                    selectedWod.done = false;
                    ViewModel.SaveDoneWod(selectedWod);                                             // method SaveDoneWod is called
                    tbMessage.Text = $"Movement {selectedWod.movementName} is marked as Undone";    // Update bottom message row
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // ---------------------------------------------------------
        // Eventhandler for Done-checkbox in datagrid - Check
        // ---------------------------------------------------------
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAthleteGrid.SelectedIndex > -1)                                               // if something is chosen from datagrid
                {
                    selectedWod.done = true;
                    ViewModel.SaveDoneWod(selectedWod);                                             // method SaveDoneWod is called
                    tbMessage.Text = $"Movement {selectedWod.movementName} is marked as Done";      // Update bottom message row
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something isn't right...", "Oops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
