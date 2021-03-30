using EfCoreTemporalTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ardalis.Specification.Supplement
{
  internal interface ITemporalCriteria<TEntity> where TEntity : class
  {
    internal Func<DbSet<TEntity>, IQueryable<TEntity>> TemporalEvaluator { get; }
  }

  public class AsTemporalAllCriteria<TEntity> : ITemporalCriteria<TEntity> where TEntity : class
  {
    Func<DbSet<TEntity>, IQueryable<TEntity>> ITemporalCriteria<TEntity>.TemporalEvaluator => dbset => dbset.AsTemporalAll();
  }

  public class AsTemporalAsOfCriteria<TEntity> : ITemporalCriteria<TEntity> where TEntity : class
  {
    private DateTime AsOf { get; set; }

    public AsTemporalAsOfCriteria(DateTime asOf)
    {
      AsOf = asOf;
    }

    Func<DbSet<TEntity>, IQueryable<TEntity>> ITemporalCriteria<TEntity>.TemporalEvaluator => dbset => dbset.AsTemporalAsOf(AsOf);
  }

  public class AsTemporalFromCriteria<TEntity> : ITemporalCriteria<TEntity> where TEntity : class
  {
    private DateTime From { get; set; }
    public DateTime To { get; }

    public AsTemporalFromCriteria(DateTime from, DateTime to)
    {
      From = from;
      To = to;
    }

    Func<DbSet<TEntity>, IQueryable<TEntity>> ITemporalCriteria<TEntity>.TemporalEvaluator => dbset => dbset.AsTemporalFrom(From, To);
  }

  public class AsTemporalBetweenCriteria<TEntity> : ITemporalCriteria<TEntity> where TEntity : class
  {
    private DateTime From { get; set; }
    public DateTime To { get; }

    public AsTemporalBetweenCriteria(DateTime from, DateTime to)
    {
      From = from;
      To = to;
    }

    Func<DbSet<TEntity>, IQueryable<TEntity>> ITemporalCriteria<TEntity>.TemporalEvaluator => dbset => dbset.AsTemporalBetween(From, To);
  }

  public class AsTemporalContainedCriteria<TEntity> : ITemporalCriteria<TEntity> where TEntity : class
  {
    private DateTime From { get; set; }
    public DateTime To { get; }

    public AsTemporalContainedCriteria(DateTime from, DateTime to)
    {
      From = from;
      To = to;
    }

    Func<DbSet<TEntity>, IQueryable<TEntity>> ITemporalCriteria<TEntity>.TemporalEvaluator => dbset => dbset.AsTemporalContained(From, To);
  }
}