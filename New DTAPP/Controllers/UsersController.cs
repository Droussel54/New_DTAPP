using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using New_DTAPP.Models;
using New_DTAPP.Data;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Repository;
using New_DTAPP.Security;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using Newtonsoft.Json;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            
            var user = await _userRepository.GetUserByIdAsync(id);
            RoleModel role = await _roleRepository.GetRoleByIdAsync(user.RoleId);
            user.Role = role;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            if (roles != null)
            {
                ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
            }
            return View();
        }

        //// POST: Users/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,FirstName,LastName,Title,Email,Disabled,RoleId")] Models.UserModel user)
        {
            
            if (ModelState.IsValid)
            {
                await _userRepository.AddUserAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        //// GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            var user = await _userRepository.GetUserByIdAsync(id);
            if (roles != null)
            {
                ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
            }

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("UserId,Username,FirstName,LastName,Title,Email,Disabled,RoleId,Role")] Models.UserModel user)
        {
            if (id == null)
            {
                return NotFound();
            }
            RoleModel role = await _roleRepository.GetRoleByIdAsync(user.RoleId);
            user.Role = role;
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.UpdateUserAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //TODO: Complete processing this concurrency exception.
                    //if ( = null)
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _userRepository.GetAllUsersAsync() == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _userRepository.GetAllUsersAsync() == null)
            {
                return Problem("Entity set 'NewDtappContext.Users'  is null.");
            }

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user != null)
            {
                await _userRepository.RemoveUserAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool? UserExists(int id)
        {
            return _userRepository.UserExistsAsync(id);
        }

        // Used on add user page to autocomplete potential usernames
        public List<string> FindUsersInAD(string userName)
        {
            List<string> potentialUsernames = _userRepository.FindUsersInAD(userName);
            return potentialUsernames;
        }

        public string FindUserInAD(string userName)
        {
            
            return _userRepository.FindUserInAD(userName);
        }
    }
}
