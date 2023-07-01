import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { TaskButtonPauseComponent } from './task-button-pause.component';

describe('TaskButtonPauseComponent', () => {
	let component: TaskButtonPauseComponent;
	let fixture: ComponentFixture<TaskButtonPauseComponent>;

	beforeEach(waitForAsync(() => {
		TestBed.configureTestingModule({
			declarations: [TaskButtonPauseComponent],
			imports: [IonicModule.forRoot()]
		}).compileComponents();

		fixture = TestBed.createComponent(TaskButtonPauseComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	}));

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
