namespace Pro.Api.Model.Concrete;

public class Partner
{
    public int PartnerId { get; set; }
    public string? PrivateLabelSite { get; set; }
    public byte StatusId { get; set; }
    public string? PartnerTypeCode { get; set; }
    public string? PartnerName { get; set; }
}