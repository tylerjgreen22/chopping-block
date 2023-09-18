import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/shared/models/post';
import { Step } from 'src/app/shared/models/step';
import { PostService } from 'src/app/shared/post.service';

// Component to show details of an individual post
@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss'],
})
export class PostDetailComponent implements OnInit {
  post?: Post;
  steps?: Step[];

  // Injecting post service to access backend and route to grab id from route params
  constructor(
    private postService: PostService,
    private route: ActivatedRoute
  ) {}

  // On page load, loads the individual product using class method
  ngOnInit(): void {
    this.loadPost();
  }

  // Grabs id from route params, uses post service with Id to retrieve post
  loadPost() {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.postService.getPost(id).subscribe({
        next: (post) => {
          this.post = post;
          this.steps = post.steps.sort((a, b) => {
            return a.stepNumber - b.stepNumber;
          });
        },
        error: (error) => console.log(error),
      });
    }
  }
}
