import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiClientService, FloorPlanDto } from '../../services/api-client-service';

@Component({
  selector: 'app-floorplan-list',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './floorplan-list.component.html',
  styleUrl: './floorplan-list.component.css'
})
export class FloorplanListComponent implements OnInit {
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
    });
  }

  editFloorPlan(plan: FloorPlanDto): void {
    if (!plan.id) return;
    
    // this.apiClient.updateFloorPlan(plan.id, plan).subscribe(() => {
    //   console.log(`Edit FloorPlan with ID: ${plan.id}`);
    // });

    //this.router.navigate(['/floorplans/edit', id]);
  }

}
