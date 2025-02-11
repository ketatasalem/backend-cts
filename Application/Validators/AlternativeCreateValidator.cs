using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;

namespace Labo_Cts_backend.Application.Validators
{
    public class AlternativeCreateValidator : AbstractValidator<AlternativeCreateDto>
    {
        public AlternativeCreateValidator()
        {
            RuleFor(alt => alt.EstParDefaut)
                .NotNull().WithMessage("L'attribution c'est une alternative par défaut ou non est requise.")
                .Must(value => value == true || value == false).WithMessage("EstParDefaut doit être une valeur booléenne valide.");

            RuleFor(alt => alt.Parametres)
                .NotEmpty().WithMessage("Le tableau des paramètres chimiques doit contenir au moins 1 paramètre.");

            RuleForEach(alt => alt.Parametres)
                .SetValidator(new ParametreVersionCreateValidator())
                .WithMessage("Les Paramètres ne doivent pas être vides s'ils sont fournis.");

            RuleForEach(alt => alt.Articles)
                .SetValidator(new ArticleVersionCreateValidator())
                .When(alt => alt.Articles != null && alt.Articles.Any())
                .WithMessage("Les Articles ne doivent pas être vides s'ils sont fournis.");

        }
    }
}
