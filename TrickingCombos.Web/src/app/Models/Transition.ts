import { Stance } from "./stance";

export interface Transition {
    id: string;
    name: string;
    stances: Stance[];
}