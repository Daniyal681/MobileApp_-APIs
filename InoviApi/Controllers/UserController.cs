using DTO;
using DTO.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InoviDataTransferObject.DTO;
using Microsoft.AspNetCore.Authorization;

namespace InoviApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private IRepositoryWrapper _reposWrapper;
        private readonly IConfiguration _configuration;
        ResponseDTO resp = new ResponseDTO();
        public UserController(IRepositoryWrapper reposWrapper, IConfiguration configuration)
        {
            _reposWrapper = reposWrapper;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignupUser([FromBody] SignupDTO req)
        {
            try
            {
                if (req == null || string.IsNullOrWhiteSpace(req.UserEmail) || string.IsNullOrWhiteSpace(req.UserPassword) || string.IsNullOrWhiteSpace(req.Name) || string.IsNullOrWhiteSpace(req.Username))
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Values Cannot Be Null!");
                }
                else
                {
                    var result = await _reposWrapper.UserRepo.SignupUser(req);
                    if (result == false)
                    {
                        return StatusCode(StatusCodes.Status208AlreadyReported, "User With The Same Email Already Exist!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status201Created, "Signup Successfully!");
                    }
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Inserting Data!");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO req)
        {
            JWT jwt = new JWT();
            try
            {
                if (req == null || string.IsNullOrWhiteSpace(req.UserEmail) || string.IsNullOrWhiteSpace(req.UserPassword))
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Values Cannot Be Null!");
                }
                else
                {
                    var result = await _reposWrapper.UserRepo.LoginUser(req);
                    if (result == null)
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, "User Not Authenticate!");
                    }
                    else
                    {
                        jwt.Email = result.Email;
                        jwt.Name = result.Name;
                        jwt.UserID = result.UserID.ToString();
                        jwt.RoleID = result.RoleID.ToString();

                        jwt.Token = GenerateJWT(jwt);
                        return StatusCode(StatusCodes.Status200OK, jwt.Token);
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Login Data!");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendOTP([FromBody] SendOTPDTO req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.UserEmail))
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Values Cannot Be Null!");
                }
                else
                {
                    var result = await _reposWrapper.UserRepo.SendOTP(req);
                    if (result == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Email Not Exist!");
                    }
                    else
                    {
                        OTPDTO otp = new OTPDTO();
                        otp.To = result;
                        otp.From = _configuration["Gmail:Email"];
                        otp.Password = _configuration["Gmail:Password"];
                        otp.Otp = (new Random()).Next(1000,9999).ToString();
                        otp.MessageBody = "Your OTP is:" + otp.Otp;
                        otp.Subject = "Password Resetting Code";

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.To.Add(otp.To);
                        mailMessage.From = new MailAddress(otp.From);
                        mailMessage.Body = otp.MessageBody;
                        mailMessage.Subject = otp.Subject;

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(otp.From, otp.Password);
                        await smtp.SendMailAsync(mailMessage);

                        HttpContext.Session.SetString("OTP", otp.Otp);

                        return StatusCode(StatusCodes.Status200OK, "OTP Send Successfully! '" + otp.Otp);
                    }
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Sending Data!");
            }
        }

        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> ValidateOTP([FromBody] ValidateOTPDTO req)
        {
            try
            {
                if (req == null || string.IsNullOrWhiteSpace(req.UserEmail) || string.IsNullOrWhiteSpace(req.Otp))
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Values Cannot Be Null!");
                }
                else
                {
                    var result = await _reposWrapper.UserRepo.ValidateOTP(req);
                    if (result == false)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "User Not Found!");
                    }
                    else
                    {
                        if (req.Otp == HttpContext.Session.GetString("OTP"))
                        {
                            return StatusCode(StatusCodes.Status200OK, "OTP Matched!");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "OTP Not Matched!");
                        }
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Validating Data!");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordDTO req)
        {
            try
            {

                if (req == null || string.IsNullOrWhiteSpace(req.UserEmail) || string.IsNullOrWhiteSpace(req.Otp) || string.IsNullOrWhiteSpace(req.UserPassword) || string.IsNullOrWhiteSpace(req.UserConfirmPassword))
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Values Cannot Be Null!");
                }
                else
                {
                    if (req.UserPassword == req.UserConfirmPassword)
                    {
                        var result = await _reposWrapper.UserRepo.UpdateUserPassword(req);
                        if (result == false)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "User Not Found!");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status200OK, "Password Updated Successfully!");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Password Not Matched!");
                    }

                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Data!");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                int userIdClaim = Convert.ToInt32(User.FindFirst("UserID")?.Value);

                var result = await _reposWrapper.UserRepo.GetUserInfo(userIdClaim);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving user information.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving user information.");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ProfilePicDTO> UploadProfile(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return null;
            }

            byte[] fileBytes = Convert.FromBase64String(base64String);

            var attachmentLink = Guid.NewGuid().ToString() + ".png";

            // Create model for the uploaded file
            var uploadedFile = new ProfilePicDTO
            {
                ProfileLink = attachmentLink,
                ProfileBytes = fileBytes
            };

            // Upload the file and update the model
            uploadedFile = await _reposWrapper.UserRepo.UploadProfile(uploadedFile);

            return uploadedFile;
        }

        private string GenerateJWT(JWT req)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         new Claim(JwtRegisteredClaimNames.Email, req.Email),
         new Claim(JwtRegisteredClaimNames.Name, req.Name),
         new Claim(JwtRegisteredClaimNames.Sid, req.UserID),
         new Claim("UserID", req.UserID),

         new Claim(ClaimTypes.Role,req.RoleID),
         new Claim("Date", DateTime.Now.ToString()),
         };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audiance"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            req.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return req.Token;
        }
        //private JwtPayload DecodeJwt(string jwtToken)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

        //    return jsonToken?.Payload;
        //}
    }
}
