using System.Configuration;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace SbManager
{
    public interface IConfig
    {
        string BusConnectionString { get; }
        string WebAppUrl { get; }
    }
    public class Config : IConfig
    {
        private static string _connectionString = null;
        public string BusConnectionString
        {
            get
            {
                if (_connectionString != null) return _connectionString;

                var connectionString = ConfigurationManager.ConnectionStrings["busConnectionString"].ConnectionString;


                if (connectionString == "local")
                    connectionString = BuildLocalEndpoint();
                else if (new Regex("^[a-z]:|^//", RegexOptions.IgnoreCase).IsMatch(connectionString))
                    connectionString = File.ReadAllText(connectionString);

                _connectionString = connectionString;
                return _connectionString;
            }
        }
        public string WebAppUrl
        {
            get { return ConfigurationManager.AppSettings["WebAppUrl"]; }
        }

        private static string BuildLocalEndpoint()
        {
            var hostname = GetHostnameAndDomainName();
            return $"Endpoint=sb://{hostname}/FamsBus;StsEndpoint=https://{hostname}:9355/FamsBus;RuntimePort=9354;ManagementPort=9355;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xadfN52mfTvng5HJak8Dnv9dfg9wizCXPqtpYi05Iwo=";
        }


        public static string GetHostnameAndDomainName()
        {
            // if No domain name return a generic string           
            string currentDomainName = IPGlobalProperties.GetIPGlobalProperties().DomainName ?? "nodomainname";
            string hostName = Dns.GetHostName();

            // check if current hostname does not contain domain name
            if (!hostName.Contains(currentDomainName))
            {
                hostName = hostName + "." + currentDomainName;
            }
            return hostName.ToLower();  // Return combined hostname and domain in lowercase
        }
    }
}
