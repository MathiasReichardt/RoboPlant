using System.Threading.Tasks;
using RoboPlant.Application.Design;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Domain.Design;

namespace RoboPlant.Application.Persistence
{
    public interface IRobotBlueprintRepository
    {
        Task<GetAllResult<RobotBlueprint>> GetAll();

        Task<GetByIdResult<RobotBlueprint>> GetById(RobotBlueprintId productionLineId);

        Task<QueryResult<RobotBlueprint>> Query(BlueprintsQuery query);
    }
}