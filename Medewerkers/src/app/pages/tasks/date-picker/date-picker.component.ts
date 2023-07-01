import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
	selector: 'date-picker',
	templateUrl: './date-picker.component.html',
	styleUrls: ['./date-picker.component.scss'],
})
export class DatePickerComponent implements OnInit {

	@Output("dateSelected") _dateSelectedChange = new EventEmitter<Date>();
	@Input("dateSelected") private _dateSelected: Date;
	get dateSelected() {return `${this._dateSelected.getDate()}/${this._dateSelected.getMonth() + 1}/${this._dateSelected.getFullYear()}`; }

	private _showDatePicker = false;
	get showDatePicker() { return this._showDatePicker; }
	

	constructor() {}

	ngOnInit() { }

	public toggleDatePicker(): void{
		this._showDatePicker = !this._showDatePicker;
	}
	
	public dateForward(): void {
		this._dateSelected.setDate(this._dateSelected.getDate() + 1);
		this._emitDate();
	}

	public dateBack(): void {
		this._dateSelected.setDate(this._dateSelected.getDate() - 1);
		this._emitDate();
	}

	private _emitDate(): void {
		this._dateSelectedChange.emit(this._dateSelected)
	}
}
