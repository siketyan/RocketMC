using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RocketMC.Enums
{
    /// <summary>
    /// ネイティブライブラリのインストール先OSの種類
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NativeOSType
    {
        /// <summary>
        /// Microsoft Windows
        /// </summary>
        [EnumMember(Value = "windows")]
        Windows,

        /// <summary>
        /// Apple Mac OS X
        /// </summary>
        [EnumMember(Value = "osx")]
        OSX,

        /// <summary>
        /// Linux
        /// 派生ディストリビューションも含みます。
        /// </summary>
        [EnumMember(Value = "linux")]
        Linux
    }
}