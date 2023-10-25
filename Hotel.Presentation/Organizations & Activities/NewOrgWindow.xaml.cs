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
        private readonly bool IsUpdating = false;
        private readonly bool Remove;
        internal NewOrgWindow(object owner, OrganizationUI? ORGui, bool remove)
        {
            InitializeComponent();
            _orgManager = new(RepositoryFactory.OrganizationRepository);
            this.Owner = (Window)owner;
            Remove = remove;

            if (Remove)
            {
                RemoveOrganization(ORGui.ID);
            }
            else
            {
                if (ORGui != null)
                {
                    orgnameB.Text = ORGui.Name;
                    emailB.Text = ORGui.Email;
                    phoneB.Text = ORGui.Phone;
                    cityB.Text = ORGui.City;
                    streetB.Text = ORGui.Street;
                    zipB.Text = ORGui.Postalcode;
                    NRB.Text = ORGui.Housenumber;

                    ConfirmNewOrg.Content = "Update";
                    IsUpdating = true;
                }
            }

        }

        internal OrganizationUI orgUI;

        private void ConfirmNewOrg_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
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

                    if (IsUpdating)
                    {
                        _orgManager.UpdateOrganization(result);
                    }
                    else
                    {
                        id = _orgManager.AddOrganization(result);
                    }
                    orgUI = new(id, name, email, phone, city, street, postalcode, nr);
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

        private void RemoveOrganization(int ID)
        {
            _orgManager.RemoveOrganziation(ID);
        }
    }
}
