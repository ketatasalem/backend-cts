using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;

namespace Labo_Cts_backend.Application.Validators
{
    public class ArticleVersionCreateValidator : AbstractValidator<ArticleVersionCreateDto>
    {
        public ArticleVersionCreateValidator()
        {
            RuleFor(pv => pv.CodeArticle)
                .NotEmpty().WithMessage("Le code d'article est requis.")
                .MinimumLength(7).WithMessage("Le code d'article doit avoir au moins 7 caractères.")
                .MaximumLength(10).WithMessage("Le code d'article ne doit pas dépasser 10 caractères.");

            RuleFor(pv => pv.ValeurMin)
                .GreaterThanOrEqualTo(0)
                .When(pv => pv.ValeurMin.HasValue)
                .WithMessage("La valeur minimale doit être positive.");

            RuleFor(pv => pv.ValeurMax)
               .GreaterThanOrEqualTo(0)
               .When(pv => pv.ValeurMax.HasValue)
               .WithMessage("La valeur maximale doit être positive.");

            RuleFor(pv => pv.Valeur)
               .GreaterThanOrEqualTo(0)
               .When(pv => pv.Valeur.HasValue)
               .WithMessage("La valeur moyenne doit être positive.");
        }
    }
}
