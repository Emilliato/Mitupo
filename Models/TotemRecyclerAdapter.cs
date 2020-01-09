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

        public TextView ViewMore { get; set; }

        public  RecyclerViewHolder(View itemView): base(itemView)
        {
            ImageView = itemView.FindViewById<ImageView>(Resource.Id.totemImage);

            Tittle = itemView.FindViewById<TextView>(Resource.Id.totem_id);

            Description = itemView.FindViewById<TextView>(Resource.Id.totem_description);

            ViewMore = itemView.FindViewById<TextView>(Resource.Id.totem_btn_view_more);
        }
       
    }

       
    

    class TotemRecyclerAdapter : RecyclerView.Adapter
    {
        

        private readonly  List<Totem> mTotems;

        private  readonly Activity _Context;

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

            //We use RecyclerViewHolder to access views
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            
            //Set an Image first
            viewHolder.ImageView.SetImageResource(ImageResource(totem.Animal.ToLower()));

            //For title
            StringBuilder builder = new StringBuilder("");

            builder.Append(totem.Animal).Append(" ").Append(totem.Id);

            //Set title
            viewHolder.Tittle.Text = builder.ToString();

            //Set Description
            viewHolder.Description.Text = SubString(totem.Description);

           
            //Handle clicks on description
            viewHolder.ViewMore.Click += (sender, args) =>
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

                "soko",

                "humba",

                "tembo"
            };


            List<int> totemIds = new List<int>
            {
                Resource.Drawable.shumba,

                Resource.Drawable.soko,

                Resource.Drawable.soko,
                 Resource.Drawable.humba,

                  Resource.Drawable.tembo
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