using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Models;
using X.PagedList;
using WajedApi.Models.BaseEntity;
using WajedApi.Serveries.AddressesServices;
using WajedApi.Serveries.MarketsService;
using WajedApi.Helpers;
using WajedApi.ViewModels;

namespace WajedApi.Serveries.OrdersServices
{
    public class OrdersServices : IOrdersServices
    {

        private readonly IMapper _mapper;


        private readonly AppDBcontext _context;


        private readonly IAddressesServices _AddressesServices;
        private readonly IMarketsService _IMarketsService;
        // -1 =>  "تم الالغاء"
        //  0 =>  "في انتظار التأكيد"
        // 1=>  "تم تأكيد طلبك"
        // 2 => "جارى التجهيز"
        // 3 => "تم التجهيز"
        // 4 =>  "جارى التوصيل"
        // 5 =>  "تم التسليم"


        List<string> orderStatuses = new List<string>(new string[] { "في انتظار التأكيد", "تم تأكيد طلبك", "جارى التجهيز", "تم التجهيز", "جارى التوصيل", "تم التسليم" });
        public OrdersServices(IMapper mapper, AppDBcontext context, IAddressesServices addressesServices, IMarketsService iMarketsService)
        {
            _mapper = mapper;
            _context = context;
            _AddressesServices = addressesServices;
            _IMarketsService = iMarketsService;
        }
        public async Task<dynamic> AddAsync(dynamic type)
        {


            Order? order = await _context.Orders!.AddAsync(type);
            Market? market = await _context.Markets!.FirstOrDefaultAsync(t => t.Id == order.RestaurantId);
            //    User? provider=await _context.Users!.FirstOrDefaultAsync(t => t.Id==market!.UserId);
            await Functions.SendNotificationAsync(_context, market!.UserId!, "طلب جديد", "تم ارسال طلب جديد", "");

            await _context.SaveChangesAsync();

            return type;
        }

        public async Task<dynamic> AddOrder(Order order, int AddressId)
        {
            List<Cart> carts = await _context.Carts!.Where(x => x.UserId == order.UserId).ToListAsync();


            AppConfig appConfigDeliveryCost = await getConfigKey("deliveryCostUnit");
            AppConfig appConfigTax = await getConfigKey("taxRatio");

            Address address = await _AddressesServices.GitById(AddressId);
            Product? product = await _context.Products!.FirstOrDefaultAsync(t => t.Id == carts.First().ProductId);
            Market market = await _IMarketsService.GitById(order!.RestaurantId);

            double distance = Functions.GetDistance(address.Lat, market.Lat, address.Lng, market.Lng);
            double productsCost = carts.Sum(i => i.Cost);
            // double.Parse(appConfigDeliveryCost.Value ?? "0.0") 
            double deliveryCost = 0.0 * distance;

            double tax = 0.15 * (productsCost + deliveryCost);

            double totalCost = productsCost + deliveryCost + tax;





            order.RestaurantId = market.Id;
            order.Tax = tax;
            order.TotalCost = totalCost;
            order.ProductsCost = productsCost;

            await _context.Orders!.AddAsync(order);
            _context.SaveChanges();

            var orderItemsToAdd = carts.ConvertAll(c => new OrderItem()
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Cost = c.Cost,
                UserId = c.UserId,
                OrderId = order.Id,
                Options = c.Options,
                CreatedAt = c.CreatedAt

            });

            _context.OrderItems!.AddRange(orderItemsToAdd);
            _context.Carts!.RemoveRange(carts);
            _context.SaveChanges();
            await Functions.SendNotificationAsync(_context, market!.UserId!, "طلب جديد", "تم ارسال طلب جديد", "");
            return order;
        }


        public async Task<dynamic> DeleteAsync(int typeId)
        {
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (order != null)
            {
                _context.Orders!.Remove(order);

                await _context.SaveChangesAsync();
            }

            return order!;
        }


