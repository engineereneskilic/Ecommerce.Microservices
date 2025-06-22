using Duende.IdentityModel;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;

namespace FreeCourse.Services.Identity.Services
{
    public class IdentityResourceOwnerPasswordvalidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<IdentityUser> _userManager;
        public IdentityResourceOwnerPasswordvalidator(UserManager<IdentityUser> userManager)
        {
           _userManager = userManager;
        }


        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);

            if (existUser == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });

                context.Result.CustomResponse = errors;

                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(existUser,context.Password);

           if(passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });

                context.Result.CustomResponse = errors;

                return;

            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(),OidcConstants.AuthenticationMethods.Password); 
            


        }
    }
}
