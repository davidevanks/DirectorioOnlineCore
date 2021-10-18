using AppDirectorioWeb.RequestProvider.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppDirectorioWeb.RequestProvider.Implementation
{
    /// <summary>
    /// Proporciona métodos para realizar las peticiones al backend
    /// </summary>
    public class BackendHelper : IBackendHelper
    {
        #region Private Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Crea una instancia del objeto
        /// </summary>
        /// <param name="configuration">Servicio de configuracion (appsettings)</param>
        /// <param name="httpContextAccessor">Servicio httpContext</param>
        public BackendHelper(

            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Public Constructors

        #region Post

        /// <summary>
        /// Permite realizar una Peticion tipo POST
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        ///    <param name="JWToken">Token opcional autorización</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PostAsync<TResult>(string uri, object data, string JWToken = null)
        {
            //HttpClient httpClient = new HttpClient();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (!String.IsNullOrEmpty(JWToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }
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
        ///  /// <param name="JWToken">Token opcional autorización</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PostTokenAsync<TResult>(string uri, object data, string JWToken = null)
        {
            var clientToken = ObtenerAuthToken();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", clientToken);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (!String.IsNullOrEmpty(JWToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            TResult result = await ManejarRespuesta<TResult>(response);

            return result;
        }

        #endregion Post

        #region GET

        /// <summary>
        /// Permite realizar una Peticion tipo Get
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        ///  <param name="JWToken">token autorización</param>
        public async Task<TResult> GetAsync<TResult>(string uri, string JWToken = null)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient httpClient = new HttpClient(clientHandler);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!String.IsNullOrEmpty(JWToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }

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
        ///   <param name="JWToken">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PutAsync<TResult>(string uri, object data, string JWToken = null)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (!String.IsNullOrEmpty(JWToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }
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
        ///   <param name="JWToken">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        public async Task<TResult> PutTokenAsync<TResult>(string uri, object data, string JWToken = null)
        {
            try
            {
                var clientToken = ObtenerAuthToken();
                var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(data));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", clientToken);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                if (!String.IsNullOrEmpty(JWToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                }
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