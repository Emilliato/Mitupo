using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Mitupo.Classes;
using Mitupo.Models;
using Mitupo.Services;
using Newtonsoft.Json;

namespace Mitupo.Models
{
    class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ImageView { get; set; }

        public TextView Tittle { get; set; }

        public TextView Description { get; set; }

        public  RecyclerViewHolder(View itemView): base(itemView)
        {
            ImageView = itemView.FindViewById<ImageView>(Resource.Id.totemImage);

            Tittle = itemView.FindViewById<TextView>(Resource.Id.totem_id);

            Description = itemView.FindViewById<TextView>(Resource.Id.totem_description);

        }
       
    }

       
    

    class TotemRecyclerAdapter : RecyclerView.Adapter
    {
        

        private readonly List<Totem> mTotems;

        private readonly Activity _Context;

        public TotemRecyclerAdapter(List<Totem> mTotems, Activity context)
        {
          
            this.mTotems = mTotems;
            _Context = context;
        }

        public override int ItemCount 
        {
            get 
            { 
                return mTotems.Count; 
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            

            Totem totem = mTotems[position];

            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;

             
            //viewHolder.mImageView
            StringBuilder builder = new StringBuilder("");

            builder.Append(totem.Animal).Append(" ").Append(totem.Id);

            // Android.Net.Uri url = Android.Net.Uri.Parse(totem.Image);

           // viewHolder.mImageView.SetImageURI(url);

            viewHolder.ImageView.SetImageResource(ImageResource(totem.Animal.ToLower())); 

            viewHolder.Tittle.Text = builder.ToString();

            viewHolder.Description.Text = SubString(totem.Description);

            //Handle clicks on description
            viewHolder.Description.Click += (sender, args) => 
            {
                Intent intent = new Intent(_Context, typeof(TotemDescription));

                intent.PutExtra("Totem", JsonConvert.SerializeObject(totem));

                _Context.StartActivity(intent);

            };

            //Handle clicks on ImageView
            viewHolder.ImageView.Click += (sender, args) => 
            {
                Intent intent = new Intent(_Context, typeof(TotemDescription));

                intent.PutExtra("Totem", JsonConvert.SerializeObject(totem));

                _Context.StartActivity(intent);

            };

        }

       
        //Shorten the description to 25 words only
        public string SubString(string description)
        {
            string[] allStrings = description.Split();

            if (allStrings.Length <=25)
            {
                return description+"......";
            }
            else
            {
                StringBuilder mBuilder = new StringBuilder();

                for (int i=0;i<26;i++)
                {
                    mBuilder.Append(allStrings[i]).Append(" ");
                }

                mBuilder.Append("......");

                return mBuilder.ToString();
            }
            
        }

        public int ImageResource(string totemName)
        {
            List<string> totemNames = new List<string>
            {
                "shumba",

                "shoko",

                "soko"
            };


            List<int> totemIds = new List<int>
            {
                Resource.Drawable.shumba,

                Resource.Drawable.soko,

                Resource.Drawable.soko
            };

            return totemIds[totemNames.IndexOf(totemName)];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);

            View mView = layoutInflater.Inflate(Resource.Layout.card_item,parent,false);

            return new RecyclerViewHolder(mView);
        }
    }
}