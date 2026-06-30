import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  user: User[] = []; // แนะนำ: ถ้าผูกกับตารางหลายคน เปลี่ยนเป็นชื่อ users จะสื่อความหมายดีขึ้นครับ
  errorMessage = '';

  // ฟอร์มสำหรับเพิ่มข้อมูลใหม่
  newProduct: Partial<Product> = { name: '', description: '', price: 0, stock: 0 };
  newUser: Partial<User> = { username: '', fullname: '', email: '', role: '' };

  constructor(
    private productService: ProductService, 
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadUser();
  }

  // ====================จัดการข้อมูลสินค้า (Product) ====================
  
  loadProducts(): void {
    this.productService.getAll().subscribe({
      next: (data) => (this.products = data),
      error: (err) => (this.errorMessage = 'ไม่สามารถโหลดข้อมูลสินค้าได้: ' + err.message)
    });
  }

  addProduct(): void {
    if (!this.newProduct.name) return;

    this.productService.create(this.newProduct).subscribe({
      next: () => {
        this.newProduct = { name: '', description: '', price: 0, stock: 0 };
        this.loadProducts();
      },
      error: (err) => (this.errorMessage = 'เพิ่มสินค้าไม่สำเร็จ: ' + err.message)
    });
  }

  deleteProduct(id: number): void {
    this.productService.delete(id).subscribe({
      next: () => this.loadProducts(),
      error: (err) => (this.errorMessage = 'ลบสินค้าไม่สำเร็จ: ' + err.message)
    });
  }

  // ==================== จัดการข้อมูลผู้ใช้งาน (User) ====================

  loadUser(): void {
    this.userService.getAll().subscribe({
      next: (data) => (this.user = data),
      error: (err) => (this.errorMessage = 'ไม่สามารถโหลดข้อมูลผู้ใช้ได้: ' + err.message)
    });
  }

  addUser(): void {
    if (!this.newUser.username) return;

    this.userService.create(this.newUser).subscribe({
      next: () => {
        this.newUser = { username: '', fullname: '', email: '', role: '' };
        this.loadUser();
      },
      error: (err) => (this.errorMessage = 'เพิ่มผู้ใช้ไม่สำเร็จ: ' + err.message)
    });
  }

  deleteUser(id: number): void {
    this.userService.delete(id).subscribe({
      next: () => this.loadUser(),
      error: (err) => (this.errorMessage = 'ลบผู้ใช้ไม่สำเร็จ: ' + err.message)
    });
  }
}