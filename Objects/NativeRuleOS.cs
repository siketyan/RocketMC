using Newtonsoft.Json;
using RocketMC.Enums;

namespace RocketMC.Objects
{
    /// <summary>
    /// ネイティブライブラリのインストールルールを適用するOS
    /// </summary>
    public class NativeRuleOS
    {
        /// <summary>
        /// OSの種類
        /// </summary>
        [JsonProperty("name")]
        public NativeOSType Type { get; set; }
    }
}