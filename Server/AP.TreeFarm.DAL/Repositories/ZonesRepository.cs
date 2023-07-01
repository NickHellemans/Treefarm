using System.Collections.Generic;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AP.MyTreeFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AP.MyTreeFarm.Infrastructure.Repositories;

public class ZonesRepository : IZoneRepository
{
    private readonly MyTreeFarmContext context;

    public ZonesRepository(MyTreeFarmContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Zone>> GetAll()
    {
        return await context.Zones.Include(z => z.Tasks).ToListAsync();
        //return Task.FromResult<IEnumerable<Zone>>(context.Zones.Take(4).ToList());
    }

    public Task<IEnumerable<Zone>> GetAll(int pageNr, int pageSize)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Zone> GetById(int id)
    {
        return await context.Zones
            .Include(z => z.Site)
            .Include(z => z.Tree)
            .Include(z => z.Tasks)
            .Include(z => z.Tasks)
            .ThenInclude(t => t.Employee)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
        
    public Zone Create(Zone newZone)
    {
        context.Zones.Add(newZone);
        return newZone;
    }

    public Zone Update(Zone modifiedZone)
    {
        context.Zones.Update(modifiedZone);
        return modifiedZone;
    }

    public void Delete(Zone zone)
    {
        context.Zones.Remove(zone);
    }
}