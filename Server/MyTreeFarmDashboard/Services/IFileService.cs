namespace MyTreeFarmDashboard.Services;

public interface IFileService
{
    Task<string> UploadImg(string folder, IFormFile? file);
    Task<string> UploadPdf(string folder, IFormFile? file);
    void DeleteFile(string filename);
}