export interface ListResponse<T> {
    code: number;
    message: string;
    items: T[];
}

export interface ObjectResponse<T> {
    code: number;
    message: string;
    item: T
}

export interface GenericResponse {
    code: number;
    message: string;
}


export interface InformacionTarjeta {
    numeroTarjeta: string;
    nombre: string;
    saldoActual: number;
    limite: number;
    saldoDisponible: number;
    cuotaMinima: number;
    totalInteres: number;
}

export interface Transaccion {
    idTransaccion: number;
    numeroTarjeta: string;
    fecha: Date;
    descripcion: string;
    monto: number;
    tipo: string;
}

export interface Pago {
    numeroTarjeta?: string | null;
    fecha: string;
    monto: number;
}

export interface Compra {
    numeroTarjeta: string | null;
    fecha: string;
    descripcion: string;
    monto: number;
}