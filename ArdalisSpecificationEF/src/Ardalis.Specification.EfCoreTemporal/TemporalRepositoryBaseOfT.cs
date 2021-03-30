using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ardalis.Specification.Supplement
{
  public abstract class TemporalRepositoryBase<T> : RepositoryBase<T> where T : class
  {
    private readonly DbContext dbContext;
    private readonly ISpecificationEvaluator specificationEvaluator;

    protected TemporalRepositoryBase(DbContext dbContext) : this(dbContext, SpecificationEvaluator.Default)
    {
    }

    protected TemporalRepositoryBase(DbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
      this.dbContext = dbContext;
      this.specificationEvaluator = specificationEvaluator;
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> ListAsync(TemporalFilter? temporalFilter, CancellationToken cancellationToken = default)
    {
      var query = temporalFilter is TemporalFilter ?
        temporalFilter?.TemporalSpecification?.TemporalCriteria<T>().TemporalEvaluator(dbContext.Set<T>()) :
        dbContext.Set<T>();
      return await query.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Filters the entities  of <typeparamref name="T"/>, to those that match the encapsulated query logic of the
    /// <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <returns>The filtered entities as an <see cref="IQueryable{T}"/>.</returns>
    protected override IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
    {
      var query = dbContext.Set<T>().AsQueryable();

      if (specification is ITemporalSpecification)
      {
        query = ((ITemporalSpecification)specification).TemporalCriteria<T>().TemporalEvaluator(dbContext.Set<T>());
      }

      return specificationEvaluator.GetQuery(query, specification, evaluateCriteriaOnly);
    }

    /// <summary>
    /// Filters all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    /// <paramref name="specification"/>, from the database.
    /// <para>
    /// Projects each entity into a new form, being <typeparamref name="TResult" />.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <returns>The filtered projected entities as an <see cref="IQueryable{T}"/>.</returns>
    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
    {
      if (specification is null) throw new ArgumentNullException("Specification is required");
      if (specification.Selector is null) throw new SelectorNotFoundException();

      var query = dbContext.Set<T>().AsQueryable();

      if (specification is ITemporalSpecification)
      {
        query = ((ITemporalSpecification)specification).TemporalCriteria<T>().TemporalEvaluator(dbContext.Set<T>());
      }

      return specificationEvaluator.GetQuery(query, specification);
    }
  }
}