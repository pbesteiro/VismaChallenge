using System.Collections.Generic;

namespace VismaUserCore.Entities
{
    public partial class Permissions: BaseEntity
    {
        public Permissions()
        {
            RolPermissions = new HashSet<RolPermissions>();
        }
        public string Description { get; set; }

        public virtual ICollection<RolPermissions> RolPermissions { get; set; }
    }
}
