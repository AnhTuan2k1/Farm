
using System;
using System.Collections.Generic;

public class Inventory
{
    private Dictionary<string, int> seeds = new();
    private Dictionary<string, int> products = new();
    private Dictionary<string, int> totalHarvested = new();
    public Dictionary<string, int> Products { get => products;}

    public Inventory() { }
    public Inventory(Dictionary<string, int> seeds, Dictionary<string, int> products, Dictionary<string, int> totalHarvested)
    {
        this.seeds = seeds;
        this.products = products;
        this.totalHarvested = totalHarvested;
    }

    public void AddSeeds(string entityName, int amount)
    {
        if (!seeds.ContainsKey(entityName)) seeds[entityName] = 0;
        seeds[entityName] += amount;
    }

    public bool ConsumeSeed(string entityName)
    {
        if (seeds.ContainsKey(entityName) && seeds[entityName] > 0)
        {
            seeds[entityName]--;
            return true;
        }
        return false;
    }

    public void AddProduct(string entityName, int amount)
    {
        if (!products.ContainsKey(entityName)) products[entityName] = 0;
        products[entityName] += amount;

        if (!totalHarvested.ContainsKey(entityName)) totalHarvested[entityName] = 0;
        totalHarvested[entityName] += amount;
    }

    public void RemoveProduct(string entityName, int amount)
    {
        if (products.ContainsKey(entityName))
        {
            products[entityName] = Math.Max(products[entityName] - amount, 0);
        }
    }

    public int GetProductCount(string entityName) => products.TryGetValue(entityName, out var count) ? count : 0;
    public int GetTotalHarvested(string entityName) => totalHarvested.TryGetValue(entityName, out var count) ? count :0;
    public int GetSeedCount(string entityName) => seeds.TryGetValue(entityName, out var count) ? count : 0;
    public int GetTotalSeedCount()
    {
        int total = 0;
        foreach (var seed in seeds)
        {
            total += seed.Value;
        }
        return total;
    }
}
