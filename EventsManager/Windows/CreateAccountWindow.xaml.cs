using EventsManager.Classes;
using EventsManagerLogic.Helpers;
using EventsManagerLogic.WindowsHelpers;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;

namespace EventsManager.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        CreateAccountWindowHelper windowHelper = new CreateAccountWindowHelper();
        
        TextValidator tv = new TextValidator();
        public CreateAccountWindow()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            if(tv.basicTextCheck(txtUsername.Text,4,30) == true && tv.basicTextCheck(txtPassword.Password, 4, 30) == true
                && tv.basicTextCheck(txtRePassword.Password, 4, 150) == true && tv.basicTextCheck(txtEmail.Text, 4, 200) == true)
            {
                string errors = windowHelper.CreateAccount(txtUsername.Text, txtPassword.Password, txtRePassword.Password, txtEmail.Text);

                if(errors == "")
                {
                    MessageBox.Show("Account Created Successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errors, "Alert", MessageBoxButton.OK);
                }
            }
        }
    }
}
