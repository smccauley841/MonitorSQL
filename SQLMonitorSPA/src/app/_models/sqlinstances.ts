export interface Sqlinstances {
    id: number;
    name: string;
    physicalServerName: string;
    sqlversion: string;
    sqlcollation: string;
    numofdatabases: number;
    createdate: Date;
    isclustered: number;
    isalwayson: number;
    osname: string;
    sqledition: string;
}
