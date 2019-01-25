﻿
using ImageCircle.Forms.Plugin.UWP;
using Xamarin.Forms.Platform.UWP;
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;

[assembly: ExportRenderer(typeof(CircleImage), typeof(ImageCircleRenderer))]
namespace ImageCircle.Forms.Plugin.UWP
{
    /// <summary>
    /// ImageCircle Implementation
    /// </summary>
    public class ImageCircleRenderer : ViewRenderer<Image, Ellipse>
    {

		ImageBrush imageBrush = null;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
							  /// <summary>
							  /// Used for registration with dependency service
							  /// </summary>
		public async static void Init()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
            var temp = DateTime.Now;
        }

        /// <summary>
        /// Register circle
        /// </summary>
        /// <param name="e"></param>
		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;

            var ellipse = new Ellipse();
            SetNativeControl(ellipse);

        }

        Xamarin.Forms.ImageSource file = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;



            var min = Math.Min(Element.Width, Element.Height);
            if (min / 2.0f <= 0)
                return;

            try
            {

                Control.Width = min;
                Control.Height = min;

                

                var force = e.PropertyName == VisualElement.XProperty.PropertyName ||
                    e.PropertyName == VisualElement.YProperty.PropertyName ||
                    e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                    e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                    e.PropertyName == VisualElement.ScaleProperty.PropertyName ||
                    e.PropertyName == VisualElement.TranslationXProperty.PropertyName ||
                    e.PropertyName == VisualElement.TranslationYProperty.PropertyName ||
                    e.PropertyName == VisualElement.RotationYProperty.PropertyName ||
                    e.PropertyName == VisualElement.RotationXProperty.PropertyName ||
                    e.PropertyName == VisualElement.RotationProperty.PropertyName ||
                    e.PropertyName == CircleImage.BorderThicknessProperty.PropertyName ||
                    e.PropertyName == CircleImage.BorderColorProperty.PropertyName ||
                    e.PropertyName == CircleImage.FillColorProperty.PropertyName ||
                    e.PropertyName == VisualElement.AnchorXProperty.PropertyName ||
                    e.PropertyName == VisualElement.AnchorYProperty.PropertyName;


                //already set
                if (file == Element.Source && !force)
                    return;

				// Fill background color
				var color = ((CircleImage)Element).FillColor;
				Control.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(
					(byte)(color.A * 255),
					(byte)(color.R * 255),
					(byte)(color.G * 255),
					(byte)(color.B * 255)));

				// Fill stroke
				color = ((CircleImage)Element).BorderColor;
				Control.StrokeThickness = ((CircleImage)Element).BorderThickness;
				Control.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(
				   (byte)(color.A * 255),
				   (byte)(color.R * 255),
				   (byte)(color.G * 255),
				   (byte)(color.B * 255)));

				if (file == Element.Source && imageBrush != null)
				{
					Control.Fill = imageBrush;
					return;
				}

				BitmapImage bitmapImage = null;

                file = Element.Source;

                // Handle file images
                if (file is FileImageSource)
                {
                    var fi = Element.Source as FileImageSource;
					
					var file = fi.File;
					bitmapImage = new BitmapImage(new Uri("ms-appx:///" + file));
					
					/*var myFile = System.IO.Path.Combine(Package.Current.InstalledLocation.Path, fi.File);
					
					var myFolder = await StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(myFile));

                    using (var s = await myFolder.OpenStreamForReadAsync(System.IO.Path.GetFileName(myFile)))
                    {
                        var memStream = new MemoryStream();
                        await s.CopyToAsync(memStream);
                        memStream.Position = 0;
                        bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(memStream.AsRandomAccessStream());
                    }*/
				}
                else if (file is UriImageSource)
                {
                    bitmapImage = new BitmapImage((Element.Source as UriImageSource).Uri);
                }
                else if (file is StreamImageSource)
                {
                    var handler = new StreamImageSourceHandler();
                    var imageSource = await handler.LoadImageAsync(file);

                    if (imageSource != null)
                    {
                        Control.Fill = imageBrush =  new ImageBrush
                        {
                            ImageSource = imageSource,
                            Stretch = Stretch.UniformToFill,
                        };
                    }
                    return;
                }

                if (bitmapImage != null)
                {
					Control.Fill = imageBrush = new ImageBrush
                    {
                        ImageSource = bitmapImage,
                        Stretch = Stretch.UniformToFill,
                    };
                }

            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image, falling back to background color.");
            }
        }
    }
}