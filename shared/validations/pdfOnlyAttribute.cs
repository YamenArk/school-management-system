using System.ComponentModel.DataAnnotations;
public class PdfOnlyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var url = value as string;

        if (string.IsNullOrWhiteSpace(url) || !url.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            return new ValidationResult("Only PDF files are allowed.");
        }

        return ValidationResult.Success;
    }
}
