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
    /// Interaction logic for EditCategoryPopUpWindow.xaml
    /// </summary>
    public partial class EditCategoryPopUpWindow : Window
    {
        public bool IsSubmit { get; set; }
        public EditCategoryPopUpWindow(string i_CategoryTitle)
        {
            InitializeComponent();

            textCategory.Text = i_CategoryTitle;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textCategory.Text))
            {
                this.IsSubmit = true;
                this.Close();
            }
        }
    }
}
