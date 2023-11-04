using Hotel.Domain.Managers;
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

namespace Hotel.Presentation
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        public ObservableCollection<ActivityUI> activities;
        private readonly OrganizationManager OM;
        private int orgID;

        public ActivityWindow(int id)
        {
            InitializeComponent();
            OM = new(RepositoryFactory.OrganizationRepository);
            orgID = id;



            activities = new(OM.GetAllActivities(id).Select(x => new ActivityUI(
                                                      x.Id,
                                                      x.Capacity,
                                                      x.Fixture,
                                                      x.Description.Name,
                                                      x.Description.DetailedDescription,
                                                      x.Description.Location,
                                                      x.Description.Duration,
                                                      x.PriceInfo.AdultPrice,
                                                      x.PriceInfo.ChildPrice,
                                                      x.PriceInfo.DiscountPercentage,
                                                      x.PriceInfo.AdultAge
                                                      )));

            activitiesgrid.ItemsSource = activities;

        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateActivity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveActivity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Activitiesgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActivityUI activity = (ActivityUI)activitiesgrid.SelectedItem;

            detailsblock.Text = activity.ToString();
        }
    }
}
