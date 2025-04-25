import { Component, inject, Input, OnInit } from '@angular/core';
import { ApiClientService, FloorPlanDto } from '../../services/api-client-service';

@Component({
  selector: 'app-floorplan-edit',
  standalone: true,
  imports: [],
  templateUrl: './floorplan-edit.component.html',
  styleUrl: './floorplan-edit.component.css'
})
export class FloorplanEditComponent implements OnInit {

  @Input()
  set id(planId: string) {
    this.planId = planId;
  }

  private planId: string | undefined = undefined;
  public floorPlan: FloorPlanDto | undefined = undefined;

  constructor(private apiClient: ApiClientService) { }

  ngOnInit(): void {
    if (!this.planId) return;
    this.apiClient.getFloorPlanById(this.planId).subscribe(floorPlan => {
      this.floorPlan = floorPlan;
    });
  }

  saveFloorPlan(): void {
    if (!this.planId || !this.floorPlan) return;
    this.apiClient.updateFloorPlan(this.planId, this.floorPlan).subscribe(() => {
      console.log(`Updated FloorPlan with ID: ${this.planId}`);
    });
  }
}
