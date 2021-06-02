using FluentValidation;
using VismaUserCore.Requests;

namespace VismaUserInsfrestucture.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserValidator()
        {
            RuleFor(user => user.DepartmentId)
            .NotNull()
            .WithMessage("The id department can't be null");

            RuleFor(user => user.DepartmentId)
            .GreaterThan(0)
            .WithMessage("The id department can't be zero");

            RuleFor(user => user.Email)
           .NotNull()
            .WithMessage("The email can´t be null");

            RuleFor(user => user.Email)
           .Must(x => x.Length >0)
            .WithMessage("the length of the email cant be zero");

            RuleFor(user => user.Email)
            .Must(x => x.Contains("@"))
            .WithMessage("The email must have @");

            RuleFor(user => user.Name)
            .NotNull()
            .WithMessage("The name can't be null");

            RuleFor(user => user.Name)
           .Must(x => x.Length > 0)
            .WithMessage("the length of the name cant be zero");


            RuleFor(user => user.Password)
            .NotNull()
            .WithMessage("The password can't be null");

            RuleFor(user => user.Password)
           .Must(x => x.Length > 0)
            .WithMessage("the length of the password cant be zero");

        }


    }

}
