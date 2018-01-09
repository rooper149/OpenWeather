using gov.weather.graphical;
using OpenWeather.Php;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace OpenWeather.Noaa.Base
{
    /// <summary>
    /// The unit of measurement used on the response
    /// </summary>
    public enum Units
    {
        /// <summary>
        /// Metric
        /// </summary>
        Metric,
        /// <summary>
        /// Imperial. A.k.a Standard Units. Used in the US, Liberia, and Panama
        /// </summary>
        Imperial
    }

    public enum RequestType
    {
        /// <summary>
        /// A detailed forcast
        /// </summary>
        Forcast,
        /// <summary>
        /// The “glance” product returns all data between the start and end times for the parameters: 
        /// Maximum Temperature, Minimum Temerature, Cloud Cover, Weather, and Icons.
        /// </summary>
        Glance
    }


    public abstract class NoaaWebServiceBase
    {
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

        internal ndfdXMLPortTypeClient CreateClient()
        {
            EndpointAddress endpointAddress = new EndpointAddress("https://graphical.weather.gov:443/xml/SOAP_server/ndfdXMLserver.php");
            CustomBinding binding = new CustomBinding(
                new CustomTextMessageBindingElement("iso-8859-1", "text/xml", MessageVersion.Soap11),
                new HttpsTransportBindingElement() { MaxReceivedMessageSize = 2097152 });
            ndfdXMLPortTypeClient client = new ndfdXMLPortTypeClient(binding, endpointAddress);
            client.Endpoint.EndpointBehaviors.Add(new HttpUserAgentEndpointBehavior("Myself"));
            client.Endpoint.Binding.OpenTimeout = TimeSpan.FromMinutes(1);
            client.Endpoint.Binding.CloseTimeout = TimeSpan.FromMinutes(1);
            client.Endpoint.Binding.SendTimeout = TimeSpan.FromMinutes(10);
            client.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            return client;
        }

        internal protected weatherParametersType ConvertToWeatherParametersType(Models.WeatherParameters weatherParameters)
        {
            return new weatherParametersType()
            {
                appt = weatherParameters.IsApparentTemperatureRequested,
                conhazo = weatherParameters.IsConvectiveHazardOutlookRequested,
                critfireo = weatherParameters.IsFireWeatherFromWindAndRelativeHumidityRequested,
                cumw34 = weatherParameters.IsProbabilisticTropicalCycloneWindSpeedOver34Knots_CumulativeRequested,
                cumw50 = weatherParameters.IsProbabilisticTropicalCycloneWindSpeedOver50Knots_CumulativeRequested,
                cumw64 = weatherParameters.IsProbabilisticTropicalCycloneWindSpeedOver64Knots_CumulativeRequested,
                dew = weatherParameters.IsDewPointTemeratureRequested,
                dryfireo = weatherParameters.IsFireWeatherFromDryThunderstormsRequested,
                iceaccum = weatherParameters.IsIceAccumulationRequested,
                icons = weatherParameters.IsWeatherIconsRequested,
                incw34 = weatherParameters.IsProbabilisticTropicalCycloneWindSpeedOver34Knots_IncrementalRequested,
                incw50 = weatherParameters.IsProbabilisticTropicalCycloneWindSpeedOver50Knots_IncrementalRequested,
                incw64 = weatherParameters.IsProbabilisticTropicalCycloneWindSpeedOver64Knots_IncrementalRequested,
                maxrh = weatherParameters.IsMaximumRelativeHumidityRequested,
                maxt = weatherParameters.IsMaximumTempuratureRequested,
                minrh = weatherParameters.IsMinimumRelativeHumidityRequested,
                mint = weatherParameters.IsMinimumTempuratureRequested,
                phail = weatherParameters.IsProbabilityOfHailRequested,
                pop12 = weatherParameters.IsProbabilityOfPrecipitationRequested,
                prcpabv14d = weatherParameters.IsProbability8To14DayTotalPrecipitationAboveMedianRequested,
                prcpabv30d = weatherParameters.IsProbabilityOneMonthTotalPrecipitationAboveMedianRequested,
                prcpabv90d = weatherParameters.IsProbabilityThreeMonthTotalPrecipitationAboveMedianRequested,
                prcpblw14d = weatherParameters.IsProbability8To14DayTotalPrecipitationBelowMedianRequested,
                prcpblw30d = weatherParameters.IsProbabilityOneMonthTotalPrecipitationBelowMedianRequested,
                prcpblw90d = weatherParameters.IsProbabilityThreeMonthTotalPrecipitationBelowMedianRequested,
                precipa_r = weatherParameters.IsRealTimeMesoscaleAnalysisPrecipitationRequested,
                ptornado = weatherParameters.IsProbabilityOfTornadoesRequested,
                ptotsvrtstm = weatherParameters.IsProbabilityOfSevereThunderstormsRequested,
                ptstmwinds = weatherParameters.IsProbabilityOfDamagingThunderstormWindsRequested,
                pxhail = weatherParameters.IsProbabilityOfExtremeHailRequested,
                pxtornado = weatherParameters.IsProbabilityOfExtremeTornadoesRequested,
                pxtotsvrtstm = weatherParameters.IsProbabilityOfExtremeSevereThunderstormsRequested,
                pxtstmwinds = weatherParameters.IsProbabilityOfExtremeThunderstormWindsRequested,
                qpf = weatherParameters.IsPrecipitationAmountRequested,
                rh = weatherParameters.IsRelativeHumidityRequested,
                sky = weatherParameters.IsCloudCoverAmountRequested,
                sky_r = weatherParameters.IsRealTimeMesoscaleAnalysisGOESEffectiveCloudAmountRequested,
                snow = weatherParameters.IsSnowFallAmountRequested,
                td_r = weatherParameters.IsRealTimeMesoscaleAnalysisDewpointTemperatureRequested,
                temp = weatherParameters.Is3HourTempRequested,
                temp_r = weatherParameters.IsRealTimeMesoscaleAnalysisTemperatureRequested,
                tmpabv14d = weatherParameters.IsProbabilityOf8To14DayAverageTemperatureAboveNormalRequested,
                tmpabv30d = weatherParameters.IsProbabilityOfOneMonthAverageTemperatureAboveNormalRequested,
                tmpabv90d = weatherParameters.IsProbabilityOfThreeMonthAverageTemperatureAboveNormalRequested,
                tmpblw14d = weatherParameters.IsProbabilityOf8To14DayAverageTemperatureBelowNormalRequested,
                tmpblw30d = weatherParameters.IsProbabilityOfOneMonthAverageTemperatureBelowNormalRequested,
                tmpblw90d = weatherParameters.IsProbabilityOfThreeMonthAverageTemperatureBelowNormalRequested,
                tstmcat = false, // removed 20140313
                tstmprb = false, // removed 20140314
                waveh = weatherParameters.IsWaveHeightRequested,
                wdir = weatherParameters.IsWindDirectionRequested,
                wdir_r = weatherParameters.IsRealTimeMesoscaleAnalysisWindDirectionRequested,
                wgust = weatherParameters.IsWindGustsRequested,
                wspd = weatherParameters.IsWindSpeedRequested,
                wspd_r = weatherParameters.IsRealTimeMesoscaleAnalysisWindSpeedRequested,
                wwa = weatherParameters.IsWatchesWarningAdvisoriesRequested,
                wx = weatherParameters.IsWeatherRequested
            };
        }

    }
}
