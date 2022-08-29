using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;

namespace SuessLabs.MokaWidget
{
  [BroadcastReceiver(Label = "Moka Pot Widget")]
  [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
  [MetaData("android.appwidget.provider", Resource = "@xml=appwidgetprovider")]
  public class MokaWidget : AppWidgetProvider
  {
    public override void OnReceive(Context context, Intent intent)
    {
      base.OnReceive(context, intent);
    }

    public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
    {
      base.OnUpdate(context, appWidgetManager, appWidgetIds);

      // TODO: Refactor as, nameof(MokaWidget)
      var component = new ComponentName(context, Java.Lang.Class.FromType(typeof(MokaWidget)).Name);
      appWidgetManager.UpdateAppWidget(component, BuildRemoteViews(context, appWidgetIds));
    }

    private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
    {
      var widgetView = new RemoteViews(context.PackageName, Resource.Layout.Widget);

      widgetView.SetTextViewText(Resource.Id.widgetMedium, "HelloAppWidget");
      widgetView.SetTextViewText(Resource.Id.widgetSmall, string.Format("Last update: {0:H:mm:ss}", DateTime.Now));

      //// RegisterClicks(context, appWidgetIds, widgetView);

      return widgetView;
    }
  }
}
