using AutoMapper;
using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eMarketApi.Repositories.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly eMarketDBContext _context;

        public CategoryRepository(IMapper mapper, eMarketDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async void Delete(int id)
        {

            var category = _context.Categories.Where(_ => _.Id.Equals(id)).FirstOrDefault();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public List<Category> Get()
        {
            List<Category> categories = new List<Category>();
            foreach (var ctg in _context.Categories)
            {
                categories.Add(_mapper.Map<Category>(ctg));
            }
            return categories;
        }

        public Category Get(int id)
        {
            var category = new Category();
            category = _mapper.Map<Category>(_context.Categories.Where(_ => _.Id.Equals(id)).FirstOrDefault());
            return category;            
        }

        public async void Post(Category category)
        {
            category.Id = new Random().Next(1, 10000);
            _context.Categories.Add(_mapper.Map<Categories>(category));
            await _context.SaveChangesAsync();
        }
    }
}
