using AP.MyTreeFarm.Application.CQRS.Sites;
using AP.MyTreeFarm.Application.CQRS.Trees;
using AP.MyTreeFarm.Application.CQRS.Zones;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyTreeFarmDashboard.Models;
using MyTreeFarmDashboard.Services;
using X.PagedList;

namespace MyTreeFarmDashboard.Controllers;

[Authorize(Roles = "Admin")]
public class SiteController : Controller
{
    private readonly IFileService _fileService;
    private readonly IRestService _restService;
    private const int PageSize = 10;
    private const int PageSizeDetail = 4;

    public SiteController(IFileService fileService, IRestService restService)
    {
        _fileService = fileService;
        _restService = restService;
    }

    // GET Sites
    public async Task<IActionResult> Index(string sortBy, string? searchBox, string currentFilter, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortBySiteName = string.IsNullOrEmpty(sortBy) ? "site_name_desc" : "";
        ViewBag.SortByPostalCode = sortBy == "postalcode" ? "postalcode_desc" : "postalcode";
        ViewBag.SortByStreet = sortBy == "street" ? "street_desc" : "street";
        ViewBag.SortByStreetNumber = sortBy == "streetNumber" ? "streetNumber_desc" : "streetNumber";
        
        if (searchBox != null)
        {
            page = 1;
        }
        else
        {
            searchBox = currentFilter;
        }

        ViewBag.CurrentFilter = searchBox;
        
        var response = await _restService.GetResource<List<SiteDTO>>($"Site/");
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");

        var sites = response.Data.AsQueryable();
        
        if (!string.IsNullOrEmpty(searchBox))
        {
            var searchBoxLower = searchBox.ToLower();
            sites = sites.Where(p =>
                p.Name.ToLower().Contains(searchBoxLower) || p.Street.ToLower().Contains(searchBoxLower) || p.PostalCode.ToLower().Contains(searchBoxLower));
        }
        
        sites = sortBy switch
        {
            "site_name_desc" => sites.OrderByDescending(p => p.Name),
            "postalcode_desc" => sites.OrderByDescending(p => p.PostalCode),
            "postalcode" => sites.OrderBy(p => p.PostalCode),
            "street_desc" => sites.OrderByDescending(p => p.Street),
            "street" => sites.OrderBy(p => p.Street),
            "streetNumber_desc" => sites.OrderByDescending(p => p.StreetNumber),
            "streetNumber" => sites.OrderBy(p => p.StreetNumber),
            _ => sites.OrderBy(t => t.Name)
        };
        
        var pagedList = await sites.ToPagedListAsync(page, PageSize);
        return View(pagedList);
    }

    // GET Detail site
    public async Task<IActionResult> Detail(int id, string sortBy, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByZoneName = string.IsNullOrEmpty(sortBy) ? "zone_name_desc" : "";
        ViewBag.SortBySurfaceArea = sortBy == "surface_area" ? "surface_area_desc" : "surface_area";
        ViewBag.SortByTree = sortBy == "tree" ? "tree_desc" : "tree";
        
        var response = await _restService.GetResource<SiteDTO>($"Site/" + id);
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");
        
        var site = response.Data;
        var zones = site.Zones.AsQueryable();
        zones = sortBy switch
        {
            "zone_name_desc" => zones.OrderByDescending(p => p.Name),
            "surface_area_desc" => zones.OrderByDescending(p => p.SurfaceArea),
            "surface_area" => zones.OrderBy(p => p.SurfaceArea),
            "tree_desc" => zones.OrderByDescending(p => p.Tree.Name),
            "tree" => zones.OrderBy(p => p.Tree.Name),
            _ => zones.OrderBy(t => t.Name)
        };
        var pagedList = await zones.ToPagedListAsync(page, PageSizeDetail);

        var siteVm = new SiteVM
        {
            Site = site,
            PagedZones = pagedList
        };
        
        return View(siteVm);
    }

