namespace Perceptron;
public class Neuron
{
    private Random _rng;
    private double _lr;
    private int _dimention;
    public delegate double ActivationFunctionDelegate(double x);
    private ActivationFunctionDelegate ActivationFunction { get; set; }
    public Neuron(int n, double learningRate)
    {
        _rng = new Random();
        _lr = learningRate;
        _dimention = n;
        Weights = new double[_dimention];
        ActivationFunction = new ActivationFunctionDelegate(Activation);
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] = _rng.NextDouble() * RandomSign;
        }
    }
    public double[] Weights { get; set; }
    
    public void UpdateActivationFunction(ActivationFunctionDelegate func)
    {
        ActivationFunction = func;
    }    

    private int RandomSign => (_rng.NextDouble() < 0.5) ? 1 : -1;
    
    public void Train(double[] inputs, double target)
    {
        if (inputs.Length != _dimention) throw new Exception("Inputs and weights must be the same length");
        double output = Guess(inputs);
        double error = target - output;
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] += _lr * error * inputs[i];
        }
    }

    public double Guess(double[] inputs)
    {
        double sum = 0;
        if (inputs.Length != _dimention) throw new Exception("Inputs and weights must be the same length");        
        for (int i = 0; i < Weights.Length; i++)
        {
            sum += Weights[i] * inputs[i];
        }
        return ActivationFunction(sum);
    }

    private double Activation(double sum)
    {
        return (sum > 0) ? 1 : -1;
    }
}
