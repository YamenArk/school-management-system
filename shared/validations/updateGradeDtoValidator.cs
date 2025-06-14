using FluentValidation;
using Shared.Types.Dtos;
public class UpdateGradeDtoValidator : AbstractValidator<UpdateGradeDto>
{
    public UpdateGradeDtoValidator()
    {
        RuleFor(x => x.Grade)
            .InclusiveBetween(0, 100)
            .WithMessage("Grade must be between 0 and 100.");
    }
}
