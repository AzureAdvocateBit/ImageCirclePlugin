## Circle Image Control Plugin for Xamarin.Forms

Simple but elegant way of display circle images in your Xamarin.Forms projects

#### Setup
* Available on NuGet: https://www.nuget.org/packages/Xam.Plugins.Forms.ImageCircle [![NuGet](https://img.shields.io/nuget/v/Xam.Plugins.Forms.ImageCircle.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Plugins.Forms.ImageCircle/)
* Install into your PCL project and Client projects.
* 
Build status: [![Build status](https://ci.appveyor.com/api/projects/status/igydt07o7nonlk3u?svg=true)](https://ci.appveyor.com/project/JamesMontemagno/imagecircleplugin)


In your iOS, Android, and Windows projects call:

```
Xamarin.Forms.Init();//platform specific init
ImageCircleRenderer.Init();
```

You must do this AFTER you call Xamarin.Forms.Init();

**Platform Support**

|Platform|Version|
| -------------------  | :------------------: |
|Xamarin.iOS|iOS 7+|
|Xamarin.Android|API 14+|
|Windows 10 UWP|10+|

#### Usage
Instead of using an Image simply use a CircleImage instead!

You **MUST** set the width & height requests to the same value and you will want to use AspectFill. Here is a sample:
```
new CircleImage
{
  BorderColor = Color.White,
  BorderThickness = 3,
  HeightRequest = 150,
  WidthRequest = 150,
  Aspect = Aspect.AspectFill,
  HorizontalOptions = LayoutOptions.Center,
  Source = UriImageSource.FromUri(new Uri("http://upload.wikimedia.org/wikipedia/commons/5/55/Tamarin_portrait.JPG"))
}
```

**XAML:**

First add the xmlns namespace:
```xml
xmlns:controls="clr-namespace:ImageCircle.Forms.Plugin.Abstractions;assembly=ImageCircle.Forms.Plugin.Abstractions"
```

Then add the xaml:

```xml
<controls:CircleImage Source="{Binding Image}" Aspect="AspectFill">
  <controls:CircleImage.WidthRequest>
    <OnPlatform x:TypeArguments="x:Double">
      <On Platform="Android, iOS">55</On>
      <On Platform="WinPhone">75</On>
    </OnPlatform>
  </controls:CircleImage.WidthRequest>
  <controls:CircleImage.HeightRequest>
    <OnPlatform x:TypeArguments="x:Double">
      <On Platform="Android, iOS">55</On>
      <On Platform="WinPhone">75</On>
    </OnPlatform>
  </controls:CircleImage.HeightRequest>
</controls:CircleImage>
```

**Bindable Properties**

You are able to set the ```BorderColor``` to a Forms.Color to display a border around your image and also ```BorderThickness``` for how thick you want it. 

You can also set ```FillColor``` to the Forms.Color to fill the circle. DO NOT set ```BackgroundColor``` as that will be the square the entire image takes up.

These are supported in iOS, Android, WinRT, and UWP (not on Windows Phone 8 Silverlight).

### Final Builds
For linking you may need to add:

#### Android:
```
ImageCircle.Forms.Plugin.Abstractions;ImageCircle.Forms.Plugin.Android;
```
#### iOS:
```
--linkskip=ImageCircle.Forms.Plugin.iOS --linkskip=ImageCircle.Forms.Plugin.Abstractions
```

#### UWP:
Be sure to read through the [troubleshooting for UWP with .NET Native](https://developer.xamarin.com/guides/xamarin-forms/platform-features/windows/installation/universal/#Troubleshooting) for your final package. You should add the package to the Init call of Xamarin.Forms such as:

```
var rendererAssemblies = new[]
{
    typeof(ImageCircleRenderer).GetTypeInfo().Assembly
};
Xamarin.Forms.Forms.Init(e, rendererAssemblies);

```


#### License
Licensed under MIT, see license file
