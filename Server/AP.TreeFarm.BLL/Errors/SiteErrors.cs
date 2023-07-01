namespace AP.MyTreeFarm.Application.Errors
{
	public static class SiteErrors
	{
		public const string
			Name = "Naam mag niet leeg of langer dan 255 karakters zijn",
			PostalCode = "Postcode mag niet leeg of langer dan 255 karakters zijn",
			Street = "Straat mag niet leeg of langer dan 255 karakters zijn",
			StreetNumber = "Huisnummer mag niet leeg of langer dan 255 karakters zijn",
			MapPicturePath = "Gelieve een correct bestandstype te gebruiken (.jpg/.png/.jpeg)";
	}
}