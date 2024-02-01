using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Net;

namespace HtmlToPdfDockerPOC
{
    public class Quote
    {
          List<QuoteItem> _items = new List<QuoteItem>();

          List<ShippingOption> _shippingOptions = new();


        public int SerialNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public QuoteStatus Status { get;  set; }

        public string ZipCode { get;  set; }

        public Project Project { get;  set; }

        public DateTime ExpireDate { get;  set; }

        public Warehouse FromWarehouse { get;  set; }

        public Customer Customer { get;  set; }

        public Customer ShippingCustomer { get;  set; }

        public Address ShippingAddress { get;  set; }

        public DateTime? ShippingDate { get;  set; }

        public Guid? AppliedShippingOptionId { get;  set; }

        public decimal Shipping { get;  set; }

        public string ShippingNotes { get;  set; }

        #region Price
        public decimal ItemsTotal { get;  set; }

        public decimal DiscountValue { get;  set; }

        public decimal SubTotal { get;  set; }

        public decimal Tax { get;  set; }

        public decimal Total { get;  set; }
        #endregion

        public List<QuoteItem> Items { get; set; }


        public List<ShippingOption> ShippingOptions { get; set; }


    }


    public class ShippingOption
    {
        public decimal ActualCharge { get;  set; }

        public decimal OriginalCharge { get;  set; }

        public string ShippingBy { get;  set; }

        public string Type { get;  set; }

        public DateTime QuoteDate { get;  set; } = DateTime.Now;

        public int NumberOfBusinessDays { get;  set; }

        public DateTime EstimatedDeliveryDate { get;  set; }

        public DateTime? EstimatedShippingDate { get;  set; }

        public bool IsFree { get;   set; }

        public bool Guaranteed5PM { get;   set; }

        public bool Guaranteed2PM { get;   set; }

        public int? OriginalTrucksCount { get;   set; }

        public int? ActualTrucksCount { get;   set; }

        public FreightCostOption FreightCostOption { get;   set; } = FreightCostOption.Separated;

        public string Title => $"{NumberOfBusinessDays} Business Day(s)";

        [NotMapped]

        public int? PortalId { get; }
    }
    public class QuoteItem
    {
        #region Product
        public Product Product { get;   set; }

        public IEnumerable<SubProductItem> SubProductItems { get;   set; }

        public IReadOnlyCollection<AttributeValue> AttributesValues { get;   set; }

        public int RowNumber { get; set; }

        public string Notes { get;   set; }

        #endregion


        #region Quantity
        public int Quantity { get;   set; }

        public ProductPackage ProductPackage { get;   set; }

        public ProductPackage DefaultProductPackage { get;   set; }

        public decimal? Value { get;   set; }

        public FeetUnitValue ValueInFeet { get;   set; }

        public bool IsBrokenBox => ProductPackage is null && Value is null && ValueInFeet is null;
        #endregion


        #region Price
        public decimal UnitPrice { get;   set; }

        public decimal? BrokenBoxFees { get;   set; }

        public bool IsBrokenBoxFeesApplied { get;   set; } = false;

        public decimal Discount { get;   set; }

        public DiscountType DiscountType { get;   set; }

        public decimal ExtraFees { get;   set; }

        public decimal DiscountValue => DiscountType == DiscountType.Value ? Discount : SubTotal * Discount / 100;

        public decimal SubTotal => CalculateSubTotal();

        public decimal Total => SubTotal - DiscountValue;


        #endregion

          decimal CalculateSubTotal()
        {
            if (SubProductItems?.Any() == true)
            {
                return Quantity * UnitPrice + ExtraFees;
            }
            if (BrokenBoxFees.HasValue)
            {
                var subTotal = Quantity * UnitPrice;
                if (IsBrokenBoxFeesApplied)
                {
                    subTotal += BrokenBoxFees.GetValueOrDefault();
                }
                return subTotal;
            }
            if (ProductPackage != null)
            {
                return Quantity * ProductPackage.TotalUnitsQuantity * UnitPrice;
            }
            if (Value.HasValue)
            {
                return Quantity * UnitPrice * (decimal)Value;
            }
            return Quantity * UnitPrice;
        }
    }

    public class Project
    {


        public string Name { get;   set; }

        public decimal Limit { get;   set; }

        public string Address { get;   set; }

        public string City { get;   set; }

        public string State { get;   set; }

        public DateTime FramingDate { get;   set; }

    }

    public enum DiscountType
    {
        Percentage = 1,
        Value
    }

    public class Address
    {
        public static Address Empty => new Address();

        public string Country { get;   set; }

        public string State { get;   set; }

        public string City { get;   set; }

        public string ZipCode { get;   set; }

        public string Address1 { get;   set; }

        public string Address2 { get;   set; }

        [DisplayName("Phone")]
        public string PhoneNumber { get;   set; }

        public string Fax { get;   set; }
    }
    public class Warehouse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public Address Address { get; set; }

