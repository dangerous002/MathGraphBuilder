using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Text.RegularExpressions;
using AngouriMath;
using MathGraph.Views;


namespace MathGraph.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    //конструктор
    public MainWindowViewModel()
    {
        InitializePlotModel();
        
        //иницилизация значений
        this.FunctionText = "cos(2*x)-sin(x)";
        this.MinX = -1000;
        this.MaxX = 1000;
        this.StepX = 0.1;
    }

    public string FunctionText { get; set;  } //текст функции
    
    public PlotModel GraphModel { get; private set; } //модель графика
    public LineSeries Series1 { get; private set; } // функция на графике
    public List<DataPoint> Points { get; set; } = new List<DataPoint>(); // список точек для графика
    public double MinX { get; set; }  // нижняя граница
    public double MaxX { get; set; } // верхняя граница
    public double StepX { get; set; } // шаг функции
    public string FormatedExpression { get; set; } // мат выражение
    public Func<double, double>? compiledFunction { get; set; }
    

    //точки для иницализации графика
    private List<DataPoint> ExamplePoints()
    {
        return new List<DataPoint>()
        {
            new DataPoint(0, 0),
        };
    }

    //инициализация графика
    private void InitializePlotModel()
    {
        this.GraphModel = new PlotModel();
        GraphModel.Title = "Graph";
        GraphModel.Subtitle = "Subtitle";
        
        GraphModel.Axes.Add(new LinearAxis { 
            Position = AxisPosition.Bottom, 
            Title = "X", 
            Minimum = -100, 
            Maximum = 100, 
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            TicklineColor = OxyColors.White,
            AxislineStyle = LineStyle.Solid,
            AxislineColor = OxyColors.White,
        });
        GraphModel.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left, 
            Title = "Y", 
            Minimum = -100, 
            Maximum = 100,
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            TicklineColor = OxyColors.White,
            AxislineStyle = LineStyle.Solid,
            AxislineColor = OxyColors.White,
        });
        
        GraphModel.PlotMargins = new OxyThickness(60, 10, 30, 40);
        
        this.Points = ExamplePoints();

        Series1 = new LineSeries
        {
            Title = "Function graph",
            Color = OxyColors.White,
            ItemsSource = Points,
        };
        Series1.MarkerType = MarkerType.Cross;
        
        GraphModel.Series.Add(Series1);
        
        GraphModel.DefaultFont = "Arial";
        GraphModel.DefaultFontSize = 14;
        GraphModel.Background = OxyColor.FromRgb(25, 25, 26);
        GraphModel.TextColor = OxyColor.FromRgb(160, 161, 163);
        GraphModel.PlotAreaBorderColor = OxyColor.FromRgb(160, 161, 163);
        
        GraphModel.InvalidatePlot(true); 
    }

    // нажатие кнопки
    public void Button_Click()
    {
        BuildGraph();
    }

    //построение графика
    private void BuildGraph()
    {
        FormatedExpression = FormatMathExpression(FunctionText);
        
        CompileExpression();
        
        // обработка ошибки компиляции мат выражения
        if (compiledFunction == null)
        {
            Console.WriteLine("Error: Could not compile expression.");
            
            GraphModel.Title = FormatedExpression;
            GraphModel.Subtitle = $"Error: Could not compile expression.";
            GraphModel.Series.Clear();
            
            GraphModel.InvalidatePlot(true); 
            
            return;
        }
        
        //обновление текста
        GraphModel.Title = FormatedExpression;
        GraphModel.Subtitle = $"Min: {MinX}   Max: {MaxX}   Step: {StepX}";
        GraphModel.Series.Clear();
        
        //обновление точек графика
        Points = GetGraphValues();
        
        //добавление нового графика
        Series1 = new LineSeries()
        {
            Title = "Function graph",
            Color = OxyColors.White,
            ItemsSource = Points,
        };
        Series1.MarkerType = MarkerType.Cross;
        GraphModel.Series.Add(Series1);
        
        // обновление графика
        GraphModel.InvalidatePlot(true); 
    }
    
    // компиляция мат выражения
    private void CompileExpression()
    {
        try
        {
            Entity expr = MathS.FromString(FormatedExpression);
            compiledFunction = expr.Compile<double, double>("x");
        }
        catch (Exception ex)
        {
            compiledFunction = null;
            Console.WriteLine("Ошибка компиляции: " + ex.Message);
        }
    }

    // вычисление точек графика
    private List<DataPoint> GetGraphValues()
    {
        Entity expr = MathS.FromString(FormatedExpression); // Парсим выражение
        
        List<DataPoint> points = new List<DataPoint>();

        for (double CurrentX = MinX; CurrentX <= MaxX; CurrentX += StepX)
        {
            double ValueY = compiledFunction(CurrentX);
            points.Add(new DataPoint(CurrentX, ValueY));
        }
        
        return points;
    }

    //форматируем математическое выражение
    static string FormatMathExpression(string expression)
    {
        expression = Regex.Replace(expression, @"\s+", " ");  // Убираем лишние пробелы
        expression = Regex.Replace(expression, @"\s*([\+\-\*/\^\(\)])\s*", "$1"); // Убираем пробелы вокруг операторов
        return expression.ToLower(); // нижний регистр
    }

    // вызов окна со списком функций
    public void FunctionListButton_Click()
    {
        var functionsWindow = new FunctionsWindow();
        functionsWindow.Show();
    }
    
}