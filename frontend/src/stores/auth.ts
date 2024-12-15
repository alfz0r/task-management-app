import { defineStore } from 'pinia';
import AuthService from '../services/AuthService';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    isAuthenticated: AuthService.isAuthenticated(),
  }),
  actions: {
    login(token: string) {
      AuthService.saveToken(token);
      this.isAuthenticated = true;
    },
    logout() {
      AuthService.removeToken();
      this.isAuthenticated = false;
    },
  },
});
