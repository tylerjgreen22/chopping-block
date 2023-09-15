import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PostService } from 'src/app/shared/post.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent {
  constructor(
    private fb: FormBuilder,
    private postService: PostService,
    private router: Router
  ) {}

  postForm = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    picture: ['', Validators.required],
    categoryId: ['', Validators.required],
    steps: this.fb.array([]) as FormArray,
  });

  get steps() {
    return this.postForm.get('steps') as FormArray;
  }

  onPictureSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.postService.uploadPicture(file).subscribe({
        next: (response) => this.postForm.get('picture')?.setValue(response),
      });
    }
  }

  removeImage() {
    // this.postService.deletePicture(this.postForm.get('picture'))
    console.log('remove picture');
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
    if (this.postForm.valid) {
      const formData = this.postForm.value;
      this.postService.createPost(formData).subscribe({
        next: () => this.router.navigateByUrl('/posts'),
      });
    }
  }
}
