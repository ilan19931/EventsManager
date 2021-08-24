using EventsManagerLogic.Classes;
using EventsManagerLogic.WindowsHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace EventsManager.Windows.Admin_Panel
{
    /// <summary>
    /// Interaction logic for ModesPage.xaml
    /// </summary>
    public partial class ModesPage : Page
    {
        AddNewEventWindowHelper helper = new AddNewEventWindowHelper();
        ObservableCollection<EventMode> eventModes = null;


        public ModesPage()
        {
            InitializeComponent();

            fetchEventModes();
        }

        private void fetchEventModes()
        {
            eventModes = helper.GetEventModes();

            listViewModes.Items.Clear();
            listViewModes.ItemsSource = eventModes;
            listViewModes.SelectedValuePath = "Mode";
        }
    }
}
