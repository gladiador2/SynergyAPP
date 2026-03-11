import logger from '../logger.js';
import { procesarFlujoAsincronoSifen, procesarFlujoAsincronoSifenLote } from './sifenService.js';

export type SifenDispatchMode = 'recibe' | 'recibeLote';

export interface SifenJob {
    jsonId: number;
    xmlGeneradoId: number;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
    mode: SifenDispatchMode;
}

const pendingJobs: SifenJob[] = [];
let activeWorkers = 0;

function getMaxWorkers(): number {
    return Math.max(1, Number(process.env.SIFEN_ASYNC_WORKERS ?? 4));
}

function processNext(): void {
    const maxWorkers = getMaxWorkers();

    while (activeWorkers < maxWorkers && pendingJobs.length > 0) {
        const job = pendingJobs.shift();
        if (!job) {
            return;
        }

        activeWorkers += 1;

        const processor = job.mode === 'recibeLote' ? procesarFlujoAsincronoSifenLote : procesarFlujoAsincronoSifen;

        processor(job)
            .then(() => {
                logger.info(`[SIFEN-ASYNC] Proceso completado jsonId=${job.jsonId} modo=${job.mode}`);
            })
            .catch((error: unknown) => {
                const message = error instanceof Error ? error.message : String(error);
                logger.error(`[SIFEN-ASYNC] Fallo en proceso jsonId=${job.jsonId} modo=${job.mode}: ${message}`);
            })
            .finally(() => {
                activeWorkers -= 1;
                processNext();
            });
    }
}

export function enqueueSifenJob(job: SifenJob): { queueSize: number; activeWorkers: number } {
    pendingJobs.push(job);
    processNext();

    return {
        queueSize: pendingJobs.length,
        activeWorkers,
    };
}

export function getSifenQueueSnapshot(): { queueSize: number; activeWorkers: number; maxWorkers: number } {
    return {
        queueSize: pendingJobs.length,
        activeWorkers,
        maxWorkers: getMaxWorkers(),
    };
}
