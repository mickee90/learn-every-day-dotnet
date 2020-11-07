using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LearnEveryDay.Models
{
  public class UserRole : IdentityRole<Guid>
  {

  }
}
