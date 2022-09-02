import { Component, OnInit,ElementRef ,ViewChild, ErrorHandler} from '@angular/core';
import { VehiclesService } from 'src/app/vehicles.service';
import { AddEditVehComponent } from '../add-edit-veh/add-edit-veh.component';
import {Vehicle} from '../interfaces';

@Component({
  selector: 'app-show-veh',
  templateUrl: './show-veh.component.html',
  styleUrls: ['./show-veh.component.css']
})
export class ShowVehComponent implements OnInit {

  constructor(private service: VehiclesService) { }

  VehiclesList:Vehicle[];
  hasErrors:boolean = false;
  errorText:string="";
  //veh:any;
  addWindowIsHidden=true;
  noElementsAvailable=false;
  isEditing=false;
  
  @ViewChild(AddEditVehComponent) addEditComp:AddEditVehComponent;

  ngOnInit(): void {//first method when in scope
   this.refreshVehList(); 
  }

  enterEditMode(editableVeh:Vehicle){
    this.isEditing=true;
    this.addWindowIsHidden=false;
    this.addEditComp.enterEditorMode(editableVeh);
  }

  addOpenClick(){
    if(!this.addWindowIsHidden && !this.isEditing){
      this.closeClick();
      return;
    }
    this.addWindowIsHidden=false;
    this.addEditComp.enterAddMode();
  }

  closeClick(){
    this.addWindowIsHidden=true;
  }

  deleteClick(id:number){
    this.service.deleteVehicle(id).subscribe();
      this.reloadPage();   
  }


  reloadPage(){
    window.location.reload();
  }

  async refreshVehList(){
    this.hasErrors=false;
   (await this.service.getVehicles()).subscribe(data=>
    {
      this.VehiclesList = data; 
    },(err)=>{
      this.hasErrors=true;
      this.errorText=err.statusText;
    });
  }

}
