using EventsManager.Windows;
using EventsManagerLogic;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Events;
using EventsManagerLogic.Helpers;
using EventsManagerLogic.WindowsHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace EventsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowHelper windowHelper = new MainWindowHelper();

        private ObservableCollection<EventInList> eventsList = null;
        private ObservableCollection<EventInList> filteredEvents = null;
        private ObservableCollection<EventInList> lastEventsList = null;
        private ObservableCollection<EventMode> eventFiltersModes;

        private ObservableCollection<Comment> commentsList = null;

        private EeventModeSearchFilter m_CurrEventModeFilter = EeventModeSearchFilter.AllOpen;

        public Statistics statistics = new Statistics();

        public MainWindow()
        {
            InitializeComponent();

            initWindow();
        }

        private void initWindow()
        {
            Helper.appSettings = Helper.appSettings.LoadSettings();

            doLogin();

            initEvents();
            initFilterPanel();
        }


        //user section
        private void doLogin()
        {
            // do auto login with access key
            if (Helper.appSettings.RememberMe == true && !string.IsNullOrEmpty(Helper.appSettings.AccessKey))
            {
                User user = windowHelper.CheckAccessKey(Helper.appSettings.AccessKey);
                if (user != null)
                {
                    Helper.user = user;
                }
            }

            // ask for login untill user is logged
            while (Helper.user == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                bool? result = loginWindow.ShowDialog();

                if (result == false)
                {
                    Environment.Exit(0);
                }
            }

            this.Visibility = Visibility.Visible;

            textBlockWelcome.Text = "Welcome: " + Helper.user.Username;
        }
        private void doLogout()
        {
            if (Helper.user != null)
            {
                Helper.user = null;
                Helper.appSettings.RememberMe = false;
                Helper.appSettings.SaveSettings();

                this.Visibility = Visibility.Hidden;

                doLogin();
            }
        }


        //events section
        private void initEvents()
        {
            eventsList = windowHelper.GetEvents();
            lastEventsList = eventsList;
            listBoxEvents.SelectedValuePath = "MyEvent.Id";
            listBoxEvents.ItemsSource = eventsList;

            statistics.EventsCount = eventsList.Count;
        }
        private void doOpenAddNewEvent()
        {
            AddNewEventWindow addNewEventWindow = new AddNewEventWindow();
            addNewEventWindow.ShowDialog();

        }
        public void AddItemIntoEventsList(Event i_Event)
        {
            EventInList newEvent = new EventInList();
            newEvent.MyEvent = i_Event;
            newEvent.EventStatus = (EEventStatus)i_Event.IsClosed;
            eventsList.Insert(0, newEvent);

            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = eventsList;
        }
        private void addNewComment()
        {
            if (!string.IsNullOrEmpty(textCommentDetails.Text))
            {
                int eventId = (int)listBoxEvents.SelectedValue;

                string errors;
                Comment newComment = windowHelper.AddNewComment(Helper.user.Id, Helper.user.Username, eventId, textCommentDetails.Text, out errors);

                if(errors == "")
                {
                    if (commentsList.Count == 1 && commentsList[0].Id == 0)
                        commentsList.Clear();

                    MessageBox.Show("Comment Created!");
                    listBoxComments.ItemsSource = null;
                    commentsList.Add(newComment);
                    listBoxComments.ItemsSource = commentsList;

                    textCommentDetails.Text = "";

                }
                else
                {
                    MessageBox.Show(errors);
                }
            }
        }
        private void doCloseEvent(int i_Tag)
        {
            Helper.sql.CloseEvent(i_Tag);

            EventInList ev = (EventInList) lastEventsList.Where(x => x.MyEvent.Id == i_Tag).FirstOrDefault();
            lastEventsList.Remove(ev);
            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = lastEventsList;
        }


        // filters section
        private void initFilterPanel()
        {
            fetchEventModesFilter();
        }
        private void fetchEventModesFilter()
        {
            comboBoxEventModes.Items.Clear();

            eventFiltersModes = windowHelper.GetEventFiltersModes();

            comboBoxEventModes.SelectedValuePath = "Mode";
            comboBoxEventModes.SelectedIndex = 0;
            comboBoxEventModes.ItemsSource = eventFiltersModes;
        }
        private void fetchEventsByModeFilter()
        {
            // show all events (no filter)
            if (comboBoxEventModes.SelectedValue == null || (EeventModeSearchFilter)comboBoxEventModes.SelectedValue == EeventModeSearchFilter.AllEvents)
            {
                listBoxEvents.ItemsSource = null;
                listBoxEvents.ItemsSource = eventsList;

                m_CurrEventModeFilter = EeventModeSearchFilter.AllOpen;

                statistics.EventsCount = eventsList.Count;
            }
            else
            {
                int n = (int)comboBoxEventModes.SelectedValue;
                EeventModeSearchFilter eventModeFilter = (EeventModeSearchFilter)n;

                m_CurrEventModeFilter = eventModeFilter;


                lastEventsList = windowHelper.GetEventsByQuery(m_CurrEventModeFilter);

                listBoxEvents.ItemsSource = null;
                listBoxEvents.ItemsSource = lastEventsList;

                statistics.EventsCount = lastEventsList.Count;
            }
        }
        private void fetchEventsByTextSearch()
        {
            string txtSearch = textEventSearch.Text;

            if (!string.IsNullOrEmpty(txtSearch))
            {
                filteredEvents = windowHelper.GetTextFilteredEvents(txtSearch, m_CurrEventModeFilter);


                listBoxEvents.ItemsSource = null;
                listBoxEvents.ItemsSource = filteredEvents;

                statistics.EventsCount = filteredEvents.Count;
            }
            else
            {
                listBoxEvents.ItemsSource = null;
                listBoxEvents.ItemsSource = lastEventsList;

                statistics.EventsCount = lastEventsList.Count;
            }
        }
        private void fetchComments()
        {
            int eventId = 0;

            if (listBoxEvents.SelectedItem != null && listBoxEvents.SelectedValue != null)
            {
                eventId = (int) listBoxEvents.SelectedValue;
            }

            commentsList = windowHelper.GetEventComments(eventId);


            listBoxComments.ItemsSource = null;
            listBoxComments.ItemsSource = commentsList;
        }


        // controls events section
        private void buttonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            doLogout();
        }
        private void buttonAddEvent_Click(object sender, RoutedEventArgs e)
        {
            doOpenAddNewEvent();

        }
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            fetchEventsByTextSearch();
        }
        private void buttonCloseEvent_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            doCloseEvent((int)button.Tag);
        }
        private void buttonAddComment_Click(object sender, RoutedEventArgs e)
        {
            addNewComment();
        }

        private void comboBoxEventModes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fetchEventsByModeFilter();
        }
        private void textEventSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.fetchEventsByTextSearch();
        }
        private void listBoxEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fetchComments();
        }

    }
}
