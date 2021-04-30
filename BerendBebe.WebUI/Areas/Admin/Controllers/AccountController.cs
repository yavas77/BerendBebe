using BerendBebe.Business.Abstract;
using BerendBebe.DTO.AccountDtos;
using BerendBebe.MvcHelpers.Enums;
using BerendBebe.MvcHelpers.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {


            if (!ModelState.IsValid)
                return View(registerDto);

            var result = await _accountService.RegisterAsync(registerDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home",  new { area = "Admin" }).ShowMessage(Status.Ok, "Başarılı", "Kullanıcı başarıyla eklendi");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


            return View(registerDto);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto signInDto)
        {
            if (!ModelState.IsValid)
                return View(signInDto);

            var identityResult = await _accountService.SignInAsync(signInDto);

            if (!identityResult.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!"); return View(signInDto);
            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
