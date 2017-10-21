using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// プロファイル
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}