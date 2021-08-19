using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OnlineShoppingApp.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace OnlineShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Index(int id=1)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewBag.userName = _userManager.GetUserName(User);

            var data = _context.Products
                .OrderBy(c => c.Id)
                .Skip((id - 1) * 20)
                .Take(20);
            int totalPage = (int) Math.Ceiling((decimal)_context.Products.Count() / 20) ;
            ViewBag.Total = totalPage;
            ViewBag.CurrentPage = id;
          
            return View(data);
        }
       
        public IActionResult SignUp()
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;


            return View();
        }

        //POST: Areas/Customar/SingUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp signup)
        {

            if (ModelState.IsValid)
            {
                _context.Add(signup);
                await _context.SaveChangesAsync();

                var User = new IdentityUser()
                {
                   
                    PhoneNumber = signup.PhoneNo,
                    Email = signup.Email,
                    UserName = signup.Name,
                    PasswordHash = signup.Password
                    
                };

                var result = await _userManager.CreateAsync(User, signup.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(User, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(signup);
        }

        public IActionResult Login()
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Invalid User Name or Passwor");
            }
            return View(login);
        }

        public IActionResult Necklece(int page = 1)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;


            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewBag.userName = _userManager.GetUserName(User);

            var data = _context.Products.Where(c => c.Category == "Necklece").OrderBy(c => c.Id).Skip((page - 1) * 20).Take(20);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 20);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            return View(data);
        }

        public IActionResult EarRing(int page = 1)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewBag.userName = _userManager.GetUserName(User);

            var data = _context.Products.Where(c => c.Category == "EarRing").OrderBy(c => c.Id).Skip((page - 1) * 20).Take(20);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 20);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            return View(data);
        }

        public IActionResult FingerRing(int page = 1)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewBag.userName = _userManager.GetUserName(User);

            var data = _context.Products.Where(c => c.Category == "FingerRing").OrderBy(c => c.Id).Skip((page - 1) * 20).Take(20);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 20);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            return View(data);
        }

        public IActionResult Locket(int page = 1)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewBag.userName = _userManager.GetUserName(User);

            var data = _context.Products.Where(c => c.Category == "Locket").OrderBy(c => c.Id).Skip((page - 1) * 20).Take(20);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 20);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            return View(data);
        }

        public IActionResult Payel(int page = 1)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;


            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewBag.userName = _userManager.GetUserName(User);

            var data = _context.Products.Where(c => c.Category == "Payel").OrderBy(c => c.Id).Skip((page - 1) * 20).Take(20);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 20);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            return View(data);
        }

        public IActionResult Bracelet(int page = 1)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;


            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewBag.userName = _userManager.GetUserName(User);

            var data = _context.Products.Where(c => c.Category == "Bracelet").OrderBy(c => c.Id).Skip((page - 1) * 20).Take(20);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 20);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            return View(data);
        }


        public IActionResult About()
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewData["Message"] = "Your application description page.";
            return View();
        }

        
        public IActionResult Contact()
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            ViewData["Message"] = "Your contact page.";
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult ProductDetails(int id)
        {
            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var ProductItem = _context.Products.FirstOrDefault(c => c.Id == id);

            Product vm = new Product();
            vm = ProductItem;

            ViewBag.data = ProductItem.ProductImagePath;
            ViewBag.ProductTitale = ProductItem.ProductTitle;

            if (_signInManager.IsSignedIn(User))
            {
                var UserAddressData = _context.SignUps.FirstOrDefault(c => c.Name == _userManager.GetUserName(User));
                ViewBag.UserAddressDataItem = UserAddressData.Address;
            }
            else
            {
                ViewBag.UserAddressDataItem = "Please login, then you will see your address.";
            }
            return View(vm);
        }

        public async Task<IActionResult> OderProduct(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                OrderProduct orderProduct = new OrderProduct();

                var UserDataList = _context.SignUps.FirstOrDefault(c => c.Name == _userManager.GetUserName(User));
                var ProductDataList = _context.Products.FirstOrDefault(c => c.Id == id);

                orderProduct.ProductId = ProductDataList.Id;
                orderProduct.ProductCode = ProductDataList.ProductCode;
                orderProduct.ProductImage = ProductDataList.ProductImagePath;
                orderProduct.ProductPrice = ProductDataList.ProductPrice;
                //orderProduct.TotalAmount = Product price er shathe delivery charge add hobe
                //orderProduct.ProductQty = Product quentity add hobe
                orderProduct.OrderDateTime = DateTime.Now;
                orderProduct.Status = "Processing";
                orderProduct.UserName = UserDataList.Name;
                orderProduct.UserPhoneNo = UserDataList.PhoneNo;
                orderProduct.UserAdress = UserDataList.Address;

                _context.OrderProducts.Add(orderProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction("UserProfile");
            }
            else
            {
                ViewBag.UserAddressDataItem = "Please login, then you will see your address.";
                return RedirectToAction("Login");
            }
        }

        public async Task<IActionResult> CheckOut(int cart_id, int P_id)
        {
            
            OrderProduct orderProduct = new OrderProduct();

            var UserDataList = _context.SignUps.FirstOrDefault(c => c.Name == _userManager.GetUserName(User));
            var ProductDataList = _context.Products.FirstOrDefault(c => c.Id == P_id);

            orderProduct.ProductId = ProductDataList.Id;
            orderProduct.ProductCode = ProductDataList.ProductCode;
            orderProduct.ProductImage = ProductDataList.ProductImagePath;
            orderProduct.ProductPrice = ProductDataList.ProductPrice;
            //orderProduct.TotalAmount = Product price er shathe delivery charge add hobe
            //orderProduct.ProductQty = Product quentity add hobe
            orderProduct.OrderDateTime = DateTime.Now;
            orderProduct.Status = "Processing";
            orderProduct.UserName = UserDataList.Name;
            orderProduct.UserPhoneNo = UserDataList.PhoneNo;
            orderProduct.UserAdress = UserDataList.Address;

            var cartproduct = await _context.AddToCarts.FindAsync(cart_id);
            _context.AddToCarts.Remove(cartproduct);

            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();
            

            return RedirectToAction("UserProfile");
          
            
        }

        public IActionResult UserProfile()
        {

            int TotalProductQnt = _context.Products.Where(c => c.Category == "Necklece").Count();
            ViewBag.NeckleceQnt = TotalProductQnt;

            int TotalEarRingQnt = _context.Products.Where(c => c.Category == "EarRing").Count();
            ViewBag.EarRingQnt = TotalEarRingQnt;

            int TotalFingerRingQnt = _context.Products.Where(c => c.Category == "FingerRing").Count();
            ViewBag.FingerRingQnt = TotalFingerRingQnt;

            int TotalLocketQnt = _context.Products.Where(c => c.Category == "Locket").Count();
            ViewBag.LocketeQnt = TotalLocketQnt;

            int TotalPayelQnt = _context.Products.Where(c => c.Category == "Payel").Count();
            ViewBag.PayeleQnt = TotalPayelQnt;

            int TotalBraceletQnt = _context.Products.Where(c => c.Category == "Bracelet").Count();
            ViewBag.BraceletQnt = TotalBraceletQnt;

            var AddToCardCount = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).Count();
            ViewBag.Count = AddToCardCount;

            if (_signInManager.IsSignedIn(User))
            {
                OrderProduct orderProduct = new OrderProduct();

                var UserDataList = _context.SignUps.FirstOrDefault(c => c.Name == _userManager.GetUserName(User));
                var UserOrderProductList = _context.OrderProducts.Where(c => c.UserName == _userManager.GetUserName(User)).OrderByDescending(c=>c.OrderDateTime).ToList();

                ViewBag.UserAddressDataItem = UserDataList.Address;
                ViewBag.UserPhoneNO = UserDataList.PhoneNo;
               // var UserWiseProductData = _context.OrderProducts.Where(c => c.UserName == ).OrderByDescending(c => c.OrderDateTime).ToList();
                ViewBag.UserProductList = UserOrderProductList;

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        public async Task<IActionResult> AddToCart(int id, int qnt)
        {
            if (_signInManager.IsSignedIn(User))
            {
                AddToCart addToCart = new AddToCart();
                var UserDataList = _context.SignUps.FirstOrDefault(c => c.Name == _userManager.GetUserName(User));
                var ProductData = _context.Products.Where(c => c.Id == id).ToList().FirstOrDefault();
                addToCart.ProductId = ProductData.Id;
                addToCart.ProductImage = ProductData.ProductImagePath;
                addToCart.ProductPrice = ProductData.ProductPrice;
                addToCart.ProductTitle = ProductData.ProductTitle;
                addToCart.UserName = UserDataList.Name;
                addToCart.Quntity = qnt;
               _context.AddToCarts.Add(addToCart);
               await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult ShowCart()
        {
            var UserCartDataList = _context.AddToCarts.Where(n => n.UserName == _userManager.GetUserName(User)).ToList();
            ViewBag.ProductData = UserCartDataList;

            return View();
        }

     
       
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.OrderProducts.FindAsync(id);
            _context.OrderProducts.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserProfile");
        }

        public async Task<IActionResult> AddToCartDelete(int id)
        {
            var product = await _context.AddToCarts.FindAsync(id);
            _context.AddToCarts.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowCart");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
