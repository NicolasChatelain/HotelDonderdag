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

        private readonly Dictionary<int, (string, string)> _validLogins;
        internal event Action<int> LoginSucces;

        public LoginScreen(Dictionary<int, (string, string)> ValidLogins)
        {
            InitializeComponent();
            _validLogins = ValidLogins;
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e) // Combination of name and input need to be correct to login.
        {
            string nameInput = NameInputBox.Text.ToLower().Trim();
            string phoneInput = PhoneInputBox.Text;

            var LoginMatch = _validLogins.FirstOrDefault(login => login.Value.Item1.ToLower() == nameInput && login.Value.Item2 == phoneInput);

            if (LoginMatch.Value != (null, null))
            {
                int id = LoginMatch.Key;
                OnLoginSucces(id);
            }
            else
            {
                MessageBox.Show("Invalid name or phonenumber.", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        protected virtual void OnLoginSucces(int id)
        {
            LoginSucces?.Invoke(id);
        }
    }
}
