using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForPortfolio.Models.Abstract;

namespace WebApiForPortfolio.Models.EF
{
    public class EFPersonRepository : IPersonRepository
    {
        private readonly AppDbContext _appDbContext;
        public EFPersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task <Person> Add(Person model)
        {
            if(model != null)
            {
                await _appDbContext.People.AddAsync(model);
                await _appDbContext.SaveChangesAsync();
                return model;
            }
            return null;
        }

        public async Task<bool> Delete(Person model)
        {
            if(model.Id != 0)
            {
                _appDbContext.People.Remove(model);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task <List<Person>> GetAllPeople()
        {
            List<Person> list = await _appDbContext.People.OrderBy(p => p.Id).ToListAsync();

            return list;
        }

        public async Task <Person> GetById(int id)
        {
            if(id != 0)
            {
                var model = await _appDbContext.People.FirstOrDefaultAsync(p => p.Id == id);
                return model;
            }
            return null;
        }

        public async Task <Person> Update(Person model)
        {
            if(model.Id != 0)
            {
                var newModel = await _appDbContext.People.FirstOrDefaultAsync(m => m.Id == model.Id);
                newModel.Name = model.Name;
                newModel.Age = model.Age;
                await _appDbContext.SaveChangesAsync();
                return newModel;
            }
            return null;
        }
    }
}
