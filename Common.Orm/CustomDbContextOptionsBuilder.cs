using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Common.Orm
{
    public static class CustomDbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseCustomSqlServerQuerySqlGenerator(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IQuerySqlGeneratorFactory, CustomSqlServerQuerySqlGeneratorFactory>();
            return optionsBuilder;
        }
    }



    class CustomSqlServerQuerySqlGeneratorFactory : SqlServerQuerySqlGeneratorFactory
    {
        private readonly ISqlServerOptions sqlServerOptions;
        public CustomSqlServerQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies, ISqlServerOptions sqlServerOptions)
            : base(dependencies, sqlServerOptions) => this.sqlServerOptions = sqlServerOptions;
        public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression) =>
            new CustomSqlServerQuerySqlGenerator(Dependencies, selectExpression, sqlServerOptions.RowNumberPagingEnabled);
    }


    public class CustomSqlServerQuerySqlGenerator : SqlServerQuerySqlGenerator
    {
        public CustomSqlServerQuerySqlGenerator(
            QuerySqlGeneratorDependencies dependencies,
            SelectExpression selectExpression, 
            bool rowNumberPagingEnabled)
            : base(dependencies, selectExpression, rowNumberPagingEnabled) { }

        public override Expression VisitTable(TableExpression tableExpression)
        {
            // base will append schema, table and alias
            var result = base.VisitTable(tableExpression);
            Sql.Append(" WITH (NOLOCK)");
            return result;
        }
    }
}
