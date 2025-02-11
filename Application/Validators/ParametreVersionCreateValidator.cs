using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;

namespace Labo_Cts_backend.Application.Validators
{
    public class ParametreVersionCreateValidator : AbstractValidator<ParametreVersionCreateDto>
    {
        public ParametreVersionCreateValidator()
        {
            RuleFor(pv => pv.IdParametreChimique)
                .NotEmpty().WithMessage("Le code du paramètre chimique est requis.")
                .MinimumLength(2).WithMessage("Le code du paramètre chimique doit avoir au moins 2 caractères.")
                .MaximumLength(10).WithMessage("Le code du paramètre chimique ne doit pas dépasser 10 caractères.");

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
