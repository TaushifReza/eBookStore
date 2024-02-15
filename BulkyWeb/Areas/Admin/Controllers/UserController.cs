using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagment(string userId)
        {
            string RoleID = _db.UserRoles.FirstOrDefault(u => u.UserId == userId).RoleId;

            RoleManagmentVM RoleVM = new RoleManagmentVM
            {
                ApplicationUser = _db.ApplicationUsers.Include(u=>u.Company).FirstOrDefault(u=>u.Id == userId),
                RoleList = _db.Roles.Select(i=>new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _db.Companies.Select(i=>new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            RoleVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(u => u.Id == RoleID).Name;
            return View(RoleVM);
        }

        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVm)
        {
            string RoleID = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagmentVm.ApplicationUser.Id).RoleId;

            string oldRole = _db.Roles.FirstOrDefault(u => u.Id == RoleID).Name;

            if (!(roleManagmentVm.ApplicationUser.Role == oldRole))
            {
                //a role was updated
                ApplicationUser applicationUser =
                    _db.ApplicationUsers.FirstOrDefault(u => u.Id == roleManagmentVm.ApplicationUser.Id);
                if (roleManagmentVm.ApplicationUser.Role == SD.Role_Company)
                {
                    applicationUser.CompanyId = roleManagmentVm.ApplicationUser.CompanyId;
                }

                if (oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }

                _db.SaveChanges();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagmentVm.ApplicationUser.Role).GetAwaiter().GetResult();
            }


            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser>? objUserList = _db.ApplicationUsers.Include(u=>u.Company).ToList();

            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in objUserList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                if (user.Company == null)
                {
                    user.Company = new() {Name = ""};
                }
            }
            return Json(new { data = objUserList });
        }
        
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb.LockoutEnd!=null&&objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked and need to unlocked them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
