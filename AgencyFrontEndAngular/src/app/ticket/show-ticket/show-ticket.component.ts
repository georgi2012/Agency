import { Component, OnInit } from '@angular/core';
import { TicketsService } from 'src/app/tickets.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-show-ticket',
  templateUrl: './show-ticket.component.html',
  styleUrls: ['./show-ticket.component.css']
})
export class ShowTicketComponent implements OnInit {

  constructor(private service: TicketsService) { }

  TicketsList:any[]=[];

  ticket:any;
  addWindowIsHidden=true;
  noElementsAvailable=false;

  ngOnInit(): void {//first method when in scope
   this.refreshTicketsList(); 
  }

  addOpenClick(){
    if(!this.addWindowIsHidden){
      this.closeClick();
      return;
    }
    this.addWindowIsHidden=false;
  }

  public closeClick(){
    this.addWindowIsHidden=true;
  }

  deleteClick(id:number){
    this.service.deleteTicket(id).subscribe(res=>{
      //alert(res.toString());
    });
    this.reloadPage();
  }

  reloadPage(){
    window.location.reload();
  }

  refreshTicketsList(){
   this.service.getTickets().subscribe(data=>
    {
      this.TicketsList = data;
    });
    this.noElementsAvailable = this.TicketsList.length == 0;
  }

}
