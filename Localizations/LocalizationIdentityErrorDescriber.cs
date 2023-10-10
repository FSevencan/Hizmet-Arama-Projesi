using Microsoft.AspNetCore.Identity;

namespace Proje.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
 

        public override IdentityError DuplicateEmail(string email)
        {

            return new() { Code = "DuplicateEmail", Description = $"*{email} daha önce başka bir kullanıcı tarafından alınmıştır" };

        }
 

    }
}
