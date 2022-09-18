using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnet_core_authentication_authorization.Pages
{
    [Authorize(Policy = "MustBelongToHRDepartment")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
