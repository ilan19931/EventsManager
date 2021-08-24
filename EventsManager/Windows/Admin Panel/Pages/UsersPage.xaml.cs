using EventsManagerLogic.Classes;
using EventsManagerLogic.WindowsHelpers.AdminPanel;
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

namespace EventsManager.Windows.Admin_Panel.Pages
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        List<User> m_AllUsers = new List<User>();
        User m_CurrentUser = null;

        ManageUsersWindowHelper m_WindowHelper = new ManageUsersWindowHelper(); 
        public UsersPage()
        {
            InitializeComponent();

            initPage();
        }

        private void initPage()
        {
            fetchAllUsers();
        }

        private void fetchAllUsers()
        {
            m_AllUsers = m_WindowHelper.GetAllUsers();

            listBoxUsers.ItemsSource = m_AllUsers;
            listBoxUsers.SelectedValuePath = "Id";
        }

        private void textUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchFilteredUsers();
        }

        private void fetchFilteredUsers()
        {
            List<User> filteredUsers;
            if (!string.IsNullOrEmpty(textUsername.Text))
            {
                filteredUsers = m_AllUsers.Where(user => user.Username.Contains(textUsername.Text)).ToList();
            }
            else
            {
                filteredUsers = m_AllUsers;
            }

            listBoxUsers.ItemsSource = filteredUsers;
            listBoxUsers.SelectedValuePath = "Id";
        }

        private void listBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeCurrentUser();
        }

        private void changeCurrentUser()
        {
            if (listBoxUsers.SelectedValue != null)
            {
                int userId = (int)listBoxUsers.SelectedValue;

                List<User> list = (List<User>)m_AllUsers.Where(user => user.Id == userId).ToList();
                this.m_CurrentUser = list[0];

                gridMain.Visibility = Visibility.Visible;
                cardMain.DataContext = m_CurrentUser;
            }
        }
    }
}
