﻿using RoboPlant.Server.REST.Production;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;
using WebApi.HypermediaExtensions.Hypermedia.Links;

namespace RoboPlant.Server.REST.EntryPoint
{
    [HypermediaObject(Title = "Entry to the RoboPlant REST API", Classes = new[] { "EntryPoint" })]
    public class EntryPointHto : HypermediaObject
    {
        public EntryPointHto()
        {
            this.Links.Add("production", new HypermediaObjectKeyReference(typeof(ProductionHto)));
        }
    }
}