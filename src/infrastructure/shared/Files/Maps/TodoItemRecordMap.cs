using CleanArchitecture.Application.Common.DTO;
using CsvHelper.Configuration;
using System.Globalization;

namespace CleanArchitecture.Infrastructure.Shared.Files.Maps
{
    public class TodoItemRecordMap : ClassMap<ExportTodoItemFileRecordDTO>
    {
        public TodoItemRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
