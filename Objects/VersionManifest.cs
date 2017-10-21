using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// バージョンマニフェスト
    /// </summary>
    public class VersionManifest
    {
        /// <summary>
        /// 最新バージョン
        /// </summary>
        [JsonProperty("latest")]
        public LatestVersion Latest { get; set; }

        /// <summary>
        /// バージョンの配列
        /// </summary>
        [JsonProperty("versions")]
        public Version[] Versions { get; set; }
    }
}