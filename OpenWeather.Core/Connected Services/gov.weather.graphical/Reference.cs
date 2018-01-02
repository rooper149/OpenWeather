﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gov.weather.graphical
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl", ConfigurationName="gov.weather.graphical.ndfdXMLPortType")]
    internal interface ndfdXMLPortType
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#NDFDgen", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="dwmlOut")]
        System.Threading.Tasks.Task<string> NDFDgenAsync(decimal latitude, decimal longitude, gov.weather.graphical.productType product, System.DateTime startTime, System.DateTime endTime, gov.weather.graphical.unitType Unit, gov.weather.graphical.weatherParametersType weatherParameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#NDFDgenByDay", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="dwmlByDayOut")]
        System.Threading.Tasks.Task<string> NDFDgenByDayAsync(decimal latitude, decimal longitude, [System.Xml.Serialization.SoapElementAttribute(DataType="date")] System.DateTime startDate, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string numDays, gov.weather.graphical.unitType Unit, gov.weather.graphical.formatType format);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#NDFDgenLatLonList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="dwmlOut")]
        System.Threading.Tasks.Task<string> NDFDgenLatLonListAsync(string listLatLon, gov.weather.graphical.productType product, System.DateTime startTime, System.DateTime endTime, gov.weather.graphical.unitType Unit, gov.weather.graphical.weatherParametersType weatherParameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#NDFDgenByDayLatLonLis" +
            "t", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="dwmlByDayOut")]
        System.Threading.Tasks.Task<string> NDFDgenByDayLatLonListAsync(string listLatLon, [System.Xml.Serialization.SoapElementAttribute(DataType="date")] System.DateTime startDate, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string numDays, gov.weather.graphical.unitType Unit, gov.weather.graphical.formatType format);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#GmlLatLonList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="dwGmlOut")]
        System.Threading.Tasks.Task<string> GmlLatLonListAsync(string listLatLon, System.DateTime requestedTime, gov.weather.graphical.featureTypeType featureType, gov.weather.graphical.weatherParametersType weatherParameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#GmlTimeSeries", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="dwGmlOut")]
        System.Threading.Tasks.Task<string> GmlTimeSeriesAsync(string listLatLon, System.DateTime startTime, System.DateTime endTime, gov.weather.graphical.compTypeType compType, gov.weather.graphical.featureTypeType featureType, string propertyName);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#LatLonListSubgrid", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="listLatLonOut")]
        System.Threading.Tasks.Task<string> LatLonListSubgridAsync(decimal lowerLeftLatitude, decimal lowerLeftLongitude, decimal upperRightLatitude, decimal upperRightLongitude, decimal resolution);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#LatLonListLine", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="listLatLonOut")]
        System.Threading.Tasks.Task<string> LatLonListLineAsync(decimal endPoint1Lat, decimal endPoint1Lon, decimal endPoint2Lat, decimal endPoint2Lon);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#LatLonListZipCode", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="listLatLonOut")]
        System.Threading.Tasks.Task<string> LatLonListZipCodeAsync(string zipCodeList);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#LatLonListSquare", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="listLatLonOut")]
        System.Threading.Tasks.Task<string> LatLonListSquareAsync(decimal centerPointLat, decimal centerPointLon, decimal distanceLat, decimal distanceLon, decimal resolution);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#CornerPoints", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="listLatLonOut")]
        System.Threading.Tasks.Task<string> CornerPointsAsync(gov.weather.graphical.sectorType sector);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://graphical.weather.gov/xml/DWMLgen/wsdl/ndfdXML.wsdl#LatLonListCityNames", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="listCityNamesOut")]
        System.Threading.Tasks.Task<string> LatLonListCityNamesAsync([System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string displayLevel);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd")]
    public enum productType
    {
        
        /// <remarks/>
        [System.Xml.Serialization.SoapEnumAttribute("time-series")]
        timeseries,
        
        /// <remarks/>
        glance,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd")]
    public enum unitType
    {
        
        /// <remarks/>
        e,
        
        /// <remarks/>
        m,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd")]
    public partial class weatherParametersType
    {
        
        private bool maxtField;
        
        private bool mintField;
        
        private bool tempField;
        
        private bool dewField;
        
        private bool pop12Field;
        
        private bool qpfField;
        
        private bool skyField;
        
        private bool snowField;
        
        private bool wspdField;
        
        private bool wdirField;
        
        private bool wxField;
        
        private bool wavehField;
        
        private bool iconsField;
        
        private bool rhField;
        
        private bool apptField;
        
        private bool incw34Field;
        
        private bool incw50Field;
        
        private bool incw64Field;
        
        private bool cumw34Field;
        
        private bool cumw50Field;
        
        private bool cumw64Field;
        
        private bool critfireoField;
        
        private bool dryfireoField;
        
        private bool conhazoField;
        
        private bool ptornadoField;
        
        private bool phailField;
        
        private bool ptstmwindsField;
        
        private bool pxtornadoField;
        
        private bool pxhailField;
        
        private bool pxtstmwindsField;
        
        private bool ptotsvrtstmField;
        
        private bool pxtotsvrtstmField;
        
        private bool tmpabv14dField;
        
        private bool tmpblw14dField;
        
        private bool tmpabv30dField;
        
        private bool tmpblw30dField;
        
        private bool tmpabv90dField;
        
        private bool tmpblw90dField;
        
        private bool prcpabv14dField;
        
        private bool prcpblw14dField;
        
        private bool prcpabv30dField;
        
        private bool prcpblw30dField;
        
        private bool prcpabv90dField;
        
        private bool prcpblw90dField;
        
        private bool precipa_rField;
        
        private bool sky_rField;
        
        private bool td_rField;
        
        private bool temp_rField;
        
        private bool wdir_rField;
        
        private bool wspd_rField;
        
        private bool wwaField;
        
        private bool tstmprbField;
        
        private bool tstmcatField;
        
        private bool wgustField;
        
        private bool iceaccumField;
        
        private bool maxrhField;
        
        private bool minrhField;
        
        /// <remarks/>
        public bool maxt
        {
            get
            {
                return this.maxtField;
            }
            set
            {
                this.maxtField = value;
            }
        }
        
        /// <remarks/>
        public bool mint
        {
            get
            {
                return this.mintField;
            }
            set
            {
                this.mintField = value;
            }
        }
        
        /// <remarks/>
        public bool temp
        {
            get
            {
                return this.tempField;
            }
            set
            {
                this.tempField = value;
            }
        }
        
        /// <remarks/>
        public bool dew
        {
            get
            {
                return this.dewField;
            }
            set
            {
                this.dewField = value;
            }
        }
        
        /// <remarks/>
        public bool pop12
        {
            get
            {
                return this.pop12Field;
            }
            set
            {
                this.pop12Field = value;
            }
        }
        
        /// <remarks/>
        public bool qpf
        {
            get
            {
                return this.qpfField;
            }
            set
            {
                this.qpfField = value;
            }
        }
        
        /// <remarks/>
        public bool sky
        {
            get
            {
                return this.skyField;
            }
            set
            {
                this.skyField = value;
            }
        }
        
        /// <remarks/>
        public bool snow
        {
            get
            {
                return this.snowField;
            }
            set
            {
                this.snowField = value;
            }
        }
        
        /// <remarks/>
        public bool wspd
        {
            get
            {
                return this.wspdField;
            }
            set
            {
                this.wspdField = value;
            }
        }
        
        /// <remarks/>
        public bool wdir
        {
            get
            {
                return this.wdirField;
            }
            set
            {
                this.wdirField = value;
            }
        }
        
        /// <remarks/>
        public bool wx
        {
            get
            {
                return this.wxField;
            }
            set
            {
                this.wxField = value;
            }
        }
        
        /// <remarks/>
        public bool waveh
        {
            get
            {
                return this.wavehField;
            }
            set
            {
                this.wavehField = value;
            }
        }
        
        /// <remarks/>
        public bool icons
        {
            get
            {
                return this.iconsField;
            }
            set
            {
                this.iconsField = value;
            }
        }
        
        /// <remarks/>
        public bool rh
        {
            get
            {
                return this.rhField;
            }
            set
            {
                this.rhField = value;
            }
        }
        
        /// <remarks/>
        public bool appt
        {
            get
            {
                return this.apptField;
            }
            set
            {
                this.apptField = value;
            }
        }
        
        /// <remarks/>
        public bool incw34
        {
            get
            {
                return this.incw34Field;
            }
            set
            {
                this.incw34Field = value;
            }
        }
        
        /// <remarks/>
        public bool incw50
        {
            get
            {
                return this.incw50Field;
            }
            set
            {
                this.incw50Field = value;
            }
        }
        
        /// <remarks/>
        public bool incw64
        {
            get
            {
                return this.incw64Field;
            }
            set
            {
                this.incw64Field = value;
            }
        }
        
        /// <remarks/>
        public bool cumw34
        {
            get
            {
                return this.cumw34Field;
            }
            set
            {
                this.cumw34Field = value;
            }
        }
        
        /// <remarks/>
        public bool cumw50
        {
            get
            {
                return this.cumw50Field;
            }
            set
            {
                this.cumw50Field = value;
            }
        }
        
        /// <remarks/>
        public bool cumw64
        {
            get
            {
                return this.cumw64Field;
            }
            set
            {
                this.cumw64Field = value;
            }
        }
        
        /// <remarks/>
        public bool critfireo
        {
            get
            {
                return this.critfireoField;
            }
            set
            {
                this.critfireoField = value;
            }
        }
        
        /// <remarks/>
        public bool dryfireo
        {
            get
            {
                return this.dryfireoField;
            }
            set
            {
                this.dryfireoField = value;
            }
        }
        
        /// <remarks/>
        public bool conhazo
        {
            get
            {
                return this.conhazoField;
            }
            set
            {
                this.conhazoField = value;
            }
        }
        
        /// <remarks/>
        public bool ptornado
        {
            get
            {
                return this.ptornadoField;
            }
            set
            {
                this.ptornadoField = value;
            }
        }
        
        /// <remarks/>
        public bool phail
        {
            get
            {
                return this.phailField;
            }
            set
            {
                this.phailField = value;
            }
        }
        
        /// <remarks/>
        public bool ptstmwinds
        {
            get
            {
                return this.ptstmwindsField;
            }
            set
            {
                this.ptstmwindsField = value;
            }
        }
        
        /// <remarks/>
        public bool pxtornado
        {
            get
            {
                return this.pxtornadoField;
            }
            set
            {
                this.pxtornadoField = value;
            }
        }
        
        /// <remarks/>
        public bool pxhail
        {
            get
            {
                return this.pxhailField;
            }
            set
            {
                this.pxhailField = value;
            }
        }
        
        /// <remarks/>
        public bool pxtstmwinds
        {
            get
            {
                return this.pxtstmwindsField;
            }
            set
            {
                this.pxtstmwindsField = value;
            }
        }
        
        /// <remarks/>
        public bool ptotsvrtstm
        {
            get
            {
                return this.ptotsvrtstmField;
            }
            set
            {
                this.ptotsvrtstmField = value;
            }
        }
        
        /// <remarks/>
        public bool pxtotsvrtstm
        {
            get
            {
                return this.pxtotsvrtstmField;
            }
            set
            {
                this.pxtotsvrtstmField = value;
            }
        }
        
        /// <remarks/>
        public bool tmpabv14d
        {
            get
            {
                return this.tmpabv14dField;
            }
            set
            {
                this.tmpabv14dField = value;
            }
        }
        
        /// <remarks/>
        public bool tmpblw14d
        {
            get
            {
                return this.tmpblw14dField;
            }
            set
            {
                this.tmpblw14dField = value;
            }
        }
        
        /// <remarks/>
        public bool tmpabv30d
        {
            get
            {
                return this.tmpabv30dField;
            }
            set
            {
                this.tmpabv30dField = value;
            }
        }
        
        /// <remarks/>
        public bool tmpblw30d
        {
            get
            {
                return this.tmpblw30dField;
            }
            set
            {
                this.tmpblw30dField = value;
            }
        }
        
        /// <remarks/>
        public bool tmpabv90d
        {
            get
            {
                return this.tmpabv90dField;
            }
            set
            {
                this.tmpabv90dField = value;
            }
        }
        
        /// <remarks/>
        public bool tmpblw90d
        {
            get
            {
                return this.tmpblw90dField;
            }
            set
            {
                this.tmpblw90dField = value;
            }
        }
        
        /// <remarks/>
        public bool prcpabv14d
        {
            get
            {
                return this.prcpabv14dField;
            }
            set
            {
                this.prcpabv14dField = value;
            }
        }
        
        /// <remarks/>
        public bool prcpblw14d
        {
            get
            {
                return this.prcpblw14dField;
            }
            set
            {
                this.prcpblw14dField = value;
            }
        }
        
        /// <remarks/>
        public bool prcpabv30d
        {
            get
            {
                return this.prcpabv30dField;
            }
            set
            {
                this.prcpabv30dField = value;
            }
        }
        
        /// <remarks/>
        public bool prcpblw30d
        {
            get
            {
                return this.prcpblw30dField;
            }
            set
            {
                this.prcpblw30dField = value;
            }
        }
        
        /// <remarks/>
        public bool prcpabv90d
        {
            get
            {
                return this.prcpabv90dField;
            }
            set
            {
                this.prcpabv90dField = value;
            }
        }
        
        /// <remarks/>
        public bool prcpblw90d
        {
            get
            {
                return this.prcpblw90dField;
            }
            set
            {
                this.prcpblw90dField = value;
            }
        }
        
        /// <remarks/>
        public bool precipa_r
        {
            get
            {
                return this.precipa_rField;
            }
            set
            {
                this.precipa_rField = value;
            }
        }
        
        /// <remarks/>
        public bool sky_r
        {
            get
            {
                return this.sky_rField;
            }
            set
            {
                this.sky_rField = value;
            }
        }
        
        /// <remarks/>
        public bool td_r
        {
            get
            {
                return this.td_rField;
            }
            set
            {
                this.td_rField = value;
            }
        }
        
        /// <remarks/>
        public bool temp_r
        {
            get
            {
                return this.temp_rField;
            }
            set
            {
                this.temp_rField = value;
            }
        }
        
        /// <remarks/>
        public bool wdir_r
        {
            get
            {
                return this.wdir_rField;
            }
            set
            {
                this.wdir_rField = value;
            }
        }
        
        /// <remarks/>
        public bool wspd_r
        {
            get
            {
                return this.wspd_rField;
            }
            set
            {
                this.wspd_rField = value;
            }
        }
        
        /// <remarks/>
        public bool wwa
        {
            get
            {
                return this.wwaField;
            }
            set
            {
                this.wwaField = value;
            }
        }
        
        /// <remarks/>
        public bool tstmprb
        {
            get
            {
                return this.tstmprbField;
            }
            set
            {
                this.tstmprbField = value;
            }
        }
        
        /// <remarks/>
        public bool tstmcat
        {
            get
            {
                return this.tstmcatField;
            }
            set
            {
                this.tstmcatField = value;
            }
        }
        
        /// <remarks/>
        public bool wgust
        {
            get
            {
                return this.wgustField;
            }
            set
            {
                this.wgustField = value;
            }
        }
        
        /// <remarks/>
        public bool iceaccum
        {
            get
            {
                return this.iceaccumField;
            }
            set
            {
                this.iceaccumField = value;
            }
        }
        
        /// <remarks/>
        public bool maxrh
        {
            get
            {
                return this.maxrhField;
            }
            set
            {
                this.maxrhField = value;
            }
        }
        
        /// <remarks/>
        public bool minrh
        {
            get
            {
                return this.minrhField;
            }
            set
            {
                this.minrhField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd")]
    public enum formatType
    {
        
        /// <remarks/>
        [System.Xml.Serialization.SoapEnumAttribute("24 hourly")]
        Item24hourly,
        
        /// <remarks/>
        [System.Xml.Serialization.SoapEnumAttribute("12 hourly")]
        Item12hourly,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd")]
    public enum featureTypeType
    {
        
        /// <remarks/>
        Forecast_Gml2Point,
        
        /// <remarks/>
        Forecast_Gml2AllWx,
        
        /// <remarks/>
        Forecast_GmlsfPoint,
        
        /// <remarks/>
        Forecast_GmlObs,
        
        /// <remarks/>
        NdfdMultiPointCoverage,
        
        /// <remarks/>
        Ndfd_KmlPoint,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd")]
    public enum compTypeType
    {
        
        /// <remarks/>
        IsEqual,
        
        /// <remarks/>
        Between,
        
        /// <remarks/>
        GreaterThan,
        
        /// <remarks/>
        GreaterThanEqualTo,
        
        /// <remarks/>
        LessThan,
        
        /// <remarks/>
        LessThanEqualTo,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="https://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd")]
    public enum sectorType
    {
        
        /// <remarks/>
        conus,
        
        /// <remarks/>
        nhemi,
        
        /// <remarks/>
        alaska,
        
        /// <remarks/>
        guam,
        
        /// <remarks/>
        hawaii,
        
        /// <remarks/>
        puertori,
        
        /// <remarks/>
        npacocn,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    internal interface ndfdXMLPortTypeChannel : gov.weather.graphical.ndfdXMLPortType, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    internal partial class ndfdXMLPortTypeClient : System.ServiceModel.ClientBase<gov.weather.graphical.ndfdXMLPortType>, gov.weather.graphical.ndfdXMLPortType
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ndfdXMLPortTypeClient() : 
                base(ndfdXMLPortTypeClient.GetDefaultBinding(), ndfdXMLPortTypeClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.ndfdXMLPort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ndfdXMLPortTypeClient(EndpointConfiguration endpointConfiguration) : 
                base(ndfdXMLPortTypeClient.GetBindingForEndpoint(endpointConfiguration), ndfdXMLPortTypeClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ndfdXMLPortTypeClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ndfdXMLPortTypeClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ndfdXMLPortTypeClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ndfdXMLPortTypeClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ndfdXMLPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> NDFDgenAsync(decimal latitude, decimal longitude, gov.weather.graphical.productType product, System.DateTime startTime, System.DateTime endTime, gov.weather.graphical.unitType Unit, gov.weather.graphical.weatherParametersType weatherParameters)
        {
            return base.Channel.NDFDgenAsync(latitude, longitude, product, startTime, endTime, Unit, weatherParameters);
        }
        
        public System.Threading.Tasks.Task<string> NDFDgenByDayAsync(decimal latitude, decimal longitude, System.DateTime startDate, string numDays, gov.weather.graphical.unitType Unit, gov.weather.graphical.formatType format)
        {
            return base.Channel.NDFDgenByDayAsync(latitude, longitude, startDate, numDays, Unit, format);
        }
        
        public System.Threading.Tasks.Task<string> NDFDgenLatLonListAsync(string listLatLon, gov.weather.graphical.productType product, System.DateTime startTime, System.DateTime endTime, gov.weather.graphical.unitType Unit, gov.weather.graphical.weatherParametersType weatherParameters)
        {
            return base.Channel.NDFDgenLatLonListAsync(listLatLon, product, startTime, endTime, Unit, weatherParameters);
        }
        
        public System.Threading.Tasks.Task<string> NDFDgenByDayLatLonListAsync(string listLatLon, System.DateTime startDate, string numDays, gov.weather.graphical.unitType Unit, gov.weather.graphical.formatType format)
        {
            return base.Channel.NDFDgenByDayLatLonListAsync(listLatLon, startDate, numDays, Unit, format);
        }
        
        public System.Threading.Tasks.Task<string> GmlLatLonListAsync(string listLatLon, System.DateTime requestedTime, gov.weather.graphical.featureTypeType featureType, gov.weather.graphical.weatherParametersType weatherParameters)
        {
            return base.Channel.GmlLatLonListAsync(listLatLon, requestedTime, featureType, weatherParameters);
        }
        
        public System.Threading.Tasks.Task<string> GmlTimeSeriesAsync(string listLatLon, System.DateTime startTime, System.DateTime endTime, gov.weather.graphical.compTypeType compType, gov.weather.graphical.featureTypeType featureType, string propertyName)
        {
            return base.Channel.GmlTimeSeriesAsync(listLatLon, startTime, endTime, compType, featureType, propertyName);
        }
        
        public System.Threading.Tasks.Task<string> LatLonListSubgridAsync(decimal lowerLeftLatitude, decimal lowerLeftLongitude, decimal upperRightLatitude, decimal upperRightLongitude, decimal resolution)
        {
            return base.Channel.LatLonListSubgridAsync(lowerLeftLatitude, lowerLeftLongitude, upperRightLatitude, upperRightLongitude, resolution);
        }
        
        public System.Threading.Tasks.Task<string> LatLonListLineAsync(decimal endPoint1Lat, decimal endPoint1Lon, decimal endPoint2Lat, decimal endPoint2Lon)
        {
            return base.Channel.LatLonListLineAsync(endPoint1Lat, endPoint1Lon, endPoint2Lat, endPoint2Lon);
        }
        
        public System.Threading.Tasks.Task<string> LatLonListZipCodeAsync(string zipCodeList)
        {
            return base.Channel.LatLonListZipCodeAsync(zipCodeList);
        }
        
        public System.Threading.Tasks.Task<string> LatLonListSquareAsync(decimal centerPointLat, decimal centerPointLon, decimal distanceLat, decimal distanceLon, decimal resolution)
        {
            return base.Channel.LatLonListSquareAsync(centerPointLat, centerPointLon, distanceLat, distanceLon, resolution);
        }
        
        public System.Threading.Tasks.Task<string> CornerPointsAsync(gov.weather.graphical.sectorType sector)
        {
            return base.Channel.CornerPointsAsync(sector);
        }
        
        public System.Threading.Tasks.Task<string> LatLonListCityNamesAsync(string displayLevel)
        {
            return base.Channel.LatLonListCityNamesAsync(displayLevel);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ndfdXMLPort))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ndfdXMLPort))
            {
                return new System.ServiceModel.EndpointAddress("https://graphical.weather.gov/xml/SOAP_server/ndfdXMLserver.php");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return ndfdXMLPortTypeClient.GetBindingForEndpoint(EndpointConfiguration.ndfdXMLPort);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return ndfdXMLPortTypeClient.GetEndpointAddress(EndpointConfiguration.ndfdXMLPort);
        }
        
        public enum EndpointConfiguration
        {
            
            ndfdXMLPort,
        }
    }
}