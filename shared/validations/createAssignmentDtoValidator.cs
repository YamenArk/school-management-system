using FluentValidation;
public class CreateAssignmentDtoValidator : AbstractValidator<CreateAssignmentDto>
{
    public CreateAssignmentDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 250).WithMessage("Title must be between 3 and 250 characters.");

        RuleFor(x => x.FileUrl)
            .NotEmpty().WithMessage("File URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("File URL must be a valid absolute URL.");

        RuleFor(x => x.MaxMark)
            .GreaterThan(0).WithMessage("MaxMark must be greater than 0.");
    }
}
