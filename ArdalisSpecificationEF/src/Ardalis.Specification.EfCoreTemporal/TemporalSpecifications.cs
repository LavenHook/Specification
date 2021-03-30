using System;

namespace Ardalis.Specification.Supplement
{
  internal interface ITemporalSpecification
  {
    ITemporalCriteria<TEntity> TemporalCriteria<TEntity>() where TEntity : class;
  }

  internal class AsTemporalAllSpecification : ITemporalSpecification
  {
    public ITemporalCriteria<TEntity> TemporalCriteria<TEntity>() where TEntity : class
    {
      return new AsTemporalAllCriteria<TEntity>();
    }
  }

  internal class AsTemporalAsOfSpecification : ITemporalSpecification
  {
    public DateTime AsOf { get; }

    public AsTemporalAsOfSpecification(DateTime asOf)
    {
      AsOf = asOf;
    }

    public ITemporalCriteria<TEntity> TemporalCriteria<TEntity>() where TEntity : class
    {
      return new AsTemporalAsOfCriteria<TEntity>(AsOf);
    }
  }

  internal class AsTemporalFromSpecification : ITemporalSpecification
  {
    public DateTime From { get; }
    public DateTime To { get; }

    public AsTemporalFromSpecification(DateTime from, DateTime to)
    {
      From = from;
      To = to;
    }

    public ITemporalCriteria<TEntity> TemporalCriteria<TEntity>() where TEntity : class
    {
      return new AsTemporalFromCriteria<TEntity>(From, To);
    }
  }

  internal class AsTemporalBetweenSpecification : ITemporalSpecification
  {
    public DateTime From { get; }
    public DateTime To { get; }

    public AsTemporalBetweenSpecification(DateTime from, DateTime to)
    {
      From = from;
      To = to;
    }

    public ITemporalCriteria<TEntity> TemporalCriteria<TEntity>() where TEntity : class
    {
      return new AsTemporalBetweenCriteria<TEntity>(From, To);
    }
  }

  internal class AsTemporalContainedSpecification : ITemporalSpecification
  {
    public DateTime From { get; }
    public DateTime To { get; }

    public AsTemporalContainedSpecification(DateTime from, DateTime to)
    {
      From = from;
      To = to;
    }

    public ITemporalCriteria<TEntity> TemporalCriteria<TEntity>() where TEntity : class
    {
      return new AsTemporalContainedCriteria<TEntity>(From, To);
    }
  }
}