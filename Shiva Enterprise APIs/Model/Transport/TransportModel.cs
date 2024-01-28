using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Transport
{
    public class TransportModel
    {
        public string TransportCode { get; set; }
        public string TransportName { get; set; }
        public string? TransportDescription { get; set; }
        public bool TransportStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
