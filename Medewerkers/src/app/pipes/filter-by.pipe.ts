import { Pipe, PipeTransform } from '@angular/core';
import { ObjectFilter } from '../interfaces/object-filter.interface';

@Pipe({
	name: 'filterBy',
	pure: false
})

// Pure: https://angular.io/guide/pipes#detecting-pure-changes-to-primitives-and-object-references
// https://stackoverflow.com/questions/41869301/how-to-re-trigger-all-pure-pipes-on-all-component-tree-in-angular-2
export class FilterByPipe implements PipeTransform {
	transform(array: any, filters: ObjectFilter[]): any[] {
		// console.log(filters)
		if (!Array.isArray(array)) {
			return;
		}

		filters.forEach((filter: ObjectFilter) => {
				array = array.filter((element:  any) => {
					const field: string = filter.field;
					const value: any = filter.value;
					if (element[field] instanceof Date) {
						return element[field].getTime() === value;
					}
					return element[field] === value
				});
			}
		);
		return array;
	}
}