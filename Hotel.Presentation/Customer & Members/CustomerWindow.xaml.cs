using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Customer___Members;
using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly CustomerManager _customerManager;
        private MemberWindow _memberWindow;
        private readonly ObservableCollection<MemberUI> _membersPerCustomer;

        public CustomerUI CustomerUI { get; set; }



        public CustomerWindow(CustomerUI customerUI, CustomerManager cm)
        {
            InitializeComponent();
            this.CustomerUI = customerUI;

            if (CustomerUI is not null) // if updating set input fields to existing data from customer
            {

                string[] addressArray = CustomerUI.GetaddressArray(CustomerUI.Address);


                Idtextbox.Text = CustomerUI.Id.ToString();
                Nametextbox.Text = CustomerUI.Name;
                Emailtextbox.Text = CustomerUI.Email;
                Phonetextbox.Text = CustomerUI.Phone;
                Citytextbox.Text = addressArray[0];
                Ziptextbox.Text = addressArray[1];
                Streettextbox.Text = addressArray[2];
                Housenumbertextbox.Text = addressArray[3];
                _membersPerCustomer = new(CustomerUI.Members); // show members
            }
            else // if customer is new, make a new empty collection for the members
            {
                _membersPerCustomer = new();
            }

            _customerManager = cm;
            MemberDataGrid.ItemsSource = _membersPerCustomer;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Address address = new(Citytextbox.Text, Streettextbox.Text, Ziptextbox.Text, Housenumbertextbox.Text);
                ContactInfo contactinfo = new(Emailtextbox.Text, Phonetextbox.Text, address);

                if (CustomerUI is null) // new customer, validate in domainlayer.
                {
                    Customer customer = new(Nametextbox.Text, contactinfo);

                    CustomerUI = new(Nametextbox.Text, Emailtextbox.Text, address.ToString(), Phonetextbox.Text);
                    CustomerUI.Members = _membersPerCustomer.ToList();
                    CustomerUI.Id = _customerManager.AddCustomer(customer, _membersPerCustomer.Select(x => new Member(x.Name, DateOnly.Parse(x.Birthday))).ToList());
                }
                else // updating existing customer
                {
                    Customer customer = new(int.Parse(Idtextbox.Text), Nametextbox.Text, contactinfo);

                    CustomerUI.Name = customer.Name;
                    CustomerUI.Phone = customer.Contact.Phone;
                    CustomerUI.Email = customer.Contact.Email;
                    CustomerUI.Address = customer.Contact.Address.ToString();

                    _customerManager.UpdateCustomerOnly(customer);


                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                CustomerUI = null;
                MessageBox.Show(ex.Message, "Something went wrong");
            }

        }

        private void Add_Member_Click(object sender, RoutedEventArgs e) // opens a new window to add members, injects the current customer's member list via ctor
        {
            _memberWindow = new(_membersPerCustomer);
            _memberWindow.UpdateConfirmation.IsEnabled = false;
            _memberWindow.ShowDialog();

            if (CustomerUI is not null) //when updating
            {
                _customerManager.AddNewMembers(
                    CustomerUI.Id,
                    CustomerUI.Members.Select(x => new Member(x.Name, DateOnly.Parse(x.Birthday))).ToList(),
                    _membersPerCustomer.Select(x => new Member(x.Name, DateOnly.Parse(x.Birthday))).ToList());
            }

            CustomerUI.Members = _membersPerCustomer.ToList();
        }

        private void Delete_Member_Click(object sender, RoutedEventArgs e) // if a member is selected in the grid delete that member from the current customer
        {
            if (MemberDataGrid.SelectedItem is not null)
            {
                MemberUI mui = MemberDataGrid.SelectedItem as MemberUI;

                if (CustomerUI is not null)
                {
                    try
                    {
                        _customerManager.RemoveMember(CustomerUI.Id, new Member(mui.Name, DateOnly.Parse(mui.Birthday)));
                        _membersPerCustomer.Remove(MemberDataGrid.SelectedItem as MemberUI);
                    }
                    catch
                    {

                    }

                }
                else
                {
                    _membersPerCustomer.Remove(MemberDataGrid.SelectedItem as MemberUI);
                }

                CustomerUI.Members = _membersPerCustomer.ToList();
            }

        }


        private void Update_Member_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem != null)
            {
                MemberUI MUI = (MemberUI)MemberDataGrid.SelectedItem;

                string name = MUI.Name;
                string birthday = MUI.Birthday;

                _memberWindow = new(_membersPerCustomer);
                _memberWindow.namebox.Text = name;
                _memberWindow.birthdaybox.Text = birthday;
                _memberWindow.UpdateConfirmation.IsEnabled = true;
                _memberWindow.ShowDialog();

                if (_memberWindow.DialogResult == true)
                {
                    Member MemberOriginalState = new(name, DateOnly.Parse(birthday));

                    string updatedName = _memberWindow.namebox.Text;
                    string updatedBirthday = _memberWindow.birthdaybox.Text;
                    int id = CustomerUI.Id;

                    Member MemberUpdatedState = new(updatedName, DateOnly.Parse(updatedBirthday));

                    _customerManager.UpdateMember(id, MemberOriginalState, MemberUpdatedState); //update DB

                    MUI.Name = updatedName;
                    MUI.Birthday = updatedBirthday;
                }

            }
        }



    }
}
