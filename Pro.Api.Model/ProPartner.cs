
namespace Pro.Api.Model.Concrete
{
    public class ProPartnerPassword
    {
        public virtual string LeadPostingPassword { get; set; } 
        public virtual string SearchHomesAPIPassword { get; set; }
    }


    public class ProPartner
    {
        public string SLODestinationURL { get; set; }
        public virtual string LeadPostingPassword { get; set; }
        public virtual string SearchHomesAPIPassword { get; set; }
        public string FromRegEmail { get; set; }
        public string PartnerName { get; set; }
        public string BrandPartnerName { get; set; }
        public string SsoDestionationUrl { get; set; }
        public string PartnerLogo { get; set; }
        public int PartnerId { get; set; }
        public string SelectMetroId { get; set; }
        public string UsesSso { get; set; }
        public string FromEmail { get; set; }
        public int MaskIndex { get; set; }
        public string PartnerSiteUrl { get; set; }
        public string MobileCustomStyle { get; set; }
        public string DesktopCustomStyle { get; set; }
    }


    [Serializable]
    public class ProBrand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string LogoSmall { get; set; }
        public string LogoMedium { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public string State { get; set; }
        public bool HasShowCase { get; set; }
        public bool IsBoyl { get; set; }
        public bool IsCustomBuilder { get; set; }
        public bool HasBilledCommununities { get; set; }
        public DateTime DateLastChanged { get; set; }
    }
    public class ProBrandComparer : EqualityComparer<ProBrand>
    {

        public override bool Equals(ProBrand b1, ProBrand b2)
        {
            return b1.BrandId == b2.BrandId;
        }

        public override int GetHashCode(ProBrand brd)
        {
            return brd.BrandId.GetHashCode();
        }

    }

    public class PartnerCustomStyle
    {
        public string HeroImage { get; set; }
        public string LinkColor { get; set; }
        public string VisitedLinkColor { get; set; }
        public string ButtonColor { get; set; }
        public string ButtonBgColor { get; set; }
        public string DisabledButtonColor { get; set; }
        public string DisabledButtonBgColor { get; set; }
        public string FooterBgColor { get; set; }
    }
}
