// add-blog.component.ts
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog.component.html',
  styleUrls: ['./add-blog.component.css']
})
export class AddBlogComponent {
  blog = {
    title: '',
    author: '',
    category: '',
    content: ''
  };
  categories = ['Entertainment', 'Education', 'Personal Life', 'Technology', 'Travel'];
  selectedFile: File | null = null;

  constructor(private http: HttpClient) {}

  onFileChange(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('title', this.blog.title);
    formData.append('author', this.blog.author);
    formData.append('category', this.blog.category);
    formData.append('content', this.blog.content);
    if (this.selectedFile) {
      formData.append('file', this.selectedFile);
    }

    this.http.post('https://your-api-url/api/Blog/addblog', formData).subscribe(response => {
      console.log('Blog submitted successfully', response);
    }, error => {
      console.error('Error submitting blog', error);
    });
  }
}
