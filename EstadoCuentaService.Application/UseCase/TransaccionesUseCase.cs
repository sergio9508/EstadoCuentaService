using AutoMapper;
using EstadoCuentaService.Application.Features.Transacciones.Command;
using EstadoCuentaService.Application.Features.Transacciones.Query;
using EstadoCuentaService.Application.Interfaces.Transacciones.Command;
using EstadoCuentaService.Application.Interfaces.Transacciones.Query;
using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using IronXL;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cell = iText.Layout.Element.Cell;

namespace EstadoCuentaService.Application.UseCase
{
    public class TransaccionesUseCase : ITransaccionesUseCase
    {
        private readonly ITransaccionesQuery _transaccionesQuery;
        private readonly ITransaccionesCommand _transaccionesCommand;
        private readonly IMapper _mapper;
        public TransaccionesUseCase(ITransaccionesCommand transaccionesCommand, ITransaccionesQuery transaccionesQuery, IMapper mapper)
        {
            _transaccionesCommand = transaccionesCommand;
            _transaccionesQuery = transaccionesQuery;
            _mapper = mapper;
        }

        public async Task<GenericResponse> GuardarCompra(GuardarCompraCommand command)
        {
            var response = new GenericResponse();

            var compra = _mapper.Map<Compras>(command);

            var items = await _transaccionesCommand.GuardarCompra(compra);

            if (items)
            {
                response.code = 1;
                response.message = "Exito";
            }
            else
            {
                response.code = 0;
                response.message = "Ocurrio un error al procesar el pago";
            }

            return response;
        }

        public async Task<GenericResponse> GuardarPago(GuardarPagoCommand command)
        {
            var response = new GenericResponse();

            var pago = _mapper.Map<Pagos>(command);

            var items = await _transaccionesCommand.GuardarPago(pago);

            if (items)
            {
                response.code = 1;
                response.message = "Exito";
            }
            else
            {
                response.code = 0;
                response.message = "Ocurrio un error al procesar el pago";
            }

            return response;
        }

        public async Task<ListResponse<Transaccion>> ObtenerTransacciones(ObtenerTransaccionesQuery query)
        {
            var response = new ListResponse<Transaccion>();

            var items = await _transaccionesQuery.ObtenerTransacciones(query.NumeroTarjeta, query.Mes);

            if (items.Count > 0)
            {
                response.code = 1;
                response.message = "Exito";
                response.items = items;
            }
            else
            {
                response.code = 0;
                response.message = "No existen Transacciones";
            }

            return response;
        }

        public async Task<ObjectResponse<byte[]>> ObtenerEstadoCuenta(ObtenerEstadoCuentaQuery query)
        {
            var response = new ObjectResponse<byte[]>();
            var items = await _transaccionesQuery.ObtenerTransacciones(query.NumeroTarjeta, query.Mes);
            if (items.Count == 0)
            {
                response.code = 1;
                response.message = "Sin transacciones";
                return response;
            }

            using MemoryStream ms = new MemoryStream();
            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.LETTER);
            Document document = new Document(pdf);

            document.SetMargins(21.80806f, 71.80806f, 71.80806f, 71.80806f);

            Paragraph header = new Paragraph($"Estado de cuentas tarjeta numero {query.NumeroTarjeta} del mes {DateTimeFormatInfo.CurrentInfo.GetMonthName(query.Mes)}").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14);
            document.Add(header);

            var table = new Table(new float[] { 100f, 100f, 250f }, false);

            table.AddHeaderCell(new Cell().SetHeight(40).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(1))
                .Add(new Paragraph("\n").SetFontSize(10))
                .Add(new Paragraph("Tipo").SetFontSize(10)));
            table.AddHeaderCell(new Cell().SetHeight(40).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(1))
               .Add(new Paragraph("\n").SetFontSize(10))
               .Add(new Paragraph("Monto").SetFontSize(10)));
            table.AddHeaderCell(new Cell().SetHeight(40).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(1))
               .Add(new Paragraph("\n").SetFontSize(10))
               .Add(new Paragraph("Descripcion").SetFontSize(10)));

            foreach (var transaccion in items)
            {
                table.AddCell(new Cell().SetHeight(15).SetTextAlignment(TextAlignment.LEFT).SetBorder(new SolidBorder(1))
                .Add(new Paragraph(transaccion.Tipo == "C" ? "Compra" : "Pago").SetFontSize(10)));
                table.AddCell(new Cell().SetHeight(15).SetTextAlignment(TextAlignment.LEFT).SetBorder(new SolidBorder(1))
                   .Add(new Paragraph($"$ {transaccion.Monto.ToString("N2")}").SetFontSize(10)));
                table.AddCell(new Cell().SetHeight(15).SetTextAlignment(TextAlignment.LEFT).SetBorder(new SolidBorder(1))
                   .Add(new Paragraph(transaccion.Descripcion).SetFontSize(10)));
            }
            document.Add(table);
            document.Close();

            response.code = 1;
            response.message = "Exito";
            response.item = ms.ToArray();
            return response;

        }

        public async Task<ObjectResponse<byte[]>> ObtenerCompras(string numeroTarjeta)
        {
            var compras = await _transaccionesQuery.ObtenerCompras(numeroTarjeta);
            var respo = new ObjectResponse<byte[]>();
            if (compras.Count == 0)
            {
                respo.code = 1;
                respo.message = "No existen compras";
                return respo;
            }

            WorkBook workBook = WorkBook.Create(ExcelFileFormat.XLSX);

            var sheet = workBook.CreateWorkSheet("Hoja 1");
            
            sheet["A1"].Value = "Descripcion";
            sheet["B1"].Value = "Fecha";
            sheet["C1"].Value = "Monto";

            foreach (var compra in compras.Select((x, i) => new { value = x, index = i + 2 }))
            {
                sheet["A" + compra.index].Value = compra.value.Descripcion;
                sheet["B" + compra.index].Value = compra.value.Fecha.ToString();
                sheet["C" + compra.index].Value = compra.value.Monto;
            }
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);


            respo.code = 1;
            respo.message = "Exito";
            respo.item = workBook.ToByteArray();
            return respo;
        }
    }
}
