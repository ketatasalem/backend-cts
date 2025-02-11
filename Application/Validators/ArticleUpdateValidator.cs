using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;

namespace Labo_Cts_backend.Application.Validators
{
    public class ArticleUpdateValidator : AbstractValidator<ArticleUpdateDto>
    {
        public ArticleUpdateValidator() 
        {
            RuleFor(a => a.UniteAnalyse)
                .NotEmpty().WithMessage("L'unité analyse est requise.")
                .MinimumLength(1).WithMessage("L'unité d'analyse doit avoir au moins 1 caractère.")
                .MaximumLength(5).WithMessage("L'unité d'analyse ne doit pas dépasser 50 caractères.");

            RuleFor(a => a.MethodeAnalyse)
                .NotEmpty().WithMessage("La méthode d'analyse est requise.")
                .MinimumLength(3).WithMessage("La méthode d'analyse doit avoir au moins 3 caractères.")
                .MaximumLength(20).WithMessage("La méthode d'analyse ne doit pas dépasser 20 caractères.");

            RuleFor(a => a.EstRatio)
                .NotNull().WithMessage("L'attribution, c'est un article à des valeurs ratio, oui ou non, est requise.")
                .Must(value => value == true || value == false).WithMessage("EstRatio doit être une valeur booléenne valide.");

            RuleFor(a => a.ValeurRatioMinimale)
                .GreaterThan(0).When(a => a.EstRatio == true).WithMessage("La valeur ratio minimale doit être supérieure à 0 si EstRatio est vrai.");

            RuleFor(a => a.ValeurRatioMaximale)
                .GreaterThan(a => a.ValeurRatioMinimale).When(a => a.EstRatio == true).WithMessage("La valeur ratio maximale doit être supérieure à la valeur ratio minimale si EstRatio est vrai.");


            RuleFor(a => a.SeuilMinimum)
                .NotNull().WithMessage("Le seuil minimal d'un article est requis.")
                .GreaterThanOrEqualTo(0).WithMessage("Le seuil minimal doit être supérieur ou égal à 0.");

        }
    }
}
