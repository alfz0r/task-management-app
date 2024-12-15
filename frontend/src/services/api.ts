import axios from 'axios';
import AuthService from './AuthService';

const api = axios.create({
  baseURL: 'https://localhost:7284/api',
});

api.interceptors.request.use((config) => {
  const token = AuthService.getToken();
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;