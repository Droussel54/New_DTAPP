using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models;

public class UserModel
{
    public UserModel()
    { }
    public int UserId { get; set; }
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(500)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(500)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    [Display(Name = "Title")]
    public string? Title { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(500)]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    public bool Disabled { get; set; }

    public string? Alias { get; set; }

    public int RoleId { get; set; }
    
    [ValidateNever]
    public virtual RoleModel Role { get; set; } = null!;


}
