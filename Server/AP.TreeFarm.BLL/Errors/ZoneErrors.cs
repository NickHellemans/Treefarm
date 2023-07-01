namespace AP.MyTreeFarm.Application.Errors
{
	public static class ZoneErrors
	{
		public const string
			Name = "Naam mag niet leeg of langer dan 255 karakters zijn",
			SurfaceArea = "Oppervlakte moet groter dan 0 zijn";
		
		// Advanced
		public const string 
			MaxThreeTreesPerZone = "Een boomsoort kan maximaal in 3 zones staan per site";
	}
}