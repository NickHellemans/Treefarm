import { Pipe, PipeTransform } from '@angular/core';
import { Task } from '../interfaces/task.interface';

@Pipe({
	name: 'sort'
})
export class SortPipe implements PipeTransform {

	transform(tasksArray: Task[]): unknown {
		if (!Array.isArray(tasksArray)) {
			return;
		}

		tasksArray.sort((a: Task, b: Task) => {
			if (a.priority === 0) {
				return 1;
			}
			else if (a.priority < b.priority) {
				return -1;
			}
			else if (a.priority > b.priority) {
				return 1;
			}
			else {
				if (a.zone.id < b.zone.id) {
					return -1;
				}
				else if (a.zone.id > b.zone.id) {
					return 1;
				}
				else {
					if (a.name < b.name) {
						return -1;
					}
					else if (a.name > b.name) {
						return 1;
					}
					else {
						return 0;
					}
				}
			}
		});
		return tasksArray;
	}

}
