using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContactManager.Pages.Users
{
    [Authorize(Roles = "ContactAdministrators")]
    public class DetailsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;  

        private readonly ILogger<DetailsModel> _logger;

        public string userName { get; set; }

        public string Email { get; set; }
        
        public string Id { get; set; }
        [BindProperty]
        public String NewRoleType { get; set; }
        public IEnumerable<string> roles { get; set; }
        
        public DetailsModel(ILogger<DetailsModel> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;  

            _logger = logger;

        }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            ViewData["roles"] = _roleManager.Roles.ToList();  

            var user = await _userManager.FindByIdAsync(id);
            roles = await _userManager.GetRolesAsync(user);
            
            Id = id;
            userName = user.UserName;


            return Page();
        }

        public async Task<IActionResult> OnPostAddRoleAsync(string Id)
        {
            if(NewRoleType == null) {
                return RedirectToPage("./Index");

            } else {
                var user = await _userManager.FindByIdAsync(Id);
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());

                await _userManager.AddToRoleAsync(user, NewRoleType);

                return RedirectToPage("./Index");
            }

        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());

            return RedirectToPage("./Index");
        }


    }
}