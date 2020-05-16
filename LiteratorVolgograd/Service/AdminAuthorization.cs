using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace LiteratorVolgograd.Service
{
    public class AdminAuthorization : IControllerModelConvention
    {
        private readonly string _policy;

        public AdminAuthorization(string policy)
        {
            _policy = policy;
        }

        public void Apply(ControllerModel controller)
        {
            controller.Filters.Add(new AuthorizeFilter(_policy));
        }
    }
}
