using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mitupo.Models;
using Newtonsoft.Json;

namespace Mitupo.Services
{
    class ReadData
    {
        
        public IEnumerable<Totem> GetTotems()
        {
            string fileName = "data.json"; 
            var assembly = typeof(MainActivity).GetTypeInfo().Assembly;
            
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}");
            using (var jsonFileReader = new System.IO.StreamReader(stream))
            {
                return System.Text.Json.JsonSerializer.Deserialize<Totem[]>(jsonFileReader.ReadToEnd(),
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               });
            }

        }
    }
}