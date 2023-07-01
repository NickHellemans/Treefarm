using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using AP.MyTreeFarm.Application.CQRS.Zones;
using AP.MyTreeFarm.Application.Interfaces;
using MyTreeFarmTests.Stubs;

namespace MyTreeFarmTests;

using NUnit.Framework;
using FluentValidation.TestHelper;

[TestFixture()]
public class TreeTaskValidatorTester 
{
    private CreateTreeTaskDTOValidator? _validator;
    private CreateTreeTaskDTOAdvancedValidator? _advancedValidator;
    private readonly IUnitofWork? _uow;
   
    
    [SetUp]
    public void Setup()
    {
       var uow = new UowStub
       {
           ZonesRepository = new ZoneRepoStub(),
           EmployeesRepository = new EmployeeRepoStub(),
           
       };

       _validator = new CreateTreeTaskDTOValidator();
       _advancedValidator = new CreateTreeTaskDTOAdvancedValidator(uow);
    }

    [Test]
    public async Task Should_have_error_when_name_is_empty()
    {
        var date = DateTime.Now;
        var model = new CreateTreeTaskDTO { Name = "", ZoneId = 0,DatePlanned = date};
        var result = await Task.FromResult(_validator.TestValidateAsync(model));
        result.Result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task Should_have_error_when_description_is_empty()
    {
        var date = DateTime.Now;
        var model = new CreateTreeTaskDTO { Name = "name",Description ="", Duration = 1,Priority = 1, EmployeeId = 1,ZoneId = 0,DatePlanned = date};
        var result = await Task.FromResult(_validator.TestValidateAsync(model));
        result.Result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    [Test]
    public async Task Should_have_error_when_duration_is_smaller_than_zero()
    {
        var date = DateTime.Now;
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = -1,Priority = 1, EmployeeId = 1,ZoneId = 0,DatePlanned = date};
        var result = await Task.FromResult(_validator.TestValidateAsync(model));
        result.Result.ShouldHaveValidationErrorFor(x => x.Duration);
    }
    
    [Test]
    public async Task Should_have_error_when_priority_is_smaller_than_zero()
    {
        var date = DateTime.Now;
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = -1, EmployeeId = 1,ZoneId = 0,DatePlanned = date};
        var result = await Task.FromResult(_validator.TestValidateAsync(model));
        result.Result.ShouldHaveValidationErrorFor(x => x.Priority);
    }
    
    

    [Test]
    public async Task Should_not_have_error_when_all_properties_are_specified() 
    {
        var date = DateTime.Now;
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 1,ZoneId = 0,DatePlanned = date};
        var result = await Task.FromResult(_validator.TestValidateAsync(model));
        result.Result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Test]
    public async Task Should_have_error_with_more_than_2_sites_per_zone()
    {
        var date = DateTime.Now;
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 1,ZoneId = 1,DatePlanned = date};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldHaveAnyValidationError();
    }
    
    [Test]
    public async Task Should_not_have_error_with_less_than_2_sites_per_zone()
    {
        var date = DateTime.Now;
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 1,ZoneId = 2,DatePlanned = date};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldNotHaveAnyValidationErrors();
    }
    [Test]
    public async Task Should_have_error_with_more_than_4_tasks_per_day_for_employee()
    {
       
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 1,ZoneId = 2,DatePlanned = new DateTime(2001,9,11)};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldHaveAnyValidationError();
    }
    [Test]
    public async Task Should_not_have_error_with_less_than_4_tasks_per_day_for_employee()
    {
       
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 3,ZoneId = 3,DatePlanned = new DateTime(2001,10,11)};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldNotHaveAnyValidationErrors();
    }
    [Test]
    public async Task Should_have_error_with_two_employees_in_same_zone_on_same_day()
    {
       
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 2,ZoneId = 2,DatePlanned = new DateTime(2001,9,11)};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldHaveAnyValidationError();
    }
    [Test]
    public async Task Should_not_have_error_with_two_employees_in_different_zones_on_same_day()
    {
       
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 3,ZoneId = 3,DatePlanned = new DateTime(2001,9,11)};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Test]
    public async Task Should_have_error_more_than_five_working_days_in_a_week()
    {
       
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 1,Priority = 1, EmployeeId = 4,ZoneId = 3,DatePlanned = new DateTime(2022,12,11)};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldHaveAnyValidationError();
    }
    
    [Test]
    public async Task Should_have_error_more_than_eight_hours_in_tasks_for_a_day()
    {
        // 480 mins = 8 hours, one of the other assigned tasks for this employee on this day contains a 1 minute duration task, this equals 481 mins
        var model = new CreateTreeTaskDTO { Name = "name",Description ="description", Duration = 480,Priority = 1, EmployeeId = 3,ZoneId = 3,DatePlanned = new DateTime(2001,9,11)};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldHaveAnyValidationError();
    }
    

    
}

