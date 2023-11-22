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

namespace Hotel.Presentation.Windows.Registrations.pages
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {

        private readonly List<(string, string)> _validLogins;
        internal event Action<string, string> LoginSucces;

        public LoginScreen(List<(string, string)> ValidLogins)
        {
            InitializeComponent();
            _validLogins = ValidLogins;
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e) // Combination of name and input need to be correct to login.
        {
            string nameInput = NameInputBox.Text.ToLower().Trim();
            string phoneInput = PhoneInputBox.Text;

            if (_validLogins.Any(login => login.Item1.ToLower() == nameInput && login.Item2 == phoneInput))
            {
                OnLoginSucces(nameInput, phoneInput);
            }
            else
            {
                MessageBox.Show("This phone number does not belong to a customer.", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        protected virtual void OnLoginSucces(string name, string phone)
        {
            LoginSucces?.Invoke(name, phone);
        }
    }
}
