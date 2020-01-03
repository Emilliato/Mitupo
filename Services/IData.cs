using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mitupo.Models;
using Refit;

namespace Mitupo.Services
{
    interface IData
    {
        [Get("/totems")]
        Task<List<Totem>> GetTotems();
    }
}