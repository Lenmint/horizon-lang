using HorizonCompiler.Evaluate.Values;

namespace HorizonCompiler.Evaluate;

public class Scope
{
    private readonly Scope? parent;

    private const int DATA_COUNT = 1;
    private readonly Dictionary<string, DataStore>[] data = new Dictionary<string, DataStore>[DATA_COUNT];

    public Scope(Scope? parent = null)
    {
        this.parent = parent;

        // Initialize dictionaries
        for (var i = 0; i < data.Length; i++)
        {
            data[i] = new Dictionary<string, DataStore>();
        }
    }

    private DataStore Find(int dictionary, string key)
    {
        return data[dictionary][key];
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

    public DataStore Search(int dictionary, string key)
    {
        return Evaluate(dictionary, key).Find(dictionary, key);
    }

    public void Add(int dictionary, string key, DataStore value)
    {
        if (Contains(dictionary, key))
            throw new ScopeException($"This key: '{key}' is already defined.");

        data[dictionary].Add(key, value);
    }

    public void Update(int dictionary, string key, DataStore value)
    {
        if (!Contains(dictionary, key))
            throw new ScopeException($"Couldn't find data in this context: [{dictionary}, {key}]");

        data[dictionary][key] = value;
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