using System.Web.Http;
using WebActivatorEx;
using APIPetCare;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace APIPetCare
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {

                        c.SingleApiVersion("v1", "APIPetCare");

                        // 🔐 Agregar configuración para JWT
                        c.ApiKey("Bearer")
                         .Description("Autenticación JWT usando el esquema Bearer. Ejemplo: \"Bearer {token}\"")
                         .Name("Authorization")
                         .In("header");

                    })
                .EnableSwaggerUi(c =>
                    {
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }
}
