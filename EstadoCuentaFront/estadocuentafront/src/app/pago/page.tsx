"use client";
import { useSearchParams } from "next/navigation";
import { ChangeEvent, FormEvent, useState } from "react";
import { guardarPago } from "../data/data";
import Link from "next/link";
import "./pago.scss";

export default function Pago() {
  const searchParams = useSearchParams();
  const numeroTarjeta = searchParams.get("numerotarjeta");
  const [hidden, setHidden] = useState(true);
  const [pago, setPago] = useState({
    monto: 0,
    numeroTarjeta: numeroTarjeta,
    fecha: "",
  });
  function handleChange(e: ChangeEvent<HTMLInputElement>) {
    console.log(e.target.value);
    setPago({ ...pago, [e.target.name]: e.target.value });
  }

  async function submit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const response = await guardarPago(pago);

    if (response.code === 1) {
      setHidden(false);
      setTimeout(() => {
        setHidden(true);
      }, 3000);
    }
  }

  return (
    <div className="pago">
      <h1>Registrar un pago</h1>
      <div style={{ visibility: hidden ? "hidden" : "visible" }}>
        <p>Pago guardado con exito</p>
      </div>
      <form onSubmit={submit}>
        <div className="form-control">
          <label htmlFor="monto">Monto: </label>
          <input
            type="number"
            name="monto"
            id="monto"
            onChange={handleChange}
            value={pago.monto}
          />
        </div>
        <div className="form-control">
          <label htmlFor="fecha">Fecha: </label>
          <input
            type="date"
            id="fecha"
            name="fecha"
            onChange={handleChange}
            value={pago.fecha}
          />
        </div>
        <div className="form-control">
          <button type="submit">Guardar</button>
          <Link href="/">Regresar</Link>
        </div>
      </form>
    </div>
  );
}
