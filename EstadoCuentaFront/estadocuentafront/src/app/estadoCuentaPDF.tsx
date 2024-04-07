"use client";
import { getEstadoCuentaPDF } from "./data/data";

export default function EstadoCuentaPDF() {
  async function obtenerEstadoPDF() {
    const response = await getEstadoCuentaPDF("1234567891234");
    const base64String = "data:application/pdf;base64," + response.item;
    const downloadLink = document.createElement("a");
    const fileName = "EstadoCuenta.pdf";
    downloadLink.href = base64String;
    downloadLink.download = fileName;
    downloadLink.click();
  }

  return (
    <div>
      <button onClick={obtenerEstadoPDF}>Obtener Estado de cuenta</button>
    </div>
  );
}
