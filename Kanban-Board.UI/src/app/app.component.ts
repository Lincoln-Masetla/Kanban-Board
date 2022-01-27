import {Component, OnInit, ViewChild, AfterViewInit} from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from './dialog/dialog.component';
import { workItem } from './models/workItem';
import { ApiService } from './services/api.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { UserDialogComponent } from './user-dialog/user-dialog.component';
import { user } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Kanban-ui';
  workItems: workItem[] = [];
  displayColumns: string[] = [
    'title',
    'description',
    'status',
    'name',
    'action',
  ];
  dataSource!: MatTableDataSource<workItem>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  statuses!: any[];

  ngOnInit(): void {
    this.getAllWorkItems();
    this.getAllStatuses();
  }

  constructor(private dialog: MatDialog, private api: ApiService) {}

  openDialog() {
    const dialogRef = this.dialog.open(DialogComponent, { width: '60%' });
    dialogRef.afterClosed().subscribe((result) => {
      this.getAllWorkItems();
    });
  }

  openUserDialog() {
    const dialogRef = this.dialog.open(UserDialogComponent, { width: '60%' });
    dialogRef.afterClosed().subscribe((result) => {
      this.getAllWorkItems();
    });
  }

  getAllWorkItems() {
    this.api.getAll().subscribe((data: workItem[]) => {
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.workItems = data;
    });
  }

  editWorkItem(row: any) {
    this.dialog.open(DialogComponent, {
      width: '60%',
      data: row,
    });
  }

  filterWorkItems(event: any) {
    console.log(event.value);
    if (event.value == 'all') {
      this.getAllWorkItems();
    } else {
      this.getWorkItemsByStatus(event.value.id);
    }
  }

  getWorkItemsByStatus(status: number) {
    this.api.getByStatus(status).subscribe((data: workItem[]) => {
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.workItems = data;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getAllStatuses() {
    this.api.getStatusesAll().subscribe((data: user[]) => {
      this.statuses = data;
    });
  }
}
