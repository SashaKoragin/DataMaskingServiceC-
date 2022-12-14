using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace DataMasking.Cors
{
    public class CorsEnablingBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CorsHeaderInjectingMessageInspector());
        }
        public void Validate(ServiceEndpoint endpoint) { }
        public override Type BehaviorType { get { return typeof(CorsEnablingBehavior); } }
        protected override object CreateBehavior() { return new CorsEnablingBehavior(); }

        private class CorsHeaderInjectingMessageInspector : IDispatchMessageInspector
        {
            public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
            {
                var httpRequest = (HttpRequestMessageProperty)request
                     .Properties[HttpRequestMessageProperty.Name];
                return new
                {
                    origin = httpRequest.Headers["Origin"],
                    handlePreflight = httpRequest.Method.Equals("OPTIONS",
                        StringComparison.InvariantCultureIgnoreCase)
                };
            }

            private static IDictionary<string, string> _headersToInject = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" },
            { "Access-Control-Allow-Origin", "*" },
            { "Vary", "Origin" },
            { "Access-Control-Request-Method", "POST, GET, PUT, DELETE, OPTIONS" },
            { "Allow",  "POST, GET, PUT, DELETE, OPTIONS" },
            { "Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Origin, User-Agent, Access-Control-Allow-Headers, Authorization, X-Requested-With" },
            { "Access-Control-Allow-Credentials", "true" }
        };
            public void BeforeSendReply(ref Message reply, object correlationState)
            {
                var state = (dynamic)correlationState;
                if (state.handlePreflight)
                {
                    reply = Message.CreateMessage(MessageVersion.None, "PreflightReturn");
                    var httpResponse = new HttpResponseMessageProperty();
                    reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);
                    httpResponse.SuppressEntityBody = true;
                    httpResponse.StatusCode = HttpStatusCode.OK;
                }
                var httpHeader = reply.Properties["httpResponse"] as HttpResponseMessageProperty;
                foreach (var item in _headersToInject)
                {
                    if (httpHeader != null) httpHeader.Headers.Add(item.Key, item.Value);
                }
            }
        }
    }
}
