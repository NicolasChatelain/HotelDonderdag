using System;
using System.Windows;


namespace Hotel.Presentation.Windows.Organizations___Activities.pages
{
    /// <summary>
    /// Interaction logic for ActivityUpdateWindow.xaml
    /// </summary>
    public partial class ActivityUpdateWindow : Window
    {
        public event Action<DateTime> UpdatedFixtureConfirmed;
        public ActivityUpdateWindow(DateTime fixture)
        {
            InitializeComponent();

            CurrentDate.Content = $"Current fixture: {fixture.ToString()}";
        }

        private void ConfirmFixtureUpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FixtureAsText = FixtureTextBox.Text;
                DateTime updatedFixture = DateTime.Parse(FixtureAsText);

                if (updatedFixture < DateTime.Now.AddDays(1))
                {
                    throw new Exception("an activity can only be planned in the future and atleast 1 day in advance.");
                }

                UpdatedFixtureConfirmed?.Invoke(updatedFixture);

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
