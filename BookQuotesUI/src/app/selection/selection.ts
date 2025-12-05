import { Component, Output, EventEmitter } from '@angular/core';
import { State } from '../../common';

@Component({
  selector: 'bq-selection',
  imports: [],
  templateUrl: './selection.html',
  styleUrl: './selection.css',
})

export class Selection {
  @Output() stateEvent = new EventEmitter<State>();

  public state = State.selector;
  public StateEnum = State;

  public SetState(state: State): void {
    this.state = state;
    this.shareState();
  }

  shareState() {
    this.stateEvent.emit(this.state);
  }
}
