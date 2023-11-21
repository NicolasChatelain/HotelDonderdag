using Hotel.Domain.Managers;
using Hotel.Presentation.Mapper;
using Hotel.Presentation.Model;
using Hotel.Presentation.Windows.Organizations___Activities.pages;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
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
        private readonly int orgID;
        private string? filter;
        private readonly DetailsPage details;

        public ActivityWindow(int id)
        {
            InitializeComponent();
            OM = new(RepositoryFactory.OrganizationRepository);
            orgID = id;
            activities = new(MapActivity.FromDomainToUI(OM, orgID, !(bool)activebox.IsChecked, filter = null));
            activitiesgrid.ItemsSource = activities;
            frame.Navigate(details = new());
        }

        private void RemoveActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityUI activity = (ActivityUI)activitiesgrid.SelectedItem;

            if (activity is not null)
            {
                var response = MessageBox.Show("Are you sure?", "Removing....", MessageBoxButton.YesNo);

                if (response == MessageBoxResult.Yes)
                {
                    OM.RemoveActivity(activity.Id);
                    activity.IsUpcoming = false;

                    if (!(bool)activebox.IsChecked)
                    {
                        activities.Remove(activity);
                    }
                }

            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SearchTextBox.Text.Length > 0)
            {
                searchLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                searchLabel.Visibility = Visibility.Visible;

                activities = new(MapActivity.FromDomainToUI(OM, orgID, !(bool)activebox.IsChecked, filter = null));
                activitiesgrid.ItemsSource = activities;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

            string query = SearchTextBox.Text;

            if (query != string.Empty)
            {
                filter = query;
            }
            else
            {
                filter = null;
            }
            activities = new(MapActivity.FromDomainToUI(OM, orgID, !(bool)activebox.IsChecked, filter));
            activitiesgrid.ItemsSource = activities;

        }

        private void New_Activity_Click(object sender, RoutedEventArgs e)
        {
            CreatingPage cp = new(OM, orgID, activities);
            frame.Navigate(cp);
        }

        private void Plan_Activity_Click(object sender, RoutedEventArgs e)
        {
            PlanningPage pl = new(OM, MapPriceInfo.FromDomainToUI(OM.GetAllPrices(orgID)), MapDescription.FromDomainToUI(OM.GetAllDescriptions(orgID)), orgID, activities);
            frame.Navigate(pl);
        }
    }
}
