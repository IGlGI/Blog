import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PostResponse} from '../models/post-response.model';
import {environment} from '../../../environments/environment';
import {FbCreateResponse} from '../models/fb-create-response.model';
import {map} from 'rxjs/operators';
import {PostRequest} from '../models/post-request';
import {Envelope} from '../models/envelope';

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

  create(post: PostRequest): Observable<any>{
    return this.http.post<any>(this.postApiUrl, post, { params: this.v1ApiParams })
      .pipe(
        map((response: Envelope) => {
          return response.result;
        })
      );
  }

  getAll(): Observable<PostResponse[]>{
    return this.http.get(this.postApiUrl, { params: this.v1ApiParams })
      .pipe(
        map((response: Envelope) => {
          return response.result;
        })
      );
  }

  getById(id: string): Observable<PostResponse>{
    return this.http.get<any>(`${this.postApiUrl}/${id}`, { params: this.v1ApiParams })
      .pipe(
        map((response: Envelope) => {
          return response.result;
        })
    );
  }

  update(post: PostResponse): Observable<PostResponse>{
    return this.http.put<PostResponse>(`${this.postApiUrl}/${post.id}`, post, { params: this.v1ApiParams });
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
