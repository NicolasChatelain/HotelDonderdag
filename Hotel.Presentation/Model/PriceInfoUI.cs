using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    public class PriceInfoUI
    {

        public PriceInfoUI(int ID, int adultPrice, int childPrice, int discountPercentage, int adultAge)
        {
            this.ID = ID;
            Adultprice = adultPrice;
            Kidsprice = childPrice;
            Discount = discountPercentage;
            Adultage = adultAge;
        }

        public int ID { get; set; }
        public int Adultprice { get; set; }
        public int Kidsprice { get; set; }
        public int Discount { get; set; }
        public int Adultage { get; set; }

        public override string ToString()
        {
            return $"adults: €{Adultprice} / kids: €{Kidsprice} / Discount: {Discount}% / Adult-Age: {Adultage}.";
        }
    }
}
