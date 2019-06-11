using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboPlant.Application.Design;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Domain.Design;
using RoboPlant.InMemoryPersistence.Models;
using SharpRepository.InMemoryRepository;

namespace RoboPlant.InMemoryPersistence
{
    public class RobotBlueprintRepository : IRobotBlueprintRepository
    {
        private readonly InMemoryRepository<RobotBlueprintModel, Guid> internalRepository;

        public RobotBlueprintRepository()
        {
            this.internalRepository = new InMemoryRepository<RobotBlueprintModel, Guid>
            {
                GenerateKeyOnAdd = true
            };
            AddDemoData();
        }

        private void AddDemoData()
        {
            var insertItems = new List<RobotBlueprintModel>
            {
                new RobotBlueprintModel
                {
                    Id = new Guid("AAAEF8DA-C0CC-4284-B50F-086E468A3782"),
                    Version = 1,
                    HumanReadableName = "Bender",
                    Description = "Robot specialized to bending steel and rules.",
                    ProductionEfford = 30
                },
                new RobotBlueprintModel
                {
                    Id = new Guid("9632D538-B44E-4034-B6CA-19EE4441000B"),
                    Version = 4,
                    HumanReadableName = "R2D2",
                    Description = "Beeps a lot.",
                    ProductionEfford = 34
                },
                new RobotBlueprintModel
                {
                    Id = new Guid("B9F4935E-0D8B-49D6-B180-042E4B9CC7B8"),
                    Version = 1,
                    HumanReadableName = "Bishop 341-B",
                    Description = "Human like android.",
                    ProductionEfford = 65
                },
                new RobotBlueprintModel
                {
                    Id = new Guid("D8404EFA-1FFE-41F2-8657-FAF0D265FD7A"),
                    Version = 1,
                    HumanReadableName = "T-800",
                    Description = "He'll be back.",
                    ProductionEfford = 40
                }

            };

            this.internalRepository.Add(insertItems);
        }

        public Task<GetAllResult<RobotBlueprint>> GetAll()
        {
            IEnumerable<RobotBlueprintModel> result;
            try
            {
                result = this.internalRepository.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult<GetAllResult<RobotBlueprint>>(new GetAllResult<RobotBlueprint>.Error(e));
            }

            var resultList = result
                .Select(m => new RobotBlueprint(new RobotBlueprintId(m.Id), m.Version, m.HumanReadableName, m.Description, m.ProductionEfford))
                .ToList();
            return Task.FromResult<GetAllResult<RobotBlueprint>>(new GetAllResult<RobotBlueprint>.Success(resultList));
        }

        public Task<GetByIdResult<RobotBlueprint>> GetById(RobotBlueprintId robotBlueprintId)
        {
            RobotBlueprintModel result;
            try
            {
                result = this.internalRepository.Get(robotBlueprintId.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult<GetByIdResult<RobotBlueprint>>(new GetByIdResult<RobotBlueprint>.Error(e));
            }

            if (result == null)
            {
                return Task.FromResult<GetByIdResult<RobotBlueprint>>(new GetByIdResult<RobotBlueprint>.NotFound());
            }

            var robotBlueprint = new RobotBlueprint(
                new RobotBlueprintId(result.Id),
                result.Version,
                result.HumanReadableName,
                result.Description,
                result.ProductionEfford);
            return Task.FromResult<GetByIdResult<RobotBlueprint>>(new GetByIdResult<RobotBlueprint>.Success(robotBlueprint));
        }

        public Task<QueryResult<RobotBlueprint>> Query(BlueprintsQuery query)
        {
            if (query.MaxProductionEfford.HasValue && query.MaxProductionEfford < 0)
            {
                return Task.FromResult<QueryResult<RobotBlueprint>>(new QueryResult<RobotBlueprint>.InvalidQuery($"{nameof(query.MaxProductionEfford)} value < 0. Must be 0<="));
            }

            if (query.NameContains != null && string.Equals(query.NameContains, string.Empty))
            {
                return Task.FromResult<QueryResult<RobotBlueprint>>(new QueryResult<RobotBlueprint>.InvalidQuery($"{nameof(query.NameContains)} is empty"));
            }

            IList<RobotBlueprintModel> getAllResult;
            try
            {
                getAllResult = this.internalRepository.GetAll().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult<QueryResult<RobotBlueprint>>(new QueryResult<RobotBlueprint>.Error(e));
            }


            var totalCount = getAllResult.Count;
            IEnumerable<RobotBlueprintModel> resultModels = getAllResult;
            if (query.MaxProductionEfford.HasValue)
            {
                resultModels = resultModels.Where(b => b.ProductionEfford <= query.MaxProductionEfford);
            }
            if (query.NameContains != null)
            {
                resultModels = resultModels.Where(b => b.HumanReadableName.Contains(query.NameContains));
            }

            var result = resultModels.Select(b => new RobotBlueprint(
                new RobotBlueprintId(b.Id),
                b.Version,
                b.HumanReadableName,
                b.Description,
                b.ProductionEfford)).ToList();

            return Task.FromResult<QueryResult<RobotBlueprint>>(new QueryResult<RobotBlueprint>.Success(result, totalCount));
        }
    }
}
