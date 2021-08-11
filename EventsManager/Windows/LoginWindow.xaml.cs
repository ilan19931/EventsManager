using EventsManager.Classes;
using EventsManagerLogic.Classes;
using EventsManagerLogic.WindowsHelpers;
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
        LoginWindowHelper windowHelper = new LoginWindowHelper();

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
                User newUser = windowHelper.CheckLoginDetails(username, password);
                if (newUser != null)
                {
                    string accessKey = "";
                    if (checkBoxRememberMe.IsChecked == true)
                    {
                        accessKey = Helper.security.GenerateAccessKey();

                        // insert accesskey into loggedInAccounts table
                        windowHelper.InsertAccessKey(newUser.Id,accessKey);
                    }

                    Helper.appSettings.AccessKey = accessKey;
                    Helper.appSettings.RememberMe = checkBoxRememberMe.IsChecked.Value;
                    Helper.appSettings.SaveSettings();

                    Helper.user = newUser;
                    DialogResult = true;
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
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            createAccountWindow.ShowDialog();
        }
    }
}
