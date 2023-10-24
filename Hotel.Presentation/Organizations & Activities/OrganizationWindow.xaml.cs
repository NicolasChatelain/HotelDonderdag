using Hotel.Domain.Model;
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
using System.Windows.Shapes;

namespace Hotel.Presentation.Organizations___Activities
{
    /// <summary>
    /// Interaction logic for OrganizationWindow.xaml
    /// </summary>
    public partial class OrganizationWindow : Window
    {
        private NewOrgWindow _newOrgWindow;

        ObservableCollection<Organizer> orgs = new()
            {
                new Organizer{ Name = "org1" },
                new Organizer{ Name = "amuchlongerorgthistime"},
                new Organizer{ Name = "qkjsdhqkjsdhkqsjdh"}
            };

        public OrganizationWindow()
        {
            InitializeComponent();
           

            OrganisationsComboBox.ItemsSource = orgs;
        }

        private void Add_New_Organization_Button_Click(object sender, RoutedEventArgs e)
        {
            _newOrgWindow = new();
            _newOrgWindow.Show();

        }

       

        
    }
}
