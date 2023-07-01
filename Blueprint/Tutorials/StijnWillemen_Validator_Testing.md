# Unit tests voor validation (independent testing)


## Probleem
Tijdens het beginnen schrijven van de unit tests voor de FluentValidation validators kwamen we er al snel achter dat de verschillende geschreven validators niet zonder Unit of Work en de Repositories konden werken.

## Oplossing
Om de tests enkel over de validators te laten gaan hebben we gebruikt gemaakt van Mocks en Stubs.


 
![image](https://user-images.githubusercontent.com/60694521/211201778-ad2921d7-f687-4e60-9062-7dcec4732bed.png)


De Unit of Work Stub die ook de IUnitofWork interface gebruikt en dus de verschillende repositories nodig heeft in de klasse. Deze moeten ook gemocked worden.

![image](https://user-images.githubusercontent.com/60694521/211201789-569cf9b2-cc4c-49dd-b82f-6fa408cbdb78.png)

 
Deze repository Mock implementeert de IZoneRepository Interface. Bij de gebruikte functies wordt hetzelfde gedrag als de originele code nagebootst maar er is meer controle. Er kan gekozen worden welke specifieke taak wordt teruggeven bijvoorbeeld.

Er zijn ook meteen stubs aanwezig in de vorm van afzonderlijke Zones die een lijst van taken bevatten (pas dit aan naarmate de eigen entity relations)



In de test file zelf wordt gebruik gemaakt van een SetUp functie die voor elke test geldt (één keer te gebruiken)
In deze setup maken we eerst een nieuwe unit of work aan. Daarna voegen we de Repositories (stubs) die we nodig hebben in deze test file toe.

Om de validators aan te kunnen roepen in de volledige test klasse worden deze eerst aangemaakt voor ze de constructors mee krijgen.

In de constructor waar het probleem is onstaan geven we de gemocked unit of work mee zodat we de depedency kunnen controleren.

![image](https://user-images.githubusercontent.com/60694521/211201850-4ae8f2d5-29bc-43cc-b04c-0befeebfed58.png)


In de aparte tests gebruiken we de gemaakte validators en voegen we een model toe van de klasse waarvan we de validators willen testen. Dit model is afhankelijk van de validator die we willen testen. Hierna gebruiken we de ingebouwde functies van FluentValidators om te testen of er validators errors voorkomen.
 
![image](https://user-images.githubusercontent.com/60694521/211201879-d5133521-6059-43f2-86d9-e5bd4877043b.png)

In dit geval wordt er getest of de werknemer waarvoor we een taak toevoegen niet al 4 taken heeft op deze specifieke dag. In de stubs staan een lijst van taken die is toegevoegd aan deze werknemer. De taken in deze lijst staan allemaal op dezelfde dag ingepland namelijk 11/9/2001. Hierdoor kunnen we nagaan of er een error is en zo de andere tests op dezelfde manier structureren.
