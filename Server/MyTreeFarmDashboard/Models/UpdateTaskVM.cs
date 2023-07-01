using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using AP.MyTreeFarm.Application.Errors;

namespace MyTreeFarmDashboard.Models
{

    public class UpdateTaskVM
    {
        //public int TaskId { get; set; }
        public List<SelectListItem> Employees { get; set; }
        public List<SelectListItem> Zones { get; set; }
        public UpdateTreeTaskDTO TreeTask { get; set; }
    }

    public class UpdateTaskVMDTOValidator : AbstractValidator<UpdateTaskVM>
    {
        public UpdateTaskVMDTOValidator()
        {
            RuleFor(x => x.TreeTask.Name).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(TreeTaskErrors.Name);
            RuleFor(x => x.TreeTask.Description).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 4000).WithMessage(TreeTaskErrors.Description);
            RuleFor(x => x.TreeTask.Priority).Must(prio => prio >= 0).WithMessage(TreeTaskErrors.Priority);
            RuleFor(x => x.TreeTask.Duration).Must(duration => duration > 0).WithMessage(TreeTaskErrors.Duration);
            RuleFor(x => x.TreeTask.DatePlanned).Must(BeAValidDate).WithMessage(TreeTaskErrors.DatePlanned);
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}