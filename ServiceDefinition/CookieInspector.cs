using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Cleverscape.UTorrentClient.WebClient.ServiceDefinition
{
    public class CookieManagerEndpointBehavior : IEndpointBehavior
    {
        #region IEndpointBehavior Members

        public void AddBindingParameters(
            ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(
            ServiceEndpoint endpoint,
            ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(CookieManagerMessageInspector.Instance);
        }

        public void ApplyDispatchBehavior(
            ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(
            ServiceEndpoint endpoint)
        {
        }

        #endregion
    }

    /// <summary>
    /// Maintains a copy of the cookies contained in the incoming HTTP response received from any service
    /// and appends it to all outgoing HTTP requests.
    /// </summary>
    /// <remarks>
    /// This class effectively allows to send any received HTTP cookies to different services,
    /// reproducing the same functionality available in ASMX Web Services proxies with the <see cref="System.Net.CookieContainer"/> class.
    /// </remarks>
    public class CookieManagerMessageInspector : IClientMessageInspector
    {
        private static CookieManagerMessageInspector instance;
        
        public string SharedCookie { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientIdentityMessageInspector"/> class.
        /// </summary>
        private CookieManagerMessageInspector()
        {
        }

        /// <summary>
        /// Gets the singleton <see cref="ClientIdentityMessageInspector" /> instance.
        /// </summary>
        public static CookieManagerMessageInspector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CookieManagerMessageInspector();
                }

                return instance;
            }
        }

        /// <summary>
        /// Inspects a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
        /// <param name="correlationState">Correlation state data.</param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            HttpResponseMessageProperty httpResponse =
                reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;

            if (httpResponse != null)
            {
                string cookie = httpResponse.Headers[HttpResponseHeader.SetCookie];

                if (!string.IsNullOrEmpty(cookie))
                {
                    this.SharedCookie = cookie;
                }
            }
        }

        /// <summary>
        /// Inspects a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        /// <param name="channel">The client object channel.</param>
        /// <returns>
        /// <strong>Null</strong> since no message correlation is used.
        /// </returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequest;

            // The HTTP request object is made available in the outgoing message only when
            // the Visual Studio Debugger is attacched to the running process
            if (!request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                request.Properties.Add(HttpRequestMessageProperty.Name, new HttpRequestMessageProperty());
            }

            httpRequest = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
            httpRequest.Headers.Add(HttpRequestHeader.Cookie, this.SharedCookie);

            return null;
        }
    }
}
