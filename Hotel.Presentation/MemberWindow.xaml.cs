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
        internal ObservableCollection<MemberUI> MembersCollection;

        public MemberWindow(ObservableCollection<MemberUI> members)
        {
            InitializeComponent();
            MembersCollection = new(members);


            MembersGrid.ItemsSource = MembersCollection;
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Member m = new(namebox.Text, DateOnly.Parse(birthdaybox.Text));
                
                MemberUI mui = new(namebox.Text, birthdaybox.Text);
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

        private void ConfirmAddedMembers_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Birthdaybox_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
