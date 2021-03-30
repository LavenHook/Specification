using System;

namespace Ardalis.Specification.Supplement
{
  public abstract class TemporalFilter
  {
    internal abstract ITemporalSpecification? TemporalSpecification { get; }
  }

  internal class AsTemporalAllFilter : TemporalFilter
  {
    internal override ITemporalSpecification? TemporalSpecification => new AsTemporalAllSpecification();
  }

  internal class AsTemporalAsOfFilter : TemporalFilter
  {
    private AsTemporalAsOfSpecification? temporalSpecification { get; set; }

    public AsTemporalAsOfFilter(DateTime asOf)
    {
      temporalSpecification = new AsTemporalAsOfSpecification(asOf);
    }

    internal override ITemporalSpecification? TemporalSpecification => temporalSpecification;
  }

  internal class AsTemporalFromFilter : TemporalFilter
  {
    private AsTemporalFromSpecification? temporalSpecification { get; set; }

    public AsTemporalFromFilter(DateTime from, DateTime to)
    {
      temporalSpecification = new AsTemporalFromSpecification(from, to);
    }

    internal override ITemporalSpecification? TemporalSpecification => temporalSpecification;
  }

  internal class AsTemporalBetweenFilter : TemporalFilter
  {
    private AsTemporalBetweenSpecification? temporalSpecification { get; set; }

    public AsTemporalBetweenFilter(DateTime from, DateTime to)
    {
      temporalSpecification = new AsTemporalBetweenSpecification(from, to);
    }

    internal override ITemporalSpecification? TemporalSpecification => temporalSpecification;
  }

  internal class AsTemporalContainedFilter : TemporalFilter
  {
    private AsTemporalContainedSpecification? temporalSpecification { get; set; }

    public AsTemporalContainedFilter(DateTime from, DateTime to)
    {
      temporalSpecification = new AsTemporalContainedSpecification(from, to);
    }

    internal override ITemporalSpecification? TemporalSpecification => temporalSpecification;
  }

  public static class TemporalFilters
  {
    public static TemporalFilter AsTemporalAll() => new AsTemporalAllFilter();

    public static TemporalFilter AsTemporalAsOf(DateTime asOf) => new AsTemporalAsOfFilter(asOf);

    public static TemporalFilter AsTemporalFrom(DateTime from, DateTime to) => new AsTemporalFromFilter(from, to);

    public static TemporalFilter AsTemporalBetween(DateTime from, DateTime to) => new AsTemporalBetweenFilter(from, to);

    public static TemporalFilter AsTemporalContained(DateTime from, DateTime to) => new AsTemporalContainedFilter(from, to);
  }
}