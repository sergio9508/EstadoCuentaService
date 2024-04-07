"use client";
import { getCompras, getEstadoCuentaPDF } from "./data/data";

export default function Compras() {
  async function obtenerComprasXSLX() {
    const response = await getCompras("1234567891234");
    const base64String =
      "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," +
      response.item;
    const downloadLink = document.createElement("a");
    const fileName = "compras.xlsx";
    downloadLink.href = base64String;
    downloadLink.download = fileName;
    downloadLink.click();
  }

  return (
    <div>
      <button onClick={obtenerComprasXSLX}>Obtener Compras</button>
    </div>
  );
}
