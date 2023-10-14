using Hotel.Domain.Managers;
using Hotel.Domain.Model;
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
using System.Windows.Shapes;

namespace Hotel.Presentation
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerUI CustomerUI { get; set; }
        private CustomerManager _customerManager;

        public CustomerWindow(CustomerUI customerUI, CustomerManager cm)
        {
            InitializeComponent();
            this.CustomerUI = customerUI;

            if (CustomerUI is not null) // if updating...
            {
                Idtextbox.Text = CustomerUI.Id.ToString();
                Nametextbox.Text = CustomerUI.Name;
                Emailtextbox.Text = CustomerUI.Email;
                Phonetextbox.Text = CustomerUI.Phone;
            }
            else
            {
                Idtextbox.IsReadOnly = true;
            }

            _customerManager = cm;
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
                Customer customer = new(Nametextbox.Text, contactinfo);

                if (CustomerUI is null)
                {
                    CustomerUI = new(Nametextbox.Text, Emailtextbox.Text, address.ToString(), Phonetextbox.Text, 0);
                }
                else
                {
                    CustomerUI.Email = Emailtextbox.Text;
                    CustomerUI.Phone = Phonetextbox.Text;
                    CustomerUI.Name = Nametextbox.Text;
                }

                DialogResult = true;

                _customerManager.AddCustomer(customer);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong");
            }

            
        }
    }
}
