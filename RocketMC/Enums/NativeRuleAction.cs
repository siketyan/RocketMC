using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RocketMC.Enums
{
    /// <summary>
    /// ネイティブライブラリのインストールルールに対するアクションの種類
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NativeRuleAction
    {
        /// <summary>
        /// インストールを許可する
        /// </summary>
        [EnumMember(Value = "allow")]
        Allow,

        /// <summary>
        /// インストールを許可しない
        /// </summary>
        [EnumMember(Value = "disallow")]
        Disallow
    }
}