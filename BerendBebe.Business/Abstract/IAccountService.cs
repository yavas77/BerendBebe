using BerendBebe.DTO.AccountDtos;
using BerendBebe.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface IAccountService
    {
        Task<bool> UserExistsAsync(string userName);
        Task<SignInResult> SignInAsync(SignInDto signInDto);
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task LogOutAsync();
    }
}
