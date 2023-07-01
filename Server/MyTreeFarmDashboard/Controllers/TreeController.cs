using System.Net;
using AP.MyTreeFarm.Application.CQRS.Trees;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyTreeFarmDashboard.Services;
using MyTreeFarmDashboard.Models;
using X.PagedList;

namespace MyTreeFarmDashboard.Controllers;

[Authorize(Roles = "Admin")]
public class TreeController : Controller
{
    private readonly IRestService _restService;
    private readonly IFileService _fileService;
    private const int PageSize = 10;
    private const int PageSizeDetail = 6;
    private readonly IWebHostEnvironment _environment;

    public TreeController(IRestService restService, IFileService fileService, IWebHostEnvironment environment)
    {
        _restService = restService;
        _fileService = fileService;
        _environment = environment;
    }
    
    // GET
    public async Task<IActionResult> Index(string sortBy, string? searchBox, string currentFilter, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByTreeName = string.IsNullOrEmpty(sortBy) ? "tree_name_desc" : "";
        
        if (searchBox != null)
        {
            page = 1;
        }
        else
        {
            searchBox = currentFilter;
        }

        ViewBag.CurrentFilter = searchBox;
        
        var response = await _restService.GetResource<List<TreeDTO>>("tree/");
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");
        var trees = response.Data.AsQueryable();
        
        if (!string.IsNullOrEmpty(searchBox))
        {
            var searchBoxLower = searchBox.ToLower();
            trees = trees.Where(p => p.Name.ToLower().Contains(searchBoxLower));
        }
        
        trees = sortBy switch
        {
            "tree_name_desc" => trees.OrderByDescending(p => p.Name),
            _ => trees.OrderBy(t => t.Name)
        };
        var pagedList = await trees.ToPagedListAsync(page, PageSize);
        return View(pagedList);
        
    }

    // GET
    public async Task<IActionResult> Detail(int id, string sortBy, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByZoneName = string.IsNullOrEmpty(sortBy) ? "zone_name_desc" : "";
        ViewBag.SortBySurfaceArea = sortBy == "surface_area" ? "surface_area_desc" : "surface_area";
        
        
        var response = await _restService.GetResource<TreeDTO>("tree/" + id);
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");
            
        var tree = response.Data;
        var zones = tree.Zones.AsQueryable();
        zones = sortBy switch
        {
            "zone_name_desc" => zones.OrderByDescending(p => p.Name),
            "surface_area_desc" => zones.OrderByDescending(p => p.SurfaceArea),
            "surface_area" => zones.OrderBy(p => p.SurfaceArea),
            _ => zones.OrderBy(t => t.Name)
        };
        var pagedList = await zones.ToPagedListAsync(page, PageSizeDetail);

        var treeVm = new TreeVM
        {
            Tree = tree,
            PagedZones = pagedList
        };
        return View(treeVm);
    }

    // GET Create
    public IActionResult Create()
    {
        return View();
    }

