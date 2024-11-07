using Microsoft.AspNetCore.Http;

namespace API.Application.Dto.Request
{
    public class RequestAllDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortOrder { get; set; } 
        public string SorterField { get; set; } 
    }

    public class FileUploadRequest
    {
        public IFormFileCollection Files { get; set; }
    }
}
