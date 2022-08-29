using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;

namespace SuessLabs.MokaWidget
{
  [BroadcastReceiver(Label = "Moka Pot Widget", Exported = false)]
  [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
  [MetaData("android.appwidget.provider", Resource = "@xml=app_widget")]
  public class MokaWidget : AppWidgetProvider
  {
    private const string IconClick = "IconClick";

    public override void OnReceive(Context context, Intent intent)
    {
      base.OnReceive(context, intent);
    }

    public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
    {
      base.OnUpdate(context, appWidgetManager, appWidgetIds);

      // TODO: Refactor as, nameof(MokaWidget)
      var component = new ComponentName(context, Java.Lang.Class.FromType(typeof(MokaWidget)).Name);
      appWidgetManager.UpdateAppWidget(component, InitRemoteViews(context, appWidgetIds));
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

    private PendingIntent IconPendingIntent(Context context, string action)
    {
      var intent = new Intent(context, typeof(MokaWidget));
      intent.SetAction(action);
      return PendingIntent.GetBroadcast(context, 0, intent, 0);
    }
  }
}
