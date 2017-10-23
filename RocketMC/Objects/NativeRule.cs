using Newtonsoft.Json;
using RocketMC.Enums;

namespace RocketMC.Objects
{
    /// <summary>
    /// ネイティブのインストールルール
    /// </summary>
    public class NativeRule
    {
        /// <summary>
        /// ルールに対するアクション
        /// </summary>
        [JsonProperty("action")]
        public NativeRuleAction Action { get; set; }

        /// <summary>
        /// ルールを適用するOS
        /// </summary>
        [JsonProperty("os")]
        public NativeRuleOS OS { get; set; }
    }
}