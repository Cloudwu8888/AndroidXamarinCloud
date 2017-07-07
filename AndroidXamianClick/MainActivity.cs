using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace AndroidXamianClick
{
    [Activity(Label = "AndroidXamianClick", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            MobileCenter.Start("465bfc70-eb81-49c4-b7d2-f998d12e5be4",
                   typeof(Analytics), typeof(Crashes));

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

