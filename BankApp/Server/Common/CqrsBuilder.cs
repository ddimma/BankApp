namespace BankApp.Server.Common
{
    public static class CqrsBuilder<TDto, TCommandOrQuery>
    {
        private static readonly Func<TDto, TCommandOrQuery> BuildFunction;

        static CqrsBuilder()
        {
            BuildFunction = CompileBuildFunction();
        }

        public static TCommandOrQuery Build(TDto dto)
        {
            return BuildFunction(dto);
        }

        private static Func<TDto, TCommandOrQuery> CompileBuildFunction()
        {
            var dtoParameter = System.Linq.Expressions.Expression.Parameter(typeof(TDto));
            var memberBindings = new List<System.Linq.Expressions.MemberBinding>();

            foreach (var property in typeof(TCommandOrQuery).GetProperties())
            {
                var dtoProperty = typeof(TDto).GetProperty(property.Name);
                if (dtoProperty is not null)
                {
                    var propertyAccess = System.Linq.Expressions.Expression.Property(dtoParameter, dtoProperty);
                    var memberBinding = System.Linq.Expressions.Expression.Bind(property, propertyAccess);
                    memberBindings.Add(memberBinding);
                }
            }

            var memberInit = System.Linq.Expressions.Expression.MemberInit(
                System.Linq.Expressions.Expression.New(typeof(TCommandOrQuery)),
                memberBindings
            );

            var lambda = System.Linq.Expressions.Expression.Lambda<Func<TDto, TCommandOrQuery>>(memberInit, dtoParameter);

            return lambda.Compile();
        }
    }
}

