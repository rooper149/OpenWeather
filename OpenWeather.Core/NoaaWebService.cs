using OpenWeather.Php;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace OpenWeather
{
    public abstract class NoaaWebServiceBase
    {

        #region Helpers

        public class HttpUserAgentMessageInspector : IClientMessageInspector
        {
            private const string USER_AGENT_HTTP_HEADER = "user-agent";

            private string m_userAgent;

            public HttpUserAgentMessageInspector(string userAgent)
            {
                this.m_userAgent = userAgent;
            }

            #region IClientMessageInspector Members

            public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
            {
            }

            public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
            {
                HttpRequestMessageProperty httpRequestMessage;
                object httpRequestMessageObject;
                if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
                {
                    httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
                    if (string.IsNullOrEmpty(httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER]))
                    {
                        httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER] = this.m_userAgent;
                    }
                }
                else
                {
                    httpRequestMessage = new HttpRequestMessageProperty();
                    httpRequestMessage.Headers.Add(USER_AGENT_HTTP_HEADER, this.m_userAgent);
                    request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
                }
                return null;
            }

            #endregion IClientMessageInspector Members

        }

        public class HttpUserAgentEndpointBehavior : IEndpointBehavior
        {
            private string m_userAgent;

            public HttpUserAgentEndpointBehavior(string userAgent)
            {
                this.m_userAgent = userAgent;
            }

            #region IEndpointBehavior Members

            public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
            {
            }

            public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
            {
                HttpUserAgentMessageInspector inspector = new HttpUserAgentMessageInspector(this.m_userAgent);
                clientRuntime.ClientMessageInspectors.Add(inspector);
            }

            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
            {
            }

            public void Validate(ServiceEndpoint endpoint)
            {
            }

            #endregion
        }


        internal gov.weather.graphical.ndfdXMLPortTypeClient CreateClient()
        {
            EndpointAddress endpointAddress = new EndpointAddress("https://graphical.weather.gov:443/xml/SOAP_server/ndfdXMLserver.php");
            CustomBinding binding = new CustomBinding(
                new CustomTextMessageBindingElement("iso-8859-1", "text/xml", MessageVersion.Soap11),
                new HttpsTransportBindingElement());
            gov.weather.graphical.ndfdXMLPortTypeClient client = new gov.weather.graphical.ndfdXMLPortTypeClient(binding, endpointAddress);
            client.Endpoint.EndpointBehaviors.Add(new HttpUserAgentEndpointBehavior("Myself"));

            return client;
        }

        #endregion



    }
}
