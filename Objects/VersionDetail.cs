using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RocketMC.Enums;
using RocketMC.Utilities;

namespace RocketMC.Objects
{
    /// <summary>
    /// バージョンの詳細
    /// </summary>
    public class VersionDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// アセットのID
        /// </summary>
        [JsonProperty("assets")]
        public string AssetsId { get; set; }

        /// <summary>
        /// アセットの一覧のダウンロード情報
        /// </summary>
        [JsonProperty("assetIndex")]
        public AssetIndexLocation AssetIndex { get; set; }

        /// <summary>
        /// ダウンロードURLに対する種類とダウンロード情報のディクショナリ
        /// </summary>
        [JsonProperty("downloads")]
        [JsonConverter(typeof(DictionaryEnumKeyConverter))]
        public Dictionary<DownloadType, DownloadLocation> Downloads { get; set; }

        /// <summary>
        /// ライブラリの配列
        /// </summary>
        [JsonProperty("libraries")]
        public Library[] Libraries { get; set; }

        /// <summary>
        /// メインクラスのパス
        /// </summary>
        [JsonProperty("mainClass")]
        public string MainClass { get; set; }

        /// <summary>
        /// 起動に使用する引数
        /// </summary>
        [JsonProperty("minecraftArguments")]
        public string MinecraftArguments { get; set; }

        /// <summary>
        /// 最終更新日時
        /// TODO: 未確定
        /// </summary>
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// リリース日時
        /// </summary>
        [JsonProperty("releaseTime")]
        public DateTime ReleaseTime { get; set; }

        /// <summary>
        /// 種類
        /// </summary>
        [JsonProperty("type")]
        public VersionType Type { get; set; }
    }
}