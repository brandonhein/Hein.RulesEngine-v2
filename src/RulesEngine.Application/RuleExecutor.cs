using Hein.RulesEngine.Application.Model;
using Hein.RulesEngine.Domain.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Application
{
    public class RuleExecutor
    {
        private readonly RuleDefinition _definition;
        public RuleExecutor(RuleDefinition definition)
        {
            _definition = definition;
        }

        public async Task<RuleModel> ApplyAsync(RuleModel model)
        {
            var assemblies = ScriptOptions.Default;
            //WithImports = "using System.Math" at the top of the script
            assemblies = assemblies.WithImports(
                "System.Math",
                "Hein.Framework.Extensions",
                "Hein.RulesEngine.Framework.Extensions");
            //WithReferences = pulling in actual .dlls outside of System
            assemblies = assemblies.WithReferences(
                typeof(Hein.Framework.Extensions.GenericExtensions).Assembly,
                typeof(Framework.Extensions.ConversionExtensions).Assembly);

            var script = CSharpScript.Create(_definition.EngineCode, assemblies, globalsType: typeof(RuleModel));
            script.Compile();
            await script.RunAsync(model);
            return model;
        }
    }
}
