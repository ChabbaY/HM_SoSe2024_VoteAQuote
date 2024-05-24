import { Author } from "./autoren/author.model";
import { Quote } from "./zitate/quote.model";

export interface VotedQuote {
    author: Author;
    quote: Quote;
    vote: number;
    color: string;
    uservote: number;
}