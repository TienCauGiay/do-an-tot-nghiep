using BE.DATN.BL.Common;
using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE.DATN.BL.Services
{
    public class AuthBL : IAuthBL
    {
        private readonly IUserDL _userDL;

        private readonly IConfiguration _configuration;

        public AuthBL (IUserDL userDL, IConfiguration configuration)
        {
            _userDL = userDL;
            _configuration = configuration;
        }
        public async Task<ReponseLogin> LoginAsync(user user)
        {
            var userName = BNTConvert.ConvertFromBase64(user.user_name);
            var passWord = BNTConvert.ConvertFromBase64(user.pass_word);

            try
            {
                // Kiểm tra xác thực người dùng bằng user_name và pass_word
                var userLogin = await _userDL.CheckLoginAsync(userName, passWord);
                if (userLogin == null)
                {
                    return new ReponseLogin(StatusCodes.Status400BadRequest,
                        "Tên đăng nhập hoặc mật khẩu không đúng.",
                        "",
                        EnumPermission.None);
                }

                // Tạo claims cho user
                var claims = new List<Claim>
                    {
                        new Claim("user_id", userLogin.user_id.ToString()),
                        new Claim("user_name", userLogin.user_name),
                        new Claim("role_code", userLogin.role_code.ToString())
                    };

                // Tạo key từ secret key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

                // Tạo signing credentials từ key
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

                // Tạo token
                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:ValidIssuer"],
                    audience: _configuration["JwtSettings:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationInMinutes"])),
                    signingCredentials: creds
                );

                return new ReponseLogin(StatusCodes.Status200OK,
                    "Đăng nhập thành công",
                    new JwtSecurityTokenHandler().WriteToken(token),
                    userLogin.role_code);
            }
            catch (Exception ex)
            {
                return new ReponseLogin();
            }
        }

    }
}
