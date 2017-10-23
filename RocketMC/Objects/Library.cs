using System.Collections.Generic;
using Newtonsoft.Json;
using RocketMC.Enums;

namespace RocketMC.Objects
{
    /// <summary>
    /// ライブラリ
    /// </summary>
    public class Library
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 各OSに対するネイティブライブラリの対応状況のディクショナリ
        /// </summary>
        [JsonProperty("natives")]
        public Dictionary<NativeOSType, string> Natives { get; set; }

        /// <summary>
        /// ネイティブライブラリのインストールルールの配列
        /// </summary>
        [JsonProperty("rules")]
        public NativeRule[] NativeRules { get; set; }

        /// <summary>
        /// ネイティブライブラリの展開ルール
        /// </summary>
        [JsonProperty("extract")]
        public ExtractRule ExtractRule { get; set; }

        /// <summary>
        /// ダウンロード情報
        /// </summary>
        [JsonProperty("downloads")]
        public LibraryDownloads Downloads { get; set; }
    }
}