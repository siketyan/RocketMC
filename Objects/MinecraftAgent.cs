using Newtonsoft.Json;

namespace RocketMC.Objects
{
    /// <summary>
    /// Minecraftエージェント
    /// </summary>
    public class MinecraftAgent
    {
        /// <summary>
        /// エージェントの名称の定数
        /// </summary>
        [JsonProperty("name")]
        public string Name => "minecraft";

        /// <summary>
        /// エージェントのバージョンの定数
        /// </summary>
        [JsonProperty("version")]
        public int Version => 1;
    }
}