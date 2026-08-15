export interface Track {
  id: number;
  title: string;
  isrc: string;
  releaseDate: string;
  genre: string;
  status: string;
  artistName: string;
  distributions?: TrackDistribution[];
}

export interface TrackDistribution {
  dspName: string;
  status: string;
}
