using FluentValidation;
using AP.MyTreeFarm.Application.Errors;

namespace AP.MyTreeFarm.Application.CQRS.Employees
{
    public class CreateEmployeeDTO
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

    }
    
    public class CreateEmployeeDTOValidator : AbstractValidator<CreateEmployeeDTO>
    {
        public CreateEmployeeDTOValidator()
        {
			RuleFor(x => x.FirstName).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.FirstName);
            RuleFor(x => x.LastName).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.LastName);
			RuleFor(x => x.EmployeeId).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.EmployeeId);
			RuleFor(x => x.Email).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.Email);
			RuleFor(x => x.Password).Must(p => !string.IsNullOrEmpty(p) && p.Length <= 255).WithMessage(EmployeeErrors.Password);
        }
    }
}
