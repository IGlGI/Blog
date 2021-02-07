import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Post} from '../models/post.model';
import {environment} from '../../../environments/environment';
import {FbCreateResponse} from '../models/fb-create-response.model';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private readonly postApiUrl: string;
  private readonly v1ApiParams: HttpParams;

  constructor(private http: HttpClient) {
    this.postApiUrl = `${environment.serverConnectionString}/api/post`;
    this.v1ApiParams = this.setV1Api();
  }

  create(post: Post): Observable<any>{
    return this.http.post<any>(this.postApiUrl, post, { params: this.v1ApiParams })
      .pipe(
        map((response: FbCreateResponse) => {
          return {
            ...post,
            id: response,
            date: new Date(post.modified)
          };
        })
      );
  }

  getAll(): Observable<Post[]>{
    return this.http.get(this.postApiUrl, { params: this.v1ApiParams })
      .pipe(
        map((response: {[key: string]: any}) => {
          return Object
            .keys(response)
            .map(key => ({
              ...response[key],
              id: response[key].id,
              modified: new Date(response[key].modified)
            }));
        })
      );
  }

  getById(id: string): Observable<Post>{
    return this.http.get<Post>(`${this.postApiUrl}/${id}`, { params: this.v1ApiParams })
      .pipe(
        map((post: Post) => {
          return {
            ...post, id,
            modified: new Date(post.modified)
          };
        })
    );
  }

  update(post: Post): Observable<Post>{
    return this.http.put<Post>(`${this.postApiUrl}/${post.id}`, post, { params: this.v1ApiParams });
  }

  delete(id: string): Observable<void>{
    return this.http.delete<void>(`${this.postApiUrl}/${id}`, { params: this.v1ApiParams });
  }

  private setV1Api(): HttpParams {
    let params = new HttpParams();
    params = params.append('api-version', '1.0');
    return params;
  }
}
