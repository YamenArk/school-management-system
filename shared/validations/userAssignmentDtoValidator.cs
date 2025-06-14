using FluentValidation;
using Shared.Types.Dtos;
public class UserAssignmentDtoValidator : AbstractValidator<UserAssignmentDto>
{
    public UserAssignmentDtoValidator()
    {
        RuleFor(x => x.Grade)
            .InclusiveBetween(0, 100)
            .WithMessage("Grade must be between 0 and 100.");

        RuleFor(x => x.FileUrl)
            .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("FileUrl must be a valid URL if provided.");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("CreatedAt cannot be a future date.");
    }
}
