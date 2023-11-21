using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Mapper;
using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

namespace Hotel.Presentation.Windows.Organizations___Activities.pages
{
    /// <summary>
    /// Interaction logic for CreatingPage.xaml
    /// </summary>
    public partial class CreatingPage : Page
    {

        private readonly OrganizationManager _manager;
        private readonly int orgID;
        ObservableCollection<ActivityUI> activities;
        private readonly DetailsPage details;
        public CreatingPage(OrganizationManager manager, int orgID, ObservableCollection<ActivityUI> activities)
        {
            InitializeComponent();

            _manager = manager;
            this.orgID = orgID;
            this.activities = activities;
            details = new();
        }

        private void ConfirmBTN_Click(object sender, RoutedEventArgs e)
        {
            string Name = name.Text;
            string Fixture = fixture.Text;
            string Capacity = capacity.Text;
            string Location = location.Text;
            string Duration = duration.Text;
            string Adultprice = adultprice.Text;
            string Kidsprice = kidsprice.Text;
            string Discount = discount.Text;
            string Adultage = adultage.Text;
            string Description = description.Text;

            try
            {
                Activity activity = _manager.ValidateActivity(Name, Fixture, Capacity, Location, Duration, Adultprice, Kidsprice, Discount, Adultage, Description, true);

                if (activity is not null)
                {
                    _manager.AddActivityToOrganization(activity, orgID);

                    ActivityUI activityUI = MapActivity.FromUItoUI(activity);
                    activities.Add(activityUI);
                    NavigationService.Navigate(details);
                    details.detailsblock.Text = activityUI.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(details);
        }
    }
}
