using System;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Application.Results;
using RoboPlant.Domain.Design;

namespace RoboPlant.Application.Design
{
    public class DesignCommandHandler
    {
        private readonly IRobotBlueprintRepository robotBlueprintRepository;

        public DesignCommandHandler(IRobotBlueprintRepository robotBlueprintRepository)
        {
            this.robotBlueprintRepository = robotBlueprintRepository;
        }

        public OperationAvailableResult CanQueryBlueprints()
        {
            return new OperationAvailableResult.Available();
        }

        public Task<QueryResult<RobotBlueprint>> Query(BlueprintsQuery blueprintQuery)
        {
            return robotBlueprintRepository.Query(blueprintQuery);
        }

        public Task<GetByIdResult<RobotBlueprint>> GetRobotBlueprintGetById(Guid blueprintId)
        {
            return robotBlueprintRepository.GetById(new RobotBlueprintId(blueprintId));
        }
    }
}
