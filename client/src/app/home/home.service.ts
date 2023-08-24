import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { PostParams } from '../shared/models/postParams';
import { Pagination } from '../shared/models/pagination';
import { Post } from '../shared/models/post';
import { Category } from '../shared/models/category';
import { Step } from '../shared/models/step';
import { environment } from 'src/environments/environment';

// Injectable service that interacts with the posts endpoint to retrieve data to display on the home page
@Injectable({
  providedIn: 'root',
})
export class HomeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Gets a Pagination object with the data as an array of posts
  getPosts(postParams: PostParams) {
    let params = new HttpParams();

    if (postParams.categoryId)
      params = params.append('categoryId', postParams.categoryId);
    if (postParams.search) params = params.append('search', postParams.search);
    params = params.append('sort', postParams.sort);
    params = params.append('pageIndex', postParams.pageIndex);
    params.append('pageSize', postParams.pageSize);

    return this.http.get<Pagination<Post[]>>(this.baseUrl + 'posts', {
      params,
    });
  }

  // Gets an individual post
  getPost(id: number) {
    return this.http.get<Post>(this.baseUrl + 'posts/' + id);
  }

  addPost(values: any) {
    return this.http.post<void>(this.baseUrl + 'posts', values);
  }

  // Gets all categories
  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'posts/categories');
  }

  // Gets the steps for a given post
  getRecipeSteps(id: number) {
    return this.http.get<Step[]>(this.baseUrl + 'posts/' + id + '/recipeSteps');
  }
}
