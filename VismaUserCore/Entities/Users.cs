using System.Collections.Generic;

namespace VismaUserCore.Entities
{
    public partial class Users: BaseEntity
    {
        public Users()
        {
            Departments = new HashSet<Departments>();
            RolsUser = new HashSet<RolsUser>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int? DepartmentId { get; set; }

        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<RolsUser> RolsUser { get; set; }
    }
}
