using Hein.Framework.Dynamo.Entity;
using System;
using System.Collections.Generic;

namespace Hein.RulesEngine.Domain.Models
{
    public class RuleDefinition : IEntity
    {
        public Guid RuleId { get; private set; }
        public string Name { get; set; }
        public List<EntityProperty> Properties { get; set; }
        public string Code { get; set; }

        public Guid GetId()
        {
            return RuleId;
        }

        public void SetId(Guid id)
        {
            RuleId = id;
        }

        public void ExecuteAfterGet()
        {
            //ope
        }

        public void ExecuteAfterSave()
        {
            //ope
        }

        public void ExecuteBeforeSave()
        {
            //ope
        }
    }
}
