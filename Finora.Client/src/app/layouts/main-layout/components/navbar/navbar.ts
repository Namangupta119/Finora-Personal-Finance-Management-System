import { Component, output } from '@angular/core';
import { MatIcon, MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-navbar',
  imports: [MatIconModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class NavbarComponent {

  readonly sidebarToggle = output<void>();

  toggleSidebar(): void{
    this.sidebarToggle.emit();
  }
}
