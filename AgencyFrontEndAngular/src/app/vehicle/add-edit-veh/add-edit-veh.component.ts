import { Component, OnInit ,Input,Output,EventEmitter, ViewChild, ElementRef} from '@angular/core';
import { VehiclesService } from 'src/app/vehicles.service';
import { ShowVehComponent } from '../show-veh/show-veh.component'; 
import { Injectable } from '@angular/core';
import { VehicleExport ,BoatExport,TrainExport,AirplaneExport,BusExport} from '../interfaces';
import { catchError, Observable, throwError } from 'rxjs';

@Component({
  selector: 'app-add-edit-veh',
  templateUrl: './add-edit-veh.component.html',
  styleUrls: ['./add-edit-veh.component.css']
})

@Injectable({
  providedIn: 'root' 
})

export class AddEditVehComponent implements OnInit {

  constructor(private service: VehiclesService, private showVeh:ShowVehComponent ){ }

 //@Input() veh:any;
 readonly AddvehicleText="Add new Vehicle";
 readonly EditVehicleText="Edit vehicle";
 readonly HasFoodText="Offers free food?";
 readonly HasWaterText="Offers water sports?";

 veh:VehicleExport={  
     PassangerCapacity: 0,
    PricePerKilometer: 0.0,
    VehicleModel:"",
    Carts: 0,
    HasFreeFood:false,
    HasWaterSports: false,
    ID: 0
  }

  capacityIsHidden:boolean = true;
  airplaneIsHidden:boolean = true;
  trainIsHidden:boolean = true;

  buttonText=this.AddvehicleText;
  checkBoxText=this.HasFoodText;
  @Input() isInEditMode=false;
  @Input() vehToEdit:any=null;
 
  VehiclesTypes:any[]=[];

  ngOnInit(): void {
    this.refreshTypesList();
  }

  refreshTypesList(){
    this.service.getTypesList().subscribe(data=>
     {
       this.VehiclesTypes = data;
     })
   }

   reloadPage(){
    window.location.reload();
  }

  close(){
    this.showVeh.closeClick();
  }

  enterAddMode(){
    this.isInEditMode=false;
    this.buttonText=this.AddvehicleText;
    this.veh.PricePerKilometer=0;
    this.veh.PassangerCapacity=0;
    this.veh.ID=0;
  }

  enterEditorMode(chosenVeh:any){
    this.veh.VehicleModel=chosenVeh.vehicleModel;
    this.veh.PricePerKilometer=chosenVeh.pricePerKilometer;
    this.veh.PassangerCapacity=chosenVeh.passangerCapacity;
    this.veh.ID=chosenVeh.vehicleID;

    this.isInEditMode=true;
    this.buttonText=this.EditVehicleText;
    this.onModelTypeChange();
  }

   onModelTypeChange(){ //vehiclemodel is the new value set
    this.capacityIsHidden=false;
      if(this.veh.VehicleModel == "Bus"){
        this.airplaneIsHidden=true;
        this.trainIsHidden=true;
      }
      else if(this.veh.VehicleModel=="Airplane"){
        this.airplaneIsHidden=false;
        this.trainIsHidden=true;
        this.checkBoxText=this.HasFoodText;
      }
      else if(this.veh.VehicleModel=="Boat"){
        this.airplaneIsHidden=false;
        this.trainIsHidden=true;
        this.checkBoxText=this.HasWaterText;
      }
      else if(this.veh.VehicleModel=="Train"){
        this.airplaneIsHidden=true;
        this.trainIsHidden=false;
      }
   }

   handleError(error:any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    alert(errorMessage);
    return throwError(() => {
        return errorMessage;
    });
  }

  makeCorrectVehicle() :any{
    switch(this.veh.VehicleModel)
    {
      case "Boat":{
        var  boat: BoatExport = {
          PassangerCapacity: this.veh.PassangerCapacity,
          PricePerKilometer: this.veh.PricePerKilometer,
          HasWaterSports: this.veh.HasWaterSports,
          ID: this.veh.ID
        }
        return boat;
      }
      case "Airplane":{
        var  plane: AirplaneExport = {
          PassangerCapacity: this.veh.PassangerCapacity,
          PricePerKilometer: this.veh.PricePerKilometer,
          HasFreeFood: this.veh.HasFreeFood,
          ID: this.veh.ID
        }
        return plane;
      }
      case "Bus":{
        var  bus: BusExport = {
          PassangerCapacity: this.veh.PassangerCapacity,
          PricePerKilometer: this.veh.PricePerKilometer,
          ID: this.veh.ID
        }
        return bus;
      }
      case "Train":{
        var  train: TrainExport = {
          PassangerCapacity: this.veh.PassangerCapacity,
          PricePerKilometer: this.veh.PricePerKilometer,
          Carts: this.veh.Carts,
          ID: this.veh.ID
        }
        return train;
      }
    }
  }

  editVehicle(){
    if(!this.isInEditMode)
    {
      let newVeh = this.makeCorrectVehicle();
      switch(this.veh.VehicleModel)
      {
        case "Boat":{
          this.service.addBoat(newVeh);
          break;
        }
        case "Airplane":{
          this.service.addAirplane(newVeh);
          break;
        }
        case "Bus":{
          this.service.addBus(newVeh);
          break;
        }
        case "Train":{
          this.service.addTrain(newVeh);
          break;
        }
      }
   
    }
    else
    {
      {
        let newVeh = this.makeCorrectVehicle();
        switch(this.veh.VehicleModel)
        {
          case "Boat":{
            this.service.putBoat(newVeh);
            break;
          }
          case "Airplane":{
            this.service.putAirplane(newVeh);
            break;
          }
          case "Bus":{
            this.service.putBus(newVeh);
            break;
          }
          case "Train":{
            this.service.putTrain(newVeh);
            break;
          }
        }
    }
  }
    this.buttonText=this.AddvehicleText;
    this.refreshTypesList();
    this.reloadPage();
    //this.showVeh.closeClick();

  }

  
 //update similar
}
