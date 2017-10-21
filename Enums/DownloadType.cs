using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RocketMC.Enums
{
    /// <summary>
    /// バージョンのダウンロードURLに対する種類
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DownloadType
    {
        /// <summary>
        /// クライアント
        /// </summary>
        [EnumMember(Value = "client")]
        Client,

        /// <summary>
        /// サーバ
        /// </summary>
        [EnumMember(Value = "server")]
        Server,

        /// <summary>
        /// Windowsサーバ
        /// Windows向けにサーバとは別にリリースされた場合に使用します。
        /// </summary>
        [EnumMember(Value = "windows_server")]
        WindowsServer
    }
}