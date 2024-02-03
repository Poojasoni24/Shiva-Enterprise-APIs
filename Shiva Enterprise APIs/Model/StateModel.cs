using Shiva_Enterprise_APIs.Entities;

namespace Shiva_Enterprise_APIs.Model
{
    public class StateModel
    {
        public string State_Name { get; set; }
        public string State_Code { get; set; }
        public Guid? Country_Id { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
