using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Data;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using WajedApi.ViewModels;
using WajedApi.Helpers;
using WajedApi.Dtos;

namespace WajedApi.Serveries.CartsService
{
    public class CartsService : ICartsService
    {

        private readonly IMapper _mapper;


        private readonly AppDBcontext _context;

        public CartsService(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }





        public int checkCart(int restaurantId, string userId, int productId)
        {
            List<Cart> carts = _context.Carts!.Where(t => t.UserId == userId).ToList();
            Product? product = _context.Products!.FirstOrDefault(x => x.Id == productId);
            Cart? isCart = _context.Carts!.FirstOrDefault(t => t.ProductId == productId && t.UserId == userId);
            if (isCart != null && carts.First().restaurantId == restaurantId)
            {
                return 0; //updated
            }
            else if (carts.Count()>0 && carts.First().restaurantId != restaurantId)
            {
                return 1; // remove and add 
            }
            else
            {
                return 2; // just add
            }


        }


        public Cart AddCartFunction(Cart cart,Product product)
        {
            cart.Cost = cart.Cost + (product!.price * cart.Quantity ?? 1);
            if (cart.Options != null)
            {


                String[] options = cart.Options!.Split("#");

                foreach (var item in options)
                {
                    ProductsOption? productsOption = _context.ProductsOptions!.FirstOrDefault(x => x.Id == int.Parse(item));
                    cart.Cost = cart.Cost + productsOption!.Price;
                }


            }
            return cart;

        }

        public void UpdateFunction(Cart cart){

        }


        public async Task<Cart> AddCart(Cart Cart)
        {
            
            // check is from markets 
            // check market 
           List<Cart> carts = await _context.Carts!.Where(t => t.UserId == Cart.UserId).ToListAsync();
            // Cart getMarket = await _context.Carts!.FirstAsync();
            Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == Cart.ProductId);

           int checkedMarket =checkCart(Cart.restaurantId ,Cart.UserId!,Cart.ProductId);
          
          Cart cartAdded =  AddCartFunction(Cart ,product!);
            switch (checkedMarket){
                case 0 :
                Cart? cartUpdated=_context.Carts!.FirstOrDefault(t => t.ProductId==Cart.ProductId&& t.UserId==Cart.UserId);
                 Console.WriteLine(cartUpdated!.Id+"market");
                 
                cartUpdated.Quantity = Cart.Quantity;
                cartUpdated.Cost = Cart.Cost + (product!.price * Cart.Quantity ?? 1);
                if (Cart.Options != null)
                {


                    String[] options = Cart.Options!.Split("#");

                    foreach (var item in options)
                    {
                        ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(item));
                        cartUpdated.Cost = cartUpdated.Cost + productsOption!.Price;
                    }


                }
                await _context.SaveChangesAsync();
          
                break;
                case 1 :
                _context.Carts!.RemoveRange(carts);
                 await _context.SaveChangesAsync();
                await _context.AddAsync(cartAdded);
                 await _context.SaveChangesAsync();
                break;

                case 2 :
                
                  await _context.AddAsync(cartAdded);
                    await _context.SaveChangesAsync();
                break;
            }

            
            // Cart? isCart = await _context.Carts!.FirstOrDefaultAsync(t => t.ProductId == Cart.ProductId && t.UserId == Cart.UserId);
            // if (getMarket == null)
            // {

            //     Cart.Cost = Cart.Cost + (product!.price * Cart.Quantity ?? 1);
            //     if (Cart.Options != null)
            //     {


            //         String[] options = Cart.Options!.Split("#");

            //         foreach (var item in options)
            //         {
            //             ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(item));
            //             Cart.Cost = Cart.Cost + productsOption!.Price;
            //         }


            //     }

            //     await _context.Carts!.AddAsync(Cart);

            //     await _context.SaveChangesAsync();



            // }
            // else if (isCart != null)
            // {

