using Microsoft.AspNetCore.Mvc;

namespace MyTreeFarmDashboard.Controllers;

public class DownloadController : Controller
{
    private readonly IWebHostEnvironment _environment;

    public DownloadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public ActionResult GetInstructions(string filename)
    {
        var filepath = Path.Combine(_environment.ContentRootPath, @"wwwroot\images\treeImages\", filename);

        try
        {
            return new FileContentResult(System.IO.File.ReadAllBytes(filepath), "application/pdf");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
    }
    
    
    public ActionResult GetQrcode(string filename)
    {
        try
        {
            var filepath = Path.Combine(_environment.ContentRootPath, @"wwwroot\images\treeImages\", filename);
            Response.Headers.Add("Content-Disposition", $"attachment;filename={filename}");
            return new FileStreamResult(new FileStream(filepath, FileMode.Open), "application/png");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
    }
    
}