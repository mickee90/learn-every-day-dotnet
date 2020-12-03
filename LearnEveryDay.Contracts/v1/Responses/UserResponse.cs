using System;
using Newtonsoft.Json;

namespace LearnEveryDay.Contracts.v1.Responses
{
  public class UserResponse
  {
    public Guid Id { get; set; }

    public int UserTypeId { get; }

    [JsonProperty("username")]
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
