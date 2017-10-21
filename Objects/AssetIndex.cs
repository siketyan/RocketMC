using System.Collections.Generic;
using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// アセットの一覧
    /// </summary>
    public class AssetIndex
    {
        /// <summary>
        /// アセットの名称とアセットオブジェクトのディクショナリ
        /// </summary>
        [JsonProperty("objects")]
        public Dictionary<string, AssetObject> Objects { get; set; }
    }
}