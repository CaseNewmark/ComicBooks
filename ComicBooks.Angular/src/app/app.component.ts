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

  floorPlans: FloorPlanDto[] = [];

  constructor(private apiClient: ApiClientService) { }
  
  ngOnInit(): void {
    this.apiClient.floorplansAll().subscribe(floorPlans => {
      this.floorPlans = floorPlans;
    });
  }
}
