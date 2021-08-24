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

namespace EventsManager.Windows.Admin_Panel
{
    /// <summary>
    /// Interaction logic for AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
        }

        private void updateFrame(object sender)
        {
            mainFrame.Source = new Uri("Pages/" + (sender as Button).Tag.ToString() + ".xaml", UriKind.Relative);

        }

        private void buttonCategories_Click(object sender, RoutedEventArgs e)
        {
            updateFrame(sender);
        }

        private void buttonEventModes_Click(object sender, RoutedEventArgs e)
        {
            updateFrame(sender);
        }

        private void buttonManageUsers_Click(object sender, RoutedEventArgs e)
        {
            updateFrame(sender);
        }
    }
}
