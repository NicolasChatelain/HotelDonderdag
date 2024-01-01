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
        private readonly Dictionary<int, (string,string)> _phonenumbersAndNames;
        private readonly LoginScreen _loginScreen;
        private ActivityRegistrationScreen _activityRegistrationScreen;

        public CustomerLoginWindow()
        {
            InitializeComponent();
            try
            {
                _manager = new(RepositoryFactory.RegistrationRepository);
                _phonenumbersAndNames = _manager.GetValidLoginPhones(); // (id, (name, phonenumber))

                _loginScreen = new(_phonenumbersAndNames);

                MainContentControl.Content = _loginScreen;
                _loginScreen.LoginSucces += LoginScreenSucces;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginScreenSucces(int id)
        {
            _activityRegistrationScreen = new(_manager, id);
            MainContentControl.Content = _activityRegistrationScreen;
        }
    }
}
