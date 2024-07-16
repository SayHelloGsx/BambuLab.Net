import { TestBed } from '@angular/core/testing';
import { NotificationService } from './services/notification.service';
import { RestService } from '@abp/ng.core';

describe('NotificationService', () => {
  let service: NotificationService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(NotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
