using Hein.Framework.Dynamo;
using Hein.Framework.Dynamo.Criterion;
using Hein.Framework.Dynamo.Entity;
using Hein.RulesEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Domain.Repositories
{
    public interface IRuleDefinitionRepository
    {
        Task<IEnumerable<RuleDefinition>> GetDefinitionsAsync();
        Task<RuleDefinition> GetDefinitionByIdAsync(string id);
        Task<RuleDefinition> GetDefinitionByNameAsync(string name);
    }

    public class RuleDefinitionRepository : IRuleDefinitionRepository
    {
        private readonly EntityRepository<RuleDefinition> _repository;
        public RuleDefinitionRepository(IRepositoryContext context)
        {
            _repository = new EntityRepository<RuleDefinition>(context);
        }

        public IEnumerable<RuleDefinition> GetDefinitions()
        {
            return _repository.GetAll();
        }

        public Task<IEnumerable<RuleDefinition>> GetDefinitionsAsync()
        {
            return Task.FromResult(GetDefinitions());
        }

        public RuleDefinition GetDefinitionById(string id)
        {
            if (Guid.TryParse(id, out var guidId))
            {
                var query = QueryOver.Of<RuleDefinition>()
                    .Where(x => x.RuleId == guidId)
                    .Top(1);

                return _repository.Find(query).FirstOrDefault(x => x.RuleId == guidId);
            }

            throw new KeyNotFoundException();
        }

        public Task<RuleDefinition> GetDefinitionByIdAsync(string id)
        {
            return Task.FromResult(GetDefinitionById(id));
        }

        public RuleDefinition GetDefinitionByName(string name)
        {
            var query = QueryOver.Of<RuleDefinition>()
                .Where(x => x.Name == name)
                .Top(1);

            return _repository.Find(query).FirstOrDefault(x => x.Name == name);
        }

        public Task<RuleDefinition> GetDefinitionByNameAsync(string name)
        {
            return Task.FromResult(GetDefinitionById(name));
        }
    }
}
