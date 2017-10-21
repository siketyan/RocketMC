using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// 認証情報の検証リクエスト
    /// </summary>
    public class ValidateRequest
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
        /// 新しい検証リクエストを作成します。
        /// </summary>
        /// <param name="accessToken">アクセストークン</param>
        /// <param name="clientToken">クライアントトークン</param>
        public ValidateRequest(string accessToken, string clientToken)
        {
            AccessToken = accessToken;
            ClientToken = clientToken;
        }
    }
}