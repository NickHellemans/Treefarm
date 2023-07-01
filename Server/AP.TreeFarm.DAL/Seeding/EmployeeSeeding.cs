using AP.MyTreeFarm.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.MyTreeFarm.Infrastructure.Seeding
{
    public static class EmployeeSeeding
    {
        public static void Seed(this EntityTypeBuilder<Employee> modelBuilder)
        {
            modelBuilder.HasData(
                new Employee()
                {
                    Id = 1,
                    EmployeeId = "A_001",
                    FirstName = "Johnny",
                    LastName = "Sins",
                    Email = "s115990@ap.be",
                    UserName = "Johnny",
                    IsActive = true,
                    IsAdmin = true,
                    Auth0Id = "auth0|63a5f4b73393043784816ed3",
                },
                new Employee()
                {
                    Id = 2,
                    EmployeeId = "W_001",
                    FirstName = "Chad",
                    LastName = "Thunderglock",
                    Email = "s117923@ap.be",
                    UserName = "Chad",
                    IsActive = true,
                    IsAdmin = false,
                    Auth0Id = "auth0|63a42ea844641e0a186ca430",
                },
                new Employee()
                {
                    Id = 3,
                    EmployeeId = "W_002",
                    FirstName = "Belle",
                    LastName = "Delphine",
                    Email = "nick-hellemans@hotmail.com",
                    UserName = "BelleDelphine",
                    IsActive = true,
                    IsAdmin = false,
                    Auth0Id = "auth0|63a21af0e2c4faec70d45c72",
                },
                new Employee()
                {
                    Id = 4,
                    EmployeeId = "W_003",
                    FirstName = "Amouranth",
                    LastName = "Siragusa",
                    Email = "redphorcys@hotmail.com",
                    UserName = "Amouranth",
                    IsActive = true,
                    IsAdmin = false,
                    Auth0Id = "auth0|63a21b713393043784814183",
                    
                }
            );
        }
    }
}