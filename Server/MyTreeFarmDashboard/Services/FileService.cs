namespace MyTreeFarmDashboard.Services;

public class FileService: IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public async Task<string> UploadImg(string folder, IFormFile? file)
    {
        var filepath = Path.Combine(_environment.ContentRootPath, @"wwwroot\images\" + folder, file.FileName);
        if (!file.ContentType.Contains("jpg") && !file.ContentType.Contains("png") && !file.ContentType.Contains("jpeg"))
        {
            return null;
        }
        await using var fileStream = new FileStream(filepath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return file.FileName;
    }

    public async Task<string> UploadPdf(string folder, IFormFile? file)
    {
        var filepath = Path.Combine(_environment.ContentRootPath, @"wwwroot\images\" + folder, file.FileName);
        if (!file.ContentType.Contains("pdf"))
        {
            return null;
        }
        await using var fileStream = new FileStream(filepath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return file.FileName;
    }

    public void DeleteFile(string filename)
    {
        string filepath = Path.Combine(_environment.ContentRootPath, @"wwwroot\images\", filename); 
        File.Delete(filepath);
    }
}