import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';//responses

@Injectable({
  providedIn: 'root'
})

export class JourneysService {
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


  getJourneys():Observable<any[]>{
    return this.http.get<any[]>(this.ApiUrl+'Journey').pipe(
      catchError((err) => {
       // alert('Error connecting to server')
        console.error('Error connecting to server');
        return throwError(err);
      }));
  }

  getJourneyById(val: any):Observable<any[]>{
    return this.http.get<any>(this.ApiUrl+'Journey/'+val);
  }

  addJourney(journey: any){
    return this.http.post(this.ApiUrl+'Journey',journey,{observe: 'response'}).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }

  deleteJourney(id: any){
    return this.http.delete(this.ApiUrl+'Journey/'+id);
  }


}
