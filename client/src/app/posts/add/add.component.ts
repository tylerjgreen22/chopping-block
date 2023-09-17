import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/shared/models/category';
import { Post } from 'src/app/shared/models/post';
import { PostService } from 'src/app/shared/post.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent implements OnInit {
  categories?: Category[];
  id: string | null = this.route.snapshot.paramMap.get('id');
  postForm = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    picture: ['', Validators.required],
    categoryId: ['', Validators.required],
    steps: this.fb.array([]) as FormArray,
  });

  constructor(
    private fb: FormBuilder,
    private postService: PostService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getCategories();
    if (this.id) this.getPost();
  }

  getPost() {
    if (this.id) {
      this.postService.getPost(this.id).subscribe({
        next: (post) => {
          post.steps = post.steps.sort((a, b) => {
            return a.stepNumber - b.stepNumber;
          });
          const stepControls = post.steps.map((step) => {
            return this.fb.group({
              id: [step.id],
              instruction: [step.instruction, Validators.required],
              stepNumber: [step.stepNumber, Validators.required],
            });
          });
          this.postForm = this.fb.group({
            title: [post.title, Validators.required],
            description: [post.description, Validators.required],
            picture: [post.picture, Validators.required],
            categoryId: [post.category, Validators.required],
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

  onCategorySelected(event: any) {
    this.postForm.get('categoryId')?.setValue(event.target.value);
  }

  onPictureSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.postService.uploadPicture(file).subscribe({
        next: (response: HttpResponse<string>) => {
          this.postForm.get('picture')?.setValue(response.body);
        },
        error: (error) => console.log(error),
      });
    }
  }

  removeImage() {
    const id = this.postForm.get('picture')?.value?.split('/')[7].split('.')[0];
    if (id) {
      this.postService
        .deletePicture(id)
        .subscribe({ next: () => this.postForm.get('picture')?.setValue('') });
    }
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

  getCategories() {
    this.postService.getCategories().subscribe({
      next: (response) =>
        (this.categories = [{ id: '', name: 'Select Category' }, ...response]),
      error: (error) => console.log(error),
    });
  }

  submitForm() {
    // if (this.postForm.valid) {
    //   const formData = this.postForm.value;
    //   this.postService.createPost(formData).subscribe({
    //     next: () => this.router.navigateByUrl('/posts'),
    //   });
    // }

    if (this.postForm.valid) {
      const formData = this.postForm.value;
      this.id
        ? this.postService.updatePost(this.id, formData).subscribe({
            next: () => this.router.navigateByUrl('/posts'),
          })
        : this.postService.createPost(formData).subscribe({
            next: () => this.router.navigateByUrl('/posts'),
          });
    }
  }
}
