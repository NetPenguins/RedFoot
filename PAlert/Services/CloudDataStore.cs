using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Google.Apis.Auth.OAuth2;
using Mono.CSharp;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace PAlert
{
    public class CloudDataStore //: IDataStore<Item>
    {
        
        //HttpClient client;
        
        Dictionary<string, Item> items;
        IFirebaseClient client;


        public CloudDataStore()
        {
            client = new FirebaseClient(InitFirebase());
            //client = new HttpClient();
            //client.BaseAddress = new Uri($"{App.BackendUrl}/");
            
            //items = new List<Item>();
        }

        private IFirebaseConfig InitFirebase()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "Your-api-goes-here", // need to utilize firebase-admin
                BasePath = "https://redfoot-18be2.firebaseio.com/"
            };
            return config;
        }

        public async Task<Dictionary<string, Item>> GetItemsAsync(string item, bool forceRefresh = false)
        {
            //if (forceRefresh && CrossConnectivity.Current.IsConnected)
            //{
            var json = await client.GetAsync(item);
            
            if (String.IsNullOrWhiteSpace(json.Body) || json.Body == "null")
            {
                return new Dictionary<string, Item>();
            }
            //var j = json.Body.Split(':', 2)[1].Split(':', 2)[1];
            //string jsonString = JsonConvert.SerializeObject(json.Body);
            items = JsonConvert.DeserializeObject<Dictionary<string, Item>>(json.Body);
            //}
            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetAsync($"/{id}");
                var val = await Task.Run(() => JsonConvert.DeserializeObject<Item>(json.Body));
                return val;
            }

            return null;
        }

        public async Task<HttpStatusCode> AddItemAsync(Item item, string name, string id)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return HttpStatusCode.BadRequest;

            var serializedItem = JsonConvert.SerializeObject(item);
            //var primer = await client.SetAsync($"{name}", "");
            //if (primer.StatusCode != HttpStatusCode.Accepted || (primer.StatusCode != HttpStatusCode.OK) || !(primer.StatusCode.Equals(HttpStatusCode.Processing)))
            //{
            //    Debug.WriteLine($"Node Primer status code returned with {primer}");
            //    //return primer.StatusCode;
            //}
            var response = await client.SetAsync($"{item.Id}/", item);//new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateItemAsync(Item item, string name)
        {
            if (item == null || item.Id.ToString() == null || !CrossConnectivity.Current.IsConnected)
                return HttpStatusCode.BadRequest;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PushAsync($"predators/{name}/{item.Id}", byteContent);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return HttpStatusCode.BadRequest;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.StatusCode;
        }

        //Need a way to call all data items filtering for locale 
    }
}
