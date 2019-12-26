using Hein.RulesEngine.Framework.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Hein.RulesEngine.Framework.Providers;

namespace Hein.RulesEngine.Domain.Magic
{
    //trial with using ICSharpCode.CodeConverter
    //public static class AdminToEngineCode
    //{
    //    public static string Convert(this string adminCode)
    //    {
    //        return ConvertAsync(adminCode).Result;
    //    }
    //
    //    public static async Task<string> ConvertAsync(this string adminCode)
    //    {
    //        var valueKeys = adminCode.FindMatches("[", "]").Distinct();
    //        foreach (var valueKey in valueKeys)
    //        {
    //            adminCode = adminCode.Replace($"[{valueKey}]", $"[___{valueKey}___]");
    //        }
    //
    //        var codeWithOptions = new CodeWithOptions(adminCode)
    //           //.WithTypeReferences(DefaultReferences.)
    //           .SetFromLanguage("Visual Basic", 14)
    //           .SetToLanguage("C#", 6);
    //        var result = await CodeConverter.Convert(codeWithOptions);
    //        return result.ConvertedCode;
    //    }
    //}

    public interface IAdminToEngineCodeConversion
    {
        Task<string> ConvertAsync(string adminCode);
    }

    public class AdminToEngineCodeConversion : IAdminToEngineCodeConversion
    {
        private readonly ICodeConversionProvider _provider;
        public AdminToEngineCodeConversion(ICodeConversionProvider provider)
        {
            _provider = provider;
        }

        public async Task<string> ConvertAsync(string adminCode)
        {
            var engineCode = string.Empty;

            var valueKeys = adminCode.FindMatches("[", "]").Distinct();
            foreach (var valueKey in valueKeys)
            {
                adminCode = adminCode.Replace($"[{valueKey}]", $"[___f_{valueKey}_e___]");
            }

            var request = new CodeConversionRequest()
            {
                Code = adminCode,
                Conversion = "vbnet2cs"
            };

            var result = await _provider.ConvertAsync(request);
            
            if (result.IsSuccess && !result.HasInternalError())
            {
                engineCode = result.Code.Replace("\r", "").Replace("\n", "");
            }

            if (string.IsNullOrEmpty(engineCode))
            {
                throw new System.Exception();
            }


            engineCode = engineCode.Replace("___f_", "Values[\"").Replace("_e___", "\"]");

            //engineCode = engineCode.Substring(1, engineCode.Length - 1);
            engineCode = engineCode.Trim();
            return engineCode;
        }
    }
}
