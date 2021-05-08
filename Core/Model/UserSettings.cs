using System.Text.Json.Serialization;

namespace Core.Model
{
    public class UserSettings
    {
        [JsonPropertyName("synchronization_enabled")]
        public bool SynchronizationEnabled { get; set; } = false;

        [JsonPropertyName("remote_address")]
        public string RemoteAddress { get; set; } = string.Empty;

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; } = string.Empty;

        [JsonPropertyName("secret")]
        public string Secret { get; set; } = string.Empty;

        [JsonPropertyName("encryption_enabled")]
        public bool EncryptionEnabled { get; set; } = false;

        [JsonPropertyName("encryption_key")]
        public string EncryptionKey { get; set; } = string.Empty;
    }
}
