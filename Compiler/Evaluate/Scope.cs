using HorizonCompiler.Evaluate.Values;

namespace HorizonCompiler.Evaluate;

public class Scope
{
    private readonly Scope? parent;

    private const int DATA_COUNT = 1;
    private readonly Dictionary<string, Value>[] data = new Dictionary<string, Value>[DATA_COUNT];

    public Scope(Scope? parent = null)
    {
        this.parent = parent;

        // Initialize dictionaries
        for (var i = 0; i < data.Length; i++)
        {
            data[i] = new Dictionary<string, Value>();
        }
    }

    private Value? Find(int dictionary, string key)
    {
        data[dictionary].TryGetValue(key, out var value);
        return value;
    }

    /// <summary>
    /// Get the correct scope that contains this context
    /// </summary>
    /// <param name="dictionary">Dictionary index</param>
    /// <param name="key">Data key</param>
    /// <returns>The scope thet contains the context</returns>
    /// <exception cref="ScopeException">If Client evaluated undified data</exception>
    private Scope Evaluate(int dictionary, string key)
    {
        var current = this;

        while (true)
        {
            if (current.Contains(dictionary, key))
                return current;

            if (current.parent != null)
            {
                current = current.parent;
                continue;
            }

            throw new ScopeException($"Couldn't find data in this context: [{dictionary}, '{key}']");
        }
    }

    public Value? Search(int dictionary, string key)
    {
        return Evaluate(dictionary, key).Find(dictionary, key);
    }

    public bool Add(int dictionary, string key, Value value)
    {
        return data[dictionary].TryAdd(key, value);
    }

    public bool Contains(int dictionary, string key)
    {
        return data[dictionary].ContainsKey(key);
    }

    public bool Delete(int dictionary, string key)
    {
        if (!Contains(dictionary, key)) return false;

        data[dictionary].Remove(key);
        return true;
    }
}