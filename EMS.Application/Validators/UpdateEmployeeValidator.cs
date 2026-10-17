using EMS.Application.DTOs;
using FluentValidation;

namespace EMS.Application.Validators;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        Include(new CreateEmployeeValidator());
    }
}