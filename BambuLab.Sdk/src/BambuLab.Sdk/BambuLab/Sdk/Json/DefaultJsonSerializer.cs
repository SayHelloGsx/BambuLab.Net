using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Concurrent;

namespace BambuLab.Sdk.Json;

public class DefaultJsonSerializer : IJsonSerializer
{
    public async Task<T> DeserializeAsync<T>(string json, bool camelCase = true)
    {
        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, CreateJsonSerializerOptions(camelCase));
        }
    }

    public T Deserialize<T>(string json, bool camelCase = true)
    {
        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            return JsonSerializer.Deserialize<T>(stream, CreateJsonSerializerOptions(camelCase));
        }
    }

    public async Task<string> SerializeAsync<T>(T data, bool camelCase = true, bool indented = false)
    {
        using (var stream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(stream, data, CreateJsonSerializerOptions(camelCase, indented));
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    public string Serialize<T>(T data, bool camelCase = true, bool indented = false)
    {
        using (var stream = new MemoryStream())
        {
            JsonSerializer.Serialize(stream, data, CreateJsonSerializerOptions(camelCase, indented));
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    private static readonly ConcurrentDictionary<object, JsonSerializerOptions> JsonSerializerOptionsCache = new ConcurrentDictionary<object, JsonSerializerOptions>();

    protected virtual JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = true, bool indented = false)
    {
        return JsonSerializerOptionsCache.GetOrAdd(new
        {
            camelCase,
            indented,
        }, _ =>
        {
            var settings = new JsonSerializerOptions();

            if (camelCase)
            {
                settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }

            if (indented)
            {
                settings.WriteIndented = true;
            }

            return settings;
        });
    }
}