            //     isCart.Quantity = Cart.Quantity;
            //     isCart.Cost = Cart.Cost + (product!.price * Cart.Quantity ?? 1);
            //     if (Cart.Options != null)
            //     {


            //         String[] options = Cart.Options!.Split("#");

            //         foreach (var item in options)
            //         {
            //             ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(item));
            //             isCart.Cost = isCart.Cost + productsOption!.Price;
            //         }


            //     }
            //     await _context.SaveChangesAsync();
            // }
            // else
            // {
            //     if (getMarket.restaurantId == Cart.restaurantId)
            //     {
            //         carts.Clear();




            //     }


            // }









            // if (isCart == null)
            // {

            //     Cart.Cost = Cart.Cost + (product!.price * Cart.Quantity ?? 1);
            //     if (Cart.Options != null)
            //     {


            //         String[] options = Cart.Options!.Split("#");

            //         foreach (var item in options)
            //         {
            //             ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(item));
            //             Cart.Cost = Cart.Cost + productsOption!.Price;
            //         }


            //     }

            //     await _context.Carts!.AddAsync(Cart);

            //     await _context.SaveChangesAsync();

            // }
            // else
            // {

            //     isCart.Quantity = Cart.Quantity;
            //     isCart.Cost = Cart.Cost + (product!.price * Cart.Quantity ?? 1);
            //     if (Cart.Options != null)
            //     {


            //         String[] options = Cart.Options!.Split("#");

            //         foreach (var item in options)
            //         {
            //             ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(item));
            //             isCart.Cost = isCart.Cost + productsOption!.Price;
            //         }


            //     }
               

            // }



