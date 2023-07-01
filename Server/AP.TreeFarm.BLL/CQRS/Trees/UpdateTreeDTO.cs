using FluentValidation;
using AP.MyTreeFarm.Application.Errors;

namespace AP.MyTreeFarm.Application.CQRS.Trees
{
    public class UpdateTreeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string InstructionsUrl { get; set; }
        public string QrCodeUrl { get; set; }

    }

    public class UpdateTreeValidator : AbstractValidator<UpdateTreeDTO>
    {
        public UpdateTreeValidator()
		{
			RuleFor(x => x.Name).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(TreeErrors.Name);
			RuleFor(x => x.PictureUrl).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(TreeErrors.PictureUrl);
			RuleFor(x => x.InstructionsUrl).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(TreeErrors.InstructionsUrl);
		}
    }
}