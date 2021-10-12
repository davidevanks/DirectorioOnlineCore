using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AppDirectorioWeb.Helper.RequestProvider.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Helper.RequestProvider.Implementation
{
    /// <summary>
    /// Proporciona métodos para realizar las peticiones al backend
    /// </summary>
    public class BackendHelper : IBackendHelper
    {
        private readonly string _backendApiUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Crea una instancia del objeto
        /// </summary>
        /// <param name="configuration">Servicio de configuracion (appsettings)</param>
        /// <param name="httpContextAccessor">Servicio httpContext</param>
        public BackendHelper(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _backendApiUrl = configuration["BackendApiUrl"];
            _httpContextAccessor = httpContextAccessor;
        }

        #region Post
        /// <summary>
        /// Permite realizar una Peticion tipo POST 
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PostAsync<TResult>(string uri, object data)
        {
            uri = _backendApiUrl + uri;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            TResult result = await ManejarRespuesta<TResult>(response);

            return result;
        }

        /// <summary>
        /// Permite realizar una Peticion tipo POST autenticada
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PostTokenAsync<TResult>(string uri, object data)
        {
            uri = _backendApiUrl + uri;
            var clientToken = ObtenerAuthToken();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", clientToken);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            TResult result = await ManejarRespuesta<TResult>(response);

            return result;
        }
        #endregion POST

        #region GET
        /// <summary>
        /// Permite realizar una Peticion tipo Get
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        public async Task<TResult> GetAsync<TResult>(string uri)
        {

            uri = _backendApiUrl + uri;
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            TResult result = await ManejarRespuesta<TResult>(response);

            return result;
        }

        /// <summary>
        /// Permite realizar una Peticion tipo Get autenticada
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        public async Task<TResult> GetTokenAsync<TResult>(string uri)
        {
            uri = _backendApiUrl + uri;
            var clientToken = ObtenerAuthToken();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", clientToken);

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            TResult result = await ManejarRespuesta<TResult>(response);

            return result;
        }
        #endregion GET

        #region Put

        /// <summary>
        /// Permite realizar una peticion tipo PUT
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PutAsync<TResult>(string uri, object data)
        {
            uri = _backendApiUrl + uri;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            TResult result = await ManejarRespuesta<TResult>(response);

            return result;
        }

        /// <summary>
        /// Permite realizar una peticion tipo PUT con autenticación
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PutTokenAsync<TResult>(string uri, object data)
        {
            try
            {
                uri = _backendApiUrl + uri;

                var clientToken = ObtenerAuthToken();
                var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(data));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", clientToken);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PutAsync(uri, content);

                TResult result = await ManejarRespuesta<TResult>(response);

                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
        #endregion Put

        #region Privados   

        /// <summary>
        /// Metodo que deserealiza el contenido
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="respuesta">Respuesta de la peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        private async Task<TResult> ManejarRespuesta<TResult>(HttpResponseMessage respuesta)
        {
            string contenido = await respuesta.Content.ReadAsStringAsync();

            if (!respuesta.IsSuccessStatusCode)
            {
                var diccionario = JsonConvert.DeserializeObject<Dictionary<string, object>>(contenido);
                var subContenido = diccionario["content"].ToString();
                //TResult objetoRespuesta = JsonConvert.DeserializeObject<TResult>(subContenido);
                //return objetoRespuesta;
                return default;
            }
            else
            {
                TResult objetoRespuesta = JsonConvert.DeserializeObject<TResult>(contenido);

                return objetoRespuesta;
            }
        }

        /// <summary>
        /// Permite obtener el token del usuario en sesion
        /// </summary>
        /// <returns>Token</returns>
        private string ObtenerAuthToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
                return httpContext.GetTokenAsync("access_token").Result;

            return null;
        }
        #endregion Privados

    }
}
