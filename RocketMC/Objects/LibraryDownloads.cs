using System.Collections.Generic;
using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// ライブラリのダウンロード情報の一覧
    /// </summary>
    public class LibraryDownloads
    {
        /// <summary>
        /// ネイティブライブラリのダウンロード情報の一覧
        /// </summary>
        [JsonProperty("classifiers")]
        public Dictionary<string, LibraryDownloadLocation> Classifiers { get; set; }

        /// <summary>
        /// アーティファクトライブラリのダウンロード情報
        /// </summary>
        [JsonProperty("artifact")]
        public LibraryDownloadLocation Artifact { get; set; }
    }
}