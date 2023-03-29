import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { MessageService } from './message.service';
import { TODOS } from './mock.todo';
import { Todo } from './todo';
import { HttpClient, HttpHeaders } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  private todoUrl = "http://localhost:3600/api/TodoItems";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private messageService: MessageService,
    private http: HttpClient
  ) { }

  getTodos(): Observable<Todo[]> {
    // const todos = of(TODOS);
    // this.messageService.add('TodoService: fetched todos');
    // return todos;

    return this.http.get<Todo[]>(this.todoUrl).pipe(
      tap(_ => this.log('fetched todoItems')),
      catchError(this.handleError<Todo[]>('getTodoItems', []))
    );
  }

  getTodo(id: number): Observable<Todo> {
    // For now, assume that a hero with the specified `id` always exists.
    // Error handling will be added in the next step of the tutorial.
    const url = `${this.todoUrl}/${id}`;
    return this.http.get<Todo>(url).pipe(
      tap(_ => this.log(`fetched todoItems id:${id}`)),
      catchError(this.handleError<Todo>('getTodoItems'))
    );
  }

  updateTodo(todo: Todo): Observable<any> {
    const url = `${this.todoUrl}/${todo.Id}`;
    return this.http.put(url, todo, this.httpOptions).pipe(
      tap(_ => this.log(`updated todo id = ${todo.Id}`)),
      catchError(this.handleError<any>('updateTodo'))
    );
  }

  addTodo(todo: Todo): Observable<Todo> {
    return this.http.post<Todo>(this.todoUrl, todo, this.httpOptions).pipe(
      tap((newTodo: Todo) => this.log(`added todo w/ id=${newTodo.Id}`)),
      catchError(this.handleError<Todo>('addTodo'))
    );
  }

  delete(id: number): Observable<Todo> {
    const url = `${this.todoUrl}/${id}`;
    return this.http.delete<Todo>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted todoItems id:${id}`)),
      catchError(this.handleError<Todo>('deleteTodoItems'))
    );
  }

  done(todo: Todo): Observable<any> {
    const url = `${this.todoUrl}/${todo.Id}`;
    todo.IsComplete = true;
    return this.http.put(url, todo, this.httpOptions).pipe(
      tap(_ => this.log(`done todo id = ${todo.Id}`)),
      catchError(this.handleError<any>('doneTodo'))
    );
  }

  undo(todo: Todo): Observable<any> {
    const url = `${this.todoUrl}/${todo.Id}`;
    todo.IsComplete = false;
    return this.http.put(url, todo, this.httpOptions).pipe(
      tap(_ => this.log(`undo todo id = ${todo.Id}`)),
      catchError(this.handleError<any>('undoTodo'))
    );
  }

  // ? Search
  search(term: string): Observable<Todo[]> {
    const url = `${this.todoUrl}/search/${term}`;

    return this.http.post<Todo[]>(url, this.httpOptions).pipe(
      tap(_ => this.log(`Search todo`)),
      catchError(this.handleError<Todo[]>('SearchTodo'))
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
