using System.Drawing;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureShop.Backend.DataAccess;
using FurnitureShop.Backend.Report.Interfaces.Services;
using FurnitureShop.Backend.Report.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Grid;

namespace FurnitureShop.Backend.Report.Services;

public class PdfReportService : IReportService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    private readonly int _margin = 40;

    private readonly PdfStringFormat _centerAlignment = new(
        PdfTextAlignment.Center,
        PdfVerticalAlignment.Middle
    );
    
    private readonly PdfTrueTypeFont _titleFont = new (
        new Font("Times New Roman", 14f, FontStyle.Bold), 
        true
    );

    private readonly PdfTrueTypeFont _regularFont = new (
        new Font("Times New Roman", 12f, FontStyle.Regular), 
        true
    );

    private readonly PdfTrueTypeFont _italicFont = new (
        new Font("Times New Roman", 12f, FontStyle.Italic),
        true
    );

    private readonly PdfTrueTypeFont _boltFont = new (
        new Font("Times New Roman", 12f, FontStyle.Bold),
        true
    );

    public PdfReportService(DataContext dataContext, IMapper mapper, IConfiguration configuration)
    {
        _dataContext = dataContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<string> CreateReportFile(DateTime start, DateTime end)
    {
        var contracts = await _dataContext.Contracts
            .AsSingleQuery()
            .AsNoTracking()
            .Where(c => start <= c.IssueDate && c.IssueDate <= end)
            .ProjectTo<ContractReport>(_mapper.ConfigurationProvider)
            .ToListAsync();

        //Create a PdfDocument object
        PdfDocument doc = new PdfDocument();

        //Add a page 
        PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, new PdfMargins(_margin));

        InsertTitle(page, start, end, 15);
        var countColumns = 5;
        var table = CreateTable(contracts, (page.Size.Width - _margin * 2) / countColumns, countColumns);
        
        InsertNote(doc.Pages[0], 40);
        
        //Draw table on the page
        table.Draw(page, new PointF(0, 100));


        //Save the document to a PDF file
        var path = GenerateReportPath(start, end);
        doc.SaveToFile(path, FileFormat.PDF);

        return path;
    }

    private string GenerateReportPath(DateTime start, DateTime end)
    {
        var startString = start.ToString("(dd-MM-yyyy hh-mm-ss)");
        var endString = end.ToString("(dd-MM-yyyy hh-mm-ss)");
        return _configuration["ReportsPath"] + startString + "_" + endString + ".pdf";
    }

    private void InsertTitle(PdfPageBase page, DateTime start, DateTime end, float y)
    {
        //Create title
        var title = @$"Отчет о выполнении договоров на продажу мебели 
за период с {start:dd/MM/yyyy} по {end:dd/MM/yyyy}";
        page.Canvas.DrawString(
            title,
            _titleFont,
            new PdfSolidBrush(Color.Black), page.Canvas.ClientSize.Width / 2,
            y,
            _centerAlignment
        );
    }

    private void InsertNote(PdfPageBase page, float y)
    {
        RectangleF noteRect = new RectangleF(0, y, page.Canvas.ClientSize.Width, 60);
        PdfStringFormat leftAlignment = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Top);
        PdfStringFormat rightAlignment = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Top);

        page.Canvas.DrawRectangle(new PdfSolidBrush(Color.White), noteRect);
        page.Canvas.DrawLine(new PdfPen(Color.Black), 0, y, page.Canvas.ClientSize.Width, y);

        var note = @"Разработчики: студенты
группы 6313-020302D
Воропаев Алексей Игоревич
Чеплакова Елизавета Юрьевна";
        
        page.Canvas.DrawString("*Примечание:", _regularFont, new PdfSolidBrush(Color.Black), noteRect, leftAlignment);
        page.Canvas.DrawString(note, _regularFont, new PdfSolidBrush(Color.Black), noteRect, rightAlignment);

    }

    private PdfGrid CreateTable(ICollection<ContractReport> contractReports, float columnWidth, int countColumns)
    {
        //Create a PdfGrid
        PdfGrid grid = new PdfGrid
        {
            Style =
            {
                //Set cell padding
                CellPadding = new PdfPaddings(2, 2, 2, 2),
                //Set font
                Font = _regularFont
            }
        };
        
        grid.Columns.Add(countColumns);

        //Header
        var header = grid.Headers.Add(1);
        header[0].Cells[0].Value = "Название мебели";
        header[0].Cells[1].Value = "Модель";
        header[0].Cells[2].Value = "Количество, шт.";
        header[0].Cells[3].Value = "Цена модели, руб.";
        header[0].Cells[4].Value = "Стоимость модели, руб";

        foreach (var contract in contractReports)
        {
            var nameContract = grid.Rows.Add();
            nameContract.Cells[0].Value = $"Номер договора: {contract.ContractNumber}";
            nameContract.Cells[0].ColumnSpan = countColumns;
            nameContract.Cells[0].Style.Font = _italicFont;

            foreach (var sale in contract.Sales)
            {
                var saleRow = grid.Rows.Add();
                saleRow.Cells[0].Value = sale.FurnitureName;
                saleRow.Cells[1].Value = sale.FurnitureModel.ToString();
                saleRow.Cells[2].Value = sale.Count.ToString();
                saleRow.Cells[3].Value = sale.FurniturePrice.ToString();
                saleRow.Cells[4].Value = sale.TotalPrice.ToString();
            }

            var contractTotalPrice = grid.Rows.Add();
            contractTotalPrice.Cells[0].Value = 
                $"Итого по договору: {contract.Sales.Sum(s => s.Count)} шт., {contract.TotalPrice} руб.";
            contractTotalPrice.Cells[0].ColumnSpan = countColumns;
            contractTotalPrice.Cells[0].Style.Font = _italicFont;
        }

        var totalPrice = grid.Rows.Add();
        totalPrice.Cells[0].ColumnSpan = countColumns;
        totalPrice.Cells[0].Value = 
            $"Итого: {contractReports.SelectMany(c => c.Sales).Sum(s => s.Count)} шт., {contractReports.Sum(c => c.TotalPrice)} руб.";
        totalPrice.Cells[0].Style.Font = _boltFont;
        
        //Set column width
        foreach (PdfGridColumn col in grid.Columns)
        {
            col.Width = columnWidth;
        }

        return grid;
    }
}