using Hein.RulesEngine.Application;
using Hein.RulesEngine.Application.Model;
using Hein.RulesEngine.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Web.Controllers
{
    [Route("Execute")]
    public class ExecuteController : Controller
    {
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RuleModel), 200)]
        public async Task<IActionResult> Index([FromBody] RuleModel request)
        {
            try
            {
                var sampleDefinition = new RuleDefinition()
                {
                    //EngineCode = "{if (Values[\"IsCallEvent\"].ToString() == \"true\" & ((long)Values[\"StatusId\"] == 8005 | (long)Values[\"StatusId\"] == 8010))    {        if ((long)Values[\"CallDuration\"] >= 127)            Values[\"StatusId\"] = (long)8015;    }}"
                    //EngineCode = "{    if (Values[\"IsCallEvent\"].ToBool() == true & (Values[\"StatusId\"].ToInt() == 8005 | Values[\"StatusId\"].ToInt() == 8010))    {        if (Values[\"CallDuration\"].ToNumber() >= 127)            Values[\"StatusId\"] = 8015;        else if (Values[\"CallDuration\"].ToNumber() < 127         )            Values[\"StatusId\"] = 8010;    }}"
                    EngineCode = "{    if (Values[\"IsCallEvent\"].ToBool() == true & Values[\"StatusId\"].ToInt().IsOneOf(8005, 8010))    {        if (Values[\"CallDuration\"].ToNumber() >= 127)            Values[\"StatusId\"] = 8015;        else if (Values[\"CallDuration\"].ToNumber() < 127         )            Values[\"StatusId\"] = 8010;    }}"
                };

                var executor = new RuleExecutor(sampleDefinition);
                var result = await executor.ApplyAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}
