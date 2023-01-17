using System.Runtime.CompilerServices;
using DaveStore.Data;
using DaveStore.Models;
using DaveStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaveStore.Pages
{
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password,
                LoginViewModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Redirect("/Home/Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return Page();
        }
    }
}
