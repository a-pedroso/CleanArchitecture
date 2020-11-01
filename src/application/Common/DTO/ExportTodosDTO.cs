namespace CleanArchitecture.Application.Common.DTO
{
    public class ExportTodosDTO
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
