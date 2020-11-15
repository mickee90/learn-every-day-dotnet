using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Requests
{
  // Validation set in /Validators
  public class UpdatePasswordRequest
  {
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }
  }
}