using System.ComponentModel;

namespace Shiva_Enterprise_APIs.Model
{
    public class RoleModel
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string? NormalizedName { get; set; }

        public string? ConcurrencyStamp { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
