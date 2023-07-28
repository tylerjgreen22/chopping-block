import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

import { Post } from '../shared/models/post';
import { PostParams } from '../shared/models/postParams';
import { HomeService } from './home.service';
import { Category } from '../shared/models/category';

// Home page component, first component seen when visiting the site
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  posts: Post[] = [];
  categories: Category[] = [];
  postParams = new PostParams();
  sortOptions = [{ name: 'Alphabetical', value: 'name' }];
  totalCount = 0;

  // Injecting Home Service to get information from the backend
  constructor(private homeService: HomeService) {}

  // On page load, get the posts and the categories using class methods
  ngOnInit(): void {
    this.getPosts();
    this.getCategories();
  }

  // Subscribes to the observable returned from the home service getPosts method, sets posts and pagination info
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

  // Subscribes to the observable returned from the home service getCategories method, sets categories, as well as a default 'All' category
  getCategories() {
    this.homeService.getCategories().subscribe({
      next: (response) =>
        (this.categories = [{ id: 0, category: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }

  // When the category is changed, updates the current post parameters
  onCategorySelected(event: any) {
    this.postParams.categoryId = event.target.value;
    this.postParams.pageIndex = 1;
    this.getPosts();
  }

  // When the sorting option is changed, updates the current post parameters
  onSortSelected(event: any) {
    this.postParams.sort = event.target.value;
    this.getPosts();
  }

  // When the page is changed, updates the current post parameters
  onPageChanged(event: any) {
    if (this.postParams.pageIndex !== event) {
      this.postParams.pageIndex = event;
      this.getPosts();
    }
  }

  // When a search is made, updates the current post parameters
  onSearch() {
    this.postParams.search = this.searchTerm?.nativeElement.value;
    this.postParams.pageIndex = 1;
    this.getPosts();
  }

  // When the reset button is pressed, creates a new post params object to reset the post parameters to defaults
  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.postParams = new PostParams();
    this.getPosts();
  }
}
