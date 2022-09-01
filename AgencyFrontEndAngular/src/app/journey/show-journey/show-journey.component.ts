import { Component, OnInit } from '@angular/core';
import { JourneysService } from 'src/app/journeys.service';

@Component({
  selector: 'app-show-journey',
  templateUrl: './show-journey.component.html',
  styleUrls: ['./show-journey.component.css']
})
export class ShowJourneyComponent implements OnInit {
  constructor(private service: JourneysService) { }

  JourneysList:any[]=[];

  addWindowIsHidden=true;
  noElementsAvailable=false;


  ngOnInit(): void {//first method when in scope
   this.refreshJourneyList(); 
  }

  addOpenClick(){
    if(!this.addWindowIsHidden){
      this.closeClick();
      return;
    }
    this.addWindowIsHidden=false;
  }

  closeClick(){
    this.addWindowIsHidden=true;
  }

  deleteClick(id:number){
    this.service.deleteJourney(id).subscribe();
      this.reloadPage();   
  }


  reloadPage(){
    window.location.reload();
  }

  refreshJourneyList(){
   this.service.getJourneys().subscribe(data=>
    {
      this.JourneysList = data; 
    });
    this.noElementsAvailable = this.JourneysList.length == 0;
  }

}
