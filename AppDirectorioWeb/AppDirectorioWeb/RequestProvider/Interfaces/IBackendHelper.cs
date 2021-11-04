using System.Threading.Tasks;

namespace AppDirectorioWeb.RequestProvider.Interfaces
{
    /// <summary>
    /// Interfaz de Helper de proveedor de solicitudes
    /// </summary>
    public interface IBackendHelper
    {
        #region Public Methods

        /// <summary>
        /// Permite realizar una peticion tipo Get
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        Task<TResult> GetAsync<TResult>(string uri, string JWToken = null);

        /// <summary>
        /// Permite realizar una Peticion tipo Get autenticada
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        Task<TResult> GetTokenAsync<TResult>(string uri);

        /// <summary>
        /// Peticion Post de forma asincronica
        /// </summary>
        /// <typeparam name="TResult">Entidad a Recibir</typeparam>
        /// <param name="uri">Url a mandar</param>
        /// <param name="data">Data a mandar</param>
        /// <returns>Entidad a Recibir</returns>
        Task<TResult> PostAsync<TResult>(string uri, object data, string JWToken = null);

        /// <summary>
        /// Permite realizar una Peticion tipo POST autenticada
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        Task<TResult> PostTokenAsync<TResult>(string uri, object data, string JWToken = null);

        /// <summary>
        /// Permite realizar una peticion tipo PUT
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        Task<TResult> PutAsync<TResult>(string uri, object data, string JWToken = null);

        /// <summary>
        /// Permite realizar una peticion tipo PUT con autenticación
        /// </summary>
        /// <typeparam name="TResult">El tipo a deserealizar</typeparam>
        /// <param name="uri">Url a donde enviar la peticion</param>
        /// <param name="data">Data necesaria para peticion</param>
        /// <returns>La respuesta deseralizada segun el tipo indicado</returns>
        Task<TResult> PutTokenAsync<TResult>(string uri, object data, string JWToken = null);

        #endregion Public Methods
    }
}