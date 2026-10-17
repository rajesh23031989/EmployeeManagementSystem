using EMS.Application.DTOs;
using FluentValidation;

namespace EMS.Application.Validators;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First Name is required.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^[0-9]{10}$")
            .WithMessage("Phone number must be exactly 10 digits.");

        RuleFor(x => x.Salary)
            .GreaterThan(0);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);

        RuleFor(x => x.DateOfJoining)
            .LessThanOrEqualTo(DateTime.Today);
    }
}