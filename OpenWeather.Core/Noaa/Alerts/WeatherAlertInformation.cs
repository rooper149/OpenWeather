using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace OpenWeather.Noaa.Alerts
{
    public enum WeatherAlertCategories
    {
        Geophysical,
        Meteorological,
        Safety,
        Security,
        Rescue,
        Fire,
        Health,
        Environmental,
        Transportation,
        Infrastructure,
        /// <summary>
        ///  Chemical, Biological, Radiological, Nuclear or High-Yield Explosive threat or attack.
        /// </summary>
        CBRNE,
        Other,
    }

    public enum WeatherAlertResponseTypes
    {
        /// <summary>
        /// Take shelter in place or per <instruction>.
        /// </summary>
        Shelter,
        /// <summary>
        ///  Relocate as instructed in the <instruction>.
        /// </summary>
        Evacuate,
        /// <summary>
        ///  Make preparations per the <instruction> .
        /// </summary>
        Prepare,
        /// <summary>
        /// Execute a pre-planned activity identified in <instruction>.
        /// </summary>
        Execute,
        /// <summary>
        /// Attend to information sources as described in <instruction> 
        /// </summary>
        Monitor,
        /// <summary>
        /// Evaluate the information in this message. (This value SHOULD NOT be used in public warning applications.) 
        /// </summary>
        Assess,
        /// <summary>
        /// No action recommended.
        /// </summary>
        None
    }

    public enum WeatherAlertUrgencies
    {
        /// <summary>
        /// Responsive action SHOULD be taken immediately.
        /// </summary>
        Immediate,
        /// <summary>
        /// Responsive action SHOULD be taken soon (within next hour).
        /// </summary>
        Expected,
        /// <summary>
        ///  Responsive action SHOULD be taken in the near future.
        /// </summary>
        Future,
        /// <summary>
        /// Responsive action is no longer required
        /// </summary>
        Past,
        /// <summary>
        /// Urgency not known 
        /// </summary>
        Unknown
    }

    public enum WeatherAlertSeverities
    {
        /// <summary>
        /// Extraordinary threat to life or property.
        /// </summary>
        Extreme,
        /// <summary>
        /// Significant threat to life or property.
        /// </summary>
        Severe,
        /// <summary>
        /// Possible threat to life or property.
        /// </summary>
        Moderate,
        /// <summary>
        /// Minimal threat to life or property.
        /// </summary>
        Minor,
        /// <summary>
        /// Severity unknown.
        /// </summary>
        Unknown
    }

    public enum WeatherAlertCertainties
    {
        /// <summary>
        ///  Determined to have occurred or to be ongoing.
        /// </summary>
        Observed,
        /// <summary>
        /// Likely (probability &gt ~50%) 
        /// </summary>
        Likely,
        /// <summary>
        /// Possible but not likely (probability &lt= ~50%)
        /// </summary>
        Possible,
        /// <summary>
        /// Not expected to occur (probability ~ 0) 
        /// </summary>
        Unlikely,
        /// <summary>
        /// Certainty unknown.
        /// </summary>
        Unknown
    }

    public class WeatherAlertInformation
    {
        /// <summary>
        /// Optional. The <see cref="CultureInfo"/> of the weather alert information.
        /// </summary>
        public CultureInfo Culture { get; internal set; }

        /// <summary>
        /// Required. The code denoting the category of the subject event of the alert message.
        /// Can contain multiple occurances.
        /// </summary>
        [Required]
        public IEnumerable<WeatherAlertCategories> Categories { get; internal set; }

        /// <summary>
        /// Required. The text denoting the type of the subject event of the alert message.
        /// </summary>
        [Required]
        public string Event { get; internal set; }

        /// <summary>
        /// Optional. The code denoting the type of action recommended for the target audience.
        /// </summary>
        public IEnumerable<WeatherAlertResponseTypes> ResponseTypes { get; internal set; }

        /// <summary>
        /// Required. The code denoting the urgency of the subject event of the alert message.
        /// </summary>
        [Required]
        public WeatherAlertUrgencies Urgency { get; internal set; }

        /// <summary>
        /// Required. The code denoting the severity of the subject event of the alert message.
        /// </summary>
        [Required]
        public WeatherAlertSeverities Severity { get; internal set; }

        /// <summary>
        /// Required. The code denoting the certainty of the subject event of the alert message.
        /// </summary>
        [Required]
        public WeatherAlertCertainties Certainty { get; internal set; }

        /// <summary>
        /// Optional. The text describing the intended audience of the alert message.
        /// </summary>
        public string Audience { get; internal set; }

        //public string EventCode { get; set; }

        /// <summary>
        /// Optional. The effective time of the information of the alert message.
        /// </summary>
        public DateTime? Effective { get; internal set; }

        /// <summary>
        /// Optional. The expected time of the beginning of the subject event of the alert message.
        /// </summary>
        public DateTime? Onset { get; internal set; }

        /// <summary>
        /// Optional. The expiry time of the information of the alert message.
        /// </summary>
        public DateTime? Expires { get; internal set; }

        /// <summary>
        /// Optional. The text naming the originator of the alert message.
        /// </summary>
        public string Sender { get; internal set; }

        /// <summary>
        /// Optional. The text headline of the alert message.
        /// </summary>
        public string Headline { get; internal set; }

        /// <summary>
        /// Optional. The text describing the subject event of the alert message.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// The text describing the recommended action to be taken by recipients of the alert message.
        /// </summary>
        public string Instruction { get; internal set; }

        /// <summary>
        /// Optional. The identifier of the hyperlink associating additional information with the alert message.
        /// </summary>
        public Uri Web { get; internal set; }

        /// <summary>
        /// Optional. The text describing the contact for follow-up and confirmation of the alert message.
        /// </summary>
        public string Contact { get; internal set; }


    }
}