    // GET Detail zone
    public async Task<IActionResult> DetailZone(int id, string sortBy, int page = 1)
    {
        ViewBag.CurrentSort = sortBy;
        ViewBag.SortByDuration = string.IsNullOrEmpty(sortBy) ? "duration_desc" : "";
        ViewBag.SortByPriority = sortBy == "priority" ? "priority_desc" : "priority";
        ViewBag.SortByEmployee = sortBy == "employee" ? "employee_desc" : "employee";
        ViewBag.SortByStatus = sortBy == "status" ? "status_desc" : "status";
        ViewBag.SortByDatePlanned = sortBy == "date_planned" ? "date_planned_desc" : "date_planned";
        
        var response = await _restService.GetResource<ZoneDTO>($"Zone/" + id);
        if (!response.IsSuccessful || response.Data == null) return RedirectToAction("ErrorPage", "Account");
        
        var zone = response.Data;
        var tasks = zone.Tasks.AsQueryable();
        tasks = sortBy switch
        {
            "duration_desc" => tasks.OrderByDescending(p => p.Duration),
            "priority_desc" => tasks.OrderByDescending(p => p.Priority),
            "priority" => tasks.OrderBy(p => p.Priority),
            "employee_desc" => tasks.OrderByDescending(p => p.Employee.LastName),
            "employee" => tasks.OrderBy(p => p.Employee.LastName),
            "status_desc" => tasks.OrderByDescending(p => p.Status),
            "status" => tasks.OrderBy(p => p.Status),
            "date_planned_desc" => tasks.OrderByDescending(p => p.DatePlanned),
            "date_planned" => tasks.OrderBy(p => p.DatePlanned),
            _ => tasks.OrderBy(t => t.Duration)
        };
        
        var pagedList = await tasks.ToPagedListAsync(page, PageSizeDetail);

        var zoneVm = new ZoneVM
        {
            Zone = zone,
            PagedTasks = pagedList
        };
        
        return View(zoneVm);
        
    }

    // GET Create site
    public IActionResult Create()
    {
        return View();
    }

    // Post Create site
    [HttpPost]
    public async Task<IActionResult> Create(CreateSiteDTO site, IFormFile? mapFile)
    {
        if (mapFile != null)
        {
            site.MapPicturePath = await _fileService.UploadImg("siteImages/", mapFile);
        }

        var response =
            await _restService.PostResource<Tuple<CreateSiteDTO, List<ValidationFailure>>, CreateSiteDTO>($"Site/",
                site);
        if (!response.IsSuccessful)
        {
            ViewData["Errors"] = response.Data.Item2;
            return View(response.Data.Item1);
        }
        TempData["AlertSuccess"] = "Site succesvol aangemaakt";
        return RedirectToAction("Index", "Site");
    }

    // GET Create Zone
    public async Task<IActionResult> CreateZone(int siteId, string siteName)
    {
        var responseTrees = await _restService.GetResource<List<TreeDTO>>("Tree/");
        if (!responseTrees.IsSuccessful)
            return RedirectToAction("ErrorPage", "Account");

        var vm = new CreateZoneVM
        {
            SiteId = siteId,
            SiteName = siteName,
            Zone = new CreateZoneDTO
            {
                SiteId = siteId
            },
            Trees = new List<SelectListItem>()
        };

        foreach (var tree in responseTrees.Data)
        {
            vm.Trees.Add(new SelectListItem { Text = tree.Name, Value = tree.Id.ToString() });
        }

        return View(vm);
    }

    //Post create zone
    [HttpPost]  
    public async Task<IActionResult> CreateZone(int siteId, CreateZoneVM zoneVM)
    {
        zoneVM.Zone.SiteId = siteId;
        var response = await _restService.PostResource<Tuple<CreateZoneDTO, List<ValidationFailure>>, CreateZoneDTO>("Zone/", zoneVM.Zone);
        if (response.IsSuccessful)
        {
            TempData["AlertSuccess"] = "Zone succesvol aangemaakt";
            return RedirectToAction("Detail", new { id = siteId });
        } 
        ViewData["Errors"] = response.Data.Item2;
        return View(zoneVM);
    }

