namespace ZAD_Management.Domain.ValueObjects;

public class RentalPricing
{
    public decimal RentPrice { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal NetRentPrice { get; private set; }

    private RentalPricing() { }

    public RentalPricing(decimal rentPrice, decimal discountPercent = 0, decimal discountAmount = 0)
    {
        if (rentPrice < 0)
            throw new ArgumentException("Rent price cannot be negative.");

        RentPrice = rentPrice;

        if (discountPercent > 0)
        {
            DiscountPercent = discountPercent;
            DiscountAmount = Math.Round(rentPrice * (discountPercent / 100m), 2);
        }
        else if (discountAmount > 0)
        {
            DiscountAmount = discountAmount;
            DiscountPercent = rentPrice > 0 ? Math.Round((discountAmount / rentPrice) * 100m, 2) : 0;
        }
        else
        {
            DiscountPercent = 0;
            DiscountAmount = 0;
        }

        if (DiscountAmount > RentPrice)
            throw new ArgumentException("Discount amount cannot exceed rent price.");

        NetRentPrice = RentPrice - DiscountAmount;
    }
}

