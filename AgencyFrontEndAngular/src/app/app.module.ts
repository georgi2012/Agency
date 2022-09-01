import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { AddEditVehComponent } from './vehicle/add-edit-veh/add-edit-veh.component';
import { ShowVehComponent } from './vehicle/show-veh/show-veh.component';
import { VehiclesService } from './vehicles.service';
import { TicketsService } from './tickets.service';
import { JourneysService } from './journeys.service';


import {HttpClientModule} from '@angular/common/http'
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { TicketComponent } from './ticket/ticket.component';
import { AddEditTicketComponent } from './ticket/add-edit-ticket/add-edit-ticket.component';
import { ShowTicketComponent } from './ticket/show-ticket/show-ticket.component';
import { JourneyComponent } from './journey/journey.component';
import { ShowJourneyComponent } from './journey/show-journey/show-journey.component';
import { AddEditJourneyComponent } from './journey/add-edit-journey/add-edit-journey.component';


@NgModule({
  declarations: [
    AppComponent,
    VehicleComponent,
    AddEditVehComponent,
    ShowVehComponent,
    TicketComponent,
    AddEditTicketComponent,
    ShowTicketComponent,
    JourneyComponent,
    ShowJourneyComponent,
    AddEditJourneyComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    VehiclesService,
    TicketsService,
    JourneysService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
