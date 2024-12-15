import api from './api';
import { Task } from '../models/Task';
import { TaskQueryParams } from '../models/TaskQueryParams';

class TaskService {
  static async getTasks(params: TaskQueryParams): Promise<Task[]> {
    const response = await api.get<Task[]>('/tasks', { params });
    return response.data;
  }

  static async getTaskById(taskId: string): Promise<Task> {
    const response = await api.get<Task>(`/tasks/${taskId}`);
    return response.data;
  }

  static async createTask(task: { name: string; isCompleted: boolean }): Promise<void> {
    await api.post('/tasks', task);
  }

  static async updateTask(task: { id: string; name: string; isCompleted: boolean }): Promise<void> {
    await api.put(`/tasks/${task.id}`, task);
  }

  static async deleteTask(taskId: number): Promise<void> {
    await api.delete(`/tasks/${taskId}`);
  }
}

export default TaskService;
