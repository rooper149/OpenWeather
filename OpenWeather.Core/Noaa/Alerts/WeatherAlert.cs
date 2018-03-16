using System;
using System.ComponentModel.DataAnnotations;

namespace OpenWeather.Noaa.Alerts
{

    #region Code Values

    public enum AlertStatusValues
    {
        /// <summary>
        ///  Actionable by all targeted recipients.
        /// </summary>
        Actual,
        /// <summary>
        /// Actionable only by designated exercise participants; exercise identifier should appear in <note>
        /// </summary>
        Exercise,
        /// <summary>
        /// For messages that support alert network internal functions.
        /// </summary>
        System,
        /// <summary>
        ///  Technical testing only, all recipients disregard.
        /// </summary>
        Test,
        /// <summary>
        ///  A preliminary template or draft, not actionable in its current form.
        /// </summary>
        Draft
    }

    public enum AlertMessageTypeValues
    {
        /// <summary>
        /// Requiring attention by targeted recipients.
        /// </summary>
        Alert,
        /// <summary>
        /// Updates and supercedes the earlier message(s) identified in <references>.,
        /// </summary>
        Update,
        /// <summary>
        /// Cancels the earlier message(s) identified in <references>
        /// </summary>
        Cancel,
        /// <summary>
        /// Acknowledges receipt and acceptance of the message(s)) identified in <references>.
        /// </summary>
        Acknowledge,
        /// <summary>
        /// Indicates rejection of the message(s) identified in <references>; explanation SHOULD appear in <note>
        /// </summary>
        Error
    }

    public enum AlertScopeValues
    {
        /// <summary>
        /// For general dissemination to unrestricted audiences.
        /// </summary>
        Public,
        /// <summary>
        /// For dissemination only to users with a known operational requirement <seealso cref="WeatherAlert.Restriction"/>.
        /// </summary>
        Restricted,
        /// <summary>
        ///  For dissemination only to specified addresses (see <address>, below)
        /// </summary>
        Private
    }

    #endregion

    public class WeatherAlert
    {

        /// <summary>
        /// Required. The identifier of the alert message.
        /// </summary>
        [Required]
        public string Id { get; internal set; }

        /// <summary>
        /// The last date and time the feed was updated.
        /// </summary>
        public DateTime Updated { get; internal set; }

        /// <summary>
        /// The last date and time the feed was published.
        /// </summary>
        public DateTime Published { get; internal set; }

        /// <summary>
        /// Required. The identifier of the sender of the alert message.
        /// </summary>
        [Required]
        public string Sender { get; internal set; }

        /// <summary>
        /// Required. The title of the alert message.
        /// </summary>
        [Required]
        public string Title { get; internal set; }

        /// <summary>
        /// A link to a web resource for more information.
        /// </summary>
        public Uri Link { get; internal set; }

        /// <summary>
        /// A summary about the alert. 
        /// </summary>
        public string Summary { get; internal set; }

        /// <summary>
        /// Required. The type of the subject event of the alert message.
        /// </summary>
        [Required]
        public string Event { get; internal set; }

        /// <summary>
        /// The effective time of the information of the alert message.
        /// </summary>
        public DateTime? Effective { get; internal set; }

        /// <summary>
        /// The expiry time of the information of the alert message.
        /// </summary>
        public DateTime? Expires { get; internal set; }

        /// <summary>
        /// Required. The code denoting the appropriate handling of the alert message.
        /// </summary>
        [Required]
        public AlertStatusValues Status { get; internal set; }

        /// <summary>
        /// Required. The code denoting the nature of the alert message.
        /// </summary>
        [Required]
        public AlertMessageTypeValues MessageType { get; internal set; }

        /// <summary>
        /// Optional. The particular source of this alert; e.g., an operator or a specific device.
        /// </summary>
        public string Source { get; internal set; }

        /// <summary>
        /// The code denoting the intended distribution of the alert message.
        /// </summary>
        public AlertScopeValues Scope { get; internal set; }

        /// <summary>
        /// Used when the <see cref="Scope"/> value is <see cref="AlertScopeValues.Restricted"/>. Otherwise null.
        /// </summary>
        public string Restriction { get; internal set; }

        /// <summary>
        /// <list type="number">
        /// <item>Used when <see cref="Scope"/> is <see cref="AlertScopeValues.Private"/>.</item>
        /// <item>Each  recipient SHALL be identified by an identifier or an address.</item>
        /// <item>Multiple space-delimited addresses MAY be included.  Addresses including whitespace MUST be enclosed in double-quotes.</item>
        /// </list>
        /// </summary>
        public string Addresses { get; internal set; }

        /// <summary>
        /// Optional. The code denoting the special handling of the alert message.
        /// </summary>
        public string Code { get; internal set; }

        /// <summary>
        /// Optional. The text describing the purpose or significance of the alert message 
        /// </summary>
        public string Note { get; internal set; }

        /// <summary>
        /// The group listing identifying earlier message(s) referenced by the alert message.
        /// </summary>
        public string References { get; internal set; }

        /// <summary>
        /// The group listing naming the referent incident(s) of the alert message.
        /// </summary>
        public string Incidents { get; internal set; }

        /// <summary>
        /// Optional. The container for information of the alert.
        /// </summary>
        public WeatherAlertInformation Information { get; internal set; }

        /// <summary>
        /// Required. The container for the area affected by the alert.
        /// </summary>
        [Required]
        public WeatherAlertArea Area { get; internal set; }
    }
}
