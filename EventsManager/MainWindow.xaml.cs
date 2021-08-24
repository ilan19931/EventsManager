using EventsManager.Windows;
using EventsManager.Windows.Admin_Panel;
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

        private ObservableCollection<Event> eventsList = null;
        private ObservableCollection<Event> lastEventsList = null;
        private ObservableCollection<EventMode> eventModesFilter;
        private ObservableCollection<EventStateFilter> eventStatesFilter = new ObservableCollection<EventStateFilter>();

        private ObservableCollection<Comment> commentsList = null;

        private EEventMode? m_CurrModeFilter;
        private EstateFilter m_CurrentStateFilter;
        private string m_CurrentTextFilter;

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
                    windowHelper.GroupId = Helper.user.GroupId;
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

                windowHelper.GroupId = Helper.user.GroupId;
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

                initWindow();
            }
        }


        //events section
        private void initEvents()
        {
            eventsList = windowHelper.GetEvents();
            lastEventsList = eventsList;

            listBoxEvents.SelectedValuePath = "Id";
            listBoxEvents.ItemsSource = eventsList;

            statistics.EventsCount = eventsList.Count;
            textStatisticsCount.Text = statistics.EventsCount.ToString();
        }
        private void doOpenAddNewEvent()
        {
            AddNewEventWindow addNewEventWindow = new AddNewEventWindow();
            addNewEventWindow.ShowDialog();

        }
        public void AddItemIntoEventsList(Event i_Event)
        {
            Event newEvent = i_Event;

            eventsList.Insert(0, newEvent);

            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = eventsList;

            updateStatistics();
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

            Event ev = (Event) lastEventsList.Where(x => x.Id == i_Tag).FirstOrDefault();
            lastEventsList.Remove(ev);
            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = lastEventsList;
        }
        private void fetchComments()
        {
            int eventId = 0;

            if (listBoxEvents.SelectedItem != null && listBoxEvents.SelectedValue != null)
            {
                eventId = (int)listBoxEvents.SelectedValue;
            }

            commentsList = windowHelper.GetEventComments(eventId);


            listBoxComments.ItemsSource = null;
            listBoxComments.ItemsSource = commentsList;
        }

        private void updateStatistics()
        {
            statistics.EventsCount = listBoxEvents.Items.Count;
            textStatisticsCount.Text = statistics.EventsCount.ToString();
        }




        // filters section
        private void initFilterPanel()
        {
            fetchEventModesFilter();
            fetchEventStateFilters();
        }

        private void fetchEventStateFilters()
        {
            foreach (EstateFilter eState in Enum.GetValues(typeof(EstateFilter)))
            {
                eventStatesFilter.Add(EventStateFilter.CreateStateFilter(eState));
            }

            comboBoxStateFilter.ItemsSource = null;
            comboBoxStateFilter.ItemsSource = eventStatesFilter;
            comboBoxStateFilter.SelectedIndex = 0;
        }
        private void fetchEventModesFilter()
        {
            comboBoxEventModes.ItemsSource = null;

            eventModesFilter = windowHelper.GetEventFiltersModes();

            comboBoxEventModes.SelectedValuePath = "Mode";
            comboBoxEventModes.SelectedIndex = 0;
            comboBoxEventModes.ItemsSource = eventModesFilter;
        }


        private void fetchEventsByModeFilter()
        {
            ObservableCollection<Event> list = null;
            m_CurrModeFilter = (comboBoxEventModes.SelectedValue != null) ? (EEventMode?)comboBoxEventModes.SelectedValue : null;

            list = windowHelper.GetEventsByModeFilter(m_CurrModeFilter, m_CurrentStateFilter, m_CurrentTextFilter);
            lastEventsList = list;

            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = list;

            updateStatistics();
        }

        private void fetchEventsByTextSearch()
        {
            ObservableCollection<Event> list = new ObservableCollection<Event>();
            m_CurrentTextFilter = (!string.IsNullOrEmpty(textEventSearch.Text)) ? textEventSearch.Text : null;

            list = windowHelper.GetTextFilteredEvents(m_CurrentTextFilter, m_CurrModeFilter, m_CurrentStateFilter);
            lastEventsList = list;

            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = list;

            updateStatistics();
        }
        private void fetchEventsByStateFilter()
        {
            if (comboBoxStateFilter.SelectedValue != null)
            {
                m_CurrentStateFilter = (EstateFilter)((EventStateFilter)comboBoxStateFilter.SelectedValue).State;
            }

            ObservableCollection<Event> list = new ObservableCollection<Event>();

            list = windowHelper.GetEventsByStateFilter(m_CurrModeFilter, m_CurrentStateFilter, m_CurrentTextFilter);
            lastEventsList = list;

            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = list;

            updateStatistics();

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

        private void comboBoxStateFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fetchEventsByStateFilter();
        }

        private void buttonOpenAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            if(Helper.user != null && Helper.user.IsAdmin == true)
            {
                AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
                adminPanelWindow.ShowDialog();
            }
        }
    }
}
