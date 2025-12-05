export enum State {
    selector,
    authors,
    books, 
    quotes,
}

export class Author {
    public firstName: string | undefined;
    public lastName: string | undefined;
}

export class Book {
    public title: string | undefined;
    public creator: Author | undefined;
}

export class Quote {
    public text: string | undefined;
    public origin: Book | undefined;
}