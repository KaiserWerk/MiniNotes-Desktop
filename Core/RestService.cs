using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Core
{
    public class RestService
    {
        private HttpClient client;

        private string identifier;
        private string secret;

        public RestService()
        {
            this.client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
        }

        public RestService SetRemote(string address)
        {
            this.client.BaseAddress = new Uri(address.TrimEnd('/') + "/");
            return this;
        }

        public RestService SetId(string id)
        {
            this.identifier = id;
            return this;
        }

        public RestService SetSecret(string s)
        {
            this.secret = s;
            return this;
        }

        public void StoreContent(string content)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, "store")
            {
                Content = new StringContent(content),
            };

            byte[] authBytes = Encoding.ASCII.GetBytes($"{this.identifier}:{this.secret}");
            string authHeader = Convert.ToBase64String(authBytes);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

            this.client.Send(msg);
        }

        public string GetContent()
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, "get");

            byte[] authBytes = Encoding.ASCII.GetBytes($"{this.identifier}:{this.secret}");
            string authHeader = Convert.ToBase64String(authBytes);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

            var resp = this.client.Send(msg);

            return resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }
}
