using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContactManager.Pages.Users
{

    public class DetailsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DetailsModel> _logger;
        private string user;

        public string userName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public IEnumerable<string> roles { get; set; }

        public DetailsModel(ILogger<DetailsModel> logger, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;

        }

        public async Task<IActionResult> OnGetAsync(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            roles = await _userManager.GetRolesAsync(user);
            
            userName = user.UserName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            var user = await _userManager.FindByIdAsync("6db94584-eeea-4137-80d1-5b1c8e76b23e");

            await _userManager.AddToRoleAsync(user, "ContactManagers");

            return Page();

        }

    }
}