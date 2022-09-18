using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnet_core_authentication_authorization.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        public void OnGet()
        {
            //this.Credential = new Credential { UserName = "admin"};
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if(Credential.UserName == "admin" && Credential.Password == "password")
            {
                //Create the Security Context
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("Manager", "true"),
                    new Claim("EmploymentDate", "2022-03-01")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new CliamsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };
               await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

                return RedirectToPage("/index");
            }

            return Page();
        }
    }

    internal class CliamsPrincipal : ClaimsPrincipal
    {
        public CliamsPrincipal(IIdentity identity) : base(identity)
        {
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
