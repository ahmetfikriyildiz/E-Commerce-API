using AutoMapper;
using ECom.Application.DTOs;
using ECom.Application.DTOs.IdentityDTO;
using ECom.Application.Services.Interfaces.Authentication;
using ECom.Application.Services.Interfaces.Logging;
using ECom.Application.Validations;
using ECom.Domain.Entities.Identity;
using ECom.Domain.Interfaces.Authentication;
using FluentValidation;


namespace ECom.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(ITokenManagement tokenManagement,IUserManagement userManagement
        ,IRoleManagement roleManagement,IAppLogger<AuthenticationService> logger
        ,IMapper mapper , IValidator<CreateUser> createUserValidator,IValidator<LoginUser> loginUserValidator
        ,IValidationService validationService) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
           var _validationResult = await validationService.ValidateAsync(user, createUserValidator);
            if (!_validationResult.Success)
            {
                return _validationResult;
            }
            var appUser = mapper.Map<AppUser>(user);
            appUser.UserName = user.Email;
            appUser.PasswordHash = user.Password;
            var result = await userManagement.CreateUser(appUser);
            if (!result)
            {
                return new ServiceResponse { Message = "Email Adress might be already in use or an unknown error occured.", Success = false };
            }
            var _user = await userManagement.GetUserByEmail(user.Email);
            var users = await userManagement.GetAllUsers();
            bool assignedResult = await roleManagement.AddUserToRole(_user! , users!.Count() > 1 ? "User" : "Admin" );

            if (!assignedResult)
            {
                int removeUserResult = await userManagement.RemoveUserByEmail(_user!.Email!);
                if (removeUserResult <= 0)
                {
                    logger.LogError(new Exception($"Account with email as {_user.Email} failed to be remove as a result of role assigning issue")
                        , "Account Could not be assigned to a role");
                    return new ServiceResponse { Message = "An error occured in creating Account. Please try again.", Success = false };
                }
            }
            return new ServiceResponse { Message = "Account created successfully", Success = true };
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validationResult = await validationService.ValidateAsync(user, loginUserValidator);
            if (!_validationResult.Success)
            {
                return new LoginResponse { Message = _validationResult.Message};
            }
            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.PasswordHash = user.Password;

            bool loginResult = await userManagement.LoginUser(mappedModel);
            if (!loginResult)
            {
                return new LoginResponse { Message = "Invalid Email or Password" };
            }
            var _user = await userManagement.GetUserByEmail(user.Email);
            var claims = await userManagement.GetUserClaims(user.Email);
            
            var jwtToken = tokenManagement.GenerateToken(claims);
            var refreshToken = tokenManagement.GetRefreshToken();

            var addRefreshTokenResult = await tokenManagement.AddRefreshToken(_user!.Id, refreshToken);
            return addRefreshTokenResult <= 0 ? new LoginResponse { Message = "An error occured in login. Please try again." } :
                new LoginResponse {Success = true , Token = jwtToken, RefreshToken = refreshToken };
        }

        public async Task<LoginResponse> RefreshToken(string refreshToken)
        {
            bool validateTokenResult = tokenManagement.ValidateRefreshToken(refreshToken).Result;
            if (!validateTokenResult)
            {
                return new LoginResponse { Message = "Invalid Refresh Token" };
            }
            var userId = await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            var user = await userManagement.GetUserById(userId);
            var claims = await userManagement.GetUserClaims(user!.Email!);
            var jwtToken = tokenManagement.GenerateToken(claims);
            var newRefreshToken = tokenManagement.GetRefreshToken();
            await tokenManagement.UpdateRefreshToken(userId, newRefreshToken);
            return new LoginResponse { Success = true, Token = jwtToken, RefreshToken = newRefreshToken };

        }
    }
}
