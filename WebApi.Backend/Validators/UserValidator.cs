using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Backend.Models;

namespace WebApi.Backend.Validators
{
    public class RegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(6).WithMessage("Username has is at least 6 character.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password has is at least 6 character.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword is required.")
                .MinimumLength(6).WithMessage("ConfirmPassword has is at least 6 character.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .MaximumLength(50).WithMessage("FirstName can not over 50 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .MaximumLength(50).WithMessage("LastName can not over 50 characters");
            RuleFor(x => x.Dob)
                .NotNull().WithMessage("Dob is required.");
            //.GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot greater than 100 years");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password is math");
                }
            });
        }
    }
}
