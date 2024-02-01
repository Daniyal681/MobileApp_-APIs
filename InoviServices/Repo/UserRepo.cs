using DTO.DTO;
using DTO.Interface;
using InoviDataAccessLayer.EntityModel;
using InoviDataTransferObject.DTO;
using Microsoft.EntityFrameworkCore;

namespace InoviServices.Repo
{
    public class UserRepo : IUserRepo
    {
        private QueryProContext _context;
        ResponseDTO resp = new ResponseDTO();
        public UserRepo(QueryProContext context)
        {
            _context = context;
        }

        public async Task<bool> SignupUser(SignupDTO req)
        {
            try
            {
                var isExist = _context.TblUsers.Where(x => x.EmailAddress == req.UserEmail).FirstOrDefault();
                if (isExist != null)
                {
                    return false;
                }
                else
                {
                    string password = req.UserPassword;
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);


                    TblUser tblReq = new TblUser
                    {
                        Name = req.Name,
                        Username = req.Username,
                        Password = hashedPassword,
                        EmailAddress = req.UserEmail,
                        UserRoleId = req.RoleID,
                        IsActive = req.IsActive,
                        CreatedOn = DateTime.Now
                    };
                    var UserEntity = _context.TblUsers.Add(tblReq).Entity;
                    _context.SaveChanges();

                    tblReq.CreatedBy = UserEntity.UserId;
                    _context.TblUsers.Update(tblReq);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JWT> LoginUser(LoginDTO req)
        {
            try
            {
                var isExist = _context.TblUsers.Where(x => x.EmailAddress == req.UserEmail).FirstOrDefault();
                if (isExist != null)
                {
                    string storedHashedPassword = isExist.Password;

                    if (BCrypt.Net.BCrypt.Verify(req.UserPassword, storedHashedPassword))
                    {
                        if (isExist.UserRoleId != null && isExist.UserRoleId != 0)
                        {
                            var result = new JWT
                            {
                                Email = isExist.EmailAddress,
                                Name = isExist.Name,
                                UserID = isExist.UserId.ToString(),
                                RoleID = isExist.UserRoleId.ToString()
                            };
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<string> SendOTP(SendOTPDTO req)
        {
            try
            {
                var isExist = _context.TblUsers.Where(x => x.EmailAddress == req.UserEmail).FirstOrDefault();
                if (isExist == null)
                {
                    return null;
                }
                else
                {
                    return isExist.EmailAddress;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> ValidateOTP(ValidateOTPDTO req)
        {
            try
            {
                var isExist = _context.TblUsers.Where(x => x.EmailAddress == req.UserEmail).FirstOrDefault();
                if (isExist != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> UpdateUserPassword(UpdateUserPasswordDTO req)
        {
            try
            {
                var isExist = _context.TblUsers.Where(x => x.EmailAddress == req.UserEmail).FirstOrDefault();
                if (isExist == null)
                {
                    return false;
                }
                else
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.UserPassword);
                    isExist.Password = hashedPassword;
                    isExist.ModifiedBy = isExist.UserId;
                    isExist.ModifiedOn = DateTime.Now;
                    _context.Update<TblUser>(isExist);
                    _context.SaveChanges();
                    return true;
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GetUserDTO> GetUserInfo(int UserID)
        {
            try
            {
                return await _context.TblUsers
                .Where(s => s.UserId == UserID)
                .Select(
                    s => new GetUserDTO
                    {
                        UserID = s.UserId,
                        RoleID = s.UserRoleId,
                        Name = s.Name,
                        Email = s.EmailAddress
                    })
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ProfilePicDTO> UploadProfile(ProfilePicDTO req)
        {
            try
            {
                var isExist = _context.TblProfiles.Where(x => x.ProfileId == req.ProfileId).FirstOrDefault();
                if (isExist != null)
                {
                    return null;
                }
                else
                {
                    TblProfile tblReq = new TblProfile
                    {
                        ProfileLink = req.ProfileLink,
                        IsActive = req.IsActive,
                        Profilebyte = req.ProfileBytes,
                        CreatedOn = DateTime.Now
                        //CreatedById = 
                    };
                    tblReq = _context.TblProfiles.Add(tblReq).Entity;
                    _context.SaveChanges();
                    req.ProfileId = tblReq.ProfileId;
                    return req;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public Task<ProfilePicDTO> UploadImage(ProfilePicDTO req)
        //{
        //    try
        //    {
        //        var isExist = _context.TblAttachments.Where(x => x.AttachmentLinkId == req.AttachmentLinkId).FirstOrDefault();
        //        if (isExist != null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            TblAttachment tblReq = new TblAttachment
        //            {
        //                Path = req.Path,
        //                Filename = req.FileName,
        //                AttachmentLink = req.AttachmentLink
        //            };
        //            tblReq = _context.TblAttachments.Add(tblReq).Entity;
        //            _context.SaveChanges();
        //            req.AttachmentLinkId = tblReq.AttachmentLinkId;
        //            return req;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }


}