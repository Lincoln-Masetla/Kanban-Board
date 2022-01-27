import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { user } from '../models/user';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.scss'],
})
export class UserDialogComponent implements OnInit {
  userForm!: FormGroup;
  users!: user[];

  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    @Inject(MAT_DIALOG_DATA) public ediData: user,
    private dialogRef: MatDialogRef<UserDialogComponent>
  ) {}

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
    });

    if (this.ediData) {
      this.userForm.controls['name'].setValue(this.ediData.name);
      this.userForm.controls['email'].setValue(this.ediData.email);
    }
  }

  actionUser() {
    if (!this.userForm.valid) return;
    if (this.ediData) {
      this.updateUser();
    } else {
      this.addUser();
    }
  }
  updateUser() {
    if (!this.userForm.valid) return;
    this.api.updateUser(this.ediData.id, this.userForm.value).subscribe({
      next: () => {
        this.dialogRef.close(true);
        alert('User updated successfully');
        this.userForm.reset();
        window.location.reload();
      },
      error: () => {
        alert('Error while updated user');
      },
    });
  }

  addUser() {
    if (!this.userForm.valid) return;
    this.api.createUser(this.userForm.value).subscribe({
      next: () => {
        alert('User added successfully');
        this.userForm.reset();
        this.dialogRef.close(true);
      },
      error: () => {
        alert('Error while adding user');
      },
    });
  }
}
