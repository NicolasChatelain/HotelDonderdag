using Hotel.Presentation.Model;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using Hotel.Domain.Managers;
using System.Linq;
using Hotel.Presentation.Mapper;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using Hotel.Domain.Model;
using System.ComponentModel;

namespace Hotel.Presentation.Windows.Registrations.pages
{
    /// <summary>
    /// Interaction logic for ActivityRegistrationScreen.xaml
    /// </summary>
    public partial class ActivityRegistrationScreen : UserControl
    {
        private readonly RegistrationsManager _registrationsManager;
        private readonly ObservableCollection<MemberUI> SubscribedMembers;
        private ActivityUI selectedActivity;
        private readonly int customerId;
        private decimal totalPrice = 0;
        internal event Action OnLogoutPress;
        public event PropertyChangedEventHandler? PropertyChanged;


        public ActivityRegistrationScreen(RegistrationsManager manager, int customerid)
        {
            InitializeComponent();

            _registrationsManager = manager;
            customerId = customerid;

            try
            {
                List<MemberUI> members = manager.GetMembersForCustomer(customerid)
                                                 .Select(x => new MemberUI(x.ID, x.Name, x.Birthday.ToString()))
                                                 .ToList();
                SubscribedMembers = new();

                PriceLabel.Content = $"Total price: €{totalPrice}";

                MemberListBox.ItemsSource = members;
                SubscribedMembersBox.ItemsSource = SubscribedMembers;

                List<ActivityUI> activities = manager.GetAllActivities().Select(MapActivity.FromDomainToUI).ToList();


                ActivityBox.ItemsSource = activities;
                SubscribedMembers.CollectionChanged += SubscriptionChanged; // deze methode wordt gecalled als er iets aan de collectie wordt veranderd (add/remove)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            OnLogoutPress?.Invoke();
        }

        private void CalculateTotalPrice()
        {
            totalPrice = 0;
            foreach (MemberUI member in SubscribedMembers)
            {
                DateTime memberBirthday = DateTime.Parse(member.Birthday);
                int age = DateTime.Now.Year - memberBirthday.Year;

                if (age < selectedActivity.AdultAge)
                {
                    totalPrice += selectedActivity.ChildPrice;
                }
                else
                {
                    totalPrice += selectedActivity.AdultPrice;
                }
            }
            ShowPriceAndApplyDiscount();
        }

        private void SubtractRemovedPrice(IEnumerable removedMembers)
        {
            foreach (MemberUI removedMember in removedMembers!)
            {
                DateTime removedMemberBirthday = DateTime.Parse(removedMember.Birthday);
                int age = DateTime.Now.Year - removedMemberBirthday.Year;

                if (age < selectedActivity.AdultAge)
                {
                    totalPrice -= selectedActivity.ChildPrice;
                }
                else
                {
                    totalPrice -= selectedActivity.AdultPrice;
                }
            }

            ShowPriceAndApplyDiscount();
        }

        private void ShowPriceAndApplyDiscount()
        {
            decimal Discount = (totalPrice * selectedActivity.DiscountPercentage) / 100;
            decimal priceAfterDiscount = totalPrice - Discount;

            PriceLabel.Content = $"Total price: €{priceAfterDiscount}";
        }

        private void SubscriptionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                CalculateTotalPrice();
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                SubtractRemovedPrice(e.OldItems!);
            }
        }

        private void SubscribeBTN_Click(object sender, RoutedEventArgs e)
        {
            MemberUI member = (MemberUI)MemberListBox.SelectedItem;

            if (member is not null && !SubscribedMembers.Contains(member))
            {
                SubscribedMembers.Add(member);
            }
        }

        private void UnsubscribeBTN_Click(object sender, RoutedEventArgs e)
        {
            MemberUI member = (MemberUI)SubscribedMembersBox.SelectedItem;

            if (member is not null)
            {
                SubscribedMembers.Remove(member);
            }
        }

        private void ActivityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActivityBox.SelectedItem is not null)
            {
                SubscribeBTN.IsEnabled = true;
                UnsubscribeBTN.IsEnabled = true;

                selectedActivity = (ActivityUI)ActivityBox.SelectedItem;
                ActivityDetailsBlock.Text = selectedActivity.ShowDetails();

                DiscountLabel.Content = $"Discount: {selectedActivity.DiscountPercentage}%";

                GetSubscribedMembers();
            }
        }

        private void GetSubscribedMembers()
        {
            List<MemberUI> subscribedMembers = _registrationsManager.GetSubscribedMembersForAcitivity(selectedActivity.Id, customerId)
                                                                        .Select(x => new MemberUI(x.ID, x.Name, x.Birthday.ToString()))
                                                                        .ToList();
            SubscribedMembers.Clear();
            CalculateTotalPrice();

            foreach (MemberUI member in subscribedMembers)
            {
                SubscribedMembers.Add(member);
            }
        }

        private void ConfirmRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SubscribedMembersBox.Items.Count > 0)
                {
                    bool succes = _registrationsManager.MakeRegistration(SubscribedMembers.Select(x => new Member(x.ID, x.Name, DateOnly.Parse(x.Birthday), new Customer(customerId))).ToList(), MapActivity.ToDomain(selectedActivity), customerId);
                    if (succes)
                    {
                        MessageBox.Show("Subscribed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MemberUI M = SubscribedMembers.Last();
                SubscribedMembers.Remove(M);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerLoginWindow clw = new();
            clw.Show();
        }
    }
}
