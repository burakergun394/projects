using System.Linq.Expressions;

namespace FollowCatcher.Domain.Common;

/// <summary>
/// Base class for specifications following the Specification pattern.
/// Specifications encapsulate query logic into reusable, composable, and testable objects.
/// </summary>
public abstract class Specification<T>
{
    /// <summary>
    /// Converts the specification to a LINQ expression that can be used in queries.
    /// </summary>
    public abstract Expression<Func<T, bool>> ToExpression();

    /// <summary>
    /// Checks if an entity satisfies this specification.
    /// </summary>
    /// <param name="entity">The entity to check.</param>
    /// <returns>True if the entity satisfies the specification; otherwise, false.</returns>
    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }

    /// <summary>
    /// Combines this specification with another using AND logic.
    /// </summary>
    /// <param name="specification">The specification to combine with.</param>
    /// <returns>A new specification that represents the AND combination.</returns>
    public Specification<T> And(Specification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    /// <summary>
    /// Combines this specification with another using OR logic.
    /// </summary>
    /// <param name="specification">The specification to combine with.</param>
    /// <returns>A new specification that represents the OR combination.</returns>
    public Specification<T> Or(Specification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    /// <summary>
    /// Negates this specification.
    /// </summary>
    /// <returns>A new specification that represents the negation.</returns>
    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}

/// <summary>
/// Specification that combines two specifications using AND logic.
/// </summary>
internal class AndSpecification<T>(Specification<T> left, Specification<T> right) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = left.ToExpression();
        var rightExpression = right.ToExpression();

        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

/// <summary>
/// Specification that combines two specifications using OR logic.
/// </summary>
internal class OrSpecification<T>(Specification<T> left, Specification<T> right) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = left.ToExpression();
        var rightExpression = right.ToExpression();

        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.OrElse(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

/// <summary>
/// Specification that negates another specification.
/// </summary>
internal class NotSpecification<T>(Specification<T> specification) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var expression = specification.ToExpression();
        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.Not(Expression.Invoke(expression, parameter));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}
