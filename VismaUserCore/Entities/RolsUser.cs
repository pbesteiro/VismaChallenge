namespace VismaUserCore.Entities
{
    public partial class RolsUser: BaseEntity
    {
        public int? RolId { get; set; }
        public int? UserId { get; set; }

        public virtual Rols Rol { get; set; }
        public virtual Users User { get; set; }
    }
}
