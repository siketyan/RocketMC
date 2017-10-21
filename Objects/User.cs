using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// ユーザ
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}