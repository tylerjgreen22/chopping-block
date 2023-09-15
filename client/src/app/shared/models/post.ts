import { Step } from './step';

// Interface for the posts retrieved from the backend
export interface Post {
  id: string;
  title: string;
  description: string;
  picture: string;
  category: string;
  user: string;
  likes: number;
  isLiked: boolean;
  steps: Step[];
}

export interface PostFormData {
  title?: string | null;
  description?: string | null;
  picture?: string | null;
  categoryId?: string | null;
  steps?: Step[];
}
