using System;
using System.Windows;


namespace Hotel.Presentation.Windows.Organizations___Activities.pages
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DiscountWindow : Window
    {
        internal event Action<double> DiscountConfirmed;
        public DiscountWindow()
        {
            InitializeComponent();
        }

        private void ConfirmDiscount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string DiscountAsText = DiscountTextBox.Text.Trim();
                double Discount = Convert.ToDouble(DiscountAsText);

                DiscountConfirmed?.Invoke(Discount);

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
