import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from 'axios';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private axiosClient: AxiosInstance;

  constructor() {
    // Initialize Axios instance
    this.axiosClient = axios.create({
      baseURL: 'https://your-dotnet-api-url.com/api', // Your .NET MVC API base URL
      timeout: 10000, // Set a timeout (optional)
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  // CRUD Methods
  async get<T>(endpoint: string): Promise<T> {
    const response = await this.axiosClient.get<T>(endpoint);
    return response.data;
  }

  async post<T>(endpoint: string, payload: any): Promise<T> {
    const response = await this.axiosClient.post<T>(endpoint, payload);
    return response.data;
  }

  async put<T>(endpoint: string, payload: any): Promise<T> {
    const response = await this.axiosClient.put<T>(endpoint, payload);
    return response.data;
  }

  async delete<T>(endpoint: string): Promise<T> {
    const response = await this.axiosClient.delete<T>(endpoint);
    return response.data;
  }
}
