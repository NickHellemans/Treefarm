import { TestBed } from '@angular/core/testing';

import { CodeScannerService } from './code-scanner.service';

describe('CodeScannerService', () => {
	let service: CodeScannerService;

	beforeEach(() => {
		TestBed.configureTestingModule({});
		service = TestBed.inject(CodeScannerService);
	});

	it('should be created', () => {
		expect(service).toBeTruthy();
	});
});
