import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PostParams } from './models/postParams';
import { Pagination } from './models/pagination';
import { Post, PostFormData } from './models/post';
import { environment } from 'src/environments/environment';
import { Category } from './models/category';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Get posts matching supplied post params
  getPosts(postParams: PostParams) {
    let params = new HttpParams();

    if (postParams.categoryId)
      params = params.append('categoryId', postParams.categoryId);
    if (postParams.search) params = params.append('search', postParams.search);
    if (postParams.byUser) params = params.append('byUser', true);
    params = params.append('sort', postParams.sort);
    params = params.append('pageIndex', postParams.pageIndex);
    params = params.append('pageSize', postParams.pageSize);

    return this.http.get<Pagination<Post[]>>(this.baseUrl + 'posts', {
      params,
    });
  }

  getPost(id: string) {
    return this.http.get<Post>(this.baseUrl + 'posts/' + id);
  }

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'posts/categories');
  }

  createLike(id: string) {
    return this.http.post<void>(this.baseUrl + 'likes/' + id, {});
  }

  deleteLike(id: string) {
    return this.http.delete<void>(this.baseUrl + 'likes/' + id);
  }

  createPost(post: PostFormData) {
    return this.http.post<Post>(this.baseUrl + 'posts', post);
  }

  updatePost(id: string, post: PostFormData) {
    return this.http.put<void>(this.baseUrl + 'posts/' + id, post);
  }

  deletePost(id: string) {
    return this.http.delete<void>(this.baseUrl + 'posts/' + id);
  }

  // Upload picture as form data
  uploadPicture(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(this.baseUrl + 'image', formData, {
      responseType: 'text',
      observe: 'response',
    });
  }

  deletePicture(id: string) {
    return this.http.delete<void>(this.baseUrl + 'image/' + id);
  }
}
