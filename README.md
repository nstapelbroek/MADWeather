#Workshop Xamarin
###Opdracht 1 - WeatherApp Shared Logic
In deze opdracht maak je kennis met Xamarin Studio. In het kort is het de bedoeling om de aangeleverde shared logic toe te passen voor Android en iOS. Deze opdracht laat je zien dat een gezamenlijke business logic kan worden gebruikt voor zowel Android als iOS applicaties.

De bedoeling van de te maken applicatie is dat je met behulp van een plaatsnaam de huidige temperatuur kan opvragen. Deze plaatsnaam moet door de telefoongebruiker ingevuld kunnen worden. Daarna kan de gebruiker op een knop drukken om het weer op te halen. De temperatuur verschijnt daarna op het beeldscherm.

Begin met het klonen [deze Git repository](https://github.com/nstapelbroek/MADWeather.git) naar je machine en open de solution file (MADWeather.sln) met Xamarin Studio. Aan de zijkant zie je de solution met drie projecten: MadWeather, MadWeather.Droid en MadWeather.iOS. MadWeather bevat de shared logic. De andere twee projecten zijn de platformspecifieke code. Kies met welk platform je gaat beginnen, Android of iOS. De klasse `WeatherStation` bevat één public static functie, gebruik deze. Zodra je de code voor het ene platform af hebt ga je door met het andere platform.

####Hints
* Begin met de demo code en pas deze aan, de manier waarop delegates worden gebruikt is anders dan tijdens de lessen.
* In de Solution Explorer (in de linker zijbalk) moet je een start-up project aangeven om te kiezen uit Android of iOS.
* Android layout files moeten opgeslagen worden voordat die in de Resource.id namespace komt. Dat duurt even.
* De shared logic maakt gebruik van asynchrone functies. Als je niet moeilijk wil doen en deze functies synchroon wil gebruiken, gebruik dan `await` bij je functieaanroep. Lees eventuele foutmeldingen die je krijgt goed.
* Probeer eerst de basis af te krijgen, voordat je ‘leuke dingen’ gaan maken.
* Als je er niet uitkomt vraag een van ons om hulp.

###Opdracht 2 - WeatherApp GPS
Bij deze opdracht ga je GPS functionaliteit toevoegen aan de WeatherApp. Het idee achter deze opdracht is om je te laten zien dat je ook API-specifieke functionaliteiten kan gebruiken in Xamarin. Begin met het platform van je eigen mobiele telefoon (als het iOS of Android is. Kies anders wat je het handigst vindt). Als je deze opdracht binnen de tijd afkrijgt voor het gekozen platform, kan je daarna met het andere platform beginnen.

Vul de klasse `WeatherStation` aan met een functie die het weer binnen kan halen op basis van GPS coördinaten. (Kijk op [openweathermap.org/current](openweathermap.org/current) voor de weather API) Voeg daarna voor Android en/of iOS hun API-specifieke GPS oplossing toe en laat op het scherm de temperatuur van je huidige locatie zien. Zorg ervoor dat niet constant een GPS locatie wordt opgevraagd.

####iOS hint
* In ‘Info.plist’ zorg dat je in de source een property van het type String met de naam `NSLocationWhenInUseUsageDescription` toevoegt. De waarde van deze property wordt door iOS gebruikt in de pop-up om te vragen om permissie voor het gebruik van GPS. Het maakt niet uit wat je hierin zet, als er maar wat in staat. Dit is van toepassing als je `RequestWhenInUseAuthorization` gebruikt.

####Android hints
* Om de een of andere reden zij er verschillende GPS tutorials voor Xamarin.Android die niet goed werken. Gebruik daarom de volgende tutorial: [http://developer.xamarin.com/guides/android/platform_features/maps_and_location/location/](http://developer.xamarin.com/guides/android/platform_features/maps_and_location/location/) tot en met het kopje *'Stop Location Updates'*.
* Gebruik in plaats van `LocationManager.GpsProvider` de `LocationManager.NetworkProvider`. Dit heeft ermee te maken dat `GpsProvider` tot vijf minuten erover kan doen om een locatie te krijgen. `NetworkProvider` krijgt zijn locatie via de GSM mast, wat een stuk sneller is dan via GPS sattelieten.
