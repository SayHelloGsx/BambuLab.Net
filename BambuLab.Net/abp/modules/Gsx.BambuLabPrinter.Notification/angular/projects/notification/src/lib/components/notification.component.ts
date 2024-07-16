import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'lib-notification',
  template: ` <p>notification works!</p> `,
  styles: [],
})
export class NotificationComponent implements OnInit {
  constructor(private service: NotificationService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