    // GET Update site
    public async Task<IActionResult> Update(int id)
    {
        var response = await _restService.GetResource<UpdateSiteDTO>($"Site/{id}");
        if (!response.IsSuccessful)
            return RedirectToAction("ErrorPage", "Account");
        
        return View(response.Data);
    }

    // Post Update site
    [HttpPost]
    public async Task<IActionResult> Update(UpdateSiteDTO site, IFormFile? mapFile)
    {
        if (mapFile != null)
        {
            try
            {
                _fileService.DeleteFile("siteImages/" + site.MapPicturePath);
            }
            catch (Exception e)
            {
                TempData["AlertError"] = "Er liep iets fout met het aanpassen van de foto";
                return RedirectToAction("Index", "Site");
            }

            site.MapPicturePath = await _fileService.UploadImg("siteImages", mapFile);
        }

        var response = await _restService.PostResource<Tuple<UpdateSiteDTO, List<ValidationFailure>>, UpdateSiteDTO>($"Site/{site.Id}", site);
        if (!response.IsSuccessful)
        {
            ViewData["Errors"] = response.Data.Item2;
            return View(response.Data.Item1);
        }
        
        TempData["AlertSuccess"] = "Site succesvol aangepast";
        return RedirectToAction("Detail", new { id = site.Id });
    }

    // GET Update Zone
    public async Task<IActionResult> UpdateZone(int siteId, string siteName, int id)
    {
        var responseZone = await _restService.GetResource<UpdateZoneDTO>($"Zone/{id}");
        if (!responseZone.IsSuccessful)
            RedirectToAction("ErrorPage", "Account");

        var responseTrees = await _restService.GetResource<List<TreeDTO>>("Tree/");
        if (!responseTrees.IsSuccessful)
            RedirectToAction("ErrorPage", "Account");
        var vm = new UpdateZoneVM
        {
            SiteId = siteId,
            SiteName = siteName,
            Trees = new List<SelectListItem>(),
            Zone = responseZone.Data
        };

        foreach (var tree in responseTrees.Data)
        {
            vm.Trees.Add(new SelectListItem { Text = tree.Name, Value = tree.Id.ToString() });
        }

        return View(vm);
    }

    // Post Update zone
    [HttpPost]
    public async Task<IActionResult> UpdateZone(int id, int siteId, UpdateZoneVM zoneVM)
    {
        zoneVM.Zone.SiteId = siteId;
        var response = await _restService.PostResource<Tuple<UpdateZoneDTO, List<ValidationFailure>>, UpdateZoneDTO>($"Zone/{id}", zoneVM.Zone);
        
        if (!response.IsSuccessful) 
        {
            ViewData["Errors"] = response.Data.Item2;
            zoneVM.Zone = response.Data.Item1;
            return View(zoneVM);
        }
        TempData["AlertSuccess"] = "Site succesvol aangepast";
        return RedirectToAction("Detail", new { id = siteId });
    }

    //Delete
    public async Task<IActionResult> Delete(int id, string filename)
    {
        var response = await _restService.DeleteResource<int>($"Site/{id}");
        if (response.IsSuccessful)
        {
            try
            {
                _fileService.DeleteFile("siteImages/" + filename);
            }
            catch (Exception e)
            {
                TempData["AlertError"] = "Er liep iets fout met het verwijderen van de foto";
                return RedirectToAction("Index", "Site");
            }

            TempData["AlertSuccess"] = "Site succesvol verwijderd";
        }
        else
        {
            TempData["AlertError"] = "Er liep iets fout met het verwijderen van de site";
        }

        return RedirectToAction("Index", "Site");
    }

    //Delete Zone
    public async Task<IActionResult> DeleteZone(int id, int siteId)
    {
        var response = await _restService.DeleteResource<int>($"Zone/{id}");
        if (response.IsSuccessful)
        {
            TempData["AlertSuccess"] = "Zone succesvol verwijderd";
        }
        else
        {
            TempData["AlertError"] = "Er liep iets fout met het verwijderen van de zone";
        }

        return RedirectToAction("Detail", new { id = siteId });
    }
}