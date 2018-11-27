using ConflictRenewal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace ConflictRenewal.Pages.Shared
{
    public class RegisterModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        private SignInManager<IdentityUser> _signManager;
        private UserManager<IdentityUser> _userManager;

        public RegisterModel(ConflictRenewal.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public IdentityUser AspNetUsers { get; set; }

        public RoleEnum rollEnum { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var users = new IdentityUser { UserName = AspNetUsers.UserName, Email = AspNetUsers.UserName, NormalizedEmail = AspNetUsers.UserName.ToUpper() };
            var result = await _userManager.CreateAsync(users, AspNetUsers.PasswordHash);

            if (result.Succeeded)
            {
                var user = _context.Users.Where(a => a.UserName == AspNetUsers.UserName).FirstOrDefault();
                var Role = _context.Roles.Where(r => r.Name == RoleEnum.User.ToString()).FirstOrDefault();

                var role = new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = Role.Id
                };
                var userexist = _context.UserRoles.Where(a => a.UserId == user.Id).ToList();
                if (userexist.Count == 0)
                {
                    _context.UserRoles.Add(role);
                    await _context.SaveChangesAsync();
                }

                await _signManager.SignInAsync(users, false);
                return RedirectToPage("/Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}