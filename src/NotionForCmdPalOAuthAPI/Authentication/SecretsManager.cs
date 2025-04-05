using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace NotionForCmdPalOAuthAPI.Authentication;

internal static class SecretsManager
{
    internal static async Task<KeyVaultSecret> GetSecretAsync(string secretName)
    {
        string kvUri = Environment.GetEnvironmentVariable("KeyVaultUrl")!;
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        return await client.GetSecretAsync(secretName);
    }
}
