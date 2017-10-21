using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// ファイルのダウンロード情報
    /// </summary>
    public class DownloadLocation
    {
        /// <summary>
        /// SHA-1ハッシュ値
        /// </summary>
        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// サイズ
        /// </summary>
        [JsonProperty("size")]
        public ulong Size { get; set; }
    }
}