using System.Collections.Generic;

namespace VismaUserCore.DTOs
{
    public class RolDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<PermissionDTO> Permissions { get; set; }
    }
}
