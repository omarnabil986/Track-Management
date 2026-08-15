import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Track } from '../../models/track.model';
import { TrackService } from '../../services/track.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-track-detail',
  imports: [CommonModule, RouterLink],
  templateUrl: './track-detail.html',
  styleUrl: './track-detail.css',
})
export class TrackDetailComponent implements OnInit {
  track?: Track;
  constructor(
    private route: ActivatedRoute,
    private trackService: TrackService,
  ) {}
  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.trackService.getTrackById(id).subscribe((t) => (this.track = t));
  }
}
