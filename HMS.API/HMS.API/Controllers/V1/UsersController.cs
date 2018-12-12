using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HMS.Business.Interfaces;
using HMS.Common.Dtos.User;
using HMS.Common.Responses.User;
using Microsoft.Extensions.Logging;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserBusiness _userBusiness;

        public UsersController(ILogger<UsersController> logger,
            IUserBusiness userBusiness)
        {
            _logger = logger;
            _userBusiness = userBusiness;
        }

        [HttpPost]
        public async Task<LoginResponse> Login(LoginDto model)
        {
            _logger.LogWarning("Tes t sdkfjdkfjf");
            return await _userBusiness.Login(model);
        }
    }
}
