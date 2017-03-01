using DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Created by Michael Takrama, Natacha Ilunga 
    /// Created on 2/24/2017
    /// 
    /// Static class containing extension methods for WpfPresentationLayer Assembly
    /// </summary>
    public static class WpfExtensionMethods
    {
        /// <summary>
        /// Image Save File Path
        /// </summary>
        public static readonly string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + "images" + @"\";

        /// <summary>
        /// Created by Michael Takrama on 2/24/2017
        /// 
        /// Extension method to saves images from database into temporary image folder.
        /// </summary>
        /// <param name="product">Product Object</param>
        public static void SaveImageToTempFile(this BrowseProductViewModel product)
        {
            SaveToTempFile(product);
        }

        /// <summary>
        /// Created by Michael Takrama, Natacha Ilunga
        /// Created on 2/24/2017
        /// 
        /// Saves images from database into temporary image folder.
        /// </summary>
        /// <param name="product">Product Object</param>
        public static void SaveToTempFile(BrowseProductViewModel product)
        {
            //Create folder location
            System.IO.Directory.CreateDirectory(FilePath);

            if (product.Image_Binary == null)
                return;
            if (product.Image_Binary.Length == 0)
                return;

            var image = new BitmapImage();
            var path = FilePath + product.ProductId + ".jpg";

            using (
            var mem = new MemoryStream(product.Image_Binary))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
                image.Freeze();
            }

            ////Saving image to file
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));



            using (
                var fileStream =
                    new FileStream(path, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
            

        }

        /// <summary>
        /// Created by Michael Takrama, Natacha Ilunga
        /// Created on 2/24/2017
        /// 
        /// Extension Method for Main Window to Dispose All Images Loaded
        /// </summary>
        /// <param name="mainWindow"></param>
        public static void DisposeImages(this MainWindow mainWindow)
        {

            var directoryInfo = new DirectoryInfo(FilePath);

            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            foreach (var dir in directoryInfo.GetDirectories())
            {
                dir.Delete(true);
            }

        }


    }
}
