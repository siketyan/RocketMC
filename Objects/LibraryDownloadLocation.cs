using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// ライブラリのダウンロード情報
    /// </summary>
    public class LibraryDownloadLocation : DownloadLocation
    {
        /// <summary>
        /// ライブラリのダウンロード先
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}