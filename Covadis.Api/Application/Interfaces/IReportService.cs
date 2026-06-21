using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.DTOs.Team;
using QuestPDF.Fluent;

namespace Covadis.Api.Application.Interfaces;

public interface IReportService
{
    Task<byte[]> GenerateTeamReportAsync(TeamReadDto team, List<TaskListDto> tasks);
}