using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace PAlert
{
    public class CloudDataStore //: IDataStore<Item>
    {
        
        //HttpClient client;
        IEnumerable<Item> items;
        IFirebaseClient client;


        public CloudDataStore()
        {
            client = new FirebaseClient(InitFirebase());
            //client = new HttpClient();
            //client.BaseAddress = new Uri($"{App.BackendUrl}/");

            items = new List<Item>();
        }

        private IFirebaseConfig InitFirebase()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "your api key goes here",
                BasePath = "https://apexalertengine.firebaseio.com"
            };
            return config;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string item, bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetAsync(item);
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json.Body));
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetAsync($"predators/{id}");
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
            var primer = await client.SetAsync($"predators/{name}/sightings/{item.Id}", "");
            if (primer.StatusCode != HttpStatusCode.Accepted || (primer.StatusCode != HttpStatusCode.OK) || !(primer.StatusCode.Equals(HttpStatusCode.Processing)))
            {
                Debug.WriteLine($"Node Primer status code returned with {primer}");
                //return primer.StatusCode;
            }
            var response = await client.SetAsync($"predators/{name}/sightings/{item.Id}/", item);//new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateItemAsync(Item item, string name)
        {
            if (item == null || item.Id.ToString() == null || !CrossConnectivity.Current.IsConnected)
                return HttpStatusCode.BadRequest;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PushAsync($"predators/{name}/sightings/{item.Id}", byteContent);

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
