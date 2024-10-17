using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void drawTriangle(object sender, RoutedEventArgs e)
        {
            int width = 500, height = 500;
            WriteableBitmap bitmap = new WriteableBitmap(width, height, 0, 0, PixelFormats.Rgb24, null);

            // width * height * 3, weil man bei Rgb24 8bit fuer jeden Farbkanal hat.
            byte[] pixels = new byte[width * height * 3];

            for (int i = 0; i < width * height; ++i)
            {
                int r = i * 3;
                int g = r + 1;
                int b = r + 2;

                pixels[r] = 0;
                pixels[g] = 0;
                pixels[b] = 0xff;
            }

            bitmap.CopyPixels(pixels, bitmap.BackBufferStride, 0);
            triangle.Source = bitmap;
        }

        private void updateImageColors()
        {
            int width = 267, height = 267;
            byte[] pixels = new byte[width * height * 4];
            int offset = 0;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < y + 1; ++x)
                {
                    int index = x * 4 + offset;
                    pixels[index + 0] = (byte)blueSlider.Value;
                    pixels[index + 1] = (byte)greenSlider.Value;
                    pixels[index + 2] = (byte)redSlider.Value;
                    pixels[index + 3] = 255;
                }

                offset += width * 4;
            }

            WriteableBitmap bitmap = new(width, height, 0, 0, PixelFormats.Bgr32, null);
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, bitmap.BackBufferStride, 0);
            triangle.Source = bitmap;
        }

        private void drawTriangle_onClick(object sender, RoutedEventArgs e)
        {
            updateImageColors();
        }

        private void BlueValueImage_Loaded(object sender, RoutedEventArgs e)
        {
            WriteableBitmap bitmap = new WriteableBitmap(18, 18, 0, 0, PixelFormats.Bgr32, null);
            (int width, int height) = (bitmap.PixelWidth, bitmap.PixelHeight);

            byte[] pixels = new byte[width * height * 4];

            byte sliderValue = (byte)blueSlider.Value;
            blueValue.Content = sliderValue;

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;

                pixels[index + 0] = sliderValue;
                pixels[index + 1] = 0;
                pixels[index + 2] = 0;
                pixels[index + 3] = 0;
            }

            bitmap.CopyPixels(pixels, bitmap.BackBufferStride, 0);
            blueValueImage.Source = bitmap;
        }

        private void RedValueImage_Loaded(object sender, RoutedEventArgs e)
        {
            WriteableBitmap bitmap = new WriteableBitmap(18, 18, 0, 0, PixelFormats.Bgr32, null);
            (int width, int height) = (bitmap.PixelWidth, bitmap.PixelHeight);

            byte[] pixels = new byte[width * height * 4];

            byte sliderValue = (byte)redSlider.Value;
            redValue.Content = sliderValue;

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;

                pixels[index + 0] = 0;
                pixels[index + 1] = 0;
                pixels[index + 2] = sliderValue;
                pixels[index + 3] = 0;
            }

            bitmap.CopyPixels(pixels, bitmap.BackBufferStride, 0);
            redValueImage.Source = bitmap;
        }

        private void GreenValueImage_Loaded(object sender, RoutedEventArgs e)
        {
            WriteableBitmap bitmap = new WriteableBitmap(18, 18, 0, 0, PixelFormats.Bgr32, null);
            (int width, int height) = (bitmap.PixelWidth, bitmap.PixelHeight);

            byte[] pixels = new byte[width * height * 4];

            byte sliderValue = (byte)greenSlider.Value;
            greenValue.Content = sliderValue;

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;

                pixels[index + 0] = 0;
                pixels[index + 1] = sliderValue;
                pixels[index + 2] = 0;
                pixels[index + 3] = 0;
            }

            bitmap.CopyPixels(pixels, bitmap.BackBufferStride, 0);
            greenValueImage.Source = bitmap;
        }

        private void updateOutputColor()
        {
            byte red = (byte)redSlider.Value;
            byte blue = (byte)blueSlider.Value;
            byte green = (byte)greenSlider.Value;

            WriteableBitmap bitmap = new WriteableBitmap(169, 18, 0, 0, PixelFormats.Bgr32, null);
            (int width, int height) = ((int)bitmap.Width, (int)bitmap.Height);


            byte[] pixels = new byte[width * height * 4];

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                pixels[index + 0] = blue;
                pixels[index + 1] = green;
                pixels[index + 2] = red;
                pixels[index + 3] = 255; // alpha
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, bitmap.BackBufferStride, 0);
            outputColor.Source = bitmap;
        }

        private void BlueSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (blueValueImage == null)
                return;

            WriteableBitmap bitmap = new WriteableBitmap(18, 18, 0, 0, PixelFormats.Bgr32, null);
            (int width, int height) = ((int)bitmap.Width, (int)bitmap.Height);

            byte[] pixels = new byte[width * height * 4];

            byte sliderValue = (byte)e.NewValue;
            blueValue.Content = sliderValue;
            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                pixels[index + 0] = sliderValue; // blue
                pixels[index + 1] = 0; // green
                pixels[index + 2] = 0; // red
                pixels[index + 3] = 0; // alpha
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, bitmap.BackBufferStride, 0);
            blueValueImage.Source = bitmap;

            updateOutputColor();
        }

        private void GreenSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (greenValueImage == null)
                return;

            WriteableBitmap bitmap = new WriteableBitmap(18, 18, 0, 0, PixelFormats.Bgr32, null);
            (int width, int height) = ((int)bitmap.Width, (int)bitmap.Height);

            byte[] pixels = new byte[width * height * 4];

            byte sliderValue = (byte)e.NewValue;
            greenValue.Content = sliderValue;

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                pixels[index + 0] = 0; // blue
                pixels[index + 1] = sliderValue; // green
                pixels[index + 2] = 0; // red
                pixels[index + 3] = 0; // alpha
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, bitmap.BackBufferStride, 0);
            greenValueImage.Source = bitmap;

            updateOutputColor();
        }

        private void RedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (redValueImage == null)
                return;

            WriteableBitmap bitmap = new WriteableBitmap(18, 18, 0, 0, PixelFormats.Bgr32, null);
            (int width, int height) = ((int)bitmap.Width, (int)bitmap.Height);

            byte[] pixels = new byte[width * height * 4];

            byte sliderValue = (byte)e.NewValue;
            redValue.Content = sliderValue;

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                pixels[index + 0] = 0; // blue
                pixels[index + 1] = 0; // green
                pixels[index + 2] = sliderValue; // red
                pixels[index + 3] = 0; // alpha
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, bitmap.BackBufferStride, 0);
            redValueImage.Source = bitmap;

            updateOutputColor();
        }

        private void LoadImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;

            byte[] pixels = new byte[width * height * 4];
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                pixels[i * 4 + 0] = (byte)(pixels[i * 4 + 0]);
                pixels[i * 4 + 1] = (byte)(pixels[i * 4 + 1]);
                pixels[i * 4 + 2] = (byte)(pixels[i * 4 + 2]);
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);

            image.Source = bitmap;
        }

        private void invert_onClick(object sender, RoutedEventArgs e)
        {
            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;

            byte[] pixels = new byte[width * height * 4];
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                pixels[i * 4 + 0] = (byte)(1 - pixels[i * 4 + 0]);
                pixels[i * 4 + 1] = (byte)(1 - pixels[i * 4 + 1]);
                pixels[i * 4 + 2] = (byte)(1 - pixels[i * 4 + 2]);
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);

            image.Source = bitmap;
        }

        private void channelChanged()
        {
            bool redEnabled = redChannelEnabled.IsChecked == true;
            bool greenEnabled = greenChannelEnabled.IsChecked == true;
            bool blueEnabled = blueChannelEnabled.IsChecked == true;

            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;

            byte[] pixels = new byte[width * height * 4];
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                pixels[i * 4 + 0] = blueEnabled ? (byte)(pixels[i * 4 + 0]) : (byte)0;
                pixels[i * 4 + 1] = greenEnabled ? (byte)(pixels[i * 4 + 1]) : (byte)0;
                pixels[i * 4 + 2] = redEnabled ? (byte)(pixels[i * 4 + 2]) : (byte)0;
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);

            image.Source = bitmap;
        }

        private void turnGray_onClick(object sender, RoutedEventArgs e)
        {
            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;
            Int32Rect rect = new Int32Rect(0, 0, width, height);

            byte[] pixels = new byte[width * height * 4];
            byte[] grayPixels = new byte[width * height]; //8 bit
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                byte blue = pixels[index];       //blue
                byte green = pixels[index + 1];  // green
                byte red = pixels[index + 2];    // red

                // formel
                byte grayValue = (byte)(0.2126 * red + 0.7152 * green + 0.0722 * blue);
                grayPixels[i] = grayValue;
            }

            WriteableBitmap grayBitmap = new WriteableBitmap(width, height, bitmap.DpiX, bitmap.DpiY, PixelFormats.Gray8, null);

            grayBitmap.WritePixels(rect, grayPixels, width, 0);

            image.Source = grayBitmap;
        }

        private void invertGrayScaleImage(object sender, RoutedEventArgs e)
        {
            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;

            byte[] pixels = new byte[width * height * 4];
            byte[] grayPixels = new byte[width * height];

            bitmap.CopyPixels(pixels, stride, 0);
            Int32Rect rect = new Int32Rect(0, 0, width, height);

            for (int i = 0; i < width * height; i++)
            {
                int index = i * 4;
                int b = 1 - pixels[index + 0];
                int g = 1 - pixels[index + 1];
                int r = 1 - pixels[index + 2];

                int gray = (byte)(0.2126 * r + 0.7152 * g + 0.0722 * b);
                grayPixels[i] = (byte)gray;
            }

            WriteableBitmap grayBitmap = new WriteableBitmap(width, height, bitmap.DpiX, bitmap.DpiY, PixelFormats.Gray8, null);
            grayBitmap.WritePixels(rect, grayPixels, width, 0);
            image.Source = grayBitmap;
        }

        private void redCheckBoxClicked(object sender, RoutedEventArgs e)
        {
            channelChanged();
        }

        private void greenCheckBoxClicked(object sender, RoutedEventArgs e)
        {
            channelChanged();
        }

        private void blueCheckBoxClicked(object sender, RoutedEventArgs e)
        {
            channelChanged();
        }

        private void grayScaleThresholdValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int threshold = (int)e.NewValue;
            if (this.thresholdValue != null)
                this.thresholdValue.Content = threshold;
            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;
            Int32Rect rect = new Int32Rect(0, 0, width, height);

            byte[] pixels = new byte[width * height * 4];
            byte[] grayPixels = new byte[width * height]; //8 bit
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                byte blue = pixels[index];       //blue
                byte green = pixels[index + 1];  // green
                byte red = pixels[index + 2];    // red

                // formel
                byte grayValue = (byte)(0.2126 * red + 0.7152 * green + 0.0722 * blue);
                grayPixels[i] = grayValue >= threshold ? (byte)255 : (byte)0;
            }

            WriteableBitmap grayBitmap = new WriteableBitmap(width, height, bitmap.DpiX, bitmap.DpiY, PixelFormats.Gray8, null);

            grayBitmap.WritePixels(rect, grayPixels, width, 0);

            image.Source = grayBitmap;

        }

        private void grayScaleGammaCorrection(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double gammaFactor = e.NewValue;
            if (this.gammaCorrectionLabel != null)
                this.gammaCorrectionLabel.Content = Math.Round(gammaFactor, 2);

            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;
            Int32Rect rect = new Int32Rect(0, 0, width, height);

            byte[] pixels = new byte[width * height * 4];
            byte[] grayPixels = new byte[width * height]; //8 bit
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                byte blue = pixels[index];       //blue
                byte green = pixels[index + 1];  // green
                byte red = pixels[index + 2];    // red

                // formel
                byte grayValue = (byte)(0.2126 * red + 0.7152 * green + 0.0722 * blue);
                double t = Math.Pow(grayValue / 255.0, 1 / gammaFactor);
                grayPixels[i] = (byte)(t * 255);
            }

            WriteableBitmap grayBitmap = new WriteableBitmap(width, height, bitmap.DpiX, bitmap.DpiY, PixelFormats.Gray8, null);
            grayBitmap.WritePixels(rect, grayPixels, width, 0);

            image.Source = grayBitmap;
        }

        private void paletteColor()
        {
            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;
            Int32Rect rect = new Int32Rect(0, 0, width, height);

            byte[] pixels = new byte[width * height * 4];
            byte[] grayPixels = new byte[width * height]; //8 bit
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                byte blue = pixels[index];       //blue
                byte green = pixels[index + 1];  // green
                byte red = pixels[index + 2];    // red

                // formel
                byte grayValue = (byte)(0.2126 * red + 0.7152 * green + 0.0722 * blue);
                grayPixels[i] = grayValue;
            }

            var colors = new List<Color>();
            for(byte i = 0; i < 255; ++i)
            {
                colors.Add(Color.FromRgb((byte)(255 - i), 0, i));
            }

            WriteableBitmap grayBitmap = new WriteableBitmap(width, height, 0, 0, PixelFormats.Indexed8, 
                new BitmapPalette(colors)
            );

            grayBitmap.WritePixels(rect, grayPixels, width, 0);
            image.Source = grayBitmap;
        }

        private void paletteColorGamma(double gamma)
        {
            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;
            Int32Rect rect = new Int32Rect(0, 0, width, height);

            byte[] pixels = new byte[width * height * 4];
            byte[] grayPixels = new byte[width * height]; //8 bit
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                int index = i * 4;
                byte blue = pixels[index];       //blue
                byte green = pixels[index + 1];  // green
                byte red = pixels[index + 2];    // red

                // formel
                byte grayValue = (byte)(0.2126 * red + 0.7152 * green + 0.0722 * blue);
                grayPixels[i] = grayValue;
            }

            var colors = new List<Color>();
            for (byte i = 0; i < 255; ++i)
            {
                byte value = (byte)(Math.Pow(i / 255.0, 1 / gamma) * 255);
                colors.Add(Color.FromRgb(value, value, value));
            }

            WriteableBitmap grayBitmap = new WriteableBitmap(width, height, 0, 0, PixelFormats.Indexed8,
                new BitmapPalette(colors)
            );

            grayBitmap.WritePixels(rect, grayPixels, width, 0);
            image.Source = grayBitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            paletteColor();
        }

        private void paletteGammaCorrectionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            paletteColorGamma(e.NewValue);
        }

        private void paletteGammaColorChange()
        {
            if (this.paletteGammaColorCorrectionSliderRed == null 
                || this.paletteGammaColorCorrectionSliderGreen == null 
                || this.paletteGammaColorCorrectionSliderBlue == null)
            {
                return;
            }

            double redGammaValue = this.paletteGammaColorCorrectionSliderRed.Value;
            double greenGammaValue = this.paletteGammaColorCorrectionSliderGreen.Value;
            double blueGammaValue = this.paletteGammaColorCorrectionSliderBlue.Value;

            Uri imagePath = new Uri("./Campus.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(imagePath);
            WriteableBitmap bitmap = new WriteableBitmap(img);

            int width = bitmap.PixelWidth, height = bitmap.PixelHeight;
            int stride = bitmap.BackBufferStride;

            byte[] pixels = new byte[width * height * 4];
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < width * height; ++i)
            {
                byte blue_pixel = (byte)(pixels[i * 4 + 0]);
                byte green_pixel = (byte)(pixels[i * 4 + 1]);
                byte red_pixel = (byte)(pixels[i * 4 + 2]);

                byte blue = (byte)(Math.Pow(blue_pixel / 255.0, 1/blueGammaValue) * 255);
                byte green = (byte)(Math.Pow(green_pixel / 255.0, 1/greenGammaValue) * 255);
                byte red = (byte)(Math.Pow(red_pixel / 255.0, 1/redGammaValue) * 255);

                pixels[i * 4 + 0] = blue;
                pixels[i * 4 + 1] = green;
                pixels[i * 4 + 2] = red;
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);

            image.Source = bitmap;

        }

        private void paletteGammaColorCorrectionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            paletteGammaColorChange();
        }
    }
}
