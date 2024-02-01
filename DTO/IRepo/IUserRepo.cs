using DTO.DTO;
using InoviDataTransferObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Interface
{
    public interface IUserRepo
    {
        Task<bool> SignupUser(SignupDTO req);
        Task<JWT> LoginUser(LoginDTO req);
        Task<string> SendOTP(SendOTPDTO req);
        Task<bool> ValidateOTP(ValidateOTPDTO req);
        Task<bool> UpdateUserPassword(UpdateUserPasswordDTO req);
        Task<GetUserDTO> GetUserInfo(int UserID);
        Task<ProfilePicDTO> UploadProfile(ProfilePicDTO req);
    }
}
