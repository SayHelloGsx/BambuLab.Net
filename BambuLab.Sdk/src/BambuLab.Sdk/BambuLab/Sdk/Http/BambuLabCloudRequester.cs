using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using BambuLab.Sdk.Exceptions;
using BambuLab.Sdk.Json;

namespace BambuLab.Sdk.Http;

public abstract class BambuLabCloudRequester
{
    protected virtual string Account { get; }
    protected virtual string Password { get; }
    protected virtual HttpClient HttpClient { get; }
    protected virtual IJsonSerializer JsonSerializer => _defaultJsonSerializer;
    protected virtual AccessTokenData AccessTokenData { get; set; }

    public virtual string AccessToken => AccessTokenData?.AccessToken;
    public virtual string UserName => AccessTokenData?.GetUserName();

    protected abstract string Domain { get; }

    private const string _mediaType = "application/json";
    private readonly IJsonSerializer _defaultJsonSerializer = new DefaultJsonSerializer();

    protected BambuLabCloudRequester(string account, string password)
    {
        Account = account;
        Password = password;

        HttpClient = new HttpClient()
        {
            BaseAddress = new Uri(Domain)
        };
    }

    public virtual async Task LoginAsync()
    {
        AccessTokenData = await GetAuthenticationTokenAsync<AccessTokenData>();
    }

    public virtual Task<bool> IsLoggedInAsync()
    {
        return Task.FromResult(AccessTokenData != null && !AccessTokenData.IsExpired());
    }

    public virtual async Task<DeviceListResponseData> GetDeviceList()
    {
        await SetAuthenticationHeaderAsync();

        var response = await HttpClient.GetAsync(BambuLabUrls.DeviceListUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new BambuLabHttpException(ErrorCodes.Http.HttpRequestFailed).WithData("StatusCode", response.StatusCode.ToString());
        }

        var bodyContent = await response.Content.ReadAsStringAsync();
        var deviceListResponse = await JsonSerializer.DeserializeAsync<DeviceListResponseData>(bodyContent);

        if (!deviceListResponse.IsSuccess())
        {
            throw new BambuLabHttpException(ErrorCodes.Http.ApiRequestFailed)
                .WithData("message", deviceListResponse.Message)
                .WithData("code", deviceListResponse.Code)
                .WithData("error", deviceListResponse.Error);
        }

        return deviceListResponse;
    }

    protected virtual async Task<T> GetAuthenticationTokenAsync<T>() where T : AccessTokenData
    {
        HttpClient.DefaultRequestHeaders.Clear();

        var requestData = new Dictionary<string, string>
        {
            { "account", Account },
            { "password", Password }
        };

        var response = await HttpClient.PostAsync(BambuLabUrls.LoginUrl, new StringContent(await JsonSerializer.SerializeAsync(requestData), Encoding.UTF8, _mediaType));

        if (!response.IsSuccessStatusCode)
        {
            throw new BambuLabHttpException(ErrorCodes.Http.LoginFailed);
        }

        var bodyContent = await response.Content.ReadAsStringAsync();
        var accessTokenData = await JsonSerializer.DeserializeAsync<T>(bodyContent);
        return accessTokenData;
    }

    protected virtual async Task SetAuthenticationHeaderAsync()
    {
        HttpClient.DefaultRequestHeaders.Clear();

        if (await IsLoggedInAsync())
        {
            HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessTokenData.AccessToken);
        }
    }
}
