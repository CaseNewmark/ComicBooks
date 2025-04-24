import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiClientService, FloorPlanDto } from '../services/api-client-service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'ComicBook.Angular';

  floorPlanName: string = '';
  createdFloorPlan?: FloorPlanDto;
  floorPlans?: FloorPlanDto[];

  constructor(private apiClient: ApiClientService) { }

  ngOnInit(): void {
    this.loadFloorPlans();
  }

  loadFloorPlans(): void {
    this.apiClient.floorplansAll().subscribe(floorPlans => {
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

  deleteFloorPlan(id: string): void {
    this.apiClient.deleteFloorPlan(id).subscribe(() => {
      this.loadFloorPlans();
    });
  }

  editFloorPlan(id: string): void {
    // Implement navigation to edit page if needed
    // For example: this.router.navigate(['/floorplans/edit', id]);
  }
}
