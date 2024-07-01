using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BambuLab.Sdk.Json;

namespace BambuLab.Sdk.Http;

public class AccessTokenData
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
    public int RefreshExpiresIn { get; set; }
    public string TfaKey { get; set; }
    public string AccessMethod { get; set; }

    protected virtual IJsonSerializer JsonSerializer => _defaultJsonSerializer;
    private readonly IJsonSerializer _defaultJsonSerializer = new DefaultJsonSerializer();

    public AccessTokenData()
    {

    }

    public virtual DateTimeOffset GetExpireOn()
    {
        var expireOn = Convert.ToInt64(GetDataFromAccessToken("exp"));
        return DateTimeOffset.FromUnixTimeSeconds(expireOn);
    }

    public virtual string GetUserName()
    {
        return GetDataFromAccessToken("username").ToString();
    }

    public virtual bool IsExpired()
    {
        var expireOn = GetExpireOn();
        return expireOn < DateTimeOffset.Now;
    }

    protected virtual string GetDataFromAccessToken(string key)
    {
        string b64String = AccessToken.Split('.')[1];
        b64String += new string('=', (4 - b64String.Length % 4) % 4);
        var playload = JsonSerializer.Deserialize<Dictionary<string, object>>(Encoding.UTF8.GetString(Convert.FromBase64String(b64String)));
        return playload[key].ToString();
    }
}
