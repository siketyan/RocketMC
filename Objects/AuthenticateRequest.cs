using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// 認証リクエスト
    /// </summary>
    public class AuthenticateRequest
    {
        /// <summary>
        /// Minecraftエージェントを示す定数
        /// </summary>
        [JsonProperty("agent")]
        public MinecraftAgent Agent => new MinecraftAgent();

        /// <summary>
        /// ユーザ名
        /// Mojangアカウントを使用する場合はメールアドレスとなります。
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
        
        /// <summary>
        /// パスワード
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// ユーザオブジェクトをリクエストするかどうか
        /// </summary>
        [JsonProperty("requestUser")]
        public bool RequestUser => true;


        /// <summary>
        /// 新しい認証リクエストを作成します。
        /// </summary>
        /// <param name="username">ユーザ名</param>
        /// <param name="password">パスワード</param>
        public AuthenticateRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}