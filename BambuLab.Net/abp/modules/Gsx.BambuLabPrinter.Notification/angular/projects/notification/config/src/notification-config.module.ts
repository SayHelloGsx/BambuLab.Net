import { ModuleWithProviders, NgModule } from '@angular/core';
import { NOTIFICATION_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class NotificationConfigModule {
  static forRoot(): ModuleWithProviders<NotificationConfigModule> {
    return {
      ngModule: NotificationConfigModule,
      providers: [NOTIFICATION_ROUTE_PROVIDERS],
    };
  }
}
