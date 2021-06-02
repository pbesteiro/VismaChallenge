namespace VismaUserCore.Entities
{
    public partial class RolPermissions: BaseEntity
    {
        public int? RolId { get; set; }
        public int? PermissionId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual Rols Rol { get; set; }
    }
}
