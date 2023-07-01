using AP.MyTreeFarm.Application.CQRS.Zones;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using AP.MyTreeFarm.Application.Errors;

namespace MyTreeFarmDashboard.Models
{



    public class UpdateZoneVM
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public List<SelectListItem> Trees { get; set; }
        public UpdateZoneDTO Zone { get; set; }
    }

    public class UpdateZoneVMDTOValidator : AbstractValidator<UpdateZoneVM>
    {
        public UpdateZoneVMDTOValidator()
        {
            RuleFor(x => x.Zone.Name).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(ZoneErrors.Name);
            RuleFor(x => x.Zone.SurfaceArea).Must(prio => prio > 0).WithMessage(ZoneErrors.SurfaceArea);
        }
    }
}