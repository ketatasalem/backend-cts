using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Response
{
    public class PosteChargeResponseDto
    {
        public string Code { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public string CodeSite { get; set; } = null!;

        public string CodeCentreCharge { get; set; } = null!;
        public virtual CentresDeCharge CodeCentreChargeNavigation { get; set; } = null!;
        public virtual Site CodeSiteNavigation { get; set; } = null!;
    }
}
