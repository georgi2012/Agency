import { Component, OnInit } from '@angular/core';
import { JourneysService } from 'src/app/journeys.service';
import { ShowJourneyComponent } from '../show-journey/show-journey.component';
import { VehiclesService } from 'src/app/vehicles.service';

@Component({
  selector: 'app-add-edit-journey',
  templateUrl: './add-edit-journey.component.html',
  styleUrls: ['./add-edit-journey.component.css']
})
export class AddEditJourneyComponent implements OnInit {


  constructor(private service: JourneysService, private showJour:ShowJourneyComponent ,
    private vehService:VehiclesService){ }

  VehicleID:string;
  Destination:string;
  StartLocation:string;
  Distance:number;

  costsAreHidden:boolean = true;

  VehiclesList:any[];
 
   ngOnInit(): void {
     this.refreshVehiclesList();
   }
 
   refreshVehiclesList(){
     this.vehService.getVehicles().subscribe(data=>
      {
        this.VehiclesList = data;
      })
    }
 
    close(){
      this.showJour.closeClick();
     }
  

    reloadPage(){
     window.location.reload();
   }
 
   addJourney(isFromEnter:boolean){
    if(isFromEnter && this.costsAreHidden){
      return;
    }
     let newJourney={
      vehicleID: this.VehicleID,
      destination:this.Destination,
      startLocation:this.StartLocation,
      distance:this.Distance
     }
     
     this.service.addJourney(newJourney);
     this.refreshVehiclesList();
     this.reloadPage();
   }

   onVehiclePickChange(){
    this.costsAreHidden=false;
   }
 
}
