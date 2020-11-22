using Newtonsoft.Json;

namespace LearnEveryDay.Contracts.v1.Requests
{
  public class UserLoginRequest
  {
    [JsonProperty("username")]
    public string UserName { get; set; }

    public string Password { get; set; }
  }
}