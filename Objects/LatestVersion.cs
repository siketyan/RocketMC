using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// 正式版、スナップショット版それぞれに対する最新バージョンのID
    /// </summary>
    public class LatestVersion
    {
        /// <summary>
        /// スナップショット版
        /// </summary>
        [JsonProperty("snapshot")]
        public string Snapshot { get; set; }

        /// <summary>
        /// 正式版
        /// </summary>
        [JsonProperty("release")]
        public string Release { get; set; }
    }
}