using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System.Configuration;
using System.Web;

namespace MyAIWebSite.ApplicationInsights
{
    public class TelemetryInitializer : ITelemetryInitializer
    {
        private ITelemetry telemetry;
        public void Initialize(ITelemetry telemetry)
        {
            this.telemetry = telemetry;

            DefinirRoleName();
            AdicionarReferrer();
        }

        private void AdicionarReferrer()
        {
            var context = HttpContext.Current;
            if (context?.Request != null)
            {
                var referrer = context.Request.UrlReferrer?.ToString();
                if (!string.IsNullOrWhiteSpace(referrer))
                    telemetry.Context.GlobalProperties.Add("referrer", referrer);
            }
        }
        private void DefinirRoleName()
        {
            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
                telemetry.Context.Cloud.RoleName = ConfigurationManager.AppSettings["RoleName"].ToString();
        }
    }
}