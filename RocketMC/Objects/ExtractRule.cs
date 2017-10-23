using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// ネイティブライブラリの展開ルール
    /// </summary>
    public class ExtractRule
    {
        /// <summary>
        /// 除外するファイル名の配列
        /// </summary>
        [JsonProperty("exclude")]
        public string[] Excludes { get; set; }
    }
}