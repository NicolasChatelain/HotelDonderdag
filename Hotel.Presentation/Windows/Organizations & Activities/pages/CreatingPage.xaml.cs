using Hotel.Domain.Managers;
using Hotel.Presentation.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Presentation.Windows.Organizations___Activities.pages
{
    /// <summary>
    /// Interaction logic for CreatingPage.xaml
    /// </summary>
    public partial class CreatingPage : Page
    {

        internal ActivityUI activity = new();
        internal bool confirmed = false;
        private OrganizationManager OM;

        public CreatingPage(OrganizationManager manager)
        {
            InitializeComponent();
            OM = manager;
        }

        private void ConfirmBTN_Click(object sender, RoutedEventArgs e)
        {
            string Name = name.Text;
            string Capacity = capacity.Text;
            string Fixture = fixture.Text;
            string Location = location.Text;
            string Duration = duration.Text;
            string Adultprice = adultprice.Text;
            string Kidsprice = kidsprice.Text;
            string Discount = discount.Text;
            string Adultage = adultage.Text;
            string Description = description.Text;

            try
            {
                OM.ValidateActivity(Name, Capacity, Fixture, Location, Duration, Adultprice, Kidsprice, Discount, Adultage, Description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //todo add acitivyty after validating

        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
