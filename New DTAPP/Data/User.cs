using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Title { get; set; }

    public string Email { get; set; } = null!;

    public bool Disabled { get; set; }

    public int RoleId { get; set; }

    public string? Alias { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Transfer> TransferCompletedUsers { get; set; } = new List<Transfer>();

    public virtual ICollection<Transfer> TransferReviewedUsers { get; set; } = new List<Transfer>();

    public virtual ICollection<Spill> SpillCompletedUsers { get; set; } = new List<Spill>();
    public virtual ICollection<Spill> SpillReviewedUsers { get; set; } = new List<Spill>();
}
