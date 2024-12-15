import api from './api';
import { AuthRequest } from '../models/AuthRequest';

class AuthService {
  static async login(credentials: AuthRequest): Promise<{ token: string }> {
    const response = await api.post<{ token: string }>('/auth/login', credentials);
    return response.data;
  }

  static async register(data: { username: string; password: string }): Promise<void> {
    await api.post('/auth/register', data);
  }

  static async logout(): Promise<void> {
    try {
      const token = this.getToken();
      if (token) {
        await api.post('/auth/logout', {}, { headers: { Authorization: `Bearer ${token}` } });
      }
    } catch (error) {
      console.error('Error logging out:', error);
    } finally {
      this.removeToken();
    }
  }

  static saveToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  static getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  static removeToken(): void {
    localStorage.removeItem('authToken');
  }

  static isAuthenticated(): boolean {
    return !!this.getToken();
  }
}

export default AuthService;
