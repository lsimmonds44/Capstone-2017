using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCPresentationLayer.Startup))]
namespace MVCPresentationLayer
{

    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Class for StartUP
    /// </summary>
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
