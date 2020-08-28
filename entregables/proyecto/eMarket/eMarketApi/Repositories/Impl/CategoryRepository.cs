using AutoMapper;
using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMarketApi.Repositories.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly eMarketDBContext _context;

        /// <summary>
        /// Constructor of <see cref="CategoryRepository"/>
        /// </summary>
        /// <param name="context">Dependecy injection of <see cref="eMarketDBContext"/></param>
        /// <param name="mapper">Dependecy injection of <see cref="IMapper"/></param>
        public CategoryRepository(IMapper mapper, eMarketDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Deletes a category by id.
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = _context.Categories.Where(_ => _.Id.Equals(id)).FirstOrDefault();
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all the categories available.
        /// </summary>
        /// <returns></returns>
        public List<Category> Get()
        {
            List<Category> categories = new List<Category>();
            foreach (var ctg in _context.Categories)
            {
                categories.Add(_mapper.Map<Category>(ctg));
            }
            return categories;
        }

        /// <summary>
        /// Gets a category by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category Get(int id)
        {
            var category = new Category();
            category = _mapper.Map<Category>(_context.Categories.Where(_ => _.Id.Equals(id)).FirstOrDefault());
            return category;            
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">A <see cref="Category"/></param>
        public async Task<bool> Post(Category category)
        {
            try
            {
                category.Id = new Random().Next(1, 10000);
                _context.Categories.Add(_mapper.Map<Categories>(category));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Update a category.
        /// </summary>
        /// <param name="category">A <see cref="Category"/></param>
        /// <returns>True if the category was updated successfully, otherwise returns false.</returns>
        public async Task<bool> Put(Category category)
        {
            Categories model = _context.Categories.Where(_ => _.Id.Equals(category.Id)).FirstOrDefault();

            if (model == null)
                return false;
            model.Name = category.Name;
            _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
