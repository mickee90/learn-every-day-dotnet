using System;

namespace LearnEveryDay.Dtos.User
{
  public class UserReadDto
  {
    public UserReadDto(LearnEveryDay.Models.User user, string token)
    {
      Id = user.Id;
      UserTypeId = user.UserTypeId;
      UserName = user.UserName;
      FirstName = user.FirstName;
      LastName = user.LastName;
      Address = user.Address;
      ZipCode = user.ZipCode;
      City = user.City;
      Email = user.Email;
      Phone = user.Phone;
      CountryId = user.CountryId;
      Avatar = user.Avatar;
      Token = token;
    }

    public Guid Id { get; set; }

    public int UserTypeId { get; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string ZipCode { get; set; }

    public string City { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public int CountryId { get; set; }

    public string Avatar { get; set; }

    public string Token { get; set; }
  }
}
