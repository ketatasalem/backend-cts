namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class ParametreChimiqueCreateDto
    {
        public string Code { get; set; } = null!;

        public string Nom { get; set; } = null!;

        public string Unite { get; set; } = null!;

        public string TypeValeur { get; set; } = null!;
    }
}
