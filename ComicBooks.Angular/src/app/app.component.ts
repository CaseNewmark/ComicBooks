import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiClientService, FloorPlanDto } from '../services/api-client-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'ComicBook.Angular';

  floorPlanName: string = '';
  createdFloorPlan?: FloorPlanDto;
  floorPlans?: FloorPlanDto[] = undefined;

  constructor(private apiClient: ApiClientService) {

  }

  ngOnInit(): void {
    this.loadFloorPlans();
  }

  loadFloorPlans(): void {
    this.apiClient.getAllFloorPlans().subscribe(floorPlans => {
      this.floorPlans = floorPlans;
    });
  }

  createFloorPlan(): void {
    if (!this.floorPlanName.trim()) return;
    this.apiClient.createFloorPlan(this.floorPlanName).subscribe(created => {
      this.createdFloorPlan = created;
      this.loadFloorPlans();
      this.floorPlanName = '';
    });
  }

  deleteFloorPlan(id: string|undefined): void {
    if (!id) return;
    this.apiClient.deleteFloorPlan(id).subscribe(() => {
      this.loadFloorPlans();
    }, (error) => {
      if (error.status === 204) return;
      console.error('Error deleting floor plan:', error);
    });
  }

  editFloorPlan(plan: FloorPlanDto): void {
    if (!plan.id) return;
    this.apiClient.updateFloorPlan(plan.id, plan).subscribe(() => {
      console.log(`Edit FloorPlan with ID: ${plan.id}`);
    },
    (error) => {
      console.error('Error updating floor plan:', error);
    });
    // Implement navigation to edit page if needed
    // For example: this.router.navigate(['/floorplans/edit', id]);
  }
}
