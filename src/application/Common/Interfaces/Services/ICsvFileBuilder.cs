using CleanArchitecture.Application.Common.DTO;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<ExportTodoItemFileRecordDTO> records);
    }
}
