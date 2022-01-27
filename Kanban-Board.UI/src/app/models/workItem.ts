import { user } from './user';
import { status } from './status';

export interface workItem {
  id: number;
  title: string;
  description: string;
  user: user;
  status: status;
}
