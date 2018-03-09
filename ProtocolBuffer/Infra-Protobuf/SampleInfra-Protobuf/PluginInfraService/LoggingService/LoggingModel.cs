using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Philips.PmsMR.Protobuf.DataModel;

namespace PluginInfraServices
{

    /// <summary>
    /// Severity class for a log entry.
    /// </summary>
    public enum LoggingSeverityType {

        /// <summary>
        /// Errors contain information about a failure of the system that leads directly to loss of performance or functionality.
        /// </summary>
        LoggingSeverityTypeError = 622,
        /// <summary>
        /// Fatal errors cause system downtime or a service to go down.
        /// </summary>
        LoggingSeverityTypeFatal = 34522,
        /// <summary>
        /// Information entries contain information about the status and usage of the system.
        /// </summary>
        LoggingSeverityTypeInformation = 1,
        /// <summary>
        /// Warnings contain information about a failure of the system that may lead to loss of performance or functionality.
        /// </summary>
        LoggingSeverityTypeWarning = 2,
    }

    /// <summary>
    /// Logging entry purpose identifier types.
    /// </summary>
    public enum LoggingType {
        /// <summary>
        /// Invalid, unknown logging purpose.
        /// </summary>
        UnknownLoggingType = 0,

        /// <summary>
        /// Development logging is meant for log entries that help the developer to understand the internal state of the software.
        /// </summary>
        LoggingTypeDevelopment = 1,
        /// <summary>
        /// Service logging is meant for log entries that support a field service engineer in diagnosis and repair of the system.
        /// </summary>
        LoggingTypeService = 32,
        /// <summary>
        /// Trace logging is meant for deducing software problems during system development.
        /// </summary>
        /// <remarks>
        /// By default, trace logging is not available at field.
        /// </remarks>
        LoggingTypeTrace = 2,
        /// <summary>
        /// Utilization logging is meant for extracting operational profiles and actual usage from the field.
        /// </summary>
        LoggingTypeUtilization = 24
    }

    /// <summary>
    /// Data record for a single loggable event.
    /// </summary>
    public class LogEntry {
        /// <summary>
        /// PII originator id.
        /// </summary>
        /// <remarks>
        /// A number reserved from a Philips wide originator-id numberspace.
        /// E.g., range 198000-199999 has been reserved for MR-Therapy applications.
        /// When the OriginatorId is concatenated with the event id (conventionally OriginatorId + "_" + EventId),
        /// the resulting id is called a LogMessageId, which is a unique identifier of the log message definition,
        /// the semantics of which should never change.
        /// The static semantics are needed when external, automated tools interpret the entries.
        /// </remarks>
        [ForcedId(1)]
        public Int32 OriginatorId;

        /// <summary>
        /// Unique identifier of the log message definition when combined with originator id.
        /// </summary>
        /// <remarks>
        /// The same event id can occur also in other originator ids, but the combination
        /// of the originator id and event id should be unique.
        /// Maximum length 100-OriginatorId.Length-1 (requirement from Common Data File format v2.0 + UX00085-01 logging guidelines).
        /// -1 comes from conventional concatenation with an underscore.
        /// </remarks>
        [ForcedId(2)]
        public string EventId;

        /// <summary>
        /// Clunks since 12:00 AM January 1, year 1 A.D. in the proleptic Gregorian Calendar.
        /// </summary>
        /// <remarks>
        /// Measured using the system clock on the hosting computer that recorded the occurrence of the event.
        /// </remarks>
        [ForcedId(3)]
        public Int64 TimeStamp;

        /// <summary>
        /// Description of the event that occurred.
        /// </summary>
        /// <remarks>
        /// Only the variable part of the Description adds to the semantics; the invariable part is coupled one-to-one to the LogMessageId. Does not contain parametrized data.
        /// Maximum length 1024 (requirement from Common Data File format v2.0).
        /// </remarks>
        [ForcedId(4)]
        public string Description;

        /// <summary>
        /// Additional information about the event that can be parametrized.
        /// </summary>
        [ForcedId(5)]
        public string AdditionalInfo;

        /// <summary>
        /// Severity of the log entry.
        /// </summary>
        [ForcedId(6)]
        public LoggingSeverityType Severity;

        /// <summary>
        /// Log entry usage type.
        /// </summary>
        /// <remarks>
        /// Describes the intended use of the entry.
        /// </remarks>
        [ForcedId(7)]
        public LoggingType Type;
    }

    /// <summary>
    /// DICOM patient identification with name+id combination.
    /// </summary>
    public class PatientIdentification
    {
        /// <summary>
        /// DICOM patient identification with id.
        /// </summary>
        public string PatientId;

        /// <summary>
        /// DICOM patient identification with name.
        /// </summary>
        public string PatientName;
    }

    /// <summary>
    /// Object Actions for audit trail entries.
    /// </summary>
    public enum AuditTrailActionType  {
        /// <summary>
        /// Unknown action.
        /// </summary>
        UnknownLoggingAuditTrailActionType = 0,

        /// <summary>
        /// Instance created.
        /// </summary>
        AuditTrailActionTypeCreate=532,

        /// <summary>
        /// Instance modified.
        /// </summary>
        AuditTrailActionTypeModify=24,

        /// <summary>
        /// Instance accessed.
        /// </summary>
        AuditTrailActionTypeAccess=63,

        /// <summary>
        /// Instance deleted.
        /// </summary>
        AuditTrailActionTypeDelete=844
    } ;

    /// <summary>
    /// Audit trail log entry for transmitted patient data (HIPAA AUDITTRAIL INSTANCES_SENT).
    /// </summary>
    public class InstancesSentEntry
    {
        /// <summary>
        /// DICOM Study Instance UID.
        /// </summary>
        public string Suid;

        /// <summary>
        /// DICOM Class Instance UID.
        /// </summary>
        public string Cuid;

        /// <summary>
        /// The number of DICOM SOP instances sent.
        /// </summary>
        public int InstancesSent;

        /// <summary>
        /// Action performed on the instances.
        /// </summary>
        public AuditTrailActionType ObjectAction;

        /// <summary>
        /// IP address where the instances were sent to.
        /// </summary>
        public string IPAddress;

        /// <summary>
        /// AETitle of the receiver of our transmission.
        /// </summary>
        public string ApplicationEntityTitle;

        /// <summary>
        /// Identification of the local user.
        /// </summary>
        public string LocalUser;

        /// <summary>
        /// Identification for the patient whose data was sent.
        /// </summary>
        public PatientIdentification Patient;
    }

}
