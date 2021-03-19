import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {PostResponse} from '../../shared/models/post-response.model';
import {PostsService} from '../../shared/services/posts.service';
import {AlertService} from '../shared/services/alert.service';
import {PostRequest} from '../../shared/models/post-request';

@Component({
  selector: 'app-create-page',
  templateUrl: './create-page.component.html',
  styleUrls: ['./create-page.component.scss']
})
export class CreatePageComponent implements OnInit {

  form: FormGroup;

  constructor(
    private postsService: PostsService,
    private alertService: AlertService
  ) {
    this.form = new FormGroup({
      title: new FormControl(null, [Validators.required]),
      text: new FormControl(null, [Validators.required]),
      author: new FormControl(null, [Validators.required])
    });
  }

  ngOnInit(): void {
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }

    const post: PostRequest = {
      title: this.form.value.title,
      text: this.form.value.text,
      authorName: this.form.value.author
    };

    this.postsService.create(post).subscribe(() => {
      this.form.reset();
      this.alertService.success('The post was successfully created!');
    });
  }
}
