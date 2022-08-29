# Building Android Widget for MAUI

## Android-Only Projects

1. Remove unnecessary Platform project folders
2. Remove build components from `.csproj` file (2 places)

## Heads-up

### Android 12 Issues

According to the [Android Activity Element](https://developer.android.com/guide/topics/manifest/activity-element#exported) docs, we have to set the `android:exported` in our `<activity>`.

Because we're use, `[IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]` in the `MokaWidget.cs`, the flag must be set.

```cs
  [BroadcastReceiver(Label = "Moka Pot Widget", Exported = true)]
```

> This element sets whether the activity can be launched by components of other applications:
>
> * If "`true`", the activity is accessible to any app, and is launchable by its exact class name.
> * If "`false`", the activity can be launched only by components of the same application, applications with the same user ID, or privileged system components. This is the default value when there are no intent filters.
>
> If an activity in your app includes intent filters, set this element to "`true`" to allow other apps to start it. For example, if the activity is the main activity of the app and includes the `category` "`android.intent.category.LAUNCHER`".
>
> If this element is set to "`false`" and an app tries to start the activity, the system throws an `ActivityNotFoundException`.


```text
Severity	Code	Description	Project	File	Line	Suppression State
Error	AMM0000
	android:exported needs to be explicitly specified for element <receiver#crc64ef76b1a33cfe8ba0.MokaWidget>. Apps targeting Android 12 and higher are required to specify an explicit value for `android:exported` when the corresponding component has an intent filter defined. See https://developer.android.com/guide/topics/manifest/activity-element#exported for details.
	SuessLabs.MokaWidget	C:\work\labs\CoffeeWidget\source\obj\Debug\net6.0-android\AndroidManifest.xml	19
```
