import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {QuillModule} from 'ngx-quill';
import {SearchPipe} from './pipes/search.pipe';
import {AlertService} from '../admin/shared/services/alert.service';

const toolbarOptions = [
  ['bold', 'italic', 'underline', 'strike'],
  ['blockquote', 'code-block'],
  [{ header: 1 }, { header: 2 }],
  [{ list: 'ordered'}, { list: 'bullet' }],
  [{ script: 'sub'}, { script: 'super' }],
  [{ direction: 'rtl' }],
  [{ size: ['small', false, 'large', 'huge'] }],
  [{ header: [1, 2, 3, 4, 5, 6, false] }],
  [{ color: [] }, { background: [] }],
  [{ font: [] }],
  [{ align: [] }],
  ['clean']
];

@NgModule({
  declarations: [SearchPipe],
  imports: [
    CommonModule,
    QuillModule.forRoot({
      modules: {
        toolbar: toolbarOptions,
      },
    })
  ],
  providers: [AlertService],
  exports: [
    QuillModule,
    SearchPipe,
    HttpClientModule,
    CommonModule
  ]
})
export class SharedModule { }
