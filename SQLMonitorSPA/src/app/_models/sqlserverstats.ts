import { Sqlinstances } from './sqlinstances';

export interface Sqlserverstats {
    instance: Sqlinstances;
    ram?: number;
    cpu?: number;
    diskIO?: number;
    timestamp?: Date;
}
