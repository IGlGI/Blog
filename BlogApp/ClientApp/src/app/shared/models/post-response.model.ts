export interface PostResponse {
  id: string;
  text: string;
  title: string;
  authorName: string;
  created?: Date;
  modified: Date;
  isDeleted?: boolean;
}
