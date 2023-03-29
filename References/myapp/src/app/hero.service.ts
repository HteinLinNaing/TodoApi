import { Injectable } from '@angular/core';
import { Hero } from './hero';
import { catchError, Observable, of, tap } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  private heroesUrl = 'http://localhost:3600/api/Hero';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private messageService: MessageService,
    private http: HttpClient,
  ) { }

  getHeroes(): Observable<Hero[]> {
    return this.http.get<Hero[]>(this.heroesUrl)
      .pipe(
        tap(_ => this.log('fetched heroes')),
        catchError(this.handleError<Hero[]>('getHeroes', []))
      );
  }

  getHero(id: number): Observable<Hero> {
    //? For now, assume that a hero with the specified `id` always exists.
    //? Error handling will be added in the next step of the tutorial.

    // const hero = HEROES.find(h => h.Id === id)!;
    // this.messageService.add(`HeroService: fetched hero id=${id}`);
    // return of(hero);

    const url = `${this.heroesUrl}/${id}`;
    return this.http.get<Hero>(url)
      .pipe(
        tap(_ => this.log(`fetched hero id=${id}`)),
        catchError(this.handleError<Hero>(`get hero id=${id}`))
      );
  }

  // ? PUT: update the hero on the server
  updateHero(hero: Hero): Observable<any> {
    const url = `${this.heroesUrl}/${hero.Id}`;
    return this.http.put(url, hero, this.httpOptions).pipe(
      tap(_ => this.log(`updated hero id = ${hero.Id}`)),
      catchError(this.handleError<any>('updateHero'))
    );
  }

  /** POST: add a new hero to the server */
  addHero(hero: Hero): Observable<Hero> {
    return this.http.post<Hero>(this.heroesUrl, hero, this.httpOptions).pipe(
      tap((newHero: Hero) => this.log(`added hero w/ id=${newHero.Id}`)),
      catchError(this.handleError<Hero>('addHero'))
    );
  }

  /** DELETE: delete the hero from the server */
  deleteHero(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/${id}`;

    return this.http.delete<Hero>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted hero id=${id}`)),
      catchError(this.handleError<Hero>('deleteHero'))
    );
  }

  // ? Search
  search(term: string): Observable<Hero[]> {
    const url = `${this.heroesUrl}/search/${term}`;

    return this.http.post<Hero[]>(url, this.httpOptions).pipe(
      tap(_ => this.log(`Search hero`)),
      catchError(this.handleError<Hero[]>('SearchHero'))
    );
  }

  // TODO: For Message Services
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`HeroService: ${message}`);
  }
}

// Todo: service ကိုသုံးတိုင်း constructor ထဲမှာ Declare လုပ်ပေးရမယ်။
// Todo: Api call တွေက Observable ဖြစ်ရမယ်။

