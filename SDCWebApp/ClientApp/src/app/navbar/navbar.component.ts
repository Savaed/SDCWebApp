import { Component, OnInit, DoCheck } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { VisitInfoService } from 'src/app/services/visit-info.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, DoCheck {
    private openingHour = new BehaviorSubject<string>(undefined);
    private closingHour = new BehaviorSubject<string>(undefined);
    private isUserLogged = new BehaviorSubject<boolean>(false);

    get isLogged(): BehaviorSubject<boolean> { return this.authService.isLogged; }

    constructor(private authService: AuthService, private infoService: VisitInfoService, private router: Router) { }

    ngOnInit() {
        this.infoService.getRecentInfo().subscribe(info => {
            const now = new Date();
            this.openingHour.next(info.openingHours.find(hour => hour.dayOfWeek.toString() === now.dayToString()).openingHour.toString());
            this.closingHour.next(info.openingHours.find(hour => hour.dayOfWeek.toString() === now.dayToString()).closingHour.toString());
        });
    }

    ngDoCheck() {
        this.authService.isLogged.subscribe(logged => this.isLogged.next(logged));
    }

    private logout() {
        this.authService.logout();
    }
}
