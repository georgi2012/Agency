import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';//responses

@Injectable({
  providedIn: 'root'
})

export class TicketsService {
  readonly ApiUrl="https://localhost:7084/";


  constructor(private http: HttpClient) { }

  getTickets():Observable<any[]>{
    return this.http.get<any[]>(this.ApiUrl+'Ticket');
  }

  getTicketsById(val: any):Observable<any[]>{
    return this.http.get<any>(this.ApiUrl+'Ticket/'+val);
  }

  addTicket(ticket: any){
    return this.http.post(this.ApiUrl+'Ticket',ticket,{observe: 'response'}).subscribe(resp => { 
      //  if(resp.status <300){
      //   alert("Added successfully");
      //  }
         if(resp.status >= 400 && resp.status <500){
        alert("Client error");
       }
       else if (resp.status>=500){
        alert("Server error");
       }
    });
  }

  deleteTicket(id: any){
    return this.http.delete(this.ApiUrl+'Ticket/'+id);
  }

}
