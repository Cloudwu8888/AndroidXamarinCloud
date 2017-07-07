using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Push;

namespace AndroidXamianClick
{
    [Activity(Label = "AndroidXamianClick", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // This should come before MobileCenter.Start() is called
            // This should come before MobileCenter.Start() is called
            Push.PushNotificationReceived += (sender, e) => {

                // Add the notification message and title to the message
                var summary = $"Push notification received:" +
                                    $"\n\tNotification title: {e.Title}" +
                                    $"\n\tMessage: {e.Message}";

                // If there is custom data associated with the notification,
                // print the entries
                if (e.CustomData != null)
                {
                    summary += "\n\tCustom data:\n";
                    foreach (var key in e.CustomData.Keys)
                    {
                        summary += $"\t\t{key} : {e.CustomData[key]}\n";
                    }
                }

                // Send the notification summary to debug output
                System.Diagnostics.Debug.WriteLine(summary);
            };
            Push.EnableFirebaseAnalytics();
            MobileCenter.Start("465bfc70-eb81-49c4-b7d2-f998d12e5be4",
                   typeof(Analytics), typeof(Crashes), typeof (Push));
            Analytics.TrackEvent("Click me");
            System.Guid? installId = MobileCenter.InstallId;
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            SetContentView(Resource.Layout.Main);

            // 获取布局中的控件

            Button say = FindViewById<Button>(Resource.Id.sayHello);

            TextView show = FindViewById<TextView>(Resource.Id.showHello);

            // 绑定 Click 事件

            say.Click += (sender, e) =>

            {

                count++;

                show.Text = "Hello, Android";

                say.Text = $"You Clicked {count}";

                // Toast 通知

                Toast.MakeText(this, $"You Clicked {count}", ToastLength.Short).Show();
                if (count>6)
                {
                    Crashes.Enabled = true;
                    //throw new System.Exception("error:click>5");
                    Crashes.GenerateTestCrash();

                }

            };


        }
    }
}

