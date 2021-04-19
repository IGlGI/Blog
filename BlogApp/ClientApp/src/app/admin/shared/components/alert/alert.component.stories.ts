import {AlertComponent} from './alert.component';
import {Meta, moduleMetadata, Story} from '@storybook/angular';
import {AlertService} from '../../services/alert.service';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

export default {
  title: 'Admin/Shared/Alert',
  component: AlertComponent,
  decorators: [
    moduleMetadata({
      imports: [BrowserAnimationsModule ],
      providers: [AlertService]
    })
  ]
} as Meta;

const Template: Story<AlertComponent> = (args) => ({
  props: args,
  template: '<app-alert></app-alert>',
  styleUrls: ['./alert.component.scss'],
  component: AlertComponent
});

export const Base = Template.bind({});
Base.args = {text: 'Test success message', type: 'success'};
