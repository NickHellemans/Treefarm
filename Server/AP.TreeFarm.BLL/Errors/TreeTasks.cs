namespace AP.MyTreeFarm.Application.Errors
{
	public static class TreeTaskErrors
	{
		public const string
			Name = "Naam mag niet leeg of langer dan 255 karakters zijn",
			Description = "Uitleg mag niet leeg of langer dan 4000 tsjerekters zijn",
			Priority = "Prioriteit mag niet leeg zijn",
			Duration = "Duur moet een positief getal zijn",
			DatePlanned = "Geplande datum moet een geldige datum zijn";


		// Advanced
		public const string 
			MaxTwoSites = "Een werknemer mag maximaal in twee sites werkzaam zijn",
			MaxFourTasksPerDay = "Een werknemer mag maximaal 4 taken per dag toegekend krijgen",
			MaxOneEmployeePerZonePerDay = "Een zone mag per dag maximaal voor 1 medewerker ingepland worden",
			MaxFiveDays = "Een werknemer mag maximaal voor 5 dagen per week ingepland worden",
			MaxEightHoursPerDay = "Een werknemer mag maar voor maximaal 8uur ingepland worden";
	}
}