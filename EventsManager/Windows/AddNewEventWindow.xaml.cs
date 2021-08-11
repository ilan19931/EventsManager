using EventsManagerLogic.Classes;
using EventsManagerLogic.Events;
using EventsManagerLogic.Helpers;
using EventsManagerLogic.WindowsHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace EventsManager.Windows
{
    /// <summary>
    /// Interaction logic for AddNewEventWindow.xaml
    /// </summary>
    public partial class AddNewEventWindow : Window
    {
        AddNewEventWindowHelper windowHelper = new AddNewEventWindowHelper();
        List<Category> categories = null;
        List<Category> subCategories = null;
        ObservableCollection<EventMode> eventModes = null;

        public AddNewEventWindow()
        {
            InitializeComponent();

            initWindow();

        }

        private void initWindow()
        {
            comboBoxCategory.Items.Clear();
            comboBoxSubCategory.Items.Clear();
            listViewModes.Items.Clear();

            fetchEventModes();
            fetchCategories();
        }

        private void fetchEventModes()
        {
            eventModes = new ObservableCollection<EventMode>();
            eventModes = windowHelper.GetEventModes();

            listViewModes.ItemsSource = eventModes;
            listViewModes.SelectedValuePath = "Mode";
        }
        private void fetchCategories()
        {
            categories = windowHelper.GetCategories();

            comboBoxCategory.DisplayMemberPath = "Title";
            comboBoxCategory.SelectedValuePath = "Id";
            comboBoxCategory.ItemsSource = categories;
        }
        private void fetchSubCategories(int i_Val)
        {
            subCategories = windowHelper.GetCategories(i_Val);

            comboBoxSubCategory.DisplayMemberPath = "Title";
            comboBoxSubCategory.SelectedValuePath = "Id";
            comboBoxSubCategory.ItemsSource = subCategories;

        }


        private void doSaveEvent()
        {
            string title = "";
            string eventDetails = textEventDetails.Text;
            int category = -1;
            int eEventMode = -1;
            Event newEvent;
            string errors = null;
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            if (listViewModes != null && listViewModes.SelectedValue != null)
                eEventMode = (int)listViewModes.SelectedValue;

            if (comboBoxCategory.SelectedValue != null && comboBoxSubCategory.SelectedValue == null)
            {
                int.TryParse(comboBoxCategory.SelectedValue.ToString(), out category);
                title = $"{comboBoxCategory.Text}";

            }
            else if(comboBoxSubCategory.SelectedValue != null)
            {
                int.TryParse(comboBoxSubCategory.SelectedValue.ToString(), out category);
                title = $"{comboBoxCategory.Text} -> {comboBoxSubCategory.Text}";
            }

            newEvent = windowHelper.InsertEvent(title, eventDetails,(int) eEventMode,category, out errors);

            if(errors == "")
            {
                MessageBox.Show("Event Added Succesfully!");

                mainWindow.AddItemIntoEventsList(newEvent);

                this.Close();
            }
            else
            {
                MessageBox.Show(errors,"alert",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }


        // controlls events
        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val = 0;
            int.TryParse((sender as ComboBox).SelectedValue.ToString(), out val);
            fetchSubCategories(val);
        }
        private void buttonSaveEvent_Click(object sender, RoutedEventArgs e)
        {
            doSaveEvent();
        }
    }
}
