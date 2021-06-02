namespace VismaUserCore.Entities
{
    public partial class Departments: BaseEntity
    {
        public string Description { get; set; }
        public int? UserManagerId { get; set; }

        public virtual Users UserManager { get; set; }
    }
}
