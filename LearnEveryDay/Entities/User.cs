using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LearnEveryDay.Entities
{
  public class User : IdentityUser<Guid>
  {

    [Required]
    [DefaultValue(2)]
    public int UserTypeId { get; set; }

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }

    [MaxLength(255)]
    public string Address { get; set; }

    [MaxLength(255)]
    public string ZipCode { get; set; }

    [MaxLength(255)]
    public string City { get; set; }

    [MaxLength(255)]
    public string Phone { get; set; }

    [Required]
    public Boolean Disabled { get; set; }

    [Required]
    public Boolean Banned { get; set; }

    [Required]
    [DefaultValue(1)]
    public int CountryId { get; set; }

    [MaxLength(255)]
    public string Avatar { get; set; }
  }
}
