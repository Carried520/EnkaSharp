using System.Text.Json;

namespace EnkaSharp;

internal static class JsonSettings
{
    internal static readonly JsonSerializerOptions CamelCase = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    internal static JsonSerializerOptions SnakeCase = new()
        { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
}