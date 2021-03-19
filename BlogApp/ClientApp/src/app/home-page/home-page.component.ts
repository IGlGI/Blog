import { Component, OnInit } from '@angular/core';
import {PostsService} from '../shared/services/posts.service';
import {Observable} from 'rxjs';
import {PostResponse} from '../shared/models/post-response.model';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  posts$: Observable<PostResponse[]>;

  constructor(
    private postsService: PostsService
  ) { }

  ngOnInit(): void {
    this.posts$ = this.postsService.getAll();
  }

}
