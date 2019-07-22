using System.Web.Mvc;
using RFAssessment.Models.Mapping;
using Unity;
using Unity.Mvc5;

namespace RFAssessment
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IMappingCreator, MappingCreator>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}