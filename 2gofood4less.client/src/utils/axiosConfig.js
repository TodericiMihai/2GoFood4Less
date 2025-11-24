import axios from 'axios';

// Create axios instance
const api = axios.create({
  baseURL: 'https://localhost:51959/api'
});

// Add request interceptor to include token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Add response interceptor to handle 401 errors
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Token expired or invalid
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      // Optional: Redirect to login if not already there
      // window.location.href = '/login'; 
    }
    return Promise.reject(error);
  }
);

export default api;
