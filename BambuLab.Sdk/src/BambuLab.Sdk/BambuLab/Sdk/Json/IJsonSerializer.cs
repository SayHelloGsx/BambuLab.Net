using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BambuLab.Sdk.Json;

public interface IJsonSerializer
{
    Task<T> DeserializeAsync<T>(string json, bool camelCase = true);
    T Deserialize<T>(string json, bool camelCase = true);
    Task<string> SerializeAsync<T>(T data, bool camelCase = true, bool indented = false);
    string Serialize<T>(T data, bool camelCase = true, bool indented = false);
}
