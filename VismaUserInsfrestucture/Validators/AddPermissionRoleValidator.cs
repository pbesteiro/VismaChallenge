using FluentValidation;
using VismaUserCore.Requests;

namespace VismaUserInsfrestucture.Validators
{
    public class AddPermissionRoleValidator : AbstractValidator<AddPermissionRoleRequest>
    {

        public AddPermissionRoleValidator()
        {
            RuleFor(permissios => permissios.RolId)
                .NotNull()
                .WithMessage("The id rol to patch can't be null");

            RuleFor(permissios => permissios.RolId)
            .GreaterThan(0)
            .WithMessage("The id rol to patch can't be 0");

            RuleFor(permissios => permissios.PermissionsId)
            .NotNull().WithMessage("The rols to assign can't be null");

            RuleFor(permissios => permissios.PermissionsId)
                      .Must(x => x.Count > 0).WithMessage("Almost one permission is requiered");

            RuleForEach(x => x.PermissionsId)
            .GreaterThan(0).WithMessage("Each Permission Id must be greaten than 0");


        }

    }
}
