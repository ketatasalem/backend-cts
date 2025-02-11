using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Domain.Vues
{
    [Keyless] // ✅ Indique que cette entité représente une vue SQL
    public class BainReferenceAlternativeView
    {
        public int CodeBain { get; set; }
        public string EmplacementBain { get; set; }
        public string CodePosteCharge { get; set; }
        public int? GammeChimiqueAlternativeId { get; set; }
        public string NomAlternative { get; set; }
    }
}
