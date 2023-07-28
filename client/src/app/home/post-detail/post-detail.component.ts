import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/shared/models/post';
import { HomeService } from '../home.service';
import { ActivatedRoute } from '@angular/router';
import { Step } from 'src/app/shared/models/step';

// Component to show details of an individual post
@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss'],
})
export class PostDetailComponent implements OnInit {
  post?: Post;
  steps?: Step[];

  // Injecting home service to access backend and route to grab id from route params
  constructor(
    private homeService: HomeService,
    private activatedRoute: ActivatedRoute
  ) {}

  // On page load, loads the individual product using class method
  ngOnInit(): void {
    this.loadPost();
    this.loadSteps();
  }

  // Grabs id from route params, uses home service with Id (casted to int with +) to retrieve post
  loadPost() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.homeService.getPost(+id).subscribe({
        next: (post) => (this.post = post),
        error: (error) => console.log(error),
      });
    }
  }

  // Grabs id from route params, uses home service with Id to retrieve steps
  loadSteps() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.homeService.getRecipeSteps(+id).subscribe({
        next: (steps) => (this.steps = steps),
        error: (error) => console.log(error),
      });
    }
  }
}
