namespace Lab2.Web.Services.Abstraction
{
    public interface IQueueService
    {
        Task Enqueue<T>(T item, CancellationToken cancellationToken);
    }
}
