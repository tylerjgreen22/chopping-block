import { Component, Input } from '@angular/core';

import { Post } from 'src/app/shared/models/post';

// Component for rendering an individual post
@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent {
  @Input() post?: Post;
}
