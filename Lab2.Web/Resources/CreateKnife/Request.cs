using Lab2.Entities;
using MediatR;

namespace Lab2.Web.Resources.CreateKnife
{
    public class Request : IRequest<Response>
    {
        public Knife Knife { get; set; }
    }
}
