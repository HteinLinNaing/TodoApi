import { Component } from '@angular/core';
import { MessageService } from '../message.service';
import { Todo } from '../todo';
import { TodoService } from '../todo.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent {
  todos: Todo[] = [];

  constructor(private todoService: TodoService, private messageService: MessageService) { }

  ngOnInit(): void {
    this.getTodos();
  }

  getTodos(): void {
    this.todoService.getTodos()
      .subscribe(todos => this.todos = todos);
  }

  add(name: string): void {
    const TodoName = name.trim();

    if (!TodoName) { return; }
    this.todoService.addTodo({ Name: TodoName } as Todo)
      .subscribe(todo => {
        this.todos.push(todo);
      });
  }

  delete(todo: Todo): void {
    this.todos = this.todos.filter(t => t !== todo);
    this.todoService.delete(todo.Id).subscribe();
  }

  done(todo: Todo): void {
    this.todoService.done(todo).subscribe();
  }

  undo(todo: Todo): void {
    this.todoService.undo(todo).subscribe();
  }
}
