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
        internal OrganizationUI orgUI;
        internal int IdWhenUpdating;
        internal NewOrgWindow(Window owner, OrganizationUI? ORGui, OrganizationManager ORGmanager)
        {
            InitializeComponent();

            _orgManager = ORGmanager;
            this.Owner = owner;
            orgUI = ORGui;


            if (ORGui != null)
            {
                IdWhenUpdating = ORGui.ID;

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

                    if (IsUpdating)
                    {
                        _orgManager.UpdateOrganization(IdWhenUpdating, result);
                        OrganizationMapper(name, email, phone, city, street, postalcode, nr);
                    }
                    else
                    {
                        int id = _orgManager.AddOrganization(result);
                        orgUI = new(id, name, email, phone, city, street, postalcode, nr);
                    }
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong");
            }

        }

        private void OrganizationMapper(string name, string email, string phone, string city, string street, string postalcode, string nr)
        {
            orgUI.Name = name;
            orgUI.Email = email;
            orgUI.Phone = phone;
            orgUI.City = city;
            orgUI.Street = street;
            orgUI.Postalcode = postalcode;
            orgUI.Housenumber = nr;

        }

        private void CancelNewOrg_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
