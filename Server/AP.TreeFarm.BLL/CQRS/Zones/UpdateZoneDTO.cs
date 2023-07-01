using System.Linq;
using FluentValidation;
using AP.MyTreeFarm.Application.Errors;
using AP.MyTreeFarm.Application.Interfaces;

namespace AP.MyTreeFarm.Application.CQRS.Zones
{

    public class UpdateZoneDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SurfaceArea { get; set; }
        public int SiteId { get; set; }
        public int TreeId { get; set; }
    }

    public class UpdateZoneDTOValidator : AbstractValidator<UpdateZoneDTO>
    {
        public UpdateZoneDTOValidator()
        {
			RuleFor(x => x.Name).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(ZoneErrors.Name);
			RuleFor(x => x.SurfaceArea).Must(prio => prio > 0).WithMessage(ZoneErrors.SurfaceArea);
        }
    }
    
    public class UpdateZoneDTOAdvancedValidator : UpdateZoneDTOValidator
    {
        private readonly IUnitofWork _uow;

        public UpdateZoneDTOAdvancedValidator(IUnitofWork uow)

        {
            _uow = uow;
            RuleFor(x => x).MustAsync(async (dto, token) =>
            {
                var site = await _uow.SitesRepository.GetById(dto.SiteId);
                var counter = 1;
                foreach (var zone in site.Zones.Where(zone => zone.Id != dto.Id))
                {
                    if (zone.TreeId == dto.TreeId) counter++;
                    if (counter > 3) return false;
                }

                return true;
            }).WithMessage(ZoneErrors.MaxThreeTreesPerZone);
        }
    }
}