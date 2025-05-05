import { Stance } from "./Stance";
import { Transition } from "./Transition";
import { Variation } from "./Variation";

export interface Trick {
    id: string;
    name: string;
    defaultLandingStance: Stance;
    transitions: Transition[];
    variations: Variation[];
}