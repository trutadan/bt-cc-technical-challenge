using Microsoft.AspNetCore.Mvc;
using technical_challenge.Exceptions;
using technical_challenge.Model.DTO;
using technical_challenge.Repository.Interface;
using technical_challenge.Service;

namespace technical_challenge.Controller
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JWTLoginService _jwtService;
        private readonly OTPLoginService _otpService;

        public UserController(IUserRepository repository, JWTLoginService jwtService, OTPLoginService otpService)
        {
            _repository = repository;
            _jwtService = jwtService;
            _otpService = otpService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterCredentials user)
        {
            try
            {
                var addedUser = _repository.AddUser(user);
                return Created("success", addedUser);
            }
            catch (RepositoryException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCredentials dto)
        {
            try
            {
                var user = await _repository.GetUserByUsername(dto.Username);

                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                {
                    return BadRequest(new { message = "Invalid credentials!" });
                }

                var otp = _otpService.GenerateTotp(user.Id.ToString(), DateTime.UtcNow);

                // Store user ID in a cookie
                Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });

                return Ok(new
                {
                    message = "success"
                });
            }
            catch (RepositoryException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet("get-otp")]
        public async Task<IActionResult> GetOTP()
        {
            var userId = Request.Cookies["userId"];

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "Invalid username!" });
            }

            try
            {
                var user = await _repository.GetUserById(int.Parse(userId));

                var newOtp = _otpService.GenerateTotp(user.Id.ToString(), DateTime.UtcNow);

                return Ok(new
                {
                    otp = newOtp
                });
            }
            catch (RepositoryException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP(OTPCode code)
        {
            // Retrieve user ID from the cookie
            var userId = Request.Cookies["userId"];

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "Invalid username!" });
            }

            try
            {
                var user = await _repository.GetUserById(int.Parse(userId));

                if (_otpService.IsTotpCodeExpired(user.Id.ToString(), DateTime.UtcNow, code.otp))
                {
                    return BadRequest(new { message = "Invalid OTP provided!" });
                }

                var (jwt, expiration) = _jwtService.Generate(user.Id);

                Response.Cookies.Append("jwt", jwt, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });

                return Ok(new
                {
                    message = "success",
                    username = user.Username,
                    expiration = DateTime.Today.AddDays(1)
                });
            }
            catch (RepositoryException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetUsername()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = await _repository.GetUserById(userId);

                return Ok(new
                {
                    message = "success",
                    username = user.Username
                });
            }
            catch (RepositoryException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            };

            Response.Cookies.Delete("jwt", cookieOptions);

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
