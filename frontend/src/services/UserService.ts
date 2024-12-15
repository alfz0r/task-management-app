import api from './api';
import { User } from '../models/User';

class UserService {
  static async getAllUsers(): Promise<User[]> {
    const response = await api.get<User[]>('/user/all');
    return response.data;
  }

  static async getUserProfile(): Promise<{ username: string }> {
    const response = await api.get<{ username: string }>('/user/profile');
    return response.data;
  }

  static async changePassword(data: { currentPassword: string; newPassword: string }): Promise<void> {
    await api.post('/user/change-password', data);
  }
}

export default UserService;
