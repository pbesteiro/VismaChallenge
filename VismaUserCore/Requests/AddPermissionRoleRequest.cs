using System;
using System.Collections.Generic;
using System.Text;

namespace VismaUserCore.Requests
{
    public class AddPermissionRoleRequest
    {
        public int? RolId { get; set; }
        public List<int> PermissionsId { get; set; }
    }
}
