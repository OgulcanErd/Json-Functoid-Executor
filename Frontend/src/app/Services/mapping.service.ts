import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MappingService {
  private apiUrl = 'https://localhost:44338/api/mapping';

  constructor(private http: HttpClient) {}

  executeMapping(body: any): Observable<any> {
    // Tek functoid çalıştırma endpoint'i
    return this.http.post<any>(this.apiUrl, body);
  }

  executePipeline(body: any[]): Observable<any> {
    // Pipeline endpoint'i
    return this.http.post<any>(`${this.apiUrl}/pipeline`, body);
  }
}
