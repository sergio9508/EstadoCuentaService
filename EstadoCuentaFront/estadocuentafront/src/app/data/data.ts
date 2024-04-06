import { Compra, GenericResponse, InformacionTarjeta, ListResponse, ObjectResponse, Pago, Transaccion } from "./definitions";

export async function getInfoTarjeta(numeroTarjeta: string): Promise<ObjectResponse<InformacionTarjeta>> {
  const res = await fetch(`http://localhost:5095/api/v1/tarjeta/getinfotarjeta?numeroTarjeta=${numeroTarjeta}`, { cache: 'no-cache' });
  const json = await res.json();

  return json;
}


export async function getEstadoCuenta(numeroTarjeta: string): Promise<ListResponse<Transaccion>> {
  const mes = new Date().getMonth();
  const res = await fetch(`http://localhost:5095/api/v1/transacciones/gettransacciones?NumeroTarjeta=${numeroTarjeta}&Mes=${mes + 1}`, { cache: 'no-cache' });
  const json = await res.json();
  return json;
}


export async function guardarPago(pago: Pago): Promise<GenericResponse> {
  const body = JSON.stringify(pago);
  const res = await fetch(`http://localhost:5095/api/v1/transacciones/guardarpago`, { cache: 'no-cache', method: 'POST', body: body, headers: { 'Content-Type': 'application/json' } });
  const json = await res.json();
  return json;
}
export async function guardarCompra(compra: Compra): Promise<GenericResponse> {
  const body = JSON.stringify(compra);
  const res = await fetch(`http://localhost:5095/api/v1/transacciones/guardarcompra`, { cache: 'no-cache', method: 'POST', body: body, headers: { 'Content-Type': 'application/json' } });
  const json = await res.json();
  return json;
}