import { Component } from '@angular/core';
import { HomeService } from 'src/app/home/home.service';
import { Post } from 'src/app/shared/models/post';
import { PostParams } from 'src/app/shared/models/postParams';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss'],
})
export class PostsComponent {
  postParams = new PostParams();
  posts: Post[] = [];
  totalCount = 0;

  constructor(private homeService: HomeService) {}

  getPosts() {
    this.homeService.getPosts(this.postParams).subscribe({
      next: (response) => {
        this.posts = response.data;
        this.postParams.pageIndex = response.pageIndex;
        this.postParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: (error) => console.log(error),
    });
  }
}
