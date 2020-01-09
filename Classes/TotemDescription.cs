using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Newtonsoft.Json;
using Mitupo.Models;

//This Activity is responsible for displaying the details and the body of  a totem.
//It uses a scrollview and two text views inside


namespace Mitupo.Classes
{
    [Activity(Label = "Totem Description", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ParentActivity= typeof(MainActivity))]
    public class TotemDescription : AppCompatActivity
    {
        //Create views 
        private TextView mDescription;
        private TextView mBody;

        //Gets the data from the previous activity
        private Totem mTotem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TotemDescription);

            // Create your application here
            Initialize();

            //This creates and populates a toolbar
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar1);

            //Set a proper title
            toolbar.Title = $"{mTotem.Animal} {mTotem.Id}";
            //Set the toolbar

            SetSupportActionBar(toolbar);
            
            
        }
        private void Initialize()
        {
            //Initilize data
            mTotem = JsonConvert.DeserializeObject<Totem>(Intent.GetStringExtra("Totem"));
            
           //Initialize the views
            mDescription = FindViewById<TextView>(Resource.Id.totemDescript);
            mBody = FindViewById<TextView>(Resource.Id.totemBody);
           
            //Use String builder to append the list into a single string
            var praise = new StringBuilder();

            foreach (var line in mTotem.Body)
            {
                praise.Append(line).Append("\n");
            }

            //Set data to views
            mDescription.Text = mTotem.Description;
            mBody.Text = praise.ToString();

        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            //Destroy the activity when the use leaves the activity
            Finish();
            
        }
    }
}