export class Invitation {
  constructor(
    public userId: string,
    public surveyId: string,
    public sendDate: string,
    public startDate: string,
    public expirationDate: string,
    public questionsOnPage: number,
    public groupId: any[]
  ) { }
}
