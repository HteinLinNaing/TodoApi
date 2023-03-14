using System.Data;
using System.Linq;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Repositories
{
    public class HeroRepository : RepositoryBase<Hero>, IHeroRepository
    {
        public HeroRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Hero>> SearchHero(string searchTerm)
        {
            return await RepositoryContext.Heroes
                        .Where(s => s.Name.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<Hero>> SearchHeroMultiple(HeroSearchPayload SearchObj)
        {
            return await RepositoryContext.Heroes
                        .Where(s => s.Name.Contains(SearchObj.Name ?? "") || s.Address.Contains(SearchObj.Address ?? ""))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public bool IsExists(long id)
        {
            return RepositoryContext.Heroes.Any(e => e.Id == id);
        }
    }

}