import {Component, OnDestroy, OnInit} from '@angular/core';
import {PostsService} from '../../shared/services/posts.service';
import {PostResponse} from '../../shared/models/post-response.model';
import {Subscription} from 'rxjs';
import {AlertService} from '../shared/services/alert.service';
import {AuthService} from '../shared/services/auth.service';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit, OnDestroy{

  posts: PostResponse[];
  searchStr = '';
  pSub: Subscription;
  rSub: Subscription;

  constructor(
    private postsService: PostsService,
    private alertService: AlertService,
    private authService: AuthService
  ) {
  }

  ngOnInit(): void {
    this.pSub = this.postsService.getAll().subscribe(result => {
      console.log(result);
      this.posts = result;
    });
  }

  remove(id: string): void {
    this.rSub = this.postsService.delete(id).subscribe(() => {
      this.posts = this.posts.filter( post => post.id !== id);
      this.alertService.danger('The post was deleted!');
    });
  }

  ngOnDestroy(): void {
    this.pSub?.unsubscribe();
    this.rSub?.unsubscribe();
  }
}
