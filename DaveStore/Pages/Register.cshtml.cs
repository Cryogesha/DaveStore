using DaveStore.Models;
using DaveStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DaveStore.Pages
{
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();
            ApplicationUser user = new()
            {
                UserName = RegisterViewModel.Email,
                Email = RegisterViewModel.Email,
                PhoneNumber = RegisterViewModel.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, RegisterViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Redirect("/Home/Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid register attempt. Please, check your data");
            return Page();
        }
    }
}
