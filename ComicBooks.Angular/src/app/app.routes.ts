import { Routes } from '@angular/router';
import { FloorplanListComponent } from './floorplan-list/floorplan-list.component';
import { FloorplanEditComponent } from './floorplan-edit/floorplan-edit.component';

export const routes: Routes = [
    { path: 'floorplans', component: FloorplanListComponent },
    { path: 'floorplans/:id', component: FloorplanEditComponent },
    { path: '',   redirectTo: '/floorplans', pathMatch: 'full' }
];
