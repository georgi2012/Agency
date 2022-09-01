export interface Vehicle{ //that is received
    vehicleID: number,
    passangerCapacity: number,
    transportType: string,
    pricePerKilometer: number,
    vehicleModel: string,
    additionalInfo: string
  }

  export interface VehicleExport{
    PassangerCapacity: number,
    PricePerKilometer: number,
    VehicleModel: string,
    Carts: number,
    HasFreeFood: boolean,
    HasWaterSports:boolean,
    ID: number
  }
  
  export interface BusExport{
    PassangerCapacity: number,
    PricePerKilometer: number,
    ID: number
  }
  
  export interface TrainExport{
    PassangerCapacity: number,
    PricePerKilometer: number,
    Carts: number,
    ID: number
  }

  export interface AirplaneExport{
    PassangerCapacity: number,
    PricePerKilometer: number,
    HasFreeFood: boolean,
    ID: number
  }

  export interface BoatExport{
    PassangerCapacity: number,
    PricePerKilometer: number,
    HasWaterSports:boolean,
    ID: number
  }
