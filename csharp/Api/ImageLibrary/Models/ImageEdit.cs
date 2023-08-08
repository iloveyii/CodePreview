using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageLibrary.Models
{
    public class ImageEdit
    {
        private Image image;
        private Image processedImage;
        private string imagePath = "";
        public ImageEdit(string imagePath) 
        {
            this.imagePath = imagePath;
            if(imagePath == null)
            {
                throw new ArgumentNullException("Image path is null");
            } else if(File.Exists(imagePath))
            {
                this.image = Image.FromFile(imagePath);
                this.processedImage = this.image;
            } else 
            { 
                throw new FileNotFoundException(imagePath); 
            }
        } 

        public ImageEdit(Image image) 
        {
            this.image = image;
        }

        public ImageEdit CropCenter( Size size )
        {
            image = Crop(size);
            return this;
        }

        public ImageEdit CropCenter(int width, int height )
        {
            Size size = new Size(width, height);
            image = Crop(size);
            return this;
        }

        private Image Crop(Size destinationSize)
        {
            var originalWidth = image.Width;
            var originalHeight = image.Height;

            //how many units are there to make the original length
            var hRatio = (float)originalHeight / destinationSize.Height;
            var wRatio = (float)originalWidth / destinationSize.Width;

            //get the shorter side
            var ratio = Math.Min(hRatio, wRatio);

            var hScale = Convert.ToInt32(destinationSize.Height * ratio);
            var wScale = Convert.ToInt32(destinationSize.Width * ratio);

            //start cropping from the center
            var startX = (originalWidth - wScale) / 2;
            var startY = (originalHeight - hScale) / 2;

            //crop the image from the specified location and size
            var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

            //the future size of the image
            var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

            //fill-in the whole bitmap
            var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            //generate the new image
            using (var g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }

            return bitmap;
        }

        public ImageEdit Resize( Size size )
        {
            ResizeThis(size);
            return this;
        }

        public ImageEdit Resize(int width, int height)
        {
            Size size = new Size(width, height);
            ResizeThis(size);
            return this;
        }

        private Image ResizeThis(Size size)
        {
            //Get the image current width  
            int sourceWidth = image.Width;
            //Get the image current height  
            int sourceHeight = image.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(image, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        public bool SaveAs( string path )
        {
            return true;
        }

        public string SaveAsIncremented(bool save = true)
        {
            if(imagePath == string.Empty)
            {
                throw new Exception("Image path is not given, only Image is given");
            }

            var newFilePath = GetIncrement(imagePath, 1);
            if(save && newFilePath != string.Empty)
                image.Save(newFilePath);

            return newFilePath;
        }

        private string GetIncrement(string _path, int inc)
        {
            var path = Path.GetDirectoryName(_path);
            var fileName = Path.GetFileName(_path);
            var fileInfo = new FileInfo(_path);
            var fileExt = fileInfo.Extension;
            var fileNameWithNoExt = fileName.Replace(fileExt, "");
            var newFilePath = Path.Combine(path, fileNameWithNoExt + "-" + inc.ToString() + fileExt);

            if(File.Exists(newFilePath))
            {   
                var newFilePathWithNoInc = Path.Combine(path, fileName);
                newFilePath = GetIncrement(newFilePathWithNoInc, ++inc);
            }

            return newFilePath;
        }
    }
}
