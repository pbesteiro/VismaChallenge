using FluentValidation;
using VismaUserCore.Requests;

namespace VismaUserInsfrestucture.Validators
{
    public class AddRolsUserValidator : AbstractValidator<AddRolsUserRequest>
    {

        public AddRolsUserValidator()
        {
            RuleFor(rols => rols.UserId)
            .NotNull()
            .WithMessage("The id user to patch can't be null");

            RuleFor(rols => rols.UserId)
            .GreaterThan(0)
            .WithMessage("The id user to patch can't be 0");

            RuleFor(rols => rols.RolsId)
            .NotNull().WithMessage("The rols to assign can't be null");

            RuleFor(rols => rols.RolsId)
            .Must(x => x.Count > 0).WithMessage("Almost one rol is requiered");

            RuleForEach(rols => rols.RolsId)
            .GreaterThan(0).WithMessage("Each Rol Id must be greaten than 0");

        }
    }
}
