using AP.MyTreeFarm.Application.Interfaces;
using FluentValidation;
using AP.MyTreeFarm.Application.Errors;

namespace AP.MyTreeFarm.Application.CQRS.Zones
{
    public class CreateZoneDTO
    {
        public string Name { get; set; }
        public float SurfaceArea { get; set; }
        public int SiteId { get; set; }
        public int TreeId { get; set; }
    }

    public class CreateZoneDTOValidator : AbstractValidator<CreateZoneDTO>
    {
        public CreateZoneDTOValidator()

        {
			RuleFor(x => x.Name).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(ZoneErrors.Name);
			RuleFor(x => x.SurfaceArea).Must(prio => prio > 0).WithMessage(ZoneErrors.SurfaceArea);
        }
    }

    public class CreateZoneDTOAdvancedValidator : CreateZoneDTOValidator
    {
        private readonly IUnitofWork _uow;

        public CreateZoneDTOAdvancedValidator(IUnitofWork uow)

        {
            _uow = uow;
            
            //Max 3 of the same trees per site
            RuleFor(x => x).MustAsync(async (dto, token) =>
            {
                var site = await _uow.SitesRepository.GetById(dto.SiteId);
                var counter = 1;
                foreach (var zone in site.Zones)
                {
                    if (zone.TreeId == dto.TreeId) counter++;
                    if (counter > 3) return false;
                }

                return true;
            }).WithMessage(ZoneErrors.MaxThreeTreesPerZone);
        }
    }
}