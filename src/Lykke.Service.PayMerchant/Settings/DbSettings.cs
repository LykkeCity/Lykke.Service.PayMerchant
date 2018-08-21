using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PayMerchant.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
