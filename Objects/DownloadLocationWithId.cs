using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// ファイルのIDを含むダウンロード情報
    /// </summary>
    public class DownloadLocationWithId : DownloadLocation
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}