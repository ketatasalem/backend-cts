namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class BainCreateDto
    {
        public string Emplacement { get; set; } = null!;
        public int DimensionLargeur { get; set; }
        public int DimensionLongueur { get; set; }
        public int DimensionHauteur { get; set; }
        public bool EstReference { get; set; }
        public int? BainReferenceCode { get; set; }
        public bool Actif { get; set; }
        public string CodePosteCharge { get; set; } = null!;
        public List<AlternativeCreateDto>? Alternatives { get; set; }
    }
}
