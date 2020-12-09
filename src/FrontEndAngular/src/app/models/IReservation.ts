export interface IReservation {

  id: string;
  description: string;
  dateOfChange: Date;
  ranking: number;
  favorited: boolean;
  clientId: string;
  clientName: string;
  ContactTypeId: string;
  ContactTypeName: string;
  ClientBirthDate: Date;
}
