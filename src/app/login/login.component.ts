import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormGroup,ReactiveFormsModule, Validators } from '@angular/forms';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  constructor(private fb:FormBuilder){}

  loginForm!:FormGroup;

  ngOnInit(): void {
    this.loginForm=this.fb.group({
      email:['',Validators.required],
      password:['',[Validators.required,Validators.minLength(6)]]
    })
  }

  onSubmit()
  {
    console.log(this.loginForm);
  }
}
