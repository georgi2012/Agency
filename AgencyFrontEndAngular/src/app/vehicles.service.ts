import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';//responses
import { BusExport, TrainExport, BoatExport, AirplaneExport } from './vehicle/interfaces';

@Injectable({
  providedIn: 'root'
})

export class VehiclesService {
  readonly ApiUrl = "https://localhost:7084/";


  constructor(private http: HttpClient) { }

  //#region Vehicle

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

  getVehicles():Observable<any[]> {
    return this.http.get<any[]>(this.ApiUrl + 'Vehicle').pipe(
      catchError((err) => {
        console.error('Error connecting to server for vehicle');
        return throwError(err);
      }));
  }

  getVehiclesByID(val: any): Observable<any[]>{
    return  this.http.get<any>(this.ApiUrl + 'Vehicle/' + val).pipe(
      catchError((err) => {
        console.error('Error connecting to server for vehicle');
        return throwError(err);
      }));
  }

  // addVehicle(val: any){
  //   return this.http.post(this.ApiUrl+val.VehicleModel,val).subscribe();//.pipe(catchError(err => {alert(err.error); return throwError(() => {
  //     //return err})}));
  //   // .pipe(
  //   //   catchError(err=>{
  //   //     alert(err.error);
  //   //     return throwError(err);
  //   //   })).subscribe(
  //   //     res => console.log('HTTP response', res),
  //   //     err => console.log('HTTP Error', err),
  //   //     () => console.log('HTTP request completed.')
  //   //   );
  // }


  async addTrain(veh: TrainExport) {
    return this.http.post(this.ApiUrl + "Train", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }

  async addBus(veh: BusExport) {
    return this.http.post(this.ApiUrl + "Bus", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }

  async addAirplane(veh: AirplaneExport) {
    return this.http.post(this.ApiUrl + "Airplane", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }
//   response => {  
    //     // let index = this.posts.indexOf(post);  
    //     // this.posts.splice(index, 1);  
    //   },  
    //   // When we're working with arrow functions we always need to put  
    //   // parameters in brackets if we more than 1 parameter or when we're using   
    //   // type annotation inside.   
    //   (error: Response) => {  
    //     if(error.status === 404)  
    //       alert('This Post Is Already Been Deleted');  
    //     else {  
    //       // We wanna display generic error message and log the error  
    //       alert('An alerted Unexpected Error Occured.');  
    //       alert(error.statusText + error.status);
    //       console.log(error);  
    //     }  
    // });


  async addBoat(veh: BoatExport) {
    return this.http.post(this.ApiUrl + "Boat", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }



  async putTrain(veh: TrainExport) {
    return this.http.put(this.ApiUrl + "Train", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );

  }

  putBus(veh: BusExport) {
    return this.http.put(this.ApiUrl + "Bus", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }

  putAirplane(veh: AirplaneExport) {
    return this.http.put(this.ApiUrl + "Airplane", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }

  putBoat(veh: BoatExport) {
    return this.http.put(this.ApiUrl + "Boat", veh).subscribe(
      ()=>{},(err:Response)=>{
        this.switchStatusCode(err.status);
      }
    );
  }


  deleteVehicle(id: any) {
    return this.http.delete(this.ApiUrl + 'Vehicle/' + id).pipe(
      catchError((err) => {
       // alert('Error connecting to server')
        console.error('Error connecting to server');
        return throwError(err);
      }));
  }

  getTypesList(): Observable<any[]> {
    return this.http.get<any[]>(this.ApiUrl + "Vehicle/Types").pipe(
      catchError((err) => {
       // alert('Error connecting to server')
        console.error('Error connecting to server');
        return throwError(err);
      }));
    }
  //#endregion
}
