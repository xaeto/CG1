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

        private void greenValueImage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
