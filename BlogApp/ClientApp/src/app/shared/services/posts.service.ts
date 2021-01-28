import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Post} from '../models/post.model';
import {environment} from '../../../environments/environment';
import {FbCreateResponse} from '../models/fb-create-response.model';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  constructor(private http: HttpClient) { }

  create(post: Post): Observable<any>{
    return this.http.post<any>(`${environment.dbConnectionString}/posts.json`, post)
      .pipe(
        map((response: FbCreateResponse) => {
          return {
            ...post,
            id: response,
            date: new Date(post.date)
          };
        })
      );
  }

  getAll(): Observable<Post[]>{
    return this.http.get(`${environment.dbConnectionString}/posts.json`)
      .pipe(
        map((response: {[key: string]: any}) => {
          return Object
            .keys(response)
            .map(key => ({
              ...response[key],
              id: key,
              date: new Date(response[key].date)
            }));
        })
      );
  }

  getById(id: string): Observable<Post>{
    return this.http.get<Post>(`${environment.dbConnectionString}/posts/${id}.json`)
      .pipe(
        map((post: Post) => {
          return {
            ...post, id,
            date: new Date(post.date)
          };
        })
    );
  }

  update(post: Post): Observable<Post>{
    return this.http.patch<Post>(`${environment.dbConnectionString}/posts/${post.id}.json`, post);
  }

  delete(id: string): Observable<void>{
    return this.http.delete<void>(`${environment.dbConnectionString}/posts/${id}.json`);
  }
}
