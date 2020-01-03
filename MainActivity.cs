 using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Mitupo.Models;
using Newtonsoft.Json;
using Mitupo.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Refit;
using System.IO;
using System.Text.Json;
using Mitupo.Classes;
using Android.Support.V7.Widget;

namespace Mitupo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //Data variables
        private List<Totem> totems ;
        private IData apiConnect;

        //Views and adapter for displaying data
        private RecyclerView mRecyclerView;
        private TotemRecyclerAdapter adapter;
        private LinearLayoutManager layoutManager;

        protected  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            Initialize();
            
        }

        private void Initialize()
        {
            //For Refit
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters =
                {
                    new StringEnumConverter()
                }
            };
            //Used when connect to an API
            apiConnect = RestService.For<IData>("https://totems.azurewebsites.net/totems");
            
            

            //Reading data from local json file
            totems = GetAll(new ReadData().GetTotems());
            ApiConnect();
            //ApiConnect() function makes a request using the global interface object initialized above

            //For views 
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.totems_list1);

            mRecyclerView.HasFixedSize = true;

            //Initialize a layout to be used by the Recyclerview
            layoutManager = new LinearLayoutManager(this);


            mRecyclerView.SetLayoutManager(layoutManager);

            adapter = new TotemRecyclerAdapter(totems,this);
            mRecyclerView.SetAdapter(adapter);


        }

        private void ShowToast(string v)
        {
            Toast.MakeText(this, v, ToastLength.Long).Show();
        }

        private List<Totem> GetAll(IEnumerable<Totem> mTotems)
        {
            List<Totem> mtotems = new List<Totem>();
            foreach (var item in mTotems)
            {
                mtotems.Add(item);
            }
            return mtotems;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
       
        private async void ApiConnect()
        {
            try
            {
                List<Totem> mtotems = await apiConnect.GetTotems();
                ShowToast(mtotems[0].Animal);
            }
            catch (Exception ex)
            {
                ShowToast("" + ex.Message);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

