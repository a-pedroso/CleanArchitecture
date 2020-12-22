namespace CleanArchitecture.Application.Common.DTO
{
    public record ExportTodosDTO
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
