using Common;

namespace Lykke.Service.PayMerchant.Core
{
    public static class LogExtensions
    {
        public static string ToDetails(this object src)
        {
            return $"Details: {src?.ToJson()}";
        }
    }
}
