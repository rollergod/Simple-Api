using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiForPortfolio.Models.Abstract
{
    public interface IPersonRepository
    {
        Task <List<Person>> GetAllPeople();
        Task <Person> GetById(int id);
        Task<bool> Delete(Person model);
        Task <Person> Update(Person model);
        Task <Person> Add(Person model);

    }
}
