import { Component, signal } from '@angular/core';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar';
import { SidebarComponent } from './components/sidebar/sidebar';

@Component({
  selector: 'app-main-layout',
  imports: [
    MatSidenavModule,
    RouterOutlet,
    NavbarComponent,
    SidebarComponent
  ],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
})
export class MainLayoutComponent {

  readonly isSidebarOpen = signal(true);


  toggleSidebar() {
    this.isSidebarOpen.update(value => !value);
  }
}
