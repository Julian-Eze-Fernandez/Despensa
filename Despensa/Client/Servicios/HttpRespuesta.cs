using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;

namespace Despensa.Client.Servicios
{
    public class HttpRespuesta<T>
    {
        public T? Respuesta { get; set; }

        //sirve como bandera
        public bool Error { get; set; }

        //Respuesta del actionresult
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public HttpRespuesta(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            this.Respuesta = response;
            this.Error = error;
            this.HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string> ObtenerError()
        {
            if (!Error)
            {
                return "";
            }

            var statuscode = HttpResponseMessage.StatusCode;

            switch (statuscode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    return "Error, no se puede procesar la información.";
                case System.Net.HttpStatusCode.Unauthorized:
                    return "Error, no está logueado.";
                case System.Net.HttpStatusCode.Forbidden:
                    return "Error, no tiene autorización a ejecutar este proceso.";
                case System.Net.HttpStatusCode.NotFound:
                    return "Error, dirección no encontrada.";
                default:
                    return HttpResponseMessage.Content.ReadAsStringAsync().Result;

            }


        }

    }
}
