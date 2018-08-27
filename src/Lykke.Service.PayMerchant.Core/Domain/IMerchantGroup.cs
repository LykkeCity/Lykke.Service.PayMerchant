namespace Lykke.Service.PayMerchant.Core.Domain
{
    public interface IMerchantGroup
    {
        string Id { get; }

        string DisplayName { get; }

        string OwnerId { get; }

        string Merchants { get; }

        MerchantGroupUse MerchantGroupUse { get; }
    }
}
