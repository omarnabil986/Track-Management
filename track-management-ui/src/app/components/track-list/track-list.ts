import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Track } from '../../models/track.model';
import { TrackService } from '../../services/track.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-track-list',
  imports: [FormsModule, CommonModule],
  templateUrl: './track-list.html',
  styleUrl: './track-list.css',
})
export class TrackList implements OnInit {
  tracks: Track[] = [];
  statusFilter = '';

  constructor(
    private trackService: TrackService,
    private router: Router,
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.trackService
      .getTracks(this.statusFilter || undefined)
      .subscribe((data) => (this.tracks = data));
  }

  onFilterChange() {
    this.load();
  }

  viewTrack(id: number) {
    this.router.navigate(['/tracks', id]);
  }
}
