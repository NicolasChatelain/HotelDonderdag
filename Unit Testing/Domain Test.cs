using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;

namespace Unit_Testing
{
    public class DomainTest
    {
        private readonly Activity activity;
        private readonly  List<Member> members;

        public DomainTest()
        {
            activity = new()
            {
                Id = 1,
                Capacity = 5,
                Description = new(),
                PriceInfo = new(),
                Fixture = new()
            };

            members = new()
            {
                new Member(1, "Elara Stormrider", new DateOnly(1990, 1, 15), new Customer(1)),
                new Member(2, "Nathaniel Evergreen", new DateOnly(1985, 3, 22), new Customer(1)),
                new Member(3, "Seraphina Midnightbane", new DateOnly(1993, 11, 5), new Customer(1)),
            };

        }


        [Fact]
        public void RegistrationSubscribe_SusbcribersListIsNull_InitListAndFillList()
        {

            #region Arrange 

            Registration registration = new()
            {
                Id = 1,
                Activity = activity
            };

            #endregion

            #region Act 

            registration.Subscribe(members);

            #endregion

            #region Assert

            Assert.NotNull(registration.Subscribers);
            Assert.Equal(registration.Subscribers, members);

            #endregion

        }

        [Fact]
        public void CheckCapacity_CapacityExceeds_ThrowsException()
        {

            #region Arrange 
            activity.Capacity = 2;

            Registration registration = new()
            {
                Id = 1,
                Activity = activity
            };

            #endregion

            #region Act & Assert

            Assert.Throws<RegistrationException>(() => registration.Subscribe(members));

            #endregion

        }

        [Fact]
        public void UpdateSubscribers_UpdatesListCorrectly_RemovesOldAddsNewMembers()
        {

            #region Arrange 

            Registration registration = new()
            {
                Id = 1,
                Activity = activity
            };

            registration.Subscribe(members);

            List<Member> newsubscribers = new()
            {
                new Member(1, "Elara Stormrider", new DateOnly(1990, 1, 15), new Customer(1)),
                new Member(2, "Nathaniel Evergreen", new DateOnly(1985, 3, 22), new Customer(1)),
                new Member(3, "Legolas Greenleaf", new DateOnly(1800, 04, 8), new Customer(1)),
            };

            #endregion

            #region Act 

            registration.UpdateSubscribers(newsubscribers, 1);

            #endregion

            #region Assert

            Assert.Equal(registration.Subscribers, newsubscribers);

            #endregion

        }

        [Fact]
        public void SetAdultPrice_ValidString_Succeeds()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "10";
            int expected = 10;

            #endregion

            #region Act 

            priceInfo.SetAdultPrice(price);

            #endregion

            #region Assert

            Assert.Equal(expected, priceInfo.AdultPrice);

            #endregion
        }

