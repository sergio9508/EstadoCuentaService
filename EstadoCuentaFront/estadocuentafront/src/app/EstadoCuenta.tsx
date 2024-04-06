import { getEstadoCuenta } from "./data/data";
import moment from "moment";
import "./estadoCuenta.scss";
export default async function EstadoCuenta() {
  const estadoCuenta = await getEstadoCuenta("1234567891234");
  console.log(estadoCuenta);
  return (
    <div className="m-4">
      <div className="gap-x-8 gap-y-8 detalle">
        {estadoCuenta.items.map((estadoCuenta) => {
          return (
            <div className="estado-cuenta" key={estadoCuenta.idTransaccion}>
              <div className="icono">
                <span className="material-icons">
                  {estadoCuenta.tipo === "C" ? "shopping_basket" : "payments"}
                </span>
              </div>
              <div className="contenido">
                <h1>{estadoCuenta.tipo === "C" ? "Compra" : "Pago"}</h1>
                <p>
                  <span>Monto:</span> {estadoCuenta.monto}
                </p>
                <p>
                  <span>Descripción:</span> {estadoCuenta.descripcion}
                </p>
                <p>
                  <span>Fecha:</span>{" "}
                  {moment(estadoCuenta.fecha).format("DD-MM-YYYY")}
                </p>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
