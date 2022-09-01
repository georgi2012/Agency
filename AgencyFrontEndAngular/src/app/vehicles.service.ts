import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';//responses
import { BusExport, TrainExport ,BoatExport,AirplaneExport } from './vehicle/interfaces';

@Injectable({
  providedIn: 'root'
})

export class VehiclesService {
  readonly ApiUrl="https://localhost:7084/";


  constructor(private http: HttpClient) { }

  //#region Vehicle

  getVehicles():Observable<any[]>{
    return this.http.get<any[]>(this.ApiUrl+'Vehicle');
  }

  getVehiclesByID(val: any):Observable<any[]>{
    return this.http.get<any>(this.ApiUrl+'Vehicle/'+val);
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
 
  
  addTrain(veh:TrainExport){
    return this.http.post(this.ApiUrl+"Train",veh).subscribe();
  }

  addBus(veh:BusExport){
    return this.http.post(this.ApiUrl+"Bus",veh).subscribe();
  }

  addAirplane(veh:AirplaneExport){
    return this.http.post(this.ApiUrl+"Airplane",veh).subscribe();
  }

  addBoat(veh:BoatExport){
    return this.http.post(this.ApiUrl+"Boat",veh).subscribe();
  }

  putTrain(veh:TrainExport){
    return this.http.put(this.ApiUrl+"Train",veh).subscribe();
  }

  putBus(veh:BusExport){
    return this.http.put(this.ApiUrl+"Bus",veh).subscribe();
  }

  putAirplane(veh:AirplaneExport){
    return this.http.put(this.ApiUrl+"Airplane",veh).subscribe();
  }

  putBoat(veh:BoatExport){
    return this.http.put(this.ApiUrl+"Boat",veh).subscribe();
  }


  deleteVehicle(id: any){
    return this.http.delete(this.ApiUrl+'Vehicle/'+id);
  }

  getTypesList():Observable<any[]>{
    return this.http.get<any[]>(this.ApiUrl+"Vehicle/Types");
  }

  //#endregion

}
