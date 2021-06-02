using System.Collections.Generic;

namespace VismaUserCore.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public ICollection<RolDTO> Rols { get; set; }
        public ICollection<DepartmentDTO> Departments { get; set; }
    }
}
