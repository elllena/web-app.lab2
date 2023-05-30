using Lab2.Web.Services.Abstraction;
using MediatR;

namespace Lab2.Web.Resources.CreateKnife
{
    public class CreateKnifeRequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IQueueService _queue;

        public CreateKnifeRequestHandler(IQueueService queue)
        {
            _queue = queue;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validator = new Validator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Response.ValidationFailed(validationResult.Errors.First().ErrorMessage);
            }

            await _queue.Enqueue(request.Knife, cancellationToken);

            return Response.Successed();
        }
    }
}