        public async Task<dynamic> UpdateOrderStatus(int typeId, int status)
        {
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (order != null)
            {
                order.Status = status;
                User? driver = await _context.Users.FirstOrDefaultAsync(t => t.Id == order.DriverId);
                if (driver != null)
                {
                    if (status != 4)
                    {
                        driver.Status = "Active";
                    }
                }

                await _context.SaveChangesAsync();
            }

            return order!;
        }

        public async Task<dynamic> GetItems(string UserId, int page)
        {
            List<Order> orders = await _context.Orders!.Where(i => i.UserId == UserId).ToListAsync();


            var pageResults = 10f;
            var pageCount = Math.Ceiling(orders.Count() / pageResults);

            var items = await orders
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();



            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public async Task<dynamic> GitById(int typeId)
        {
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == typeId);
            return order!;
        }

        public async Task<dynamic> GitOrderDetails(int orderId, int AddressId)
        {
            List<OrderItem> orderItems = await _context.OrderItems!.Where(t => t.OrderId == orderId).ToListAsync();


            List<OrderDetails> orderDetails = new List<OrderDetails>();
            Order? order = await _context.Orders!.FirstOrDefaultAsync(t => t.Id == orderId);
            Console.WriteLine(order + "MARKET");

            Market? market = await _context.Markets!.FirstOrDefaultAsync(t => t.Id == order!.RestaurantId);

            foreach (OrderItem item in orderItems)
            {
                List<ProductsOption> optionsList = new List<ProductsOption>();
                Product? product = await _context.Products!.FirstOrDefaultAsync(t => t.Id == item.ProductId);



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


                orderDetails.Add(new OrderDetails
                {
                    Product = product,
                    Order = item,
                    Options = optionsList
                });

            }


            return new ResponseOrder
            {
                Products = orderDetails,
                Market = market,
                order = order
            };

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObject(dynamic category)
        {
            // nothing
        }


        private async Task<AppConfig> getConfigKey(string key)
        {
            AppConfig? appConfig = await _context.AppConfigs!.FirstOrDefaultAsync(x => x.Key == key);
            return appConfig!;
        }

        public async Task<dynamic> GitOrdersByMarketId(int marketId)
        {
            List<Order> orders = await _context.Orders!.Where(t => t.RestaurantId == marketId).ToListAsync();

            return new
            {
                successOrders = orders.Where(t => t.Status == 5).ToList(),
                unsuccessfulOrders = orders.Where(t => t.Status == -1).ToList(),
            };
        }

        public async Task<dynamic> GitOrdersByDriverId(string driverId, int AddressId)
        {
            List<ResponseOrder> currentOrders = new List<ResponseOrder>();
            List<ResponseOrder> successOrders = new List<ResponseOrder>();
            List<ResponseOrder> unSuccessOrders = new List<ResponseOrder>();

            Address? userAddress = await _context.Addresses!.FirstOrDefaultAsync(x => x.Id == AddressId);
            List<Order> orders = await _context.Orders!.ToListAsync();

            foreach (var item in orders)
            {
                User? user = await _context.Users!.FirstOrDefaultAsync(t => t.Id == item.UserId);

                if (userAddress != null)
                {
                    double distance = Functions.GetDistance(user!.Lat ?? 0.0, userAddress!.Lng, user!.Lng ?? 0.0, userAddress.Lat);

                    if (distance < 30)
                    {
                        currentOrders.Add(await Functions.getOrderDetails(item.Id, _context));

                    }
                }



            }
            return new
            {
                currentOrders = currentOrders,
                successOrders = orders.Where(t => t.DriverId == driverId && t.Status == 5).ToList(),
                unsuccessfulOrders = orders.Where(t => t.DriverId == driverId && t.Status == -1).ToList(),
            };
        }

        public async Task<dynamic> AcceptOrderDriver(int orderId, string driverId)
        {
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                order.Status = 4;
                order.DriverId = driverId;
                User? driver = await _context.Users.FirstOrDefaultAsync(t => t.Id == driverId);
                if (driver != null)
                {
                    driver.Status = "UnActive";
                }
            }
            await _context.SaveChangesAsync();

            return order!;
        }
    }




}