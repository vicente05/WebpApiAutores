using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebpApiAutores.Filtros
{
    public class FiltroDeExcepcion : ExceptionFilterAttribute
    {
        public ILogger<FiltroDeExcepcion> logger { get; }

        public FiltroDeExcepcion(ILogger<FiltroDeExcepcion> logger)
        {
            this.logger = logger;
        }


        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}