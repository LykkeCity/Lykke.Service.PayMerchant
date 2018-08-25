namespace Lykke.Service.PayMerchant.Core.Domain
{
    public class MerchantGroup : IMerchantGroup
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string OwnerId { get; set; }

        public string Merchants { get; set; }

        public MerchantGroupUse MerchantGroupUse { get; set; }
    }
}
