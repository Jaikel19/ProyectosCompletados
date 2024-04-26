using System.Text;

namespace WebUI.Utility
{
    public class HTTPRequest
    {

        private HttpClient _httpClient;
        private string _url;

        public HTTPRequest()
        {
            _httpClient = new HttpClient();
            _url = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        }

        public string Get(string url, string token = null)
        {
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            }

            //hacemos la solicitud el get. 
            var webRequest = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/{1}", _url, url));

            var response = _httpClient.Send(webRequest);

            //toma la respuesta como Stream 
            using var reader = new StreamReader(response.Content.ReadAsStream());

            return reader.ReadToEnd();
        }

        public string Post(string url, string jsonContent, string token = null)
        {
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            }

            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var content = new ByteArrayContent(messageBytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = _httpClient.PostAsync(string.Format("{0}/{1}", _url, url), content).Result;

            if (response.IsSuccessStatusCode)
            {
                using var reader = new StreamReader(response.Content.ReadAsStream());

                return reader.ReadToEnd();
            }

            return string.Empty;
        }

    }
}
