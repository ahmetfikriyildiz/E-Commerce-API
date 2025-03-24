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

        public Task<LoginResponse> LoginUser(LoginUser user)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> ReviveToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
