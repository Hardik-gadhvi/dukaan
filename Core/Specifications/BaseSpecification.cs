using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? critaria) : ISpecification<T>
    {
        protected BaseSpecification() : this(null) { }
        public Expression<Func<T, bool>>? Critaria => critaria;

        public Expression<Func<T, object>>? OrderBy {  get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool isDistinct { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public IQueryable<T> ApplyCritaria(IQueryable<T> query)
        {
            if(Critaria != null)
            {
                query = query.Where(Critaria);
            }
            return query;
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderBy) {

            OrderBy = orderBy;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {

            OrderByDescending = orderByDescending;
        }
        protected void ApplyDistinct()
        {
            isDistinct = true;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }

    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> critaria)
        : BaseSpecification<T>(critaria), ISpecification<T, TResult>
    {
        protected BaseSpecification() : this(null!) { }
        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, TResult>> selectExpression) { Select = selectExpression; }
    }

}

