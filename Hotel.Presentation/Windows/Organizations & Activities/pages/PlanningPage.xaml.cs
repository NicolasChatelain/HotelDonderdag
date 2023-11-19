using Hotel.Domain.Managers;
using Hotel.Presentation.Model;
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

namespace Hotel.Presentation.Windows.Organizations___Activities.pages
{
    /// <summary>
    /// Interaction logic for PlanningPage.xaml
    /// </summary>
    public partial class PlanningPage : Page
    {
        private readonly OrganizationManager _manager;
        private readonly int orgID;
        private ObservableCollection<ActivityUI> activities;
        public PlanningPage(OrganizationManager OM, List<PriceInfoUI> prices, List<DescriptionUI> activity_names, int id, ObservableCollection<ActivityUI> activities)
        {
            InitializeComponent();

            pricingbox.ItemsSource = prices;
            activityname.ItemsSource = activity_names;
            _manager = OM;
            orgID = id;
            this.activities = activities;
        }

        private void PlanBTN_Click(object sender, RoutedEventArgs e)
        {
            string Fixture = fixture.Text;
            string Capacity = capacity.Text;
            DescriptionUI description = (DescriptionUI)activityname.SelectedItem;
            PriceInfoUI price = (PriceInfoUI)pricingbox.SelectedItem;

            try
            {
                _manager.ValidateExistingActivty(Fixture, Capacity);
                int id = _manager.PlanExistingActivity(price.ID, Fixture, Capacity, description.ID, orgID);
                activities.Add(new ActivityUI(id,
                                              int.Parse(Capacity),
                                              DateTime.Parse(Fixture),
                                              true,
                                              description.Name,
                                              description.Description,
                                              description.Location,
                                              int.Parse(description.Duration),
                                              price.Adultprice,
                                              price.Kidsprice,
                                              price.Discount,
                                              price.Adultage));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
