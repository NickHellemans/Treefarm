using FluentValidation;
using AP.MyTreeFarm.Application.Errors;

namespace AP.MyTreeFarm.Application.CQRS.Sites;

public class CreateSiteDTO
{
    public string Name { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    
    public string MapPicturePath { get; set; }
}

public class CreateSiteDTOValidator : AbstractValidator<CreateSiteDTO>
{
    public CreateSiteDTOValidator()
    {
		RuleFor(x => x.Name).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(SiteErrors.Name);
		RuleFor(x => x.PostalCode).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(SiteErrors.PostalCode);
		RuleFor(x => x.Street).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(SiteErrors.Street);
		RuleFor(x => x.StreetNumber).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(SiteErrors.StreetNumber);
		RuleFor(x => x.MapPicturePath).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(SiteErrors.MapPicturePath);
    }
}