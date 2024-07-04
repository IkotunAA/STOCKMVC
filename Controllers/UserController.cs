using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using STOCKMVC.Models;
using STOCKMVC.Services.Interfaces;
using System.Security.Claims;
using STOCKMVC.Entities;
using STOCKMVC.Context;
using STOCKMVC.Constants;
using STOCKMVC.Enum;
using Microsoft.AspNetCore.Authorization;
using STOCKMVC.Repositories.Interfaces;

namespace STOCKMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly StockDbContext _context;

        public UserController(IIdentityService identityService, IUserRepository userRepository, StockDbContext context)
        {
            _identityService = identityService;
            _userRepository = userRepository;
            _context = context;
        }
        public IActionResult Register()
        {
            var selectedRoles = _identityService.GetRoles();
            var roles = selectedRoles.Where(x => x.Name != RoleConstant.Admin);
            var model = new RegisterViewModel
            {
                Roles = roles.Select(role => new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var registerModel = new RegisterModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Role = model.SelectedRole,
                Gender = model.Gender
            };

            var response = _identityService.Register(registerModel);
            if (response.Status)
            {
                ViewBag.Message = response.Message = "Registeration Successful";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = response.Message = "Registeration not successful";
                return View();
            }

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.FindByUserName(model.UserName) ?? _userRepository.FindByEmail(model.UserName);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

                var roles = user.UserRoles.Select(ur => ur.Role.Name);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                //TempData = "Login Successfully";

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Allow refresh
                    AllowRefresh = true,

                    // Refreshing the authentication session should be allowed.
                    IsPersistent = true,

                    // The time at which the authentication ticket was issued.
                    IssuedUtc = DateTime.UtcNow,

                    // The time at which the authentication ticket expires.
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                if (roles.Contains(RoleConstant.Customer))
                {
                    return RedirectToAction("Dashboard", "Customer");
                }
                else if (roles.Contains(RoleConstant.BusinessOwner))
                {
                    return RedirectToAction("Dashboard", "BusinessOwner");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

