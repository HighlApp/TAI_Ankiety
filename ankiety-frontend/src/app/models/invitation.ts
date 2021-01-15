export class Invitation {
  constructor(
    public id: string,
    public userId: string,
    public surveyId: string,
    public sendDate: string,
    public startDate: string,
    public expirationDate: string,
    public questionsOnPage: number,
    public username: string,
    public surveyName: string,
    public expired: boolean,
    public filledSurvey: boolean,
  ) { }
}
