using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;

namespace Labo_Cts_backend.Application.Validators
{
    public class BainUpdateValidator : AbstractValidator<BainUpdateDto>
    {
        public BainUpdateValidator()
        {

            RuleFor(b => b.Emplacement)
               .NotEmpty().WithMessage("L'emplacement est requis.")
               .MinimumLength(1).WithMessage("L'emplacement doit avoir au moins 1 caractère.")
               .MaximumLength(50).WithMessage("L'emplacement ne doit pas dépasser 50 caractères.");

            RuleFor(b => b.DimensionLargeur)
                .NotEmpty().WithMessage("La dimension largeur est requise.")
                .GreaterThan(0).WithMessage("La dimension largeur doit être positive.");

            RuleFor(b => b.DimensionLongueur)
                .NotEmpty().WithMessage("La dimension longueur est requise.")
                .GreaterThan(0).WithMessage("La dimension longueur doit être positive.");

            RuleFor(b => b.DimensionHauteur)
                .NotEmpty().WithMessage("La dimension hauteur est requise.")
                .GreaterThan(0).WithMessage("La dimension hauteur doit être positive.");

            RuleFor(b => b.CodePosteCharge)
                .NotEmpty().WithMessage("Le code poste charge est requis.")
                .MinimumLength(5).WithMessage("L'emplacement doit avoir au moins 5 caractère.")
                .MaximumLength(10).WithMessage("Le code poste charge ne doit pas dépasser 10 caractères.");

            RuleFor(b => b.EstReference)
                .NotNull().WithMessage("L'attribution c'est un bain de référence ou non est requise.")
                .Must(value => value == true || value == false).WithMessage("EstReference doit être une valeur booléenne valide.");

            RuleFor(b => b.Alternatives)
                .NotEmpty().When(b => b.EstReference == true).WithMessage("Le tableau des alternatives doit contenir au moins 1 alternative car il s'agit d'un bain de référence.");

            RuleForEach(b => b.Alternatives)
                .SetValidator(new AlternativeUpdateValidator());

            RuleFor(b => b.Alternatives)
                .Must(alts => alts.Count(alt => alt.EstParDefaut) == 1)
                .When(b => b.EstReference == true)
                .WithMessage("Un bain de référence doit avoir exactement une alternative définie comme par défaut.");


            RuleFor(b => b.CodePosteCharge)
                .NotEmpty().When(b => b.EstReference == false).WithMessage("Le Code de la poste de la charge est obligatoire car il ne s'agit pas d'un bain de référence.");

            RuleFor(b => b.Actif)
                .NotNull().WithMessage("L'attribution c'est un bain actif ou non est requise.")
                .Must(value => value == true || value == false).WithMessage("Actif doit être une valeur booléenne valide.");
        }
    }
}
