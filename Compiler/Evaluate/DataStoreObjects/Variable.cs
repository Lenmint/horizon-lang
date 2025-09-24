using HorizonCompiler.Evaluate.Values;

namespace HorizonCompiler.Evaluate.DataStoreObjects;

public class Variable(Value? value, bool constant) : DataStore
{
    public Value? value = value;
    public bool constant = constant;
}