using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AP.MyTreeFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AP.MyTreeFarm.Infrastructure.Repositories;

public class SitesRepository : ISiteRepository
{
    private readonly MyTreeFarmContext context;

    public SitesRepository(MyTreeFarmContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Site>> GetAll()
    {
        return await context.Sites.Include(s => s.Zones).ToListAsync();
    }

    public Task<IEnumerable<Site>> GetAll(int pageNr, int pageSize)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Site> GetById(int id)
    {
        return await context.Sites.Include(s => s.Zones)
            .ThenInclude(z => z.Tree).FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public Site Create(Site newSite)
    {
        context.Sites.Add(newSite);
        return newSite;
    }

    public Site Update(Site modifiedSite)
    {
        context.Sites.Update(modifiedSite);
        return modifiedSite;
    }

    public void Delete(Site site)
    {
        context.Sites.Remove(site);
    }
}