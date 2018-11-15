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

            //var hashed = Convert.ToBase64String(HashPasswordV2(AspNetUsers.PasswordHash, _rng));

            //AspNetUsers.NormalizedUserName = AspNetUsers.UserName.ToUpper();
            //AspNetUsers.Email = AspNetUsers.UserName;
            //AspNetUsers.NormalizedEmail = AspNetUsers.UserName.ToUpper();
            //AspNetUsers.EmailConfirmed = false;
            //AspNetUsers.PasswordHash = hashed;
            //AspNetUsers.SecurityStamp = Guid.NewGuid().ToString("N");
            //AspNetUsers.ConcurrencyStamp = Guid.NewGuid().ToString();
            //AspNetUsers.PhoneNumber = null;
            //AspNetUsers.PhoneNumberConfirmed = false;
            //AspNetUsers.TwoFactorEnabled = false;
            //AspNetUsers.LockoutEnabled = true;
            //AspNetUsers.AccessFailedCount = 0;

            //_context.Users.Add(AspNetUsers);
            //await _context.SaveChangesAsync();

            //var user = _context.Users.Where(a => a.UserName == AspNetUsers.UserName).FirstOrDefault();
            //var Role = _context.Roles.Where(r => r.Name == "User").FirstOrDefault();

            //var role = new IdentityUserRole<string>
            //{
            //    UserId = user.Id,
            //    RoleId = Role.Id
            //};
            //var userexist = _context.UserRoles.Where(a => a.UserId == user.Id).ToList();
            //if (userexist.Count == 0)
            //{
            //    _context.UserRoles.Add(role);
            //    await _context.SaveChangesAsync();
            //}
            return Page();
        }

        //private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
        //{
        //    const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
        //    const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
        //    const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
        //    const int SaltSize = 128 / 8; // 128 bits

        //    // Produce a version 2 text hash.
        //    byte[] salt = new byte[SaltSize];
        //    rng = RandomNumberGenerator.Create();
        //    rng.GetBytes(salt);
        //    byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

        //    var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
        //    outputBytes[0] = 0x00; // format marker
        //    Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        //    Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
        //    return outputBytes;
        //}
    }
}