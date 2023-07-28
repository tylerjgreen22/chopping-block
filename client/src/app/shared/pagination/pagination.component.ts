import { Component, EventEmitter, Input, Output } from '@angular/core';

// Component for pagination, obtained from Angular Bootstrap: https://valor-software.com/ngx-bootstrap/#/components/pagination?tab=overview
@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent {
  @Input() totalCount?: number;
  @Input() pageSize?: number;
  @Output() pageChanged = new EventEmitter<number>();

  // When the component is changed, it emits an event containing the page index needed to update the shown posts
  onPaginationChanged(event: any) {
    this.pageChanged.emit(event.page);
  }
}
