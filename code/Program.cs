using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
[Config(typeof(Config))]
public class DataBenchmarks
{
    private List<int> dataUnsorted;
    private List<int> dataSorted;
    private int[] arrayUnsorted;
    private int[] arraySorted;
    private const int seed = 1234;

    [Params(100_000)]
    public int DataSize;

    private int searchTarget;
    private class Config : ManualConfig
    {
        public Config()
        {
            AddJob(Job.Default
                .WithLaunchCount(1)
                .WithWarmupCount(3)
                .WithIterationCount(50)
                .WithMaxIterationCount(51)
                .WithMinIterationCount(49)
            );
            AddExporter(JsonExporter.Brief);
        }
    }

    [GlobalSetup]
    public void Setup()
    {
        CreateListUnsorted();
        CreateListSorted();

        GetListUnsorted();
        GetListSorted();

        searchTarget = dataUnsorted[DataSize / 2];
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public void CreateListUnsorted()
    {
        var rng = new Random(seed);
        dataUnsorted = Enumerable.Range(0, DataSize)
                                 .Select(_ => rng.Next())
                                 .ToList();
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public void CreateListSorted()
    {
        dataSorted = dataUnsorted.OrderBy(x => x).ToList();
    }
    public void GetListUnsorted()
    {
        arrayUnsorted = dataUnsorted.ToArray();
    }

    public void GetListSorted()
    {
        arraySorted = dataSorted.ToArray();
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public bool SearchLINQUnsorted()
    {
        return dataUnsorted.Contains(searchTarget);
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public bool SearchLINQSorted()
    {
        return dataSorted.Contains(searchTarget);
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public bool BinarySearchLINQ()
    {
        return Array.BinarySearch(arraySorted, searchTarget) >= 0;
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public bool BinarySearchStandard()
    {
        int left = 0;
        int right = arraySorted.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (arraySorted[mid] == searchTarget)
                return true;
            else if (arraySorted[mid] < searchTarget)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return false;
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public bool LinearSearchFor()
    {
        for (int i = 0; i < arrayUnsorted.Length; i++)
        {
            if (arrayUnsorted[i] == searchTarget)
                return true;
        }
        return false;
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public bool LinearSearchWhile()
    {
        int i = 0;
        while (i < arrayUnsorted.Length)
        {
            if (arrayUnsorted[i] == searchTarget)
                return true;
            i++;
        }
        return false;
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public List<int> SortLINQ()
    {
        return dataUnsorted.OrderBy(x => x).ToList();
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public int[] SortMergeSort()
    {
        var copy = arrayUnsorted.ToArray();
        MergeSort(copy, 0, copy.Length - 1);
        return copy;
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public int[] SortQuickSort()
    {
        var copy = arrayUnsorted.ToArray();
        QuickSort(copy, 0, copy.Length - 1);
        return copy;
    }

    private void MergeSort(int[] array, int left, int right)
    {
        if (left >= right) return;

        int mid = (left + right) / 2;
        MergeSort(array, left, mid);
        MergeSort(array, mid + 1, right);
        Merge(array, left, mid, right);
    }

    private void Merge(int[] array, int left, int mid, int right)
    {
        int[] temp = new int[right - left + 1];
        int i = left, j = mid + 1, k = 0;

        while (i <= mid && j <= right)
            temp[k++] = array[i] <= array[j] ? array[i++] : array[j++];

        while (i <= mid)
            temp[k++] = array[i++];
        while (j <= right)
            temp[k++] = array[j++];

        for (i = left, k = 0; i <= right; i++, k++)
            array[i] = temp[k];
    }


    private void QuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(array, low, high);
            QuickSort(array, low, pi - 1);
            QuickSort(array, pi + 1, high);
        }
    }

    private int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }
    public static void ExportData()
    {
        // Path to the JSON file under the Vagrant shared folder
        var jsonPath = "/vagrant/code/LINQ_Benchmarking/bin/Release/net8.0/BenchmarkDotNet.Artifacts/results/DataBenchmarks-report-brief.json";
        var outputPath = "/vagrant/benchmark_results_vertical.xlsx";

        if (!File.Exists(jsonPath))
        {
            Console.WriteLine("JSON file not found at: " + jsonPath);
            return;
        }

        var json = File.ReadAllText(jsonPath);
        var root = JObject.Parse(json);
        var benchmarks = root["Benchmarks"];

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("VerticalValues");

        int column = 1;

        foreach (var benchmark in benchmarks)
        {
            var name = benchmark["DisplayInfo"]?.ToString();
            var originalValues = benchmark["Statistics"]?["OriginalValues"] as JArray;

            if (name != null && originalValues != null)
            {
                worksheet.Cell(1, column).Value = name;

                for (int i = 0; i < originalValues.Count; i++)
                {
                    worksheet.Cell(i + 2, column).Value = (double)originalValues[i];
                }

                column++;
            }
        }

        workbook.SaveAs(outputPath);
        Console.WriteLine($"Excel file created: {outputPath}");
    }
}

    public class Program
{
    public static void Main(string[] args)
    {

        //BenchmarkRunner.Run<DataBenchmarks>();
        DataBenchmarks.ExportData();
    }
}