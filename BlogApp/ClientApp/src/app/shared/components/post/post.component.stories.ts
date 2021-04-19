import {PostComponent} from './post.component';
import {Meta, moduleMetadata, Story} from '@storybook/angular';
import {APP_BASE_HREF} from '@angular/common';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {RouterTestingModule} from '@angular/router/testing';

export default {
  title: 'Shared/Components/Post',
  component: PostComponent,
  decorators: [
    moduleMetadata({
      imports: [BrowserAnimationsModule, RouterTestingModule],
    })
  ]
} as Meta;

const Template: Story<PostComponent> = (args) => ({
  props: args,
  component: PostComponent
});

export const Base = Template.bind({});
Base.args = {
  post: {
    id: '1',
    text: 'demo text',
    title: 'Demo post',
    authorName: 'Author 1',
    created: Date.now(),
    modified: Date.now(),
    isDeleted: false,
  }
};
