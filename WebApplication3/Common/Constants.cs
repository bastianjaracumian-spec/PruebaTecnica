namespace ApiAtencionesMédicas.Common
{
    public class Constants
    {
        public class StatusCode
        {
            public const int status200 = 200;
            public const int status201 = 201;
            public const int status204 = 204;
            public const int status302 = 302;
            public const int status400 = 400;
            public const int status401 = 401;
            public const int status404 = 404;
            public const int status500 = 500;

        }

        public class ResponseMessage
        {
            public const string AuthRequired = "Authorization es requerido";
            public const string NoAutorizado = "Acción no autorizada";
            public const string ErrorInterno = "Ocurrió un error";
            public const string InsercionCorrecta = "Se ha insertado correctamente";
            public const string ActualizacionCorrecta = "Se ha actualizado correctamente";
            public const string EliminaciónCorrecta = "Se ha eliminado correctamente";
         
        }
    }

}
