namespace Lykke.Service.PayMerchant.Core.Domain
{
    public interface IMerchant
    {
        string Id { get; }

        string Name { get; set; }

        string DisplayName { get; set; }

        string ApiKey { get; set; }

        string LwId { get; set; }

        string Email { get; set; }
    }
}
