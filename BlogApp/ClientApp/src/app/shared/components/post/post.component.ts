import {Component, Input, OnInit} from '@angular/core';
import {PostResponse} from '../../models/post-response.model';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {

  @Input() post: PostResponse;

  constructor() { }

  ngOnInit(): void {
  }

}
