using FluentValidation;
using Shared.Types.Dtos;
public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(5, 250).WithMessage("Title must be between 5 and 250 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}

public class UpdateCourseDtoValidator : AbstractValidator<UpdateCourseDto>
{
    public UpdateCourseDtoValidator()
    {
        RuleFor(x => x)
            .Must(dto => !string.IsNullOrWhiteSpace(dto.Title) || !string.IsNullOrWhiteSpace(dto.Description))
            .WithMessage("At least one field (Title or Description) must be provided.");

        When(x => x.Title != null, () =>
        {
            RuleFor(x => x.Title)
                .Length(5, 250).WithMessage("Title must be between 5 and 250 characters.");
        });

        When(x => x.Description != null, () =>
        {
            RuleFor(x => x.Description)
                .Length(5, 1024).WithMessage("Description must be between 5 and 1024 characters.");
        });
    }
}
