using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Managers;
using Hotel.Presentation.Model;
using Hotel.Util;
using System;
using System.Windows;

namespace Hotel.Presentation.Organizations___Activities
{
    /// <summary>
    /// Interaction logic for NewOrgWindow.xaml
    /// </summary>
    public partial class NewOrgWindow : Window
    {
        private readonly OrganizationManager _orgManager;
        public NewOrgWindow()
        {
            InitializeComponent();
            _orgManager = new(RepositoryFactory.OrganizationRepository);
        }

        internal OrganizationUI orgUI;

        private void ConfirmNewOrg_Click(object sender, RoutedEventArgs e)
        {
            string name = orgnameB.Text;
            string email = emailB.Text;
            string phone = phoneB.Text;
            string city = cityB.Text;
            string street = streetB.Text;
            string postalcode = zipB.Text;
            string nr = NRB.Text;

            try
            {
                object result = _orgManager.ValidateInputs(name, email, phone, city, street, postalcode, nr);

                if (result != null)
                {
                    //_orgManager.AddOrganization(result);
                    orgUI = new(name, email, phone, city, street, postalcode, nr);
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong");
            }

        }

        private void CancelNewOrg_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
