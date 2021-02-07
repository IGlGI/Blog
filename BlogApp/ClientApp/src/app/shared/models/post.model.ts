export interface Post {
  id?: string;
  text: string;
  title: string;
  author: string;
  created?: Date;
  modified: Date;
  isDeleted?: boolean;
}
