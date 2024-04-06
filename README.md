
# Estado de cuentas

proyecto para gestionar estado de cuentas de una tarjeta de credito Bakend esta hecho con el patron CQRS y mediador usando .net 6 el front se ha realizado con el framework next.js el cual utiliza la libreria de react




## Instalacion

Correr el script script.sql en SQLServer para crear la base de datos utilizada

script contiene 2 datos configurables para el manejo de los intereses y el calculo de la cuota minima los cuales son: 
- interes
- porcentajeSaldoMinimo


    
## Ejecutar el proyecto localmente

Clone el proyecto 

```bash
  git clone https://github.com/sergio9508/EstadoCuentaService.git
```

Ir al directorio del proyecto 

```bash
  cd EstadoCuentaService
```
Para ejecutar la api dirigirse a la carpeta que contiente el proyecto del api es necesario tener .net instalado 

```bash
  cd EstadoCuentaService.WebApi
```

Iniciar el servidor

```bash
  dotnet run
```

Para poder iniciar el Front dirigirse a la carpeta que lo contiente 
```bash
  cd EstadoCuentaServiceFront
```
```bash
  cd estadocuentafront
```
Instalar las dependenciar
```bash
  npm install
```
 Iniciar el serividor localmente
 ```bash
  npm run dev
```