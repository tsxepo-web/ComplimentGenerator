import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-compliment',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './compliment.component.html',
  styleUrl: './compliment.component.css',
})
export class ComplimentComponent implements OnInit {
  compliments: any[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadCompliments();
  }

  loadCompliments(): void {
    this.apiService.getAll().subscribe((data) => {
      this.compliments = data;
    });
  }

  addCompliment(newCompliment: string): void {
    this.apiService.create({ content: newCompliment }).subscribe(() => {
      this.loadCompliments();
    });
  }

  updateCompliment(id: string, updatedContent: string): void {
    this.apiService.update(id, { content: updatedContent }).subscribe(() => {
      this.loadCompliments();
    });
  }

  deleteCompliment(id: string): void {
    this.apiService.delete(id).subscribe(() => {
      this.loadCompliments();
    });
  }
}
