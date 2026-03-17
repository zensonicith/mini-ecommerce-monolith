using FluentValidation;
using MyApp.Application.DTOs;

namespace MyApp.Application.Validators;

internal sealed class UploadImageRequestValidator : AbstractValidator<UploadImageRequestDto>
{
    public UploadImageRequestValidator()
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image is required.")
            .Must(f => f == null || f.Length > 0).WithMessage("Image must not be empty.");
    }
}
