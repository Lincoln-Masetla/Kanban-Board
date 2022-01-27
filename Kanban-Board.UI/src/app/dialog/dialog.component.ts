import { Component, Inject, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {  MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { user } from '../models/user';
import { workItem } from '../models/workItem';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements OnInit {
  workItemForm!: FormGroup;
  users!: user[];
  statuses!: user[];

  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    @Inject(MAT_DIALOG_DATA) public ediData: any,
    private dialogRef: MatDialogRef<DialogComponent>
  ) {}

  ngOnInit(): void {
    this.getAllUsers();
    this.getAllStatuses();
    this.workItemForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      statusId: ['', Validators.required],
      userId: ['', Validators.required],
    });

    if (this.ediData) {
      this.workItemForm.controls['title'].setValue(this.ediData.title);
      this.workItemForm.controls['description'].setValue(
        this.ediData.description
      );
      this.workItemForm.controls['statusId'].setValue(
        this.ediData.status.result.id
      );
      this.workItemForm.controls['userId'].setValue(
        this.ediData.user.result.id
      );
    }
  }

  actionWorkItem() {
    if (!this.workItemForm.valid) return;
    if (this.ediData) {
      this.updateWorkItem();
    } else {
      this.addWorkItem();
    }
  }

  updateWorkItem() {
    if (!this.workItemForm.valid) return;
    this.api.update(this.ediData.id, this.workItemForm.value).subscribe({
      next: () => {
        this.dialogRef.close(true);
        alert('WorkItem updated successfully');
        this.workItemForm.reset();
        window.location.reload();
      },
      error: () => {
        alert('Error while updated workItem');
      },
    });
  }

  addWorkItem() {
    if (!this.workItemForm.valid) return;
    this.api.create(this.workItemForm.value).subscribe({
      next: () => {
        alert('WorkItem added successfully');
        this.workItemForm.reset();
        this.dialogRef.close(true);
      },
      error: () => {
        alert('Error while adding workItem');
      },
    });
  }

  getAllUsers() {
    this.api.getUsersAll().subscribe((data: user[]) => {
      this.users = data;
      console.log(this.users);
    });
  }

  getAllStatuses() {
    this.api.getStatusesAll().subscribe((data: user[]) => {
      this.statuses = data;
      console.log(this.users);
    });
  }
}
