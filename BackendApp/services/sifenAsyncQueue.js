import logger from '../logger.js';
import { procesarFlujoAsincronoSifen } from './sifenService.js';
const pendingJobs = [];
let activeWorkers = 0;
function getMaxWorkers() {
    return Math.max(1, Number(process.env.SIFEN_ASYNC_WORKERS ?? 4));
}
function processNext() {
    const maxWorkers = getMaxWorkers();
    while (activeWorkers < maxWorkers && pendingJobs.length > 0) {
        const job = pendingJobs.shift();
        if (!job) {
            return;
        }
        activeWorkers += 1;
        procesarFlujoAsincronoSifen(job)
            .then(() => {
            logger.info(`[SIFEN-ASYNC] Proceso completado jsonId=${job.jsonId}`);
        })
            .catch((error) => {
            const message = error instanceof Error ? error.message : String(error);
            logger.error(`[SIFEN-ASYNC] Fallo en proceso jsonId=${job.jsonId}: ${message}`);
        })
            .finally(() => {
            activeWorkers -= 1;
            processNext();
        });
    }
}
export function enqueueSifenJob(job) {
    pendingJobs.push(job);
    processNext();
    return {
        queueSize: pendingJobs.length,
        activeWorkers,
    };
}
export function getSifenQueueSnapshot() {
    return {
        queueSize: pendingJobs.length,
        activeWorkers,
        maxWorkers: getMaxWorkers(),
    };
}
//# sourceMappingURL=sifenAsyncQueue.js.map