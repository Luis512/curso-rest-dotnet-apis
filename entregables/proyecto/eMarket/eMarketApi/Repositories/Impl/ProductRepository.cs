using AutoMapper;
using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace eMarketApi.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;

        private readonly eMarketDBContext _context;

        public ProductRepository(IMapper mapper, eMarketDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Delete(int id)
        {
            var product = _context.Products.Where(_ => _.Id.Equals(id)).FirstOrDefault();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public List<Product> Get()
        {
            List<Product> products = new List<Product>();
            foreach (var prdt in _context.Products)
            {
                products.Add(_mapper.Map<Product>(prdt));
            }
            return products;
        }

        public Product Get(int id)
        {
            var product = new Product();
            product = _mapper.Map<Product>(_context.Products.Where(_ => _.Id.Equals(id)).FirstOrDefault());
            return product;
        }

        public List<Product> GetProductsByCategory(int id)
        {
            List<Product> products = new List<Product>();
            var productsByCat = _context.Products.Where(_ => _.IdCategory.Equals(id));
            foreach (var prdt in productsByCat)
            {
                products.Add(_mapper.Map<Product>(prdt));
            }

            return products;
        }

        public List<Product> GetProductsByOrder(int id)
        {
            List<Product> products = new List<Product>();
            var productsByOrder = _context.ProductByOrder.Where(_ => _.IdOrder.Equals(id)).ToList();
            foreach (var prdt in productsByOrder)
            {
                products.Add(_mapper.Map<Product>(_context.Products.Find(prdt.IdProduct)));
            }

            return products;
        }

        public async Task<bool> Post(Product product)
        {
            try
            {
                product.Id = new Random().Next(1, 10000);
                _context.Products.Add(_mapper.Map<Products>(product));
                await _context.SaveChangesAsync();                
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> Put(Product product)
        {
            Products model = _context.Products.Where(_ => _.Id.Equals(product.Id)).FirstOrDefault();

            if (model == null)
                return false;
            model.Name = product.Name;
            model.Price = product.Price;
            model.Stock = product.Stock;

            _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
