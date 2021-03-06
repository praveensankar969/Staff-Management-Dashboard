import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class HttpInterceptService implements HttpInterceptor {

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    const token = localStorage.getItem("TOKEN");
    if (token != null) {
      req = req.clone({
        url: req.url,
        setHeaders: {
          Authorization: 'Bearer ' + token
        }
      })
      return next.handle(req);
    }
    else{
      return next.handle(req);
    }

  }
}
