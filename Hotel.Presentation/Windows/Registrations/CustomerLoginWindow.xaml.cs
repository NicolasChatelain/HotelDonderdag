using Hotel.Domain.Managers;
using Hotel.Presentation.Model;
using Hotel.Presentation.Windows.Registrations.pages;
using Hotel.Util;
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

namespace Hotel.Presentation.Windows.Registrations
{
    /// <summary>
    /// Interaction logic for CustomerLoginWindow.xaml
    /// </summary>
    public partial class CustomerLoginWindow : Window
    {
        private readonly RegistrationsManager _manager;
        private List<(string, string)> _validCustomerLogins = new();
        private Dictionary<int, (string,string)> _phonenumbers;
        private readonly LoginScreen _loginScreen;
        private ActivityRegistrationScreen _activityRegistrationScreen;

        public CustomerLoginWindow()
        {
            InitializeComponent();
            try
            {
                _manager = new(RepositoryFactory.RegistrationRepository);
                _phonenumbers = _manager.GetValidLoginPhones();
                _validCustomerLogins.AddRange(_phonenumbers.Values);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _loginScreen = new(_validCustomerLogins);

            MainContentControl.Content = _loginScreen;
            _loginScreen.LoginSucces += LoginScreenSucces;


        }

        private void LoginScreenSucces(string name, string phone)
        {
            _activityRegistrationScreen = new();
            MainContentControl.Content = _activityRegistrationScreen;

            int CustomerId = _phonenumbers.FirstOrDefault(x => x.Value.Item2 == phone).Key; // find the customerid based on login

            try
            {
                List<MemberUI> members = _manager.GetMembersForCustomer(CustomerId)
                                                 .Select(x => new MemberUI(x.Name, x.Birthday.ToString()))
                                                 .ToList();

                

                _activityRegistrationScreen.MemberListBox.ItemsSource = members;
                _activityRegistrationScreen.ActivityBox.ItemsSource = members;
                _activityRegistrationScreen.CustomerLabel.Content += name;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
