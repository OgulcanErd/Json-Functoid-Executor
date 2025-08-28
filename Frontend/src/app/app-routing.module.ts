import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MappingComponent } from './mapping/mapping.component';

const routes: Routes = [
  { path: '', component: MappingComponent }, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
