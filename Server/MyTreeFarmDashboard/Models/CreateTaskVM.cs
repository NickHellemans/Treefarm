using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using AP.MyTreeFarm.Application.Errors;

namespace MyTreeFarmDashboard.Models
{



    public class CreateTaskVM
    {
        public List<SelectListItem> employees { get; set; }
        public List<SelectListItem> zones { get; set; }
        public CreateTreeTaskDTO treeTask { get; set; }
    }


    public class CreateTaskVMDTOValidator : AbstractValidator<CreateTaskVM>
    {
        public CreateTaskVMDTOValidator()
        {
            RuleFor(x => x.treeTask.Name).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(TreeTaskErrors.Name);
            RuleFor(x => x.treeTask.Description).Must(desc => !string.IsNullOrEmpty(desc) && desc.Length <= 4000).WithMessage(TreeTaskErrors.Description);
			RuleFor(x => x.treeTask.Priority).Must(prio => prio >= 0).WithMessage(TreeTaskErrors.Priority);
			RuleFor(x => x.treeTask.Duration).Must(duration => duration > 0).WithMessage(TreeTaskErrors.Duration);
			RuleFor(x => x.treeTask.DatePlanned).Must(BeAValidDate).WithMessage(TreeTaskErrors.DatePlanned);

        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
