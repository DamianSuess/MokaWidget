// To debug the application, you must include the following.
// Otherwise, it will exit out after uploading.
#if DEBUG

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace SuessLabs.MokaWidget;

[Activity(
  Theme = "@style/Maui.SplashTheme",
  MainLauncher = true,
  Exported = true,
  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
  protected override void OnCreate(Bundle savedInstanceState)
  {
    base.OnCreate(savedInstanceState);

    SetContentView(Resource.Layout.Main);

    Toast.MakeText(this, "Long-pree the homescreen to add the widget", ToastLength.Long).Show();
    Finish();
  }
}

#endif
