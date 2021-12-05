// Forgot to save the first solutin but this should be easy to convert

string[] input = File.ReadAllLines("Input.txt");

List<((int x, int y) startPosition, (int x, int y) endPosition)> lines = new();

// Get all start and end positions of lines
foreach (var line in input)
{
    var StartEnd = line.Split(" -> ", StringSplitOptions.None);
    for (int i = 0; i < StartEnd.Length; i = i + 2)
    {
        string? value = StartEnd[i];
        var pos1 = StartEnd[0].Split(',');
        var pos2 = StartEnd[1].Split(',');
        int startX = int.Parse(pos1[0]);
        int startY = int.Parse(pos1[1]);
        int endX = int.Parse(pos2[0]);
        int endY = int.Parse(pos2[1]);
        var startPosition = (startX: startX, startY: startY);
        var endPosition = (endX: endX, endY: endY);
        var StartEndPoints = (StartPosition: startPosition, EndPosition: endPosition);
        lines.Add(StartEndPoints);
    }
}

// Create the map

int maximumStartingXValue = lines.Max(x => x.startPosition.x);
int maximumStartingYValue = lines.Max(y => y.startPosition.y);
int maximumEndingXValue = lines.Max(x => x.endPosition.x);
var maximumEndingYValue = lines.Max(x => x.endPosition.y);
var maxXValue = Math.Max(maximumStartingXValue, maximumEndingXValue);
int maxYValue = Math.Max(maximumStartingYValue, maximumEndingYValue);
int[,] map = new int[maxXValue+1, maxYValue+1];

foreach (var line in lines)
{
    plotLine(line.startPosition.x, line.startPosition.y, line.endPosition.x, line.endPosition.y);
}

int intersectionCounter = 0;

for (int y = 0; y <= maxYValue; y++)
{
    for (int x = 0; x <= maxXValue; x++)
    {       
        if (map[x, y] > 1)
        {
            intersectionCounter++;
        }
    }  
}

Console.WriteLine(intersectionCounter);

void plotLine(int x0, int y0, int x1, int y1)
{
    if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
    {
        if (x0 > x1)
        {
            plotLineLow(x1, y1, x0, y0);
        }
        else
        {
            plotLineLow(x0, y0, x1, y1);
        }
    }
    else
    {
        if (y0 > y1)
        {
            plotLineHigh(x1, y1, x0, y0);
        }
        else
        {
            plotLineHigh(x0, y0, x1, y1);
        }
    }
}

void plotLineLow(int x0, int y0, int x1, int y1)
{
    int dx = x1 - x0;
    int dy = y1 - y0;
    int yi = 1;
    if (dy < 0)
    {
        yi = -1;
        dy = -dy;
    }
    int D = (2 * dy) - dx;
    int y = y0;

    for (int x = x0; x <= x1; x++)
    {
        map[x, y]++;
        if (D > 0)
        {
            y = y + yi;
            D = D + (2 * (dy - dx));
        }
        else
        {
            D = D + 2 * dy;
        }
    }
}

void plotLineHigh(int x0, int y0, int x1, int y1)
{
    int dx = x1 - x0;
    int dy = y1 - y0;
    int xi = 1;
    if (dx < 0)
    {
        xi = -1;
        dx = -dx;
    }
    int D = (2 * dx) - dy;
    int x = x0;
    for (int y = y0; y <= y1; y++)
    {
        map[x, y]++;
        if (D > 0)
        {
            x = x + xi;
            D = D + (2 * (dx - dy));
        }
        else
        {
            D = D + 2 * dx;
        }
    }
}