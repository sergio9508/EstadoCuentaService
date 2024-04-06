import Link from "next/link";
import { getInfoTarjeta } from "./data/data";
import "./informacionTarjeta.scss";
import path from "path";

export default async function InformacionTarjeta() {
  const infoTarjeta = await getInfoTarjeta("1234567891234");
  return (
    <div className="m-4">
      <div className="columns-2">
        <div className="informacion-tarjeta">
          <div className="icono">
            <span className="material-icons">person</span>
          </div>
          <div className="content">
            <p>
              <span>Nombre:</span> {infoTarjeta.item.nombre}
            </p>
            <p>
              <span>Saldo:</span> ${infoTarjeta.item.saldoActual}
            </p>
            <p>
              <span>Saldo Disponible:</span> ${infoTarjeta.item.saldoDisponible}
            </p>
            <p>
              <span>Total a Pagar:</span> ${infoTarjeta.item.totalInteres}
            </p>
            <p>
              <span>CuotaMinima: </span>${infoTarjeta.item.cuotaMinima}
            </p>
          </div>
        </div>
        <div className="botones">
          <Link
            href={{
              pathname: "/pago",
              query: { numerotarjeta: "1234567891234" },
            }}
            className="btn-pago"
          >
            Agregar Pago
          </Link>
          <Link
            href={{
              pathname: "/compra",
              query: { numerotarjeta: "1234567891234" },
            }}
            className="btn-compra"
          >
            Agregar Compra
          </Link>
        </div>
      </div>
    </div>
  );
}
