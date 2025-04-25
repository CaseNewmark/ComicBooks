import { Routes } from '@angular/router';
import { FloorplanListComponent } from './floorplan-list/floorplan-list.component';

export const routes: Routes = [
    { path: 'floorplans', component: FloorplanListComponent },
    { path: '',   redirectTo: '/floorplans', pathMatch: 'full' }
];
