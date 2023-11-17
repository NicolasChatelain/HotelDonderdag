using Hotel.Domain.Managers;
using Hotel.Presentation.Mapper;
using Hotel.Presentation.Model;
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
        private int orgID;
        private string? filter;

        public ActivityWindow(int id)
        {
            InitializeComponent();
            OM = new(RepositoryFactory.OrganizationRepository);
            orgID = id;

            activities = new(MapActivity.FromDomainToUI(OM, orgID, (bool)activebox.IsChecked, filter = null));
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
            ActivityUI activity = (ActivityUI)activitiesgrid.SelectedItem;

            if (activity is not null)
            {
                var response = MessageBox.Show("Are you sure?", "Removing....", MessageBoxButton.YesNo);

                if (response == MessageBoxResult.Yes)
                {
                    OM.RemoveActivity(activity.Id);
                    activity.IsActive = false;
                    activities.Remove(activity);
                }

            }

        }

        private void Activitiesgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ActivityUI activity = (ActivityUI)activitiesgrid.SelectedItem;
            if (activity is not null)
            {
                detailsblock.Text = activity.ToString();
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
                activitiesgrid.ItemsSource = MapActivity.FromDomainToUI(OM, orgID, (bool)activebox.IsChecked, filter = null);
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
            activitiesgrid.ItemsSource = MapActivity.FromDomainToUI(OM, orgID, (bool)activebox.IsChecked, filter);

        }


    }
}
