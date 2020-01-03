using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Mitupo.Models
{
    class Totem
    {
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Animal { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("url")]
        public string Image { get; set; }
        public string[] Body { get; set; }

        public Totem()
        {
        }

        public Totem(string id, string animal, string origin, string description, string image, string[] body)
        {
            Id = id;
            Animal = animal;
            Origin = origin;
            Description = description;
            Image = image;
            Body = body;
        }
        public override string ToString() => JsonSerializer.Serialize<Totem>(this);
  
    }
}