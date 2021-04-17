using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.BL.Implementations
{
    public class AuthBl : IAuthBl
    {
        private readonly UserManager<Auth> _userManager;
        private readonly IMapper _mapper;

        public AuthBl(UserManager<Auth> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task<Guid?> SignUp(AuthDto authDto, string rol)
        {
            var auth = _mapper.Map<Auth>(authDto);
            
            var result = await _userManager.CreateAsync(auth, authDto.Password);

            if (!result.Succeeded)
            {
                return null;
            }
            
            await _userManager.AddToRoleAsync(auth, rol);

            return auth.Id;
        }

        public async Task<Guid?> SignIn(AuthDto authDto)
        {
            var auth = await _userManager.Users.FirstOrDefaultAsync(u =>
                u.UserName == authDto.UserName || u.Email == authDto.Email);

            if (auth == null)
            {
                return null;
            }

            var passIsValid = await _userManager.CheckPasswordAsync(auth, authDto.Password);

            if (!passIsValid)
            {
                return null;
            }

            return auth.Id;
        }
    }
}