public class ZoneValidatorTester
{
    private CreateZoneDTOValidator? _validator;
    private CreateZoneDTOAdvancedValidator? _advancedValidator;
    private UpdateZoneDTOAdvancedValidator? _advancedUpdateValidator;
    private readonly IUnitofWork? _uow;
   
    
    [SetUp]
    public void Setup()
    {
        var uow = new UowStub
        {
            ZonesRepository = new ZoneRepoStub(),
            EmployeesRepository = new EmployeeRepoStub(),
            SitesRepository = new SiteRepoStub()
           
        };

        _validator = new CreateZoneDTOValidator();
        _advancedValidator = new CreateZoneDTOAdvancedValidator(uow);
        _advancedUpdateValidator = new UpdateZoneDTOAdvancedValidator(uow);
    }
    
    
    [Test]
    public async Task Should_have_error_when_name_is_empty()
    {
        var model = new CreateZoneDTO { Name = "", SurfaceArea = 1 , SiteId = 2, TreeId = 3 };
        var result = await Task.FromResult(_validator.TestValidateAsync(model));
        result.Result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task Should_have_error_when_surfaceArea_is_empty()
    {
        var model = new CreateZoneDTO { Name = "d" , SiteId = 2, TreeId = 3 };
        var result = await Task.FromResult(_validator.TestValidateAsync(model));
        result.Result.ShouldHaveValidationErrorFor(x => x.SurfaceArea);
    }
    
    [Test]
    public async Task Should_have_error_when_site_with_tree_that_is_present_in_three_zones_already_is_added()
    {
        var model = new CreateZoneDTO { Name = "d" , SurfaceArea = 1, SiteId = 1, TreeId = 1 };
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldHaveAnyValidationError();
    }
    [Test]
    public async Task Should_not_have_error_when_site_with_tree_that_is_not_present_in_three_zones_already_is_added()
    {
        var model = new CreateZoneDTO { Name = "d" , SurfaceArea = 1, SiteId = 1, TreeId = 3};
        var result = await Task.FromResult(_advancedValidator.TestValidateAsync(model));
        result.Result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Test]
    public async Task Should_have_error_when_site_with_tree_that_is_present_in_three_zones_already_is_updated()
    {
        var model = new UpdateZoneDTO { Name = "d" , SurfaceArea = 1, SiteId = 1, TreeId = 1,Id = 0 };
        var result = await Task.FromResult(_advancedUpdateValidator.TestValidateAsync(model));
        result.Result.ShouldHaveAnyValidationError();
    }
    [Test]
    public async Task Should_not_have_error_when_site_with_tree_that_is_not_present_in_three_zones_already_is_updated()
    {
        var model = new UpdateZoneDTO { Name = "d" , SurfaceArea = 1, SiteId = 1, TreeId = 3, Id = 0};
        var result = await Task.FromResult(_advancedUpdateValidator.TestValidateAsync(model));
        result.Result.ShouldNotHaveAnyValidationErrors();
    }
    
    
}