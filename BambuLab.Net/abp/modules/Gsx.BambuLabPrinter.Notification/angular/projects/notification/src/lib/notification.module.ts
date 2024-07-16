import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NotificationComponent } from './components/notification.component';
import { NotificationRoutingModule } from './notification-routing.module';

@NgModule({
  declarations: [NotificationComponent],
  imports: [CoreModule, ThemeSharedModule, NotificationRoutingModule],
  exports: [NotificationComponent],
})
export class NotificationModule {
  static forChild(): ModuleWithProviders<NotificationModule> {
    return {
      ngModule: NotificationModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<NotificationModule> {
    return new LazyModuleFactory(NotificationModule.forChild());
  }
}
