using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetCare_CasoEstudio2.Startup))]
namespace PetCare_CasoEstudio2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
