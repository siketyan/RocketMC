using System;
using Newtonsoft.Json;
using RocketMC.Enums;

namespace RocketMC.Objects
{
    /// <summary>
    /// バージョン
    /// </summary>
    public class Version
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 種類
        /// </summary>
        [JsonProperty("type")]
        public VersionType Type { get; set; }

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
        /// バージョンの詳細のURL
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}