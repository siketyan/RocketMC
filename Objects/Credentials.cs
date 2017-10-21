using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// 認証情報
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// アクセストークン
        /// </summary>
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// クライアントトークン
        /// </summary>
        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }

        /// <summary>
        /// 選択されたプロファイル
        /// </summary>
        [JsonProperty("selectedProfile")]
        public Profile SelectedProfile { get; set; }

        /// <summary>
        /// ユーザが持つプロファイルの配列
        /// </summary>
        [JsonProperty("availableProfiles")]
        public Profile[] AvailableProfiles { get; set; }

        /// <summary>
        /// ユーザ
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }
    }
}