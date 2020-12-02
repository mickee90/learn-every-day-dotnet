using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Requests
{
  public class UpdateUserRequest
  {
    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string Email { get; set; }

    public string Address { get; set; }

    public string ZipCode { get; set; }

    public string City { get; set; }

    public string Phone { get; set; }
  }
}