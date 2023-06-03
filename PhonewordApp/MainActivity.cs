using System.Collections.Generic;
using System.Net.Mime;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace PhonewordApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private static readonly List<string> phoneNumbers = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            TextView translatedPhoneNumber = FindViewById<TextView>(Resource.Id.TranslatePhoneWord);
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.TranslationHitoryButton);

            string translatedNumber = string.Empty;
            translateButton.Click += (sender, e) =>
            {
                translatedNumber = PhoneTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrEmpty(translatedNumber))
                {
                    translatedPhoneNumber.Text = "";
                }
                else
                {
                    translatedPhoneNumber.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    translationHistoryButton.Enabled = true;
                }
            };

            translationHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };
        }
    }
}