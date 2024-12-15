public class WeatherResponse
{
    public string LocalObservationDateTime { get; set; }
    public string WeatherText { get; set; }
    public Temperature Temperature { get; set; }
}

public class Temperature
{
    public Metric Metric { get; set; }
}

public class Metric
{
    public double Value { get; set; }
    public string Unit { get; set; }
    public int UnitType { get; set; }
}