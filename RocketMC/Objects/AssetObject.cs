using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// アセットオブジェクト
    /// </summary>
    public class AssetObject
    {
        /// <summary>
        /// ハッシュ値
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// サイズ
        /// </summary>
        [JsonProperty("size")]
        public ulong Size { get; set; }
    }
}