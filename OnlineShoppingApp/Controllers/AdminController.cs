using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp.Data;
using OnlineShoppingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShoppingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _he;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, IHostingEnvironment he, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _he = he;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Admin
        public IActionResult Index(int id=1)
        {
            var data = _context.Products
               .OrderBy(c => c.Id)
               .Skip((id - 1) * 20)
               .Take(20);
            int totalPage = (int)Math.Ceiling((decimal)_context.Products.Count() / 20);
            ViewBag.Total = totalPage;
            ViewBag.CurrentPage = id;

            return View(data);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewBag.User = _userManager.Users;
            ViewBag.Role = _roleManager.Roles;

            return View();
        }

        //Role Create
        [HttpPost]
        public async Task<IActionResult> CreateRole(string userrole)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(userrole))
            {
                if (await _roleManager.RoleExistsAsync(userrole))
                {
                    msg = "Role [" + userrole + "] already exist!!";
                }
                else
                {
                    IdentityRole r = new IdentityRole(userrole);

                    await _roleManager.CreateAsync(r);
                    msg = "Role [" + userrole + "] Save Successfully!!";
                }
            }
            else
            {
                msg = "Please Enter a Valid Role.";
            }
            ViewBag.msg = msg;
            return RedirectToAction("Index");
        }

        //Role Assign for Authorize
        [HttpPost]
        public async Task<IActionResult> RoleAssign(string userdata, string userrole)
        {
            string msg = "";

            if (!string.IsNullOrEmpty(userdata) && !string.IsNullOrEmpty(userrole))
            {
                IdentityUser u = await _userManager.FindByNameAsync(userdata);

                if (u != null)
                {
                    if (await _roleManager.RoleExistsAsync(userrole))
                    {
                        await _userManager.AddToRoleAsync(u, userrole);
                        msg = "Role has been assigned to user.";
                    }
                    else
                    {
                        msg = "User already Exist.";
                    }
                }
                else
                {
                    msg = "User is not found.";
                }
            }
            else
            {
                msg = "Please Enter a valid data!!";
            }

            TempData["msg"] = msg;

            return RedirectToAction("Index");
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,ProductCode,ProductTitle,ProductImagePath,ProductPrice,ProductAskPrice")]*/ Product product, IFormFile file)
        {
            /*wait Upload(file);*/

            string path = "";
            string filename = "";
            try
            {
                if (file.Length > 0)
                {
                    filename = Path.GetFileNameWithoutExtension(file.FileName);
                    string extention = Path.GetExtension(file.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
                    path = Path.GetFullPath(Path.Combine(_he.WebRootPath, "Images"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            product.ProductImagePath = filename;
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Category,ProductCode,ProductTitle,ProductImagePath,ProductPrice,ProductAskPrice,Status")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(product);
        //}

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,ProductCode,ProductTitle,ProductImagePath,ProductPrice,ProductAskPrice,Status")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
