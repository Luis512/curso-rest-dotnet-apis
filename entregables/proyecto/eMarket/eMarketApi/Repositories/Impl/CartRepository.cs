using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;
using Microsoft.Extensions.Logging;

namespace eMarketApi.Repositories.Impl
{
    public class CartRepository : ICartRepository
    {
        private readonly eMarketDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CartRepository> _logger;

        public CartRepository(eMarketDBContext context, IMapper mapper, ILogger<CartRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> CheckoutCart(Cart cart)
        {
            if (cart is null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            decimal orderId = new Random().Next(1, 1000);
            Orders order = new Orders()
            {
                Id = orderId,
                IdUser = _context.User.FirstOrDefault(u => u.Username.Equals(cart.Username)).Id,
                Status = "PROCESSED",
                Total = cart.Total,
                ProcessDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
             var wasOrderSaved =  await _context.SaveChangesAsync();
           if(wasOrderSaved > 0)
           {
                if (!AddProductsToOrder(orderId, cart.Products))
                    throw new Exception("Error al agregar los productos a la orden");
                var updateResult = await UpdateProducts(cart.Products);
                if (updateResult)
                    throw new Exception("Error al actualizar la cantidad en stock");
           }
           return true;
        }

        public List<Order> GetPastOrders(string username)
        {
            var query = from order in _context.Orders
                        where order.IdUser ==_context.User.FirstOrDefault(u => u.Username.Equals(username)).Id
                        select order;
            List<Order> pastOrders = new List<Order>();
            foreach (var q in query)
            {
                pastOrders.Add(new Order(q.Id, q.Total, q.ProcessDate));
            }
            return pastOrders;
        }

        private bool AddProductsToOrder(decimal idOrder, List<Product> products)
        {
            foreach (var prod in products)
            {
                _context.ProductByOrder.Add(new ProductByOrder
                {
                    Id = new Random().Next(1,1000000),
                    IdOrder = idOrder,
                    IdProduct =  prod.Id
                });
            }

            _context.SaveChanges();
            return true;
        }

        private async Task<bool> UpdateProducts(List<Product> products)
        {
            var prods = new List<Products>();

            foreach(var prod in products)
            {
                var updatedProd = _mapper.Map<Products>(prod);
                updatedProd.Stock -= prod.Quantity;
                prods.Add(updatedProd);
            }
            _context.Products.UpdateRange(prods);
            await  _context.SaveChangesAsync();
            return true;
        }
    }
}