    //Post create
    [HttpPost]
    public async Task<IActionResult> Create(CreateTreeDTO tree, IFormFile? PictureUrl, IFormFile? InstructionsUrl)
    {
        if (PictureUrl != null && InstructionsUrl != null)
        {
            tree.PictureUrl = await _fileService.UploadImg("treeImages/", PictureUrl);
            tree.InstructionsUrl = await _fileService.UploadPdf("treeImages/", InstructionsUrl);
            try
            {
                var urlToInstructions =
                    $"https://localhost:44312/Download/GetInstructions?filename={InstructionsUrl.FileName}";
                var remoteQrUri =
                    $"https://chart.googleapis.com/chart?cht=qr&chl=http%3A%2F%2F{urlToInstructions}&chs=547x547&choe=UTF-8&chld=L|2";
                var myWebClient = new WebClient();
                var qrFileName = InstructionsUrl.FileName.Replace(".pdf", "_QR_Image.png");
                var qrImageFilePath =
                    Path.Combine(_environment.ContentRootPath, @"wwwroot\images\treeImages\", qrFileName);
                myWebClient.DownloadFile(remoteQrUri, qrImageFilePath);
                tree.QrCodeUrl = qrFileName;
            }
            catch (Exception e)
            {
                return RedirectToAction("ErrorPage", "Account");
            }

        }
        
        var response = await _restService.PostResource<Tuple<CreateTreeDTO, List<ValidationFailure>>, CreateTreeDTO>("Tree/", tree);
        if (!response.IsSuccessful)
        {
            ViewData["Errors"] = response.Data.Item2;
            return View(response.Data.Item1);
        }
        
        TempData["AlertSuccess"] = "Boomsoort succesvol aangemaakt";
        return RedirectToAction("Index", "Tree");
    }
    
    // GET Update
    public async Task<IActionResult> Update(int id)
    {
        var response = await _restService.GetResource<TreeDTO>($"tree/{id}");
        if (response.IsSuccessful)
            return View(response.Data);
        return RedirectToAction("ErrorPage", "Account");
    }

    //Post Update
    [HttpPost]
    public async Task<IActionResult> Update(UpdateTreeDTO tree, IFormFile? pictureFile, IFormFile? instructionsFile)
    {
        if (pictureFile != null)
        {
            try
            {
                _fileService.DeleteFile("treeImages/" + tree.PictureUrl);
            }
            catch (Exception e)
            {
                TempData["AlertError"] = "Er liep iets fout met het aanpassen van de foto";
                return RedirectToAction("Index", "Site");
            }

            tree.PictureUrl = await _fileService.UploadImg("treeImages/", pictureFile);
        }
        
        if (instructionsFile != null)
        {
            try
            {
                _fileService.DeleteFile("treeImages/" + tree.InstructionsUrl);
            }
            catch (Exception e)
            {
                TempData["AlertError"] = "Er liep iets fout met het aanpassen van de instructies";
                return RedirectToAction("Index", "Site");
            }

            tree.InstructionsUrl = await _fileService.UploadPdf("treeImages/", instructionsFile);
            
            try
            {
                _fileService.DeleteFile("treeImages/" + tree.QrCodeUrl);
            }
            catch (Exception e)
            {
                TempData["AlertError"] = "Er liep iets fout met het aanpassen van de QR code";
                return RedirectToAction("Index", "Site");
            }
            
            try
            {
                var urlToInstructions = $"https://localhost:44312/Download/GetInstructions?filename={instructionsFile.FileName}";
                var remoteQrUri = $"https://chart.googleapis.com/chart?cht=qr&chl=http%3A%2F%2F{urlToInstructions}&chs=547x547&choe=UTF-8&chld=L|2";
                var myWebClient = new WebClient();
                var qrFileName = instructionsFile.FileName.Replace(".pdf", "_QR_Image.png");
                var qrImageFilePath = Path.Combine(_environment.ContentRootPath, @"wwwroot\images\treeImages\", qrFileName);
                myWebClient.DownloadFile(remoteQrUri, qrImageFilePath);
                tree.QrCodeUrl = qrFileName;
            }
            catch (Exception e)
            {
                return RedirectToAction("ErrorPage", "Account");
            }
        }
        
        var response = await _restService.PostResource<Tuple<TreeDTO, List<ValidationFailure>>, UpdateTreeDTO>($"Tree/{tree.Id}", tree);
        
        if (!response.IsSuccessful)
        {
            ViewData["Errors"] = response.Data.Item2;
            return View(response.Data.Item1);
        }
        
        TempData["AlertSuccess"] = "Boomsoort succesvol aangepast";
        return RedirectToAction("Index", "Tree");
    }

    // GET Delete
    public async Task<IActionResult> Delete(int id, string pictureUrl, string instructionsUrl, string qrUrl)
    {
        var response = await _restService.DeleteResource<int>($"Tree/{id}");
        if (response.IsSuccessful)
        {
            try
            {
                _fileService.DeleteFile("treeImages/" + pictureUrl);
                _fileService.DeleteFile("treeImages/" + instructionsUrl);
                _fileService.DeleteFile("treeImages/" + qrUrl);
            }
            catch (Exception e)
            {
                TempData["AlertError"] = "Er liep iets fout met het verwijderen van de foto";
                return RedirectToAction("Index", "Site");
            }
            TempData["AlertSuccess"] = "Boomsoort succesvol verwijderd";
        }
        else
        {
            TempData["AlertError"] = "Er liep iets fout met het verwijderen van de boomsoort";
        }

        return RedirectToAction("Index", "Tree");
    }
}