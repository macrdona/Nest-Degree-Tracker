import { Story, Meta } from '@storybook/react';
import App from './App';

export default {
    title: 'App',
    component: App,
} as Meta;

const Template: Story<typeof App> = (props) => <App {...props} />;
export const Base = Template.bind({});