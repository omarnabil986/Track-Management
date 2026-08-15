import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Track } from "../models/track.model";

@Injectable({ providedIn: "root" })
export class TrackService {
  private baseUrl = "https://localhost:7053/api/tracks";

  constructor(private http: HttpClient) {}

  getTracks(
    status?: string,
    artistId?: number,
    genre?: string,
  ): Observable<Track[]> {
    let params = new HttpParams();
    if (status) params = params.set("status", status);
    if (artistId) params = params.set("artistId", artistId);
    if (genre) params = params.set("genre", genre);
    return this.http.get<Track[]>(this.baseUrl, { params });
  }

  getTrackById(id: number): Observable<Track> {
    return this.http.get<Track>(`${this.baseUrl}/${id}`);
  }
}
