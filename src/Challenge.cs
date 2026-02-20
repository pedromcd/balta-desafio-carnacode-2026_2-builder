using System;
using System.Collections.Generic;

namespace DesignPatternChallenge
{
    // ============================
    // 1) OBJETO FINAL (RELATÓRIO)
    // ============================
    // Agora o relatório é imutável: só pode ser criado via Builder (mais seguro).
    public class SalesReport
    {
        public string Title { get; }
        public string Format { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public bool IncludeHeader { get; }
        public bool IncludeFooter { get; }
        public string HeaderText { get; }
        public string FooterText { get; }

        public bool IncludeCharts { get; }
        public string ChartType { get; }

        public bool IncludeSummary { get; }
        public IReadOnlyList<string> Columns { get; }
        public IReadOnlyList<string> Filters { get; }

        public string SortBy { get; }
        public string GroupBy { get; }
        public bool IncludeTotals { get; }

        public string Orientation { get; }
        public string PageSize { get; }
        public bool IncludePageNumbers { get; }

        public string CompanyLogo { get; }
        public string WaterMark { get; }

        internal SalesReport(
            string title,
            string format,
            DateTime startDate,
            DateTime endDate,
            bool includeHeader,
            bool includeFooter,
            string headerText,
            string footerText,
            bool includeCharts,
            string chartType,
            bool includeSummary,
            List<string> columns,
            List<string> filters,
            string sortBy,
            string groupBy,
            bool includeTotals,
            string orientation,
            string pageSize,
            bool includePageNumbers,
            string companyLogo,
            string waterMark)
        {
            Title = title;
            Format = format;
            StartDate = startDate;
            EndDate = endDate;

            IncludeHeader = includeHeader;
            IncludeFooter = includeFooter;
            HeaderText = headerText;
            FooterText = footerText;

            IncludeCharts = includeCharts;
            ChartType = chartType;

            IncludeSummary = includeSummary;
            Columns = columns.AsReadOnly();
            Filters = filters.AsReadOnly();

            SortBy = sortBy;
            GroupBy = groupBy;
            IncludeTotals = includeTotals;

            Orientation = orientation;
            PageSize = pageSize;
            IncludePageNumbers = includePageNumbers;

            CompanyLogo = companyLogo;
            WaterMark = waterMark;
        }

        public void Generate()
        {
            Console.WriteLine($"\n=== Gerando Relatório: {Title} ===");
            Console.WriteLine($"Formato: {Format}");
            Console.WriteLine($"Período: {StartDate:dd/MM/yyyy} a {EndDate:dd/MM/yyyy}");

            if (IncludeHeader)
                Console.WriteLine($"Cabeçalho: {HeaderText}");

            if (!string.IsNullOrWhiteSpace(CompanyLogo))
                Console.WriteLine($"Logo: {CompanyLogo}");

            if (IncludeCharts)
                Console.WriteLine($"Gráfico: {ChartType}");

            Console.WriteLine($"Colunas: {string.Join(", ", Columns)}");

            if (Filters.Count > 0)
                Console.WriteLine($"Filtros: {string.Join(", ", Filters)}");

            if (!string.IsNullOrWhiteSpace(GroupBy))
                Console.WriteLine($"Agrupado por: {GroupBy}");

            if (!string.IsNullOrWhiteSpace(SortBy))
                Console.WriteLine($"Ordenado por: {SortBy}");

            if (IncludeTotals)
                Console.WriteLine("Totais: Sim");

            if (!string.IsNullOrWhiteSpace(PageSize))
                Console.WriteLine($"Página: {PageSize} / {Orientation}");

            if (IncludePageNumbers)
                Console.WriteLine("Paginação: Sim");

            if (!string.IsNullOrWhiteSpace(WaterMark))
                Console.WriteLine($"Marca d’água: {WaterMark}");

            if (IncludeFooter)
                Console.WriteLine($"Rodapé: {FooterText}");

            Console.WriteLine("Relatório gerado com sucesso!");
        }
    }

    // ============================
    // 2) BUILDER (FLUENT API)
    // ============================
    public class SalesReportBuilder
    {
        // obrigatórios
        private string _title;
        private string _format;
        private DateTime? _startDate;
        private DateTime? _endDate;

        // opcionais com defaults
        private bool _includeHeader = false;
        private bool _includeFooter = false;
        private string _headerText = "";
        private string _footerText = "";

        private bool _includeCharts = false;
        private string _chartType = "";

        private bool _includeSummary = false;
        private readonly List<string> _columns = new();
        private readonly List<string> _filters = new();

        private string _sortBy = "";
        private string _groupBy = "";
        private bool _includeTotals = false;

        private string _orientation = "Portrait";
        private string _pageSize = "A4";
        private bool _includePageNumbers = false;

        private string _companyLogo = "";
        private string _waterMark = "";

        // ----- Passos fluentes -----
        public SalesReportBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public SalesReportBuilder WithFormat(string format)
        {
            _format = format;
            return this;
        }

        public SalesReportBuilder WithPeriod(DateTime start, DateTime end)
        {
            _startDate = start;
            _endDate = end;
            return this;
        }

        public SalesReportBuilder IncludeHeader(string headerText)
        {
            _includeHeader = true;
            _headerText = headerText ?? "";
            return this;
        }

        public SalesReportBuilder IncludeFooter(string footerText)
        {
            _includeFooter = true;
            _footerText = footerText ?? "";
            return this;
        }

        public SalesReportBuilder IncludeCharts(string chartType)
        {
            _includeCharts = true;
            _chartType = chartType ?? "";
            return this;
        }

        public SalesReportBuilder IncludeSummary()
        {
            _includeSummary = true;
            return this;
        }

