using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;
using Microsoft.IdentityModel.Tokens;

namespace Labo_Cts_backend.Application.Validators
{
    public class PlanDemandeInterventionCreateValidator : AbstractValidator<PlanDemandeInterventionCreateDto>
    {
        public PlanDemandeInterventionCreateValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Le code est requis.")
                .MinimumLength(3).WithMessage("Le code doit avoir au moins 3 caractères.")
                .MaximumLength(50).WithMessage("Le code ne doit pas dépasser 50 caractères.");

            RuleFor(p => p.Intitule)
                .NotEmpty().WithMessage("L'intitulé est requis.")
                .MinimumLength(3).WithMessage("L'intitulé doit avoir au moins 3 caractères.")
                .MaximumLength(50).WithMessage("L'intitulé ne doit pas dépasser 50 caractères.");

            // ✅ Règle conditionnelle pour CodePosteCharge et Frequence
            RuleFor(p => p.Frequence)
                .NotEmpty().WithMessage("La fréquence est requise lorsque le code poste charge est défini.")
                .When(p => !string.IsNullOrEmpty(p.CodePosteCharge));

            RuleFor(p => p.CodePosteCharge)
                .NotEmpty().WithMessage("Le code poste charge est requis lorsque la fréquence est définie.")
                .When(p => !string.IsNullOrEmpty(p.Frequence) && p.Frequence != "0");

            // ✅ Règle conditionnelle pour Compteur et CodeBain
            RuleFor(p => p.Compteur)
                .NotNull().WithMessage("Le compteur est requis lorsque le code bain est défini et strictement supérieur à 0.")
                .GreaterThan(0).WithMessage("Le compteur doit être strictement supérieur à 0 lorsque le code bain est défini et strictement supérieur à 0.")
                .When(p => !string.IsNullOrEmpty(p.CodeBain) && int.TryParse(p.CodeBain, out int codeBain) && codeBain > 0);

            RuleFor(p => p.CodeBain)
                .NotEmpty().WithMessage("Le code bain est requis lorsque le compteur est défini et strictement supérieur à 0.")
                .When(p => p.Compteur.HasValue && p.Compteur > 0);

            // ✅ Validation pour s'assurer qu'au moins un groupe est rempli
            RuleFor(p => p)
                .Must(p =>
                    (!string.IsNullOrEmpty(p.CodePosteCharge) && !string.IsNullOrEmpty(p.Frequence)) ||
                    (p.Compteur.HasValue && p.Compteur > 0 && !string.IsNullOrEmpty(p.CodeBain))
                )
                .WithMessage("Veuillez renseigner soit la fréquence et le code poste charge, soit le compteur et le code bain.");
        }
    }

}
