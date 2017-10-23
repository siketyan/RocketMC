using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RocketMC.Enums
{
    /// <summary>
    /// バージョンの種類
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VersionType
    {
        /// <summary>
        /// 正式版
        /// </summary>
        [EnumMember(Value = "release")]
        Release,

        /// <summary>
        /// スナップショット版
        /// </summary>
        [EnumMember(Value = "snapshot")]
        Snapshot,

        /// <summary>
        /// アルファ版（旧）
        /// </summary>
        [EnumMember(Value = "old_alpha")]
        Alpha,

        /// <summary>
        /// ベータ版（旧）
        /// </summary>
        [EnumMember(Value = "old_beta")]
        Beta
    }
}