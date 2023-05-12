using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;

namespace SuessLabs.MokaWidget
{
  [BroadcastReceiver(Label = "Moka Pot Widget", Exported = false)]
  [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
  [MetaData("android.appwidget.provider", Resource = "@xml/app_widget")]
  public class MokaWidget : AppWidgetProvider
  {
    private const string IconClick = "IconClick";

    public override void OnReceive(Context context, Intent intent)
    {
      base.OnReceive(context, intent);

      switch (intent.Action)
      {
        case IconClick:
          //// StartStopTimer();
          LaunchSettings(context);
          break;

        case "android.appwidget.action.APPWIDGET_ENABLED":
          System.Diagnostics.Debug.WriteLine("The widget was created on the home screen.");
          break;

        case "android.appwidget.action.APPWIDGET_UPDATE":
          System.Diagnostics.Debug.WriteLine("The widget was updated - i.e. Clicked.");

          var widgetView = new RemoteViews(context.PackageName, Resource.Layout.Widget);
          widgetView.SetTextViewText(Resource.Id.widgetSmall, string.Format("Last update: {0:H:mm:ss}", DateTime.Now));

          break;
        case "android.appwidget.action.APPWIDGET_UPDATE_OPTIONS":
          System.Diagnostics.Debug.WriteLine("The widget was resized.");
          break;
        default:
          break;
      }
    }

    public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
    {
      base.OnUpdate(context, appWidgetManager, appWidgetIds);

      // Push update for this widget to the home screen
      // Note:
      //  The following is not the same as, nameof(MokaWidget)
      //  Java.Lang.Class.FromType(typeof(MokaWidget)).Name; =  "crc64ef76b1a33cfe8ba0.MokaWidget"
      // .
      var component = new ComponentName(context, Java.Lang.Class.FromType(typeof(MokaWidget)).Name);
      appWidgetManager.UpdateAppWidget(component, InitRemoteViews(context, appWidgetIds));
    }

    private PendingIntent IconPendingIntent(Context context, string action)
    {
      var intent = new Intent(context, typeof(MokaWidget));
      intent.SetAction(action);
      return PendingIntent.GetBroadcast(context, 0, intent, 0);
    }

    private RemoteViews InitRemoteViews(Context context, int[] appWidgetIds)
    {
      var widgetView = new RemoteViews(context.PackageName, Resource.Layout.Widget);

      // Set Sample TextView Text
      widgetView.SetTextViewText(Resource.Id.widgetMedium, "Moka Pot Timer");
      widgetView.SetTextViewText(Resource.Id.widgetSmall, string.Format("Last update: {0:H:mm:ss}", DateTime.Now));

      // Register OnClick of widget
      var intent = new Intent(context, typeof(MokaWidget));
      intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
      intent.PutExtra(AppWidgetManager.ExtraAppwidgetId, appWidgetIds);

      // Click event of the main background
      var pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
      widgetView.SetOnClickPendingIntent(Resource.Id.widgetBackground, pendingIntent);

      // Click event of the Icon
      widgetView.SetOnClickPendingIntent(Resource.Id.widgetMokaIcon, IconPendingIntent(context, IconClick));

      return widgetView;
    }

    /// <summary>Used for testing clicking.</summary>
    /// <param name="context"></param>
    private void LaunchSettings(Context context)
    {
      var pm = context.PackageManager;
      try
      {
        // Launch Android Settings
        var packageName = "com.android.settings";
        var launch = pm.GetLaunchIntentForPackage(packageName);
        context.StartActivity(launch);
      }
      catch
      {
      }
    }
  }
}
