import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '',redirectTo:'homepage',pathMatch: 'full'},
  {path: 'login',component:LoginComponent ,canActivate:[] },
  {path: 'signup',component: RegisterComponent,canActivate:[] },
  {path: 'homepage',component: HomePageComponent}
  {path: 'user/blogs/:id' ,component: BlogComponent},
  {path: 'user/blogs/addblog' ,component: AddBlogComponent},
  {path: 'user/blogs/editblog' ,component: EditComponent},
  {path: 'user/blogs/deleteblog' ,component: DeleteComponent}, 
  {path: 'user/blogs/viewblogs' ,component: BlogComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
