import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//routing
import { VehicleComponent } from './vehicle/vehicle.component';
import { TicketComponent } from './ticket/ticket.component';
import { JourneyComponent } from './journey/journey.component';

const routes: Routes = [
{path:'vehicle' ,component:VehicleComponent},
{path:'ticket' ,component:TicketComponent},
{path:'journey',component:JourneyComponent}
//other paths
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
