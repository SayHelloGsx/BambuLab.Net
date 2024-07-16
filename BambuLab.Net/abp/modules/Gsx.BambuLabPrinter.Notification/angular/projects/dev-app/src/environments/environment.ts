import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Notification',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44329/',
    redirectUri: baseUrl,
    clientId: 'Notification_App',
    responseType: 'code',
    scope: 'offline_access Notification',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44329',
      rootNamespace: 'Gsx.BambuLabPrinter.Notification',
    },
    Notification: {
      url: 'https://localhost:44325',
      rootNamespace: 'Gsx.BambuLabPrinter.Notification',
    },
  },
} as Environment;
