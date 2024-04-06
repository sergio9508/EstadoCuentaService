import EstadoCuenta from "./EstadoCuenta";
import InformacionTarjeta from "./InformacionTarjeta";
import { getInfoTarjeta } from "./data/data";
import "./globals.css";
export default async function Home() {
  return (
    <div className="container mx-auto">
      <InformacionTarjeta />
      <EstadoCuenta />
    </div>
  );
}