        [Fact]
        public void SetAdultPrice_InvalidString_ThrowsPriceInfoException()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "ten";

            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetAdultPrice(price));

            #endregion
        }

        [Fact]
        public void SetAdultPrice_ValidStringButLessthan0_ThrowsPriceInfoException()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "-1";

            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetAdultPrice(price));

            #endregion
        }

        [Fact]
        public void SetChildPrice_ValidString_Succeeds()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "10";
            int expected = 10;

            #endregion

            #region Act 

            priceInfo.SetKidsPrice(price);

            #endregion

            #region Assert

            Assert.Equal(expected, priceInfo.ChildPrice);

            #endregion
        }

        [Fact]
        public void SetChildPrice_InvalidString_ThrowsPriceInfoException()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "ten";

            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetKidsPrice(price));

            #endregion
        }

        [Fact]
        public void SetChildPrice_ValidStringButLessthan0_ThrowsPriceInfoException()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "-1";

            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetKidsPrice(price));

            #endregion
        }

        [Fact]
        public void SetDiscount_ValidString_Succeeds()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "50";
            int expected = 50;

            #endregion

            #region Act 

            priceInfo.SetDiscount(price);

            #endregion

            #region Assert

            Assert.Equal(expected, priceInfo.DiscountPercentage);

            #endregion
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("101")]
        public void SetDiscount_ValueOutsideBoundary_Succeeds(string discountValue)
        {

            #region Arrange 
            PriceInfo priceInfo = new();
            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetDiscount(discountValue));
            #endregion
        }

        [Fact]
        public void SetDiscount_InvalidString_ThrowsPriceInfoException()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "ten";

            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetDiscount(price));

            #endregion
        }

        [Fact]
        public void SetAdultAge_ValidString_Succeeds()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "10";
            int expected = 10;

            #endregion

            #region Act 

            priceInfo.SetAdultAge(price);

            #endregion

            #region Assert

            Assert.Equal(expected, priceInfo.AdultAge);

            #endregion
        }

        [Fact]
        public void SetAdultAge_InvalidString_ThrowsPriceInfoException()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "ten";

            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetAdultAge(price));

            #endregion
        }

        [Fact]
        public void SetAdultAge_ValidStringButLessThan10_ThrowsPriceInfoException()
        {
            #region Arrange 

            PriceInfo priceInfo = new();
            string price = "9";

            #endregion

            #region Act & Assert

            Assert.Throws<PriceInfoException>(() => priceInfo.SetAdultAge(price));

            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SetPropertiesAddress_ValueNullOrEmpty_ThrowsAddressException(string? value)
        {

            #region Arrange 
            Address address = new();
            #endregion

            #region Act & Assert

            Assert.Throws<AddressException>(() => address.City = value);
            Assert.Throws<AddressException>(() => address.Street = value);
            Assert.Throws<AddressException>(() => address.HouseNumber = value);
            Assert.Throws<AddressException>(() => address.PostalCode = value);

            #endregion

        }

        [Fact]
        public void SetPropertiesAddress_ValidInfo_Succeeds()
        {

            #region Arrange 

            string City = "Geraardsbergen";
            string Zip = "9500";
            string Housenumber = "61";
            string Street = "Hogeweg";

            #endregion

            #region Act 

            Address address = new(City, Street, Zip, Housenumber);

            #endregion

            #region Assert

            Assert.Equal(City, address.City);
            Assert.Equal(Street, address.Street);
            Assert.Equal(Housenumber, address.HouseNumber);
            Assert.Equal(Zip, address.PostalCode);

            #endregion

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("@@")]
        [InlineData("chatelain.gmail.com")]
        public void SetEmail_InvalidInfo_ThrowsContactInfoException(string value)
        {

            #region Arrange

            ContactInfo contactInfo;

            #endregion

            #region Act & Assert

            Assert.Throws<ContactInfoException>(() => contactInfo = new(value, "123465789", new()));

            #endregion

        }

        [Theory]
        [InlineData(null)]   
        [InlineData("")]    
        [InlineData("   ")] 
        [InlineData("abc")] 
        public void SetPhone_InvalidInfo_ThrowsContactInfoException(string value)
        {
            #region Arrange

            ContactInfo contactInfo;

            #endregion

            #region Act & Assert

            Assert.Throws<ContactInfoException>(() => contactInfo = new("chatelain777@gmail.com", value, new()));

            #endregion
        }

        [Theory]
        [InlineData("123456789")]
        [InlineData(" 1 2 3 4 5 6 7 8 9 ")]
        [InlineData("+1 23 456789 ")]
        public void SetPhone_ValidInfo_Succeeds(string value)
        {
            #region Arrange

            string expected = "123456789";

            #endregion

            #region Act 

            ContactInfo contactInfo = new("abc123@gmail.com", value, new());

            #endregion

            #region Assert

            Assert.Equal(expected, contactInfo.Phone);

            #endregion

        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void SetCustomerName_InvalidInfo_ThrowsCustomerException(string value)
        {

            #region Arrange 

            Customer customer = new(1);

            #endregion

            #region Act & Assert

            Assert.Throws<CustomerException>(() => customer.Name = value);

            #endregion

        }

    }
}