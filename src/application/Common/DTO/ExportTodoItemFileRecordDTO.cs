namespace CleanArchitecture.Application.Common.DTO
{
    public record ExportTodoItemFileRecordDTO
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
