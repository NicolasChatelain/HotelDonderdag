using Hotel.Domain.Managers;
using Hotel.Domain.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CustomerUI> customerUisCollection = new();
        private readonly CustomerManager customerManager;


        public MainWindow()
        {
            InitializeComponent();

            customerManager = new(RepositoryFactory.CustomerRepository);
            customerUisCollection = new(customerManager.GetCustomers(null).Select(x => new CustomerUI(
                x.Id,
                x.Name,
                x.Contact.Email,
                x.Contact.Address.ToString(),
                x.Contact.Phone,
                x.GetMembers().Select(m => new MemberUI(m.Name, m.Birthday.ToString())).ToList()))
                .ToList());

            CustomerDataGrid.ItemsSource = customerUisCollection;

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customerUisCollection = new(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone,x.GetMembers().Select(m => new MemberUI(m.Name, m.Birthday.ToString())).ToList())).ToList());
            CustomerDataGrid.ItemsSource = customerUisCollection;
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem is null)
            {
                MessageBox.Show("No one was selected to delete.", "delete customer");
            }
            else
            {

                try
                {
                    CustomerUI cui = (CustomerUI)CustomerDataGrid.SelectedItem;
                    int customerID = cui.Id;


                    customerUisCollection.Remove(cui);
                    customerManager.RemoveCustomer(customerID); //set customer inactive (status 0)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Something went wrong");
                }

            }
        }

        private void MenuItem_Click_Update(object sender, RoutedEventArgs e)
        {

            if (CustomerDataGrid.SelectedItem is null)
            {
                MessageBox.Show("No one was selected to update.", "update customer");
            }
            else
            {
                CustomerWindow w = new((CustomerUI)CustomerDataGrid.SelectedItem, customerManager);
                w.AddButton.Content = "Confirm Update";

                if (w.ShowDialog() == true)
                {


                    CustomerUI cui = w.CustomerUI;
                    CustomerUI item = customerUisCollection.FirstOrDefault(c => c.Id == cui.Id);

                    int index = customerUisCollection.IndexOf(item);
                    customerUisCollection[index] = item;


                }
            }
        }

        private void MenuItem_Click_Add(object sender, RoutedEventArgs e)
        {
            

            CustomerWindow w = new(null, customerManager);
            w.AddButton.Content = "Add customer";
            if (w.ShowDialog() == true)
            {
                CustomerUI cui = w.CustomerUI;
                customerUisCollection.Add(cui);
            }
        }



    }
}
