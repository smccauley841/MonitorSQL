export interface Databases {
    id: number;
    name: string;
    collation: string;
    status: string;
    recovery: string;
    owner: string;
    instance: number;
    dataFileUsed?: number;
    logFileUsed?: number;
}
