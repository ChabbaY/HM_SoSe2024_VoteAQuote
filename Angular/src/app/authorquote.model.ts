import { Author } from "./autoren/author.model";
import { Quote } from "./zitate/quote.model";

export interface AuthorQuote {
    author: Author;
    quotes: Quote[];
    color: string;
}