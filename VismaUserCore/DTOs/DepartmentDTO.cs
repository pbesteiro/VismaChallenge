namespace VismaUserCore.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? UserManagerId { get; set; }
    }
}
