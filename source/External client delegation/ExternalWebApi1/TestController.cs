using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using IdentityModel.Client;

namespace ExternalWebApi1
{
  [Route("test")]
  public class TestController : ApiController
  {
    public string Get()
    {
      TokenClient tokenClient = new TokenClient(
        "http://localhost:44333/connect/token",
        "ExternalWebApi1",
        "4B79A70F-3919-435C-B46C-571068F7AF37"
        );

      var caller = User as ClaimsPrincipal;

      HttpClient client = new HttpClient();
      client.SetBearerToken(caller.FindFirst("token").Value);

      var result = client.GetStringAsync("http://localhost:44339/test").Result;

      return result;
    }
  }
}