import { Injectable } from '@angular/core';
import { Component, OnInit } from '@angular/core';
//run the callback in the context of the Angular Zone (or NgZone)
import { NgZone } from '@angular/core';
import { AngularFire, AuthProviders, AuthMethods, FirebaseObjectObservable, FirebaseAuthState } from 'angularfire2';
import { Router } from '@angular/router';
import { moveIn, fallIn, moveInLeft } from '../router.animations';
import {Http, Headers} from '@angular/http';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css'],
  animations: [moveIn(), fallIn(), moveInLeft()],
  host: {'[@moveIn]': ''}  
})

@Injectable()
export class MembersComponent implements OnInit {

  //Show name when signed in
  name: any;
  state: string = '';
  hasPaid: FirebaseObjectObservable<any[]>;
  hasPaidPath: string = '';
  userID: any;
  email: any;

  constructor(public af: AngularFire, private router: Router, public http: Http, private _zone: NgZone/*run the callback in the context of the Angular Zone (or NgZone)*/) {

    this.af.auth.subscribe(auth => {

      //set name variable to username
      if(auth) {       
              
        this.name = auth;
        this.userID = auth.uid;
        this.email = auth.auth.email;

        //Check if a user has paid and redirect page as necessary
        this.hasPaidPath = 'users/' + this.userID;
        
        this.hasPaid = af.database.object(this.hasPaidPath);

        af.database.object(`${this.hasPaidPath}`).subscribe(snapshot => {
          
          console.log(snapshot.hasPaid);

          if (snapshot.hasPaid==true){
              this.router.navigateByUrl('/download');
          } else {
              console.log(this.name);                        
          }
        });        
      }
    });
} 

  //Activates the Stripe payment widget
  openCheckout() {
    var hasPaid = this.hasPaid;
    var userEmail = this.email;
    var http = this.http;
    var _zone = this._zone;

    //open the stripe widget
    var handler = (<any>window).StripeCheckout.configure({
      key: 'pk_live_Fg9bVFc7jtI6TU0fmEUriQuk',
      locale: 'auto',
      token: function (token: any) {

        //console.log ("pre zone: " + token);

        //run the callback in the context of the Angular Zone (or NgZone)
        _zone.run(() => {
        
            //send the user's email address to be processed by the backend
            var headers = new Headers();        
            headers.append('Content-Type', 'application/json');

            //Only works if cors is enabled in the browser.
            //http.post('http://localhost:3333/sendmail', {"userIdentifier":userEmail}, {headers: headers}).subscribe(result => console.log(result));
            http.post('/sendmail', {"userIdentifier":userEmail}, {headers: headers}).subscribe(result => console.log(result));
            http.post('/cc', {"stripeToken":token.id}, {headers: headers}).subscribe(result => console.log(result));
        
            console.log ("in zone: " + token.id);

            hasPaid.update({ hasPaid: true });
        });
      }
    });

    handler.open({
      name: 'Documents',
      description: 'Do-it-yourself documents',
      amount: 5000
    });

  }

    //Activates the Stripe payment widget
  openCheckoutPremium() {
    var hasPaid = this.hasPaid;
    var userEmail = this.email;
    var http = this.http;
    var _zone = this._zone;

    //open the stripe widget
    var handler = (<any>window).StripeCheckout.configure({
      key: 'pk_live_Fg9bVFc7jtI6TU0fmEUriQuk',
      locale: 'auto',
      token: function (token: any) {

        //console.log ("pre zone: " + token);

        //run the callback in the context of the Angular Zone (or NgZone)
        _zone.run(() => {
        
            //send the user's email address to be processed by the backend
            var headers = new Headers();        
            headers.append('Content-Type', 'application/json');

            //Only works if cors is enabled in the browser.
            //http.post('http://localhost:3333/sendmail', {"userIdentifier":userEmail}, {headers: headers}).subscribe(result => console.log(result));
            http.post('/sendmailpremium', {"userIdentifier":userEmail}, {headers: headers}).subscribe(result => console.log(result));
            http.post('/ccpremium', {"stripeToken":token.id}, {headers: headers}).subscribe(result => console.log(result));
        
            console.log ("in zone: " + token.id);

            hasPaid.update({ hasPaid: true });
        });
      }
    });

    handler.open({
      name: 'Documents and Training',
      description: 'Do-it-yourself documents & online training',
      amount: 50000
    });

  }

  //Logout and redirect to sign in page
  logout() {
     this.af.auth.logout();
     this.router.navigateByUrl('/login');
  }

  ngOnInit() {
  }

}