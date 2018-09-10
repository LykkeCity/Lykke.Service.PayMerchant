namespace Lykke.Service.PayMerchant.Models
{
    /// <summary>
    /// Merchant model
    /// </summary>
    public class MerchantModel
    {
        /// <summary>
        /// Gets or sets merchant id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets merchant name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets merchant display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets merchant api key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets merchant Lykke wallet id
        /// </summary>
        public string LwId { get; set; }

        /// <summary>
        /// Gets or sets merchant's email address
        /// </summary>
        public string Email { get; set; }
    }
}
