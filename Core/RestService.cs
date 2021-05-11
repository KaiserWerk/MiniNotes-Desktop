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

        private string baseAddress;
        private string identifier;
        private string secret;

        public RestService()
        {
            this.client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
        }

        public RestService SetRemote(string address)
        {
            //this.client.BaseAddress = new Uri(address.TrimEnd('/') + "/");
            this.baseAddress = address;
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
            var msg = new HttpRequestMessage(HttpMethod.Post, $"{this.baseAddress}/store")
            {
                Content = new StringContent(content),
            };

            byte[] authBytes = Encoding.ASCII.GetBytes($"{this.identifier}:{this.secret}");
            string authHeader = Convert.ToBase64String(authBytes);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

            try
            {
                this.client.Send(msg);
            }
            catch
            {
                throw;
            }
        }

        public string GetContent()
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, $"{this.baseAddress}/get");

            byte[] authBytes = Encoding.ASCII.GetBytes($"{this.identifier}:{this.secret}");
            string authHeader = Convert.ToBase64String(authBytes);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

            var resp = this.client.Send(msg);

            try
            {
                return resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch
            {
                throw;
            }
        }
    }
}
