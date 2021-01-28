import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {PostsService} from '../../shared/services/posts.service';
import {switchMap} from 'rxjs/operators';
import {Post} from '../../shared/models/post.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Subscription} from 'rxjs';
import {AlertService} from '../shared/services/alert.service';

@Component({
  selector: 'app-edit-page',
  templateUrl: './edit-page.component.html',
  styleUrls: ['./edit-page.component.scss']
})
export class EditPageComponent implements OnInit, OnDestroy {

  form: FormGroup;
  post: Post;
  uSub: Subscription;
  submitted: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private postsService: PostsService,
    private alertService: AlertService
  ) {
  }

  ngOnInit(): void {
    this.route.params.pipe(
      switchMap( (params: Params) => {
        return this.postsService.getById(params.id);
      })
    ).subscribe( (post: Post) => {
      this.post = post;
      this.form = new FormGroup({
        title: new FormControl(post.title, [Validators.required]),
        text: new FormControl(post.text, [Validators.required]),
      });
    });
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }
    this.submitted = true;

    this.uSub = this.postsService.update({
      ...this.post,
      title: this.form.value.title,
      text: this.form.value.text,
    })
      .subscribe(() => {
        this.submitted = false;
        this.alertService.success('The post was updated!');
        this.router.navigate(['/admin', 'dashboard']);
    });
  }

  ngOnDestroy(): void {
    this.uSub?.unsubscribe();
  }
}
