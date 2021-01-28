import {APP_INITIALIZER, Inject, NgModule} from '@angular/core';
import {AuthModule, LogLevel, OidcConfigService} from 'angular-auth-oidc-client';
import {environment} from '../../environments/environment';

@NgModule({
  imports: [AuthModule.forRoot()],
  providers: [
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService],
      multi: true,
    },
  ],
  exports: [AuthModule],
})

export class AuthConfigModule {}

export function configureAuth(oidcConfigService: OidcConfigService): any {
  return () =>
    oidcConfigService.withConfig({
      stsServer: environment.isConnectionString,
      redirectUrl: `${environment.selfAddress}/admin/dashboard`,
      clientId: 'spaCodeClient',
      responseType: 'code',
      scope: 'openid profile resourceApi',
      postLogoutRedirectUri: environment.selfAddress,
      forbiddenRoute: '/forbidden',
      unauthorizedRoute: '/unauthorized',
      useRefreshToken: true,
      silentRenew: true,
      logLevel: LogLevel.Debug,
    },
      {
        issuer: environment.isConnectionString,
        jwksUri: environment.isConnectionString + '/.well-known/openid-configuration/jwks',
        authorizationEndpoint: environment.isConnectionString + '/connect/authorize',
        tokenEndpoint: environment.isConnectionString + '/connect/token',
        userinfoEndpoint: environment.isConnectionString + '/connect/userinfo',
        endSessionEndpoint: environment.isConnectionString + '/connect/endsession',
        checkSessionIframe: environment.isConnectionString + '/connect/checksession',
        revocationEndpoint: environment.isConnectionString + '/connect/revocation',
        introspectionEndpoint: environment.isConnectionString + '/connect/introspect',
      });
}
