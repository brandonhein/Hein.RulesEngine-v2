using Hein.Framework.Http;
using Hein.Framework.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Framework.Providers
{
    public interface ICodeConversionProvider
    {
        Task<CodeConversionResult> ConvertAsync(CodeConversionRequest request);
    }

    public class CodeConversionProvider : ICodeConversionProvider
    {
        private readonly IApiService _service;
        private const string _url = "https://codeconverter.icsharpcode.net/api/converter/";
        public CodeConversionProvider(IApiService service)
        {
            _service = service;
        }

        public async Task<CodeConversionResult> ConvertAsync(CodeConversionRequest request)
        {
            var json = request.ToJson();
            var apiRequest = new ApiRequest(_url)
            {
                Accept = HttpContentType.Json,
                ContentType = HttpContentType.Json,
                Method = HttpMethod.Post,
                RequestBody = json
            };

            var response = await _service.New(apiRequest).ResponseAsync();
            return Deserialize.JsonToObject<CodeConversionResult>(response.ResponseString);
        }
    }

    public class CodeConversionRequest
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("requestedConversion")]
        public string Conversion { get; set; }
    }

    public class CodeConversionResult
    {
        [JsonProperty("conversionOk")]
        public bool IsSuccess { get; set; }
        [JsonProperty("convertedCode")]
        public string Code { get; set; }

        public bool HasInternalError()
        {
            return Code.Contains("ICSharpCode.CodeConverter");
        }
    }
}
