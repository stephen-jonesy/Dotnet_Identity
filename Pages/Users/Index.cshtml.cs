using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages.Users;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _logger = logger;
    }

        public IList<UserRolesViewModel> userList { get; set; } = new List<UserRolesViewModel>();

        public class UserRolesViewModel
        {
            public string UserName { get; set; }
            public string Id { get; set; }

            public string Email { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            Console.WriteLine(currentUser);
            var users = await _userManager.Users.ToListAsync();
            Console.WriteLine(users);

            foreach (IdentityUser user in users)
            {
                Console.WriteLine(user.Id);
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };

                userList.Add(urv);
            }
            return Page();
        }
}
