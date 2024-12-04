using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Web;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Project.Features.Service
{
    public class ImageReSize
    {
        public string path_image(string sub_folder = "")
        {
            sub_folder = !String.IsNullOrEmpty(sub_folder) ? "/" + sub_folder : "";
            var path = HttpContext.Current.Server.MapPath($"~/Images{sub_folder}");

            if (System.IO.Directory.Exists(path))
            {
                //Console.WriteLine("That path exists already.");
                return path;
            }
            else
            {
                System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
                return path;
            }
        }

        public string saveImage(string value64, string name_extension = "jpg", string sub_folder = "", int width = 1000, int height = 1000)
        {
            if (string.IsNullOrWhiteSpace(value64))
            {
                throw new Exception("Image invalid.");
            }

            var path = path_image(sub_folder);
            string guid = Guid.NewGuid().ToString();
            var con_img = ConvertBase64ToImage(value64);//convert 64 to image
            con_img = new ImageReSize().ResizeImage(con_img, width, height);//resize max width 1000
            sub_folder = !String.IsNullOrEmpty(sub_folder) ? sub_folder + "/" : "";
            string name_str = sub_folder + guid + "." + name_extension;
            string filepath = path + "\\" + guid + "." + name_extension;

            using (var b = new System.Drawing.Bitmap(con_img.Width, con_img.Height))
            {
                b.SetResolution(con_img.HorizontalResolution, con_img.VerticalResolution);

                using (var g = System.Drawing.Graphics.FromImage(b))
                {
                    if (name_extension == "png")
                        g.Clear(System.Drawing.Color.Empty);
                    else
                        g.Clear(System.Drawing.Color.White);
                    g.DrawImageUnscaled(con_img, 0, 0);
                }

                if (name_extension == "png")
                    b.Save(filepath, ImageFormat.Png);
                else
                    b.Save(filepath, ImageFormat.Jpeg);
            }

            return name_str;
        }

        public Image ConvertBase64ToImage(string img)
        {
            try
            {
                string[] split_img = img.Split(',');
                byte[] imageBytes;
                MemoryStream ms;

                var a = split_img.Length == 1 ? split_img[0] : split_img[1];

                imageBytes = Convert.FromBase64String(a);
                ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);

                return image;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Image ResizeImage(Image img, int wwidth = 420, int height = 420)
        {
            try
            {
                if (wwidth == 0 || height == 0)
                {
                    return img;
                }
                if (img.Width < wwidth)
                {
                    return img;
                }

                int resizeMaxWidth = wwidth;
                double resizeMaxHeight = height * img.Height / img.Width;

                Image newImage = new Bitmap(resizeMaxWidth, Convert.ToInt32(resizeMaxHeight));

                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(img, 0, 0, resizeMaxWidth, Convert.ToInt32(resizeMaxHeight));

                }

                return newImage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteFileMany(string[] vals) //value เป็น string ที่เก็บชื่อไฟล์ภาพ
        {
            try
            {
                foreach (var val in vals)
                {
                    string pathfile = HttpContext.Current.Server.MapPath("~/Images/" + val);
                    if (File.Exists(pathfile))
                    {
                        File.Delete(pathfile);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<string> ImageToBase64(string filename, string sub_folder = null)
        {
            var _filename = filename.Split(',');
            List<string> base64file = new List<string>();
            for (int i = 0; i < _filename.Count(); i++)
            {
                try
                {
                    using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/Images/" + _filename[i])))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string base64String = Convert.ToBase64String(imageBytes);

                            base64file.Insert(i, base64String);
                        }
                    }
                }
                catch
                {

                }
            }
            return base64file;
        }
    }
}
