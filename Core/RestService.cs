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

        private string remoteUrl;
        private string identifier;
        private string secret;

        public RestService()
        {
            var handler = new HttpClientHandler()
            {
                UseProxy = true,
                Proxy = new WebProxy("http://127.0.0.1:8888"),
            };
            this.client = new HttpClient(handler);
            
        }

        public RestService SetRemote(string address)
        {
            this.remoteUrl = address;
            return this;
        }

        public RestService SetId(string id)
        {
            this.identifier = id;
            return this;
        }

        public RestService SetSecret(string secret)
        {
            this.secret = secret;
            return this;
        }

        public void StoreContent(string content)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, this.remoteUrl)
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
            return ""; // TODO
        }
    }
}
