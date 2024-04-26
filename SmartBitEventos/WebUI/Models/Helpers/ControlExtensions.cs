
using RazorLight;
using WebApp.Models.Controls;
namespace WebApp.Helpers
{
    public static class ControlExtensions
    {
        public static string RenderViewToString(string viewName, object model)
        {
            var viewsPath = Path.Combine(Directory.GetCurrentDirectory(), "Models", "Controls");

            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(viewsPath) // Ruta a la carpeta donde se encuentran las vistas
                .UseMemoryCachingProvider()
                .Build();

            var result = engine.CompileRenderAsync($"{viewName}", model).GetAwaiter().GetResult();
            return result;
        }

        public static string CtrlChart(string viewName, string id, string title, string xAxis, string yAxis, string series)
        {
            var ctrl = new CtrlChartModel
            {
                ViewName = viewName,
                Id = id,
                Title = title,
                XAxis = xAxis,
                YAxis = yAxis,
                Series = series
            };

            return RenderViewToString("CtrlChartModel", ctrl);
        }

        public static string CtrlCard(string viewName, string id, string title, string amount,
            string subAmount, string subTitle, string dropDown, string icon)
        {
            var ctrl = new CtrlCardModel
            {
                ViewName = viewName,
                Id = id,
                Title = title,
                Amount = amount,
                SubAmount = subAmount,
                SubTitle = subTitle,
                DropDown = dropDown,
                Icon = icon
            };

            return RenderViewToString("CtrlCardModel", ctrl);
        }
    }
}