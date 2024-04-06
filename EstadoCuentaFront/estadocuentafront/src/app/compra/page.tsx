"use client";
import { useSearchParams } from "next/navigation";
import { ChangeEvent, FormEvent, useState } from "react";
import { guardarCompra } from "../data/data";
import "./compra.scss";
import Link from "next/link";

export default function Compra() {
  const searchParams = useSearchParams();
  const numeroTarjeta = searchParams.get("numerotarjeta");
  const [hidden, setHidden] = useState(true);
  const [compra, setCompra] = useState({
    monto: 0,
    numeroTarjeta: numeroTarjeta,
    descripcion: "",
    fecha: "",
  });
  function handleChange(e: ChangeEvent<HTMLInputElement>) {
    console.log(e.target.value);
    setCompra({ ...compra, [e.target.name]: e.target.value });
  }

  async function submit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const response = await guardarCompra(compra);

    if (response.code === 1) {
      setHidden(false);
      setTimeout(() => {
        setHidden(true);
      }, 3000);
    }
  }

  return (
    <div className="compra">
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
            value={compra.monto}
          />
        </div>
        <div className="form-control">
          <label htmlFor="fecha">Fecha: </label>
          <input
            type="date"
            id="fecha"
            name="fecha"
            onChange={handleChange}
            value={compra.fecha}
          />
        </div>
        <div className="form-control">
          <label htmlFor="fecha">Descripcion: </label>
          <input
            type="text"
            id="descripcion"
            name="descripcion"
            onChange={handleChange}
            value={compra.descripcion}
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
