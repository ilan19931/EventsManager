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

namespace EventsManager.Windows.Pop_Ups
{
    /// <summary>
    /// Interaction logic for AddCategoryPopUpWindow.xaml
    /// </summary>
    public partial class AddCategoryPopUpWindow : Window
    {
        public bool IsSubmit { get; set; }
        public AddCategoryPopUpWindow()
        {
            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            this.IsSubmit = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
