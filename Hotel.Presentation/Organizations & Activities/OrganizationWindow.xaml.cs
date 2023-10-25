using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using Hotel.Util;
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
        private OrganizationManager _organizationManager;
        private readonly ObservableCollection<OrganizationUI> orgs;


        public OrganizationWindow()
        {
            InitializeComponent();
            _organizationManager = new(RepositoryFactory.OrganizationRepository);
            orgs = new(_organizationManager.GetAllOrganizations().Select(org => new OrganizationUI(org.Id, 
                                                                                                   org.Name, 
                                                                                                   org.Contact.Email, 
                                                                                                   org.Contact.Phone, 
                                                                                                   org.Contact.Address.City,
                                                                                                   org.Contact.Address.Street,
                                                                                                   org.Contact.Address.PostalCode,
                                                                                                   org.Contact.Address.HouseNumber)));


            OrganisationsComboBox.ItemsSource = orgs;
        }

        private void Add_New_Organization_Button_Click(object sender, RoutedEventArgs e)
        {
            _newOrgWindow = new(this, null, false);
            _newOrgWindow.ShowDialog();

            if (_newOrgWindow.DialogResult == true)
            {
                orgs.Add(_newOrgWindow.orgUI);
            }
        }

        private void Update_Organization_Click(object sender, RoutedEventArgs e)
        {
            OrganizationUI orgui = (OrganizationUI)OrganisationsComboBox.SelectedItem;

            _newOrgWindow = new(this, orgui, false);
            _newOrgWindow.ShowDialog();

            if (_newOrgWindow.DialogResult == true)
            {
                OrganizationUI? ORG = orgs.FirstOrDefault(org => org.ID == orgui.ID);
                if (ORG != null)
                {
                    ORG = _newOrgWindow.orgUI;
                }
            }
        }

        private void OrganisationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveORG.Visibility = Visibility.Visible;
            Update_Organization.IsEnabled = true;
            Manage_activities.IsEnabled = true;
        }

        private void RemoveORG_Click(object sender, RoutedEventArgs e)
        {
            OrganizationUI orgui = (OrganizationUI)OrganisationsComboBox.SelectedItem;

            var result = MessageBox.Show($"Do you want remove {orgui.Name}?", "remove", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _newOrgWindow = new(this, orgui, true);
                orgs.Remove(orgui);
            }
            else
            {
                OrganisationsComboBox.SelectedItem = null;
                RemoveORG.Visibility = Visibility.Collapsed;
                Update_Organization.IsEnabled = false;
                Manage_activities.IsEnabled = false;
            }
        }

        private void Manage_activities_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
