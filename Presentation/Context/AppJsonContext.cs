using Application.Dto.Directory.Collections;
using Application.Dto.Directory.Info;
using Application.Dto.Directory.Security.Windows;
using Presentation.Dto.Requests;
using Sudzekai.WebOs.Core;
using System.Text.Json.Serialization;

namespace Presentation.Context
{
    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(PluginInfo))]
    [JsonSerializable(typeof(DirectoryFilesDto))]
    [JsonSerializable(typeof(DirectoryDirectoriesDto))]
    [JsonSerializable(typeof(DirectoryGeneralInfoDto))]
    [JsonSerializable(typeof(WindowsDirectorySecurityInfo))]
    [JsonSerializable(typeof(CreateDirectoryRequest))]
    [JsonSerializable(typeof(DeleteDirectoryRequest))]
    internal partial class PluginJsonContext : JsonSerializerContext;
}
