import { Component, ElementRef, ViewChild } from '@angular/core';
import { Post } from '../models/post';
import { Category } from '../models/category';
import { PostParams } from '../models/postParams';
import { PostService } from '../post.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss'],
})
export class FeedComponent {
  @ViewChild('search') searchTerm?: ElementRef;
  posts: Post[] = [];
  categories: Category[] = [];
  postParams = new PostParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Most liked', value: 'likesDesc' },
    { name: 'New', value: 'newest' },
  ];
  totalCount = 0;
  currentRoute?: string;

  // Injecting Home Service to get information from the backend
  constructor(private postService: PostService, public router: Router) {}

  // On page load, get the posts and the categories using class methods
  ngOnInit(): void {
    this.getPosts();
    this.getCategories();
  }

  // Subscribes to the observable returned from the home service getPosts method, sets posts and pagination info
  getPosts() {
    if (this.router.url === '/posts') this.postParams.byUser = true;
    this.postService.getPosts(this.postParams).subscribe({
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
    this.postService.getCategories().subscribe({
      next: (response) =>
        (this.categories = [{ id: 0, name: 'All' }, ...response]),
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

  onLike(id: string) {
    this.postService.createLike(id).subscribe({
      next: () => this.getPosts(),
    });
  }

  onUnlike(id: string) {
    this.postService.deleteLike(id).subscribe({
      next: () => this.getPosts(),
    });
  }

  onDelete(id: string) {
    this.postService.deletePost(id).subscribe({
      next: () => this.getPosts(),
    });
  }
}
