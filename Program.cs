using CartService.DTO;
using CartService.DTO.Clothing;
using CartService.DTO.Discount;
using System;

namespace CartService
{
    class Program
    {
        static void Main(string[] args)
        {
            // Created list of product
            ProductBase tShirt1 = new ClothingProduct() { Category = DTO.Category.Clothing, Code = "T100", Name = "TShirt1", Size = ClothSize.M, ProductPrice = new DTO.ProductPrice() { Price = 500, UnitName = "Per" } };
            ProductBase tShirt2 = new ClothingProduct() { Category = DTO.Category.Clothing, Code = "T100", Name = "TShirt2", Size = ClothSize.M, ProductPrice = new DTO.ProductPrice() { Price = 400, UnitName = "Per" } };
            ProductBase tShirt3 = new ClothingProduct() { Category = DTO.Category.Clothing, Code = "T100", Name = "TShirt2", Size = ClothSize.M, ProductPrice = new DTO.ProductPrice() { Price = 350, UnitName = "Per" } };

            ProductBase jeans1 = new ClothingProduct() { Category = DTO.Category.Clothing, Code = "T110", Name = "Jeans1", Size = ClothSize.M, ProductPrice = new DTO.ProductPrice() { Price = 1500, UnitName = "Per" } };
            ProductBase jeans2 = new ClothingProduct() { Category = DTO.Category.Clothing, Code = "T110", Name = "Jeans2", Size = ClothSize.M, ProductPrice = new DTO.ProductPrice() { Price = 1449, UnitName = "Per" } };


            ProductBase trouser = new ClothingProduct() { Category = DTO.Category.Clothing, Code = "T120", Name = "trouser", Size = ClothSize.M, ProductPrice = new DTO.ProductPrice() { Price = 1000, UnitName = "Per" } };


            // Create Discount Promo code
            DiscountPromo discountPromo = new FlatPercentageDoscount(10) { Code = "10Percent", Name = "Plat 10% discount", ExpireOn = DateTime.UtcNow.AddDays(30) };
            DiscountPromo buy2Get10Percent = new Buy2And10PercentDiscount(10) { Code = "Buy2Get10Percent", Name = "Buy 2 Get Plat 10% discount", ExpireOn = DateTime.UtcNow.AddDays(30) };
            DiscountPromo buy3Get1Free = new BuyNGetXFreeDiscount(3, 1, true) { Code = "Buy3Get1Free", Name = "Buy 3 Get 1 Free", ExpireOn = DateTime.UtcNow.AddDays(30) };
            DiscountPromo customDiscount = new CustomDiscount((products) => { return decimal.Zero; }) { Code = "Custom", Name = "Custom", ExpireOn = DateTime.UtcNow.AddDays(30) };


            DiscountService discountService = new DiscountService();
            discountService.AddDiscount(discountPromo);
            discountService.AddDiscount(buy2Get10Percent);
            discountService.AddDiscount(buy3Get1Free); // Requested in assignment
            discountService.AddDiscount(customDiscount);


            // Add product for discount
            discountService.AddDiscountedProduct("10Percent", "T120");
            discountService.AddDiscountedProduct("Buy2Get10Percent", "T110");
            discountService.AddDiscountedProduct("Buy3Get1Free", "T100");


            CartService cartService = new CartService(discountService);
            //cartService.AddItem(trouser);
            //cartService.AddItem(jeans1);
            //cartService.AddItem(jeans1);
            cartService.AddItem(tShirt1);
            cartService.AddItem(tShirt2);
            cartService.AddItem(tShirt3);


            var price = cartService.GetTotalPrice();


        }
    }
}
