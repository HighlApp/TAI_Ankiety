import { SelectionModel } from "@angular/cdk/collections";
import { DatePipe } from "@angular/common";
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { InvitationService } from "../shared/invitation.service";

@Component({
  providers: [DatePipe],
  selector: "app-invitations",
  templateUrl: "./invitations.component.html",
  styleUrls: ["./invitations.component.css"]
})
export class InvitationsComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  selection = new SelectionModel<any>(true, []);
  dataSource: any;
  placeholder: string;
  displayedColumns = ["surveyName","sentTo", "sendDate", "startDate", "expirationDate", "filledSurvey"];
  pageSize = 5;  
  pageSizeOptions: number[] = [1, 5, 10, 20];  

  constructor(private invitationService: InvitationService,
    public datepipe: DatePipe) { }

  ngOnInit(): void {
    this.invitationService
      .getInvitations()
      .subscribe((res: any) => {
        console.log(res);
        res.data.forEach(element => {
          element.sendDate = this.datepipe.transform(element.sendDate, 'yyyy/MM/dd HH:mm');
          element.startDate = this.datepipe.transform(element.startDate, 'yyyy/MM/dd HH:mm')
          element.expirationDate = this.datepipe.transform(element.expirationDate, 'yyyy/MM/dd HH:mm')
        });
        this.dataSource = new MatTableDataSource(res.data);
        this.dataSource.paginator = this.paginator;
      });
  }

  filterData(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}

