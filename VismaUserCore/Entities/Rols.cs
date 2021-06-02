using System.Collections.Generic;

namespace VismaUserCore.Entities
{
    public partial class Rols: BaseEntity
    {
        public Rols()
        {
            RolPermissions = new HashSet<RolPermissions>();
            RolsUser = new HashSet<RolsUser>();
        }

        public string Description { get; set; }

        public virtual ICollection<RolPermissions> RolPermissions { get; set; }
        public virtual ICollection<RolsUser> RolsUser { get; set; }
    }
}
