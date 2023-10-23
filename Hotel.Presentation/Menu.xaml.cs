using Hotel.Presentation.Organizations___Activities;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private MainWindow? _mainWindow;
        private OrganizationWindow? _organizationWindow;

        public Menu()
        {
            InitializeComponent();
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow = new();
            this.Hide();
            _mainWindow.ShowDialog();
            this.Show();
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            _organizationWindow = new();
            this.Hide();
            _organizationWindow.ShowDialog();
            this.Show();

        }
    }
}
