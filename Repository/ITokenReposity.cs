using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repository
{
    public interface ITokenReposity
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