        public SalesReportBuilder AddColumn(string column)
        {
            if (!string.IsNullOrWhiteSpace(column))
                _columns.Add(column);
            return this;
        }

        public SalesReportBuilder AddColumns(IEnumerable<string> columns)
        {
            if (columns != null)
                foreach (var c in columns) AddColumn(c);
            return this;
        }

        public SalesReportBuilder AddFilter(string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
                _filters.Add(filter);
            return this;
        }

        public SalesReportBuilder AddFilters(IEnumerable<string> filters)
        {
            if (filters != null)
                foreach (var f in filters) AddFilter(f);
            return this;
        }

        public SalesReportBuilder SortBy(string sortBy)
        {
            _sortBy = sortBy ?? "";
            return this;
        }

        public SalesReportBuilder GroupBy(string groupBy)
        {
            _groupBy = groupBy ?? "";
            return this;
        }

        public SalesReportBuilder IncludeTotals()
        {
            _includeTotals = true;
            return this;
        }

        public SalesReportBuilder Page(string pageSize, string orientation = "Portrait")
        {
            _pageSize = pageSize ?? "A4";
            _orientation = orientation ?? "Portrait";
            return this;
        }

        public SalesReportBuilder IncludePageNumbers()
        {
            _includePageNumbers = true;
            return this;
        }

        public SalesReportBuilder WithCompanyLogo(string logoPath)
        {
            _companyLogo = logoPath ?? "";
            return this;
        }

        public SalesReportBuilder WithWaterMark(string waterMark)
        {
            _waterMark = waterMark ?? "";
            return this;
        }

        // ----- Presets (reuso de configuração comum) -----
        public SalesReportBuilder UseConfidentialPdfTemplate()
        {
            // Exemplo: um “pacote” de configs comuns
            return this
                .WithFormat("PDF")
                .IncludeHeader("Relatório de Vendas")
                .IncludeFooter("Confidencial")
                .WithWaterMark("Confidencial")
                .WithCompanyLogo("logo.png")
                .Page("A4", "Portrait")
                .IncludePageNumbers();
        }

        // ----- Build com validação -----
        public SalesReport Build()
        {
            // 1) Validar obrigatórios
            if (string.IsNullOrWhiteSpace(_title))
                throw new InvalidOperationException("Title é obrigatório (use WithTitle).");

            if (string.IsNullOrWhiteSpace(_format))
                throw new InvalidOperationException("Format é obrigatório (use WithFormat ou um template).");

            if (_startDate is null || _endDate is null)
                throw new InvalidOperationException("Período é obrigatório (use WithPeriod).");

            if (_startDate > _endDate)
                throw new InvalidOperationException("StartDate não pode ser maior que EndDate.");

            if (_columns.Count == 0)
                throw new InvalidOperationException("Informe ao menos 1 coluna (use AddColumn/AddColumns).");

            // 2) Validar coerência de opcionais
            if (_includeHeader && string.IsNullOrWhiteSpace(_headerText))
                throw new InvalidOperationException("HeaderText é obrigatório quando IncludeHeader é usado.");

            if (_includeFooter && string.IsNullOrWhiteSpace(_footerText))
                throw new InvalidOperationException("FooterText é obrigatório quando IncludeFooter é usado.");

            if (_includeCharts && string.IsNullOrWhiteSpace(_chartType))
                throw new InvalidOperationException("ChartType é obrigatório quando IncludeCharts é usado.");

            // 3) Criar objeto final
            return new SalesReport(
                _title,
                _format,
                _startDate.Value,
                _endDate.Value,
                _includeHeader,
                _includeFooter,
                _headerText,
                _footerText,
                _includeCharts,
                _chartType,
                _includeSummary,
                new List<string>(_columns),
                new List<string>(_filters),
                _sortBy,
                _groupBy,
                _includeTotals,
                _orientation,
                _pageSize,
                _includePageNumbers,
                _companyLogo,
                _waterMark
            );
        }
    }

    // ============================
    // 3) DEMO
    // ============================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Relatórios (Builder) ===");

            // Caso 1: Relatório complexo, mas legível
            var report1 = new SalesReportBuilder()
                .WithTitle("Vendas Mensais")
                .WithFormat("PDF")
                .WithPeriod(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31))
                .IncludeHeader("Relatório de Vendas")
                .IncludeFooter("Confidencial")
                .WithCompanyLogo("logo.png")
                .WithWaterMark("Confidencial")
                .AddColumns(new[] { "Produto", "Quantidade", "Valor" })
                .AddFilter("Status=Ativo")
                .GroupBy("Categoria")
                .SortBy("Valor")
                .IncludeCharts("Bar")
                .IncludeTotals()
                .Page("A4", "Portrait")
                .IncludePageNumbers()
                .Build();

            report1.Generate();

            // Caso 2: Evita setters soltos e garante obrigatórios
            var report2 = new SalesReportBuilder()
                .WithTitle("Relatório Trimestral")
                .WithFormat("Excel")
                .WithPeriod(new DateTime(2024, 1, 1), new DateTime(2024, 3, 31))
                .AddColumns(new[] { "Vendedor", "Região", "Total" })
                .IncludeCharts("Line")
                .GroupBy("Região")
                .IncludeTotals()
                .Build();

            report2.Generate();

            // Caso 3: Reuso de configurações comuns (Template/Preset)
            var report3 = new SalesReportBuilder()
                .WithTitle("Vendas Anuais")
                .UseConfidentialPdfTemplate()
                .WithPeriod(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31))
                .AddColumns(new[] { "Produto", "Quantidade", "Valor" })
                .IncludeCharts("Pie")
                .IncludeTotals()
                .Page("A4", "Landscape")
                .Build();

            report3.Generate();

            Console.ReadLine();
        }
    }
}