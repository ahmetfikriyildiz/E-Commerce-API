using AutoMapper;
using ECom.Application.DTOs;
using ECom.Application.DTOs.IdentityDTO;
using ECom.Application.Services.Interfaces.Authentication;
using ECom.Application.Services.Interfaces.Logging;
using ECom.Domain.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(ITokenManagement tokenManagement,IUserManagement userManagement
        ,IRoleManagement roleManagement,IAppLogger<AuthenticationService> logger
        ,IMapper mapper) : IAuthenticationService
    {
        public Task<ServiceResponse> CreateUser(CreateUser user)
        {
            throw new NotImplementedException();
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
