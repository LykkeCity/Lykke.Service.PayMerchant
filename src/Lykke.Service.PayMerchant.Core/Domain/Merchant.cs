namespace Lykke.Service.PayMerchant.Core.Domain
{
    public class Merchant : IMerchant
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string PublicKey { get; set; }

        public string ApiKey { get; set; }

        public string LwId { get; set; }
    }
}
