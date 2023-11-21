using Hotel.Presentation.Model;
using System.Windows;
using System.Windows.Controls;



namespace Hotel.Presentation.Windows.Registrations.pages
{
    /// <summary>
    /// Interaction logic for ActivityRegistrationScreen.xaml
    /// </summary>
    public partial class ActivityRegistrationScreen : UserControl
    {
        public ActivityRegistrationScreen()
        {
            InitializeComponent();
        }

        private void SubscribeBTN_Click(object sender, RoutedEventArgs e)
        {
            MemberUI member = (MemberUI)MemberListBox.SelectedItem;

            if (member is not null && !SubscribedMembersBox.Items.Contains(member))
            {
                SubscribedMembersBox.Items.Add(member);
            }
        }

        private void UnsubscribeBTN_Click(object sender, RoutedEventArgs e)
        {
            MemberUI member = (MemberUI)SubscribedMembersBox.SelectedItem;

            if (member is not null)
            {
                SubscribedMembersBox.Items.Remove(member);
            }
        }
    }
}
