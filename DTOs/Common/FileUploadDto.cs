namespace CarDealershipAPI.DTOs.common
{
    public class FileUploadDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }

}
