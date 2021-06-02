using System;
using System.Collections.Generic;
using System.Text;

namespace VismaUserCore.Requests
{
    public class AddRolsUserRequest
    {
        public AddRolsUserRequest()
        {
            RolsId = new List<int>();
        }
        public int? UserId { get; set; }
        public List<int> RolsId { get; set; }
    }
}
