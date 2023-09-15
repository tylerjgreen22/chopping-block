import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';

import { Post } from 'src/app/shared/models/post';

// Component for rendering an individual post
@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent {
  @Input() post?: Post;
  @Output() likeClicked = new EventEmitter<string>();
  @Output() unlikeClicked = new EventEmitter<string>();
  @Output() deleteClicked = new EventEmitter<string>();

  constructor(public router: Router) {}

  likePost() {
    this.likeClicked.emit(this.post?.id);
  }

  unlikePost() {
    this.unlikeClicked.emit(this.post?.id);
  }

  deletePost() {
    this.deleteClicked.emit(this.post?.id);
  }
}
