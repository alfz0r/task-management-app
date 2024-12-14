export interface Task {
  id: string;
  name: string;
  isCompleted: boolean;
  userId?: number;
  createdAt?: string;
  createdBy?: string;
  modifiedAt?: string | null;
  modifiedBy?: string | null;
}
