using System.Collections.Generic;

namespace RocketMC.Objects
{
    /// <summary>
    /// ライブラリのダウンロード結果
    /// </summary>
    public class LibrariesDownloadResult
    {
        /// <summary>
        /// クラスパスに追加しなければいけないライブラリの場所のリスト
        /// </summary>
        public List<string> Classpath { get; set; }

        /// <summary>
        /// 展開しなければいけないネイティブライブラリの場所と展開ルールのディクショナリ
        /// </summary>
        public Dictionary<string, ExtractRule> Extracts { get; set; }
    }
}