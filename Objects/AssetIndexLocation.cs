using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// アセットの一覧のダウンロード情報
    /// </summary>
    public class AssetIndexLocation : DownloadLocationWithId
    {
        /// <summary>
        /// アセットの合計サイズ
        /// </summary>
        [JsonProperty("totalSize")]
        public ulong TotalSize { get; set; }
    }
}