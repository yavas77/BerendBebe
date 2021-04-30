using BerendBebe.Business.Abstract;
using BerendBebe.DTO.AccountDtos;
using BerendBebe.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _loginManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountManager(UserManager<User> userManager, SignInManager<User> loginManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PasswordHash = registerDto.Password
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            return result;
        }

        public async Task<SignInResult> SignInAsync(SignInDto signInDto)
        {
            var user = await _userManager.FindByNameAsync(signInDto.UserName);

            var result = await _loginManager.PasswordSignInAsync(signInDto.UserName, signInDto.Password, signInDto.IsPersistent, false);
            return result;
        }

        public Task<bool> UserExistsAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task LogOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
