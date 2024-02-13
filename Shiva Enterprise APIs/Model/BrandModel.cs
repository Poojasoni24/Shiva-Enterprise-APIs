namespace Shiva_Enterprise_APIs.Model
{
    public class BrandModel
    { 
    public string BrandCode { get; set; }
    public string BrandName { get; set; }
    public string? BrandDescription { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}
}