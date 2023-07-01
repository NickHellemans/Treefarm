## Probleemstelling

In de uitleg van de opdracht vinden we volgende paragraaf:

> ... 
Aangezien we bomen kweken moeten we ook informatie over bomen kunnen beheren in
het admin gedeelte van de applicatie. Deze informatie omvat onder andere foto’s (ter
herkenning), de zone(s) op de kweeksite(s) waar de bomen gekweekt worden, de
onderhoudsinstructies in pdf-formaat en een QR-code. Deze QR-code wordt dan bij de zone
waarin de bomen gekweekt worden geplaatst. Wanneer medewerkers op basis van hun
takenlijst de bomen moeten verzorgen **kunnen ze de QR-code inscannen** om het juiste
onderhoudsplan te openen. Dit onderhoudsplan verschilt immers van seizoen tot seizoen
...

Het zal dus mogelijk moeten zijn voor medewerkers om via hun applicatie QR-codes te scannen. Aangezien *Ionic* (via *Capacitor*) zelf geen plugin heeft om dit te implementeren, zullen we gebruik moeten maken van externe plugins. De documentatie verwijst ons door naar [Awesome Cordova Plugins](https://danielsogl.gitbook.io/awesome-cordova-plugins/). Wanneer we zoeken naar een plugin om barcodes te scannen, merken we al snel op dat de resultaten oftewel outdated zijn, oftewel een licensie vereisen. Er is echter nog wel een werkende scanner te vinden, namelijk [PhoneGap Plugin BarcodeScanner](https://github.com/phonegap/phonegap-plugin-barcodescanner). Deze repository werkt nog wel op de laatste veries van Ionic, maar is echter gearchiveerd, en dus bestaat er de kans dat deze in de toekomst niet meer zal werken. Bovendien geeft deze plugin weinig controle over de scanner, zoals het afhandelen van permissies of het sluiten van de scanner, wat niet ideaal is. Gelukkige konden we een andere repository vinden die wel up to date is: [Barcode Scanner](https://github.com/capacitor-community/barcode-scanner) van de [Capacitor Community](https://github.com/capacitor-community), wat ons toch enigszins gerust stelt dat deze repository veilig is om te gebruiken.

Het probleem was de plugin werkende te krijgen en de UI gebruiksvriendelijk te maken, aangezien de plugin enkel en alleen de interfaces aanbied om de API van de smartphone aan te spreken, en dus niet de user interfaces om dat te doen. Dit maakt het iets lastiger om de scanner te implementeren, maar geeft ons wel meer controle. 

## Implementatie
De werking van de plugin zelf wordt degelijk uitgelegd in de readme van de repo, maar de code ziet er niet helemaal ordelijk uit. Boven mag er zeker niet vergeten worden om de [Troubleshooting](https://github.com/capacitor-community/barcode-scanner#troubleshooting) te bekijken aangezien we met Ionic werken, anders gaan we geen cameraview te zien krijgen. 

De effectieve implementatie wordt aan de gebruiker overgelaten. Omdat bij *Angular* informatie uitwisselen tussen componenten eenvoudiger gaat door het gebruik van *services*, creëren we een service voor het gebruik van de scanner. Deze service zal al de code bevatten voor het gebruik van de scanner, zodat we deze enkel nog moeten oproepen in onze componenten: 

**code-scanner.service.ts**
```typescript
import { Injectable } from '@angular/core';

// Capacitor
import { Capacitor } from '@capacitor/core';
import { BarcodeScanner, ScanOptions, ScanResult, SupportedFormat } from '@capacitor-community/barcode-scanner';

@Injectable({
	providedIn: 'root'
})

export class CodeScannerService {
	constructor() {}

	async scannerStart() {
		if (Capacitor.isNativePlatform()) {
			const hasPermission = await this._checkPermission();
			if (!hasPermission) {
				return BarcodeScanner.openAppSettings();
			}

			const options: ScanOptions = {
				targetedFormats: [
					SupportedFormat.QR_CODE
				]
			}

			document.querySelector('body').classList.add('scanner-active');

			const result: ScanResult = await BarcodeScanner.startScan(options);
		}
	}

	async scannerStop() {
		BarcodeScanner.stopScan();	
		document.querySelector('body').classList.remove('scanner-active');
	}

	private async _checkPermission() {
		return new Promise(async (resolve, reject) => {
			const status = await BarcodeScanner.checkPermission({ force: true });
			if (status.granted) {
				resolve(true);
			}
			else if (status.denied) {
				resolve(false);
			}
		});
	}
}
```

Om de scanner te starten moeten we de functie "scannerStart" uit de *CodeScannerService* aanspreken. Deze gaat eerst nagaan of je wel degelijk de permissies hebt om een scan te starten, vooraleer de scan kan starten. Indien je de permissies expliciet gewijgerd hebt, zal je worden doorverwezen naar de settings van de app, om daar alsnog toestemming te geven.

Bij het starten van het scannen wordt enkel en alleen de cameraview getoond, en heb je bijvoorbeeld geen buttons voor het annuleren van de scan, of het aanzetten van de flashlight. Aangezien we toch zeker de scan willen kunnen onderbreken, of de flashlight kunnen bedienen van uit de applicatie, hebben we deze zelf geïmplementeerd in de component die we gebruiken voor het scannen:

**camera.component.ts**
```typescript
// Angular
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
	selector: 'camera',
	templateUrl: './camera.component.html',
	styleUrls: ['./camera.component.scss'],
})
export class CameraComponent implements OnInit, OnDestroy {

	constructor(
		private codeScannerService: CodeScannerService
	) { }

	ngOnInit(): void {}

	ngOnDestroy(): void {}

	scannerStop(): void {
		this.codeScannerService.scannerStop();
	}

	toggleTorch(): void {}
}
```

In **camera.component.html**
```html
<div>
	<div class="button-camera">
		<ion-button shape="circle" color="dark" (click)="toggleTorch()">
			<ion-icon slot="icon-only" name="flashlight"></ion-icon>
		</ion-button>
	</div>
	<div class="button-camera">
		<ion-button shape="circle" color="dark" (click)="scannerStop()">
			<ion-icon slot="icon-only" name="close-outline"></ion-icon>
		</ion-button>
	</div>
</div>
```

In **camera.component.scss**
```scss
ion-button[shape=circle] {
	--border-radius: 50%;
	width: 56px;
	height: 56px;
}
```

We zijn op het punt gekomen waar we de scanner kunnen starten en stoppen. Verder zouden we ook nog gebruik willen maken van de flashlight. De knop is immers al voorzien, we moeten alleen nog de functionaliteit voorzien:

In **code-scanner.service.ts**
```ts
	export class CodeScannerService {
		...
		private _isTorchActive$ = new BehaviorSubject<boolean>(false);
		get isTorchActive$() { return this._isTorchActive$; }
		
		...

		async toggleTorch() {
			const isTorchActive: boolean = this._isTorchActive$.value;

			if (!isTorchActive) {
				this._isTorchActive$.next(true);
				return await BarcodeScanner.enableTorch();
			}
			this._isTorchActive$.next(false);
			return await BarcodeScanner.disableTorch();
		}
		...
	}
```

Aangezien we moeten bijhouden wanneer de flashlight wordt geactiveerd, zullen we dit ergens moeten bijhouden. Op die manier kunnen we ook op de UI tonen wanneer de flashlight aan staat. Dit doen we met de variabele "_isTorchActive$", een observable. 

Door te *subscriben* op de observable kunnen we bepaalde UI elementen tonen, verbergen, uitschakelen, ... wanneer we de flashlight activeren. In ons geval zullen we de button van de flashlight van kleur doen veranderen wanneer deze aanstaat:

In **camera.component.ts**
```typescript
// Angular
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
	selector: 'camera',
	templateUrl: './camera.component.html',
	styleUrls: ['./camera.component.scss'],
})
export class CameraComponent implements OnInit, OnDestroy {
	private _subscriptions: Subscription[];

	private _isTorchActive: boolean;
	get isTorchActive(): boolean { return this._isTorchActive; }

	constructor(
		private codeScannerService: CodeScannerService
	) { }

	ngOnInit(): void {
		this._subscriptions = [
			this.codeScannerService.isTorchActive$.subscribe((isTorchActive: boolean) => {
				this._isTorchActive = isTorchActive;
			}),
		];
	}

	ngOnDestroy(): void {
		this._subscriptions.forEach(subscription => subscription.unsubscribe());
	}

	scannerStop(): void {
		this.codeScannerService.scannerStop();
	}

	toggleTorch(): void {
		this.codeScannerService.toggleTorch();
	}
}
```

In **camera.component.html**
```html 
...
<ion-button shape="circle" color="{{isTorchActive ? 'light' : 'dark'}}" (click)="toggleTorch()">
	<ion-icon slot="icon-only" name="flashlight"></ion-icon>
</ion-button>
...
```

Nu we een volledig functionele scanner hebben, rest ons enkel nog het afhandelen van het resultaat van de scan:

In **code-scanner.service.ts**
```typescript
export class CodeScannerService {
	...
	async scannerStart() {
		...
		const result: ScanResult = await BarcodeScanner.startScan(options);

		if (result.hasContent) {
			console.log(result.content)
		}
		this.scannerStop();
		...
	}
	...
}
```