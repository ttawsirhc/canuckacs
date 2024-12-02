import { Component, OnInit } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { RouterModule, RouterLink, RouterOutlet } from '@angular/router';
import { ApiService } from './api.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HomeComponent, RouterLink, RouterOutlet],
  template: `
    <main>
      <a [routerLink]="['/']">
        <header class="brand-name">
          <img class="brand-logo" src="/assets/logo.svg" alt="logo" aria-hidden="true" />
        </header>
      </a>
      <section class="content">
        <router-outlet></router-outlet>
      </section>
    </main>
  `,  
  // template: `<h1>Hello World!</h1>`,
  /*
  template: `
  <main>
    <header class="brand-name">
      <img class="brand-logo" src="/assets/logo.svg" alt="logo" aria-hidden="true" />
    </header>
  </main>
  `,
  */
  /*
  imports: [HomeComponent, RouterModule, RouterLink, RouterOutlet],
  template: `
  <main>
    <header class="brand-name">
      <img class="brand-logo" src="/assets/logo.svg" alt="logo" aria-hidden="true" />
    </header>
    <section class="content">
      <router-outlet></router-outlet>
      <!-- <app-home></app-home> -->
    </section>
  </main>
  `,
  */
  styleUrls: ['./app.component.css'],
}) // end @Component
export class AppComponent implements OnInit {
  title = 'homes'; // original: title = 'default';

  data: any;
  newItem = { name: 'Example Item' };

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadData();
  }

  async loadData() {
    try {
      this.data = await this.apiService.get('http://localhost:5062/api/Tags'); // GET: Fetch data from "/items"
      console.log('Data fetched:', this.data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  }

  async addItem() {
    try {
      const result = await this.apiService.post('/items', this.newItem); // POST: Add new item
      console.log('Item added:', result);
    } catch (error) {
      console.error('Error adding item:', error);
    }
  }

  async updateItem(id: number) {
    try {
      const result = await this.apiService.put(`/items/${id}`, { name: 'Updated Item' }); // PUT: Update item
      console.log('Item updated:', result);
    } catch (error) {
      console.error('Error updating item:', error);
    }
  }

  async deleteItem(id: number) {
    try {
      const result = await this.apiService.delete(`/items/${id}`); // DELETE: Remove item
      console.log('Item deleted:', result);
    } catch (error) {
      console.error('Error deleting item:', error);
    }
  }
} // end class AppComponent
