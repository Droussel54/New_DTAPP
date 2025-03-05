using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int Ordering { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
