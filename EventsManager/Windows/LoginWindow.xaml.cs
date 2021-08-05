using EventsManager.Classes;
using EventsManagerLogic.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private TextValidator m_TextValidator = new TextValidator();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Password;

            if (m_TextValidator.basicTextCheck(username, 4, 30) && m_TextValidator.basicTextCheck(password, 4, 150))
            {
                User newUser = Helper.sql.checkLoginDetails(username, password);
                if (newUser != null)
                {
                    Helper.user = newUser;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("wrong details", "Alert", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("wrong details", "Alert", MessageBoxButton.OK);
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
