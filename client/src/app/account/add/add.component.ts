import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { HomeService } from 'src/app/home/home.service';
import { Category } from 'src/app/shared/models/category';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent implements OnInit {
  addForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    picture: new FormControl('', [Validators.required]),
    recipeCategory: new FormControl('', [Validators.required]),
  });
  categories: Category[] = [];

  constructor(private homeService: HomeService) {}

  ngOnInit(): void {
    this.getCategories();
  }

  onSubmit() {
    console.log(this.addForm.value);
  }

  getCategories() {
    this.homeService.getCategories().subscribe({
      next: (response) => (this.categories = response),
      error: (error) => console.log(error),
    });
  }
}