        public static explicit operator string(Warehouse v)
        {
            return "Warehouse";
        }
    }

    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Guid CompanyId { get; set; }

        public string CompanyName { get; set; }

        public Guid CompanyTypeId { get; set; }

        public string CompanyTypeName { get; set; }

        public static explicit operator string(Customer v)
        {
            return "Customer";
        }
    }

    public enum QuoteStatus
    {
        Forecast = 1,
        Pending,
        Expired,
        Draft
    }

    public enum FreightCostOption
    {

        Integrated = 1,
        Separated,
        NoFreight
    }

    public class Product
    {
        public Guid Id { get;   set; }

        public int? TsnNumber { get; set; }

        public string Name { get;   set; }

        public int Number { get;   set; }

        public string Nature { get;   set; }

        public Guid MeasurmentUnitId { get;   set; }

        public string MeasurmentUnit { get;   set; }

        public Measurement Measurement { get;   set; }

        public bool ERPFreeShipping { get;   set; }

        public bool StoreFreeShipping { get;   set; }

        public bool ERPFullContainer { get;   set; }

        public bool StoreFullContainer { get;   set; }

        public decimal LengthInInches { get;   set; }

        public decimal WidthInInches { get;   set; }

        public decimal HeightInInches { get;   set; }

        public decimal WeightInLbs { get;   set; }

        public decimal Size { get;   set; }

        public PalletType? FitInPallet { get; set; }

        public ProductType Type { get; set; }
    }
    public enum PalletType
    {
        Large = 1,
        Small
    }
    public enum Measurement
    {
        Length = 1,
        Area,
        Volume,
        Mass,
        Quantity,
        DigitalStorage
    }

    public enum ProductType
    {
        RawMaterial = 1,
        FinishedGood,
        SpecialProduct
    }

    public class SubProductItem
    {
        public Guid Id { get;   set; }

        public string Name { get;   set; }

        public int Number { get;   set; }

        public Guid MeasurmentUnitId { get;   set; }

        public string MeasurmentUnit { get;   set; }

        public Measurement Measurement { get;   set; }

        public decimal LengthInInches { get;   set; }

        public decimal WidthInInches { get;   set; }

        public decimal HeightInInches { get;   set; }

        public decimal WeightInLbs { get;   set; }

        public decimal Size { get;   set; }

        public decimal UnitPrice { get;   set; }

        public int Quantity { get;   set; }

        public decimal? Value { get;   set; }

        public FeetUnitValue ValueInFeet { get;   set; }

        public decimal TotalPrice => Quantity * UnitPrice * (Value ?? 1);

    }
    public class FeetUnitValue
    {
        public int Feet { get; set; }

        public int Inches { get; set; }

        public int UpperFraction { get; set; }

        public int LowerFraction { get; set; }

        public decimal GetTotalValueInFeets()
        {
            return GetTotalValueIInInches() / 12;
        }

        public decimal GetTotalValueIInInches()
        {
            var fraction = (decimal)UpperFraction / LowerFraction;
            var totalLengthInInches = (Feet * 12) + Inches + fraction;
            return totalLengthInInches;
        }

        public string GetFormatedString()
        {
            var fraction = (decimal)UpperFraction / LowerFraction;
            var totalLengthInInches = Inches + fraction;
            return $"{Feet} ft {totalLengthInInches} in";
        }

        public string GetFormatedValue()
        {
            var formatedValue = "";
            var singleQuote = "'";
            var doubleQuote = '"';

            if (Feet > 0)
            {
                formatedValue += $"{Feet}{singleQuote}";
            }

            if (UpperFraction > 0 && LowerFraction > 0)
            {
                var fractionFormated = $" {UpperFraction}/{LowerFraction}{doubleQuote}";
                if (Inches > 0)
                {

                    formatedValue += $" {Inches} {fractionFormated}";
                }
                else
                {
                    formatedValue += fractionFormated;
                }
            }
            else if (Inches > 0)
            {
                formatedValue += $"{Inches}{doubleQuote}";
            }

            return formatedValue;
        }
    }

    public class AttributeValue
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }

    public class AttributeValuesEqualityComparer : IEqualityComparer<AttributeValue>
    {
        public bool Equals(AttributeValue x, AttributeValue y)
        {
            return x.Id == y.Id && x.Value == y.Value;
        }

        public int GetHashCode([DisallowNull] AttributeValue obj)
        {
            return obj.GetHashCode();
        }
    }

    public class ProductPackage
    {
        public Guid Id { get;   set; }

        public string Type { get;   set; }

        public string Name { get;   set; }

        public decimal WeightInLbs { get;   set; }

        public decimal Quantity { get;   set; }

        public decimal TotalUnitsQuantity { get;   set; }

        public decimal PackageSize { get;   set; }

        public decimal PackageLengthInInches { get;   set; }

        public decimal PackageWidthInInches { get;   set; }

        public decimal PackageHeightInInches { get;   set; }
    }

}