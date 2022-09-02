import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';//responses

@Injectable({
  providedIn: 'root'
})

export class TicketsService {
  readonly ApiUrl="https://localhost:7084/";


  constructor(private http: HttpClient) { }

  switchStatusCode(code:number){
    if(code >=500){
      alert("Server error occured");
     }
    else if (code >=400){
      alert("Invalid data.");
    }
    else if(code == 204){
      alert("Content was not found in base");
    }
    else {
      alert("Unknown error occured");
    }
  }

  getTickets():Observable<any[]>{
    return this.http.get<any[]>(this.ApiUrl+'Ticket').pipe(
      catchError((err) => {
        console.error('Error connecting to server from ticket');
        return throwError(err);
      }));
  }

  getTicketsById(val: any):Observable<any[]>{
    return this.http.get<any>(this.ApiUrl+'Ticket/'+val);
  }

  addTicket(ticket: any){
    return this.http.post(this.ApiUrl+'Ticket',ticket,{observe: 'response'}).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }

  deleteTicket(id: any){
    return this.http.delete(this.ApiUrl+'Ticket/'+id);
  }

}
