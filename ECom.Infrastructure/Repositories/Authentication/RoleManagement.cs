﻿using ECom.Domain.Entities.Identity;
using ECom.Domain.Interfaces.Authentication;
using ECom.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<AppUser> userManager) : IRoleManagement
    {
        public async Task<bool> AddUserToRole(AppUser user, string roleName) =>
            (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
        }
    }
    public class UserManagement(IRoleManagement roleManagement,UserManager<AppUser> userManager,AppDbContext context) : IUserManagement
    {
        public async Task<bool> CreateUser(AppUser user)
        {
            var _user= await GetUserByEmail(user.Email!);
            if (_user != null) return false;

            return (await userManager.CreateAsync(user!, user!.PasswordHash!)).Succeeded;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers() => await context.Users.ToListAsync();

        public async Task<AppUser> GetUserByEmail(string email) => await userManager.FindByEmailAsync(email);

        public Task<AppUser> GetUserById(string id) => userManager.FindByIdAsync(id);

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var _user = await GetUserByEmail(email);
            string? roleName = await roleManagement.GetUserRole(_user!.Email!);

            List<Claim> claims = [
                new Claim("FullName", _user!.FullName),
                new Claim(ClaimTypes.NameIdentifier, _user!.Id),
                new Claim(ClaimTypes.Email, _user!.Email!),
                new Claim(ClaimTypes.Role, roleName!)
                ];
            return claims;
        }

        public async Task<bool> LoginUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user is null) return false;

            string? roleName = await roleManagement.GetUserRole(_user!.Email!);
            if (string.IsNullOrEmpty(roleName)) return false;

            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(_=>_.Email == email);
            context.Users.Remove(user);
            return await context.SaveChangesAsync();
        }
    }
    public class TokenManagement : ITokenManagement
    {
        public Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public string GetRefreshToken()
        {
            throw new NotImplementedException();
        }

        public List<Claim> GetUserClaims(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
