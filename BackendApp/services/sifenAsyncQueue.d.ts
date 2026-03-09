export interface SifenJob {
    jsonId: number;
    xmlGeneradoId: number;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
}
export declare function enqueueSifenJob(job: SifenJob): {
    queueSize: number;
    activeWorkers: number;
};
export declare function getSifenQueueSnapshot(): {
    queueSize: number;
    activeWorkers: number;
    maxWorkers: number;
};
//# sourceMappingURL=sifenAsyncQueue.d.ts.map