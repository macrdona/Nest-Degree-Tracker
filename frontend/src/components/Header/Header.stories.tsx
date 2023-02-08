import { Story, Meta } from '@storybook/react';
import Header from './Header';

export default {
    title: 'Header',
    component: Header,
} as Meta;

const Template: Story<typeof Header> = (props) => <Header {...props} />;
export const Base = Template.bind({});