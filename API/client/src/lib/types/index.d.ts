type ApiResponse<T> = {
    data: T;
    message: string;
    statusCode: number;
  };

type Activity = {
    id: string
    description: string
    date: Date
    title: string
    isCancelled: boolean
    city: string
    venue: string
    latitude: number
    longitude: number
    categoryName: string
    categoryId: string
    // cityId: string
};

type BasicListObject = {
    id: string
    name : string
}

type LocationIQ = {
  place_id: string
  osm_id: string
  osm_type: string
  licence: string
  lat: string
  lon: string
  boundingbox: string[]
  class: string
  type: string
  display_name: string
  display_place: string
  display_address: string
  address: Address
}

type AddressIQ = {
  name: string
  road?: string
  suburb?: string
  town?: string
  village?: string
  city?: string
  county: string
  state: string
  postcode: string
  country: string
  country_code: string
  house_number?: string
  neighbourhood?: string
}

type Login = {
  username:string,
  password:string
}

export type Result<T> = {
 data: T,
 message: string,
 statusCode: number,
 exceptionDetails: string,
 isSuccess: boolean,
}