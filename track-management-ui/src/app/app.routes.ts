import { Routes } from '@angular/router';
import { TrackDetailComponent } from './components/track-detail/track-detail';
import { TrackList } from './components/track-list/track-list';

export const routes: Routes = [
  { path: '', component: TrackList },
  { path: 'tracks/:id', component: TrackDetailComponent },
];