            return Cart;


        }


        //  حساب ديليفرى كوست معطل 
        public async Task<ResponseCart> GetCarts(string UserId, string code, int AddressId)
        {
            CouponResponse? couponResponse = new CouponResponse();
            List<CartDetails> cartDetails = new List<CartDetails>();
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.Id == AddressId);
            List<Cart> carts = await _context.Carts!.Where(i => i.UserId == UserId).ToListAsync();

            if (carts.Count > 0)
            {
                AppConfig appConfigDeliveryCost = await getConfigKey("deliveryCostUnit");
                AppConfig appConfigTax = await getConfigKey("taxRatio");

                foreach (var item in carts)
                {
                    List<ProductsOption> optionsList = new List<ProductsOption>();

                    Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    // get Options
                    if (item.Options != null)
                    {
                        String[] options = item.Options!.Split("#");

                        optionsList.Clear();
                        foreach (var option in options)
                        {
                            ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(option));
                            if (productsOption != null)
                                optionsList.Add(productsOption!);
                        }


                    }

                    cartDetails.Add(
                        new CartDetails
                        {
                            Cart = item,
                            Product = product,
                            Options = optionsList!
                        });
                }


                Product? productNew = await _context.Products!.FirstOrDefaultAsync(x => x.Id == carts.First().ProductId);
                // Market? market = await _context.Markets!.FirstOrDefaultAsync(t => t.Id == productNew!.restaurantId);

                // double distance = Functions.GetDistance(address!.Lat, market!.Lat, address.Lng, market.Lng);
                double productsCost = carts.Sum(i => i.Cost);
                // double.Parse(appConfigDeliveryCost.Value ?? "0.0") 
                double deliveryCost = 0.0;

                double tax = 0.15 * (productsCost + deliveryCost);

                double totalCost = productsCost + deliveryCost + tax;

                // CHICK COUPON
                if (code != null)
                {

                    Coupon? coupon = await _context.Coupons!.FirstOrDefaultAsync(x => x.Code == code);

                    if (coupon != null)
                    {
                        DateTime dateNow = DateTime.Now.AddHours(3);
                        if (dateNow.Ticks > coupon.EndDate.Ticks)
                        {
                            couponResponse = new CouponResponse
                            {
                                Status = "error",
                                Message = "coupon is expired",
                                coupon = coupon

                            };

                        }
                        else if (coupon.MinPrice > totalCost)
                        {

                            couponResponse = new CouponResponse
                            {
                                Status = "error",
                                Message = "order less than coupon Minimum price",
                                coupon = coupon

                            };

                        }

                        else if (coupon.MaxUseCount != 0 && coupon.UseCount >= coupon.MaxUseCount)
                        {

                            couponResponse = new CouponResponse
                            {
                                Status = "error",
                                Message = "coupon use count completed",
                                coupon = coupon

                            };

                        }
                        else if (coupon.UserId != null && coupon.UserId != UserId)
                        {

                            couponResponse = new CouponResponse
                            {
                                Status = "error",
                                Message = "you do not have permission to use this coupon",
                                coupon = coupon

                            };

                        }
                        else
                        {
                            totalCost = totalCost - coupon!.Discount;
                            coupon.UseCount = coupon.UseCount + 1;
                            _context.SaveChanges();
                            couponResponse = new CouponResponse
                            {
                                Status = coupon.Discount.ToString(),
                                Message = "Success",
                                coupon = coupon

                            };

                        }






                    }
                    else
                    {
                        couponResponse = new CouponResponse
                        {
                            Status = "error",
                            Message = "coupon not found",


                        };
                    }



                }


                ResponseCart responseCart = new ResponseCart
                {
                    Carts = cartDetails,
                    TotalCost = totalCost,
                    ProductsCost = productsCost,
                    DeliveryCost = deliveryCost,
                    Tax = tax,
                    CouponDetails = couponResponse!
                };

                return responseCart;

            }
            else
            {

                ResponseCart responseCart = new ResponseCart
                {
                    Carts = cartDetails,
                    TotalCost = 0.0,
                    ProductsCost = 0.0,
                    DeliveryCost = 0.0,
                    Tax = 0.0,
                    CouponDetails = couponResponse!
                };

                return responseCart;
            }


        }

        private async Task<AppConfig> getConfigKey(string key)
        {
            AppConfig? appConfig = await _context.AppConfigs!.FirstOrDefaultAsync(x => x.Key == key);
            return appConfig!;
        }

        public async Task<Cart> GitCartById(int CartId)
        {
            Cart? cart = await _context.Carts!.FirstOrDefaultAsync(x => x.Id == CartId);
            return cart!;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        // public async Task<Cart> UpdateCart(UpdateCartDto updateCart, int id)
        // {

        //     Cart? cart = await _context.Carts!.FirstOrDefaultAsync(x => x.Id == id);

        //     Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == cart!.ProductId);
        //     updateCart.ProductId = cart!.ProductId;
        //     updateCart.Cost = product!.price * updateCart.Quantity ?? 1;
        //     updateCart.Options = cart.Options;

        //     _mapper.Map(updateCart, cart);

        //     // _repository.UpdateCart(cart);
        //     await _context.SaveChangesAsync();

        //     return cart;
        //     ///  no thing
        // }
            public async Task<Cart> UpdateCart(Cart updateCart)
            {

            Cart? cart = await _context.Carts!.FirstOrDefaultAsync(x => x.Id == updateCart.Id);

            Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == cart!.ProductId);
            updateCart.ProductId = cart!.ProductId;
            updateCart.Cost = product!.price * updateCart.Quantity ?? 1;
            updateCart.Options = cart.Options;
            updateCart.restaurantId = cart.restaurantId;
           
            _mapper.Map(updateCart, cart);

            // _repository.UpdateCart(cart);
            await _context.SaveChangesAsync();

            return cart;
            ///  no thing
        }



        public async Task<Cart> DeleteCart(int CartId)
        {
            Cart? cart = await _context.Carts!.FirstOrDefaultAsync(x => x.Id == CartId);

            if (cart != null)
            {
                _context.Carts!.Remove(cart);

                await _context.SaveChangesAsync();
            }

            return cart!;
        }

    }
}