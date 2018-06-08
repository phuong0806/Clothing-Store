using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Common
{
    public class ImageHelper
    {
        public static List<ImageViewModel> loadListImage()
        {
            string[] filesindirectory = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/libs/Image/product"));
            List<ImageViewModel> images = new List<ImageViewModel>();
            foreach (string item in filesindirectory)
            {
                string path = String.Format("/libs/Image/product/{0}", System.IO.Path.GetFileName(item));
                string name = System.IO.Path.GetFileName(item);
                images.Add(new ImageViewModel(name, path));
            }
            return images;
        }

        public static string saveImage(HttpPostedFileBase file)
        {
            var path = "";
            var fileName = Path.GetFileName(file.FileName);
            var dir = "/libs/Image/product/";
            if (file.ContentLength > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".png")
                {
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~" + dir), fileName);
                    if (!File.Exists(path))
                    {
                        file.SaveAs(path);
                    }
                    return dir + fileName;
                }
            }
            return "";
        }

        public static List<ImageViewModel> DeleteImageByfilename(string filename)
        {
            string filepath = HttpContext.Current.Server.MapPath("~" + filename);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            return loadListImage();
        }
    }
}
