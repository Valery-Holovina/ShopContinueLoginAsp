using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop_P41.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
            
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Role _role)
        {
            var role_exists = await _roleManager.RoleExistsAsync(_role.Name);
            if (role_exists)
            {
                return BadRequest("The role name exists ...");
            }
            var newRole = new IdentityRole(_role.Name);
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Assing", "Role");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        public IActionResult Assign()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Assign(string UserId, string RoleId)
        {
            if(!string.IsNullOrEmpty(UserId) || !string.IsNullOrEmpty(RoleId))
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    return BadRequest("User id is incorrect ...");
                }
                var role_exists = await _roleManager.FindByIdAsync(RoleId);
                if (role_exists == null)
                {
                    return BadRequest("Role name is incorrect ...");
                }
                var result = await _userManager.AddToRoleAsync(user, role_exists.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest("Error model state ...");
        }
    }
}