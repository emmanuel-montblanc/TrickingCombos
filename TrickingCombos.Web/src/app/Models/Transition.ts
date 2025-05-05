import { Stance } from "./Stance";

export interface Transition {
    id: string;
    name: string;
    stances: Stance[];
}