using Perceptron;
var rng = new Random();
int xDim = 100;
var yDim = 100;
var repetitions = 10;
var nodes = 100;
var learningRate = .7;
var testSet = new List<Point>();
var learningSet = new List<Point>();
var truth = new List<double>();
var accuracyRate = new List<double>();
var results = new List<List<(double, bool)>>();
var brain = new Neuron(2, learningRate);


Run();


void Run()
{
    Console.WriteLine("Simple Perceptron!");
    Console.WriteLine(new String('-', Console.WindowWidth));
    Console.WriteLine($"Set Length: {nodes}\nLearning Rate: {learningRate}\nTraining Cycles: {repetitions}");
    for (int i = 0; i < nodes; i++) // initialize the test set and the learning set
    {
        testSet.Add(new(rng.Next(xDim), rng.Next(yDim)));
        learningSet.Add(new Point(rng.Next(xDim), rng.Next(yDim)));
    }

    foreach (var point in testSet) // Calculate the "truth" of the testing set
    {
        truth.Add(point.State);
    }

    for (int i = 0; i < repetitions; i++) // Train and guess n times
    {
        if (i != 0) Train(); // Do not train until a first guess has been made
        results.Add(Guess(testSet));
    }
    Console.WriteLine(new String('-', Console.WindowWidth));
    Console.WriteLine($"Accurracy rates: {(accuracyRate.Select(x => x.ToString()).Aggregate((x, y) => $"{x}%\t{y}"))}%");
    Console.WriteLine(new String('=', Console.WindowWidth));
}

void Train()
{
    foreach (var point in learningSet)
    {
        brain.Train(point, point.State);
    }
}

List<ValueTuple<double, bool>> Guess(List<Point> points)
{
    var list = new List<ValueTuple<double, bool>>();
    for (int i = 0; i < points.Count; i++)
    {
        var res = brain.Guess(points[i]);
        list.Add((res, truth[i] == res));
    }
    CalculateAccuracy(list);
    return list;
}

void CalculateAccuracy(List<ValueTuple<double, bool>> result) {
    accuracyRate.Add(result.Where(x => x.Item2 is true).Count() / (double)result.Count * 100);
}