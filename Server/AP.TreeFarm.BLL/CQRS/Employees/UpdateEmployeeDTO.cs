using FluentValidation;
using AP.MyTreeFarm.Application.Errors;

namespace AP.MyTreeFarm.Application.CQRS.Employees
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateEmployeeDTOValidator : AbstractValidator<UpdateEmployeeDTO>
    {
        public UpdateEmployeeDTOValidator()
        {
            RuleFor(x => x.FirstName).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.FirstName);
            RuleFor(x => x.LastName).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.LastName);
            RuleFor(x => x.EmployeeId).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.EmployeeId);
            RuleFor(x => x.Email).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.Email);
        }
    }
}
