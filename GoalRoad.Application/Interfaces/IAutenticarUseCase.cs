namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface IAutenticarUseCase
    {
        Task<string?> AutenticarAsync(string email, string senha);
    }
}
