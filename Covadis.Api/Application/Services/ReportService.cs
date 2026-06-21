using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.DTOs.Team;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Task = System.Threading.Tasks.Task;

namespace Covadis.Api.Application.Services;

public class ReportService : IReportService
{
    public async Task<byte[]> GenerateTeamReportAsync(TeamReadDto team, List<TaskListDto> tasks)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        
        return await Task.Run(() =>
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text($"Team Report - {team.Name}")
                        .FontSize(20)
                        .Bold();

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm}");
                        column.Item().Text($"Total Tasks: {tasks.Count}");

                        column.Item().PaddingTop(10);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Task").Bold();
                                header.Cell().Text("Status").Bold();
                                header.Cell().Text("Due Date").Bold();
                            });

                            foreach (var task in tasks)
                            {
                                table.Cell().Text(task.Title);

                                table.Cell().Text(
                                    task.Status.ToString()
                                );

                                table.Cell().Text(
                                    task.DueDate.ToString("yyyy-MM-dd") ?? "-"
                                );
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf();
        });
    }
}