using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models;

public class RoleModel
{
    public RoleModel()
    { }

    public int RoleId { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Role Name")]
    public string RoleName { get; set; } = null!;

    public int Ordering { get; set; }

    public virtual ICollection<UserModel> Users { get; set; } = new List<UserModel>();
}
