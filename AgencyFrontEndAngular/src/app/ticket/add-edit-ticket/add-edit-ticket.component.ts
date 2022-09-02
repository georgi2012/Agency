import { Component, OnInit } from '@angular/core';
import { TicketsService } from 'src/app/tickets.service';
import { ShowTicketComponent } from '../show-ticket/show-ticket.component';
import { JourneysService } from 'src/app/journeys.service';

@Component({
  selector: 'app-add-edit-ticket',
  templateUrl: './add-edit-ticket.component.html',
  styleUrls: ['./add-edit-ticket.component.css']
})
export class AddEditTicketComponent implements OnInit {

  constructor(private service: TicketsService, private showVeh:ShowTicketComponent ,
    private journeyService :JourneysService){ }

  AdministrativeCosts:number;
  JourneyID:string;

  costsAreHidden:boolean = true;

  JourneysList:any[];
 
   ngOnInit(): void {
     this.refreshJourneyList();
   }
 
   refreshJourneyList(){
     this.journeyService.getJourneys().subscribe(data=>
      {
        this.JourneysList = data;
      },(err)=>{
        close();
      });
    }
 
    reloadPage(){
     window.location.reload();
   }
 
   close(){
    this.showVeh.closeClick();
   }

   addTicket(){
     let newTicket={
      administrativeCosts: this.AdministrativeCosts,
      journeyID: this.JourneyID
     }
     
     this.service.addTicket(newTicket);
     this.refreshJourneyList();
     this.reloadPage();
   }

   onJourneyPickChange(){
    this.costsAreHidden=false;
   }
 
}
