using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.HealthCheck
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly ApplicationContext _context;

        public DatabaseHealthCheck(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Carreiras.AsNoTracking().Take(1).AnyAsync(cancellationToken);

                return HealthCheckResult.Healthy("Banco de dados Respondeu!!");
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy("Banco de dados nao Respondeu!!");
            }
        }
    }
}

