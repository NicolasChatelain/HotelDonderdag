using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Model;
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
using System.Windows.Shapes;

namespace Hotel.Presentation.Customer___Members
{
    /// <summary>
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        internal ObservableCollection<MemberUI> MembersCollection; // collection that shows all members in the current customer of this window

        public MemberWindow(ObservableCollection<MemberUI> members) // inject the member collection from previous window and set equal to membercollection property and grid source
        {
            InitializeComponent();
            MembersCollection = members;

            MembersGrid.ItemsSource = MembersCollection;
        }

        private void AddMember_Click(object sender, RoutedEventArgs e) // makes a Customer and CustomerUI, validates via Customer in domainlayer
        {
            try
            {

                Member m = new(namebox.Text, DateOnly.Parse(birthdaybox.Text));
                MemberUI mui = new(m.Name, m.Birthday.ToString());

                if (MembersCollection.Contains(mui))
                {
                    throw new Exception("A member can not be added more than once.");
                }
                MembersCollection.Add(mui);

            }
            catch (FormatException)
            {
                MessageBox.Show("This date is not valid.", "Something went wrong.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong.");
            }


        }


        private void SaveMembers_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Birthdaybox_TextChanged(object sender, TextChangedEventArgs e) // method to show and hide the placeholder date format in the textbox
        {
            if (birthdaybox.Text != string.Empty)
            {
                dateformatlabel.Visibility = Visibility.Hidden;
            }
            else
            {
                dateformatlabel.Visibility = Visibility.Visible;
            }
        }

        private void UpdateConfirmation_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
