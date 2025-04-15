import { Stance } from "./stance";

export interface Variation {
    id: string;
    name: string;
    landingStance: Stance;
}