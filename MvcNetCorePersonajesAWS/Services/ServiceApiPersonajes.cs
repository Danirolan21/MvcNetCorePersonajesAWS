using MvcNetCorePersonajesAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcNetCorePersonajesAWS.Services
{
    public class ServiceApiPersonajes
    {
        private MediaTypeWithQualityHeaderValue header;
        private string UrlApi;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Personaje>> GetPersonajes()
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                    if (response.IsSuccessStatusCode)
                    {
                        List<Personaje> personajes = await response.Content.ReadAsAsync<List<Personaje>>();
                        return personajes;
                    }
                    else
                        return null;
                }
            }
        }

        public async Task CreatePersonajesAsync(string nombre, string imagen)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    Personaje personaje = new Personaje
                    {
                        IdPersonaje = 0,
                        Nombre = nombre,
                        Imagen = imagen
                    };
                    string json = JsonConvert.SerializeObject(personaje);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(this.UrlApi + request, content);
                }
            }
        }

        public async Task<Personaje> GetPersonaje(int id)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes/" + id;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                    if (response.IsSuccessStatusCode)
                    {
                        Personaje personaje = await response.Content.ReadAsAsync<Personaje>();
                        return personaje;
                    }
                    else
                        return null;
                }
            }
        }

        public async Task UpdatePersonajesAsync(int idpersonaje, string nombre, string imagen)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes/" + idpersonaje;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    Personaje personaje = new Personaje
                    {
                        IdPersonaje = idpersonaje,
                        Nombre = nombre,
                        Imagen = imagen
                    };
                    string json = JsonConvert.SerializeObject(personaje);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(this.UrlApi + request, content);
                }
            }
        }

        public async Task DeletePersonajesAsync(int id)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes/" + id;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    HttpResponseMessage response = await client.DeleteAsync(this.UrlApi + request);
                }
            }
        }
    }
}
