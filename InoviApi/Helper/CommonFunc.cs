using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace InoviWebApi.Helper
{
    public class CommonFunc
    {
        //private IConfiguration _configuration;
        //public CommonFunc(IConfiguration configuration)
        //{
        //   _configuration = configuration;
        //}
        //public Image Base64ToImage(string base64String)
        //{
        //    // Convert base 64 string to byte[]
        //    byte[] imageBytes = Convert.FromBase64String(base64String);
        //    // Convert byte[] to Image
        //    using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
        //    {
        //        Image image = Image.FromStream(ms, true);
        //        return image;
        //    }
        //}

        //public bool SaveImage(string ImgStr, string ImgName)
        //{
        //    Image image = SAWHelpers.Base64ToImage(ImgStr);
        //    String path = _configuration["filepath"] //Path
        //                                                                        //Check if directory exist
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
        //    }
        //    string imageName = ImgName + ".jpg";
        //    //set the image path
        //    string imgPath = Path.Combine(path, imageName);
        //    image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    return true;
        //}
    }
}
