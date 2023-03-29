import { Component, OnInit } from '@angular/core';
import { Hero } from '../hero';
import { HeroService } from '../hero.service';
import { Todo } from '../todo';
import { TodoService } from '../todo.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  heroes: Hero[] = [];
  todos: Todo[] = [];
  herosearch: Hero[] = [];
  todosearch: Todo[] = [];

  constructor(private heroService: HeroService, private todoService: TodoService) { }

  ngOnInit(): void {
    this.getHeroes();
    this.getTodos();
  }

  getHeroes(): void {
    this.heroService.getHeroes()
      .subscribe(heroes => this.heroes = heroes.slice(1, 5));
  }

  getTodos(): void {
    this.todoService.getTodos()
      .subscribe(todos => this.todos = todos.slice(1, 5));
  }

  heroSearch(term: string): void {
    const searchTerm = term.trim();

    if (!searchTerm) { return; }
    this.heroService.search(searchTerm)
      .subscribe(heroes => this.herosearch = heroes);
  }

  todoSearch(term: string): void {
    const searchTerm = term.trim();

    if (!searchTerm) { return; }
    this.todoService.search(searchTerm)
      .subscribe(todos => this.todosearch = todos);
  }
}
