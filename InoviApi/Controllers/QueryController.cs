using DTO;
using DTO.DTO;
using InoviDataTransferObject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace InoviWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class QueryController : ControllerBase
    {

        private IRepositoryWrapper _reposWrapper;
        private IConfiguration _configuration;
        public QueryController(IRepositoryWrapper reposWrapper, IConfiguration configuration)
        {
            _reposWrapper = reposWrapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuery([FromBody] AddQueryDTO req)
        {
            try
            {
                if (req == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Values Cannot Be Null!");
                }
                else
                {
                    req.UserID = Convert.ToInt32(User.FindFirst("UserID")?.Value);

                    req.AttachmentIds = await UploadBase(req.Attachments);

                    var result = await _reposWrapper.QueryRepo.AddQuery(req);
                    if (result == true)
                    {
                        return StatusCode(StatusCodes.Status200OK, "Query Added Successfully!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Null Data!");
                    }
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Adding Data!");
            }
        }
        [HttpPost]
        [Authorize(Roles = "1,4")]
        public async Task<IActionResult> UpdateQuery([FromBody] UpdateStatus req)
        {
            try
            {
                req.UserID = Convert.ToInt32(User.FindFirst("UserID")?.Value);
                var result = await _reposWrapper.QueryRepo.UpdateQuery(req);
                if (result == true)
                {
                    return StatusCode(StatusCodes.Status200OK, "Query Updated Successfully!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Null Data!");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Data!");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetQueryList()
        {
            try
            {
                int userIdClaim = Convert.ToInt32(User.FindFirst("UserID")?.Value);
                int userRoleClaim = Convert.ToInt32(User.FindFirst(ClaimTypes.Role)?.Value);

                var result = await _reposWrapper.QueryRepo.GetQueryList(new GetUserIDAndRoleID { RoleID = userRoleClaim, UserID = userIdClaim });
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Null Data!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Data!");
            }

        }
        [HttpPost]
        public async Task<IActionResult> GetQueryById(int queryId)
        {
            try
            {
                var result = await _reposWrapper.QueryRepo.GetQueryById(queryId);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Query not found!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Data!");
            }
        }

        [Obsolete]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImages([FromBody] List<string> base64Strings)
        {
            try
            {
                List<AttachmentLinkList> uploadedFiles = new List<AttachmentLinkList>();

                foreach (var base64String in base64Strings)
                {
                    if (string.IsNullOrEmpty(base64String))
                    {
                        return BadRequest("Invalid file content");
                    }
                    else
                    {
                        // Decode the Base64 string to byte array
                        byte[] fileBytes = Convert.FromBase64String(base64String);

                        // Process each file, generate attachment link ID
                        var attachmentLink = Guid.NewGuid().ToString();
                        var filePath = _configuration["filepath"] + attachmentLink + ".png";

                        // Save the byte array to the file
                        System.IO.File.WriteAllBytes(filePath, fileBytes);

                        // Create model for the uploaded file
                        var uploadedFile = new AttachmentLinkList
                        {
                            FileName = "yourfilename.ext", // Provide the original filename here
                            AttachmentLink = attachmentLink,
                            Path = filePath
                        };

                        uploadedFiles.Add(uploadedFile);

                        var result = await _reposWrapper.QueryRepo.UploadImage(uploadedFile);

                        if (result != null)
                        {
                            return StatusCode(StatusCodes.Status200OK, result);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Null Data!");
                        }
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<List<AttachmentLinkList>> UploadBase(List<string> base64Strings)
        {
            List<AttachmentLinkList> lst = new List<AttachmentLinkList>();

            foreach (var base64String in base64Strings)
            {
                if (!string.IsNullOrEmpty(base64String))
                {
                    byte[] fileBytes = Convert.FromBase64String(base64String);

                    var attachmentLink = Guid.NewGuid().ToString() + ".png";

                    var uploadedFile = new AttachmentLinkList
                    {
                        AttachmentLink = attachmentLink,
                        FileBytes = fileBytes 
                    };

                    uploadedFile = await _reposWrapper.QueryRepo.UploadImage(uploadedFile);

                    lst.Add(uploadedFile);
                }
            }

            return lst;
        }

        //private async Task<List<AttachmentLinkList>> UploadBase(List<string> base64Strings)
        //{
        //    List<AttachmentLinkList> lst = new List<AttachmentLinkList>();

        //    foreach (var base64String in base64Strings)
        //    {
        //        if (!string.IsNullOrEmpty(base64String))
        //        {
        //            // Decode the Base64 string to byte array
        //            byte[] fileBytes = Convert.FromBase64String(base64String);

        //            // Process each file, generate attachment link ID
        //            var attachmentLink = Guid.NewGuid().ToString() + ".png";
        //            var filePath = _configuration["filepath"] + "\\" + attachmentLink;

        //            // Save the byte array to the file
        //            System.IO.File.WriteAllBytes(filePath, fileBytes);

        //            // Create model for the uploaded file
        //            var uploadedFile = new AttachmentLinkList
        //            {
        //                FileName = attachmentLink, // Provide the original filename here
        //                AttachmentLink = attachmentLink,
        //                Path = filePath
        //            };


        //            uploadedFile = await _reposWrapper.QueryRepo.UploadImage(uploadedFile);

        //            lst.Add(uploadedFile);

        //        }
        //    }

        //    return lst;
        //}

        //public IActionResult UploadImages([FromForm] List<IFormFile> files)
        //{
        //    try
        //    {
        //        List<AttachmentLinkList> uploadedFiles = new List<AttachmentLinkList>();

        //        foreach (var file in files)
        //        {
        //            if (file == null || file.Length == 0)
        //                return BadRequest("Invalid file");

        //            // Process each file, generate attachment link ID
        //            var attachmentLink = Guid.NewGuid().ToString();
        //            var filePath = _configuration["filepath"] + attachmentLink + "_" + file.FileName;

        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            // Create model for the uploaded file
        //            var uploadedFile = new AttachmentLinkList
        //            {
        //                FileName = file.FileName,
        //                AttachmentLink = attachmentLink
        //            };

        //            uploadedFiles.Add(uploadedFile);
        //        }

        //        return Ok(uploadedFiles);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


    }
}
