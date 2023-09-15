import { Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/shared/models/post';
import { PostService } from 'src/app/shared/post.service';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss'],
})
export class UpdateComponent implements OnInit {
  post?: Post;
  id: string | null = this.route.snapshot.paramMap.get('id');

  postForm: FormGroup = new FormGroup({
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    picture: new FormControl('', Validators.required),
    categoryId: new FormControl('', Validators.required),
    steps: new FormArray([]),
  });
  constructor(
    private fb: FormBuilder,
    private postService: PostService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getPost();
  }

  getPost() {
    if (this.id) {
      this.postService.getPost(this.id).subscribe({
        next: (post) => {
          this.post = post;
          this.post.steps = post.steps.sort((a, b) => {
            return a.stepNumber - b.stepNumber;
          });
          const stepControls = this.post.steps.map((step) => {
            return this.fb.group({
              id: [step.id],
              instruction: [step.instruction, Validators.required],
              stepNumber: [step.stepNumber, Validators.required],
            });
          });
          this.postForm = this.fb.group({
            title: [this.post.title, Validators.required],
            description: [this.post.description, Validators.required],
            picture: [this.post.picture, Validators.required],
            categoryId: [this.post.category, Validators.required],
            steps: this.fb.array(stepControls) as FormArray,
          });
        },
        error: (error) => console.log(error),
      });
    }
  }

  get steps() {
    return this.postForm.get('steps') as FormArray;
  }

  addStep() {
    this.steps.push(
      this.fb.group({
        instruction: ['', Validators.required],
        stepNumber: [this.steps.length + 1, Validators.required],
      })
    );
  }

  removeStep(index: number) {
    this.steps.removeAt(index);
  }

  submitForm() {
    if (this.postForm.valid && this.id) {
      const formData = this.postForm.value;
      this.postService.updatePost(this.id, formData).subscribe({
        next: () => this.router.navigateByUrl('/posts'),
      });
    }
  }
}
