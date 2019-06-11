using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Actions;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;

namespace RoboPlant.Server.REST.Design
{
    [HypermediaObject(Title = "Access to the available Robot designs which are ready for production", Classes = new[] { "Design" })]
    public class DesignHto : HypermediaObject
    {
        [HypermediaAction(Name = "NewDesignQuery", Title = "Create a new query for the Design collection.")]
        public HypermediaAction<BlueprintQueryParameters> NewDesignQuery { get; private set; }

        public DesignHto()
        {
            NewDesignQuery = new HypermediaAction<BlueprintQueryParameters>( () => true );
        }
    }
}