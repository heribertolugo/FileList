using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models.Win32
{

    [Flags]
    internal enum ImageListDrawItemConstants
    {
        ILD_NORMAL = 0,
        ILD_TRANSPARENT = 1,
        ILD_BLEND25 = 2,
        ILD_SELECTED = 4,
        ILD_MASK = 16, // 0x00000010
        ILD_IMAGE = 32, // 0x00000020
        ILD_ROP = 64, // 0x00000040
        ILD_PRESERVEALPHA = 4096, // 0x00001000
        ILD_SCALE = 8192, // 0x00002000
        ILD_DPISCALE = 16384, // 0x00004000
    }

    [Flags]
    internal enum ImageListDrawStateConstants
    {
        ILS_NORMAL = 0,
        ILS_GLOW = 1,
        ILS_SHADOW = 2,
        ILS_SATURATE = 4,
        ILS_ALPHA = 8,
    }

    internal enum SysImageListSize
    {
        largeIcons = 0,
        smallIcons = 1,
        extraLargeIcons = 2,
        jumbo = 4,
    }

    [Flags]
    internal enum ShellIconStateConstants
    {
        ShellIconStateNormal = 0,
        ShellIconStateLinkOverlay = 32768, // 0x00008000
        ShellIconStateSelected = 65536, // 0x00010000
        ShellIconStateOpen = 2,
        ShellIconAddOverlays = 32, // 0x00000020
    }

    [Flags]
    internal enum SHGetFileInfoConstants
    {
        SHGFI_ICON = 256, // 0x00000100
        SHGFI_DISPLAYNAME = 512, // 0x00000200
        SHGFI_TYPENAME = 1024, // 0x00000400
        SHGFI_ATTRIBUTES = 2048, // 0x00000800
        SHGFI_ICONLOCATION = 4096, // 0x00001000
        SHGFI_EXETYPE = 8192, // 0x00002000
        SHGFI_SYSICONINDEX = 16384, // 0x00004000
        SHGFI_LINKOVERLAY = 32768, // 0x00008000
        SHGFI_SELECTED = 65536, // 0x00010000
        SHGFI_ATTR_SPECIFIED = 131072, // 0x00020000
        SHGFI_LARGEICON = 0,
        SHGFI_SMALLICON = 1,
        SHGFI_OPENICON = 2,
        SHGFI_SHELLICONSIZE = 4,
        SHGFI_USEFILEATTRIBUTES = 16, // 0x00000010
        SHGFI_ADDOVERLAYS = 32, // 0x00000020
        SHGFI_OVERLAYINDEX = 64, // 0x00000040
    }


    public enum MSHCTX : uint
    {
        /// <summary>
        /// The unmarshaling process is local and has shared memory access with the marshaling process.
        /// </summary>
        MSHCTX_LOCAL = 0,
        /// <summary>
        /// The unmarshaling process does not have shared memory access with the marshaling process.
        /// </summary>
        MSHCTX_NOSHAREDMEM = 1,
        /// <summary>
        /// The unmarshaling process is on a different computer. The marshaling code cannot assume that a particular piece of application code is installed on that computer.
        /// </summary>
        MSHCTX_DIFFERENTMACHINE = 2,
        /// <summary>
        /// The unmarshaling will be done in another apartment in the same process.
        /// </summary>
        MSHCTX_INPROC = 3,
        /// <summary>
        /// Create a new context in the current apartment.
        /// </summary>
        MSHCTX_CROSSCTX = 4,
        MSHCTX_RESERVED1 = 8
    }

    [Flags]
    public enum MSHLFLAGS : uint
    {
        /// <summary>
        /// The marshaling is occurring because an interface pointer is being passed from one process to another. This is the normal case. The data packet produced by the marshaling process will be unmarshaled in the destination process. The marshaled data packet can be unmarshaled just once, or not at all. If the receiver unmarshals the data packet successfully, the CoReleaseMarshalData function is automatically called on the data packet as part of the unmarshaling process. If the receiver does not or cannot unmarshal the data packet, the sender must call CoReleaseMarshalData on the data packet.
        /// </summary>
        MSHLFLAGS_NORMAL = 0,
        /// <summary>
        /// The marshaling is occurring because the data packet is to be stored in a globally accessible table from which it can be unmarshaled one or more times, or not at all. The presence of the data packet in the table counts as a strong reference to the interface being marshaled, meaning that it is sufficient to keep the object alive. When the data packet is removed from the table, the table implementer must call the CoReleaseMarshalData function on the data packet.
        /// MSHLFLAGS_TABLESTRONG is used by the RegisterDragDrop function when registering a window as a drop target. This keeps the window registered as a drop target no matter how many times the end user drags across the window. The RevokeDragDrop function calls CoReleaseMarshalData.
        /// </summary>
        MSHLFLAGS_TABLESTRONG = 1,
        /// <summary>
        /// The marshaling is occurring because the data packet is to be stored in a globally accessible table from which it can be unmarshaled one or more times, or not at all. However, the presence of the data packet in the table acts as a weak reference to the interface being marshaled, meaning that it is not sufficient to keep the object alive. When the data packet is removed from the table, the table implementer must call the CoReleaseMarshalData function on the data packet.
        /// MSHLFLAGS_TABLEWEAK is typically used when registering an object in the running object table (ROT). This prevents the object's entry in the ROT from keeping the object alive in the absence of any other connections. See IRunningObjectTable::Register for more information.
        /// </summary>
        MSHLFLAGS_TABLEWEAK = 2,
        /// <summary>
        /// Adding this flag to an original object marshaling (as opposed to marshaling a proxy) will disable the ping protocol for that object.
        /// </summary>
        MSHLFLAGS_NOPING = 4,
        MSHLFLAGS_RESERVED1 = 8,
        MSHLFLAGS_RESERVED2 = 16,
        MSHLFLAGS_RESERVED3 = 32,
        MSHLFLAGS_RESERVED4 = 64
    }

    public enum STREAM_SEEK
    {
        /// <summary>
        /// The new seek pointer is an offset relative to the beginning of the stream. In this case, the dlibMove parameter is the new seek position relative to the beginning of the stream.
        /// </summary>
        STREAM_SEEK_SET = 0,
        /// <summary>
        /// The new seek pointer is an offset relative to the current seek pointer location. In this case, the dlibMove parameter is the signed displacement from the current seek position.
        /// </summary>
        STREAM_SEEK_CUR = 1,
        /// <summary>
        /// The new seek pointer is an offset relative to the end of the stream. In this case, the dlibMove parameter is the new seek position relative to the end of the stream.
        /// </summary>
        STREAM_SEEK_END = 2
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/com/com-error-codes
    /// </summary>
    public struct HRESULT : IEquatable<HRESULT>, IEquatable<int>, IEquatable<long>
    {
        private readonly long _value;
        private readonly string _hex, _name, _desc;
        private readonly bool _isSet;

        // list of all known values
        private static readonly HashSet<HRESULT> results = new HashSet<HRESULT>();
        // special cases
        public static readonly HRESULT NONE = new HRESULT(null, "NONE", "Empty Default"); // non-initialized value
        public static readonly HRESULT S_OK = new HRESULT(0, "S_OK", "Success"); // Non-Error code -> success value

        #region Definitions
        // ************************************************************************** \\
        #region COM Error Codes (Generic)
        /// <summary>
        /// Catastrophic failure
        /// </summary>
        public static HRESULT E_UNEXPECTED = new HRESULT("0x8000FFFF", "E_UNEXPECTED", "Catastrophic failure");

        /// <summary>
        /// Not implemented
        /// </summary>
        public static HRESULT E_NOTIMPL = new HRESULT("0x80004001", "E_NOTIMPL", "Not implemented");

        /// <summary>
        /// Ran out of memory
        /// </summary>
        public static HRESULT E_OUTOFMEMORY = new HRESULT("0x8007000E", "E_OUTOFMEMORY", "Ran out of memory");

        /// <summary>
        /// One or more arguments are invalid
        /// </summary>
        public static HRESULT E_INVALIDARG = new HRESULT("0x80070057", "E_INVALIDARG", "One or more arguments are invalid");

        /// <summary>
        /// No such interface supported
        /// </summary>
        public static HRESULT E_NOINTERFACE = new HRESULT("0x80004002", "E_NOINTERFACE", "No such interface supported");

        /// <summary>
        /// Invalid pointer
        /// </summary>
        public static HRESULT E_POINTER = new HRESULT("0x80004003", "E_POINTER", "Invalid pointer");

        /// <summary>
        /// Invalid handle
        /// </summary>
        public static HRESULT E_HANDLE = new HRESULT("0x80070006", "E_HANDLE", "Invalid handle");

        /// <summary>
        /// Operation aborted
        /// </summary>
        public static HRESULT E_ABORT = new HRESULT("0x80004004", "E_ABORT", "Operation aborted");

        /// <summary>
        /// Unspecified error
        /// </summary>
        public static HRESULT E_FAIL = new HRESULT("0x80004005", "E_FAIL", "Unspecified error");

        /// <summary>
        /// General access denied error
        /// </summary>
        public static HRESULT E_ACCESSDENIED = new HRESULT("0x80070005", "E_ACCESSDENIED", "General access denied error");

        /// <summary>
        /// The data necessary to complete this operation is not yet available.
        /// </summary>
        public static HRESULT E_PENDING = new HRESULT("0x8000000A", "E_PENDING", "The data necessary to complete this operation is not yet available.");

        /// <summary>
        /// The operation attempted to access data outside the valid range
        /// </summary>
        public static HRESULT E_BOUNDS = new HRESULT("0x8000000B", "E_BOUNDS", "The operation attempted to access data outside the valid range");

        /// <summary>
        /// A concurrent or interleaved operation changed the state of the object, invalidating this operation.
        /// </summary>
        public static HRESULT E_CHANGED_STATE = new HRESULT("0x8000000C", "E_CHANGED_STATE", "A concurrent or interleaved operation changed the state of the object, invalidating this operation.");

        /// <summary>
        /// An illegal state change was requested.
        /// </summary>
        public static HRESULT E_ILLEGAL_STATE_CHANGE = new HRESULT("0x8000000D", "E_ILLEGAL_STATE_CHANGE", "An illegal state change was requested.");

        /// <summary>
        /// A method was called at an unexpected time.
        /// </summary>
        public static HRESULT E_ILLEGAL_METHOD_CALL = new HRESULT("0x8000000E", "E_ILLEGAL_METHOD_CALL", "A method was called at an unexpected time.");

        /// <summary>
        /// Typename or Namespace was not found in metadata file.
        /// </summary>
        public static HRESULT RO_E_METADATA_NAME_NOT_FOUND = new HRESULT("0x8000000F", "RO_E_METADATA_NAME_NOT_FOUND", "Typename or Namespace was not found in metadata file.");

        /// <summary>
        /// Name is an existing namespace rather than a typename.
        /// </summary>
        public static HRESULT RO_E_METADATA_NAME_IS_NAMESPACE = new HRESULT("0x80000010", "RO_E_METADATA_NAME_IS_NAMESPACE", "Name is an existing namespace rather than a typename.");

        /// <summary>
        /// Typename has an invalid format.
        /// </summary>
        public static HRESULT RO_E_METADATA_INVALID_TYPE_FORMAT = new HRESULT("0x80000011", "RO_E_METADATA_INVALID_TYPE_FORMAT", "Typename has an invalid format.");

        /// <summary>
        /// Metadata file is invalid or corrupted.
        /// </summary>
        public static HRESULT RO_E_INVALID_METADATA_FILE = new HRESULT("0x80000012", "RO_E_INVALID_METADATA_FILE", "Metadata file is invalid or corrupted.");

        /// <summary>
        /// The object has been closed.
        /// </summary>
        public static HRESULT RO_E_CLOSED = new HRESULT("0x80000013", "RO_E_CLOSED", "The object has been closed.");

        /// <summary>
        /// Only one thread may access the object during a write operation.
        /// </summary>
        public static HRESULT RO_E_EXCLUSIVE_WRITE = new HRESULT("0x80000014", "RO_E_EXCLUSIVE_WRITE", "Only one thread may access the object during a write operation.");

        /// <summary>
        /// Operation is prohibited during change notification.
        /// </summary>
        public static HRESULT RO_E_CHANGE_NOTIFICATION_IN_PROGRESS = new HRESULT("0x80000015", "RO_E_CHANGE_NOTIFICATION_IN_PROGRESS", "Operation is prohibited during change notification.");

        /// <summary>
        /// The text associated with this error code could not be found.
        /// </summary>
        public static HRESULT RO_E_ERROR_STRING_NOT_FOUND = new HRESULT("0x80000016", "RO_E_ERROR_STRING_NOT_FOUND", "The text associated with this error code could not be found.");

        /// <summary>
        /// String not null terminated.
        /// </summary>
        public static HRESULT E_STRING_NOT_NULL_TERMINATED = new HRESULT("0x80000017", "E_STRING_NOT_NULL_TERMINATED", "String not null terminated.");

        /// <summary>
        /// A delegate was assigned when not allowed.
        /// </summary>
        public static HRESULT E_ILLEGAL_DELEGATE_ASSIGNMENT = new HRESULT("0x80000018", "E_ILLEGAL_DELEGATE_ASSIGNMENT", "A delegate was assigned when not allowed.");

        /// <summary>
        /// An async operation was not properly started.
        /// </summary>
        public static HRESULT E_ASYNC_OPERATION_NOT_STARTED = new HRESULT("0x80000019", "E_ASYNC_OPERATION_NOT_STARTED", "An async operation was not properly started.");

        /// <summary>
        /// The application is exiting and cannot service this request.
        /// </summary>
        public static HRESULT E_APPLICATION_EXITING = new HRESULT("0x8000001A", "E_APPLICATION_EXITING", "The application is exiting and cannot service this request.");

        /// <summary>
        /// The application view is exiting and cannot service this request.
        /// </summary>
        public static HRESULT E_APPLICATION_VIEW_EXITING = new HRESULT("0x8000001B", "E_APPLICATION_VIEW_EXITING", "The application view is exiting and cannot service this request.");

        /// <summary>
        /// The object must support the IAgileObject interface.
        /// </summary>
        public static HRESULT RO_E_MUST_BE_AGILE = new HRESULT("0x8000001C", "RO_E_MUST_BE_AGILE", "The object must support the IAgileObject interface.");

        /// <summary>
        /// Activating a single-threaded class from MTA is not supported.
        /// </summary>
        public static HRESULT RO_E_UNSUPPORTED_FROM_MTA = new HRESULT("0x8000001D", "RO_E_UNSUPPORTED_FROM_MTA", "Activating a single-threaded class from MTA is not supported.");

        /// <summary>
        /// The object has been committed.
        /// </summary>
        public static HRESULT RO_E_COMMITTED = new HRESULT("0x8000001E", "RO_E_COMMITTED", "The object has been committed.");

        /// <summary>
        /// Thread local storage failure
        /// </summary>
        public static HRESULT CO_E_INIT_TLS = new HRESULT("0x80004006", "CO_E_INIT_TLS", "Thread local storage failure");

        /// <summary>
        /// Get shared memory allocator failure
        /// </summary>
        public static HRESULT CO_E_INIT_SHARED_ALLOCATOR = new HRESULT("0x80004007", "CO_E_INIT_SHARED_ALLOCATOR", "Get shared memory allocator failure");

        /// <summary>
        /// Get memory allocator failure
        /// </summary>
        public static HRESULT CO_E_INIT_MEMORY_ALLOCATOR = new HRESULT("0x80004008", "CO_E_INIT_MEMORY_ALLOCATOR", "Get memory allocator failure");

        /// <summary>
        /// Unable to initialize class cache
        /// </summary>
        public static HRESULT CO_E_INIT_CLASS_CACHE = new HRESULT("0x80004009", "CO_E_INIT_CLASS_CACHE", "Unable to initialize class cache");

        /// <summary>
        /// Unable to initialize RPC services
        /// </summary>
        public static HRESULT CO_E_INIT_RPC_CHANNEL = new HRESULT("0x8000400A", "CO_E_INIT_RPC_CHANNEL", "Unable to initialize RPC services");

        /// <summary>
        /// Cannot set thread local storage channel control
        /// </summary>
        public static HRESULT CO_E_INIT_TLS_SET_CHANNEL_CONTROL = new HRESULT("0x8000400B", "CO_E_INIT_TLS_SET_CHANNEL_CONTROL", "Cannot set thread local storage channel control");

        /// <summary>
        /// Could not allocate thread local storage channel control
        /// </summary>
        public static HRESULT CO_E_INIT_TLS_CHANNEL_CONTROL = new HRESULT("0x8000400C", "CO_E_INIT_TLS_CHANNEL_CONTROL", "Could not allocate thread local storage channel control");

        /// <summary>
        /// The user supplied memory allocator is unacceptable
        /// </summary>
        public static HRESULT CO_E_INIT_UNACCEPTED_USER_ALLOCATOR = new HRESULT("0x8000400D", "CO_E_INIT_UNACCEPTED_USER_ALLOCATOR", "The user supplied memory allocator is unacceptable");

        /// <summary>
        /// The OLE service mutex already exists
        /// </summary>
        public static HRESULT CO_E_INIT_SCM_MUTEX_EXISTS = new HRESULT("0x8000400E", "CO_E_INIT_SCM_MUTEX_EXISTS", "The OLE service mutex already exists");

        /// <summary>
        /// The OLE service file mapping already exists
        /// </summary>
        public static HRESULT CO_E_INIT_SCM_FILE_MAPPING_EXISTS = new HRESULT("0x8000400F", "CO_E_INIT_SCM_FILE_MAPPING_EXISTS", "The OLE service file mapping already exists");

        /// <summary>
        /// Unable to map view of file for OLE service
        /// </summary>
        public static HRESULT CO_E_INIT_SCM_MAP_VIEW_OF_FILE = new HRESULT("0x80004010", "CO_E_INIT_SCM_MAP_VIEW_OF_FILE", "Unable to map view of file for OLE service");

        /// <summary>
        /// Failure attempting to launch OLE service
        /// </summary>
        public static HRESULT CO_E_INIT_SCM_EXEC_FAILURE = new HRESULT("0x80004011", "CO_E_INIT_SCM_EXEC_FAILURE", "Failure attempting to launch OLE service");

        /// <summary>
        /// There was an attempt to call CoInitialize a second time while single threaded
        /// </summary>
        public static HRESULT CO_E_INIT_ONLY_SINGLE_THREADED = new HRESULT("0x80004012", "CO_E_INIT_ONLY_SINGLE_THREADED", "There was an attempt to call CoInitialize a second time while single threaded");

        /// <summary>
        /// A Remote activation was necessary but was not allowed
        /// </summary>
        public static HRESULT CO_E_CANT_REMOTE = new HRESULT("0x80004013", "CO_E_CANT_REMOTE", "A Remote activation was necessary but was not allowed");

        /// <summary>
        /// A Remote activation was necessary but the server name provided was invalid
        /// </summary>
        public static HRESULT CO_E_BAD_SERVER_NAME = new HRESULT("0x80004014", "CO_E_BAD_SERVER_NAME", "A Remote activation was necessary but the server name provided was invalid");

        /// <summary>
        /// The class is configured to run as a security id different from the caller
        /// </summary>
        public static HRESULT CO_E_WRONG_SERVER_IDENTITY = new HRESULT("0x80004015", "CO_E_WRONG_SERVER_IDENTITY", "The class is configured to run as a security id different from the caller");

        /// <summary>
        /// Use of Ole1 services requiring DDE windows is disabled
        /// </summary>
        public static HRESULT CO_E_OLE1DDE_DISABLED = new HRESULT("0x80004016", "CO_E_OLE1DDE_DISABLED", "Use of Ole1 services requiring DDE windows is disabled");

        /// <summary>
        /// A RunAs specification must be \ or simply . 
        /// </summary>
        public static HRESULT CO_E_RUNAS_SYNTAX = new HRESULT("0x80004017", "CO_E_RUNAS_SYNTAX", "A RunAs specification must be \\ or simply . ");

        /// <summary>
        /// The server process could not be started. The pathname may be incorrect.
        /// </summary>
        public static HRESULT CO_E_CREATEPROCESS_FAILURE = new HRESULT("0x80004018", "CO_E_CREATEPROCESS_FAILURE", "The server process could not be started. The pathname may be incorrect.");

        /// <summary>
        /// The server process could not be started as the configured identity. The pathname may be incorrect or unavailable.
        /// </summary>
        public static HRESULT CO_E_RUNAS_CREATEPROCESS_FAILURE = new HRESULT("0x80004019", "CO_E_RUNAS_CREATEPROCESS_FAILURE", "The server process could not be started as the configured identity. The pathname may be incorrect or unavailable.");

        /// <summary>
        /// The server process could not be started because the configured identity is incorrect. Check the user name and password.
        /// </summary>
        public static HRESULT CO_E_RUNAS_LOGON_FAILURE = new HRESULT("0x8000401A", "CO_E_RUNAS_LOGON_FAILURE", "The server process could not be started because the configured identity is incorrect. Check the user name and password.");

        /// <summary>
        /// The client is not allowed to launch this server.
        /// </summary>
        public static HRESULT CO_E_LAUNCH_PERMSSION_DENIED = new HRESULT("0x8000401B", "CO_E_LAUNCH_PERMSSION_DENIED", "The client is not allowed to launch this server.");

        /// <summary>
        /// The service providing this server could not be started.
        /// </summary>
        public static HRESULT CO_E_START_SERVICE_FAILURE = new HRESULT("0x8000401C", "CO_E_START_SERVICE_FAILURE", "The service providing this server could not be started.");

        /// <summary>
        /// This computer was unable to communicate with the computer providing the server.
        /// </summary>
        public static HRESULT CO_E_REMOTE_COMMUNICATION_FAILURE = new HRESULT("0x8000401D", "CO_E_REMOTE_COMMUNICATION_FAILURE", "This computer was unable to communicate with the computer providing the server.");

        /// <summary>
        /// The server did not respond after being launched.
        /// </summary>
        public static HRESULT CO_E_SERVER_START_TIMEOUT = new HRESULT("0x8000401E", "CO_E_SERVER_START_TIMEOUT", "The server did not respond after being launched.");

        /// <summary>
        /// The registration information for this server is inconsistent or incomplete.
        /// </summary>
        public static HRESULT CO_E_CLSREG_INCONSISTENT = new HRESULT("0x8000401F", "CO_E_CLSREG_INCONSISTENT", "The registration information for this server is inconsistent or incomplete.");

        /// <summary>
        /// The registration information for this interface is inconsistent or incomplete.
        /// </summary>
        public static HRESULT CO_E_IIDREG_INCONSISTENT = new HRESULT("0x80004020", "CO_E_IIDREG_INCONSISTENT", "The registration information for this interface is inconsistent or incomplete.");

        /// <summary>
        /// The operation attempted is not supported.
        /// </summary>
        public static HRESULT CO_E_NOT_SUPPORTED = new HRESULT("0x80004021", "CO_E_NOT_SUPPORTED", "The operation attempted is not supported.");

        /// <summary>
        /// A dll must be loaded.
        /// </summary>
        public static HRESULT CO_E_RELOAD_DLL = new HRESULT("0x80004022", "CO_E_RELOAD_DLL", "A dll must be loaded.");

        /// <summary>
        /// A Microsoft Software Installer error was encountered.
        /// </summary>
        public static HRESULT CO_E_MSI_ERROR = new HRESULT("0x80004023", "CO_E_MSI_ERROR", "A Microsoft Software Installer error was encountered.");

        /// <summary>
        /// The specified activation could not occur in the client context as specified.
        /// </summary>
        public static HRESULT CO_E_ATTEMPT_TO_CREATE_OUTSIDE_CLIENT_CONTEXT = new HRESULT("0x80004024", "CO_E_ATTEMPT_TO_CREATE_OUTSIDE_CLIENT_CONTEXT", "The specified activation could not occur in the client context as specified.");

        /// <summary>
        /// Activations on the server are paused.
        /// </summary>
        public static HRESULT CO_E_SERVER_PAUSED = new HRESULT("0x80004025", "CO_E_SERVER_PAUSED", "Activations on the server are paused.");

        /// <summary>
        /// Activations on the server are not paused.
        /// </summary>
        public static HRESULT CO_E_SERVER_NOT_PAUSED = new HRESULT("0x80004026", "CO_E_SERVER_NOT_PAUSED", "Activations on the server are not paused.");

        /// <summary>
        /// The component or application containing the component has been disabled.
        /// </summary>
        public static HRESULT CO_E_CLASS_DISABLED = new HRESULT("0x80004027", "CO_E_CLASS_DISABLED", "The component or application containing the component has been disabled.");

        /// <summary>
        /// The common language runtime is not available
        /// </summary>
        public static HRESULT CO_E_CLRNOTAVAILABLE = new HRESULT("0x80004028", "CO_E_CLRNOTAVAILABLE", "The common language runtime is not available");

        /// <summary>
        /// The thread-pool rejected the submitted asynchronous work.
        /// </summary>
        public static HRESULT CO_E_ASYNC_WORK_REJECTED = new HRESULT("0x80004029", "CO_E_ASYNC_WORK_REJECTED", "The thread-pool rejected the submitted asynchronous work.");

        /// <summary>
        /// The server started, but did not finish initializing in a timely fashion.
        /// </summary>
        public static HRESULT CO_E_SERVER_INIT_TIMEOUT = new HRESULT("0x8000402A", "CO_E_SERVER_INIT_TIMEOUT", "The server started, but did not finish initializing in a timely fashion.");

        /// <summary>
        /// Unable to complete the call since there is no COM+ security context inside IObjectControl.Activate.
        /// </summary>
        public static HRESULT CO_E_NO_SECCTX_IN_ACTIVATE = new HRESULT("0x8000402B", "CO_E_NO_SECCTX_IN_ACTIVATE", "Unable to complete the call since there is no COM+ security context inside IObjectControl.Activate.");

        /// <summary>
        /// The provided tracker configuration is invalid
        /// </summary>
        public static HRESULT CO_E_TRACKER_CONFIG = new HRESULT("0x80004030", "CO_E_TRACKER_CONFIG", "The provided tracker configuration is invalid");

        /// <summary>
        /// The provided thread pool configuration is invalid
        /// </summary>
        public static HRESULT CO_E_THREADPOOL_CONFIG = new HRESULT("0x80004031", "CO_E_THREADPOOL_CONFIG", "The provided thread pool configuration is invalid");

        /// <summary>
        /// The provided side-by-side configuration is invalid
        /// </summary>
        public static HRESULT CO_E_SXS_CONFIG = new HRESULT("0x80004032", "CO_E_SXS_CONFIG", "The provided side-by-side configuration is invalid");

        /// <summary>
        /// The server principal name (SPN) obtained during security negotiation is malformed.
        /// </summary>
        public static HRESULT CO_E_MALFORMED_SPN = new HRESULT("0x80004033", "CO_E_MALFORMED_SPN", "The server principal name (SPN) obtained during security negotiation is malformed.");

        /// <summary>
        /// Invalid OLEVERB structure
        /// </summary>
        public static HRESULT OLE_E_OLEVERB = new HRESULT("0x80040000", "OLE_E_OLEVERB", "Invalid OLEVERB structure");

        /// <summary>
        /// Invalid advise flags
        /// </summary>
        public static HRESULT OLE_E_ADVF = new HRESULT("0x80040001", "OLE_E_ADVF", "Invalid advise flags");

        /// <summary>
        /// Can't enumerate any more, because the associated data is missing
        /// </summary>
        public static HRESULT OLE_E_ENUM_NOMORE = new HRESULT("0x80040002", "OLE_E_ENUM_NOMORE", "Can't enumerate any more, because the associated data is missing");

        /// <summary>
        /// This implementation doesn't take advises
        /// </summary>
        public static HRESULT OLE_E_ADVISENOTSUPPORTED = new HRESULT("0x80040003", "OLE_E_ADVISENOTSUPPORTED", "This implementation doesn't take advises");

        /// <summary>
        /// There is no connection for this connection ID
        /// </summary>
        public static HRESULT OLE_E_NOCONNECTION = new HRESULT("0x80040004", "OLE_E_NOCONNECTION", "There is no connection for this connection ID");

        /// <summary>
        /// Need to run the object to perform this operation
        /// </summary>
        public static HRESULT OLE_E_NOTRUNNING = new HRESULT("0x80040005", "OLE_E_NOTRUNNING", "Need to run the object to perform this operation");

        /// <summary>
        /// There is no cache to operate on
        /// </summary>
        public static HRESULT OLE_E_NOCACHE = new HRESULT("0x80040006", "OLE_E_NOCACHE", "There is no cache to operate on");

        /// <summary>
        /// Uninitialized object
        /// </summary>
        public static HRESULT OLE_E_BLANK = new HRESULT("0x80040007", "OLE_E_BLANK", "Uninitialized object");

        /// <summary>
        /// Linked object's source class has changed
        /// </summary>
        public static HRESULT OLE_E_CLASSDIFF = new HRESULT("0x80040008", "OLE_E_CLASSDIFF", "Linked object's source class has changed");

        /// <summary>
        /// Not able to get the moniker of the object
        /// </summary>
        public static HRESULT OLE_E_CANT_GETMONIKER = new HRESULT("0x80040009", "OLE_E_CANT_GETMONIKER", "Not able to get the moniker of the object");

        /// <summary>
        /// Not able to bind to the source
        /// </summary>
        public static HRESULT OLE_E_CANT_BINDTOSOURCE = new HRESULT("0x8004000A", "OLE_E_CANT_BINDTOSOURCE", "Not able to bind to the source");

        /// <summary>
        /// Object is static; operation not allowed
        /// </summary>
        public static HRESULT OLE_E_STATIC = new HRESULT("0x8004000B", "OLE_E_STATIC", "Object is static; operation not allowed");

        /// <summary>
        /// User canceled out of save dialog
        /// </summary>
        public static HRESULT OLE_E_PROMPTSAVECANCELLED = new HRESULT("0x8004000C", "OLE_E_PROMPTSAVECANCELLED", "User canceled out of save dialog");

        /// <summary>
        /// Invalid rectangle
        /// </summary>
        public static HRESULT OLE_E_INVALIDRECT = new HRESULT("0x8004000D", "OLE_E_INVALIDRECT", "Invalid rectangle");

        /// <summary>
        /// compobj.dll is too old for the ole2.dll initialized
        /// </summary>
        public static HRESULT OLE_E_WRONGCOMPOBJ = new HRESULT("0x8004000E", "OLE_E_WRONGCOMPOBJ", "compobj.dll is too old for the ole2.dll initialized");

        /// <summary>
        /// Invalid window handle
        /// </summary>
        public static HRESULT OLE_E_INVALIDHWND = new HRESULT("0x8004000F", "OLE_E_INVALIDHWND", "Invalid window handle");

        /// <summary>
        /// Object is not in any of the inplace active states
        /// </summary>
        public static HRESULT OLE_E_NOT_INPLACEACTIVE = new HRESULT("0x80040010", "OLE_E_NOT_INPLACEACTIVE", "Object is not in any of the inplace active states");

        /// <summary>
        /// Not able to convert object
        /// </summary>
        public static HRESULT OLE_E_CANTCONVERT = new HRESULT("0x80040011", "OLE_E_CANTCONVERT", "Not able to convert object");

        /// <summary>
        /// Not able to perform the operation because object is not given storage yet
        /// </summary>
        public static HRESULT OLE_E_NOSTORAGE = new HRESULT("0x80040012", "OLE_E_NOSTORAGE", "Not able to perform the operation because object is not given storage yet");

        /// <summary>
        /// Invalid FORMATETC structure
        /// </summary>
        public static HRESULT DV_E_FORMATETC = new HRESULT("0x80040064", "DV_E_FORMATETC", "Invalid FORMATETC structure");

        /// <summary>
        /// Invalid DVTARGETDEVICE structure
        /// </summary>
        public static HRESULT DV_E_DVTARGETDEVICE = new HRESULT("0x80040065", "DV_E_DVTARGETDEVICE", "Invalid DVTARGETDEVICE structure");

        /// <summary>
        /// Invalid STDGMEDIUM structure
        /// </summary>
        public static HRESULT DV_E_STGMEDIUM = new HRESULT("0x80040066", "DV_E_STGMEDIUM", "Invalid STDGMEDIUM structure");

        /// <summary>
        /// Invalid STATDATA structure
        /// </summary>
        public static HRESULT DV_E_STATDATA = new HRESULT("0x80040067", "DV_E_STATDATA", "Invalid STATDATA structure");

        /// <summary>
        /// Invalid lindex
        /// </summary>
        public static HRESULT DV_E_LINDEX = new HRESULT("0x80040068", "DV_E_LINDEX", "Invalid lindex");

        /// <summary>
        /// Invalid tymed
        /// </summary>
        public static HRESULT DV_E_TYMED = new HRESULT("0x80040069", "DV_E_TYMED", "Invalid tymed");

        /// <summary>
        /// Invalid clipboard format
        /// </summary>
        public static HRESULT DV_E_CLIPFORMAT = new HRESULT("0x8004006A", "DV_E_CLIPFORMAT", "Invalid clipboard format");

        /// <summary>
        /// Invalid aspect(s)
        /// </summary>
        public static HRESULT DV_E_DVASPECT = new HRESULT("0x8004006B", "DV_E_DVASPECT", "Invalid aspect(s)");

        /// <summary>
        /// tdSize parameter of the DVTARGETDEVICE structure is invalid
        /// </summary>
        public static HRESULT DV_E_DVTARGETDEVICE_SIZE = new HRESULT("0x8004006C", "DV_E_DVTARGETDEVICE_SIZE", "tdSize parameter of the DVTARGETDEVICE structure is invalid");

        /// <summary>
        /// Object doesn't support IViewObject interface
        /// </summary>
        public static HRESULT DV_E_NOIVIEWOBJECT = new HRESULT("0x8004006D", "DV_E_NOIVIEWOBJECT", "Object doesn't support IViewObject interface");

        /// <summary>
        /// Trying to revoke a drop target that has not been registered
        /// </summary>
        public static HRESULT DRAGDROP_E_NOTREGISTERED = new HRESULT("0x80040100", "DRAGDROP_E_NOTREGISTERED", "Trying to revoke a drop target that has not been registered");

        /// <summary>
        /// This window has already been registered as a drop target
        /// </summary>
        public static HRESULT DRAGDROP_E_ALREADYREGISTERED = new HRESULT("0x80040101", "DRAGDROP_E_ALREADYREGISTERED", "This window has already been registered as a drop target");

        /// <summary>
        /// Invalid window handle
        /// </summary>
        public static HRESULT DRAGDROP_E_INVALIDHWND = new HRESULT("0x80040102", "DRAGDROP_E_INVALIDHWND", "Invalid window handle");

        /// <summary>
        /// Class does not support aggregation (or class object is remote)
        /// </summary>
        public static HRESULT CLASS_E_NOAGGREGATION = new HRESULT("0x80040110", "CLASS_E_NOAGGREGATION", "Class does not support aggregation (or class object is remote)");

        /// <summary>
        /// ClassFactory cannot supply requested class
        /// </summary>
        public static HRESULT CLASS_E_CLASSNOTAVAILABLE = new HRESULT("0x80040111", "CLASS_E_CLASSNOTAVAILABLE", "ClassFactory cannot supply requested class");

        /// <summary>
        /// Class is not licensed for use
        /// </summary>
        public static HRESULT CLASS_E_NOTLICENSED = new HRESULT("0x80040112", "CLASS_E_NOTLICENSED", "Class is not licensed for use");

        /// <summary>
        /// Error drawing view
        /// </summary>
        public static HRESULT VIEW_E_DRAW = new HRESULT("0x80040140", "VIEW_E_DRAW", "Error drawing view");

        /// <summary>
        /// Could not read key from registry
        /// </summary>
        public static HRESULT REGDB_E_READREGDB = new HRESULT("0x80040150", "REGDB_E_READREGDB", "Could not read key from registry");

        /// <summary>
        /// Could not write key to registry
        /// </summary>
        public static HRESULT REGDB_E_WRITEREGDB = new HRESULT("0x80040151", "REGDB_E_WRITEREGDB", "Could not write key to registry");

        /// <summary>
        /// Could not find the key in the registry
        /// </summary>
        public static HRESULT REGDB_E_KEYMISSING = new HRESULT("0x80040152", "REGDB_E_KEYMISSING", "Could not find the key in the registry");

        /// <summary>
        /// Invalid value for registry
        /// </summary>
        public static HRESULT REGDB_E_INVALIDVALUE = new HRESULT("0x80040153", "REGDB_E_INVALIDVALUE", "Invalid value for registry");

        /// <summary>
        /// Class not registered
        /// </summary>
        public static HRESULT REGDB_E_CLASSNOTREG = new HRESULT("0x80040154", "REGDB_E_CLASSNOTREG", "Class not registered");

        /// <summary>
        /// Interface not registered
        /// </summary>
        public static HRESULT REGDB_E_IIDNOTREG = new HRESULT("0x80040155", "REGDB_E_IIDNOTREG", "Interface not registered");

        /// <summary>
        /// Threading model entry is not valid
        /// </summary>
        public static HRESULT REGDB_E_BADTHREADINGMODEL = new HRESULT("0x80040156", "REGDB_E_BADTHREADINGMODEL", "Threading model entry is not valid");

        /// <summary>
        /// CATID does not exist
        /// </summary>
        public static HRESULT CAT_E_CATIDNOEXIST = new HRESULT("0x80040160", "CAT_E_CATIDNOEXIST", "CATID does not exist");

        /// <summary>
        /// Description not found
        /// </summary>
        public static HRESULT CAT_E_NODESCRIPTION = new HRESULT("0x80040161", "CAT_E_NODESCRIPTION", "Description not found");

        /// <summary>
        /// No package in the software installation data in the Active Directory meets this criteria.
        /// </summary>
        public static HRESULT CS_E_PACKAGE_NOTFOUND = new HRESULT("0x80040164", "CS_E_PACKAGE_NOTFOUND", "No package in the software installation data in the Active Directory meets this criteria.");

        /// <summary>
        /// Deleting this will break the referential integrity of the software installation data in the Active Directory.
        /// </summary>
        public static HRESULT CS_E_NOT_DELETABLE = new HRESULT("0x80040165", "CS_E_NOT_DELETABLE", "Deleting this will break the referential integrity of the software installation data in the Active Directory.");

        /// <summary>
        /// The CLSID was not found in the software installation data in the Active Directory.
        /// </summary>
        public static HRESULT CS_E_CLASS_NOTFOUND = new HRESULT("0x80040166", "CS_E_CLASS_NOTFOUND", "The CLSID was not found in the software installation data in the Active Directory.");

        /// <summary>
        /// The software installation data in the Active Directory is corrupt.
        /// </summary>
        public static HRESULT CS_E_INVALID_VERSION = new HRESULT("0x80040167", "CS_E_INVALID_VERSION", "The software installation data in the Active Directory is corrupt.");

        /// <summary>
        /// There is no software installation data in the Active Directory.
        /// </summary>
        public static HRESULT CS_E_NO_CLASSSTORE = new HRESULT("0x80040168", "CS_E_NO_CLASSSTORE", "There is no software installation data in the Active Directory.");

        /// <summary>
        /// There is no software installation data object in the Active Directory.
        /// </summary>
        public static HRESULT CS_E_OBJECT_NOTFOUND = new HRESULT("0x80040169", "CS_E_OBJECT_NOTFOUND", "There is no software installation data object in the Active Directory.");

        /// <summary>
        /// The software installation data object in the Active Directory already exists.
        /// </summary>
        public static HRESULT CS_E_OBJECT_ALREADY_EXISTS = new HRESULT("0x8004016A", "CS_E_OBJECT_ALREADY_EXISTS", "The software installation data object in the Active Directory already exists.");

        /// <summary>
        /// The path to the software installation data in the Active Directory is not correct.
        /// </summary>
        public static HRESULT CS_E_INVALID_PATH = new HRESULT("0x8004016B", "CS_E_INVALID_PATH", "The path to the software installation data in the Active Directory is not correct.");

        /// <summary>
        /// A network error interrupted the operation.
        /// </summary>
        public static HRESULT CS_E_NETWORK_ERROR = new HRESULT("0x8004016C", "CS_E_NETWORK_ERROR", "A network error interrupted the operation.");

        /// <summary>
        /// The size of this object exceeds the maximum size set by the Administrator.
        /// </summary>
        public static HRESULT CS_E_ADMIN_LIMIT_EXCEEDED = new HRESULT("0x8004016D", "CS_E_ADMIN_LIMIT_EXCEEDED", "The size of this object exceeds the maximum size set by the Administrator.");

        /// <summary>
        /// The schema for the software installation data in the Active Directory does not match the required schema.
        /// </summary>
        public static HRESULT CS_E_SCHEMA_MISMATCH = new HRESULT("0x8004016E", "CS_E_SCHEMA_MISMATCH", "The schema for the software installation data in the Active Directory does not match the required schema.");

        /// <summary>
        /// An error occurred in the software installation data in the Active Directory.
        /// </summary>
        public static HRESULT CS_E_INTERNAL_ERROR = new HRESULT("0x8004016F", "CS_E_INTERNAL_ERROR", "An error occurred in the software installation data in the Active Directory.");

        /// <summary>
        /// Cache not updated
        /// </summary>
        public static HRESULT CACHE_E_NOCACHE_UPDATED = new HRESULT("0x80040170", "CACHE_E_NOCACHE_UPDATED", "Cache not updated");

        /// <summary>
        /// No verbs for OLE object
        /// </summary>
        public static HRESULT OLEOBJ_E_NOVERBS = new HRESULT("0x80040180", "OLEOBJ_E_NOVERBS", "No verbs for OLE object");

        /// <summary>
        /// Invalid verb for OLE object
        /// </summary>
        public static HRESULT OLEOBJ_E_INVALIDVERB = new HRESULT("0x80040181", "OLEOBJ_E_INVALIDVERB", "Invalid verb for OLE object");

        /// <summary>
        /// Undo is not available
        /// </summary>
        public static HRESULT INPLACE_E_NOTUNDOABLE = new HRESULT("0x800401A0", "INPLACE_E_NOTUNDOABLE", "Undo is not available");

        /// <summary>
        /// Space for tools is not available
        /// </summary>
        public static HRESULT INPLACE_E_NOTOOLSPACE = new HRESULT("0x800401A1", "INPLACE_E_NOTOOLSPACE", "Space for tools is not available");

        /// <summary>
        /// OLESTREAM Get method failed
        /// </summary>
        public static HRESULT CONVERT10_E_OLESTREAM_GET = new HRESULT("0x800401C0", "CONVERT10_E_OLESTREAM_GET", "OLESTREAM Get method failed");

        /// <summary>
        /// OLESTREAM Put method failed
        /// </summary>
        public static HRESULT CONVERT10_E_OLESTREAM_PUT = new HRESULT("0x800401C1", "CONVERT10_E_OLESTREAM_PUT", "OLESTREAM Put method failed");

        /// <summary>
        /// Contents of the OLESTREAM not in correct format
        /// </summary>
        public static HRESULT CONVERT10_E_OLESTREAM_FMT = new HRESULT("0x800401C2", "CONVERT10_E_OLESTREAM_FMT", "Contents of the OLESTREAM not in correct format");

        /// <summary>
        /// There was an error in a Windows GDI call while converting the bitmap to a DIB
        /// </summary>
        public static HRESULT CONVERT10_E_OLESTREAM_BITMAP_TO_DIB = new HRESULT("0x800401C3", "CONVERT10_E_OLESTREAM_BITMAP_TO_DIB", "There was an error in a Windows GDI call while converting the bitmap to a DIB");

        /// <summary>
        /// Contents of the IStorage not in correct format
        /// </summary>
        public static HRESULT CONVERT10_E_STG_FMT = new HRESULT("0x800401C4", "CONVERT10_E_STG_FMT", "Contents of the IStorage not in correct format");

        /// <summary>
        /// Contents of IStorage is missing one of the standard streams
        /// </summary>
        public static HRESULT CONVERT10_E_STG_NO_STD_STREAM = new HRESULT("0x800401C5", "CONVERT10_E_STG_NO_STD_STREAM", "Contents of IStorage is missing one of the standard streams");

        /// <summary>
        /// There was an error in a Windows GDI call while converting the DIB to a bitmap.
        /// </summary>
        public static HRESULT CONVERT10_E_STG_DIB_TO_BITMAP = new HRESULT("0x800401C6", "CONVERT10_E_STG_DIB_TO_BITMAP", "There was an error in a Windows GDI call while converting the DIB to a bitmap.");

        /// <summary>
        /// OpenClipboard Failed
        /// </summary>
        public static HRESULT CLIPBRD_E_CANT_OPEN = new HRESULT("0x800401D0", "CLIPBRD_E_CANT_OPEN", "OpenClipboard Failed");

        /// <summary>
        /// EmptyClipboard Failed
        /// </summary>
        public static HRESULT CLIPBRD_E_CANT_EMPTY = new HRESULT("0x800401D1", "CLIPBRD_E_CANT_EMPTY", "EmptyClipboard Failed");

        /// <summary>
        /// SetClipboard Failed
        /// </summary>
        public static HRESULT CLIPBRD_E_CANT_SET = new HRESULT("0x800401D2", "CLIPBRD_E_CANT_SET", "SetClipboard Failed");

        /// <summary>
        /// Data on clipboard is invalid
        /// </summary>
        public static HRESULT CLIPBRD_E_BAD_DATA = new HRESULT("0x800401D3", "CLIPBRD_E_BAD_DATA", "Data on clipboard is invalid");

        /// <summary>
        /// CloseClipboard Failed
        /// </summary>
        public static HRESULT CLIPBRD_E_CANT_CLOSE = new HRESULT("0x800401D4", "CLIPBRD_E_CANT_CLOSE", "CloseClipboard Failed");

        /// <summary>
        /// Moniker needs to be connected manually
        /// </summary>
        public static HRESULT MK_E_CONNECTMANUALLY = new HRESULT("0x800401E0", "MK_E_CONNECTMANUALLY", "Moniker needs to be connected manually");

        /// <summary>
        /// Operation exceeded deadline
        /// </summary>
        public static HRESULT MK_E_EXCEEDEDDEADLINE = new HRESULT("0x800401E1", "MK_E_EXCEEDEDDEADLINE", "Operation exceeded deadline");

        /// <summary>
        /// Moniker needs to be generic
        /// </summary>
        public static HRESULT MK_E_NEEDGENERIC = new HRESULT("0x800401E2", "MK_E_NEEDGENERIC", "Moniker needs to be generic");

        /// <summary>
        /// Operation unavailable
        /// </summary>
        public static HRESULT MK_E_UNAVAILABLE = new HRESULT("0x800401E3", "MK_E_UNAVAILABLE", "Operation unavailable");

        /// <summary>
        /// Invalid syntax
        /// </summary>
        public static HRESULT MK_E_SYNTAX = new HRESULT("0x800401E4", "MK_E_SYNTAX", "Invalid syntax");

        /// <summary>
        /// No object for moniker
        /// </summary>
        public static HRESULT MK_E_NOOBJECT = new HRESULT("0x800401E5", "MK_E_NOOBJECT", "No object for moniker");

        /// <summary>
        /// Bad extension for file
        /// </summary>
        public static HRESULT MK_E_INVALIDEXTENSION = new HRESULT("0x800401E6", "MK_E_INVALIDEXTENSION", "Bad extension for file");

        /// <summary>
        /// Intermediate operation failed
        /// </summary>
        public static HRESULT MK_E_INTERMEDIATEINTERFACENOTSUPPORTED = new HRESULT("0x800401E7", "MK_E_INTERMEDIATEINTERFACENOTSUPPORTED", "Intermediate operation failed");

        /// <summary>
        /// Moniker is not bindable
        /// </summary>
        public static HRESULT MK_E_NOTBINDABLE = new HRESULT("0x800401E8", "MK_E_NOTBINDABLE", "Moniker is not bindable");

        /// <summary>
        /// Moniker is not bound
        /// </summary>
        public static HRESULT MK_E_NOTBOUND = new HRESULT("0x800401E9", "MK_E_NOTBOUND", "Moniker is not bound");

        /// <summary>
        /// Moniker cannot open file
        /// </summary>
        public static HRESULT MK_E_CANTOPENFILE = new HRESULT("0x800401EA", "MK_E_CANTOPENFILE", "Moniker cannot open file");

        /// <summary>
        /// User input required for operation to succeed
        /// </summary>
        public static HRESULT MK_E_MUSTBOTHERUSER = new HRESULT("0x800401EB", "MK_E_MUSTBOTHERUSER", "User input required for operation to succeed");

        /// <summary>
        /// Moniker class has no inverse
        /// </summary>
        public static HRESULT MK_E_NOINVERSE = new HRESULT("0x800401EC", "MK_E_NOINVERSE", "Moniker class has no inverse");

        /// <summary>
        /// Moniker does not refer to storage
        /// </summary>
        public static HRESULT MK_E_NOSTORAGE = new HRESULT("0x800401ED", "MK_E_NOSTORAGE", "Moniker does not refer to storage");

        /// <summary>
        /// No common prefix
        /// </summary>
        public static HRESULT MK_E_NOPREFIX = new HRESULT("0x800401EE", "MK_E_NOPREFIX", "No common prefix");

        /// <summary>
        /// Moniker could not be enumerated
        /// </summary>
        public static HRESULT MK_E_ENUMERATION_FAILED = new HRESULT("0x800401EF", "MK_E_ENUMERATION_FAILED", "Moniker could not be enumerated");

        /// <summary>
        /// CoInitialize has not been called.
        /// </summary>
        public static HRESULT CO_E_NOTINITIALIZED = new HRESULT("0x800401F0", "CO_E_NOTINITIALIZED", "CoInitialize has not been called.");

        /// <summary>
        /// CoInitialize has already been called.
        /// </summary>
        public static HRESULT CO_E_ALREADYINITIALIZED = new HRESULT("0x800401F1", "CO_E_ALREADYINITIALIZED", "CoInitialize has already been called.");

        /// <summary>
        /// Class of object cannot be determined
        /// </summary>
        public static HRESULT CO_E_CANTDETERMINECLASS = new HRESULT("0x800401F2", "CO_E_CANTDETERMINECLASS", "Class of object cannot be determined");

        /// <summary>
        /// Invalid class string
        /// </summary>
        public static HRESULT CO_E_CLASSSTRING = new HRESULT("0x800401F3", "CO_E_CLASSSTRING", "Invalid class string");

        /// <summary>
        /// Invalid interface string
        /// </summary>
        public static HRESULT CO_E_IIDSTRING = new HRESULT("0x800401F4", "CO_E_IIDSTRING", "Invalid interface string");

        /// <summary>
        /// Application not found
        /// </summary>
        public static HRESULT CO_E_APPNOTFOUND = new HRESULT("0x800401F5", "CO_E_APPNOTFOUND", "Application not found");

        /// <summary>
        /// Application cannot be run more than once
        /// </summary>
        public static HRESULT CO_E_APPSINGLEUSE = new HRESULT("0x800401F6", "CO_E_APPSINGLEUSE", "Application cannot be run more than once");

        /// <summary>
        /// Some error in application program
        /// </summary>
        public static HRESULT CO_E_ERRORINAPP = new HRESULT("0x800401F7", "CO_E_ERRORINAPP", "Some error in application program");

        /// <summary>
        /// DLL for class not found
        /// </summary>
        public static HRESULT CO_E_DLLNOTFOUND = new HRESULT("0x800401F8", "CO_E_DLLNOTFOUND", "DLL for class not found");

        /// <summary>
        /// Error in the DLL
        /// </summary>
        public static HRESULT CO_E_ERRORINDLL = new HRESULT("0x800401F9", "CO_E_ERRORINDLL", "Error in the DLL");

        /// <summary>
        /// Wrong operating system or operating system version for the application
        /// </summary>
        public static HRESULT CO_E_WRONGOSFORAPP = new HRESULT("0x800401FA", "CO_E_WRONGOSFORAPP", "Wrong operating system or operating system version for the application");

        /// <summary>
        /// Object is not registered
        /// </summary>
        public static HRESULT CO_E_OBJNOTREG = new HRESULT("0x800401FB", "CO_E_OBJNOTREG", "Object is not registered");

        /// <summary>
        /// Object is already registered
        /// </summary>
        public static HRESULT CO_E_OBJISREG = new HRESULT("0x800401FC", "CO_E_OBJISREG", "Object is already registered");

        /// <summary>
        /// Object is not connected to server
        /// </summary>
        public static HRESULT CO_E_OBJNOTCONNECTED = new HRESULT("0x800401FD", "CO_E_OBJNOTCONNECTED", "Object is not connected to server");

        /// <summary>
        /// Application was launched but it didn't register a class factory
        /// </summary>
        public static HRESULT CO_E_APPDIDNTREG = new HRESULT("0x800401FE", "CO_E_APPDIDNTREG", "Application was launched but it didn't register a class factory");

        /// <summary>
        /// Object has been released
        /// </summary>
        public static HRESULT CO_E_RELEASED = new HRESULT("0x800401FF", "CO_E_RELEASED", "Object has been released");

        /// <summary>
        /// An event was able to invoke some but not all of the subscribers
        /// </summary>
        public static HRESULT EVENT_S_SOME_SUBSCRIBERS_FAILED = new HRESULT("0x00040200", "EVENT_S_SOME_SUBSCRIBERS_FAILED", "An event was able to invoke some but not all of the subscribers");

        /// <summary>
        /// An event was unable to invoke any of the subscribers
        /// </summary>
        public static HRESULT EVENT_E_ALL_SUBSCRIBERS_FAILED = new HRESULT("0x80040201", "EVENT_E_ALL_SUBSCRIBERS_FAILED", "An event was unable to invoke any of the subscribers");

        /// <summary>
        /// An event was delivered but there were no subscribers
        /// </summary>
        public static HRESULT EVENT_S_NOSUBSCRIBERS = new HRESULT("0x00040202", "EVENT_S_NOSUBSCRIBERS", "An event was delivered but there were no subscribers");

        /// <summary>
        /// A syntax error occurred trying to evaluate a query string
        /// </summary>
        public static HRESULT EVENT_E_QUERYSYNTAX = new HRESULT("0x80040203", "EVENT_E_QUERYSYNTAX", "A syntax error occurred trying to evaluate a query string");

        /// <summary>
        /// An invalid field name was used in a query string
        /// </summary>
        public static HRESULT EVENT_E_QUERYFIELD = new HRESULT("0x80040204", "EVENT_E_QUERYFIELD", "An invalid field name was used in a query string");

        /// <summary>
        /// An unexpected exception was raised
        /// </summary>
        public static HRESULT EVENT_E_INTERNALEXCEPTION = new HRESULT("0x80040205", "EVENT_E_INTERNALEXCEPTION", "An unexpected exception was raised");

        /// <summary>
        /// An unexpected internal error was detected
        /// </summary>
        public static HRESULT EVENT_E_INTERNALERROR = new HRESULT("0x80040206", "EVENT_E_INTERNALERROR", "An unexpected internal error was detected");

        /// <summary>
        /// The owner SID on a per-user subscription doesn't exist
        /// </summary>
        public static HRESULT EVENT_E_INVALID_PER_USER_SID = new HRESULT("0x80040207", "EVENT_E_INVALID_PER_USER_SID", "The owner SID on a per-user subscription doesn't exist");

        /// <summary>
        /// A user-supplied component or subscriber raised an exception
        /// </summary>
        public static HRESULT EVENT_E_USER_EXCEPTION = new HRESULT("0x80040208", "EVENT_E_USER_EXCEPTION", "A user-supplied component or subscriber raised an exception");

        /// <summary>
        /// An interface has too many methods to fire events from
        /// </summary>
        public static HRESULT EVENT_E_TOO_MANY_METHODS = new HRESULT("0x80040209", "EVENT_E_TOO_MANY_METHODS", "An interface has too many methods to fire events from");

        /// <summary>
        /// A subscription cannot be stored unless its event class already exists
        /// </summary>
        public static HRESULT EVENT_E_MISSING_EVENTCLASS = new HRESULT("0x8004020A", "EVENT_E_MISSING_EVENTCLASS", "A subscription cannot be stored unless its event class already exists");

        /// <summary>
        /// Not all the objects requested could be removed
        /// </summary>
        public static HRESULT EVENT_E_NOT_ALL_REMOVED = new HRESULT("0x8004020B", "EVENT_E_NOT_ALL_REMOVED", "Not all the objects requested could be removed");

        /// <summary>
        /// COM+ is required for this operation, but is not installed
        /// </summary>
        public static HRESULT EVENT_E_COMPLUS_NOT_INSTALLED = new HRESULT("0x8004020C", "EVENT_E_COMPLUS_NOT_INSTALLED", "COM+ is required for this operation, but is not installed");

        /// <summary>
        /// Cannot modify or delete an object that was not added using the COM+ Admin SDK
        /// </summary>
        public static HRESULT EVENT_E_CANT_MODIFY_OR_DELETE_UNCONFIGURED_OBJECT = new HRESULT("0x8004020D", "EVENT_E_CANT_MODIFY_OR_DELETE_UNCONFIGURED_OBJECT", "Cannot modify or delete an object that was not added using the COM+ Admin SDK");

        /// <summary>
        /// Cannot modify or delete an object that was added using the COM+ Admin SDK
        /// </summary>
        public static HRESULT EVENT_E_CANT_MODIFY_OR_DELETE_CONFIGURED_OBJECT = new HRESULT("0x8004020E", "EVENT_E_CANT_MODIFY_OR_DELETE_CONFIGURED_OBJECT", "Cannot modify or delete an object that was added using the COM+ Admin SDK");

        /// <summary>
        /// The event class for this subscription is in an invalid partition
        /// </summary>
        public static HRESULT EVENT_E_INVALID_EVENT_CLASS_PARTITION = new HRESULT("0x8004020F", "EVENT_E_INVALID_EVENT_CLASS_PARTITION", "The event class for this subscription is in an invalid partition");
        #endregion

        #region COM Error Codes (XACT, SCHED, OLE)
        /// <summary>
        /// Another single phase resource manager has already been enlisted in this transaction.
        /// </summary>
        public static HRESULT XACT_E_ALREADYOTHERSINGLEPHASE = new HRESULT("0x8004D000", "XACT_E_ALREADYOTHERSINGLEPHASE", "Another single phase resource manager has already been enlisted in this transaction.");

        /// <summary>
        /// A retaining commit or abort is not supported
        /// </summary>
        public static HRESULT XACT_E_CANTRETAIN = new HRESULT("0x8004D001", "XACT_E_CANTRETAIN", "A retaining commit or abort is not supported");

        /// <summary>
        /// The transaction failed to commit for an unknown reason. The transaction was aborted.
        /// </summary>
        public static HRESULT XACT_E_COMMITFAILED = new HRESULT("0x8004D002", "XACT_E_COMMITFAILED", "The transaction failed to commit for an unknown reason. The transaction was aborted.");

        /// <summary>
        /// Cannot call commit on this transaction object because the calling application did not initiate the transaction.
        /// </summary>
        public static HRESULT XACT_E_COMMITPREVENTED = new HRESULT("0x8004D003", "XACT_E_COMMITPREVENTED", "Cannot call commit on this transaction object because the calling application did not initiate the transaction.");

        /// <summary>
        /// Instead of committing, the resource heuristically aborted.
        /// </summary>
        public static HRESULT XACT_E_HEURISTICABORT = new HRESULT("0x8004D004", "XACT_E_HEURISTICABORT", "Instead of committing, the resource heuristically aborted.");

        /// <summary>
        /// Instead of aborting, the resource heuristically committed.
        /// </summary>
        public static HRESULT XACT_E_HEURISTICCOMMIT = new HRESULT("0x8004D005", "XACT_E_HEURISTICCOMMIT", "Instead of aborting, the resource heuristically committed.");

        /// <summary>
        /// Some of the states of the resource were committed while others were aborted, likely because of heuristic decisions.
        /// </summary>
        public static HRESULT XACT_E_HEURISTICDAMAGE = new HRESULT("0x8004D006", "XACT_E_HEURISTICDAMAGE", "Some of the states of the resource were committed while others were aborted, likely because of heuristic decisions.");

        /// <summary>
        /// Some of the states of the resource may have been committed while others may have been aborted, likely because of heuristic decisions.
        /// </summary>
        public static HRESULT XACT_E_HEURISTICDANGER = new HRESULT("0x8004D007", "XACT_E_HEURISTICDANGER", "Some of the states of the resource may have been committed while others may have been aborted, likely because of heuristic decisions.");

        /// <summary>
        /// The requested isolation level is not valid or supported.
        /// </summary>
        public static HRESULT XACT_E_ISOLATIONLEVEL = new HRESULT("0x8004D008", "XACT_E_ISOLATIONLEVEL", "The requested isolation level is not valid or supported.");

        /// <summary>
        /// The transaction manager doesn't support an asynchronous operation for this method.
        /// </summary>
        public static HRESULT XACT_E_NOASYNC = new HRESULT("0x8004D009", "XACT_E_NOASYNC", "The transaction manager doesn't support an asynchronous operation for this method.");

        /// <summary>
        /// Unable to enlist in the transaction.
        /// </summary>
        public static HRESULT XACT_E_NOENLIST = new HRESULT("0x8004D00A", "XACT_E_NOENLIST", "Unable to enlist in the transaction.");

        /// <summary>
        /// The requested semantics of retention of isolation across retaining commit and abort boundaries cannot be supported by this transaction implementation, or isoFlags was not equal to zero.
        /// </summary>
        public static HRESULT XACT_E_NOISORETAIN = new HRESULT("0x8004D00B", "XACT_E_NOISORETAIN", "The requested semantics of retention of isolation across retaining commit and abort boundaries cannot be supported by this transaction implementation, or isoFlags was not equal to zero.");

        /// <summary>
        /// There is no resource presently associated with this enlistment
        /// </summary>
        public static HRESULT XACT_E_NORESOURCE = new HRESULT("0x8004D00C", "XACT_E_NORESOURCE", "There is no resource presently associated with this enlistment");

        /// <summary>
        /// The transaction failed to commit due to the failure of optimistic concurrency control in at least one of the resource managers.
        /// </summary>
        public static HRESULT XACT_E_NOTCURRENT = new HRESULT("0x8004D00D", "XACT_E_NOTCURRENT", "The transaction failed to commit due to the failure of optimistic concurrency control in at least one of the resource managers.");

        /// <summary>
        /// The transaction has already been implicitly or explicitly committed or aborted
        /// </summary>
        public static HRESULT XACT_E_NOTRANSACTION = new HRESULT("0x8004D00E", "XACT_E_NOTRANSACTION", "The transaction has already been implicitly or explicitly committed or aborted");

        /// <summary>
        /// An invalid combination of flags was specified
        /// </summary>
        public static HRESULT XACT_E_NOTSUPPORTED = new HRESULT("0x8004D00F", "XACT_E_NOTSUPPORTED", "An invalid combination of flags was specified");

        /// <summary>
        /// The resource manager id is not associated with this transaction or the transaction manager.
        /// </summary>
        public static HRESULT XACT_E_UNKNOWNRMGRID = new HRESULT("0x8004D010", "XACT_E_UNKNOWNRMGRID", "The resource manager id is not associated with this transaction or the transaction manager.");

        /// <summary>
        /// This method was called in the wrong state
        /// </summary>
        public static HRESULT XACT_E_WRONGSTATE = new HRESULT("0x8004D011", "XACT_E_WRONGSTATE", "This method was called in the wrong state");

        /// <summary>
        /// The indicated unit of work does not match the unit of work expected by the resource manager.
        /// </summary>
        public static HRESULT XACT_E_WRONGUOW = new HRESULT("0x8004D012", "XACT_E_WRONGUOW", "The indicated unit of work does not match the unit of work expected by the resource manager.");

        /// <summary>
        /// An enlistment in a transaction already exists.
        /// </summary>
        public static HRESULT XACT_E_XTIONEXISTS = new HRESULT("0x8004D013", "XACT_E_XTIONEXISTS", "An enlistment in a transaction already exists.");

        /// <summary>
        /// An import object for the transaction could not be found.
        /// </summary>
        public static HRESULT XACT_E_NOIMPORTOBJECT = new HRESULT("0x8004D014", "XACT_E_NOIMPORTOBJECT", "An import object for the transaction could not be found.");

        /// <summary>
        /// The transaction cookie is invalid.
        /// </summary>
        public static HRESULT XACT_E_INVALIDCOOKIE = new HRESULT("0x8004D015", "XACT_E_INVALIDCOOKIE", "The transaction cookie is invalid.");

        /// <summary>
        /// The transaction status is in doubt. A communication failure occurred, or a transaction manager or resource manager has failed
        /// </summary>
        public static HRESULT XACT_E_INDOUBT = new HRESULT("0x8004D016", "XACT_E_INDOUBT", "The transaction status is in doubt. A communication failure occurred, or a transaction manager or resource manager has failed");

        /// <summary>
        /// A time-out was specified, but time-outs are not supported.
        /// </summary>
        public static HRESULT XACT_E_NOTIMEOUT = new HRESULT("0x8004D017", "XACT_E_NOTIMEOUT", "A time-out was specified, but time-outs are not supported.");

        /// <summary>
        /// The requested operation is already in progress for the transaction.
        /// </summary>
        public static HRESULT XACT_E_ALREADYINPROGRESS = new HRESULT("0x8004D018", "XACT_E_ALREADYINPROGRESS", "The requested operation is already in progress for the transaction.");

        /// <summary>
        /// The transaction has already been aborted.
        /// </summary>
        public static HRESULT XACT_E_ABORTED = new HRESULT("0x8004D019", "XACT_E_ABORTED", "The transaction has already been aborted.");

        /// <summary>
        /// The Transaction Manager returned a log full error.
        /// </summary>
        public static HRESULT XACT_E_LOGFULL = new HRESULT("0x8004D01A", "XACT_E_LOGFULL", "The Transaction Manager returned a log full error.");

        /// <summary>
        /// The Transaction Manager is not available.
        /// </summary>
        public static HRESULT XACT_E_TMNOTAVAILABLE = new HRESULT("0x8004D01B", "XACT_E_TMNOTAVAILABLE", "The Transaction Manager is not available.");

        /// <summary>
        /// A connection with the transaction manager was lost.
        /// </summary>
        public static HRESULT XACT_E_CONNECTION_DOWN = new HRESULT("0x8004D01C", "XACT_E_CONNECTION_DOWN", "A connection with the transaction manager was lost.");

        /// <summary>
        /// A request to establish a connection with the transaction manager was denied.
        /// </summary>
        public static HRESULT XACT_E_CONNECTION_DENIED = new HRESULT("0x8004D01D", "XACT_E_CONNECTION_DENIED", "A request to establish a connection with the transaction manager was denied.");

        /// <summary>
        /// Resource manager reenlistment to determine transaction status timed out.
        /// </summary>
        public static HRESULT XACT_E_REENLISTTIMEOUT = new HRESULT("0x8004D01E", "XACT_E_REENLISTTIMEOUT", "Resource manager reenlistment to determine transaction status timed out.");

        /// <summary>
        /// This transaction manager failed to establish a connection with another TIP transaction manager.
        /// </summary>
        public static HRESULT XACT_E_TIP_CONNECT_FAILED = new HRESULT("0x8004D01F", "XACT_E_TIP_CONNECT_FAILED", "This transaction manager failed to establish a connection with another TIP transaction manager.");

        /// <summary>
        /// This transaction manager encountered a protocol error with another TIP transaction manager.
        /// </summary>
        public static HRESULT XACT_E_TIP_PROTOCOL_ERROR = new HRESULT("0x8004D020", "XACT_E_TIP_PROTOCOL_ERROR", "This transaction manager encountered a protocol error with another TIP transaction manager.");

        /// <summary>
        /// This transaction manager could not propagate a transaction from another TIP transaction manager.
        /// </summary>
        public static HRESULT XACT_E_TIP_PULL_FAILED = new HRESULT("0x8004D021", "XACT_E_TIP_PULL_FAILED", "This transaction manager could not propagate a transaction from another TIP transaction manager.");

        /// <summary>
        /// The Transaction Manager on the destination machine is not available.
        /// </summary>
        public static HRESULT XACT_E_DEST_TMNOTAVAILABLE = new HRESULT("0x8004D022", "XACT_E_DEST_TMNOTAVAILABLE", "The Transaction Manager on the destination machine is not available.");

        /// <summary>
        /// The Transaction Manager has disabled its support for TIP.
        /// </summary>
        public static HRESULT XACT_E_TIP_DISABLED = new HRESULT("0x8004D023", "XACT_E_TIP_DISABLED", "The Transaction Manager has disabled its support for TIP.");

        /// <summary>
        /// The transaction manager has disabled its support for remote/network transactions.
        /// </summary>
        public static HRESULT XACT_E_NETWORK_TX_DISABLED = new HRESULT("0x8004D024", "XACT_E_NETWORK_TX_DISABLED", "The transaction manager has disabled its support for remote/network transactions.");

        /// <summary>
        /// The partner transaction manager has disabled its support for remote/network transactions.
        /// </summary>
        public static HRESULT XACT_E_PARTNER_NETWORK_TX_DISABLED = new HRESULT("0x8004D025", "XACT_E_PARTNER_NETWORK_TX_DISABLED", "The partner transaction manager has disabled its support for remote/network transactions.");

        /// <summary>
        /// The transaction manager has disabled its support for XA transactions.
        /// </summary>
        public static HRESULT XACT_E_XA_TX_DISABLED = new HRESULT("0x8004D026", "XACT_E_XA_TX_DISABLED", "The transaction manager has disabled its support for XA transactions.");

        /// <summary>
        /// MSDTC was unable to read its configuration information.
        /// </summary>
        public static HRESULT XACT_E_UNABLE_TO_READ_DTC_CONFIG = new HRESULT("0x8004D027", "XACT_E_UNABLE_TO_READ_DTC_CONFIG", "MSDTC was unable to read its configuration information.");

        /// <summary>
        /// MSDTC was unable to load the dtc proxy dll.
        /// </summary>
        public static HRESULT XACT_E_UNABLE_TO_LOAD_DTC_PROXY = new HRESULT("0x8004D028", "XACT_E_UNABLE_TO_LOAD_DTC_PROXY", "MSDTC was unable to load the dtc proxy dll.");

        /// <summary>
        /// The local transaction has aborted.
        /// </summary>
        public static HRESULT XACT_E_ABORTING = new HRESULT("0x8004D029", "XACT_E_ABORTING", "The local transaction has aborted.");

        /// <summary>
        /// The MSDTC transaction manager was unable to push the transaction to the destination transaction manager due to communication problems. Possible causes are: a firewall is present and it doesn't have an exception for the MSDTC process, the two machines cannot find each other by their NetBIOS names, or the support for network transactions is not enabled for one of the two transaction managers.
        /// </summary>
        public static HRESULT XACT_E_PUSH_COMM_FAILURE = new HRESULT("0x8004D02A", "XACT_E_PUSH_COMM_FAILURE", "The MSDTC transaction manager was unable to push the transaction to the destination transaction manager due to communication problems. Possible causes are: a firewall is present and it doesn't have an exception for the MSDTC process, the two machines cannot find each other by their NetBIOS names, or the support for network transactions is not enabled for one of the two transaction managers.");

        /// <summary>
        /// The MSDTC transaction manager was unable to pull the transaction from the source transaction manager due to communication problems. Possible causes are: a firewall is present and it doesn't have an exception for the MSDTC process, the two machines cannot find each other by their NetBIOS names, or the support for network transactions is not enabled for one of the two transaction managers.
        /// </summary>
        public static HRESULT XACT_E_PULL_COMM_FAILURE = new HRESULT("0x8004D02B", "XACT_E_PULL_COMM_FAILURE", "The MSDTC transaction manager was unable to pull the transaction from the source transaction manager due to communication problems. Possible causes are: a firewall is present and it doesn't have an exception for the MSDTC process, the two machines cannot find each other by their NetBIOS names, or the support for network transactions is not enabled for one of the two transaction managers.");

        /// <summary>
        /// The MSDTC transaction manager has disabled its support for SNA LU 6.2 transactions.
        /// </summary>
        public static HRESULT XACT_E_LU_TX_DISABLED = new HRESULT("0x8004D02C", "XACT_E_LU_TX_DISABLED", "The MSDTC transaction manager has disabled its support for SNA LU 6.2 transactions.");

        /// <summary>
        /// XACT_E_CLERKNOTFOUND
        /// </summary>
        public static HRESULT XACT_E_CLERKNOTFOUND = new HRESULT("0x8004D080", "XACT_E_CLERKNOTFOUND", "XACT_E_CLERKNOTFOUND");

        /// <summary>
        /// XACT_E_CLERKEXISTS
        /// </summary>
        public static HRESULT XACT_E_CLERKEXISTS = new HRESULT("0x8004D081", "XACT_E_CLERKEXISTS", "XACT_E_CLERKEXISTS");

        /// <summary>
        /// XACT_E_RECOVERYINPROGRESS
        /// </summary>
        public static HRESULT XACT_E_RECOVERYINPROGRESS = new HRESULT("0x8004D082", "XACT_E_RECOVERYINPROGRESS", "XACT_E_RECOVERYINPROGRESS");

        /// <summary>
        /// XACT_E_TRANSACTIONCLOSED
        /// </summary>
        public static HRESULT XACT_E_TRANSACTIONCLOSED = new HRESULT("0x8004D083", "XACT_E_TRANSACTIONCLOSED", "XACT_E_TRANSACTIONCLOSED");

        /// <summary>
        /// XACT_E_INVALIDLSN
        /// </summary>
        public static HRESULT XACT_E_INVALIDLSN = new HRESULT("0x8004D084", "XACT_E_INVALIDLSN", "XACT_E_INVALIDLSN");

        /// <summary>
        /// XACT_E_REPLAYREQUEST
        /// </summary>
        public static HRESULT XACT_E_REPLAYREQUEST = new HRESULT("0x8004D085", "XACT_E_REPLAYREQUEST", "XACT_E_REPLAYREQUEST");

        /// <summary>
        /// An asynchronous operation was specified. The operation has begun, but its outcome is not known yet.
        /// </summary>
        public static HRESULT XACT_S_ASYNC = new HRESULT("0x0004D000", "XACT_S_ASYNC", "An asynchronous operation was specified. The operation has begun, but its outcome is not known yet.");

        /// <summary>
        /// XACT_S_DEFECT
        /// </summary>
        public static HRESULT XACT_S_DEFECT = new HRESULT("0x0004D001", "XACT_S_DEFECT", "XACT_S_DEFECT");

        /// <summary>
        /// The method call succeeded because the transaction was read-only.
        /// </summary>
        public static HRESULT XACT_S_READONLY = new HRESULT("0x0004D002", "XACT_S_READONLY", "The method call succeeded because the transaction was read-only.");

        /// <summary>
        /// The transaction was successfully aborted. However, this is a coordinated transaction, and some number of enlisted resources were aborted outright because they could not support abort-retaining semantics
        /// </summary>
        public static HRESULT XACT_S_SOMENORETAIN = new HRESULT("0x0004D003", "XACT_S_SOMENORETAIN", "The transaction was successfully aborted. However, this is a coordinated transaction, and some number of enlisted resources were aborted outright because they could not support abort-retaining semantics");

        /// <summary>
        /// No changes were made during this call, but the sink wants another chance to look if any other sinks make further changes.
        /// </summary>
        public static HRESULT XACT_S_OKINFORM = new HRESULT("0x0004D004", "XACT_S_OKINFORM", "No changes were made during this call, but the sink wants another chance to look if any other sinks make further changes.");

        /// <summary>
        /// The sink is content and wishes the transaction to proceed. Changes were made to one or more resources during this call.
        /// </summary>
        public static HRESULT XACT_S_MADECHANGESCONTENT = new HRESULT("0x0004D005", "XACT_S_MADECHANGESCONTENT", "The sink is content and wishes the transaction to proceed. Changes were made to one or more resources during this call.");

        /// <summary>
        /// The sink is for the moment and wishes the transaction to proceed, but if other changes are made following this return by other event sinks then this sink wants another chance to look
        /// </summary>
        public static HRESULT XACT_S_MADECHANGESINFORM = new HRESULT("0x0004D006", "XACT_S_MADECHANGESINFORM", "The sink is for the moment and wishes the transaction to proceed, but if other changes are made following this return by other event sinks then this sink wants another chance to look");

        /// <summary>
        /// The transaction was successfully aborted. However, the abort was non-retaining.
        /// </summary>
        public static HRESULT XACT_S_ALLNORETAIN = new HRESULT("0x0004D007", "XACT_S_ALLNORETAIN", "The transaction was successfully aborted. However, the abort was non-retaining.");

        /// <summary>
        /// An abort operation was already in progress.
        /// </summary>
        public static HRESULT XACT_S_ABORTING = new HRESULT("0x0004D008", "XACT_S_ABORTING", "An abort operation was already in progress.");

        /// <summary>
        /// The resource manager has performed a single-phase commit of the transaction.
        /// </summary>
        public static HRESULT XACT_S_SINGLEPHASE = new HRESULT("0x0004D009", "XACT_S_SINGLEPHASE", "The resource manager has performed a single-phase commit of the transaction.");

        /// <summary>
        /// The local transaction has not aborted.
        /// </summary>
        public static HRESULT XACT_S_LOCALLY_OK = new HRESULT("0x0004D00A", "XACT_S_LOCALLY_OK", "The local transaction has not aborted.");

        /// <summary>
        /// The resource manager has requested to be the coordinator (last resource manager) for the transaction.
        /// </summary>
        public static HRESULT XACT_S_LASTRESOURCEMANAGER = new HRESULT("0x0004D010", "XACT_S_LASTRESOURCEMANAGER", "The resource manager has requested to be the coordinator (last resource manager) for the transaction.");

        /// <summary>
        /// The root transaction wanted to commit, but transaction aborted
        /// </summary>
        public static HRESULT CONTEXT_E_ABORTED = new HRESULT("0x8004E002", "CONTEXT_E_ABORTED", "The root transaction wanted to commit, but transaction aborted");

        /// <summary>
        /// You made a method call on a COM+ component that has a transaction that has already aborted or in the process of aborting.
        /// </summary>
        public static HRESULT CONTEXT_E_ABORTING = new HRESULT("0x8004E003", "CONTEXT_E_ABORTING", "You made a method call on a COM+ component that has a transaction that has already aborted or in the process of aborting.");

        /// <summary>
        /// There is no MTS object context
        /// </summary>
        public static HRESULT CONTEXT_E_NOCONTEXT = new HRESULT("0x8004E004", "CONTEXT_E_NOCONTEXT", "There is no MTS object context");

        /// <summary>
        /// The component is configured to use synchronization and this method call would cause a deadlock to occur.
        /// </summary>
        public static HRESULT CONTEXT_E_WOULD_DEADLOCK = new HRESULT("0x8004E005", "CONTEXT_E_WOULD_DEADLOCK", "The component is configured to use synchronization and this method call would cause a deadlock to occur.");

        /// <summary>
        /// The component is configured to use synchronization and a thread has timed out waiting to enter the context.
        /// </summary>
        public static HRESULT CONTEXT_E_SYNCH_TIMEOUT = new HRESULT("0x8004E006", "CONTEXT_E_SYNCH_TIMEOUT", "The component is configured to use synchronization and a thread has timed out waiting to enter the context.");

        /// <summary>
        /// You made a method call on a COM+ component that has a transaction that has already committed or aborted.
        /// </summary>
        public static HRESULT CONTEXT_E_OLDREF = new HRESULT("0x8004E007", "CONTEXT_E_OLDREF", "You made a method call on a COM+ component that has a transaction that has already committed or aborted.");

        /// <summary>
        /// The specified role was not configured for the application
        /// </summary>
        public static HRESULT CONTEXT_E_ROLENOTFOUND = new HRESULT("0x8004E00C", "CONTEXT_E_ROLENOTFOUND", "The specified role was not configured for the application");

        /// <summary>
        /// COM+ was unable to talk to the Microsoft Distributed Transaction Coordinator
        /// </summary>
        public static HRESULT CONTEXT_E_TMNOTAVAILABLE = new HRESULT("0x8004E00F", "CONTEXT_E_TMNOTAVAILABLE", "COM+ was unable to talk to the Microsoft Distributed Transaction Coordinator");

        /// <summary>
        /// An unexpected error occurred during COM+ Activation.
        /// </summary>
        public static HRESULT CO_E_ACTIVATIONFAILED = new HRESULT("0x8004E021", "CO_E_ACTIVATIONFAILED", "An unexpected error occurred during COM+ Activation.");

        /// <summary>
        /// COM+ Activation failed. Check the event log for more information
        /// </summary>
        public static HRESULT CO_E_ACTIVATIONFAILED_EVENTLOGGED = new HRESULT("0x8004E022", "CO_E_ACTIVATIONFAILED_EVENTLOGGED", "COM+ Activation failed. Check the event log for more information");

        /// <summary>
        /// COM+ Activation failed due to a catalog or configuration error.
        /// </summary>
        public static HRESULT CO_E_ACTIVATIONFAILED_CATALOGERROR = new HRESULT("0x8004E023", "CO_E_ACTIVATIONFAILED_CATALOGERROR", "COM+ Activation failed due to a catalog or configuration error.");

        /// <summary>
        /// COM+ activation failed because the activation could not be completed in the specified amount of time.
        /// </summary>
        public static HRESULT CO_E_ACTIVATIONFAILED_TIMEOUT = new HRESULT("0x8004E024", "CO_E_ACTIVATIONFAILED_TIMEOUT", "COM+ activation failed because the activation could not be completed in the specified amount of time.");

        /// <summary>
        /// COM+ Activation failed because an initialization function failed. Check the event log for more information.
        /// </summary>
        public static HRESULT CO_E_INITIALIZATIONFAILED = new HRESULT("0x8004E025", "CO_E_INITIALIZATIONFAILED", "COM+ Activation failed because an initialization function failed. Check the event log for more information.");

        /// <summary>
        /// The requested operation requires that JIT be in the current context and it is not
        /// </summary>
        public static HRESULT CONTEXT_E_NOJIT = new HRESULT("0x8004E026", "CONTEXT_E_NOJIT", "The requested operation requires that JIT be in the current context and it is not");

        /// <summary>
        /// The requested operation requires that the current context have a Transaction, and it does not
        /// </summary>
        public static HRESULT CONTEXT_E_NOTRANSACTION = new HRESULT("0x8004E027", "CONTEXT_E_NOTRANSACTION", "The requested operation requires that the current context have a Transaction, and it does not");

        /// <summary>
        /// The components threading model has changed after install into a COM+ Application. Please re-install component.
        /// </summary>
        public static HRESULT CO_E_THREADINGMODEL_CHANGED = new HRESULT("0x8004E028", "CO_E_THREADINGMODEL_CHANGED", "The components threading model has changed after install into a COM+ Application. Please re-install component.");

        /// <summary>
        /// IIS intrinsics not available. Start your work with IIS.
        /// </summary>
        public static HRESULT CO_E_NOIISINTRINSICS = new HRESULT("0x8004E029", "CO_E_NOIISINTRINSICS", "IIS intrinsics not available. Start your work with IIS.");

        /// <summary>
        /// An attempt to write a cookie failed.
        /// </summary>
        public static HRESULT CO_E_NOCOOKIES = new HRESULT("0x8004E02A", "CO_E_NOCOOKIES", "An attempt to write a cookie failed.");

        /// <summary>
        /// An attempt to use a database generated a database specific error.
        /// </summary>
        public static HRESULT CO_E_DBERROR = new HRESULT("0x8004E02B", "CO_E_DBERROR", "An attempt to use a database generated a database specific error.");

        /// <summary>
        /// The COM+ component you created must use object pooling to work.
        /// </summary>
        public static HRESULT CO_E_NOTPOOLED = new HRESULT("0x8004E02C", "CO_E_NOTPOOLED", "The COM+ component you created must use object pooling to work.");

        /// <summary>
        /// The COM+ component you created must use object construction to work correctly.
        /// </summary>
        public static HRESULT CO_E_NOTCONSTRUCTED = new HRESULT("0x8004E02D", "CO_E_NOTCONSTRUCTED", "The COM+ component you created must use object construction to work correctly.");

        /// <summary>
        /// The COM+ component requires synchronization, and it is not configured for it.
        /// </summary>
        public static HRESULT CO_E_NOSYNCHRONIZATION = new HRESULT("0x8004E02E", "CO_E_NOSYNCHRONIZATION", "The COM+ component requires synchronization, and it is not configured for it.");

        /// <summary>
        /// The TxIsolation Level property for the COM+ component being created is stronger than the TxIsolationLevel for the &quot;root&quot; component for the transaction. The creation failed.
        /// </summary>
        public static HRESULT CO_E_ISOLEVELMISMATCH = new HRESULT("0x8004E02F", "CO_E_ISOLEVELMISMATCH", "The TxIsolation Level property for the COM+ component being created is stronger than the TxIsolationLevel for the \"root\" component for the transaction. The creation failed.");

        /// <summary>
        /// The component attempted to make a cross-context call between invocations of EnterTransactionScopeand ExitTransactionScope. This is not allowed. Cross-context calls cannot be made while inside of a transaction scope.
        /// </summary>
        public static HRESULT CO_E_CALL_OUT_OF_TX_SCOPE_NOT_ALLOWED = new HRESULT("0x8004E030", "CO_E_CALL_OUT_OF_TX_SCOPE_NOT_ALLOWED", "The component attempted to make a cross-context call between invocations of EnterTransactionScopeand ExitTransactionScope. This is not allowed. Cross-context calls cannot be made while inside of a transaction scope.");

        /// <summary>
        /// The component made a call to EnterTransactionScope, but did not make a corresponding call to ExitTransactionScope before returning.
        /// </summary>
        public static HRESULT CO_E_EXIT_TRANSACTION_SCOPE_NOT_CALLED = new HRESULT("0x8004E031", "CO_E_EXIT_TRANSACTION_SCOPE_NOT_CALLED", "The component made a call to EnterTransactionScope, but did not make a corresponding call to ExitTransactionScope before returning.");

        /// <summary>
        /// Use the registry database to provide the requested information
        /// </summary>
        public static HRESULT OLE_S_USEREG = new HRESULT("0x00040000", "OLE_S_USEREG", "Use the registry database to provide the requested information");

        /// <summary>
        /// Success, but static
        /// </summary>
        public static HRESULT OLE_S_STATIC = new HRESULT("0x00040001", "OLE_S_STATIC", "Success, but static");

        /// <summary>
        /// Macintosh clipboard format
        /// </summary>
        public static HRESULT OLE_S_MAC_CLIPFORMAT = new HRESULT("0x00040002", "OLE_S_MAC_CLIPFORMAT", "Macintosh clipboard format");

        /// <summary>
        /// Successful drop took place
        /// </summary>
        public static HRESULT DRAGDROP_S_DROP = new HRESULT("0x00040100", "DRAGDROP_S_DROP", "Successful drop took place");

        /// <summary>
        /// Drag-drop operation canceled
        /// </summary>
        public static HRESULT DRAGDROP_S_CANCEL = new HRESULT("0x00040101", "DRAGDROP_S_CANCEL", "Drag-drop operation canceled");

        /// <summary>
        /// Use the default cursor
        /// </summary>
        public static HRESULT DRAGDROP_S_USEDEFAULTCURSORS = new HRESULT("0x00040102", "DRAGDROP_S_USEDEFAULTCURSORS", "Use the default cursor");

        /// <summary>
        /// Data has same FORMATETC
        /// </summary>
        public static HRESULT DATA_S_SAMEFORMATETC = new HRESULT("0x00040130", "DATA_S_SAMEFORMATETC", "Data has same FORMATETC");

        /// <summary>
        /// View is already frozen
        /// </summary>
        public static HRESULT VIEW_S_ALREADY_FROZEN = new HRESULT("0x00040140", "VIEW_S_ALREADY_FROZEN", "View is already frozen");

        /// <summary>
        /// FORMATETC not supported
        /// </summary>
        public static HRESULT CACHE_S_FORMATETC_NOTSUPPORTED = new HRESULT("0x00040170", "CACHE_S_FORMATETC_NOTSUPPORTED", "FORMATETC not supported");

        /// <summary>
        /// Same cache
        /// </summary>
        public static HRESULT CACHE_S_SAMECACHE = new HRESULT("0x00040171", "CACHE_S_SAMECACHE", "Same cache");

        /// <summary>
        /// Some cache(s) not updated
        /// </summary>
        public static HRESULT CACHE_S_SOMECACHES_NOTUPDATED = new HRESULT("0x00040172", "CACHE_S_SOMECACHES_NOTUPDATED", "Some cache(s) not updated");

        /// <summary>
        /// Invalid verb for OLE object
        /// </summary>
        public static HRESULT OLEOBJ_S_INVALIDVERB = new HRESULT("0x00040180", "OLEOBJ_S_INVALIDVERB", "Invalid verb for OLE object");

        /// <summary>
        /// Verb number is valid but verb cannot be done now
        /// </summary>
        public static HRESULT OLEOBJ_S_CANNOT_DOVERB_NOW = new HRESULT("0x00040181", "OLEOBJ_S_CANNOT_DOVERB_NOW", "Verb number is valid but verb cannot be done now");

        /// <summary>
        /// Invalid window handle passed
        /// </summary>
        public static HRESULT OLEOBJ_S_INVALIDHWND = new HRESULT("0x00040182", "OLEOBJ_S_INVALIDHWND", "Invalid window handle passed");

        /// <summary>
        /// Message is too long; some of it had to be truncated before displaying
        /// </summary>
        public static HRESULT INPLACE_S_TRUNCATED = new HRESULT("0x000401A0", "INPLACE_S_TRUNCATED", "Message is too long; some of it had to be truncated before displaying");

        /// <summary>
        /// Unable to convert OLESTREAM to IStorage
        /// </summary>
        public static HRESULT CONVERT10_S_NO_PRESENTATION = new HRESULT("0x000401C0", "CONVERT10_S_NO_PRESENTATION", "Unable to convert OLESTREAM to IStorage");

        /// <summary>
        /// Moniker reduced to itself
        /// </summary>
        public static HRESULT MK_S_REDUCED_TO_SELF = new HRESULT("0x000401E2", "MK_S_REDUCED_TO_SELF", "Moniker reduced to itself");

        /// <summary>
        /// Common prefix is this moniker
        /// </summary>
        public static HRESULT MK_S_ME = new HRESULT("0x000401E4", "MK_S_ME", "Common prefix is this moniker");

        /// <summary>
        /// Common prefix is input moniker
        /// </summary>
        public static HRESULT MK_S_HIM = new HRESULT("0x000401E5", "MK_S_HIM", "Common prefix is input moniker");

        /// <summary>
        /// Common prefix is both monikers
        /// </summary>
        public static HRESULT MK_S_US = new HRESULT("0x000401E6", "MK_S_US", "Common prefix is both monikers");

        /// <summary>
        /// Moniker is already registered in running object table
        /// </summary>
        public static HRESULT MK_S_MONIKERALREADYREGISTERED = new HRESULT("0x000401E7", "MK_S_MONIKERALREADYREGISTERED", "Moniker is already registered in running object table");

        /// <summary>
        /// The task is ready to run at its next scheduled time.
        /// </summary>
        public static HRESULT SCHED_S_TASK_READY = new HRESULT("0x00041300", "SCHED_S_TASK_READY", "The task is ready to run at its next scheduled time.");

        /// <summary>
        /// The task is currently running.
        /// </summary>
        public static HRESULT SCHED_S_TASK_RUNNING = new HRESULT("0x00041301", "SCHED_S_TASK_RUNNING", "The task is currently running.");

        /// <summary>
        /// The task will not run at the scheduled times because it has been disabled.
        /// </summary>
        public static HRESULT SCHED_S_TASK_DISABLED = new HRESULT("0x00041302", "SCHED_S_TASK_DISABLED", "The task will not run at the scheduled times because it has been disabled.");

        /// <summary>
        /// The task has not yet run.
        /// </summary>
        public static HRESULT SCHED_S_TASK_HAS_NOT_RUN = new HRESULT("0x00041303", "SCHED_S_TASK_HAS_NOT_RUN", "The task has not yet run.");

        /// <summary>
        /// There are no more runs scheduled for this task.
        /// </summary>
        public static HRESULT SCHED_S_TASK_NO_MORE_RUNS = new HRESULT("0x00041304", "SCHED_S_TASK_NO_MORE_RUNS", "There are no more runs scheduled for this task.");

        /// <summary>
        /// One or more of the properties that are needed to run this task on a schedule have not been set.
        /// </summary>
        public static HRESULT SCHED_S_TASK_NOT_SCHEDULED = new HRESULT("0x00041305", "SCHED_S_TASK_NOT_SCHEDULED", "One or more of the properties that are needed to run this task on a schedule have not been set.");

        /// <summary>
        /// The last run of the task was terminated by the user.
        /// </summary>
        public static HRESULT SCHED_S_TASK_TERMINATED = new HRESULT("0x00041306", "SCHED_S_TASK_TERMINATED", "The last run of the task was terminated by the user.");

        /// <summary>
        /// Either the task has no triggers or the existing triggers are disabled or not set.
        /// </summary>
        public static HRESULT SCHED_S_TASK_NO_VALID_TRIGGERS = new HRESULT("0x00041307", "SCHED_S_TASK_NO_VALID_TRIGGERS", "Either the task has no triggers or the existing triggers are disabled or not set.");

        /// <summary>
        /// Event triggers don't have set run times.
        /// </summary>
        public static HRESULT SCHED_S_EVENT_TRIGGER = new HRESULT("0x00041308", "SCHED_S_EVENT_TRIGGER", "Event triggers don't have set run times.");

        /// <summary>
        /// Trigger not found.
        /// </summary>
        public static HRESULT SCHED_E_TRIGGER_NOT_FOUND = new HRESULT("0x80041309", "SCHED_E_TRIGGER_NOT_FOUND", "Trigger not found.");

        /// <summary>
        /// One or more of the properties that are needed to run this task have not been set.
        /// </summary>
        public static HRESULT SCHED_E_TASK_NOT_READY = new HRESULT("0x8004130A", "SCHED_E_TASK_NOT_READY", "One or more of the properties that are needed to run this task have not been set.");

        /// <summary>
        /// There is no running instance of the task.
        /// </summary>
        public static HRESULT SCHED_E_TASK_NOT_RUNNING = new HRESULT("0x8004130B", "SCHED_E_TASK_NOT_RUNNING", "There is no running instance of the task.");

        /// <summary>
        /// The Task Scheduler Service is not installed on this computer.
        /// </summary>
        public static HRESULT SCHED_E_SERVICE_NOT_INSTALLED = new HRESULT("0x8004130C", "SCHED_E_SERVICE_NOT_INSTALLED", "The Task Scheduler Service is not installed on this computer.");

        /// <summary>
        /// The task object could not be opened.
        /// </summary>
        public static HRESULT SCHED_E_CANNOT_OPEN_TASK = new HRESULT("0x8004130D", "SCHED_E_CANNOT_OPEN_TASK", "The task object could not be opened.");

        /// <summary>
        /// The object is either an invalid task object or is not a task object.
        /// </summary>
        public static HRESULT SCHED_E_INVALID_TASK = new HRESULT("0x8004130E", "SCHED_E_INVALID_TASK", "The object is either an invalid task object or is not a task object.");

        /// <summary>
        /// No account information could be found in the Task Scheduler security database for the task indicated.
        /// </summary>
        public static HRESULT SCHED_E_ACCOUNT_INFORMATION_NOT_SET = new HRESULT("0x8004130F", "SCHED_E_ACCOUNT_INFORMATION_NOT_SET", "No account information could be found in the Task Scheduler security database for the task indicated.");

        /// <summary>
        /// Unable to establish existence of the account specified.
        /// </summary>
        public static HRESULT SCHED_E_ACCOUNT_NAME_NOT_FOUND = new HRESULT("0x80041310", "SCHED_E_ACCOUNT_NAME_NOT_FOUND", "Unable to establish existence of the account specified.");

        /// <summary>
        /// Corruption was detected in the Task Scheduler security database; the database has been reset.
        /// </summary>
        public static HRESULT SCHED_E_ACCOUNT_DBASE_CORRUPT = new HRESULT("0x80041311", "SCHED_E_ACCOUNT_DBASE_CORRUPT", "Corruption was detected in the Task Scheduler security database; the database has been reset.");

        /// <summary>
        /// Task Scheduler security services are not available.
        /// </summary>
        public static HRESULT SCHED_E_NO_SECURITY_SERVICES = new HRESULT("0x80041312", "SCHED_E_NO_SECURITY_SERVICES", "Task Scheduler security services are not available.");

        /// <summary>
        /// The task object version is either unsupported or invalid.
        /// </summary>
        public static HRESULT SCHED_E_UNKNOWN_OBJECT_VERSION = new HRESULT("0x80041313", "SCHED_E_UNKNOWN_OBJECT_VERSION", "The task object version is either unsupported or invalid.");

        /// <summary>
        /// The task has been configured with an unsupported combination of account settings and run time options.
        /// </summary>
        public static HRESULT SCHED_E_UNSUPPORTED_ACCOUNT_OPTION = new HRESULT("0x80041314", "SCHED_E_UNSUPPORTED_ACCOUNT_OPTION", "The task has been configured with an unsupported combination of account settings and run time options.");

        /// <summary>
        /// The Task Scheduler Service is not running.
        /// </summary>
        public static HRESULT SCHED_E_SERVICE_NOT_RUNNING = new HRESULT("0x80041315", "SCHED_E_SERVICE_NOT_RUNNING", "The Task Scheduler Service is not running.");

        /// <summary>
        /// The task XML contains an unexpected node.
        /// </summary>
        public static HRESULT SCHED_E_UNEXPECTEDNODE = new HRESULT("0x80041316", "SCHED_E_UNEXPECTEDNODE", "The task XML contains an unexpected node.");

        /// <summary>
        /// The task XML contains an element or attribute from an unexpected namespace.
        /// </summary>
        public static HRESULT SCHED_E_NAMESPACE = new HRESULT("0x80041317", "SCHED_E_NAMESPACE", "The task XML contains an element or attribute from an unexpected namespace.");

        /// <summary>
        /// The task XML contains a value which is incorrectly formatted or out of range.
        /// </summary>
        public static HRESULT SCHED_E_INVALIDVALUE = new HRESULT("0x80041318", "SCHED_E_INVALIDVALUE", "The task XML contains a value which is incorrectly formatted or out of range.");

        /// <summary>
        /// The task XML is missing a required element or attribute.
        /// </summary>
        public static HRESULT SCHED_E_MISSINGNODE = new HRESULT("0x80041319", "SCHED_E_MISSINGNODE", "The task XML is missing a required element or attribute.");

        /// <summary>
        /// The task XML is malformed.
        /// </summary>
        public static HRESULT SCHED_E_MALFORMEDXML = new HRESULT("0x8004131A", "SCHED_E_MALFORMEDXML", "The task XML is malformed.");

        /// <summary>
        /// The task is registered, but not all specified triggers will start the task.
        /// </summary>
        public static HRESULT SCHED_S_SOME_TRIGGERS_FAILED = new HRESULT("0x0004131B", "SCHED_S_SOME_TRIGGERS_FAILED", "The task is registered, but not all specified triggers will start the task.");

        /// <summary>
        /// The task is registered, but may fail to start. Batch logon privilege needs to be enabled for the task principal.
        /// </summary>
        public static HRESULT SCHED_S_BATCH_LOGON_PROBLEM = new HRESULT("0x0004131C", "SCHED_S_BATCH_LOGON_PROBLEM", "The task is registered, but may fail to start. Batch logon privilege needs to be enabled for the task principal.");

        /// <summary>
        /// The task XML contains too many nodes of the same type.
        /// </summary>
        public static HRESULT SCHED_E_TOO_MANY_NODES = new HRESULT("0x8004131D", "SCHED_E_TOO_MANY_NODES", "The task XML contains too many nodes of the same type.");

        /// <summary>
        /// The task cannot be started after the trigger's end boundary.
        /// </summary>
        public static HRESULT SCHED_E_PAST_END_BOUNDARY = new HRESULT("0x8004131E", "SCHED_E_PAST_END_BOUNDARY", "The task cannot be started after the trigger's end boundary.");

        /// <summary>
        /// An instance of this task is already running.
        /// </summary>
        public static HRESULT SCHED_E_ALREADY_RUNNING = new HRESULT("0x8004131F", "SCHED_E_ALREADY_RUNNING", "An instance of this task is already running.");

        /// <summary>
        /// The task will not run because the user is not logged on.
        /// </summary>
        public static HRESULT SCHED_E_USER_NOT_LOGGED_ON = new HRESULT("0x80041320", "SCHED_E_USER_NOT_LOGGED_ON", "The task will not run because the user is not logged on.");

        /// <summary>
        /// The task image is corrupt or has been tampered with.
        /// </summary>
        public static HRESULT SCHED_E_INVALID_TASK_HASH = new HRESULT("0x80041321", "SCHED_E_INVALID_TASK_HASH", "The task image is corrupt or has been tampered with.");

        /// <summary>
        /// The Task Scheduler service is not available.
        /// </summary>
        public static HRESULT SCHED_E_SERVICE_NOT_AVAILABLE = new HRESULT("0x80041322", "SCHED_E_SERVICE_NOT_AVAILABLE", "The Task Scheduler service is not available.");

        /// <summary>
        /// The Task Scheduler service is too busy to handle your request. Please try again later.
        /// </summary>
        public static HRESULT SCHED_E_SERVICE_TOO_BUSY = new HRESULT("0x80041323", "SCHED_E_SERVICE_TOO_BUSY", "The Task Scheduler service is too busy to handle your request. Please try again later.");

        /// <summary>
        /// The Task Scheduler service attempted to run the task, but the task did not run due to one of the constraints in the task definition.
        /// </summary>
        public static HRESULT SCHED_E_TASK_ATTEMPTED = new HRESULT("0x80041324", "SCHED_E_TASK_ATTEMPTED", "The Task Scheduler service attempted to run the task, but the task did not run due to one of the constraints in the task definition.");

        /// <summary>
        /// The Task Scheduler service has asked the task to run.
        /// </summary>
        public static HRESULT SCHED_S_TASK_QUEUED = new HRESULT("0x00041325", "SCHED_S_TASK_QUEUED", "The Task Scheduler service has asked the task to run.");

        /// <summary>
        /// The task is disabled.
        /// </summary>
        public static HRESULT SCHED_E_TASK_DISABLED = new HRESULT("0x80041326", "SCHED_E_TASK_DISABLED", "The task is disabled.");

        /// <summary>
        /// The task has properties that are not compatible with previous versions of Windows.
        /// </summary>
        public static HRESULT SCHED_E_TASK_NOT_V1_COMPAT = new HRESULT("0x80041327", "SCHED_E_TASK_NOT_V1_COMPAT", "The task has properties that are not compatible with previous versions of Windows.");

        /// <summary>
        /// The task settings do not allow the task to start on demand.
        /// </summary>
        public static HRESULT SCHED_E_START_ON_DEMAND = new HRESULT("0x80041328", "SCHED_E_START_ON_DEMAND", "The task settings do not allow the task to start on demand.");

        /// <summary>
        /// The combination of properties that task is using is not compatible with the scheduling engine.
        /// </summary>
        public static HRESULT SCHED_E_TASK_NOT_UBPM_COMPAT = new HRESULT("0x80041329", "SCHED_E_TASK_NOT_UBPM_COMPAT", "The combination of properties that task is using is not compatible with the scheduling engine.");

        /// <summary>
        /// Attempt to create a class object failed
        /// </summary>
        public static HRESULT CO_E_CLASS_CREATE_FAILED = new HRESULT("0x80080001", "CO_E_CLASS_CREATE_FAILED", "Attempt to create a class object failed");

        /// <summary>
        /// OLE service could not bind object
        /// </summary>
        public static HRESULT CO_E_SCM_ERROR = new HRESULT("0x80080002", "CO_E_SCM_ERROR", "OLE service could not bind object");

        /// <summary>
        /// RPC communication failed with OLE service
        /// </summary>
        public static HRESULT CO_E_SCM_RPC_FAILURE = new HRESULT("0x80080003", "CO_E_SCM_RPC_FAILURE", "RPC communication failed with OLE service");

        /// <summary>
        /// Bad path to object
        /// </summary>
        public static HRESULT CO_E_BAD_PATH = new HRESULT("0x80080004", "CO_E_BAD_PATH", "Bad path to object");

        /// <summary>
        /// Server execution failed
        /// </summary>
        public static HRESULT CO_E_SERVER_EXEC_FAILURE = new HRESULT("0x80080005", "CO_E_SERVER_EXEC_FAILURE", "Server execution failed");

        /// <summary>
        /// OLE service could not communicate with the object server
        /// </summary>
        public static HRESULT CO_E_OBJSRV_RPC_FAILURE = new HRESULT("0x80080006", "CO_E_OBJSRV_RPC_FAILURE", "OLE service could not communicate with the object server");

        /// <summary>
        /// Moniker path could not be normalized
        /// </summary>
        public static HRESULT MK_E_NO_NORMALIZED = new HRESULT("0x80080007", "MK_E_NO_NORMALIZED", "Moniker path could not be normalized");

        /// <summary>
        /// Object server is stopping when OLE service contacts it
        /// </summary>
        public static HRESULT CO_E_SERVER_STOPPING = new HRESULT("0x80080008", "CO_E_SERVER_STOPPING", "Object server is stopping when OLE service contacts it");

        /// <summary>
        /// An invalid root block pointer was specified
        /// </summary>
        public static HRESULT MEM_E_INVALID_ROOT = new HRESULT("0x80080009", "MEM_E_INVALID_ROOT", "An invalid root block pointer was specified");

        /// <summary>
        /// An allocation chain contained an invalid link pointer
        /// </summary>
        public static HRESULT MEM_E_INVALID_LINK = new HRESULT("0x80080010", "MEM_E_INVALID_LINK", "An allocation chain contained an invalid link pointer");

        /// <summary>
        /// The requested allocation size was too large
        /// </summary>
        public static HRESULT MEM_E_INVALID_SIZE = new HRESULT("0x80080011", "MEM_E_INVALID_SIZE", "The requested allocation size was too large");

        /// <summary>
        /// Not all the requested interfaces were available
        /// </summary>
        public static HRESULT CO_S_NOTALLINTERFACES = new HRESULT("0x00080012", "CO_S_NOTALLINTERFACES", "Not all the requested interfaces were available");

        /// <summary>
        /// The specified machine name was not found in the cache.
        /// </summary>
        public static HRESULT CO_S_MACHINENAMENOTFOUND = new HRESULT("0x00080013", "CO_S_MACHINENAMENOTFOUND", "The specified machine name was not found in the cache.");

        /// <summary>
        /// The activation requires a display name to be present under the CLSID key.
        /// </summary>
        public static HRESULT CO_E_MISSING_DISPLAYNAME = new HRESULT("0x80080015", "CO_E_MISSING_DISPLAYNAME", "The activation requires a display name to be present under the CLSID key.");

        /// <summary>
        /// The activation requires that the RunAs value for the application is Activate As Activator.
        /// </summary>
        public static HRESULT CO_E_RUNAS_VALUE_MUST_BE_AAA = new HRESULT("0x80080016", "CO_E_RUNAS_VALUE_MUST_BE_AAA", "The activation requires that the RunAs value for the application is Activate As Activator.");

        /// <summary>
        /// The class is not configured to support Elevated activation.
        /// </summary>
        public static HRESULT CO_E_ELEVATION_DISABLED = new HRESULT("0x80080017", "CO_E_ELEVATION_DISABLED", "The class is not configured to support Elevated activation.");

        /// <summary>
        /// Unknown interface.
        /// </summary>
        public static HRESULT DISP_E_UNKNOWNINTERFACE = new HRESULT("0x80020001", "DISP_E_UNKNOWNINTERFACE", "Unknown interface.");

        /// <summary>
        /// Member not found.
        /// </summary>
        public static HRESULT DISP_E_MEMBERNOTFOUND = new HRESULT("0x80020003", "DISP_E_MEMBERNOTFOUND", "Member not found.");

        /// <summary>
        /// Parameter not found.
        /// </summary>
        public static HRESULT DISP_E_PARAMNOTFOUND = new HRESULT("0x80020004", "DISP_E_PARAMNOTFOUND", "Parameter not found.");

        /// <summary>
        /// Type mismatch.
        /// </summary>
        public static HRESULT DISP_E_TYPEMISMATCH = new HRESULT("0x80020005", "DISP_E_TYPEMISMATCH", "Type mismatch.");

        /// <summary>
        /// Unknown name.
        /// </summary>
        public static HRESULT DISP_E_UNKNOWNNAME = new HRESULT("0x80020006", "DISP_E_UNKNOWNNAME", "Unknown name.");

        /// <summary>
        /// No named arguments.
        /// </summary>
        public static HRESULT DISP_E_NONAMEDARGS = new HRESULT("0x80020007", "DISP_E_NONAMEDARGS", "No named arguments.");

        /// <summary>
        /// Bad variable type.
        /// </summary>
        public static HRESULT DISP_E_BADVARTYPE = new HRESULT("0x80020008", "DISP_E_BADVARTYPE", "Bad variable type.");

        /// <summary>
        /// Exception occurred.
        /// </summary>
        public static HRESULT DISP_E_EXCEPTION = new HRESULT("0x80020009", "DISP_E_EXCEPTION", "Exception occurred.");

        /// <summary>
        /// Out of present range.
        /// </summary>
        public static HRESULT DISP_E_OVERFLOW = new HRESULT("0x8002000A", "DISP_E_OVERFLOW", "Out of present range.");

        /// <summary>
        /// Invalid index.
        /// </summary>
        public static HRESULT DISP_E_BADINDEX = new HRESULT("0x8002000B", "DISP_E_BADINDEX", "Invalid index.");

        /// <summary>
        /// Unknown language.
        /// </summary>
        public static HRESULT DISP_E_UNKNOWNLCID = new HRESULT("0x8002000C", "DISP_E_UNKNOWNLCID", "Unknown language.");

        /// <summary>
        /// Memory is locked.
        /// </summary>
        public static HRESULT DISP_E_ARRAYISLOCKED = new HRESULT("0x8002000D", "DISP_E_ARRAYISLOCKED", "Memory is locked.");

        /// <summary>
        /// Invalid number of parameters.
        /// </summary>
        public static HRESULT DISP_E_BADPARAMCOUNT = new HRESULT("0x8002000E", "DISP_E_BADPARAMCOUNT", "Invalid number of parameters.");

        /// <summary>
        /// Parameter not optional.
        /// </summary>
        public static HRESULT DISP_E_PARAMNOTOPTIONAL = new HRESULT("0x8002000F", "DISP_E_PARAMNOTOPTIONAL", "Parameter not optional.");

        /// <summary>
        /// Invalid callee.
        /// </summary>
        public static HRESULT DISP_E_BADCALLEE = new HRESULT("0x80020010", "DISP_E_BADCALLEE", "Invalid callee.");

        /// <summary>
        /// Does not support a collection.
        /// </summary>
        public static HRESULT DISP_E_NOTACOLLECTION = new HRESULT("0x80020011", "DISP_E_NOTACOLLECTION", "Does not support a collection.");

        /// <summary>
        /// Division by zero.
        /// </summary>
        public static HRESULT DISP_E_DIVBYZERO = new HRESULT("0x80020012", "DISP_E_DIVBYZERO", "Division by zero.");

        /// <summary>
        /// Buffer too small
        /// </summary>
        public static HRESULT DISP_E_BUFFERTOOSMALL = new HRESULT("0x80020013", "DISP_E_BUFFERTOOSMALL", "Buffer too small");

        /// <summary>
        /// Buffer too small.
        /// </summary>
        public static HRESULT TYPE_E_BUFFERTOOSMALL = new HRESULT("0x80028016", "TYPE_E_BUFFERTOOSMALL", "Buffer too small.");

        /// <summary>
        /// Field name not defined in the record.
        /// </summary>
        public static HRESULT TYPE_E_FIELDNOTFOUND = new HRESULT("0x80028017", "TYPE_E_FIELDNOTFOUND", "Field name not defined in the record.");

        /// <summary>
        /// Old format or invalid type library.
        /// </summary>
        public static HRESULT TYPE_E_INVDATAREAD = new HRESULT("0x80028018", "TYPE_E_INVDATAREAD", "Old format or invalid type library.");

        /// <summary>
        /// Old format or invalid type library.
        /// </summary>
        public static HRESULT TYPE_E_UNSUPFORMAT = new HRESULT("0x80028019", "TYPE_E_UNSUPFORMAT", "Old format or invalid type library.");

        /// <summary>
        /// Error accessing the OLE registry.
        /// </summary>
        public static HRESULT TYPE_E_REGISTRYACCESS = new HRESULT("0x8002801C", "TYPE_E_REGISTRYACCESS", "Error accessing the OLE registry.");

        /// <summary>
        /// Library not registered.
        /// </summary>
        public static HRESULT TYPE_E_LIBNOTREGISTERED = new HRESULT("0x8002801D", "TYPE_E_LIBNOTREGISTERED", "Library not registered.");

        /// <summary>
        /// Bound to unknown type.
        /// </summary>
        public static HRESULT TYPE_E_UNDEFINEDTYPE = new HRESULT("0x80028027", "TYPE_E_UNDEFINEDTYPE", "Bound to unknown type.");

        /// <summary>
        /// Qualified name disallowed.
        /// </summary>
        public static HRESULT TYPE_E_QUALIFIEDNAMEDISALLOWED = new HRESULT("0x80028028", "TYPE_E_QUALIFIEDNAMEDISALLOWED", "Qualified name disallowed.");

        /// <summary>
        /// Invalid forward reference, or reference to uncompiled type.
        /// </summary>
        public static HRESULT TYPE_E_INVALIDSTATE = new HRESULT("0x80028029", "TYPE_E_INVALIDSTATE", "Invalid forward reference, or reference to uncompiled type.");

        /// <summary>
        /// Type mismatch.
        /// </summary>
        public static HRESULT TYPE_E_WRONGTYPEKIND = new HRESULT("0x8002802A", "TYPE_E_WRONGTYPEKIND", "Type mismatch.");

        /// <summary>
        /// Element not found.
        /// </summary>
        public static HRESULT TYPE_E_ELEMENTNOTFOUND = new HRESULT("0x8002802B", "TYPE_E_ELEMENTNOTFOUND", "Element not found.");

        /// <summary>
        /// Ambiguous name.
        /// </summary>
        public static HRESULT TYPE_E_AMBIGUOUSNAME = new HRESULT("0x8002802C", "TYPE_E_AMBIGUOUSNAME", "Ambiguous name.");

        /// <summary>
        /// Name already exists in the library.
        /// </summary>
        public static HRESULT TYPE_E_NAMECONFLICT = new HRESULT("0x8002802D", "TYPE_E_NAMECONFLICT", "Name already exists in the library.");

        /// <summary>
        /// Unknown LCID.
        /// </summary>
        public static HRESULT TYPE_E_UNKNOWNLCID = new HRESULT("0x8002802E", "TYPE_E_UNKNOWNLCID", "Unknown LCID.");

        /// <summary>
        /// Function not defined in specified DLL.
        /// </summary>
        public static HRESULT TYPE_E_DLLFUNCTIONNOTFOUND = new HRESULT("0x8002802F", "TYPE_E_DLLFUNCTIONNOTFOUND", "Function not defined in specified DLL.");

        /// <summary>
        /// Wrong module kind for the operation.
        /// </summary>
        public static HRESULT TYPE_E_BADMODULEKIND = new HRESULT("0x800288BD", "TYPE_E_BADMODULEKIND", "Wrong module kind for the operation.");

        /// <summary>
        /// Size may not exceed 64K.
        /// </summary>
        public static HRESULT TYPE_E_SIZETOOBIG = new HRESULT("0x800288C5", "TYPE_E_SIZETOOBIG", "Size may not exceed 64K.");

        /// <summary>
        /// Duplicate ID in inheritance hierarchy.
        /// </summary>
        public static HRESULT TYPE_E_DUPLICATEID = new HRESULT("0x800288C6", "TYPE_E_DUPLICATEID", "Duplicate ID in inheritance hierarchy.");

        /// <summary>
        /// Incorrect inheritance depth in standard OLE hmember.
        /// </summary>
        public static HRESULT TYPE_E_INVALIDID = new HRESULT("0x800288CF", "TYPE_E_INVALIDID", "Incorrect inheritance depth in standard OLE hmember.");

        /// <summary>
        /// Type mismatch.
        /// </summary>
        public static HRESULT TYPE_E_TYPEMISMATCH = new HRESULT("0x80028CA0", "TYPE_E_TYPEMISMATCH", "Type mismatch.");

        /// <summary>
        /// Invalid number of arguments.
        /// </summary>
        public static HRESULT TYPE_E_OUTOFBOUNDS = new HRESULT("0x80028CA1", "TYPE_E_OUTOFBOUNDS", "Invalid number of arguments.");

        /// <summary>
        /// I/O Error.
        /// </summary>
        public static HRESULT TYPE_E_IOERROR = new HRESULT("0x80028CA2", "TYPE_E_IOERROR", "I/O Error.");

        /// <summary>
        /// Error creating unique tmp file.
        /// </summary>
        public static HRESULT TYPE_E_CANTCREATETMPFILE = new HRESULT("0x80028CA3", "TYPE_E_CANTCREATETMPFILE", "Error creating unique tmp file.");

        /// <summary>
        /// Error loading type library/DLL.
        /// </summary>
        public static HRESULT TYPE_E_CANTLOADLIBRARY = new HRESULT("0x80029C4A", "TYPE_E_CANTLOADLIBRARY", "Error loading type library/DLL.");

        /// <summary>
        /// Inconsistent property functions.
        /// </summary>
        public static HRESULT TYPE_E_INCONSISTENTPROPFUNCS = new HRESULT("0x80029C83", "TYPE_E_INCONSISTENTPROPFUNCS", "Inconsistent property functions.");
        #endregion

        #region COM Error Codes (STG, RPC)
        /// <summary>
        /// Unable to perform requested operation.
        /// </summary>
        public static HRESULT STG_E_INVALIDFUNCTION = new HRESULT("0x80030001", "STG_E_INVALIDFUNCTION", "Unable to perform requested operation.");

        /// <summary>
        /// could not be found.
        /// </summary>
        public static HRESULT STG_E_FILENOTFOUND = new HRESULT("0x80030002", "STG_E_FILENOTFOUND", "could not be found.");

        /// <summary>
        /// The path %1 could not be found.
        /// </summary>
        public static HRESULT STG_E_PATHNOTFOUND = new HRESULT("0x80030003", "STG_E_PATHNOTFOUND", "The path %1 could not be found.");

        /// <summary>
        /// There are insufficient resources to open another file.
        /// </summary>
        public static HRESULT STG_E_TOOMANYOPENFILES = new HRESULT("0x80030004", "STG_E_TOOMANYOPENFILES", "There are insufficient resources to open another file.");

        /// <summary>
        /// Access Denied.
        /// </summary>
        public static HRESULT STG_E_ACCESSDENIED = new HRESULT("0x80030005", "STG_E_ACCESSDENIED", "Access Denied.");

        /// <summary>
        /// Attempted an operation on an invalid object.
        /// </summary>
        public static HRESULT STG_E_INVALIDHANDLE = new HRESULT("0x80030006", "STG_E_INVALIDHANDLE", "Attempted an operation on an invalid object.");

        /// <summary>
        /// There is insufficient memory available to complete operation.
        /// </summary>
        public static HRESULT STG_E_INSUFFICIENTMEMORY = new HRESULT("0x80030008", "STG_E_INSUFFICIENTMEMORY", "There is insufficient memory available to complete operation.");

        /// <summary>
        /// Invalid pointer error.
        /// </summary>
        public static HRESULT STG_E_INVALIDPOINTER = new HRESULT("0x80030009", "STG_E_INVALIDPOINTER", "Invalid pointer error.");

        /// <summary>
        /// There are no more entries to return.
        /// </summary>
        public static HRESULT STG_E_NOMOREFILES = new HRESULT("0x80030012", "STG_E_NOMOREFILES", "There are no more entries to return.");

        /// <summary>
        /// Disk is write-protected.
        /// </summary>
        public static HRESULT STG_E_DISKISWRITEPROTECTED = new HRESULT("0x80030013", "STG_E_DISKISWRITEPROTECTED", "Disk is write-protected.");

        /// <summary>
        /// An error occurred during a seek operation.
        /// </summary>
        public static HRESULT STG_E_SEEKERROR = new HRESULT("0x80030019", "STG_E_SEEKERROR", "An error occurred during a seek operation.");

        /// <summary>
        /// A disk error occurred during a write operation.
        /// </summary>
        public static HRESULT STG_E_WRITEFAULT = new HRESULT("0x8003001D", "STG_E_WRITEFAULT", "A disk error occurred during a write operation.");

        /// <summary>
        /// A disk error occurred during a read operation.
        /// </summary>
        public static HRESULT STG_E_READFAULT = new HRESULT("0x8003001E", "STG_E_READFAULT", "A disk error occurred during a read operation.");

        /// <summary>
        /// A share violation has occurred.
        /// </summary>
        public static HRESULT STG_E_SHAREVIOLATION = new HRESULT("0x80030020", "STG_E_SHAREVIOLATION", "A share violation has occurred.");

        /// <summary>
        /// A lock violation has occurred.
        /// </summary>
        public static HRESULT STG_E_LOCKVIOLATION = new HRESULT("0x80030021", "STG_E_LOCKVIOLATION", "A lock violation has occurred.");

        /// <summary>
        /// already exists.
        /// </summary>
        public static HRESULT STG_E_FILEALREADYEXISTS = new HRESULT("0x80030050", "STG_E_FILEALREADYEXISTS", "already exists.");

        /// <summary>
        /// Invalid parameter error.
        /// </summary>
        public static HRESULT STG_E_INVALIDPARAMETER = new HRESULT("0x80030057", "STG_E_INVALIDPARAMETER", "Invalid parameter error.");

        /// <summary>
        /// There is insufficient disk space to complete operation.
        /// </summary>
        public static HRESULT STG_E_MEDIUMFULL = new HRESULT("0x80030070", "STG_E_MEDIUMFULL", "There is insufficient disk space to complete operation.");

        /// <summary>
        /// Illegal write of non-simple property to simple property set.
        /// </summary>
        public static HRESULT STG_E_PROPSETMISMATCHED = new HRESULT("0x800300F0", "STG_E_PROPSETMISMATCHED", "Illegal write of non-simple property to simple property set.");

        /// <summary>
        /// An API call exited abnormally.
        /// </summary>
        public static HRESULT STG_E_ABNORMALAPIEXIT = new HRESULT("0x800300FA", "STG_E_ABNORMALAPIEXIT", "An API call exited abnormally.");

        /// <summary>
        /// The file %1 is not a valid compound file.
        /// </summary>
        public static HRESULT STG_E_INVALIDHEADER = new HRESULT("0x800300FB", "STG_E_INVALIDHEADER", "The file %1 is not a valid compound file.");

        /// <summary>
        /// The name %1 is not valid.
        /// </summary>
        public static HRESULT STG_E_INVALIDNAME = new HRESULT("0x800300FC", "STG_E_INVALIDNAME", "The name %1 is not valid.");

        /// <summary>
        /// An unexpected error occurred.
        /// </summary>
        public static HRESULT STG_E_UNKNOWN = new HRESULT("0x800300FD", "STG_E_UNKNOWN", "An unexpected error occurred.");

        /// <summary>
        /// That function is not implemented.
        /// </summary>
        public static HRESULT STG_E_UNIMPLEMENTEDFUNCTION = new HRESULT("0x800300FE", "STG_E_UNIMPLEMENTEDFUNCTION", "That function is not implemented.");

        /// <summary>
        /// Invalid flag error.
        /// </summary>
        public static HRESULT STG_E_INVALIDFLAG = new HRESULT("0x800300FF", "STG_E_INVALIDFLAG", "Invalid flag error.");

        /// <summary>
        /// Attempted to use an object that is busy.
        /// </summary>
        public static HRESULT STG_E_INUSE = new HRESULT("0x80030100", "STG_E_INUSE", "Attempted to use an object that is busy.");

        /// <summary>
        /// The storage has been changed since the last commit.
        /// </summary>
        public static HRESULT STG_E_NOTCURRENT = new HRESULT("0x80030101", "STG_E_NOTCURRENT", "The storage has been changed since the last commit.");

        /// <summary>
        /// Attempted to use an object that has ceased to exist.
        /// </summary>
        public static HRESULT STG_E_REVERTED = new HRESULT("0x80030102", "STG_E_REVERTED", "Attempted to use an object that has ceased to exist.");

        /// <summary>
        /// Can't save.
        /// </summary>
        public static HRESULT STG_E_CANTSAVE = new HRESULT("0x80030103", "STG_E_CANTSAVE", "Can't save.");

        /// <summary>
        /// The compound file %1 was produced with an incompatible version of storage.
        /// </summary>
        public static HRESULT STG_E_OLDFORMAT = new HRESULT("0x80030104", "STG_E_OLDFORMAT", "The compound file %1 was produced with an incompatible version of storage.");

        /// <summary>
        /// The compound file %1 was produced with a newer version of storage.
        /// </summary>
        public static HRESULT STG_E_OLDDLL = new HRESULT("0x80030105", "STG_E_OLDDLL", "The compound file %1 was produced with a newer version of storage.");

        /// <summary>
        /// Share.exe or equivalent is required for operation.
        /// </summary>
        public static HRESULT STG_E_SHAREREQUIRED = new HRESULT("0x80030106", "STG_E_SHAREREQUIRED", "Share.exe or equivalent is required for operation.");

        /// <summary>
        /// Illegal operation called on non-file based storage.
        /// </summary>
        public static HRESULT STG_E_NOTFILEBASEDSTORAGE = new HRESULT("0x80030107", "STG_E_NOTFILEBASEDSTORAGE", "Illegal operation called on non-file based storage.");

        /// <summary>
        /// Illegal operation called on object with extant marshallings.
        /// </summary>
        public static HRESULT STG_E_EXTANTMARSHALLINGS = new HRESULT("0x80030108", "STG_E_EXTANTMARSHALLINGS", "Illegal operation called on object with extant marshallings.");

        /// <summary>
        /// The docfile has been corrupted.
        /// </summary>
        public static HRESULT STG_E_DOCFILECORRUPT = new HRESULT("0x80030109", "STG_E_DOCFILECORRUPT", "The docfile has been corrupted.");

        /// <summary>
        /// OLE32.DLL has been loaded at the wrong address.
        /// </summary>
        public static HRESULT STG_E_BADBASEADDRESS = new HRESULT("0x80030110", "STG_E_BADBASEADDRESS", "OLE32.DLL has been loaded at the wrong address.");

        /// <summary>
        /// The compound file is too large for the current implementation
        /// </summary>
        public static HRESULT STG_E_DOCFILETOOLARGE = new HRESULT("0x80030111", "STG_E_DOCFILETOOLARGE", "The compound file is too large for the current implementation");

        /// <summary>
        /// The compound file was not created with the STGM_SIMPLE flag
        /// </summary>
        public static HRESULT STG_E_NOTSIMPLEFORMAT = new HRESULT("0x80030112", "STG_E_NOTSIMPLEFORMAT", "The compound file was not created with the STGM_SIMPLE flag");

        /// <summary>
        /// The file download was aborted abnormally. The file is incomplete.
        /// </summary>
        public static HRESULT STG_E_INCOMPLETE = new HRESULT("0x80030201", "STG_E_INCOMPLETE", "The file download was aborted abnormally. The file is incomplete.");

        /// <summary>
        /// The file download has been terminated.
        /// </summary>
        public static HRESULT STG_E_TERMINATED = new HRESULT("0x80030202", "STG_E_TERMINATED", "The file download has been terminated.");

        /// <summary>
        /// The underlying file was converted to compound file format.
        /// </summary>
        public static HRESULT STG_S_CONVERTED = new HRESULT("0x00030200", "STG_S_CONVERTED", "The underlying file was converted to compound file format.");

        /// <summary>
        /// The storage operation should block until more data is available.
        /// </summary>
        public static HRESULT STG_S_BLOCK = new HRESULT("0x00030201", "STG_S_BLOCK", "The storage operation should block until more data is available.");

        /// <summary>
        /// The storage operation should retry immediately.
        /// </summary>
        public static HRESULT STG_S_RETRYNOW = new HRESULT("0x00030202", "STG_S_RETRYNOW", "The storage operation should retry immediately.");

        /// <summary>
        /// The notified event sink will not influence the storage operation.
        /// </summary>
        public static HRESULT STG_S_MONITORING = new HRESULT("0x00030203", "STG_S_MONITORING", "The notified event sink will not influence the storage operation.");

        /// <summary>
        /// Multiple opens prevent consolidated. (commit succeeded).
        /// </summary>
        public static HRESULT STG_S_MULTIPLEOPENS = new HRESULT("0x00030204", "STG_S_MULTIPLEOPENS", "Multiple opens prevent consolidated. (commit succeeded).");

        /// <summary>
        /// Consolidation of the storage file failed. (commit succeeded).
        /// </summary>
        public static HRESULT STG_S_CONSOLIDATIONFAILED = new HRESULT("0x00030205", "STG_S_CONSOLIDATIONFAILED", "Consolidation of the storage file failed. (commit succeeded).");

        /// <summary>
        /// Consolidation of the storage file is inappropriate. (commit succeeded).
        /// </summary>
        public static HRESULT STG_S_CANNOTCONSOLIDATE = new HRESULT("0x00030206", "STG_S_CANNOTCONSOLIDATE", "Consolidation of the storage file is inappropriate. (commit succeeded).");

        /// <summary>
        /// Generic Copy Protection Error.
        /// </summary>
        public static HRESULT STG_E_STATUS_COPY_PROTECTION_FAILURE = new HRESULT("0x80030305", "STG_E_STATUS_COPY_PROTECTION_FAILURE", "Generic Copy Protection Error.");

        /// <summary>
        /// Copy Protection Error - DVD CSS Authentication failed.
        /// </summary>
        public static HRESULT STG_E_CSS_AUTHENTICATION_FAILURE = new HRESULT("0x80030306", "STG_E_CSS_AUTHENTICATION_FAILURE", "Copy Protection Error - DVD CSS Authentication failed.");

        /// <summary>
        /// Copy Protection Error - The given sector does not have a valid CSS key.
        /// </summary>
        public static HRESULT STG_E_CSS_KEY_NOT_PRESENT = new HRESULT("0x80030307", "STG_E_CSS_KEY_NOT_PRESENT", "Copy Protection Error - The given sector does not have a valid CSS key.");

        /// <summary>
        /// Copy Protection Error - DVD session key not established.
        /// </summary>
        public static HRESULT STG_E_CSS_KEY_NOT_ESTABLISHED = new HRESULT("0x80030308", "STG_E_CSS_KEY_NOT_ESTABLISHED", "Copy Protection Error - DVD session key not established.");

        /// <summary>
        /// Copy Protection Error - The read failed because the sector is encrypted.
        /// </summary>
        public static HRESULT STG_E_CSS_SCRAMBLED_SECTOR = new HRESULT("0x80030309", "STG_E_CSS_SCRAMBLED_SECTOR", "Copy Protection Error - The read failed because the sector is encrypted.");

        /// <summary>
        /// Copy Protection Error - The current DVD's region does not correspond to the region setting of the drive.
        /// </summary>
        public static HRESULT STG_E_CSS_REGION_MISMATCH = new HRESULT("0x8003030A", "STG_E_CSS_REGION_MISMATCH", "Copy Protection Error - The current DVD's region does not correspond to the region setting of the drive.");

        /// <summary>
        /// Copy Protection Error - The drive's region setting may be permanent or the number of user resets has been exhausted.
        /// </summary>
        public static HRESULT STG_E_RESETS_EXHAUSTED = new HRESULT("0x8003030B", "STG_E_RESETS_EXHAUSTED", "Copy Protection Error - The drive's region setting may be permanent or the number of user resets has been exhausted.");

        /// <summary>
        /// Call was rejected by callee.
        /// </summary>
        public static HRESULT RPC_E_CALL_REJECTED = new HRESULT("0x80010001", "RPC_E_CALL_REJECTED", "Call was rejected by callee.");

        /// <summary>
        /// Call was canceled by the message filter.
        /// </summary>
        public static HRESULT RPC_E_CALL_CANCELED = new HRESULT("0x80010002", "RPC_E_CALL_CANCELED", "Call was canceled by the message filter.");

        /// <summary>
        /// The caller is dispatching an intertask SendMessage call and cannot call out via PostMessage.
        /// </summary>
        public static HRESULT RPC_E_CANTPOST_INSENDCALL = new HRESULT("0x80010003", "RPC_E_CANTPOST_INSENDCALL", "The caller is dispatching an intertask SendMessage call and cannot call out via PostMessage.");

        /// <summary>
        /// The caller is dispatching an asynchronous call and cannot make an outgoing call on behalf of this call.
        /// </summary>
        public static HRESULT RPC_E_CANTCALLOUT_INASYNCCALL = new HRESULT("0x80010004", "RPC_E_CANTCALLOUT_INASYNCCALL", "The caller is dispatching an asynchronous call and cannot make an outgoing call on behalf of this call.");

        /// <summary>
        /// It is illegal to call out while inside message filter.
        /// </summary>
        public static HRESULT RPC_E_CANTCALLOUT_INEXTERNALCALL = new HRESULT("0x80010005", "RPC_E_CANTCALLOUT_INEXTERNALCALL", "It is illegal to call out while inside message filter.");

        /// <summary>
        /// The connection terminated or is in a bogus state and cannot be used any more. Other connections are still valid.
        /// </summary>
        public static HRESULT RPC_E_CONNECTION_TERMINATED = new HRESULT("0x80010006", "RPC_E_CONNECTION_TERMINATED", "The connection terminated or is in a bogus state and cannot be used any more. Other connections are still valid.");

        /// <summary>
        /// The callee (server [not server application]) is not available and disappeared; all connections are invalid. The call may have executed.
        /// </summary>
        public static HRESULT RPC_E_SERVER_DIED = new HRESULT("0x80010007", "RPC_E_SERVER_DIED", "The callee (server [not server application]) is not available and disappeared; all connections are invalid. The call may have executed.");

        /// <summary>
        /// The caller (client) disappeared while the callee (server) was processing a call.
        /// </summary>
        public static HRESULT RPC_E_CLIENT_DIED = new HRESULT("0x80010008", "RPC_E_CLIENT_DIED", "The caller (client) disappeared while the callee (server) was processing a call.");

        /// <summary>
        /// The data packet with the marshalled parameter data is incorrect.
        /// </summary>
        public static HRESULT RPC_E_INVALID_DATAPACKET = new HRESULT("0x80010009", "RPC_E_INVALID_DATAPACKET", "The data packet with the marshalled parameter data is incorrect.");

        /// <summary>
        /// The call was not transmitted properly; the message queue was full and was not emptied after yielding.
        /// </summary>
        public static HRESULT RPC_E_CANTTRANSMIT_CALL = new HRESULT("0x8001000A", "RPC_E_CANTTRANSMIT_CALL", "The call was not transmitted properly; the message queue was full and was not emptied after yielding.");

        /// <summary>
        /// The client (caller) cannot marshal the parameter data - low memory, etc.
        /// </summary>
        public static HRESULT RPC_E_CLIENT_CANTMARSHAL_DATA = new HRESULT("0x8001000B", "RPC_E_CLIENT_CANTMARSHAL_DATA", "The client (caller) cannot marshal the parameter data - low memory, etc.");

        /// <summary>
        /// The client (caller) cannot unmarshal the return data - low memory, etc.
        /// </summary>
        public static HRESULT RPC_E_CLIENT_CANTUNMARSHAL_DATA = new HRESULT("0x8001000C", "RPC_E_CLIENT_CANTUNMARSHAL_DATA", "The client (caller) cannot unmarshal the return data - low memory, etc.");

        /// <summary>
        /// The server (callee) cannot marshal the return data - low memory, etc.
        /// </summary>
        public static HRESULT RPC_E_SERVER_CANTMARSHAL_DATA = new HRESULT("0x8001000D", "RPC_E_SERVER_CANTMARSHAL_DATA", "The server (callee) cannot marshal the return data - low memory, etc.");

        /// <summary>
        /// The server (callee) cannot unmarshal the parameter data - low memory, etc.
        /// </summary>
        public static HRESULT RPC_E_SERVER_CANTUNMARSHAL_DATA = new HRESULT("0x8001000E", "RPC_E_SERVER_CANTUNMARSHAL_DATA", "The server (callee) cannot unmarshal the parameter data - low memory, etc.");

        /// <summary>
        /// Received data is invalid; could be server or client data.
        /// </summary>
        public static HRESULT RPC_E_INVALID_DATA = new HRESULT("0x8001000F", "RPC_E_INVALID_DATA", "Received data is invalid; could be server or client data.");

        /// <summary>
        /// A particular parameter is invalid and cannot be (un)marshalled.
        /// </summary>
        public static HRESULT RPC_E_INVALID_PARAMETER = new HRESULT("0x80010010", "RPC_E_INVALID_PARAMETER", "A particular parameter is invalid and cannot be (un)marshalled.");

        /// <summary>
        /// There is no second outgoing call on same channel in DDE conversation.
        /// </summary>
        public static HRESULT RPC_E_CANTCALLOUT_AGAIN = new HRESULT("0x80010011", "RPC_E_CANTCALLOUT_AGAIN", "There is no second outgoing call on same channel in DDE conversation.");

        /// <summary>
        /// The callee (server [not server application]) is not available and disappeared; all connections are invalid. The call did not execute.
        /// </summary>
        public static HRESULT RPC_E_SERVER_DIED_DNE = new HRESULT("0x80010012", "RPC_E_SERVER_DIED_DNE", "The callee (server [not server application]) is not available and disappeared; all connections are invalid. The call did not execute.");

        /// <summary>
        /// System call failed.
        /// </summary>
        public static HRESULT RPC_E_SYS_CALL_FAILED = new HRESULT("0x80010100", "RPC_E_SYS_CALL_FAILED", "System call failed.");

        /// <summary>
        /// Could not allocate some required resource (memory, events, ...)
        /// </summary>
        public static HRESULT RPC_E_OUT_OF_RESOURCES = new HRESULT("0x80010101", "RPC_E_OUT_OF_RESOURCES", "Could not allocate some required resource (memory, events, ...)");

        /// <summary>
        /// Attempted to make calls on more than one thread in single threaded mode.
        /// </summary>
        public static HRESULT RPC_E_ATTEMPTED_MULTITHREAD = new HRESULT("0x80010102", "RPC_E_ATTEMPTED_MULTITHREAD", "Attempted to make calls on more than one thread in single threaded mode.");

        /// <summary>
        /// The requested interface is not registered on the server object.
        /// </summary>
        public static HRESULT RPC_E_NOT_REGISTERED = new HRESULT("0x80010103", "RPC_E_NOT_REGISTERED", "The requested interface is not registered on the server object.");

        /// <summary>
        /// RPC could not call the server or could not return the results of calling the server.
        /// </summary>
        public static HRESULT RPC_E_FAULT = new HRESULT("0x80010104", "RPC_E_FAULT", "RPC could not call the server or could not return the results of calling the server.");

        /// <summary>
        /// The server threw an exception.
        /// </summary>
        public static HRESULT RPC_E_SERVERFAULT = new HRESULT("0x80010105", "RPC_E_SERVERFAULT", "The server threw an exception.");

        /// <summary>
        /// Cannot change thread mode after it is set.
        /// </summary>
        public static HRESULT RPC_E_CHANGED_MODE = new HRESULT("0x80010106", "RPC_E_CHANGED_MODE", "Cannot change thread mode after it is set.");

        /// <summary>
        /// The method called does not exist on the server.
        /// </summary>
        public static HRESULT RPC_E_INVALIDMETHOD = new HRESULT("0x80010107", "RPC_E_INVALIDMETHOD", "The method called does not exist on the server.");

        /// <summary>
        /// The object invoked has disconnected from its clients.
        /// </summary>
        public static HRESULT RPC_E_DISCONNECTED = new HRESULT("0x80010108", "RPC_E_DISCONNECTED", "The object invoked has disconnected from its clients.");

        /// <summary>
        /// The object invoked chose not to process the call now. Try again later.
        /// </summary>
        public static HRESULT RPC_E_RETRY = new HRESULT("0x80010109", "RPC_E_RETRY", "The object invoked chose not to process the call now. Try again later.");

        /// <summary>
        /// The message filter indicated that the application is busy.
        /// </summary>
        public static HRESULT RPC_E_SERVERCALL_RETRYLATER = new HRESULT("0x8001010A", "RPC_E_SERVERCALL_RETRYLATER", "The message filter indicated that the application is busy.");

        /// <summary>
        /// The message filter rejected the call.
        /// </summary>
        public static HRESULT RPC_E_SERVERCALL_REJECTED = new HRESULT("0x8001010B", "RPC_E_SERVERCALL_REJECTED", "The message filter rejected the call.");

        /// <summary>
        /// A call control interfaces was called with invalid data.
        /// </summary>
        public static HRESULT RPC_E_INVALID_CALLDATA = new HRESULT("0x8001010C", "RPC_E_INVALID_CALLDATA", "A call control interfaces was called with invalid data.");

        /// <summary>
        /// An outgoing call cannot be made since the application is dispatching an input-synchronous call.
        /// </summary>
        public static HRESULT RPC_E_CANTCALLOUT_ININPUTSYNCCALL = new HRESULT("0x8001010D", "RPC_E_CANTCALLOUT_ININPUTSYNCCALL", "An outgoing call cannot be made since the application is dispatching an input-synchronous call.");

        /// <summary>
        /// The application called an interface that was marshalled for a different thread.
        /// </summary>
        public static HRESULT RPC_E_WRONG_THREAD = new HRESULT("0x8001010E", "RPC_E_WRONG_THREAD", "The application called an interface that was marshalled for a different thread.");

        /// <summary>
        /// CoInitialize has not been called on the current thread.
        /// </summary>
        public static HRESULT RPC_E_THREAD_NOT_INIT = new HRESULT("0x8001010F", "RPC_E_THREAD_NOT_INIT", "CoInitialize has not been called on the current thread.");

        /// <summary>
        /// The version of OLE on the client and server machines does not match.
        /// </summary>
        public static HRESULT RPC_E_VERSION_MISMATCH = new HRESULT("0x80010110", "RPC_E_VERSION_MISMATCH", "The version of OLE on the client and server machines does not match.");

        /// <summary>
        /// OLE received a packet with an invalid header.
        /// </summary>
        public static HRESULT RPC_E_INVALID_HEADER = new HRESULT("0x80010111", "RPC_E_INVALID_HEADER", "OLE received a packet with an invalid header.");

        /// <summary>
        /// OLE received a packet with an invalid extension.
        /// </summary>
        public static HRESULT RPC_E_INVALID_EXTENSION = new HRESULT("0x80010112", "RPC_E_INVALID_EXTENSION", "OLE received a packet with an invalid extension.");

        /// <summary>
        /// The requested object or interface does not exist.
        /// </summary>
        public static HRESULT RPC_E_INVALID_IPID = new HRESULT("0x80010113", "RPC_E_INVALID_IPID", "The requested object or interface does not exist.");

        /// <summary>
        /// The requested object does not exist.
        /// </summary>
        public static HRESULT RPC_E_INVALID_OBJECT = new HRESULT("0x80010114", "RPC_E_INVALID_OBJECT", "The requested object does not exist.");

        /// <summary>
        /// OLE has sent a request and is waiting for a reply.
        /// </summary>
        public static HRESULT RPC_S_CALLPENDING = new HRESULT("0x80010115", "RPC_S_CALLPENDING", "OLE has sent a request and is waiting for a reply.");

        /// <summary>
        /// OLE is waiting before retrying a request.
        /// </summary>
        public static HRESULT RPC_S_WAITONTIMER = new HRESULT("0x80010116", "RPC_S_WAITONTIMER", "OLE is waiting before retrying a request.");

        /// <summary>
        /// Call context cannot be accessed after call completed.
        /// </summary>
        public static HRESULT RPC_E_CALL_COMPLETE = new HRESULT("0x80010117", "RPC_E_CALL_COMPLETE", "Call context cannot be accessed after call completed.");

        /// <summary>
        /// Impersonate on unsecure calls is not supported.
        /// </summary>
        public static HRESULT RPC_E_UNSECURE_CALL = new HRESULT("0x80010118", "RPC_E_UNSECURE_CALL", "Impersonate on unsecure calls is not supported.");

        /// <summary>
        /// Security must be initialized before any interfaces are marshalled or unmarshalled. It cannot be changed once initialized.
        /// </summary>
        public static HRESULT RPC_E_TOO_LATE = new HRESULT("0x80010119", "RPC_E_TOO_LATE", "Security must be initialized before any interfaces are marshalled or unmarshalled. It cannot be changed once initialized.");

        /// <summary>
        /// No security packages are installed on this machine or the user is not logged on or there are no compatible security packages between the client and server.
        /// </summary>
        public static HRESULT RPC_E_NO_GOOD_SECURITY_PACKAGES = new HRESULT("0x8001011A", "RPC_E_NO_GOOD_SECURITY_PACKAGES", "No security packages are installed on this machine or the user is not logged on or there are no compatible security packages between the client and server.");

        /// <summary>
        /// Access is denied.
        /// </summary>
        public static HRESULT RPC_E_ACCESS_DENIED = new HRESULT("0x8001011B", "RPC_E_ACCESS_DENIED", "Access is denied.");

        /// <summary>
        /// Remote calls are not allowed for this process.
        /// </summary>
        public static HRESULT RPC_E_REMOTE_DISABLED = new HRESULT("0x8001011C", "RPC_E_REMOTE_DISABLED", "Remote calls are not allowed for this process.");

        /// <summary>
        /// The marshaled interface data packet (OBJREF) has an invalid or unknown format.
        /// </summary>
        public static HRESULT RPC_E_INVALID_OBJREF = new HRESULT("0x8001011D", "RPC_E_INVALID_OBJREF", "The marshaled interface data packet (OBJREF) has an invalid or unknown format.");

        /// <summary>
        /// No context is associated with this call. This happens for some custom marshalled calls and on the client side of the call.
        /// </summary>
        public static HRESULT RPC_E_NO_CONTEXT = new HRESULT("0x8001011E", "RPC_E_NO_CONTEXT", "No context is associated with this call. This happens for some custom marshalled calls and on the client side of the call.");

        /// <summary>
        /// This operation returned because the timeout period expired.
        /// </summary>
        public static HRESULT RPC_E_TIMEOUT = new HRESULT("0x8001011F", "RPC_E_TIMEOUT", "This operation returned because the timeout period expired.");

        /// <summary>
        /// There are no synchronize objects to wait on.
        /// </summary>
        public static HRESULT RPC_E_NO_SYNC = new HRESULT("0x80010120", "RPC_E_NO_SYNC", "There are no synchronize objects to wait on.");

        /// <summary>
        /// Full subject issuer chain SSL principal name expected from the server.
        /// </summary>
        public static HRESULT RPC_E_FULLSIC_REQUIRED = new HRESULT("0x80010121", "RPC_E_FULLSIC_REQUIRED", "Full subject issuer chain SSL principal name expected from the server.");

        /// <summary>
        /// Principal name is not a valid MSSTD name.
        /// </summary>
        public static HRESULT RPC_E_INVALID_STD_NAME = new HRESULT("0x80010122", "RPC_E_INVALID_STD_NAME", "Principal name is not a valid MSSTD name.");

        /// <summary>
        /// Unable to impersonate DCOM client
        /// </summary>
        public static HRESULT CO_E_FAILEDTOIMPERSONATE = new HRESULT("0x80010123", "CO_E_FAILEDTOIMPERSONATE", "Unable to impersonate DCOM client");

        /// <summary>
        /// Unable to obtain server's security context
        /// </summary>
        public static HRESULT CO_E_FAILEDTOGETSECCTX = new HRESULT("0x80010124", "CO_E_FAILEDTOGETSECCTX", "Unable to obtain server's security context");

        /// <summary>
        /// Unable to open the access token of the current thread
        /// </summary>
        public static HRESULT CO_E_FAILEDTOOPENTHREADTOKEN = new HRESULT("0x80010125", "CO_E_FAILEDTOOPENTHREADTOKEN", "Unable to open the access token of the current thread");

        /// <summary>
        /// Unable to obtain user info from an access token
        /// </summary>
        public static HRESULT CO_E_FAILEDTOGETTOKENINFO = new HRESULT("0x80010126", "CO_E_FAILEDTOGETTOKENINFO", "Unable to obtain user info from an access token");

        /// <summary>
        /// The client who called IAccessControl::IsAccessPermitted was not the trustee provided to the method
        /// </summary>
        public static HRESULT CO_E_TRUSTEEDOESNTMATCHCLIENT = new HRESULT("0x80010127", "CO_E_TRUSTEEDOESNTMATCHCLIENT", "The client who called IAccessControl::IsAccessPermitted was not the trustee provided to the method");

        /// <summary>
        /// Unable to obtain the client's security blanket
        /// </summary>
        public static HRESULT CO_E_FAILEDTOQUERYCLIENTBLANKET = new HRESULT("0x80010128", "CO_E_FAILEDTOQUERYCLIENTBLANKET", "Unable to obtain the client's security blanket");

        /// <summary>
        /// Unable to set a discretionary ACL into a security descriptor
        /// </summary>
        public static HRESULT CO_E_FAILEDTOSETDACL = new HRESULT("0x80010129", "CO_E_FAILEDTOSETDACL", "Unable to set a discretionary ACL into a security descriptor");

        /// <summary>
        /// The system function, AccessCheck, returned false
        /// </summary>
        public static HRESULT CO_E_ACCESSCHECKFAILED = new HRESULT("0x8001012A", "CO_E_ACCESSCHECKFAILED", "The system function, AccessCheck, returned false");

        /// <summary>
        /// Either NetAccessDel or NetAccessAdd returned an error code.
        /// </summary>
        public static HRESULT CO_E_NETACCESSAPIFAILED = new HRESULT("0x8001012B", "CO_E_NETACCESSAPIFAILED", "Either NetAccessDel or NetAccessAdd returned an error code.");

        /// <summary>
        /// One of the trustee strings provided by the user did not conform to the \ syntax and it was not the &quot;*&quot; string
        /// </summary>
        public static HRESULT CO_E_WRONGTRUSTEENAMESYNTAX = new HRESULT("0x8001012C", "CO_E_WRONGTRUSTEENAMESYNTAX", "One of the trustee strings provided by the user did not conform to the \\ syntax and it was not the \" * \" string");

        /// <summary>
        /// One of the security identifiers provided by the user was invalid
        /// </summary>
        public static HRESULT CO_E_INVALIDSID = new HRESULT("0x8001012D", "CO_E_INVALIDSID", "One of the security identifiers provided by the user was invalid");

        /// <summary>
        /// Unable to convert a wide character trustee string to a multibyte trustee string
        /// </summary>
        public static HRESULT CO_E_CONVERSIONFAILED = new HRESULT("0x8001012E", "CO_E_CONVERSIONFAILED", "Unable to convert a wide character trustee string to a multibyte trustee string");

        /// <summary>
        /// Unable to find a security identifier that corresponds to a trustee string provided by the user
        /// </summary>
        public static HRESULT CO_E_NOMATCHINGSIDFOUND = new HRESULT("0x8001012F", "CO_E_NOMATCHINGSIDFOUND", "Unable to find a security identifier that corresponds to a trustee string provided by the user");

        /// <summary>
        /// The system function, LookupAccountSID, failed
        /// </summary>
        public static HRESULT CO_E_LOOKUPACCSIDFAILED = new HRESULT("0x80010130", "CO_E_LOOKUPACCSIDFAILED", "The system function, LookupAccountSID, failed");

        /// <summary>
        /// Unable to find a trustee name that corresponds to a security identifier provided by the user
        /// </summary>
        public static HRESULT CO_E_NOMATCHINGNAMEFOUND = new HRESULT("0x80010131", "CO_E_NOMATCHINGNAMEFOUND", "Unable to find a trustee name that corresponds to a security identifier provided by the user");

        /// <summary>
        /// The system function, LookupAccountName, failed
        /// </summary>
        public static HRESULT CO_E_LOOKUPACCNAMEFAILED = new HRESULT("0x80010132", "CO_E_LOOKUPACCNAMEFAILED", "The system function, LookupAccountName, failed");

        /// <summary>
        /// Unable to set or reset a serialization handle
        /// </summary>
        public static HRESULT CO_E_SETSERLHNDLFAILED = new HRESULT("0x80010133", "CO_E_SETSERLHNDLFAILED", "Unable to set or reset a serialization handle");

        /// <summary>
        /// Unable to obtain the Windows directory
        /// </summary>
        public static HRESULT CO_E_FAILEDTOGETWINDIR = new HRESULT("0x80010134", "CO_E_FAILEDTOGETWINDIR", "Unable to obtain the Windows directory");

        /// <summary>
        /// Path too long
        /// </summary>
        public static HRESULT CO_E_PATHTOOLONG = new HRESULT("0x80010135", "CO_E_PATHTOOLONG", "Path too long");

        /// <summary>
        /// Unable to generate a uuid.
        /// </summary>
        public static HRESULT CO_E_FAILEDTOGENUUID = new HRESULT("0x80010136", "CO_E_FAILEDTOGENUUID", "Unable to generate a uuid.");

        /// <summary>
        /// Unable to create file
        /// </summary>
        public static HRESULT CO_E_FAILEDTOCREATEFILE = new HRESULT("0x80010137", "CO_E_FAILEDTOCREATEFILE", "Unable to create file");

        /// <summary>
        /// Unable to close a serialization handle or a file handle.
        /// </summary>
        public static HRESULT CO_E_FAILEDTOCLOSEHANDLE = new HRESULT("0x80010138", "CO_E_FAILEDTOCLOSEHANDLE", "Unable to close a serialization handle or a file handle.");

        /// <summary>
        /// The number of ACEs in an ACL exceeds the system limit.
        /// </summary>
        public static HRESULT CO_E_EXCEEDSYSACLLIMIT = new HRESULT("0x80010139", "CO_E_EXCEEDSYSACLLIMIT", "The number of ACEs in an ACL exceeds the system limit.");

        /// <summary>
        /// Not all the DENY_ACCESS ACEs are arranged in front of the GRANT_ACCESS ACEs in the stream.
        /// </summary>
        public static HRESULT CO_E_ACESINWRONGORDER = new HRESULT("0x8001013A", "CO_E_ACESINWRONGORDER", "Not all the DENY_ACCESS ACEs are arranged in front of the GRANT_ACCESS ACEs in the stream.");

        /// <summary>
        /// The version of ACL format in the stream is not supported by this implementation of IAccessControl
        /// </summary>
        public static HRESULT CO_E_INCOMPATIBLESTREAMVERSION = new HRESULT("0x8001013B", "CO_E_INCOMPATIBLESTREAMVERSION", "The version of ACL format in the stream is not supported by this implementation of IAccessControl");

        /// <summary>
        /// Unable to open the access token of the server process
        /// </summary>
        public static HRESULT CO_E_FAILEDTOOPENPROCESSTOKEN = new HRESULT("0x8001013C", "CO_E_FAILEDTOOPENPROCESSTOKEN", "Unable to open the access token of the server process");

        /// <summary>
        /// Unable to decode the ACL in the stream provided by the user
        /// </summary>
        public static HRESULT CO_E_DECODEFAILED = new HRESULT("0x8001013D", "CO_E_DECODEFAILED", "Unable to decode the ACL in the stream provided by the user");

        /// <summary>
        /// The COM IAccessControl object is not initialized
        /// </summary>
        public static HRESULT CO_E_ACNOTINITIALIZED = new HRESULT("0x8001013F", "CO_E_ACNOTINITIALIZED", "The COM IAccessControl object is not initialized");

        /// <summary>
        /// Call Cancellation is disabled
        /// </summary>
        public static HRESULT CO_E_CANCEL_DISABLED = new HRESULT("0x80010140", "CO_E_CANCEL_DISABLED", "Call Cancellation is disabled");
        #endregion

        #region COM Error Codes (Security and Setup)
        /// <summary>
        /// The specified event is currently not being audited.
        /// </summary>
        public static HRESULT ERROR_AUDITING_DISABLED = new HRESULT("0xC0090001", "ERROR_AUDITING_DISABLED", "The specified event is currently not being audited.");

        /// <summary>
        /// The SID filtering operation removed all SIDs.
        /// </summary>
        public static HRESULT ERROR_ALL_SIDS_FILTERED = new HRESULT("0xC0090002", "ERROR_ALL_SIDS_FILTERED", "The SID filtering operation removed all SIDs.");

        /// <summary>
        /// Business rule scripts are disabled for the calling application.
        /// </summary>
        public static HRESULT ERROR_BIZRULES_NOT_ENABLED = new HRESULT("0xC0090003", "ERROR_BIZRULES_NOT_ENABLED", "Business rule scripts are disabled for the calling application.");

        /// <summary>
        /// The packaging API has encountered an internal error.
        /// </summary>
        public static HRESULT APPX_E_PACKAGING_INTERNAL = new HRESULT("0x80080200", "APPX_E_PACKAGING_INTERNAL", "The packaging API has encountered an internal error.");

        /// <summary>
        /// The file is not a valid package because its contents are interleaved.
        /// </summary>
        public static HRESULT APPX_E_INTERLEAVING_NOT_ALLOWED = new HRESULT("0x80080201", "APPX_E_INTERLEAVING_NOT_ALLOWED", "The file is not a valid package because its contents are interleaved.");

        /// <summary>
        /// The file is not a valid package because it contains OPC relationships.
        /// </summary>
        public static HRESULT APPX_E_RELATIONSHIPS_NOT_ALLOWED = new HRESULT("0x80080202", "APPX_E_RELATIONSHIPS_NOT_ALLOWED", "The file is not a valid package because it contains OPC relationships.");

        /// <summary>
        /// The file is not a valid package because it is missing a manifest or block map, or missing a signature file when the code integrity file is present.
        /// </summary>
        public static HRESULT APPX_E_MISSING_REQUIRED_FILE = new HRESULT("0x80080203", "APPX_E_MISSING_REQUIRED_FILE", "The file is not a valid package because it is missing a manifest or block map, or missing a signature file when the code integrity file is present.");

        /// <summary>
        /// The package's manifest is invalid.
        /// </summary>
        public static HRESULT APPX_E_INVALID_MANIFEST = new HRESULT("0x80080204", "APPX_E_INVALID_MANIFEST", "The package's manifest is invalid.");

        /// <summary>
        /// The package's block map is invalid.
        /// </summary>
        public static HRESULT APPX_E_INVALID_BLOCKMAP = new HRESULT("0x80080205", "APPX_E_INVALID_BLOCKMAP", "The package's block map is invalid.");

        /// <summary>
        /// The package's content cannot be read because it is corrupt.
        /// </summary>
        public static HRESULT APPX_E_CORRUPT_CONTENT = new HRESULT("0x80080206", "APPX_E_CORRUPT_CONTENT", "The package's content cannot be read because it is corrupt.");

        /// <summary>
        /// The computed hash value of the block does not match the one stored in the block map.
        /// </summary>
        public static HRESULT APPX_E_BLOCK_HASH_INVALID = new HRESULT("0x80080207", "APPX_E_BLOCK_HASH_INVALID", "The computed hash value of the block does not match the one stored in the block map.");

        /// <summary>
        /// The requested byte range is over 4GB when translated to byte range of blocks.
        /// </summary>
        public static HRESULT APPX_E_REQUESTED_RANGE_TOO_LARGE = new HRESULT("0x80080208", "APPX_E_REQUESTED_RANGE_TOO_LARGE", "The requested byte range is over 4GB when translated to byte range of blocks.");

        /// <summary>
        /// The SIP_SUBJECTINFO structure used to sign the package didn't contain the required data.
        /// </summary>
        public static HRESULT APPX_E_INVALID_SIP_CLIENT_DATA = new HRESULT("0x80080209", "APPX_E_INVALID_SIP_CLIENT_DATA", "The SIP_SUBJECTINFO structure used to sign the package didn't contain the required data.");

        /// <summary>
        /// The app didn't start in the required time.
        /// </summary>
        public static HRESULT E_APPLICATION_ACTIVATION_TIMED_OUT = new HRESULT("0x8027025A", "E_APPLICATION_ACTIVATION_TIMED_OUT", "The app didn't start in the required time.");

        /// <summary>
        /// The app didn't start.
        /// </summary>
        public static HRESULT E_APPLICATION_ACTIVATION_EXEC_FAILURE = new HRESULT("0x8027025B", "E_APPLICATION_ACTIVATION_EXEC_FAILURE", "The app didn't start.");

        /// <summary>
        /// This app failed to launch because of an issue with its license. Please try again in a moment.
        /// </summary>
        public static HRESULT E_APPLICATION_TEMPORARY_LICENSE_ERROR = new HRESULT("0x8027025C", "E_APPLICATION_TEMPORARY_LICENSE_ERROR", "This app failed to launch because of an issue with its license. Please try again in a moment.");

        /// <summary>
        /// Bad UID.
        /// </summary>
        public static HRESULT NTE_BAD_UID = new HRESULT("0x80090001", "NTE_BAD_UID", "Bad UID.");

        /// <summary>
        /// Bad Hash.
        /// </summary>
        public static HRESULT NTE_BAD_HASH = new HRESULT("0x80090002", "NTE_BAD_HASH", "Bad Hash.");

        /// <summary>
        /// Bad Key.
        /// </summary>
        public static HRESULT NTE_BAD_KEY = new HRESULT("0x80090003", "NTE_BAD_KEY", "Bad Key.");

        /// <summary>
        /// Bad Length.
        /// </summary>
        public static HRESULT NTE_BAD_LEN = new HRESULT("0x80090004", "NTE_BAD_LEN", "Bad Length.");

        /// <summary>
        /// Bad Data.
        /// </summary>
        public static HRESULT NTE_BAD_DATA = new HRESULT("0x80090005", "NTE_BAD_DATA", "Bad Data.");

        /// <summary>
        /// Invalid Signature.
        /// </summary>
        public static HRESULT NTE_BAD_SIGNATURE = new HRESULT("0x80090006", "NTE_BAD_SIGNATURE", "Invalid Signature.");

        /// <summary>
        /// Bad Version of provider.
        /// </summary>
        public static HRESULT NTE_BAD_VER = new HRESULT("0x80090007", "NTE_BAD_VER", "Bad Version of provider.");

        /// <summary>
        /// Invalid algorithm specified.
        /// </summary>
        public static HRESULT NTE_BAD_ALGID = new HRESULT("0x80090008", "NTE_BAD_ALGID", "Invalid algorithm specified.");

        /// <summary>
        /// Invalid flags specified.
        /// </summary>
        public static HRESULT NTE_BAD_FLAGS = new HRESULT("0x80090009", "NTE_BAD_FLAGS", "Invalid flags specified.");

        /// <summary>
        /// Invalid type specified.
        /// </summary>
        public static HRESULT NTE_BAD_TYPE = new HRESULT("0x8009000A", "NTE_BAD_TYPE", "Invalid type specified.");

        /// <summary>
        /// Key not valid for use in specified state.
        /// </summary>
        public static HRESULT NTE_BAD_KEY_STATE = new HRESULT("0x8009000B", "NTE_BAD_KEY_STATE", "Key not valid for use in specified state.");

        /// <summary>
        /// Hash not valid for use in specified state.
        /// </summary>
        public static HRESULT NTE_BAD_HASH_STATE = new HRESULT("0x8009000C", "NTE_BAD_HASH_STATE", "Hash not valid for use in specified state.");

        /// <summary>
        /// Key does not exist.
        /// </summary>
        public static HRESULT NTE_NO_KEY = new HRESULT("0x8009000D", "NTE_NO_KEY", "Key does not exist.");

        /// <summary>
        /// Insufficient memory available for the operation.
        /// </summary>
        public static HRESULT NTE_NO_MEMORY = new HRESULT("0x8009000E", "NTE_NO_MEMORY", "Insufficient memory available for the operation.");

        /// <summary>
        /// Object already exists.
        /// </summary>
        public static HRESULT NTE_EXISTS = new HRESULT("0x8009000F", "NTE_EXISTS", "Object already exists.");

        /// <summary>
        /// Access denied.
        /// </summary>
        public static HRESULT NTE_PERM = new HRESULT("0x80090010", "NTE_PERM", "Access denied.");

        /// <summary>
        /// Object was not found.
        /// </summary>
        public static HRESULT NTE_NOT_FOUND = new HRESULT("0x80090011", "NTE_NOT_FOUND", "Object was not found.");

        /// <summary>
        /// Data already encrypted.
        /// </summary>
        public static HRESULT NTE_DOUBLE_ENCRYPT = new HRESULT("0x80090012", "NTE_DOUBLE_ENCRYPT", "Data already encrypted.");

        /// <summary>
        /// Invalid provider specified.
        /// </summary>
        public static HRESULT NTE_BAD_PROVIDER = new HRESULT("0x80090013", "NTE_BAD_PROVIDER", "Invalid provider specified.");

        /// <summary>
        /// Invalid provider type specified.
        /// </summary>
        public static HRESULT NTE_BAD_PROV_TYPE = new HRESULT("0x80090014", "NTE_BAD_PROV_TYPE", "Invalid provider type specified.");

        /// <summary>
        /// Provider's public key is invalid.
        /// </summary>
        public static HRESULT NTE_BAD_PUBLIC_KEY = new HRESULT("0x80090015", "NTE_BAD_PUBLIC_KEY", "Provider's public key is invalid.");

        /// <summary>
        /// Keyset does not exist
        /// </summary>
        public static HRESULT NTE_BAD_KEYSET = new HRESULT("0x80090016", "NTE_BAD_KEYSET", "Keyset does not exist");

        /// <summary>
        /// Provider type not defined.
        /// </summary>
        public static HRESULT NTE_PROV_TYPE_NOT_DEF = new HRESULT("0x80090017", "NTE_PROV_TYPE_NOT_DEF", "Provider type not defined.");

        /// <summary>
        /// Provider type as registered is invalid.
        /// </summary>
        public static HRESULT NTE_PROV_TYPE_ENTRY_BAD = new HRESULT("0x80090018", "NTE_PROV_TYPE_ENTRY_BAD", "Provider type as registered is invalid.");

        /// <summary>
        /// The keyset is not defined.
        /// </summary>
        public static HRESULT NTE_KEYSET_NOT_DEF = new HRESULT("0x80090019", "NTE_KEYSET_NOT_DEF", "The keyset is not defined.");

        /// <summary>
        /// Keyset as registered is invalid.
        /// </summary>
        public static HRESULT NTE_KEYSET_ENTRY_BAD = new HRESULT("0x8009001A", "NTE_KEYSET_ENTRY_BAD", "Keyset as registered is invalid.");

        /// <summary>
        /// Provider type does not match registered value.
        /// </summary>
        public static HRESULT NTE_PROV_TYPE_NO_MATCH = new HRESULT("0x8009001B", "NTE_PROV_TYPE_NO_MATCH", "Provider type does not match registered value.");

        /// <summary>
        /// The digital signature file is corrupt.
        /// </summary>
        public static HRESULT NTE_SIGNATURE_FILE_BAD = new HRESULT("0x8009001C", "NTE_SIGNATURE_FILE_BAD", "The digital signature file is corrupt.");

        /// <summary>
        /// Provider DLL failed to initialize correctly.
        /// </summary>
        public static HRESULT NTE_PROVIDER_DLL_FAIL = new HRESULT("0x8009001D", "NTE_PROVIDER_DLL_FAIL", "Provider DLL failed to initialize correctly.");

        /// <summary>
        /// Provider DLL could not be found.
        /// </summary>
        public static HRESULT NTE_PROV_DLL_NOT_FOUND = new HRESULT("0x8009001E", "NTE_PROV_DLL_NOT_FOUND", "Provider DLL could not be found.");

        /// <summary>
        /// The Keyset parameter is invalid.
        /// </summary>
        public static HRESULT NTE_BAD_KEYSET_PARAM = new HRESULT("0x8009001F", "NTE_BAD_KEYSET_PARAM", "The Keyset parameter is invalid.");

        /// <summary>
        /// An internal error occurred.
        /// </summary>
        public static HRESULT NTE_FAIL = new HRESULT("0x80090020", "NTE_FAIL", "An internal error occurred.");

        /// <summary>
        /// A base error occurred.
        /// </summary>
        public static HRESULT NTE_SYS_ERR = new HRESULT("0x80090021", "NTE_SYS_ERR", "A base error occurred.");

        /// <summary>
        /// Provider could not perform the action since the context was acquired as silent.
        /// </summary>
        public static HRESULT NTE_SILENT_CONTEXT = new HRESULT("0x80090022", "NTE_SILENT_CONTEXT", "Provider could not perform the action since the context was acquired as silent.");

        /// <summary>
        /// The security token does not have storage space available for an additional container.
        /// </summary>
        public static HRESULT NTE_TOKEN_KEYSET_STORAGE_FULL = new HRESULT("0x80090023", "NTE_TOKEN_KEYSET_STORAGE_FULL", "The security token does not have storage space available for an additional container.");

        /// <summary>
        /// The profile for the user is a temporary profile.
        /// </summary>
        public static HRESULT NTE_TEMPORARY_PROFILE = new HRESULT("0x80090024", "NTE_TEMPORARY_PROFILE", "The profile for the user is a temporary profile.");

        /// <summary>
        /// The key parameters could not be set because the CSP uses fixed parameters.
        /// </summary>
        public static HRESULT NTE_FIXEDPARAMETER = new HRESULT("0x80090025", "NTE_FIXEDPARAMETER", "The key parameters could not be set because the CSP uses fixed parameters.");

        /// <summary>
        /// The supplied handle is invalid.
        /// </summary>
        public static HRESULT NTE_INVALID_HANDLE = new HRESULT("0x80090026", "NTE_INVALID_HANDLE", "The supplied handle is invalid.");

        /// <summary>
        /// The parameter is incorrect.
        /// </summary>
        public static HRESULT NTE_INVALID_PARAMETER = new HRESULT("0x80090027", "NTE_INVALID_PARAMETER", "The parameter is incorrect.");

        /// <summary>
        /// The buffer supplied to a function was too small.
        /// </summary>
        public static HRESULT NTE_BUFFER_TOO_SMALL = new HRESULT("0x80090028", "NTE_BUFFER_TOO_SMALL", "The buffer supplied to a function was too small.");

        /// <summary>
        /// The requested operation is not supported.
        /// </summary>
        public static HRESULT NTE_NOT_SUPPORTED = new HRESULT("0x80090029", "NTE_NOT_SUPPORTED", "The requested operation is not supported.");

        /// <summary>
        /// No more data is available.
        /// </summary>
        public static HRESULT NTE_NO_MORE_ITEMS = new HRESULT("0x8009002A", "NTE_NO_MORE_ITEMS", "No more data is available.");

        /// <summary>
        /// The supplied buffers overlap incorrectly.
        /// </summary>
        public static HRESULT NTE_BUFFERS_OVERLAP = new HRESULT("0x8009002B", "NTE_BUFFERS_OVERLAP", "The supplied buffers overlap incorrectly.");

        /// <summary>
        /// The specified data could not be decrypted.
        /// </summary>
        public static HRESULT NTE_DECRYPTION_FAILURE = new HRESULT("0x8009002C", "NTE_DECRYPTION_FAILURE", "The specified data could not be decrypted.");

        /// <summary>
        /// An internal consistency check failed.
        /// </summary>
        public static HRESULT NTE_INTERNAL_ERROR = new HRESULT("0x8009002D", "NTE_INTERNAL_ERROR", "An internal consistency check failed.");

        /// <summary>
        /// This operation requires input from the user.
        /// </summary>
        public static HRESULT NTE_UI_REQUIRED = new HRESULT("0x8009002E", "NTE_UI_REQUIRED", "This operation requires input from the user.");

        /// <summary>
        /// The cryptographic provider does not support HMAC.
        /// </summary>
        public static HRESULT NTE_HMAC_NOT_SUPPORTED = new HRESULT("0x8009002F", "NTE_HMAC_NOT_SUPPORTED", "The cryptographic provider does not support HMAC.");

        /// <summary>
        /// The device that is required by this cryptographic provider is not ready for use.
        /// </summary>
        public static HRESULT NTE_DEVICE_NOT_READY = new HRESULT("0x80090030", "NTE_DEVICE_NOT_READY", "The device that is required by this cryptographic provider is not ready for use.");

        /// <summary>
        /// The dictionary attack mitigation is triggered and the provided authorization was ignored by the provider.
        /// </summary>
        public static HRESULT NTE_AUTHENTICATION_IGNORED = new HRESULT("0x80090031", "NTE_AUTHENTICATION_IGNORED", "The dictionary attack mitigation is triggered and the provided authorization was ignored by the provider.");

        /// <summary>
        /// The validation of the provided data failed the integrity or signature validation.
        /// </summary>
        public static HRESULT NTE_VALIDATION_FAILED = new HRESULT("0x80090032", "NTE_VALIDATION_FAILED", "The validation of the provided data failed the integrity or signature validation.");

        /// <summary>
        /// Incorrect password.
        /// </summary>
        public static HRESULT NTE_INCORRECT_PASSWORD = new HRESULT("0x80090033", "NTE_INCORRECT_PASSWORD", "Incorrect password.");

        /// <summary>
        /// Encryption failed.
        /// </summary>
        public static HRESULT NTE_ENCRYPTION_FAILURE = new HRESULT("0x80090034", "NTE_ENCRYPTION_FAILURE", "Encryption failed.");

        /// <summary>
        /// Not enough memory is available to complete this request
        /// </summary>
        public static HRESULT SEC_E_INSUFFICIENT_MEMORY = new HRESULT("0x80090300", "SEC_E_INSUFFICIENT_MEMORY", "Not enough memory is available to complete this request");

        /// <summary>
        /// The handle specified is invalid
        /// </summary>
        public static HRESULT SEC_E_INVALID_HANDLE = new HRESULT("0x80090301", "SEC_E_INVALID_HANDLE", "The handle specified is invalid");

        /// <summary>
        /// The function requested is not supported
        /// </summary>
        public static HRESULT SEC_E_UNSUPPORTED_FUNCTION = new HRESULT("0x80090302", "SEC_E_UNSUPPORTED_FUNCTION", "The function requested is not supported");

        /// <summary>
        /// The specified target is unknown or unreachable
        /// </summary>
        public static HRESULT SEC_E_TARGET_UNKNOWN = new HRESULT("0x80090303", "SEC_E_TARGET_UNKNOWN", "The specified target is unknown or unreachable");

        /// <summary>
        /// The Local Security Authority cannot be contacted
        /// </summary>
        public static HRESULT SEC_E_INTERNAL_ERROR = new HRESULT("0x80090304", "SEC_E_INTERNAL_ERROR", "The Local Security Authority cannot be contacted");

        /// <summary>
        /// The requested security package does not exist
        /// </summary>
        public static HRESULT SEC_E_SECPKG_NOT_FOUND = new HRESULT("0x80090305", "SEC_E_SECPKG_NOT_FOUND", "The requested security package does not exist");

        /// <summary>
        /// The caller is not the owner of the desired credentials
        /// </summary>
        public static HRESULT SEC_E_NOT_OWNER = new HRESULT("0x80090306", "SEC_E_NOT_OWNER", "The caller is not the owner of the desired credentials");

        /// <summary>
        /// The security package failed to initialize, and cannot be installed
        /// </summary>
        public static HRESULT SEC_E_CANNOT_INSTALL = new HRESULT("0x80090307", "SEC_E_CANNOT_INSTALL", "The security package failed to initialize, and cannot be installed");

        /// <summary>
        /// The token supplied to the function is invalid
        /// </summary>
        public static HRESULT SEC_E_INVALID_TOKEN = new HRESULT("0x80090308", "SEC_E_INVALID_TOKEN", "The token supplied to the function is invalid");

        /// <summary>
        /// The security package is not able to marshal the logon buffer, so the logon attempt has failed
        /// </summary>
        public static HRESULT SEC_E_CANNOT_PACK = new HRESULT("0x80090309", "SEC_E_CANNOT_PACK", "The security package is not able to marshal the logon buffer, so the logon attempt has failed");

        /// <summary>
        /// The per-message Quality of Protection is not supported by the security package
        /// </summary>
        public static HRESULT SEC_E_QOP_NOT_SUPPORTED = new HRESULT("0x8009030A", "SEC_E_QOP_NOT_SUPPORTED", "The per-message Quality of Protection is not supported by the security package");

        /// <summary>
        /// The security context does not allow impersonation of the client
        /// </summary>
        public static HRESULT SEC_E_NO_IMPERSONATION = new HRESULT("0x8009030B", "SEC_E_NO_IMPERSONATION", "The security context does not allow impersonation of the client");

        /// <summary>
        /// The logon attempt failed
        /// </summary>
        public static HRESULT SEC_E_LOGON_DENIED = new HRESULT("0x8009030C", "SEC_E_LOGON_DENIED", "The logon attempt failed");

        /// <summary>
        /// The credentials supplied to the package were not recognized
        /// </summary>
        public static HRESULT SEC_E_UNKNOWN_CREDENTIALS = new HRESULT("0x8009030D", "SEC_E_UNKNOWN_CREDENTIALS", "The credentials supplied to the package were not recognized");

        /// <summary>
        /// No credentials are available in the security package
        /// </summary>
        public static HRESULT SEC_E_NO_CREDENTIALS = new HRESULT("0x8009030E", "SEC_E_NO_CREDENTIALS", "No credentials are available in the security package");

        /// <summary>
        /// The message or signature supplied for verification has been altered
        /// </summary>
        public static HRESULT SEC_E_MESSAGE_ALTERED = new HRESULT("0x8009030F", "SEC_E_MESSAGE_ALTERED", "The message or signature supplied for verification has been altered");

        /// <summary>
        /// The message supplied for verification is out of sequence
        /// </summary>
        public static HRESULT SEC_E_OUT_OF_SEQUENCE = new HRESULT("0x80090310", "SEC_E_OUT_OF_SEQUENCE", "The message supplied for verification is out of sequence");

        /// <summary>
        /// No authority could be contacted for authentication.
        /// </summary>
        public static HRESULT SEC_E_NO_AUTHENTICATING_AUTHORITY = new HRESULT("0x80090311", "SEC_E_NO_AUTHENTICATING_AUTHORITY", "No authority could be contacted for authentication.");

        /// <summary>
        /// The function completed successfully, but must be called again to complete the context
        /// </summary>
        public static HRESULT SEC_I_CONTINUE_NEEDED = new HRESULT("0x00090312", "SEC_I_CONTINUE_NEEDED", "The function completed successfully, but must be called again to complete the context");

        /// <summary>
        /// The function completed successfully, but CompleteToken must be called
        /// </summary>
        public static HRESULT SEC_I_COMPLETE_NEEDED = new HRESULT("0x00090313", "SEC_I_COMPLETE_NEEDED", "The function completed successfully, but CompleteToken must be called");

        /// <summary>
        /// The function completed successfully, but both CompleteToken and this function must be called to complete the context
        /// </summary>
        public static HRESULT SEC_I_COMPLETE_AND_CONTINUE = new HRESULT("0x00090314", "SEC_I_COMPLETE_AND_CONTINUE", "The function completed successfully, but both CompleteToken and this function must be called to complete the context");

        /// <summary>
        /// The logon was completed, but no network authority was available. The logon was made using locally known information
        /// </summary>
        public static HRESULT SEC_I_LOCAL_LOGON = new HRESULT("0x00090315", "SEC_I_LOCAL_LOGON", "The logon was completed, but no network authority was available. The logon was made using locally known information");

        /// <summary>
        /// The requested security package does not exist
        /// </summary>
        public static HRESULT SEC_E_BAD_PKGID = new HRESULT("0x80090316", "SEC_E_BAD_PKGID", "The requested security package does not exist");

        /// <summary>
        /// The context has expired and can no longer be used.
        /// </summary>
        public static HRESULT SEC_E_CONTEXT_EXPIRED = new HRESULT("0x80090317", "SEC_E_CONTEXT_EXPIRED", "The context has expired and can no longer be used.");

        /// <summary>
        /// The context has expired and can no longer be used.
        /// </summary>
        public static HRESULT SEC_I_CONTEXT_EXPIRED = new HRESULT("0x00090317", "SEC_I_CONTEXT_EXPIRED", "The context has expired and can no longer be used.");

        /// <summary>
        /// The supplied message is incomplete. The signature was not verified.
        /// </summary>
        public static HRESULT SEC_E_INCOMPLETE_MESSAGE = new HRESULT("0x80090318", "SEC_E_INCOMPLETE_MESSAGE", "The supplied message is incomplete. The signature was not verified.");

        /// <summary>
        /// The credentials supplied were not complete, and could not be verified. The context could not be initialized.
        /// </summary>
        public static HRESULT SEC_E_INCOMPLETE_CREDENTIALS = new HRESULT("0x80090320", "SEC_E_INCOMPLETE_CREDENTIALS", "The credentials supplied were not complete, and could not be verified. The context could not be initialized.");

        /// <summary>
        /// The buffers supplied to a function was too small.
        /// </summary>
        public static HRESULT SEC_E_BUFFER_TOO_SMALL = new HRESULT("0x80090321", "SEC_E_BUFFER_TOO_SMALL", "The buffers supplied to a function was too small.");

        /// <summary>
        /// The credentials supplied were not complete, and could not be verified. Additional information can be returned from the context.
        /// </summary>
        public static HRESULT SEC_I_INCOMPLETE_CREDENTIALS = new HRESULT("0x00090320", "SEC_I_INCOMPLETE_CREDENTIALS", "The credentials supplied were not complete, and could not be verified. Additional information can be returned from the context.");

        /// <summary>
        /// The context data must be renegotiated with the peer.
        /// </summary>
        public static HRESULT SEC_I_RENEGOTIATE = new HRESULT("0x00090321", "SEC_I_RENEGOTIATE", "The context data must be renegotiated with the peer.");

        /// <summary>
        /// The target principal name is incorrect.
        /// </summary>
        public static HRESULT SEC_E_WRONG_PRINCIPAL = new HRESULT("0x80090322", "SEC_E_WRONG_PRINCIPAL", "The target principal name is incorrect.");

        /// <summary>
        /// There is no LSA mode context associated with this context.
        /// </summary>
        public static HRESULT SEC_I_NO_LSA_CONTEXT = new HRESULT("0x00090323", "SEC_I_NO_LSA_CONTEXT", "There is no LSA mode context associated with this context.");

        /// <summary>
        /// The clocks on the client and server machines are skewed.
        /// </summary>
        public static HRESULT SEC_E_TIME_SKEW = new HRESULT("0x80090324", "SEC_E_TIME_SKEW", "The clocks on the client and server machines are skewed.");

        /// <summary>
        /// The certificate chain was issued by an authority that is not trusted.
        /// </summary>
        public static HRESULT SEC_E_UNTRUSTED_ROOT = new HRESULT("0x80090325", "SEC_E_UNTRUSTED_ROOT", "The certificate chain was issued by an authority that is not trusted.");

        /// <summary>
        /// The message received was unexpected or badly formatted.
        /// </summary>
        public static HRESULT SEC_E_ILLEGAL_MESSAGE = new HRESULT("0x80090326", "SEC_E_ILLEGAL_MESSAGE", "The message received was unexpected or badly formatted.");

        /// <summary>
        /// An unknown error occurred while processing the certificate.
        /// </summary>
        public static HRESULT SEC_E_CERT_UNKNOWN = new HRESULT("0x80090327", "SEC_E_CERT_UNKNOWN", "An unknown error occurred while processing the certificate.");

        /// <summary>
        /// The received certificate has expired.
        /// </summary>
        public static HRESULT SEC_E_CERT_EXPIRED = new HRESULT("0x80090328", "SEC_E_CERT_EXPIRED", "The received certificate has expired.");

        /// <summary>
        /// The specified data could not be encrypted.
        /// </summary>
        public static HRESULT SEC_E_ENCRYPT_FAILURE = new HRESULT("0x80090329", "SEC_E_ENCRYPT_FAILURE", "The specified data could not be encrypted.");

        /// <summary>
        /// The specified data could not be decrypted.
        /// </summary>
        public static HRESULT SEC_E_DECRYPT_FAILURE = new HRESULT("0x80090330", "SEC_E_DECRYPT_FAILURE", "The specified data could not be decrypted.");

        /// <summary>
        /// The client and server cannot communicate, because they do not possess a common algorithm.
        /// </summary>
        public static HRESULT SEC_E_ALGORITHM_MISMATCH = new HRESULT("0x80090331", "SEC_E_ALGORITHM_MISMATCH", "The client and server cannot communicate, because they do not possess a common algorithm.");

        /// <summary>
        /// The security context could not be established due to a failure in the requested quality of service (e.g. mutual authentication or delegation).
        /// </summary>
        public static HRESULT SEC_E_SECURITY_QOS_FAILED = new HRESULT("0x80090332", "SEC_E_SECURITY_QOS_FAILED", "The security context could not be established due to a failure in the requested quality of service (e.g. mutual authentication or delegation).");

        /// <summary>
        /// A security context was deleted before the context was completed. This is considered a logon failure.
        /// </summary>
        public static HRESULT SEC_E_UNFINISHED_CONTEXT_DELETED = new HRESULT("0x80090333", "SEC_E_UNFINISHED_CONTEXT_DELETED", "A security context was deleted before the context was completed. This is considered a logon failure.");

        /// <summary>
        /// The client is trying to negotiate a context and the server requires user-to-user but didn't send a TGT reply.
        /// </summary>
        public static HRESULT SEC_E_NO_TGT_REPLY = new HRESULT("0x80090334", "SEC_E_NO_TGT_REPLY", "The client is trying to negotiate a context and the server requires user-to-user but didn't send a TGT reply.");

        /// <summary>
        /// Unable to accomplish the requested task because the local machine does not have any IP addresses.
        /// </summary>
        public static HRESULT SEC_E_NO_IP_ADDRESSES = new HRESULT("0x80090335", "SEC_E_NO_IP_ADDRESSES", "Unable to accomplish the requested task because the local machine does not have any IP addresses.");

        /// <summary>
        /// The supplied credential handle does not match the credential associated with the security context.
        /// </summary>
        public static HRESULT SEC_E_WRONG_CREDENTIAL_HANDLE = new HRESULT("0x80090336", "SEC_E_WRONG_CREDENTIAL_HANDLE", "The supplied credential handle does not match the credential associated with the security context.");

        /// <summary>
        /// The crypto system or checksum function is invalid because a required function is unavailable.
        /// </summary>
        public static HRESULT SEC_E_CRYPTO_SYSTEM_INVALID = new HRESULT("0x80090337", "SEC_E_CRYPTO_SYSTEM_INVALID", "The crypto system or checksum function is invalid because a required function is unavailable.");

        /// <summary>
        /// The number of maximum ticket referrals has been exceeded.
        /// </summary>
        public static HRESULT SEC_E_MAX_REFERRALS_EXCEEDED = new HRESULT("0x80090338", "SEC_E_MAX_REFERRALS_EXCEEDED", "The number of maximum ticket referrals has been exceeded.");

        /// <summary>
        /// The local machine must be a Kerberos KDC (domain controller) and it is not.
        /// </summary>
        public static HRESULT SEC_E_MUST_BE_KDC = new HRESULT("0x80090339", "SEC_E_MUST_BE_KDC", "The local machine must be a Kerberos KDC (domain controller) and it is not.");

        /// <summary>
        /// The other end of the security negotiation is requires strong crypto but it is not supported on the local machine.
        /// </summary>
        public static HRESULT SEC_E_STRONG_CRYPTO_NOT_SUPPORTED = new HRESULT("0x8009033A", "SEC_E_STRONG_CRYPTO_NOT_SUPPORTED", "The other end of the security negotiation is requires strong crypto but it is not supported on the local machine.");

        /// <summary>
        /// The KDC reply contained more than one principal name.
        /// </summary>
        public static HRESULT SEC_E_TOO_MANY_PRINCIPALS = new HRESULT("0x8009033B", "SEC_E_TOO_MANY_PRINCIPALS", "The KDC reply contained more than one principal name.");

        /// <summary>
        /// Expected to find PA data for a hint of what etype to use, but it was not found.
        /// </summary>
        public static HRESULT SEC_E_NO_PA_DATA = new HRESULT("0x8009033C", "SEC_E_NO_PA_DATA", "Expected to find PA data for a hint of what etype to use, but it was not found.");

        /// <summary>
        /// The client certificate does not contain a valid UPN, or does not match the client name in the logon request. Please contact your administrator.
        /// </summary>
        public static HRESULT SEC_E_PKINIT_NAME_MISMATCH = new HRESULT("0x8009033D", "SEC_E_PKINIT_NAME_MISMATCH", "The client certificate does not contain a valid UPN, or does not match the client name in the logon request. Please contact your administrator.");

        /// <summary>
        /// Smartcard logon is required and was not used.
        /// </summary>
        public static HRESULT SEC_E_SMARTCARD_LOGON_REQUIRED = new HRESULT("0x8009033E", "SEC_E_SMARTCARD_LOGON_REQUIRED", "Smartcard logon is required and was not used.");

        /// <summary>
        /// A system shutdown is in progress.
        /// </summary>
        public static HRESULT SEC_E_SHUTDOWN_IN_PROGRESS = new HRESULT("0x8009033F", "SEC_E_SHUTDOWN_IN_PROGRESS", "A system shutdown is in progress.");

        /// <summary>
        /// An invalid request was sent to the KDC.
        /// </summary>
        public static HRESULT SEC_E_KDC_INVALID_REQUEST = new HRESULT("0x80090340", "SEC_E_KDC_INVALID_REQUEST", "An invalid request was sent to the KDC.");

        /// <summary>
        /// The KDC was unable to generate a referral for the service requested.
        /// </summary>
        public static HRESULT SEC_E_KDC_UNABLE_TO_REFER = new HRESULT("0x80090341", "SEC_E_KDC_UNABLE_TO_REFER", "The KDC was unable to generate a referral for the service requested.");

        /// <summary>
        /// The encryption type requested is not supported by the KDC.
        /// </summary>
        public static HRESULT SEC_E_KDC_UNKNOWN_ETYPE = new HRESULT("0x80090342", "SEC_E_KDC_UNKNOWN_ETYPE", "The encryption type requested is not supported by the KDC.");

        /// <summary>
        /// An unsupported preauthentication mechanism was presented to the Kerberos package.
        /// </summary>
        public static HRESULT SEC_E_UNSUPPORTED_PREAUTH = new HRESULT("0x80090343", "SEC_E_UNSUPPORTED_PREAUTH", "An unsupported preauthentication mechanism was presented to the Kerberos package.");

        /// <summary>
        /// The requested operation cannot be completed. The computer must be trusted for delegation and the current user account must be configured to allow delegation.
        /// </summary>
        public static HRESULT SEC_E_DELEGATION_REQUIRED = new HRESULT("0x80090345", "SEC_E_DELEGATION_REQUIRED", "The requested operation cannot be completed. The computer must be trusted for delegation and the current user account must be configured to allow delegation.");

        /// <summary>
        /// Client's supplied SSPI channel bindings were incorrect.
        /// </summary>
        public static HRESULT SEC_E_BAD_BINDINGS = new HRESULT("0x80090346", "SEC_E_BAD_BINDINGS", "Client's supplied SSPI channel bindings were incorrect.");

        /// <summary>
        /// The received certificate was mapped to multiple accounts.
        /// </summary>
        public static HRESULT SEC_E_MULTIPLE_ACCOUNTS = new HRESULT("0x80090347", "SEC_E_MULTIPLE_ACCOUNTS", "The received certificate was mapped to multiple accounts.");

        /// <summary>
        /// SEC_E_NO_KERB_KEY
        /// </summary>
        public static HRESULT SEC_E_NO_KERB_KEY = new HRESULT("0x80090348", "SEC_E_NO_KERB_KEY", "SEC_E_NO_KERB_KEY");

        /// <summary>
        /// The certificate is not valid for the requested usage.
        /// </summary>
        public static HRESULT SEC_E_CERT_WRONG_USAGE = new HRESULT("0x80090349", "SEC_E_CERT_WRONG_USAGE", "The certificate is not valid for the requested usage.");

        /// <summary>
        /// The system cannot contact a domain controller to service the authentication request. Please try again later.
        /// </summary>
        public static HRESULT SEC_E_DOWNGRADE_DETECTED = new HRESULT("0x80090350", "SEC_E_DOWNGRADE_DETECTED", "The system cannot contact a domain controller to service the authentication request. Please try again later.");

        /// <summary>
        /// The smartcard certificate used for authentication has been revoked. Please contact your system administrator. There may be additional information in the event log.
        /// </summary>
        public static HRESULT SEC_E_SMARTCARD_CERT_REVOKED = new HRESULT("0x80090351", "SEC_E_SMARTCARD_CERT_REVOKED", "The smartcard certificate used for authentication has been revoked. Please contact your system administrator. There may be additional information in the event log.");

        /// <summary>
        /// An untrusted certificate authority was detected While processing the smartcard certificate used for authentication. Please contact your system administrator.
        /// </summary>
        public static HRESULT SEC_E_ISSUING_CA_UNTRUSTED = new HRESULT("0x80090352", "SEC_E_ISSUING_CA_UNTRUSTED", "An untrusted certificate authority was detected While processing the smartcard certificate used for authentication. Please contact your system administrator.");

        /// <summary>
        /// The revocation status of the smartcard certificate used for authentication could not be determined. Please contact your system administrator.
        /// </summary>
        public static HRESULT SEC_E_REVOCATION_OFFLINE_C = new HRESULT("0x80090353", "SEC_E_REVOCATION_OFFLINE_C", "The revocation status of the smartcard certificate used for authentication could not be determined. Please contact your system administrator.");

        /// <summary>
        /// The smartcard certificate used for authentication was not trusted. Please contact your system administrator.
        /// </summary>
        public static HRESULT SEC_E_PKINIT_CLIENT_FAILURE = new HRESULT("0x80090354", "SEC_E_PKINIT_CLIENT_FAILURE", "The smartcard certificate used for authentication was not trusted. Please contact your system administrator.");

        /// <summary>
        /// The smartcard certificate used for authentication has expired. Please contact your system administrator.
        /// </summary>
        public static HRESULT SEC_E_SMARTCARD_CERT_EXPIRED = new HRESULT("0x80090355", "SEC_E_SMARTCARD_CERT_EXPIRED", "The smartcard certificate used for authentication has expired. Please contact your system administrator.");

        /// <summary>
        /// The Kerberos subsystem encountered an error. A service for user protocol request was made against a domain controller which does not support service for user.
        /// </summary>
        public static HRESULT SEC_E_NO_S4U_PROT_SUPPORT = new HRESULT("0x80090356", "SEC_E_NO_S4U_PROT_SUPPORT", "The Kerberos subsystem encountered an error. A service for user protocol request was made against a domain controller which does not support service for user.");

        /// <summary>
        /// An attempt was made by this server to make a Kerberos constrained delegation request for a target outside of the server's realm. This is not supported, and indicates a misconfiguration on this server's allowed to delegate to list. Please contact your administrator.
        /// </summary>
        public static HRESULT SEC_E_CROSSREALM_DELEGATION_FAILURE = new HRESULT("0x80090357", "SEC_E_CROSSREALM_DELEGATION_FAILURE", "An attempt was made by this server to make a Kerberos constrained delegation request for a target outside of the server's realm. This is not supported, and indicates a misconfiguration on this server's allowed to delegate to list. Please contact your administrator.");

        /// <summary>
        /// The revocation status of the domain controller certificate used for smartcard authentication could not be determined. There is additional information in the system event log. Please contact your system administrator.
        /// </summary>
        public static HRESULT SEC_E_REVOCATION_OFFLINE_KDC = new HRESULT("0x80090358", "SEC_E_REVOCATION_OFFLINE_KDC", "The revocation status of the domain controller certificate used for smartcard authentication could not be determined. There is additional information in the system event log. Please contact your system administrator.");

        /// <summary>
        /// An untrusted certificate authority was detected while processing the domain controller certificate used for authentication. There is additional information in the system event log. Please contact your system administrator.
        /// </summary>
        public static HRESULT SEC_E_ISSUING_CA_UNTRUSTED_KDC = new HRESULT("0x80090359", "SEC_E_ISSUING_CA_UNTRUSTED_KDC", "An untrusted certificate authority was detected while processing the domain controller certificate used for authentication. There is additional information in the system event log. Please contact your system administrator.");

        /// <summary>
        /// The domain controller certificate used for smartcard logon has expired. Please contact your system administrator with the contents of your system event log.
        /// </summary>
        public static HRESULT SEC_E_KDC_CERT_EXPIRED = new HRESULT("0x8009035A", "SEC_E_KDC_CERT_EXPIRED", "The domain controller certificate used for smartcard logon has expired. Please contact your system administrator with the contents of your system event log.");

        /// <summary>
        /// The domain controller certificate used for smartcard logon has been revoked. Please contact your system administrator with the contents of your system event log.
        /// </summary>
        public static HRESULT SEC_E_KDC_CERT_REVOKED = new HRESULT("0x8009035B", "SEC_E_KDC_CERT_REVOKED", "The domain controller certificate used for smartcard logon has been revoked. Please contact your system administrator with the contents of your system event log.");

        /// <summary>
        /// A signature operation must be performed before the user can authenticate.
        /// </summary>
        public static HRESULT SEC_I_SIGNATURE_NEEDED = new HRESULT("0x0009035C", "SEC_I_SIGNATURE_NEEDED", "A signature operation must be performed before the user can authenticate.");

        /// <summary>
        /// One or more of the parameters passed to the function was invalid.
        /// </summary>
        public static HRESULT SEC_E_INVALID_PARAMETER = new HRESULT("0x8009035D", "SEC_E_INVALID_PARAMETER", "One or more of the parameters passed to the function was invalid.");

        /// <summary>
        /// Client policy does not allow credential delegation to target server.
        /// </summary>
        public static HRESULT SEC_E_DELEGATION_POLICY = new HRESULT("0x8009035E", "SEC_E_DELEGATION_POLICY", "Client policy does not allow credential delegation to target server.");

        /// <summary>
        /// Client policy does not allow credential delegation to target server with NLTM only authentication.
        /// </summary>
        public static HRESULT SEC_E_POLICY_NLTM_ONLY = new HRESULT("0x8009035F", "SEC_E_POLICY_NLTM_ONLY", "Client policy does not allow credential delegation to target server with NLTM only authentication.");

        /// <summary>
        /// The recipient rejected the renegotiation request.
        /// </summary>
        public static HRESULT SEC_I_NO_RENEGOTIATION = new HRESULT("0x00090360", "SEC_I_NO_RENEGOTIATION", "The recipient rejected the renegotiation request.");

        /// <summary>
        /// The required security context does not exist.
        /// </summary>
        public static HRESULT SEC_E_NO_CONTEXT = new HRESULT("0x80090361", "SEC_E_NO_CONTEXT", "The required security context does not exist.");

        /// <summary>
        /// The PKU2U protocol encountered an error while attempting to utilize the associated certificates.
        /// </summary>
        public static HRESULT SEC_E_PKU2U_CERT_FAILURE = new HRESULT("0x80090362", "SEC_E_PKU2U_CERT_FAILURE", "The PKU2U protocol encountered an error while attempting to utilize the associated certificates.");

        /// <summary>
        /// The identity of the server computer could not be verified.
        /// </summary>
        public static HRESULT SEC_E_MUTUAL_AUTH_FAILED = new HRESULT("0x80090363", "SEC_E_MUTUAL_AUTH_FAILED", "The identity of the server computer could not be verified.");

        /// <summary>
        /// The returned buffer is only a fragment of the message. More fragments need to be returned.
        /// </summary>
        public static HRESULT SEC_I_MESSAGE_FRAGMENT = new HRESULT("0x00090364", "SEC_I_MESSAGE_FRAGMENT", "The returned buffer is only a fragment of the message. More fragments need to be returned.");

        /// <summary>
        /// Only https scheme is allowed.
        /// </summary>
        public static HRESULT SEC_E_ONLY_HTTPS_ALLOWED = new HRESULT("0x80090365", "SEC_E_ONLY_HTTPS_ALLOWED", "Only https scheme is allowed.");

        /// <summary>
        /// The function completed successfully, but must be called again to complete the context. Early start can be used.
        /// </summary>
        public static HRESULT SEC_I_CONTINUE_NEEDED_MESSAGE_OK = new HRESULT("0x80090366", "SEC_I_CONTINUE_NEEDED_MESSAGE_OK", "The function completed successfully, but must be called again to complete the context. Early start can be used.");

        /// <summary>
        /// An error occurred while performing an operation on a cryptographic message.
        /// </summary>
        public static HRESULT CRYPT_E_MSG_ERROR = new HRESULT("0x80091001", "CRYPT_E_MSG_ERROR", "An error occurred while performing an operation on a cryptographic message.");

        /// <summary>
        /// Unknown cryptographic algorithm.
        /// </summary>
        public static HRESULT CRYPT_E_UNKNOWN_ALGO = new HRESULT("0x80091002", "CRYPT_E_UNKNOWN_ALGO", "Unknown cryptographic algorithm.");

        /// <summary>
        /// The object identifier is poorly formatted.
        /// </summary>
        public static HRESULT CRYPT_E_OID_FORMAT = new HRESULT("0x80091003", "CRYPT_E_OID_FORMAT", "The object identifier is poorly formatted.");

        /// <summary>
        /// Invalid cryptographic message type.
        /// </summary>
        public static HRESULT CRYPT_E_INVALID_MSG_TYPE = new HRESULT("0x80091004", "CRYPT_E_INVALID_MSG_TYPE", "Invalid cryptographic message type.");

        /// <summary>
        /// Unexpected cryptographic message encoding.
        /// </summary>
        public static HRESULT CRYPT_E_UNEXPECTED_ENCODING = new HRESULT("0x80091005", "CRYPT_E_UNEXPECTED_ENCODING", "Unexpected cryptographic message encoding.");

        /// <summary>
        /// The cryptographic message does not contain an expected authenticated attribute.
        /// </summary>
        public static HRESULT CRYPT_E_AUTH_ATTR_MISSING = new HRESULT("0x80091006", "CRYPT_E_AUTH_ATTR_MISSING", "The cryptographic message does not contain an expected authenticated attribute.");

        /// <summary>
        /// The hash value is not correct.
        /// </summary>
        public static HRESULT CRYPT_E_HASH_VALUE = new HRESULT("0x80091007", "CRYPT_E_HASH_VALUE", "The hash value is not correct.");

        /// <summary>
        /// The index value is not valid.
        /// </summary>
        public static HRESULT CRYPT_E_INVALID_INDEX = new HRESULT("0x80091008", "CRYPT_E_INVALID_INDEX", "The index value is not valid.");

        /// <summary>
        /// The content of the cryptographic message has already been decrypted.
        /// </summary>
        public static HRESULT CRYPT_E_ALREADY_DECRYPTED = new HRESULT("0x80091009", "CRYPT_E_ALREADY_DECRYPTED", "The content of the cryptographic message has already been decrypted.");

        /// <summary>
        /// The content of the cryptographic message has not been decrypted yet.
        /// </summary>
        public static HRESULT CRYPT_E_NOT_DECRYPTED = new HRESULT("0x8009100A", "CRYPT_E_NOT_DECRYPTED", "The content of the cryptographic message has not been decrypted yet.");

        /// <summary>
        /// The enveloped-data message does not contain the specified recipient.
        /// </summary>
        public static HRESULT CRYPT_E_RECIPIENT_NOT_FOUND = new HRESULT("0x8009100B", "CRYPT_E_RECIPIENT_NOT_FOUND", "The enveloped-data message does not contain the specified recipient.");

        /// <summary>
        /// Invalid control type.
        /// </summary>
        public static HRESULT CRYPT_E_CONTROL_TYPE = new HRESULT("0x8009100C", "CRYPT_E_CONTROL_TYPE", "Invalid control type.");

        /// <summary>
        /// Invalid issuer and/or serial number.
        /// </summary>
        public static HRESULT CRYPT_E_ISSUER_SERIALNUMBER = new HRESULT("0x8009100D", "CRYPT_E_ISSUER_SERIALNUMBER", "Invalid issuer and/or serial number.");

        /// <summary>
        /// Cannot find the original signer.
        /// </summary>
        public static HRESULT CRYPT_E_SIGNER_NOT_FOUND = new HRESULT("0x8009100E", "CRYPT_E_SIGNER_NOT_FOUND", "Cannot find the original signer.");

        /// <summary>
        /// The cryptographic message does not contain all of the requested attributes.
        /// </summary>
        public static HRESULT CRYPT_E_ATTRIBUTES_MISSING = new HRESULT("0x8009100F", "CRYPT_E_ATTRIBUTES_MISSING", "The cryptographic message does not contain all of the requested attributes.");

        /// <summary>
        /// The streamed cryptographic message is not ready to return data.
        /// </summary>
        public static HRESULT CRYPT_E_STREAM_MSG_NOT_READY = new HRESULT("0x80091010", "CRYPT_E_STREAM_MSG_NOT_READY", "The streamed cryptographic message is not ready to return data.");

        /// <summary>
        /// The streamed cryptographic message requires more data to complete the decode operation.
        /// </summary>
        public static HRESULT CRYPT_E_STREAM_INSUFFICIENT_DATA = new HRESULT("0x80091011", "CRYPT_E_STREAM_INSUFFICIENT_DATA", "The streamed cryptographic message requires more data to complete the decode operation.");

        /// <summary>
        /// The protected data needs to be re-protected.
        /// </summary>
        public static HRESULT CRYPT_I_NEW_PROTECTION_REQUIRED = new HRESULT("0x00091012", "CRYPT_I_NEW_PROTECTION_REQUIRED", "The protected data needs to be re-protected.");

        /// <summary>
        /// The length specified for the output data was insufficient.
        /// </summary>
        public static HRESULT CRYPT_E_BAD_LEN = new HRESULT("0x80092001", "CRYPT_E_BAD_LEN", "The length specified for the output data was insufficient.");

        /// <summary>
        /// An error occurred during encode or decode operation.
        /// </summary>
        public static HRESULT CRYPT_E_BAD_ENCODE = new HRESULT("0x80092002", "CRYPT_E_BAD_ENCODE", "An error occurred during encode or decode operation.");

        /// <summary>
        /// An error occurred while reading or writing to a file.
        /// </summary>
        public static HRESULT CRYPT_E_FILE_ERROR = new HRESULT("0x80092003", "CRYPT_E_FILE_ERROR", "An error occurred while reading or writing to a file.");

        /// <summary>
        /// Cannot find object or property.
        /// </summary>
        public static HRESULT CRYPT_E_NOT_FOUND = new HRESULT("0x80092004", "CRYPT_E_NOT_FOUND", "Cannot find object or property.");

        /// <summary>
        /// The object or property already exists.
        /// </summary>
        public static HRESULT CRYPT_E_EXISTS = new HRESULT("0x80092005", "CRYPT_E_EXISTS", "The object or property already exists.");

        /// <summary>
        /// No provider was specified for the store or object.
        /// </summary>
        public static HRESULT CRYPT_E_NO_PROVIDER = new HRESULT("0x80092006", "CRYPT_E_NO_PROVIDER", "No provider was specified for the store or object.");

        /// <summary>
        /// The specified certificate is self signed.
        /// </summary>
        public static HRESULT CRYPT_E_SELF_SIGNED = new HRESULT("0x80092007", "CRYPT_E_SELF_SIGNED", "The specified certificate is self signed.");

        /// <summary>
        /// The previous certificate or CRL context was deleted.
        /// </summary>
        public static HRESULT CRYPT_E_DELETED_PREV = new HRESULT("0x80092008", "CRYPT_E_DELETED_PREV", "The previous certificate or CRL context was deleted.");

        /// <summary>
        /// Cannot find the requested object.
        /// </summary>
        public static HRESULT CRYPT_E_NO_MATCH = new HRESULT("0x80092009", "CRYPT_E_NO_MATCH", "Cannot find the requested object.");

        /// <summary>
        /// The certificate does not have a property that references a private key.
        /// </summary>
        public static HRESULT CRYPT_E_UNEXPECTED_MSG_TYPE = new HRESULT("0x8009200A", "CRYPT_E_UNEXPECTED_MSG_TYPE", "The certificate does not have a property that references a private key.");

        /// <summary>
        /// Cannot find the certificate and private key for decryption.
        /// </summary>
        public static HRESULT CRYPT_E_NO_KEY_PROPERTY = new HRESULT("0x8009200B", "CRYPT_E_NO_KEY_PROPERTY", "Cannot find the certificate and private key for decryption.");

        /// <summary>
        /// Cannot find the certificate and private key to use for decryption.
        /// </summary>
        public static HRESULT CRYPT_E_NO_DECRYPT_CERT = new HRESULT("0x8009200C", "CRYPT_E_NO_DECRYPT_CERT", "Cannot find the certificate and private key to use for decryption.");

        /// <summary>
        /// Not a cryptographic message or the cryptographic message is not formatted correctly.
        /// </summary>
        public static HRESULT CRYPT_E_BAD_MSG = new HRESULT("0x8009200D", "CRYPT_E_BAD_MSG", "Not a cryptographic message or the cryptographic message is not formatted correctly.");

        /// <summary>
        /// The signed cryptographic message does not have a signer for the specified signer index.
        /// </summary>
        public static HRESULT CRYPT_E_NO_SIGNER = new HRESULT("0x8009200E", "CRYPT_E_NO_SIGNER", "The signed cryptographic message does not have a signer for the specified signer index.");

        /// <summary>
        /// Final closure is pending until additional frees or closes.
        /// </summary>
        public static HRESULT CRYPT_E_PENDING_CLOSE = new HRESULT("0x8009200F", "CRYPT_E_PENDING_CLOSE", "Final closure is pending until additional frees or closes.");

        /// <summary>
        /// The certificate is revoked.
        /// </summary>
        public static HRESULT CRYPT_E_REVOKED = new HRESULT("0x80092010", "CRYPT_E_REVOKED", "The certificate is revoked.");

        /// <summary>
        /// No Dll or exported function was found to verify revocation.
        /// </summary>
        public static HRESULT CRYPT_E_NO_REVOCATION_DLL = new HRESULT("0x80092011", "CRYPT_E_NO_REVOCATION_DLL", "No Dll or exported function was found to verify revocation.");

        /// <summary>
        /// The revocation function was unable to check revocation for the certificate.
        /// </summary>
        public static HRESULT CRYPT_E_NO_REVOCATION_CHECK = new HRESULT("0x80092012", "CRYPT_E_NO_REVOCATION_CHECK", "The revocation function was unable to check revocation for the certificate.");

        /// <summary>
        /// The revocation function was unable to check revocation because the revocation server was offline.
        /// </summary>
        public static HRESULT CRYPT_E_REVOCATION_OFFLINE = new HRESULT("0x80092013", "CRYPT_E_REVOCATION_OFFLINE", "The revocation function was unable to check revocation because the revocation server was offline.");

        /// <summary>
        /// The certificate is not in the revocation server's database.
        /// </summary>
        public static HRESULT CRYPT_E_NOT_IN_REVOCATION_DATABASE = new HRESULT("0x80092014", "CRYPT_E_NOT_IN_REVOCATION_DATABASE", "The certificate is not in the revocation server's database.");

        /// <summary>
        /// The string contains a non-numeric character.
        /// </summary>
        public static HRESULT CRYPT_E_INVALID_NUMERIC_STRING = new HRESULT("0x80092020", "CRYPT_E_INVALID_NUMERIC_STRING", "The string contains a non-numeric character.");

        /// <summary>
        /// The string contains a non-printable character.
        /// </summary>
        public static HRESULT CRYPT_E_INVALID_PRINTABLE_STRING = new HRESULT("0x80092021", "CRYPT_E_INVALID_PRINTABLE_STRING", "The string contains a non-printable character.");

        /// <summary>
        /// The string contains a character not in the 7 bit ASCII character set.
        /// </summary>
        public static HRESULT CRYPT_E_INVALID_IA5_STRING = new HRESULT("0x80092022", "CRYPT_E_INVALID_IA5_STRING", "The string contains a character not in the 7 bit ASCII character set.");

        /// <summary>
        /// The string contains an invalid X500 name attribute key, oid, value or delimiter.
        /// </summary>
        public static HRESULT CRYPT_E_INVALID_X500_STRING = new HRESULT("0x80092023", "CRYPT_E_INVALID_X500_STRING", "The string contains an invalid X500 name attribute key, oid, value or delimiter.");

        /// <summary>
        /// The dwValueType for the CERT_NAME_VALUE is not one of the character strings. Most likely it is either a CERT_RDN_ENCODED_BLOB or CERT_RDN_OCTET_STRING.
        /// </summary>
        public static HRESULT CRYPT_E_NOT_CHAR_STRING = new HRESULT("0x80092024", "CRYPT_E_NOT_CHAR_STRING", "The dwValueType for the CERT_NAME_VALUE is not one of the character strings. Most likely it is either a CERT_RDN_ENCODED_BLOB or CERT_RDN_OCTET_STRING.");

        /// <summary>
        /// The Put operation cannot continue. The file needs to be resized. However, there is already a signature present. A complete signing operation must be done.
        /// </summary>
        public static HRESULT CRYPT_E_FILERESIZED = new HRESULT("0x80092025", "CRYPT_E_FILERESIZED", "The Put operation cannot continue. The file needs to be resized. However, there is already a signature present. A complete signing operation must be done.");

        /// <summary>
        /// The cryptographic operation failed due to a local security option setting.
        /// </summary>
        public static HRESULT CRYPT_E_SECURITY_SETTINGS = new HRESULT("0x80092026", "CRYPT_E_SECURITY_SETTINGS", "The cryptographic operation failed due to a local security option setting.");

        /// <summary>
        /// No DLL or exported function was found to verify subject usage.
        /// </summary>
        public static HRESULT CRYPT_E_NO_VERIFY_USAGE_DLL = new HRESULT("0x80092027", "CRYPT_E_NO_VERIFY_USAGE_DLL", "No DLL or exported function was found to verify subject usage.");

        /// <summary>
        /// The called function was unable to do a usage check on the subject.
        /// </summary>
        public static HRESULT CRYPT_E_NO_VERIFY_USAGE_CHECK = new HRESULT("0x80092028", "CRYPT_E_NO_VERIFY_USAGE_CHECK", "The called function was unable to do a usage check on the subject.");

        /// <summary>
        /// Since the server was offline, the called function was unable to complete the usage check.
        /// </summary>
        public static HRESULT CRYPT_E_VERIFY_USAGE_OFFLINE = new HRESULT("0x80092029", "CRYPT_E_VERIFY_USAGE_OFFLINE", "Since the server was offline, the called function was unable to complete the usage check.");

        /// <summary>
        /// The subject was not found in a Certificate Trust List (CTL).
        /// </summary>
        public static HRESULT CRYPT_E_NOT_IN_CTL = new HRESULT("0x8009202A", "CRYPT_E_NOT_IN_CTL", "The subject was not found in a Certificate Trust List (CTL).");

        /// <summary>
        /// None of the signers of the cryptographic message or certificate trust list is trusted.
        /// </summary>
        public static HRESULT CRYPT_E_NO_TRUSTED_SIGNER = new HRESULT("0x8009202B", "CRYPT_E_NO_TRUSTED_SIGNER", "None of the signers of the cryptographic message or certificate trust list is trusted.");

        /// <summary>
        /// The public key's algorithm parameters are missing.
        /// </summary>
        public static HRESULT CRYPT_E_MISSING_PUBKEY_PARA = new HRESULT("0x8009202C", "CRYPT_E_MISSING_PUBKEY_PARA", "The public key's algorithm parameters are missing.");

        /// <summary>
        /// An object could not be located using the object locator infrastructure with the given name.
        /// </summary>
        public static HRESULT CRYPT_E_OBJECT_LOCATOR_NOT_FOUND = new HRESULT("0x8009202d", "CRYPT_E_OBJECT_LOCATOR_NOT_FOUND", "An object could not be located using the object locator infrastructure with the given name.");

        /// <summary>
        /// OSS Certificate encode/decode error code base See asn1code.h for a definition of the OSS runtime errors. The OSS error values are offset by CRYPT_E_OSS_ERROR.
        /// </summary>
        public static HRESULT CRYPT_E_OSS_ERROR = new HRESULT("0x80093000", "CRYPT_E_OSS_ERROR", "OSS Certificate encode/decode error code base See asn1code.h for a definition of the OSS runtime errors. The OSS error values are offset by CRYPT_E_OSS_ERROR.");

        /// <summary>
        /// OSS ASN.1 Error: Output Buffer is too small.
        /// </summary>
        public static HRESULT OSS_MORE_BUF = new HRESULT("0x80093001", "OSS_MORE_BUF", "OSS ASN.1 Error: Output Buffer is too small.");

        /// <summary>
        /// OSS ASN.1 Error: Signed integer is encoded as a unsigned integer.
        /// </summary>
        public static HRESULT OSS_NEGATIVE_UINTEGER = new HRESULT("0x80093002", "OSS_NEGATIVE_UINTEGER", "OSS ASN.1 Error: Signed integer is encoded as a unsigned integer.");

        /// <summary>
        /// OSS ASN.1 Error: Unknown ASN.1 data type.
        /// </summary>
        public static HRESULT OSS_PDU_RANGE = new HRESULT("0x80093003", "OSS_PDU_RANGE", "OSS ASN.1 Error: Unknown ASN.1 data type.");

        /// <summary>
        /// OSS ASN.1 Error: Output buffer is too small, the decoded data has been truncated.
        /// </summary>
        public static HRESULT OSS_MORE_INPUT = new HRESULT("0x80093004", "OSS_MORE_INPUT", "OSS ASN.1 Error: Output buffer is too small, the decoded data has been truncated.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_DATA_ERROR = new HRESULT("0x80093005", "OSS_DATA_ERROR", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid argument.
        /// </summary>
        public static HRESULT OSS_BAD_ARG = new HRESULT("0x80093006", "OSS_BAD_ARG", "OSS ASN.1 Error: Invalid argument.");

        /// <summary>
        /// OSS ASN.1 Error: Encode/Decode version mismatch.
        /// </summary>
        public static HRESULT OSS_BAD_VERSION = new HRESULT("0x80093007", "OSS_BAD_VERSION", "OSS ASN.1 Error: Encode/Decode version mismatch.");

        /// <summary>
        /// OSS ASN.1 Error: Out of memory.
        /// </summary>
        public static HRESULT OSS_OUT_MEMORY = new HRESULT("0x80093008", "OSS_OUT_MEMORY", "OSS ASN.1 Error: Out of memory.");

        /// <summary>
        /// OSS ASN.1 Error: Encode/Decode Error.
        /// </summary>
        public static HRESULT OSS_PDU_MISMATCH = new HRESULT("0x80093009", "OSS_PDU_MISMATCH", "OSS ASN.1 Error: Encode/Decode Error.");

        /// <summary>
        /// OSS ASN.1 Error: Internal Error.
        /// </summary>
        public static HRESULT OSS_LIMITED = new HRESULT("0x8009300A", "OSS_LIMITED", "OSS ASN.1 Error: Internal Error.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_BAD_PTR = new HRESULT("0x8009300B", "OSS_BAD_PTR", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_BAD_TIME = new HRESULT("0x8009300C", "OSS_BAD_TIME", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Unsupported BER indefinite-length encoding.
        /// </summary>
        public static HRESULT OSS_INDEFINITE_NOT_SUPPORTED = new HRESULT("0x8009300D", "OSS_INDEFINITE_NOT_SUPPORTED", "OSS ASN.1 Error: Unsupported BER indefinite-length encoding.");

        /// <summary>
        /// OSS ASN.1 Error: Access violation.
        /// </summary>
        public static HRESULT OSS_MEM_ERROR = new HRESULT("0x8009300E", "OSS_MEM_ERROR", "OSS ASN.1 Error: Access violation.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_BAD_TABLE = new HRESULT("0x8009300F", "OSS_BAD_TABLE", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_TOO_LONG = new HRESULT("0x80093010", "OSS_TOO_LONG", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_CONSTRAINT_VIOLATED = new HRESULT("0x80093011", "OSS_CONSTRAINT_VIOLATED", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Internal Error.
        /// </summary>
        public static HRESULT OSS_FATAL_ERROR = new HRESULT("0x80093012", "OSS_FATAL_ERROR", "OSS ASN.1 Error: Internal Error.");

        /// <summary>
        /// OSS ASN.1 Error: Multi-threading conflict.
        /// </summary>
        public static HRESULT OSS_ACCESS_SERIALIZATION_ERROR = new HRESULT("0x80093013", "OSS_ACCESS_SERIALIZATION_ERROR", "OSS ASN.1 Error: Multi-threading conflict.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_NULL_TBL = new HRESULT("0x80093014", "OSS_NULL_TBL", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_NULL_FCN = new HRESULT("0x80093015", "OSS_NULL_FCN", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_BAD_ENCRULES = new HRESULT("0x80093016", "OSS_BAD_ENCRULES", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Encode/Decode function not implemented.
        /// </summary>
        public static HRESULT OSS_UNAVAIL_ENCRULES = new HRESULT("0x80093017", "OSS_UNAVAIL_ENCRULES", "OSS ASN.1 Error: Encode/Decode function not implemented.");

        /// <summary>
        /// OSS ASN.1 Error: Trace file error.
        /// </summary>
        public static HRESULT OSS_CANT_OPEN_TRACE_WINDOW = new HRESULT("0x80093018", "OSS_CANT_OPEN_TRACE_WINDOW", "OSS ASN.1 Error: Trace file error.");

        /// <summary>
        /// OSS ASN.1 Error: Function not implemented.
        /// </summary>
        public static HRESULT OSS_UNIMPLEMENTED = new HRESULT("0x80093019", "OSS_UNIMPLEMENTED", "OSS ASN.1 Error: Function not implemented.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_OID_DLL_NOT_LINKED = new HRESULT("0x8009301A", "OSS_OID_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Trace file error.
        /// </summary>
        public static HRESULT OSS_CANT_OPEN_TRACE_FILE = new HRESULT("0x8009301B", "OSS_CANT_OPEN_TRACE_FILE", "OSS ASN.1 Error: Trace file error.");

        /// <summary>
        /// OSS ASN.1 Error: Trace file error.
        /// </summary>
        public static HRESULT OSS_TRACE_FILE_ALREADY_OPEN = new HRESULT("0x8009301C", "OSS_TRACE_FILE_ALREADY_OPEN", "OSS ASN.1 Error: Trace file error.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_TABLE_MISMATCH = new HRESULT("0x8009301D", "OSS_TABLE_MISMATCH", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Invalid data.
        /// </summary>
        public static HRESULT OSS_TYPE_NOT_SUPPORTED = new HRESULT("0x8009301E", "OSS_TYPE_NOT_SUPPORTED", "OSS ASN.1 Error: Invalid data.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_REAL_DLL_NOT_LINKED = new HRESULT("0x8009301F", "OSS_REAL_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_REAL_CODE_NOT_LINKED = new HRESULT("0x80093020", "OSS_REAL_CODE_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_OUT_OF_RANGE = new HRESULT("0x80093021", "OSS_OUT_OF_RANGE", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_COPIER_DLL_NOT_LINKED = new HRESULT("0x80093022", "OSS_COPIER_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_CONSTRAINT_DLL_NOT_LINKED = new HRESULT("0x80093023", "OSS_CONSTRAINT_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_COMPARATOR_DLL_NOT_LINKED = new HRESULT("0x80093024", "OSS_COMPARATOR_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_COMPARATOR_CODE_NOT_LINKED = new HRESULT("0x80093025", "OSS_COMPARATOR_CODE_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_MEM_MGR_DLL_NOT_LINKED = new HRESULT("0x80093026", "OSS_MEM_MGR_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_PDV_DLL_NOT_LINKED = new HRESULT("0x80093027", "OSS_PDV_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_PDV_CODE_NOT_LINKED = new HRESULT("0x80093028", "OSS_PDV_CODE_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_API_DLL_NOT_LINKED = new HRESULT("0x80093029", "OSS_API_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_BERDER_DLL_NOT_LINKED = new HRESULT("0x8009302A", "OSS_BERDER_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_PER_DLL_NOT_LINKED = new HRESULT("0x8009302B", "OSS_PER_DLL_NOT_LINKED", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: Program link error.
        /// </summary>
        public static HRESULT OSS_OPEN_TYPE_ERROR = new HRESULT("0x8009302C", "OSS_OPEN_TYPE_ERROR", "OSS ASN.1 Error: Program link error.");

        /// <summary>
        /// OSS ASN.1 Error: System resource error.
        /// </summary>
        public static HRESULT OSS_MUTEX_NOT_CREATED = new HRESULT("0x8009302D", "OSS_MUTEX_NOT_CREATED", "OSS ASN.1 Error: System resource error.");

        /// <summary>
        /// OSS ASN.1 Error: Trace file error.
        /// </summary>
        public static HRESULT OSS_CANT_CLOSE_TRACE_FILE = new HRESULT("0x8009302E", "OSS_CANT_CLOSE_TRACE_FILE", "OSS ASN.1 Error: Trace file error.");

        /// <summary>
        /// ASN1 Certificate encode/decode error code base. The ASN1 error values are offset by CRYPT_E_ASN1_ERROR.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_ERROR = new HRESULT("0x80093100", "CRYPT_E_ASN1_ERROR", "ASN1 Certificate encode/decode error code base. The ASN1 error values are offset by CRYPT_E_ASN1_ERROR.");

        /// <summary>
        /// ASN1 internal encode or decode error.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_INTERNAL = new HRESULT("0x80093101", "CRYPT_E_ASN1_INTERNAL", "ASN1 internal encode or decode error.");

        /// <summary>
        /// ASN1 unexpected end of data.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_EOD = new HRESULT("0x80093102", "CRYPT_E_ASN1_EOD", "ASN1 unexpected end of data.");

        /// <summary>
        /// ASN1 corrupted data.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_CORRUPT = new HRESULT("0x80093103", "CRYPT_E_ASN1_CORRUPT", "ASN1 corrupted data.");

        /// <summary>
        /// ASN1 value too large.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_LARGE = new HRESULT("0x80093104", "CRYPT_E_ASN1_LARGE", "ASN1 value too large.");

        /// <summary>
        /// ASN1 constraint violated.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_CONSTRAINT = new HRESULT("0x80093105", "CRYPT_E_ASN1_CONSTRAINT", "ASN1 constraint violated.");

        /// <summary>
        /// ASN1 out of memory.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_MEMORY = new HRESULT("0x80093106", "CRYPT_E_ASN1_MEMORY", "ASN1 out of memory.");

        /// <summary>
        /// ASN1 buffer overflow.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_OVERFLOW = new HRESULT("0x80093107", "CRYPT_E_ASN1_OVERFLOW", "ASN1 buffer overflow.");

        /// <summary>
        /// ASN1 function not supported for this PDU.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_BADPDU = new HRESULT("0x80093108", "CRYPT_E_ASN1_BADPDU", "ASN1 function not supported for this PDU.");

        /// <summary>
        /// ASN1 bad arguments to function call.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_BADARGS = new HRESULT("0x80093109", "CRYPT_E_ASN1_BADARGS", "ASN1 bad arguments to function call.");

        /// <summary>
        /// ASN1 bad real value.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_BADREAL = new HRESULT("0x8009310A", "CRYPT_E_ASN1_BADREAL", "ASN1 bad real value.");

        /// <summary>
        /// ASN1 bad tag value met.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_BADTAG = new HRESULT("0x8009310B", "CRYPT_E_ASN1_BADTAG", "ASN1 bad tag value met.");

        /// <summary>
        /// ASN1 bad choice value.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_CHOICE = new HRESULT("0x8009310C", "CRYPT_E_ASN1_CHOICE", "ASN1 bad choice value.");

        /// <summary>
        /// ASN1 bad encoding rule.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_RULE = new HRESULT("0x8009310D", "CRYPT_E_ASN1_RULE", "ASN1 bad encoding rule.");

        /// <summary>
        /// ASN1 bad unicode (UTF8).
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_UTF8 = new HRESULT("0x8009310E", "CRYPT_E_ASN1_UTF8", "ASN1 bad unicode (UTF8).");

        /// <summary>
        /// ASN1 bad PDU type.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_PDU_TYPE = new HRESULT("0x80093133", "CRYPT_E_ASN1_PDU_TYPE", "ASN1 bad PDU type.");

        /// <summary>
        /// ASN1 not yet implemented.
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_NYI = new HRESULT("0x80093134", "CRYPT_E_ASN1_NYI", "ASN1 not yet implemented.");

        /// <summary>
        /// ASN1 skipped unknown extension(s).
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_EXTENDED = new HRESULT("0x80093201", "CRYPT_E_ASN1_EXTENDED", "ASN1 skipped unknown extension(s).");

        /// <summary>
        /// ASN1 end of data expected
        /// </summary>
        public static HRESULT CRYPT_E_ASN1_NOEOD = new HRESULT("0x80093202", "CRYPT_E_ASN1_NOEOD", "ASN1 end of data expected");

        /// <summary>
        /// The request subject name is invalid or too long.
        /// </summary>
        public static HRESULT CERTSRV_E_BAD_REQUESTSUBJECT = new HRESULT("0x80094001", "CERTSRV_E_BAD_REQUESTSUBJECT", "The request subject name is invalid or too long.");

        /// <summary>
        /// The request does not exist.
        /// </summary>
        public static HRESULT CERTSRV_E_NO_REQUEST = new HRESULT("0x80094002", "CERTSRV_E_NO_REQUEST", "The request does not exist.");

        /// <summary>
        /// The request's current status does not allow this operation.
        /// </summary>
        public static HRESULT CERTSRV_E_BAD_REQUESTSTATUS = new HRESULT("0x80094003", "CERTSRV_E_BAD_REQUESTSTATUS", "The request's current status does not allow this operation.");

        /// <summary>
        /// The requested property value is empty.
        /// </summary>
        public static HRESULT CERTSRV_E_PROPERTY_EMPTY = new HRESULT("0x80094004", "CERTSRV_E_PROPERTY_EMPTY", "The requested property value is empty.");

        /// <summary>
        /// The certification authority's certificate contains invalid data.
        /// </summary>
        public static HRESULT CERTSRV_E_INVALID_CA_CERTIFICATE = new HRESULT("0x80094005", "CERTSRV_E_INVALID_CA_CERTIFICATE", "The certification authority's certificate contains invalid data.");

        /// <summary>
        /// Certificate service has been suspended for a database restore operation.
        /// </summary>
        public static HRESULT CERTSRV_E_SERVER_SUSPENDED = new HRESULT("0x80094006", "CERTSRV_E_SERVER_SUSPENDED", "Certificate service has been suspended for a database restore operation.");

        /// <summary>
        /// The certificate contains an encoded length that is potentially incompatible with older enrollment software.
        /// </summary>
        public static HRESULT CERTSRV_E_ENCODING_LENGTH = new HRESULT("0x80094007", "CERTSRV_E_ENCODING_LENGTH", "The certificate contains an encoded length that is potentially incompatible with older enrollment software.");

        /// <summary>
        /// The operation is denied. The user has multiple roles assigned and the certification authority is configured to enforce role separation.
        /// </summary>
        public static HRESULT CERTSRV_E_ROLECONFLICT = new HRESULT("0x80094008", "CERTSRV_E_ROLECONFLICT", "The operation is denied. The user has multiple roles assigned and the certification authority is configured to enforce role separation.");

        /// <summary>
        /// The operation is denied. It can only be performed by a certificate manager that is allowed to manage certificates for the current requester.
        /// </summary>
        public static HRESULT CERTSRV_E_RESTRICTEDOFFICER = new HRESULT("0x80094009", "CERTSRV_E_RESTRICTEDOFFICER", "The operation is denied. It can only be performed by a certificate manager that is allowed to manage certificates for the current requester.");

        /// <summary>
        /// Cannot archive private key. The certification authority is not configured for key archival.
        /// </summary>
        public static HRESULT CERTSRV_E_KEY_ARCHIVAL_NOT_CONFIGURED = new HRESULT("0x8009400A", "CERTSRV_E_KEY_ARCHIVAL_NOT_CONFIGURED", "Cannot archive private key. The certification authority is not configured for key archival.");

        /// <summary>
        /// Cannot archive private key. The certification authority could not verify one or more key recovery certificates.
        /// </summary>
        public static HRESULT CERTSRV_E_NO_VALID_KRA = new HRESULT("0x8009400B", "CERTSRV_E_NO_VALID_KRA", "Cannot archive private key. The certification authority could not verify one or more key recovery certificates.");

        /// <summary>
        /// The request is incorrectly formatted. The encrypted private key must be in an unauthenticated attribute in an outermost signature.
        /// </summary>
        public static HRESULT CERTSRV_E_BAD_REQUEST_KEY_ARCHIVAL = new HRESULT("0x8009400C", "CERTSRV_E_BAD_REQUEST_KEY_ARCHIVAL", "The request is incorrectly formatted. The encrypted private key must be in an unauthenticated attribute in an outermost signature.");

        /// <summary>
        /// At least one security principal must have the permission to manage this CA.
        /// </summary>
        public static HRESULT CERTSRV_E_NO_CAADMIN_DEFINED = new HRESULT("0x8009400D", "CERTSRV_E_NO_CAADMIN_DEFINED", "At least one security principal must have the permission to manage this CA.");

        /// <summary>
        /// The request contains an invalid renewal certificate attribute.
        /// </summary>
        public static HRESULT CERTSRV_E_BAD_RENEWAL_CERT_ATTRIBUTE = new HRESULT("0x8009400E", "CERTSRV_E_BAD_RENEWAL_CERT_ATTRIBUTE", "The request contains an invalid renewal certificate attribute.");

        /// <summary>
        /// An attempt was made to open a Certification Authority database session, but there are already too many active sessions. The server may need to be configured to allow additional sessions.
        /// </summary>
        public static HRESULT CERTSRV_E_NO_DB_SESSIONS = new HRESULT("0x8009400F", "CERTSRV_E_NO_DB_SESSIONS", "An attempt was made to open a Certification Authority database session, but there are already too many active sessions. The server may need to be configured to allow additional sessions.");

        /// <summary>
        /// A memory reference caused a data alignment fault.
        /// </summary>
        public static HRESULT CERTSRV_E_ALIGNMENT_FAULT = new HRESULT("0x80094010", "CERTSRV_E_ALIGNMENT_FAULT", "A memory reference caused a data alignment fault.");

        /// <summary>
        /// The permissions on this certification authority do not allow the current user to enroll for certificates.
        /// </summary>
        public static HRESULT CERTSRV_E_ENROLL_DENIED = new HRESULT("0x80094011", "CERTSRV_E_ENROLL_DENIED", "The permissions on this certification authority do not allow the current user to enroll for certificates.");

        /// <summary>
        /// The permissions on the certificate template do not allow the current user to enroll for this type of certificate.
        /// </summary>
        public static HRESULT CERTSRV_E_TEMPLATE_DENIED = new HRESULT("0x80094012", "CERTSRV_E_TEMPLATE_DENIED", "The permissions on the certificate template do not allow the current user to enroll for this type of certificate.");

        /// <summary>
        /// The contacted domain controller cannot support signed LDAP traffic. Update the domain controller or configure Certificate Services to use SSL for Active Directory access.
        /// </summary>
        public static HRESULT CERTSRV_E_DOWNLEVEL_DC_SSL_OR_UPGRADE = new HRESULT("0x80094013", "CERTSRV_E_DOWNLEVEL_DC_SSL_OR_UPGRADE", "The contacted domain controller cannot support signed LDAP traffic. Update the domain controller or configure Certificate Services to use SSL for Active Directory access.");

        /// <summary>
        /// The request was denied by a certificate manager or CA administrator.
        /// </summary>
        public static HRESULT CERTSRV_E_ADMIN_DENIED_REQUEST = new HRESULT("0x80094014", "CERTSRV_E_ADMIN_DENIED_REQUEST", "The request was denied by a certificate manager or CA administrator.");

        /// <summary>
        /// An enrollment policy server cannot be located.
        /// </summary>
        public static HRESULT CERTSRV_E_NO_POLICY_SERVER = new HRESULT("0x80094015", "CERTSRV_E_NO_POLICY_SERVER", "An enrollment policy server cannot be located.");

        /// <summary>
        /// The requested certificate template is not supported by this CA.
        /// </summary>
        public static HRESULT CERTSRV_E_UNSUPPORTED_CERT_TYPE = new HRESULT("0x80094800", "CERTSRV_E_UNSUPPORTED_CERT_TYPE", "The requested certificate template is not supported by this CA.");

        /// <summary>
        /// The request contains no certificate template information.
        /// </summary>
        public static HRESULT CERTSRV_E_NO_CERT_TYPE = new HRESULT("0x80094801", "CERTSRV_E_NO_CERT_TYPE", "The request contains no certificate template information.");

        /// <summary>
        /// The request contains conflicting template information.
        /// </summary>
        public static HRESULT CERTSRV_E_TEMPLATE_CONFLICT = new HRESULT("0x80094802", "CERTSRV_E_TEMPLATE_CONFLICT", "The request contains conflicting template information.");

        /// <summary>
        /// The request is missing a required Subject Alternate name extension.
        /// </summary>
        public static HRESULT CERTSRV_E_SUBJECT_ALT_NAME_REQUIRED = new HRESULT("0x80094803", "CERTSRV_E_SUBJECT_ALT_NAME_REQUIRED", "The request is missing a required Subject Alternate name extension.");

        /// <summary>
        /// The request is missing a required private key for archival by the server.
        /// </summary>
        public static HRESULT CERTSRV_E_ARCHIVED_KEY_REQUIRED = new HRESULT("0x80094804", "CERTSRV_E_ARCHIVED_KEY_REQUIRED", "The request is missing a required private key for archival by the server.");

        /// <summary>
        /// The request is missing a required SMIME capabilities extension.
        /// </summary>
        public static HRESULT CERTSRV_E_SMIME_REQUIRED = new HRESULT("0x80094805", "CERTSRV_E_SMIME_REQUIRED", "The request is missing a required SMIME capabilities extension.");

        /// <summary>
        /// The request was made on behalf of a subject other than the caller. The certificate template must be configured to require at least one signature to authorize the request.
        /// </summary>
        public static HRESULT CERTSRV_E_BAD_RENEWAL_SUBJECT = new HRESULT("0x80094806", "CERTSRV_E_BAD_RENEWAL_SUBJECT", "The request was made on behalf of a subject other than the caller. The certificate template must be configured to require at least one signature to authorize the request.");

        /// <summary>
        /// The request template version is newer than the supported template version.
        /// </summary>
        public static HRESULT CERTSRV_E_BAD_TEMPLATE_VERSION = new HRESULT("0x80094807", "CERTSRV_E_BAD_TEMPLATE_VERSION", "The request template version is newer than the supported template version.");

        /// <summary>
        /// The template is missing a required signature policy attribute.
        /// </summary>
        public static HRESULT CERTSRV_E_TEMPLATE_POLICY_REQUIRED = new HRESULT("0x80094808", "CERTSRV_E_TEMPLATE_POLICY_REQUIRED", "The template is missing a required signature policy attribute.");

        /// <summary>
        /// The request is missing required signature policy information.
        /// </summary>
        public static HRESULT CERTSRV_E_SIGNATURE_POLICY_REQUIRED = new HRESULT("0x80094809", "CERTSRV_E_SIGNATURE_POLICY_REQUIRED", "The request is missing required signature policy information.");

        /// <summary>
        /// The request is missing one or more required signatures.
        /// </summary>
        public static HRESULT CERTSRV_E_SIGNATURE_COUNT = new HRESULT("0x8009480A", "CERTSRV_E_SIGNATURE_COUNT", "The request is missing one or more required signatures.");

        /// <summary>
        /// One or more signatures did not include the required application or issuance policies. The request is missing one or more required valid signatures.
        /// </summary>
        public static HRESULT CERTSRV_E_SIGNATURE_REJECTED = new HRESULT("0x8009480B", "CERTSRV_E_SIGNATURE_REJECTED", "One or more signatures did not include the required application or issuance policies. The request is missing one or more required valid signatures.");

        /// <summary>
        /// The request is missing one or more required signature issuance policies.
        /// </summary>
        public static HRESULT CERTSRV_E_ISSUANCE_POLICY_REQUIRED = new HRESULT("0x8009480C", "CERTSRV_E_ISSUANCE_POLICY_REQUIRED", "The request is missing one or more required signature issuance policies.");

        /// <summary>
        /// The UPN is unavailable and cannot be added to the Subject Alternate name.
        /// </summary>
        public static HRESULT CERTSRV_E_SUBJECT_UPN_REQUIRED = new HRESULT("0x8009480D", "CERTSRV_E_SUBJECT_UPN_REQUIRED", "The UPN is unavailable and cannot be added to the Subject Alternate name.");

        /// <summary>
        /// The Active Directory GUID is unavailable and cannot be added to the Subject Alternate name.
        /// </summary>
        public static HRESULT CERTSRV_E_SUBJECT_DIRECTORY_GUID_REQUIRED = new HRESULT("0x8009480E", "CERTSRV_E_SUBJECT_DIRECTORY_GUID_REQUIRED", "The Active Directory GUID is unavailable and cannot be added to the Subject Alternate name.");

        /// <summary>
        /// The DNS name is unavailable and cannot be added to the Subject Alternate name.
        /// </summary>
        public static HRESULT CERTSRV_E_SUBJECT_DNS_REQUIRED = new HRESULT("0x8009480F", "CERTSRV_E_SUBJECT_DNS_REQUIRED", "The DNS name is unavailable and cannot be added to the Subject Alternate name.");

        /// <summary>
        /// The request includes a private key for archival by the server, but key archival is not enabled for the specified certificate template.
        /// </summary>
        public static HRESULT CERTSRV_E_ARCHIVED_KEY_UNEXPECTED = new HRESULT("0x80094810", "CERTSRV_E_ARCHIVED_KEY_UNEXPECTED", "The request includes a private key for archival by the server, but key archival is not enabled for the specified certificate template.");

        /// <summary>
        /// The public key does not meet the minimum size required by the specified certificate template.
        /// </summary>
        public static HRESULT CERTSRV_E_KEY_LENGTH = new HRESULT("0x80094811", "CERTSRV_E_KEY_LENGTH", "The public key does not meet the minimum size required by the specified certificate template.");

        /// <summary>
        /// The EMail name is unavailable and cannot be added to the Subject or Subject Alternate name.
        /// </summary>
        public static HRESULT CERTSRV_E_SUBJECT_EMAIL_REQUIRED = new HRESULT("0x80094812", "CERTSRV_E_SUBJECT_EMAIL_REQUIRED", "The EMail name is unavailable and cannot be added to the Subject or Subject Alternate name.");

        /// <summary>
        /// One or more certificate templates to be enabled on this certification authority could not be found.
        /// </summary>
        public static HRESULT CERTSRV_E_UNKNOWN_CERT_TYPE = new HRESULT("0x80094813", "CERTSRV_E_UNKNOWN_CERT_TYPE", "One or more certificate templates to be enabled on this certification authority could not be found.");

        /// <summary>
        /// The certificate template renewal period is longer than the certificate validity period. The template should be reconfigured or the CA certificate renewed.
        /// </summary>
        public static HRESULT CERTSRV_E_CERT_TYPE_OVERLAP = new HRESULT("0x80094814", "CERTSRV_E_CERT_TYPE_OVERLAP", "The certificate template renewal period is longer than the certificate validity period. The template should be reconfigured or the CA certificate renewed.");

        /// <summary>
        /// The certificate template requires too many RA signatures. Only one RA signature is allowed.
        /// </summary>
        public static HRESULT CERTSRV_E_TOO_MANY_SIGNATURES = new HRESULT("0x80094815", "CERTSRV_E_TOO_MANY_SIGNATURES", "The certificate template requires too many RA signatures. Only one RA signature is allowed.");

        /// <summary>
        /// The certificate template requires renewal with the same public key, but the request uses a different public key.
        /// </summary>
        public static HRESULT CERTSRV_E_RENEWAL_BAD_PUBLIC_KEY = new HRESULT("0x80094816", "CERTSRV_E_RENEWAL_BAD_PUBLIC_KEY", "The certificate template requires renewal with the same public key, but the request uses a different public key.");

        /// <summary>
        /// The key is not exportable.
        /// </summary>
        public static HRESULT XENROLL_E_KEY_NOT_EXPORTABLE = new HRESULT("0x80095000", "XENROLL_E_KEY_NOT_EXPORTABLE", "The key is not exportable.");

        /// <summary>
        /// You cannot add the root CA certificate into your local store.
        /// </summary>
        public static HRESULT XENROLL_E_CANNOT_ADD_ROOT_CERT = new HRESULT("0x80095001", "XENROLL_E_CANNOT_ADD_ROOT_CERT", "You cannot add the root CA certificate into your local store.");

        /// <summary>
        /// The key archival hash attribute was not found in the response.
        /// </summary>
        public static HRESULT XENROLL_E_RESPONSE_KA_HASH_NOT_FOUND = new HRESULT("0x80095002", "XENROLL_E_RESPONSE_KA_HASH_NOT_FOUND", "The key archival hash attribute was not found in the response.");

        /// <summary>
        /// An unexpected key archival hash attribute was found in the response.
        /// </summary>
        public static HRESULT XENROLL_E_RESPONSE_UNEXPECTED_KA_HASH = new HRESULT("0x80095003", "XENROLL_E_RESPONSE_UNEXPECTED_KA_HASH", "An unexpected key archival hash attribute was found in the response.");

        /// <summary>
        /// There is a key archival hash mismatch between the request and the response.
        /// </summary>
        public static HRESULT XENROLL_E_RESPONSE_KA_HASH_MISMATCH = new HRESULT("0x80095004", "XENROLL_E_RESPONSE_KA_HASH_MISMATCH", "There is a key archival hash mismatch between the request and the response.");

        /// <summary>
        /// Signing certificate cannot include SMIME extension.
        /// </summary>
        public static HRESULT XENROLL_E_KEYSPEC_SMIME_MISMATCH = new HRESULT("0x80095005", "XENROLL_E_KEYSPEC_SMIME_MISMATCH", "Signing certificate cannot include SMIME extension.");

        /// <summary>
        /// A system-level error occurred while verifying trust.
        /// </summary>
        public static HRESULT TRUST_E_SYSTEM_ERROR = new HRESULT("0x80096001", "TRUST_E_SYSTEM_ERROR", "A system-level error occurred while verifying trust.");

        /// <summary>
        /// The certificate for the signer of the message is invalid or not found.
        /// </summary>
        public static HRESULT TRUST_E_NO_SIGNER_CERT = new HRESULT("0x80096002", "TRUST_E_NO_SIGNER_CERT", "The certificate for the signer of the message is invalid or not found.");

        /// <summary>
        /// One of the counter signatures was invalid.
        /// </summary>
        public static HRESULT TRUST_E_COUNTER_SIGNER = new HRESULT("0x80096003", "TRUST_E_COUNTER_SIGNER", "One of the counter signatures was invalid.");

        /// <summary>
        /// The signature of the certificate cannot be verified.
        /// </summary>
        public static HRESULT TRUST_E_CERT_SIGNATURE = new HRESULT("0x80096004", "TRUST_E_CERT_SIGNATURE", "The signature of the certificate cannot be verified.");

        /// <summary>
        /// The timestamp signature and/or certificate could not be verified or is malformed.
        /// </summary>
        public static HRESULT TRUST_E_TIME_STAMP = new HRESULT("0x80096005", "TRUST_E_TIME_STAMP", "The timestamp signature and/or certificate could not be verified or is malformed.");

        /// <summary>
        /// The digital signature of the object did not verify.
        /// </summary>
        public static HRESULT TRUST_E_BAD_DIGEST = new HRESULT("0x80096010", "TRUST_E_BAD_DIGEST", "The digital signature of the object did not verify.");

        /// <summary>
        /// A certificate's basic constraint extension has not been observed.
        /// </summary>
        public static HRESULT TRUST_E_BASIC_CONSTRAINTS = new HRESULT("0x80096019", "TRUST_E_BASIC_CONSTRAINTS", "A certificate's basic constraint extension has not been observed.");

        /// <summary>
        /// The certificate does not meet or contain the Authenticode(tm) financial extensions.
        /// </summary>
        public static HRESULT TRUST_E_FINANCIAL_CRITERIA = new HRESULT("0x8009601E", "TRUST_E_FINANCIAL_CRITERIA", "The certificate does not meet or contain the Authenticode(tm) financial extensions.");

        /// <summary>
        /// Tried to reference a part of the file outside the proper range.
        /// </summary>
        public static HRESULT MSSIPOTF_E_OUTOFMEMRANGE = new HRESULT("0x80097001", "MSSIPOTF_E_OUTOFMEMRANGE", "Tried to reference a part of the file outside the proper range.");

        /// <summary>
        /// Could not retrieve an object from the file.
        /// </summary>
        public static HRESULT MSSIPOTF_E_CANTGETOBJECT = new HRESULT("0x80097002", "MSSIPOTF_E_CANTGETOBJECT", "Could not retrieve an object from the file.");

        /// <summary>
        /// Could not find the head table in the file.
        /// </summary>
        public static HRESULT MSSIPOTF_E_NOHEADTABLE = new HRESULT("0x80097003", "MSSIPOTF_E_NOHEADTABLE", "Could not find the head table in the file.");

        /// <summary>
        /// The magic number in the head table is incorrect.
        /// </summary>
        public static HRESULT MSSIPOTF_E_BAD_MAGICNUMBER = new HRESULT("0x80097004", "MSSIPOTF_E_BAD_MAGICNUMBER", "The magic number in the head table is incorrect.");

        /// <summary>
        /// The offset table has incorrect values.
        /// </summary>
        public static HRESULT MSSIPOTF_E_BAD_OFFSET_TABLE = new HRESULT("0x80097005", "MSSIPOTF_E_BAD_OFFSET_TABLE", "The offset table has incorrect values.");

        /// <summary>
        /// Duplicate table tags or tags out of alphabetical order.
        /// </summary>
        public static HRESULT MSSIPOTF_E_TABLE_TAGORDER = new HRESULT("0x80097006", "MSSIPOTF_E_TABLE_TAGORDER", "Duplicate table tags or tags out of alphabetical order.");

        /// <summary>
        /// A table does not start on a long word boundary.
        /// </summary>
        public static HRESULT MSSIPOTF_E_TABLE_LONGWORD = new HRESULT("0x80097007", "MSSIPOTF_E_TABLE_LONGWORD", "A table does not start on a long word boundary.");

        /// <summary>
        /// First table does not appear after header information.
        /// </summary>
        public static HRESULT MSSIPOTF_E_BAD_FIRST_TABLE_PLACEMENT = new HRESULT("0x80097008", "MSSIPOTF_E_BAD_FIRST_TABLE_PLACEMENT", "First table does not appear after header information.");

        /// <summary>
        /// Two or more tables overlap.
        /// </summary>
        public static HRESULT MSSIPOTF_E_TABLES_OVERLAP = new HRESULT("0x80097009", "MSSIPOTF_E_TABLES_OVERLAP", "Two or more tables overlap.");

        /// <summary>
        /// Too many pad bytes between tables or pad bytes are not 0.
        /// </summary>
        public static HRESULT MSSIPOTF_E_TABLE_PADBYTES = new HRESULT("0x8009700A", "MSSIPOTF_E_TABLE_PADBYTES", "Too many pad bytes between tables or pad bytes are not 0.");

        /// <summary>
        /// File is too small to contain the last table.
        /// </summary>
        public static HRESULT MSSIPOTF_E_FILETOOSMALL = new HRESULT("0x8009700B", "MSSIPOTF_E_FILETOOSMALL", "File is too small to contain the last table.");

        /// <summary>
        /// A table checksum is incorrect.
        /// </summary>
        public static HRESULT MSSIPOTF_E_TABLE_CHECKSUM = new HRESULT("0x8009700C", "MSSIPOTF_E_TABLE_CHECKSUM", "A table checksum is incorrect.");

        /// <summary>
        /// The file checksum is incorrect.
        /// </summary>
        public static HRESULT MSSIPOTF_E_FILE_CHECKSUM = new HRESULT("0x8009700D", "MSSIPOTF_E_FILE_CHECKSUM", "The file checksum is incorrect.");

        /// <summary>
        /// The signature does not have the correct attributes for the policy.
        /// </summary>
        public static HRESULT MSSIPOTF_E_FAILED_POLICY = new HRESULT("0x80097010", "MSSIPOTF_E_FAILED_POLICY", "The signature does not have the correct attributes for the policy.");

        /// <summary>
        /// The file did not pass the hints check.
        /// </summary>
        public static HRESULT MSSIPOTF_E_FAILED_HINTS_CHECK = new HRESULT("0x80097011", "MSSIPOTF_E_FAILED_HINTS_CHECK", "The file did not pass the hints check.");

        /// <summary>
        /// The file is not an OpenType file.
        /// </summary>
        public static HRESULT MSSIPOTF_E_NOT_OPENTYPE = new HRESULT("0x80097012", "MSSIPOTF_E_NOT_OPENTYPE", "The file is not an OpenType file.");

        /// <summary>
        /// Failed on a file operation (open, map, read, write).
        /// </summary>
        public static HRESULT MSSIPOTF_E_FILE = new HRESULT("0x80097013", "MSSIPOTF_E_FILE", "Failed on a file operation (open, map, read, write).");

        /// <summary>
        /// A call to a CryptoAPI function failed.
        /// </summary>
        public static HRESULT MSSIPOTF_E_CRYPT = new HRESULT("0x80097014", "MSSIPOTF_E_CRYPT", "A call to a CryptoAPI function failed.");

        /// <summary>
        /// There is a bad version number in the file.
        /// </summary>
        public static HRESULT MSSIPOTF_E_BADVERSION = new HRESULT("0x80097015", "MSSIPOTF_E_BADVERSION", "There is a bad version number in the file.");

        /// <summary>
        /// The structure of the DSIG table is incorrect.
        /// </summary>
        public static HRESULT MSSIPOTF_E_DSIG_STRUCTURE = new HRESULT("0x80097016", "MSSIPOTF_E_DSIG_STRUCTURE", "The structure of the DSIG table is incorrect.");

        /// <summary>
        /// A check failed in a partially constant table.
        /// </summary>
        public static HRESULT MSSIPOTF_E_PCONST_CHECK = new HRESULT("0x80097017", "MSSIPOTF_E_PCONST_CHECK", "A check failed in a partially constant table.");

        /// <summary>
        /// Some kind of structural error.
        /// </summary>
        public static HRESULT MSSIPOTF_E_STRUCTURE = new HRESULT("0x80097018", "MSSIPOTF_E_STRUCTURE", "Some kind of structural error.");

        /// <summary>
        /// The requested credential requires confirmation.
        /// </summary>
        public static HRESULT ERROR_CRED_REQUIRES_CONFIRMATION = new HRESULT("0x80097019", "ERROR_CRED_REQUIRES_CONFIRMATION", "The requested credential requires confirmation.");

        /// <summary>
        /// Unknown trust provider.
        /// </summary>
        public static HRESULT TRUST_E_PROVIDER_UNKNOWN = new HRESULT("0x800B0001", "TRUST_E_PROVIDER_UNKNOWN", "Unknown trust provider.");

        /// <summary>
        /// The trust verification action specified is not supported by the specified trust provider.
        /// </summary>
        public static HRESULT TRUST_E_ACTION_UNKNOWN = new HRESULT("0x800B0002", "TRUST_E_ACTION_UNKNOWN", "The trust verification action specified is not supported by the specified trust provider.");

        /// <summary>
        /// The form specified for the subject is not one supported or known by the specified trust provider.
        /// </summary>
        public static HRESULT TRUST_E_SUBJECT_FORM_UNKNOWN = new HRESULT("0x800B0003", "TRUST_E_SUBJECT_FORM_UNKNOWN", "The form specified for the subject is not one supported or known by the specified trust provider.");

        /// <summary>
        /// The subject is not trusted for the specified action.
        /// </summary>
        public static HRESULT TRUST_E_SUBJECT_NOT_TRUSTED = new HRESULT("0x800B0004", "TRUST_E_SUBJECT_NOT_TRUSTED", "The subject is not trusted for the specified action.");

        /// <summary>
        /// Error due to problem in ASN.1 encoding process.
        /// </summary>
        public static HRESULT DIGSIG_E_ENCODE = new HRESULT("0x800B0005", "DIGSIG_E_ENCODE", "Error due to problem in ASN.1 encoding process.");

        /// <summary>
        /// Error due to problem in ASN.1 decoding process.
        /// </summary>
        public static HRESULT DIGSIG_E_DECODE = new HRESULT("0x800B0006", "DIGSIG_E_DECODE", "Error due to problem in ASN.1 decoding process.");

        /// <summary>
        /// Reading / writing Extensions where Attributes are appropriate, and visa versa.
        /// </summary>
        public static HRESULT DIGSIG_E_EXTENSIBILITY = new HRESULT("0x800B0007", "DIGSIG_E_EXTENSIBILITY", "Reading / writing Extensions where Attributes are appropriate, and visa versa.");

        /// <summary>
        /// Unspecified cryptographic failure.
        /// </summary>
        public static HRESULT DIGSIG_E_CRYPTO = new HRESULT("0x800B0008", "DIGSIG_E_CRYPTO", "Unspecified cryptographic failure.");

        /// <summary>
        /// The size of the data could not be determined.
        /// </summary>
        public static HRESULT PERSIST_E_SIZEDEFINITE = new HRESULT("0x800B0009", "PERSIST_E_SIZEDEFINITE", "The size of the data could not be determined.");

        /// <summary>
        /// The size of the indefinite-sized data could not be determined.
        /// </summary>
        public static HRESULT PERSIST_E_SIZEINDEFINITE = new HRESULT("0x800B000A", "PERSIST_E_SIZEINDEFINITE", "The size of the indefinite-sized data could not be determined.");

        /// <summary>
        /// This object does not read and write self-sizing data.
        /// </summary>
        public static HRESULT PERSIST_E_NOTSELFSIZING = new HRESULT("0x800B000B", "PERSIST_E_NOTSELFSIZING", "This object does not read and write self-sizing data.");

        /// <summary>
        /// No signature was present in the subject.
        /// </summary>
        public static HRESULT TRUST_E_NOSIGNATURE = new HRESULT("0x800B0100", "TRUST_E_NOSIGNATURE", "No signature was present in the subject.");

        /// <summary>
        /// A required certificate is not within its validity period when verifying against the current system clock or the timestamp in the signed file.
        /// </summary>
        public static HRESULT CERT_E_EXPIRED = new HRESULT("0x800B0101", "CERT_E_EXPIRED", "A required certificate is not within its validity period when verifying against the current system clock or the timestamp in the signed file.");

        /// <summary>
        /// The validity periods of the certification chain do not nest correctly.
        /// </summary>
        public static HRESULT CERT_E_VALIDITYPERIODNESTING = new HRESULT("0x800B0102", "CERT_E_VALIDITYPERIODNESTING", "The validity periods of the certification chain do not nest correctly.");

        /// <summary>
        /// A certificate that can only be used as an end-entity is being used as a CA or visa versa.
        /// </summary>
        public static HRESULT CERT_E_ROLE = new HRESULT("0x800B0103", "CERT_E_ROLE", "A certificate that can only be used as an end-entity is being used as a CA or visa versa.");

        /// <summary>
        /// A path length constraint in the certification chain has been violated.
        /// </summary>
        public static HRESULT CERT_E_PATHLENCONST = new HRESULT("0x800B0104", "CERT_E_PATHLENCONST", "A path length constraint in the certification chain has been violated.");

        /// <summary>
        /// A certificate contains an unknown extension that is marked 'critical'.
        /// </summary>
        public static HRESULT CERT_E_CRITICAL = new HRESULT("0x800B0105", "CERT_E_CRITICAL", "A certificate contains an unknown extension that is marked 'critical'.");

        /// <summary>
        /// A certificate being used for a purpose other than the ones specified by its CA.
        /// </summary>
        public static HRESULT CERT_E_PURPOSE = new HRESULT("0x800B0106", "CERT_E_PURPOSE", "A certificate being used for a purpose other than the ones specified by its CA.");

        /// <summary>
        /// A parent of a given certificate in fact did not issue that child certificate.
        /// </summary>
        public static HRESULT CERT_E_ISSUERCHAINING = new HRESULT("0x800B0107", "CERT_E_ISSUERCHAINING", "A parent of a given certificate in fact did not issue that child certificate.");

        /// <summary>
        /// A certificate is missing or has an empty value for an important field, such as a subject or issuer name.
        /// </summary>
        public static HRESULT CERT_E_MALFORMED = new HRESULT("0x800B0108", "CERT_E_MALFORMED", "A certificate is missing or has an empty value for an important field, such as a subject or issuer name.");

        /// <summary>
        /// A certificate chain processed, but terminated in a root certificate which is not trusted by the trust provider.
        /// </summary>
        public static HRESULT CERT_E_UNTRUSTEDROOT = new HRESULT("0x800B0109", "CERT_E_UNTRUSTEDROOT", "A certificate chain processed, but terminated in a root certificate which is not trusted by the trust provider.");

        /// <summary>
        /// A certificate chain could not be built to a trusted root authority.
        /// </summary>
        public static HRESULT CERT_E_CHAINING = new HRESULT("0x800B010A", "CERT_E_CHAINING", "A certificate chain could not be built to a trusted root authority.");

        /// <summary>
        /// Generic trust failure.
        /// </summary>
        public static HRESULT TRUST_E_FAIL = new HRESULT("0x800B010B", "TRUST_E_FAIL", "Generic trust failure.");

        /// <summary>
        /// A certificate was explicitly revoked by its issuer.
        /// </summary>
        public static HRESULT CERT_E_REVOKED = new HRESULT("0x800B010C", "CERT_E_REVOKED", "A certificate was explicitly revoked by its issuer.");

        /// <summary>
        /// The certification path terminates with the test root which is not trusted with the current policy settings.
        /// </summary>
        public static HRESULT CERT_E_UNTRUSTEDTESTROOT = new HRESULT("0x800B010D", "CERT_E_UNTRUSTEDTESTROOT", "The certification path terminates with the test root which is not trusted with the current policy settings.");

        /// <summary>
        /// The revocation process could not continue - the certificate(s) could not be checked.
        /// </summary>
        public static HRESULT CERT_E_REVOCATION_FAILURE = new HRESULT("0x800B010E", "CERT_E_REVOCATION_FAILURE", "The revocation process could not continue - the certificate(s) could not be checked.");

        /// <summary>
        /// The certificate's CN name does not match the passed value.
        /// </summary>
        public static HRESULT CERT_E_CN_NO_MATCH = new HRESULT("0x800B010F", "CERT_E_CN_NO_MATCH", "The certificate's CN name does not match the passed value.");

        /// <summary>
        /// The certificate is not valid for the requested usage.
        /// </summary>
        public static HRESULT CERT_E_WRONG_USAGE = new HRESULT("0x800B0110", "CERT_E_WRONG_USAGE", "The certificate is not valid for the requested usage.");

        /// <summary>
        /// The certificate was explicitly marked as untrusted by the user.
        /// </summary>
        public static HRESULT TRUST_E_EXPLICIT_DISTRUST = new HRESULT("0x800B0111", "TRUST_E_EXPLICIT_DISTRUST", "The certificate was explicitly marked as untrusted by the user.");

        /// <summary>
        /// A certification chain processed correctly, but one of the CA certificates is not trusted by the policy provider.
        /// </summary>
        public static HRESULT CERT_E_UNTRUSTEDCA = new HRESULT("0x800B0112", "CERT_E_UNTRUSTEDCA", "A certification chain processed correctly, but one of the CA certificates is not trusted by the policy provider.");

        /// <summary>
        /// The certificate has invalid policy.
        /// </summary>
        public static HRESULT CERT_E_INVALID_POLICY = new HRESULT("0x800B0113", "CERT_E_INVALID_POLICY", "The certificate has invalid policy.");

        /// <summary>
        /// The certificate has an invalid name. The name is not included in the permitted list or is explicitly excluded.
        /// </summary>
        public static HRESULT CERT_E_INVALID_NAME = new HRESULT("0x800B0114", "CERT_E_INVALID_NAME", "The certificate has an invalid name. The name is not included in the permitted list or is explicitly excluded.");

        /// <summary>
        /// A non-empty line was encountered in the INF before the start of a section.
        /// </summary>
        public static HRESULT SPAPI_E_EXPECTED_SECTION_NAME = new HRESULT("0x800F0000", "SPAPI_E_EXPECTED_SECTION_NAME", "A non-empty line was encountered in the INF before the start of a section.");

        /// <summary>
        /// A section name marker in the INF is not complete, or does not exist on a line by itself.
        /// </summary>
        public static HRESULT SPAPI_E_BAD_SECTION_NAME_LINE = new HRESULT("0x800F0001", "SPAPI_E_BAD_SECTION_NAME_LINE", "A section name marker in the INF is not complete, or does not exist on a line by itself.");

        /// <summary>
        /// An INF section was encountered whose name exceeds the maximum section name length.
        /// </summary>
        public static HRESULT SPAPI_E_SECTION_NAME_TOO_LONG = new HRESULT("0x800F0002", "SPAPI_E_SECTION_NAME_TOO_LONG", "An INF section was encountered whose name exceeds the maximum section name length.");

        /// <summary>
        /// The syntax of the INF is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_GENERAL_SYNTAX = new HRESULT("0x800F0003", "SPAPI_E_GENERAL_SYNTAX", "The syntax of the INF is invalid.");

        /// <summary>
        /// The style of the INF is different than what was requested.
        /// </summary>
        public static HRESULT SPAPI_E_WRONG_INF_STYLE = new HRESULT("0x800F0100", "SPAPI_E_WRONG_INF_STYLE", "The style of the INF is different than what was requested.");

        /// <summary>
        /// The required section was not found in the INF.
        /// </summary>
        public static HRESULT SPAPI_E_SECTION_NOT_FOUND = new HRESULT("0x800F0101", "SPAPI_E_SECTION_NOT_FOUND", "The required section was not found in the INF.");

        /// <summary>
        /// The required line was not found in the INF.
        /// </summary>
        public static HRESULT SPAPI_E_LINE_NOT_FOUND = new HRESULT("0x800F0102", "SPAPI_E_LINE_NOT_FOUND", "The required line was not found in the INF.");

        /// <summary>
        /// The files affected by the installation of this file queue have not been backed up for uninstall.
        /// </summary>
        public static HRESULT SPAPI_E_NO_BACKUP = new HRESULT("0x800F0103", "SPAPI_E_NO_BACKUP", "The files affected by the installation of this file queue have not been backed up for uninstall.");

        /// <summary>
        /// The INF or the device information set or element does not have an associated install class.
        /// </summary>
        public static HRESULT SPAPI_E_NO_ASSOCIATED_CLASS = new HRESULT("0x800F0200", "SPAPI_E_NO_ASSOCIATED_CLASS", "The INF or the device information set or element does not have an associated install class.");

        /// <summary>
        /// The INF or the device information set or element does not match the specified install class.
        /// </summary>
        public static HRESULT SPAPI_E_CLASS_MISMATCH = new HRESULT("0x800F0201", "SPAPI_E_CLASS_MISMATCH", "The INF or the device information set or element does not match the specified install class.");

        /// <summary>
        /// An existing device was found that is a duplicate of the device being manually installed.
        /// </summary>
        public static HRESULT SPAPI_E_DUPLICATE_FOUND = new HRESULT("0x800F0202", "SPAPI_E_DUPLICATE_FOUND", "An existing device was found that is a duplicate of the device being manually installed.");

        /// <summary>
        /// There is no driver selected for the device information set or element.
        /// </summary>
        public static HRESULT SPAPI_E_NO_DRIVER_SELECTED = new HRESULT("0x800F0203", "SPAPI_E_NO_DRIVER_SELECTED", "There is no driver selected for the device information set or element.");

        /// <summary>
        /// The requested device registry key does not exist.
        /// </summary>
        public static HRESULT SPAPI_E_KEY_DOES_NOT_EXIST = new HRESULT("0x800F0204", "SPAPI_E_KEY_DOES_NOT_EXIST", "The requested device registry key does not exist.");

        /// <summary>
        /// The device instance name is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_DEVINST_NAME = new HRESULT("0x800F0205", "SPAPI_E_INVALID_DEVINST_NAME", "The device instance name is invalid.");

        /// <summary>
        /// The install class is not present or is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_CLASS = new HRESULT("0x800F0206", "SPAPI_E_INVALID_CLASS", "The install class is not present or is invalid.");

        /// <summary>
        /// The device instance cannot be created because it already exists.
        /// </summary>
        public static HRESULT SPAPI_E_DEVINST_ALREADY_EXISTS = new HRESULT("0x800F0207", "SPAPI_E_DEVINST_ALREADY_EXISTS", "The device instance cannot be created because it already exists.");

        /// <summary>
        /// The operation cannot be performed on a device information element that has not been registered.
        /// </summary>
        public static HRESULT SPAPI_E_DEVINFO_NOT_REGISTERED = new HRESULT("0x800F0208", "SPAPI_E_DEVINFO_NOT_REGISTERED", "The operation cannot be performed on a device information element that has not been registered.");

        /// <summary>
        /// The device property code is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_REG_PROPERTY = new HRESULT("0x800F0209", "SPAPI_E_INVALID_REG_PROPERTY", "The device property code is invalid.");

        /// <summary>
        /// The INF from which a driver list is to be built does not exist.
        /// </summary>
        public static HRESULT SPAPI_E_NO_INF = new HRESULT("0x800F020A", "SPAPI_E_NO_INF", "The INF from which a driver list is to be built does not exist.");

        /// <summary>
        /// The device instance does not exist in the hardware tree.
        /// </summary>
        public static HRESULT SPAPI_E_NO_SUCH_DEVINST = new HRESULT("0x800F020B", "SPAPI_E_NO_SUCH_DEVINST", "The device instance does not exist in the hardware tree.");

        /// <summary>
        /// The icon representing this install class cannot be loaded.
        /// </summary>
        public static HRESULT SPAPI_E_CANT_LOAD_CLASS_ICON = new HRESULT("0x800F020C", "SPAPI_E_CANT_LOAD_CLASS_ICON", "The icon representing this install class cannot be loaded.");

        /// <summary>
        /// The class installer registry entry is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_CLASS_INSTALLER = new HRESULT("0x800F020D", "SPAPI_E_INVALID_CLASS_INSTALLER", "The class installer registry entry is invalid.");

        /// <summary>
        /// The class installer has indicated that the default action should be performed for this installation request.
        /// </summary>
        public static HRESULT SPAPI_E_DI_DO_DEFAULT = new HRESULT("0x800F020E", "SPAPI_E_DI_DO_DEFAULT", "The class installer has indicated that the default action should be performed for this installation request.");

        /// <summary>
        /// The operation does not require any files to be copied.
        /// </summary>
        public static HRESULT SPAPI_E_DI_NOFILECOPY = new HRESULT("0x800F020F", "SPAPI_E_DI_NOFILECOPY", "The operation does not require any files to be copied.");

        /// <summary>
        /// The specified hardware profile does not exist.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_HWPROFILE = new HRESULT("0x800F0210", "SPAPI_E_INVALID_HWPROFILE", "The specified hardware profile does not exist.");

        /// <summary>
        /// There is no device information element currently selected for this device information set.
        /// </summary>
        public static HRESULT SPAPI_E_NO_DEVICE_SELECTED = new HRESULT("0x800F0211", "SPAPI_E_NO_DEVICE_SELECTED", "There is no device information element currently selected for this device information set.");

        /// <summary>
        /// The operation cannot be performed because the device information set is locked.
        /// </summary>
        public static HRESULT SPAPI_E_DEVINFO_LIST_LOCKED = new HRESULT("0x800F0212", "SPAPI_E_DEVINFO_LIST_LOCKED", "The operation cannot be performed because the device information set is locked.");

        /// <summary>
        /// The operation cannot be performed because the device information element is locked.
        /// </summary>
        public static HRESULT SPAPI_E_DEVINFO_DATA_LOCKED = new HRESULT("0x800F0213", "SPAPI_E_DEVINFO_DATA_LOCKED", "The operation cannot be performed because the device information element is locked.");

        /// <summary>
        /// The specified path does not contain any applicable device INFs.
        /// </summary>
        public static HRESULT SPAPI_E_DI_BAD_PATH = new HRESULT("0x800F0214", "SPAPI_E_DI_BAD_PATH", "The specified path does not contain any applicable device INFs.");

        /// <summary>
        /// No class installer parameters have been set for the device information set or element.
        /// </summary>
        public static HRESULT SPAPI_E_NO_CLASSINSTALL_PARAMS = new HRESULT("0x800F0215", "SPAPI_E_NO_CLASSINSTALL_PARAMS", "No class installer parameters have been set for the device information set or element.");

        /// <summary>
        /// The operation cannot be performed because the file queue is locked.
        /// </summary>
        public static HRESULT SPAPI_E_FILEQUEUE_LOCKED = new HRESULT("0x800F0216", "SPAPI_E_FILEQUEUE_LOCKED", "The operation cannot be performed because the file queue is locked.");

        /// <summary>
        /// A service installation section in this INF is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_BAD_SERVICE_INSTALLSECT = new HRESULT("0x800F0217", "SPAPI_E_BAD_SERVICE_INSTALLSECT", "A service installation section in this INF is invalid.");

        /// <summary>
        /// There is no class driver list for the device information element.
        /// </summary>
        public static HRESULT SPAPI_E_NO_CLASS_DRIVER_LIST = new HRESULT("0x800F0218", "SPAPI_E_NO_CLASS_DRIVER_LIST", "There is no class driver list for the device information element.");

        /// <summary>
        /// The installation failed because a function driver was not specified for this device instance.
        /// </summary>
        public static HRESULT SPAPI_E_NO_ASSOCIATED_SERVICE = new HRESULT("0x800F0219", "SPAPI_E_NO_ASSOCIATED_SERVICE", "The installation failed because a function driver was not specified for this device instance.");

        /// <summary>
        /// There is presently no default device interface designated for this interface class.
        /// </summary>
        public static HRESULT SPAPI_E_NO_DEFAULT_DEVICE_INTERFACE = new HRESULT("0x800F021A", "SPAPI_E_NO_DEFAULT_DEVICE_INTERFACE", "There is presently no default device interface designated for this interface class.");

        /// <summary>
        /// The operation cannot be performed because the device interface is currently active.
        /// </summary>
        public static HRESULT SPAPI_E_DEVICE_INTERFACE_ACTIVE = new HRESULT("0x800F021B", "SPAPI_E_DEVICE_INTERFACE_ACTIVE", "The operation cannot be performed because the device interface is currently active.");

        /// <summary>
        /// The operation cannot be performed because the device interface has been removed from the system.
        /// </summary>
        public static HRESULT SPAPI_E_DEVICE_INTERFACE_REMOVED = new HRESULT("0x800F021C", "SPAPI_E_DEVICE_INTERFACE_REMOVED", "The operation cannot be performed because the device interface has been removed from the system.");

        /// <summary>
        /// An interface installation section in this INF is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_BAD_INTERFACE_INSTALLSECT = new HRESULT("0x800F021D", "SPAPI_E_BAD_INTERFACE_INSTALLSECT", "An interface installation section in this INF is invalid.");

        /// <summary>
        /// This interface class does not exist in the system.
        /// </summary>
        public static HRESULT SPAPI_E_NO_SUCH_INTERFACE_CLASS = new HRESULT("0x800F021E", "SPAPI_E_NO_SUCH_INTERFACE_CLASS", "This interface class does not exist in the system.");

        /// <summary>
        /// The reference string supplied for this interface device is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_REFERENCE_STRING = new HRESULT("0x800F021F", "SPAPI_E_INVALID_REFERENCE_STRING", "The reference string supplied for this interface device is invalid.");

        /// <summary>
        /// The specified machine name does not conform to UNC naming conventions.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_MACHINENAME = new HRESULT("0x800F0220", "SPAPI_E_INVALID_MACHINENAME", "The specified machine name does not conform to UNC naming conventions.");

        /// <summary>
        /// A general remote communication error occurred.
        /// </summary>
        public static HRESULT SPAPI_E_REMOTE_COMM_FAILURE = new HRESULT("0x800F0221", "SPAPI_E_REMOTE_COMM_FAILURE", "A general remote communication error occurred.");

        /// <summary>
        /// The machine selected for remote communication is not available at this time.
        /// </summary>
        public static HRESULT SPAPI_E_MACHINE_UNAVAILABLE = new HRESULT("0x800F0222", "SPAPI_E_MACHINE_UNAVAILABLE", "The machine selected for remote communication is not available at this time.");

        /// <summary>
        /// The Plug and Play service is not available on the remote machine.
        /// </summary>
        public static HRESULT SPAPI_E_NO_CONFIGMGR_SERVICES = new HRESULT("0x800F0223", "SPAPI_E_NO_CONFIGMGR_SERVICES", "The Plug and Play service is not available on the remote machine.");

        /// <summary>
        /// The property page provider registry entry is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_PROPPAGE_PROVIDER = new HRESULT("0x800F0224", "SPAPI_E_INVALID_PROPPAGE_PROVIDER", "The property page provider registry entry is invalid.");

        /// <summary>
        /// The requested device interface is not present in the system.
        /// </summary>
        public static HRESULT SPAPI_E_NO_SUCH_DEVICE_INTERFACE = new HRESULT("0x800F0225", "SPAPI_E_NO_SUCH_DEVICE_INTERFACE", "The requested device interface is not present in the system.");

        /// <summary>
        /// The device's co-installer has additional work to perform after installation is complete.
        /// </summary>
        public static HRESULT SPAPI_E_DI_POSTPROCESSING_REQUIRED = new HRESULT("0x800F0226", "SPAPI_E_DI_POSTPROCESSING_REQUIRED", "The device's co-installer has additional work to perform after installation is complete.");

        /// <summary>
        /// The device's co-installer is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_COINSTALLER = new HRESULT("0x800F0227", "SPAPI_E_INVALID_COINSTALLER", "The device's co-installer is invalid.");

        /// <summary>
        /// There are no compatible drivers for this device.
        /// </summary>
        public static HRESULT SPAPI_E_NO_COMPAT_DRIVERS = new HRESULT("0x800F0228", "SPAPI_E_NO_COMPAT_DRIVERS", "There are no compatible drivers for this device.");

        /// <summary>
        /// There is no icon that represents this device or device type.
        /// </summary>
        public static HRESULT SPAPI_E_NO_DEVICE_ICON = new HRESULT("0x800F0229", "SPAPI_E_NO_DEVICE_ICON", "There is no icon that represents this device or device type.");

        /// <summary>
        /// A logical configuration specified in this INF is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_INF_LOGCONFIG = new HRESULT("0x800F022A", "SPAPI_E_INVALID_INF_LOGCONFIG", "A logical configuration specified in this INF is invalid.");

        /// <summary>
        /// The class installer has denied the request to install or upgrade this device.
        /// </summary>
        public static HRESULT SPAPI_E_DI_DONT_INSTALL = new HRESULT("0x800F022B", "SPAPI_E_DI_DONT_INSTALL", "The class installer has denied the request to install or upgrade this device.");

        /// <summary>
        /// One of the filter drivers installed for this device is invalid.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_FILTER_DRIVER = new HRESULT("0x800F022C", "SPAPI_E_INVALID_FILTER_DRIVER", "One of the filter drivers installed for this device is invalid.");

        /// <summary>
        /// The driver selected for this device does not support this version of Windows.
        /// </summary>
        public static HRESULT SPAPI_E_NON_WINDOWS_NT_DRIVER = new HRESULT("0x800F022D", "SPAPI_E_NON_WINDOWS_NT_DRIVER", "The driver selected for this device does not support this version of Windows.");

        /// <summary>
        /// The driver selected for this device does not support Windows.
        /// </summary>
        public static HRESULT SPAPI_E_NON_WINDOWS_DRIVER = new HRESULT("0x800F022E", "SPAPI_E_NON_WINDOWS_DRIVER", "The driver selected for this device does not support Windows.");

        /// <summary>
        /// The third-party INF does not contain digital signature information.
        /// </summary>
        public static HRESULT SPAPI_E_NO_CATALOG_FOR_OEM_INF = new HRESULT("0x800F022F", "SPAPI_E_NO_CATALOG_FOR_OEM_INF", "The third-party INF does not contain digital signature information.");

        /// <summary>
        /// An invalid attempt was made to use a device installation file queue for verification of digital signatures relative to other platforms.
        /// </summary>
        public static HRESULT SPAPI_E_DEVINSTALL_QUEUE_NONNATIVE = new HRESULT("0x800F0230", "SPAPI_E_DEVINSTALL_QUEUE_NONNATIVE", "An invalid attempt was made to use a device installation file queue for verification of digital signatures relative to other platforms.");

        /// <summary>
        /// The device cannot be disabled.
        /// </summary>
        public static HRESULT SPAPI_E_NOT_DISABLEABLE = new HRESULT("0x800F0231", "SPAPI_E_NOT_DISABLEABLE", "The device cannot be disabled.");

        /// <summary>
        /// The device could not be dynamically removed.
        /// </summary>
        public static HRESULT SPAPI_E_CANT_REMOVE_DEVINST = new HRESULT("0x800F0232", "SPAPI_E_CANT_REMOVE_DEVINST", "The device could not be dynamically removed.");

        /// <summary>
        /// Cannot copy to specified target.
        /// </summary>
        public static HRESULT SPAPI_E_INVALID_TARGET = new HRESULT("0x800F0233", "SPAPI_E_INVALID_TARGET", "Cannot copy to specified target.");

        /// <summary>
        /// Driver is not intended for this platform.
        /// </summary>
        public static HRESULT SPAPI_E_DRIVER_NONNATIVE = new HRESULT("0x800F0234", "SPAPI_E_DRIVER_NONNATIVE", "Driver is not intended for this platform.");

        /// <summary>
        /// Operation not allowed in WOW64.
        /// </summary>
        public static HRESULT SPAPI_E_IN_WOW64 = new HRESULT("0x800F0235", "SPAPI_E_IN_WOW64", "Operation not allowed in WOW64.");

        /// <summary>
        /// The operation involving unsigned file copying was rolled back, so that a system restore point could be set.
        /// </summary>
        public static HRESULT SPAPI_E_SET_SYSTEM_RESTORE_POINT = new HRESULT("0x800F0236", "SPAPI_E_SET_SYSTEM_RESTORE_POINT", "The operation involving unsigned file copying was rolled back, so that a system restore point could be set.");

        /// <summary>
        /// An INF was copied into the Windows INF directory in an improper manner.
        /// </summary>
        public static HRESULT SPAPI_E_INCORRECTLY_COPIED_INF = new HRESULT("0x800F0237", "SPAPI_E_INCORRECTLY_COPIED_INF", "An INF was copied into the Windows INF directory in an improper manner.");

        /// <summary>
        /// The Security Configuration Editor (SCE) APIs have been disabled on this Embedded product.
        /// </summary>
        public static HRESULT SPAPI_E_SCE_DISABLED = new HRESULT("0x800F0238", "SPAPI_E_SCE_DISABLED", "The Security Configuration Editor (SCE) APIs have been disabled on this Embedded product.");

        /// <summary>
        /// An unknown exception was encountered.
        /// </summary>
        public static HRESULT SPAPI_E_UNKNOWN_EXCEPTION = new HRESULT("0x800F0239", "SPAPI_E_UNKNOWN_EXCEPTION", "An unknown exception was encountered.");

        /// <summary>
        /// A problem was encountered when accessing the Plug and Play registry database.
        /// </summary>
        public static HRESULT SPAPI_E_PNP_REGISTRY_ERROR = new HRESULT("0x800F023A", "SPAPI_E_PNP_REGISTRY_ERROR", "A problem was encountered when accessing the Plug and Play registry database.");

        /// <summary>
        /// The requested operation is not supported for a remote machine.
        /// </summary>
        public static HRESULT SPAPI_E_REMOTE_REQUEST_UNSUPPORTED = new HRESULT("0x800F023B", "SPAPI_E_REMOTE_REQUEST_UNSUPPORTED", "The requested operation is not supported for a remote machine.");

        /// <summary>
        /// The specified file is not an installed OEM INF.
        /// </summary>
        public static HRESULT SPAPI_E_NOT_AN_INSTALLED_OEM_INF = new HRESULT("0x800F023C", "SPAPI_E_NOT_AN_INSTALLED_OEM_INF", "The specified file is not an installed OEM INF.");

        /// <summary>
        /// One or more devices are presently installed using the specified INF.
        /// </summary>
        public static HRESULT SPAPI_E_INF_IN_USE_BY_DEVICES = new HRESULT("0x800F023D", "SPAPI_E_INF_IN_USE_BY_DEVICES", "One or more devices are presently installed using the specified INF.");

        /// <summary>
        /// The requested device install operation is obsolete.
        /// </summary>
        public static HRESULT SPAPI_E_DI_FUNCTION_OBSOLETE = new HRESULT("0x800F023E", "SPAPI_E_DI_FUNCTION_OBSOLETE", "The requested device install operation is obsolete.");

        /// <summary>
        /// A file could not be verified because it does not have an associated catalog signed via Authenticode(tm).
        /// </summary>
        public static HRESULT SPAPI_E_NO_AUTHENTICODE_CATALOG = new HRESULT("0x800F023F", "SPAPI_E_NO_AUTHENTICODE_CATALOG", "A file could not be verified because it does not have an associated catalog signed via Authenticode(tm).");

        /// <summary>
        /// Authenticode(tm) signature verification is not supported for the specified INF.
        /// </summary>
        public static HRESULT SPAPI_E_AUTHENTICODE_DISALLOWED = new HRESULT("0x800F0240", "SPAPI_E_AUTHENTICODE_DISALLOWED", "Authenticode(tm) signature verification is not supported for the specified INF.");

        /// <summary>
        /// The INF was signed with an Authenticode(tm) catalog from a trusted publisher.
        /// </summary>
        public static HRESULT SPAPI_E_AUTHENTICODE_TRUSTED_PUBLISHER = new HRESULT("0x800F0241", "SPAPI_E_AUTHENTICODE_TRUSTED_PUBLISHER", "The INF was signed with an Authenticode(tm) catalog from a trusted publisher.");

        /// <summary>
        /// The publisher of an Authenticode(tm) signed catalog has not yet been established as trusted.
        /// </summary>
        public static HRESULT SPAPI_E_AUTHENTICODE_TRUST_NOT_ESTABLISHED = new HRESULT("0x800F0242", "SPAPI_E_AUTHENTICODE_TRUST_NOT_ESTABLISHED", "The publisher of an Authenticode(tm) signed catalog has not yet been established as trusted.");

        /// <summary>
        /// The publisher of an Authenticode(tm) signed catalog was not established as trusted.
        /// </summary>
        public static HRESULT SPAPI_E_AUTHENTICODE_PUBLISHER_NOT_TRUSTED = new HRESULT("0x800F0243", "SPAPI_E_AUTHENTICODE_PUBLISHER_NOT_TRUSTED", "The publisher of an Authenticode(tm) signed catalog was not established as trusted.");

        /// <summary>
        /// The software was tested for compliance with Windows Logo requirements on a different version of Windows, and may not be compatible with this version.
        /// </summary>
        public static HRESULT SPAPI_E_SIGNATURE_OSATTRIBUTE_MISMATCH = new HRESULT("0x800F0244", "SPAPI_E_SIGNATURE_OSATTRIBUTE_MISMATCH", "The software was tested for compliance with Windows Logo requirements on a different version of Windows, and may not be compatible with this version.");

        /// <summary>
        /// The file may only be validated by a catalog signed via Authenticode(tm).
        /// </summary>
        public static HRESULT SPAPI_E_ONLY_VALIDATE_VIA_AUTHENTICODE = new HRESULT("0x800F0245", "SPAPI_E_ONLY_VALIDATE_VIA_AUTHENTICODE", "The file may only be validated by a catalog signed via Authenticode(tm).");

        /// <summary>
        /// One of the installers for this device cannot perform the installation at this time.
        /// </summary>
        public static HRESULT SPAPI_E_DEVICE_INSTALLER_NOT_READY = new HRESULT("0x800F0246", "SPAPI_E_DEVICE_INSTALLER_NOT_READY", "One of the installers for this device cannot perform the installation at this time.");

        /// <summary>
        /// A problem was encountered while attempting to add the driver to the store.
        /// </summary>
        public static HRESULT SPAPI_E_DRIVER_STORE_ADD_FAILED = new HRESULT("0x800F0247", "SPAPI_E_DRIVER_STORE_ADD_FAILED", "A problem was encountered while attempting to add the driver to the store.");

        /// <summary>
        /// The installation of this device is forbidden by system policy. Contact your system administrator.
        /// </summary>
        public static HRESULT SPAPI_E_DEVICE_INSTALL_BLOCKED = new HRESULT("0x800F0248", "SPAPI_E_DEVICE_INSTALL_BLOCKED", "The installation of this device is forbidden by system policy. Contact your system administrator.");

        /// <summary>
        /// The installation of this driver is forbidden by system policy. Contact your system administrator.
        /// </summary>
        public static HRESULT SPAPI_E_DRIVER_INSTALL_BLOCKED = new HRESULT("0x800F0249", "SPAPI_E_DRIVER_INSTALL_BLOCKED", "The installation of this driver is forbidden by system policy. Contact your system administrator.");

        /// <summary>
        /// The specified INF is the wrong type for this operation.
        /// </summary>
        public static HRESULT SPAPI_E_WRONG_INF_TYPE = new HRESULT("0x800F024A", "SPAPI_E_WRONG_INF_TYPE", "The specified INF is the wrong type for this operation.");

        /// <summary>
        /// The hash for the file is not present in the specified catalog file. The file is likely corrupt or the victim of tampering.
        /// </summary>
        public static HRESULT SPAPI_E_FILE_HASH_NOT_IN_CATALOG = new HRESULT("0x800F024B", "SPAPI_E_FILE_HASH_NOT_IN_CATALOG", "The hash for the file is not present in the specified catalog file. The file is likely corrupt or the victim of tampering.");

        /// <summary>
        /// A problem was encountered while attempting to delete the driver from the store.
        /// </summary>
        public static HRESULT SPAPI_E_DRIVER_STORE_DELETE_FAILED = new HRESULT("0x800F024C", "SPAPI_E_DRIVER_STORE_DELETE_FAILED", "A problem was encountered while attempting to delete the driver from the store.");

        /// <summary>
        /// An unrecoverable stack overflow was encountered.
        /// </summary>
        public static HRESULT SPAPI_E_UNRECOVERABLE_STACK_OVERFLOW = new HRESULT("0x800F0300", "SPAPI_E_UNRECOVERABLE_STACK_OVERFLOW", "An unrecoverable stack overflow was encountered.");

        /// <summary>
        /// No installed components were detected.
        /// </summary>
        public static HRESULT SPAPI_E_ERROR_NOT_INSTALLED = new HRESULT("0x800F1000", "SPAPI_E_ERROR_NOT_INSTALLED", "No installed components were detected.");

        /// <summary>
        /// An internal consistency check failed.
        /// </summary>
        public static HRESULT SCARD_F_INTERNAL_ERROR = new HRESULT("0x80100001", "SCARD_F_INTERNAL_ERROR", "An internal consistency check failed.");

        /// <summary>
        /// The action was canceled by an SCardCancel request.
        /// </summary>
        public static HRESULT SCARD_E_CANCELLED = new HRESULT("0x80100002", "SCARD_E_CANCELLED", "The action was canceled by an SCardCancel request.");

        /// <summary>
        /// The supplied handle was invalid.
        /// </summary>
        public static HRESULT SCARD_E_INVALID_HANDLE = new HRESULT("0x80100003", "SCARD_E_INVALID_HANDLE", "The supplied handle was invalid.");

        /// <summary>
        /// One or more of the supplied parameters could not be properly interpreted.
        /// </summary>
        public static HRESULT SCARD_E_INVALID_PARAMETER = new HRESULT("0x80100004", "SCARD_E_INVALID_PARAMETER", "One or more of the supplied parameters could not be properly interpreted.");

        /// <summary>
        /// Registry startup information is missing or invalid.
        /// </summary>
        public static HRESULT SCARD_E_INVALID_TARGET = new HRESULT("0x80100005", "SCARD_E_INVALID_TARGET", "Registry startup information is missing or invalid.");

        /// <summary>
        /// Not enough memory available to complete this command.
        /// </summary>
        public static HRESULT SCARD_E_NO_MEMORY = new HRESULT("0x80100006", "SCARD_E_NO_MEMORY", "Not enough memory available to complete this command.");

        /// <summary>
        /// An internal consistency timer has expired.
        /// </summary>
        public static HRESULT SCARD_F_WAITED_TOO_LONG = new HRESULT("0x80100007", "SCARD_F_WAITED_TOO_LONG", "An internal consistency timer has expired.");

        /// <summary>
        /// The data buffer to receive returned data is too small for the returned data.
        /// </summary>
        public static HRESULT SCARD_E_INSUFFICIENT_BUFFER = new HRESULT("0x80100008", "SCARD_E_INSUFFICIENT_BUFFER", "The data buffer to receive returned data is too small for the returned data.");

        /// <summary>
        /// The specified reader name is not recognized.
        /// </summary>
        public static HRESULT SCARD_E_UNKNOWN_READER = new HRESULT("0x80100009", "SCARD_E_UNKNOWN_READER", "The specified reader name is not recognized.");

        /// <summary>
        /// The user-specified timeout value has expired.
        /// </summary>
        public static HRESULT SCARD_E_TIMEOUT = new HRESULT("0x8010000A", "SCARD_E_TIMEOUT", "The user-specified timeout value has expired.");

        /// <summary>
        /// The smart card cannot be accessed because of other connections outstanding.
        /// </summary>
        public static HRESULT SCARD_E_SHARING_VIOLATION = new HRESULT("0x8010000B", "SCARD_E_SHARING_VIOLATION", "The smart card cannot be accessed because of other connections outstanding.");

        /// <summary>
        /// The operation requires a Smart Card, but no Smart Card is currently in the device.
        /// </summary>
        public static HRESULT SCARD_E_NO_SMARTCARD = new HRESULT("0x8010000C", "SCARD_E_NO_SMARTCARD", "The operation requires a Smart Card, but no Smart Card is currently in the device.");

        /// <summary>
        /// The specified smart card name is not recognized.
        /// </summary>
        public static HRESULT SCARD_E_UNKNOWN_CARD = new HRESULT("0x8010000D", "SCARD_E_UNKNOWN_CARD", "The specified smart card name is not recognized.");

        /// <summary>
        /// The system could not dispose of the media in the requested manner.
        /// </summary>
        public static HRESULT SCARD_E_CANT_DISPOSE = new HRESULT("0x8010000E", "SCARD_E_CANT_DISPOSE", "The system could not dispose of the media in the requested manner.");

        /// <summary>
        /// The requested protocols are incompatible with the protocol currently in use with the smart card.
        /// </summary>
        public static HRESULT SCARD_E_PROTO_MISMATCH = new HRESULT("0x8010000F", "SCARD_E_PROTO_MISMATCH", "The requested protocols are incompatible with the protocol currently in use with the smart card.");

        /// <summary>
        /// The reader or smart card is not ready to accept commands.
        /// </summary>
        public static HRESULT SCARD_E_NOT_READY = new HRESULT("0x80100010", "SCARD_E_NOT_READY", "The reader or smart card is not ready to accept commands.");

        /// <summary>
        /// One or more of the supplied parameters values could not be properly interpreted.
        /// </summary>
        public static HRESULT SCARD_E_INVALID_VALUE = new HRESULT("0x80100011", "SCARD_E_INVALID_VALUE", "One or more of the supplied parameters values could not be properly interpreted.");

        /// <summary>
        /// The action was canceled by the system, presumably to log off or shut down.
        /// </summary>
        public static HRESULT SCARD_E_SYSTEM_CANCELLED = new HRESULT("0x80100012", "SCARD_E_SYSTEM_CANCELLED", "The action was canceled by the system, presumably to log off or shut down.");

        /// <summary>
        /// An internal communications error has been detected.
        /// </summary>
        public static HRESULT SCARD_F_COMM_ERROR = new HRESULT("0x80100013", "SCARD_F_COMM_ERROR", "An internal communications error has been detected.");

        /// <summary>
        /// An internal error has been detected, but the source is unknown.
        /// </summary>
        public static HRESULT SCARD_F_UNKNOWN_ERROR = new HRESULT("0x80100014", "SCARD_F_UNKNOWN_ERROR", "An internal error has been detected, but the source is unknown.");

        /// <summary>
        /// An ATR obtained from the registry is not a valid ATR string.
        /// </summary>
        public static HRESULT SCARD_E_INVALID_ATR = new HRESULT("0x80100015", "SCARD_E_INVALID_ATR", "An ATR obtained from the registry is not a valid ATR string.");

        /// <summary>
        /// An attempt was made to end a non-existent transaction.
        /// </summary>
        public static HRESULT SCARD_E_NOT_TRANSACTED = new HRESULT("0x80100016", "SCARD_E_NOT_TRANSACTED", "An attempt was made to end a non-existent transaction.");

        /// <summary>
        /// The specified reader is not currently available for use.
        /// </summary>
        public static HRESULT SCARD_E_READER_UNAVAILABLE = new HRESULT("0x80100017", "SCARD_E_READER_UNAVAILABLE", "The specified reader is not currently available for use.");

        /// <summary>
        /// The operation has been aborted to allow the server application to exit.
        /// </summary>
        public static HRESULT SCARD_P_SHUTDOWN = new HRESULT("0x80100018", "SCARD_P_SHUTDOWN", "The operation has been aborted to allow the server application to exit.");

        /// <summary>
        /// The PCI Receive buffer was too small.
        /// </summary>
        public static HRESULT SCARD_E_PCI_TOO_SMALL = new HRESULT("0x80100019", "SCARD_E_PCI_TOO_SMALL", "The PCI Receive buffer was too small.");

        /// <summary>
        /// The reader driver does not meet minimal requirements for support.
        /// </summary>
        public static HRESULT SCARD_E_READER_UNSUPPORTED = new HRESULT("0x8010001A", "SCARD_E_READER_UNSUPPORTED", "The reader driver does not meet minimal requirements for support.");

        /// <summary>
        /// The reader driver did not produce a unique reader name.
        /// </summary>
        public static HRESULT SCARD_E_DUPLICATE_READER = new HRESULT("0x8010001B", "SCARD_E_DUPLICATE_READER", "The reader driver did not produce a unique reader name.");

        /// <summary>
        /// The smart card does not meet minimal requirements for support.
        /// </summary>
        public static HRESULT SCARD_E_CARD_UNSUPPORTED = new HRESULT("0x8010001C", "SCARD_E_CARD_UNSUPPORTED", "The smart card does not meet minimal requirements for support.");

        /// <summary>
        /// The Smart card resource manager is not running.
        /// </summary>
        public static HRESULT SCARD_E_NO_SERVICE = new HRESULT("0x8010001D", "SCARD_E_NO_SERVICE", "The Smart card resource manager is not running.");

        /// <summary>
        /// The Smart card resource manager has shut down.
        /// </summary>
        public static HRESULT SCARD_E_SERVICE_STOPPED = new HRESULT("0x8010001E", "SCARD_E_SERVICE_STOPPED", "The Smart card resource manager has shut down.");

        /// <summary>
        /// An unexpected card error has occurred.
        /// </summary>
        public static HRESULT SCARD_E_UNEXPECTED = new HRESULT("0x8010001F", "SCARD_E_UNEXPECTED", "An unexpected card error has occurred.");

        /// <summary>
        /// No Primary Provider can be found for the smart card.
        /// </summary>
        public static HRESULT SCARD_E_ICC_INSTALLATION = new HRESULT("0x80100020", "SCARD_E_ICC_INSTALLATION", "No Primary Provider can be found for the smart card.");

        /// <summary>
        /// The requested order of object creation is not supported.
        /// </summary>
        public static HRESULT SCARD_E_ICC_CREATEORDER = new HRESULT("0x80100021", "SCARD_E_ICC_CREATEORDER", "The requested order of object creation is not supported.");

        /// <summary>
        /// This smart card does not support the requested feature.
        /// </summary>
        public static HRESULT SCARD_E_UNSUPPORTED_FEATURE = new HRESULT("0x80100022", "SCARD_E_UNSUPPORTED_FEATURE", "This smart card does not support the requested feature.");

        /// <summary>
        /// The identified directory does not exist in the smart card.
        /// </summary>
        public static HRESULT SCARD_E_DIR_NOT_FOUND = new HRESULT("0x80100023", "SCARD_E_DIR_NOT_FOUND", "The identified directory does not exist in the smart card.");

        /// <summary>
        /// The identified file does not exist in the smart card.
        /// </summary>
        public static HRESULT SCARD_E_FILE_NOT_FOUND = new HRESULT("0x80100024", "SCARD_E_FILE_NOT_FOUND", "The identified file does not exist in the smart card.");

        /// <summary>
        /// The supplied path does not represent a smart card directory.
        /// </summary>
        public static HRESULT SCARD_E_NO_DIR = new HRESULT("0x80100025", "SCARD_E_NO_DIR", "The supplied path does not represent a smart card directory.");

        /// <summary>
        /// The supplied path does not represent a smart card file.
        /// </summary>
        public static HRESULT SCARD_E_NO_FILE = new HRESULT("0x80100026", "SCARD_E_NO_FILE", "The supplied path does not represent a smart card file.");

        /// <summary>
        /// Access is denied to this file.
        /// </summary>
        public static HRESULT SCARD_E_NO_ACCESS = new HRESULT("0x80100027", "SCARD_E_NO_ACCESS", "Access is denied to this file.");

        /// <summary>
        /// The smartcard does not have enough memory to store the information.
        /// </summary>
        public static HRESULT SCARD_E_WRITE_TOO_MANY = new HRESULT("0x80100028", "SCARD_E_WRITE_TOO_MANY", "The smartcard does not have enough memory to store the information.");

        /// <summary>
        /// There was an error trying to set the smart card file object pointer.
        /// </summary>
        public static HRESULT SCARD_E_BAD_SEEK = new HRESULT("0x80100029", "SCARD_E_BAD_SEEK", "There was an error trying to set the smart card file object pointer.");

        /// <summary>
        /// The supplied PIN is incorrect.
        /// </summary>
        public static HRESULT SCARD_E_INVALID_CHV = new HRESULT("0x8010002A", "SCARD_E_INVALID_CHV", "The supplied PIN is incorrect.");

        /// <summary>
        /// An unrecognized error code was returned from a layered component.
        /// </summary>
        public static HRESULT SCARD_E_UNKNOWN_RES_MNG = new HRESULT("0x8010002B", "SCARD_E_UNKNOWN_RES_MNG", "An unrecognized error code was returned from a layered component.");

        /// <summary>
        /// The requested certificate does not exist.
        /// </summary>
        public static HRESULT SCARD_E_NO_SUCH_CERTIFICATE = new HRESULT("0x8010002C", "SCARD_E_NO_SUCH_CERTIFICATE", "The requested certificate does not exist.");

        /// <summary>
        /// The requested certificate could not be obtained.
        /// </summary>
        public static HRESULT SCARD_E_CERTIFICATE_UNAVAILABLE = new HRESULT("0x8010002D", "SCARD_E_CERTIFICATE_UNAVAILABLE", "The requested certificate could not be obtained.");

        /// <summary>
        /// Cannot find a smart card reader.
        /// </summary>
        public static HRESULT SCARD_E_NO_READERS_AVAILABLE = new HRESULT("0x8010002E", "SCARD_E_NO_READERS_AVAILABLE", "Cannot find a smart card reader.");

        /// <summary>
        /// A communications error with the smart card has been detected. Retry the operation.
        /// </summary>
        public static HRESULT SCARD_E_COMM_DATA_LOST = new HRESULT("0x8010002F", "SCARD_E_COMM_DATA_LOST", "A communications error with the smart card has been detected. Retry the operation.");

        /// <summary>
        /// The requested key container does not exist on the smart card.
        /// </summary>
        public static HRESULT SCARD_E_NO_KEY_CONTAINER = new HRESULT("0x80100030", "SCARD_E_NO_KEY_CONTAINER", "The requested key container does not exist on the smart card.");

        /// <summary>
        /// The Smart card resource manager is too busy to complete this operation.
        /// </summary>
        public static HRESULT SCARD_E_SERVER_TOO_BUSY = new HRESULT("0x80100031", "SCARD_E_SERVER_TOO_BUSY", "The Smart card resource manager is too busy to complete this operation.");

        /// <summary>
        /// The smart card PIN cache has expired.
        /// </summary>
        public static HRESULT SCARD_E_PIN_CACHE_EXPIRED = new HRESULT("0x80100032", "SCARD_E_PIN_CACHE_EXPIRED", "The smart card PIN cache has expired.");

        /// <summary>
        /// The smart card PIN cannot be cached.
        /// </summary>
        public static HRESULT SCARD_E_NO_PIN_CACHE = new HRESULT("0x80100033", "SCARD_E_NO_PIN_CACHE", "The smart card PIN cannot be cached.");

        /// <summary>
        /// The smart card is read only and cannot be written to.
        /// </summary>
        public static HRESULT SCARD_E_READ_ONLY_CARD = new HRESULT("0x80100034", "SCARD_E_READ_ONLY_CARD", "The smart card is read only and cannot be written to.");

        /// <summary>
        /// The reader cannot communicate with the smart card, due to ATR configuration conflicts.
        /// </summary>
        public static HRESULT SCARD_W_UNSUPPORTED_CARD = new HRESULT("0x80100065", "SCARD_W_UNSUPPORTED_CARD", "The reader cannot communicate with the smart card, due to ATR configuration conflicts.");

        /// <summary>
        /// The smart card is not responding to a reset.
        /// </summary>
        public static HRESULT SCARD_W_UNRESPONSIVE_CARD = new HRESULT("0x80100066", "SCARD_W_UNRESPONSIVE_CARD", "The smart card is not responding to a reset.");

        /// <summary>
        /// Power has been removed from the smart card, so that further communication is not possible.
        /// </summary>
        public static HRESULT SCARD_W_UNPOWERED_CARD = new HRESULT("0x80100067", "SCARD_W_UNPOWERED_CARD", "Power has been removed from the smart card, so that further communication is not possible.");

        /// <summary>
        /// The smart card has been reset, so any shared state information is invalid.
        /// </summary>
        public static HRESULT SCARD_W_RESET_CARD = new HRESULT("0x80100068", "SCARD_W_RESET_CARD", "The smart card has been reset, so any shared state information is invalid.");

        /// <summary>
        /// The smart card has been removed, so that further communication is not possible.
        /// </summary>
        public static HRESULT SCARD_W_REMOVED_CARD = new HRESULT("0x80100069", "SCARD_W_REMOVED_CARD", "The smart card has been removed, so that further communication is not possible.");

        /// <summary>
        /// Access was denied because of a security violation.
        /// </summary>
        public static HRESULT SCARD_W_SECURITY_VIOLATION = new HRESULT("0x8010006A", "SCARD_W_SECURITY_VIOLATION", "Access was denied because of a security violation.");

        /// <summary>
        /// The card cannot be accessed because the wrong PIN was presented.
        /// </summary>
        public static HRESULT SCARD_W_WRONG_CHV = new HRESULT("0x8010006B", "SCARD_W_WRONG_CHV", "The card cannot be accessed because the wrong PIN was presented.");

        /// <summary>
        /// The card cannot be accessed because the maximum number of PIN entry attempts has been reached.
        /// </summary>
        public static HRESULT SCARD_W_CHV_BLOCKED = new HRESULT("0x8010006C", "SCARD_W_CHV_BLOCKED", "The card cannot be accessed because the maximum number of PIN entry attempts has been reached.");

        /// <summary>
        /// The end of the smart card file has been reached.
        /// </summary>
        public static HRESULT SCARD_W_EOF = new HRESULT("0x8010006D", "SCARD_W_EOF", "The end of the smart card file has been reached.");

        /// <summary>
        /// The action was canceled by the user.
        /// </summary>
        public static HRESULT SCARD_W_CANCELLED_BY_USER = new HRESULT("0x8010006E", "SCARD_W_CANCELLED_BY_USER", "The action was canceled by the user.");

        /// <summary>
        /// No PIN was presented to the smart card.
        /// </summary>
        public static HRESULT SCARD_W_CARD_NOT_AUTHENTICATED = new HRESULT("0x8010006F", "SCARD_W_CARD_NOT_AUTHENTICATED", "No PIN was presented to the smart card.");

        /// <summary>
        /// The requested item could not be found in the cache.
        /// </summary>
        public static HRESULT SCARD_W_CACHE_ITEM_NOT_FOUND = new HRESULT("0x80100070", "SCARD_W_CACHE_ITEM_NOT_FOUND", "The requested item could not be found in the cache.");

        /// <summary>
        /// The requested cache item is too old and was deleted from the cache.
        /// </summary>
        public static HRESULT SCARD_W_CACHE_ITEM_STALE = new HRESULT("0x80100071", "SCARD_W_CACHE_ITEM_STALE", "The requested cache item is too old and was deleted from the cache.");

        /// <summary>
        /// The new cache item exceeds the maximum per-item size defined for the cache.
        /// </summary>
        public static HRESULT SCARD_W_CACHE_ITEM_TOO_BIG = new HRESULT("0x80100072", "SCARD_W_CACHE_ITEM_TOO_BIG", "The new cache item exceeds the maximum per-item size defined for the cache.");

        /// <summary>
        /// Authentication target is invalid or not configured correctly.
        /// </summary>
        public static HRESULT ONL_E_INVALID_AUTHENTICATION_TARGET = new HRESULT("0x8A020001", "ONL_E_INVALID_AUTHENTICATION_TARGET", "Authentication target is invalid or not configured correctly.");
        #endregion

        #region COM Error Codes (COMADMIN, FILTER, GRAPHICS)
        /// <summary>
        /// Errors occurred accessing one or more objects - the ErrorInfo collection may have more detail
        /// </summary>
        public static HRESULT COMADMIN_E_OBJECTERRORS = new HRESULT("0x80110401", "COMADMIN_E_OBJECTERRORS", "Errors occurred accessing one or more objects - the ErrorInfo collection may have more detail");

        /// <summary>
        /// One or more of the object's properties are missing or invalid
        /// </summary>
        public static HRESULT COMADMIN_E_OBJECTINVALID = new HRESULT("0x80110402", "COMADMIN_E_OBJECTINVALID", "One or more of the object's properties are missing or invalid");

        /// <summary>
        /// The object was not found in the catalog
        /// </summary>
        public static HRESULT COMADMIN_E_KEYMISSING = new HRESULT("0x80110403", "COMADMIN_E_KEYMISSING", "The object was not found in the catalog");

        /// <summary>
        /// The object is already registered
        /// </summary>
        public static HRESULT COMADMIN_E_ALREADYINSTALLED = new HRESULT("0x80110404", "COMADMIN_E_ALREADYINSTALLED", "The object is already registered");

        /// <summary>
        /// Error occurred writing to the application file
        /// </summary>
        public static HRESULT COMADMIN_E_APP_FILE_WRITEFAIL = new HRESULT("0x80110407", "COMADMIN_E_APP_FILE_WRITEFAIL", "Error occurred writing to the application file");

        /// <summary>
        /// Error occurred reading the application file
        /// </summary>
        public static HRESULT COMADMIN_E_APP_FILE_READFAIL = new HRESULT("0x80110408", "COMADMIN_E_APP_FILE_READFAIL", "Error occurred reading the application file");

        /// <summary>
        /// Invalid version number in application file
        /// </summary>
        public static HRESULT COMADMIN_E_APP_FILE_VERSION = new HRESULT("0x80110409", "COMADMIN_E_APP_FILE_VERSION", "Invalid version number in application file");

        /// <summary>
        /// The file path is invalid
        /// </summary>
        public static HRESULT COMADMIN_E_BADPATH = new HRESULT("0x8011040A", "COMADMIN_E_BADPATH", "The file path is invalid");

        /// <summary>
        /// The application is already installed
        /// </summary>
        public static HRESULT COMADMIN_E_APPLICATIONEXISTS = new HRESULT("0x8011040B", "COMADMIN_E_APPLICATIONEXISTS", "The application is already installed");

        /// <summary>
        /// The role already exists
        /// </summary>
        public static HRESULT COMADMIN_E_ROLEEXISTS = new HRESULT("0x8011040C", "COMADMIN_E_ROLEEXISTS", "The role already exists");

        /// <summary>
        /// An error occurred copying the file
        /// </summary>
        public static HRESULT COMADMIN_E_CANTCOPYFILE = new HRESULT("0x8011040D", "COMADMIN_E_CANTCOPYFILE", "An error occurred copying the file");

        /// <summary>
        /// One or more users are not valid
        /// </summary>
        public static HRESULT COMADMIN_E_NOUSER = new HRESULT("0x8011040F", "COMADMIN_E_NOUSER", "One or more users are not valid");

        /// <summary>
        /// One or more users in the application file are not valid
        /// </summary>
        public static HRESULT COMADMIN_E_INVALIDUSERIDS = new HRESULT("0x80110410", "COMADMIN_E_INVALIDUSERIDS", "One or more users in the application file are not valid");

        /// <summary>
        /// The component's CLSID is missing or corrupt
        /// </summary>
        public static HRESULT COMADMIN_E_NOREGISTRYCLSID = new HRESULT("0x80110411", "COMADMIN_E_NOREGISTRYCLSID", "The component's CLSID is missing or corrupt");

        /// <summary>
        /// The component's progID is missing or corrupt
        /// </summary>
        public static HRESULT COMADMIN_E_BADREGISTRYPROGID = new HRESULT("0x80110412", "COMADMIN_E_BADREGISTRYPROGID", "The component's progID is missing or corrupt");

        /// <summary>
        /// Unable to set required authentication level for update request
        /// </summary>
        public static HRESULT COMADMIN_E_AUTHENTICATIONLEVEL = new HRESULT("0x80110413", "COMADMIN_E_AUTHENTICATIONLEVEL", "Unable to set required authentication level for update request");

        /// <summary>
        /// The identity or password set on the application is not valid
        /// </summary>
        public static HRESULT COMADMIN_E_USERPASSWDNOTVALID = new HRESULT("0x80110414", "COMADMIN_E_USERPASSWDNOTVALID", "The identity or password set on the application is not valid");

        /// <summary>
        /// Application file CLSIDs or IIDs do not match corresponding DLLs
        /// </summary>
        public static HRESULT COMADMIN_E_CLSIDORIIDMISMATCH = new HRESULT("0x80110418", "COMADMIN_E_CLSIDORIIDMISMATCH", "Application file CLSIDs or IIDs do not match corresponding DLLs");

        /// <summary>
        /// Interface information is either missing or changed
        /// </summary>
        public static HRESULT COMADMIN_E_REMOTEINTERFACE = new HRESULT("0x80110419", "COMADMIN_E_REMOTEINTERFACE", "Interface information is either missing or changed");

        /// <summary>
        /// DllRegisterServer failed on component install
        /// </summary>
        public static HRESULT COMADMIN_E_DLLREGISTERSERVER = new HRESULT("0x8011041A", "COMADMIN_E_DLLREGISTERSERVER", "DllRegisterServer failed on component install");

        /// <summary>
        /// No server file share available
        /// </summary>
        public static HRESULT COMADMIN_E_NOSERVERSHARE = new HRESULT("0x8011041B", "COMADMIN_E_NOSERVERSHARE", "No server file share available");

        /// <summary>
        /// DLL could not be loaded
        /// </summary>
        public static HRESULT COMADMIN_E_DLLLOADFAILED = new HRESULT("0x8011041D", "COMADMIN_E_DLLLOADFAILED", "DLL could not be loaded");

        /// <summary>
        /// The registered TypeLib ID is not valid
        /// </summary>
        public static HRESULT COMADMIN_E_BADREGISTRYLIBID = new HRESULT("0x8011041E", "COMADMIN_E_BADREGISTRYLIBID", "The registered TypeLib ID is not valid");

        /// <summary>
        /// Application install directory not found
        /// </summary>
        public static HRESULT COMADMIN_E_APPDIRNOTFOUND = new HRESULT("0x8011041F", "COMADMIN_E_APPDIRNOTFOUND", "Application install directory not found");

        /// <summary>
        /// Errors occurred while in the component registrar
        /// </summary>
        public static HRESULT COMADMIN_E_REGISTRARFAILED = new HRESULT("0x80110423", "COMADMIN_E_REGISTRARFAILED", "Errors occurred while in the component registrar");

        /// <summary>
        /// The file does not exist
        /// </summary>
        public static HRESULT COMADMIN_E_COMPFILE_DOESNOTEXIST = new HRESULT("0x80110424", "COMADMIN_E_COMPFILE_DOESNOTEXIST", "The file does not exist");

        /// <summary>
        /// The DLL could not be loaded
        /// </summary>
        public static HRESULT COMADMIN_E_COMPFILE_LOADDLLFAIL = new HRESULT("0x80110425", "COMADMIN_E_COMPFILE_LOADDLLFAIL", "The DLL could not be loaded");

        /// <summary>
        /// GetClassObject failed in the DLL
        /// </summary>
        public static HRESULT COMADMIN_E_COMPFILE_GETCLASSOBJ = new HRESULT("0x80110426", "COMADMIN_E_COMPFILE_GETCLASSOBJ", "GetClassObject failed in the DLL");

        /// <summary>
        /// The DLL does not support the components listed in the TypeLib
        /// </summary>
        public static HRESULT COMADMIN_E_COMPFILE_CLASSNOTAVAIL = new HRESULT("0x80110427", "COMADMIN_E_COMPFILE_CLASSNOTAVAIL", "The DLL does not support the components listed in the TypeLib");

        /// <summary>
        /// The TypeLib could not be loaded
        /// </summary>
        public static HRESULT COMADMIN_E_COMPFILE_BADTLB = new HRESULT("0x80110428", "COMADMIN_E_COMPFILE_BADTLB", "The TypeLib could not be loaded");

        /// <summary>
        /// The file does not contain components or component information
        /// </summary>
        public static HRESULT COMADMIN_E_COMPFILE_NOTINSTALLABLE = new HRESULT("0x80110429", "COMADMIN_E_COMPFILE_NOTINSTALLABLE", "The file does not contain components or component information");

        /// <summary>
        /// Changes to this object and its sub-objects have been disabled
        /// </summary>
        public static HRESULT COMADMIN_E_NOTCHANGEABLE = new HRESULT("0x8011042A", "COMADMIN_E_NOTCHANGEABLE", "Changes to this object and its sub-objects have been disabled");

        /// <summary>
        /// The delete function has been disabled for this object
        /// </summary>
        public static HRESULT COMADMIN_E_NOTDELETEABLE = new HRESULT("0x8011042B", "COMADMIN_E_NOTDELETEABLE", "The delete function has been disabled for this object");

        /// <summary>
        /// The server catalog version is not supported
        /// </summary>
        public static HRESULT COMADMIN_E_SESSION = new HRESULT("0x8011042C", "COMADMIN_E_SESSION", "The server catalog version is not supported");

        /// <summary>
        /// The component move was disallowed, because the source or destination application is either a system application or currently locked against changes
        /// </summary>
        public static HRESULT COMADMIN_E_COMP_MOVE_LOCKED = new HRESULT("0x8011042D", "COMADMIN_E_COMP_MOVE_LOCKED", "The component move was disallowed, because the source or destination application is either a system application or currently locked against changes");

        /// <summary>
        /// The component move failed because the destination application no longer exists
        /// </summary>
        public static HRESULT COMADMIN_E_COMP_MOVE_BAD_DEST = new HRESULT("0x8011042E", "COMADMIN_E_COMP_MOVE_BAD_DEST", "The component move failed because the destination application no longer exists");

        /// <summary>
        /// The system was unable to register the TypeLib
        /// </summary>
        public static HRESULT COMADMIN_E_REGISTERTLB = new HRESULT("0x80110430", "COMADMIN_E_REGISTERTLB", "The system was unable to register the TypeLib");

        /// <summary>
        /// This operation cannot be performed on the system application
        /// </summary>
        public static HRESULT COMADMIN_E_SYSTEMAPP = new HRESULT("0x80110433", "COMADMIN_E_SYSTEMAPP", "This operation cannot be performed on the system application");

        /// <summary>
        /// The component registrar referenced in this file is not available
        /// </summary>
        public static HRESULT COMADMIN_E_COMPFILE_NOREGISTRAR = new HRESULT("0x80110434", "COMADMIN_E_COMPFILE_NOREGISTRAR", "The component registrar referenced in this file is not available");

        /// <summary>
        /// A component in the same DLL is already installed
        /// </summary>
        public static HRESULT COMADMIN_E_COREQCOMPINSTALLED = new HRESULT("0x80110435", "COMADMIN_E_COREQCOMPINSTALLED", "A component in the same DLL is already installed");

        /// <summary>
        /// The service is not installed
        /// </summary>
        public static HRESULT COMADMIN_E_SERVICENOTINSTALLED = new HRESULT("0x80110436", "COMADMIN_E_SERVICENOTINSTALLED", "The service is not installed");

        /// <summary>
        /// One or more property settings are either invalid or in conflict with each other
        /// </summary>
        public static HRESULT COMADMIN_E_PROPERTYSAVEFAILED = new HRESULT("0x80110437", "COMADMIN_E_PROPERTYSAVEFAILED", "One or more property settings are either invalid or in conflict with each other");

        /// <summary>
        /// The object you are attempting to add or rename already exists
        /// </summary>
        public static HRESULT COMADMIN_E_OBJECTEXISTS = new HRESULT("0x80110438", "COMADMIN_E_OBJECTEXISTS", "The object you are attempting to add or rename already exists");

        /// <summary>
        /// The component already exists
        /// </summary>
        public static HRESULT COMADMIN_E_COMPONENTEXISTS = new HRESULT("0x80110439", "COMADMIN_E_COMPONENTEXISTS", "The component already exists");

        /// <summary>
        /// The registration file is corrupt
        /// </summary>
        public static HRESULT COMADMIN_E_REGFILE_CORRUPT = new HRESULT("0x8011043B", "COMADMIN_E_REGFILE_CORRUPT", "The registration file is corrupt");

        /// <summary>
        /// The property value is too large
        /// </summary>
        public static HRESULT COMADMIN_E_PROPERTY_OVERFLOW = new HRESULT("0x8011043C", "COMADMIN_E_PROPERTY_OVERFLOW", "The property value is too large");

        /// <summary>
        /// Object was not found in registry
        /// </summary>
        public static HRESULT COMADMIN_E_NOTINREGISTRY = new HRESULT("0x8011043E", "COMADMIN_E_NOTINREGISTRY", "Object was not found in registry");

        /// <summary>
        /// This object is not poolable
        /// </summary>
        public static HRESULT COMADMIN_E_OBJECTNOTPOOLABLE = new HRESULT("0x8011043F", "COMADMIN_E_OBJECTNOTPOOLABLE", "This object is not poolable");

        /// <summary>
        /// A CLSID with the same GUID as the new application ID is already installed on this machine
        /// </summary>
        public static HRESULT COMADMIN_E_APPLID_MATCHES_CLSID = new HRESULT("0x80110446", "COMADMIN_E_APPLID_MATCHES_CLSID", "A CLSID with the same GUID as the new application ID is already installed on this machine");

        /// <summary>
        /// A role assigned to a component, interface, or method did not exist in the application
        /// </summary>
        public static HRESULT COMADMIN_E_ROLE_DOES_NOT_EXIST = new HRESULT("0x80110447", "COMADMIN_E_ROLE_DOES_NOT_EXIST", "A role assigned to a component, interface, or method did not exist in the application");

        /// <summary>
        /// You must have components in an application in order to start the application
        /// </summary>
        public static HRESULT COMADMIN_E_START_APP_NEEDS_COMPONENTS = new HRESULT("0x80110448", "COMADMIN_E_START_APP_NEEDS_COMPONENTS", "You must have components in an application in order to start the application");

        /// <summary>
        /// This operation is not enabled on this platform
        /// </summary>
        public static HRESULT COMADMIN_E_REQUIRES_DIFFERENT_PLATFORM = new HRESULT("0x80110449", "COMADMIN_E_REQUIRES_DIFFERENT_PLATFORM", "This operation is not enabled on this platform");

        /// <summary>
        /// Application Proxy is not exportable
        /// </summary>
        public static HRESULT COMADMIN_E_CAN_NOT_EXPORT_APP_PROXY = new HRESULT("0x8011044A", "COMADMIN_E_CAN_NOT_EXPORT_APP_PROXY", "Application Proxy is not exportable");

        /// <summary>
        /// Failed to start application because it is either a library application or an application proxy
        /// </summary>
        public static HRESULT COMADMIN_E_CAN_NOT_START_APP = new HRESULT("0x8011044B", "COMADMIN_E_CAN_NOT_START_APP", "Failed to start application because it is either a library application or an application proxy");

        /// <summary>
        /// System application is not exportable
        /// </summary>
        public static HRESULT COMADMIN_E_CAN_NOT_EXPORT_SYS_APP = new HRESULT("0x8011044C", "COMADMIN_E_CAN_NOT_EXPORT_SYS_APP", "System application is not exportable");

        /// <summary>
        /// Cannot subscribe to this component (the component may have been imported)
        /// </summary>
        public static HRESULT COMADMIN_E_CANT_SUBSCRIBE_TO_COMPONENT = new HRESULT("0x8011044D", "COMADMIN_E_CANT_SUBSCRIBE_TO_COMPONENT", "Cannot subscribe to this component (the component may have been imported)");

        /// <summary>
        /// An event class cannot also be a subscriber component
        /// </summary>
        public static HRESULT COMADMIN_E_EVENTCLASS_CANT_BE_SUBSCRIBER = new HRESULT("0x8011044E", "COMADMIN_E_EVENTCLASS_CANT_BE_SUBSCRIBER", "An event class cannot also be a subscriber component");

        /// <summary>
        /// Library applications and application proxies are incompatible
        /// </summary>
        public static HRESULT COMADMIN_E_LIB_APP_PROXY_INCOMPATIBLE = new HRESULT("0x8011044F", "COMADMIN_E_LIB_APP_PROXY_INCOMPATIBLE", "Library applications and application proxies are incompatible");

        /// <summary>
        /// This function is valid for the base partition only
        /// </summary>
        public static HRESULT COMADMIN_E_BASE_PARTITION_ONLY = new HRESULT("0x80110450", "COMADMIN_E_BASE_PARTITION_ONLY", "This function is valid for the base partition only");

        /// <summary>
        /// You cannot start an application that has been disabled
        /// </summary>
        public static HRESULT COMADMIN_E_START_APP_DISABLED = new HRESULT("0x80110451", "COMADMIN_E_START_APP_DISABLED", "You cannot start an application that has been disabled");

        /// <summary>
        /// The specified partition name is already in use on this computer
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_DUPLICATE_PARTITION_NAME = new HRESULT("0x80110457", "COMADMIN_E_CAT_DUPLICATE_PARTITION_NAME", "The specified partition name is already in use on this computer");

        /// <summary>
        /// The specified partition name is invalid. Check that the name contains at least one visible character
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_INVALID_PARTITION_NAME = new HRESULT("0x80110458", "COMADMIN_E_CAT_INVALID_PARTITION_NAME", "The specified partition name is invalid. Check that the name contains at least one visible character");

        /// <summary>
        /// The partition cannot be deleted because it is the default partition for one or more users
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_PARTITION_IN_USE = new HRESULT("0x80110459", "COMADMIN_E_CAT_PARTITION_IN_USE", "The partition cannot be deleted because it is the default partition for one or more users");

        /// <summary>
        /// The partition cannot be exported, because one or more components in the partition have the same file name
        /// </summary>
        public static HRESULT COMADMIN_E_FILE_PARTITION_DUPLICATE_FILES = new HRESULT("0x8011045A", "COMADMIN_E_FILE_PARTITION_DUPLICATE_FILES", "The partition cannot be exported, because one or more components in the partition have the same file name");

        /// <summary>
        /// Applications that contain one or more imported components cannot be installed into a non-base partition
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_IMPORTED_COMPONENTS_NOT_ALLOWED = new HRESULT("0x8011045B", "COMADMIN_E_CAT_IMPORTED_COMPONENTS_NOT_ALLOWED", "Applications that contain one or more imported components cannot be installed into a non-base partition");

        /// <summary>
        /// The application name is not unique and cannot be resolved to an application id
        /// </summary>
        public static HRESULT COMADMIN_E_AMBIGUOUS_APPLICATION_NAME = new HRESULT("0x8011045C", "COMADMIN_E_AMBIGUOUS_APPLICATION_NAME", "The application name is not unique and cannot be resolved to an application id");

        /// <summary>
        /// The partition name is not unique and cannot be resolved to a partition id
        /// </summary>
        public static HRESULT COMADMIN_E_AMBIGUOUS_PARTITION_NAME = new HRESULT("0x8011045D", "COMADMIN_E_AMBIGUOUS_PARTITION_NAME", "The partition name is not unique and cannot be resolved to a partition id");

        /// <summary>
        /// The COM+ registry database has not been initialized
        /// </summary>
        public static HRESULT COMADMIN_E_REGDB_NOTINITIALIZED = new HRESULT("0x80110472", "COMADMIN_E_REGDB_NOTINITIALIZED", "The COM+ registry database has not been initialized");

        /// <summary>
        /// The COM+ registry database is not open
        /// </summary>
        public static HRESULT COMADMIN_E_REGDB_NOTOPEN = new HRESULT("0x80110473", "COMADMIN_E_REGDB_NOTOPEN", "The COM+ registry database is not open");

        /// <summary>
        /// The COM+ registry database detected a system error
        /// </summary>
        public static HRESULT COMADMIN_E_REGDB_SYSTEMERR = new HRESULT("0x80110474", "COMADMIN_E_REGDB_SYSTEMERR", "The COM+ registry database detected a system error");

        /// <summary>
        /// The COM+ registry database is already running
        /// </summary>
        public static HRESULT COMADMIN_E_REGDB_ALREADYRUNNING = new HRESULT("0x80110475", "COMADMIN_E_REGDB_ALREADYRUNNING", "The COM+ registry database is already running");

        /// <summary>
        /// This version of the COM+ registry database cannot be migrated
        /// </summary>
        public static HRESULT COMADMIN_E_MIG_VERSIONNOTSUPPORTED = new HRESULT("0x80110480", "COMADMIN_E_MIG_VERSIONNOTSUPPORTED", "This version of the COM+ registry database cannot be migrated");

        /// <summary>
        /// The schema version to be migrated could not be found in the COM+ registry database
        /// </summary>
        public static HRESULT COMADMIN_E_MIG_SCHEMANOTFOUND = new HRESULT("0x80110481", "COMADMIN_E_MIG_SCHEMANOTFOUND", "The schema version to be migrated could not be found in the COM+ registry database");

        /// <summary>
        /// There was a type mismatch between binaries
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_BITNESSMISMATCH = new HRESULT("0x80110482", "COMADMIN_E_CAT_BITNESSMISMATCH", "There was a type mismatch between binaries");

        /// <summary>
        /// A binary of unknown or invalid type was provided
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_UNACCEPTABLEBITNESS = new HRESULT("0x80110483", "COMADMIN_E_CAT_UNACCEPTABLEBITNESS", "A binary of unknown or invalid type was provided");

        /// <summary>
        /// There was a type mismatch between a binary and an application
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_WRONGAPPBITNESS = new HRESULT("0x80110484", "COMADMIN_E_CAT_WRONGAPPBITNESS", "There was a type mismatch between a binary and an application");

        /// <summary>
        /// The application cannot be paused or resumed
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_PAUSE_RESUME_NOT_SUPPORTED = new HRESULT("0x80110485", "COMADMIN_E_CAT_PAUSE_RESUME_NOT_SUPPORTED", "The application cannot be paused or resumed");

        /// <summary>
        /// The COM+ Catalog Server threw an exception during execution
        /// </summary>
        public static HRESULT COMADMIN_E_CAT_SERVERFAULT = new HRESULT("0x80110486", "COMADMIN_E_CAT_SERVERFAULT", "The COM+ Catalog Server threw an exception during execution");

        /// <summary>
        /// Only COM+ Applications marked &quot;queued&quot; can be invoked using the &quot;queue&quot; moniker
        /// </summary>
        public static HRESULT COMQC_E_APPLICATION_NOT_QUEUED = new HRESULT("0x80110600", "COMQC_E_APPLICATION_NOT_QUEUED", "Only COM+ Applications marked \"queued\" can be invoked using the \"queue\" moniker");

        /// <summary>
        /// At least one interface must be marked &quot;queued&quot; in order to create a queued component instance with the &quot;queue&quot; moniker
        /// </summary>
        public static HRESULT COMQC_E_NO_QUEUEABLE_INTERFACES = new HRESULT("0x80110601", "COMQC_E_NO_QUEUEABLE_INTERFACES", "At least one interface must be marked \"queued\" in order to create a queued component instance with the \"queue\" moniker");

        /// <summary>
        /// MSMQ is required for the requested operation and is not installed
        /// </summary>
        public static HRESULT COMQC_E_QUEUING_SERVICE_NOT_AVAILABLE = new HRESULT("0x80110602", "COMQC_E_QUEUING_SERVICE_NOT_AVAILABLE", "MSMQ is required for the requested operation and is not installed");

        /// <summary>
        /// Unable to marshal an interface that does not support IPersistStream
        /// </summary>
        public static HRESULT COMQC_E_NO_IPERSISTSTREAM = new HRESULT("0x80110603", "COMQC_E_NO_IPERSISTSTREAM", "Unable to marshal an interface that does not support IPersistStream");

        /// <summary>
        /// The message is improperly formatted or was damaged in transit
        /// </summary>
        public static HRESULT COMQC_E_BAD_MESSAGE = new HRESULT("0x80110604", "COMQC_E_BAD_MESSAGE", "The message is improperly formatted or was damaged in transit");

        /// <summary>
        /// An unauthenticated message was received by an application that accepts only authenticated messages
        /// </summary>
        public static HRESULT COMQC_E_UNAUTHENTICATED = new HRESULT("0x80110605", "COMQC_E_UNAUTHENTICATED", "An unauthenticated message was received by an application that accepts only authenticated messages");

        /// <summary>
        /// The message was requeued or moved by a user not in the &quot;QC Trusted User&quot; role
        /// </summary>
        public static HRESULT COMQC_E_UNTRUSTED_ENQUEUER = new HRESULT("0x80110606", "COMQC_E_UNTRUSTED_ENQUEUER", "The message was requeued or moved by a user not in the \"QC Trusted User\" role");

        /// <summary>
        /// Cannot create a duplicate resource of type Distributed Transaction Coordinator
        /// </summary>
        public static HRESULT MSDTC_E_DUPLICATE_RESOURCE = new HRESULT("0x80110701", "MSDTC_E_DUPLICATE_RESOURCE", "Cannot create a duplicate resource of type Distributed Transaction Coordinator");

        /// <summary>
        /// One of the objects being inserted or updated does not belong to a valid parent collection
        /// </summary>
        public static HRESULT COMADMIN_E_OBJECT_PARENT_MISSING = new HRESULT("0x80110808", "COMADMIN_E_OBJECT_PARENT_MISSING", "One of the objects being inserted or updated does not belong to a valid parent collection");

        /// <summary>
        /// One of the specified objects cannot be found
        /// </summary>
        public static HRESULT COMADMIN_E_OBJECT_DOES_NOT_EXIST = new HRESULT("0x80110809", "COMADMIN_E_OBJECT_DOES_NOT_EXIST", "One of the specified objects cannot be found");

        /// <summary>
        /// The specified application is not currently running
        /// </summary>
        public static HRESULT COMADMIN_E_APP_NOT_RUNNING = new HRESULT("0x8011080A", "COMADMIN_E_APP_NOT_RUNNING", "The specified application is not currently running");

        /// <summary>
        /// The partition(s) specified are not valid.
        /// </summary>
        public static HRESULT COMADMIN_E_INVALID_PARTITION = new HRESULT("0x8011080B", "COMADMIN_E_INVALID_PARTITION", "The partition(s) specified are not valid.");

        /// <summary>
        /// COM+ applications that run as NT service may not be pooled or recycled
        /// </summary>
        public static HRESULT COMADMIN_E_SVCAPP_NOT_POOLABLE_OR_RECYCLABLE = new HRESULT("0x8011080D", "COMADMIN_E_SVCAPP_NOT_POOLABLE_OR_RECYCLABLE", "COM+ applications that run as NT service may not be pooled or recycled");

        /// <summary>
        /// One or more users are already assigned to a local partition set.
        /// </summary>
        public static HRESULT COMADMIN_E_USER_IN_SET = new HRESULT("0x8011080E", "COMADMIN_E_USER_IN_SET", "One or more users are already assigned to a local partition set.");

        /// <summary>
        /// Library applications may not be recycled.
        /// </summary>
        public static HRESULT COMADMIN_E_CANTRECYCLELIBRARYAPPS = new HRESULT("0x8011080F", "COMADMIN_E_CANTRECYCLELIBRARYAPPS", "Library applications may not be recycled.");

        /// <summary>
        /// Applications running as NT services may not be recycled.
        /// </summary>
        public static HRESULT COMADMIN_E_CANTRECYCLESERVICEAPPS = new HRESULT("0x80110811", "COMADMIN_E_CANTRECYCLESERVICEAPPS", "Applications running as NT services may not be recycled.");

        /// <summary>
        /// The process has already been recycled.
        /// </summary>
        public static HRESULT COMADMIN_E_PROCESSALREADYRECYCLED = new HRESULT("0x80110812", "COMADMIN_E_PROCESSALREADYRECYCLED", "The process has already been recycled.");

        /// <summary>
        /// A paused process may not be recycled.
        /// </summary>
        public static HRESULT COMADMIN_E_PAUSEDPROCESSMAYNOTBERECYCLED = new HRESULT("0x80110813", "COMADMIN_E_PAUSEDPROCESSMAYNOTBERECYCLED", "A paused process may not be recycled.");

        /// <summary>
        /// Library applications may not be NT services.
        /// </summary>
        public static HRESULT COMADMIN_E_CANTMAKEINPROCSERVICE = new HRESULT("0x80110814", "COMADMIN_E_CANTMAKEINPROCSERVICE", "Library applications may not be NT services.");

        /// <summary>
        /// The ProgID provided to the copy operation is invalid. The ProgID is in use by another registered CLSID.
        /// </summary>
        public static HRESULT COMADMIN_E_PROGIDINUSEBYCLSID = new HRESULT("0x80110815", "COMADMIN_E_PROGIDINUSEBYCLSID", "The ProgID provided to the copy operation is invalid. The ProgID is in use by another registered CLSID.");

        /// <summary>
        /// The partition specified as default is not a member of the partition set.
        /// </summary>
        public static HRESULT COMADMIN_E_DEFAULT_PARTITION_NOT_IN_SET = new HRESULT("0x80110816", "COMADMIN_E_DEFAULT_PARTITION_NOT_IN_SET", "The partition specified as default is not a member of the partition set.");

        /// <summary>
        /// A recycled process may not be paused.
        /// </summary>
        public static HRESULT COMADMIN_E_RECYCLEDPROCESSMAYNOTBEPAUSED = new HRESULT("0x80110817", "COMADMIN_E_RECYCLEDPROCESSMAYNOTBEPAUSED", "A recycled process may not be paused.");

        /// <summary>
        /// Access to the specified partition is denied.
        /// </summary>
        public static HRESULT COMADMIN_E_PARTITION_ACCESSDENIED = new HRESULT("0x80110818", "COMADMIN_E_PARTITION_ACCESSDENIED", "Access to the specified partition is denied.");

        /// <summary>
        /// Only Application Files (*.MSI files) can be installed into partitions.
        /// </summary>
        public static HRESULT COMADMIN_E_PARTITION_MSI_ONLY = new HRESULT("0x80110819", "COMADMIN_E_PARTITION_MSI_ONLY", "Only Application Files (*.MSI files) can be installed into partitions.");

        /// <summary>
        /// Applications containing one or more legacy components may not be exported to 1.0 format.
        /// </summary>
        public static HRESULT COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_1_0_FORMAT = new HRESULT("0x8011081A", "COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_1_0_FORMAT", "Applications containing one or more legacy components may not be exported to 1.0 format.");

        /// <summary>
        /// Legacy components may not exist in non-base partitions.
        /// </summary>
        public static HRESULT COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_NONBASE_PARTITIONS = new HRESULT("0x8011081B", "COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_NONBASE_PARTITIONS", "Legacy components may not exist in non-base partitions.");

        /// <summary>
        /// A component cannot be moved (or copied) from the System Application, an application proxy or a non-changeable application
        /// </summary>
        public static HRESULT COMADMIN_E_COMP_MOVE_SOURCE = new HRESULT("0x8011081C", "COMADMIN_E_COMP_MOVE_SOURCE", "A component cannot be moved (or copied) from the System Application, an application proxy or a non-changeable application");

        /// <summary>
        /// A component cannot be moved (or copied) to the System Application, an application proxy or a non-changeable application
        /// </summary>
        public static HRESULT COMADMIN_E_COMP_MOVE_DEST = new HRESULT("0x8011081D", "COMADMIN_E_COMP_MOVE_DEST", "A component cannot be moved (or copied) to the System Application, an application proxy or a non-changeable application");

        /// <summary>
        /// A private component cannot be moved (or copied) to a library application or to the base partition
        /// </summary>
        public static HRESULT COMADMIN_E_COMP_MOVE_PRIVATE = new HRESULT("0x8011081E", "COMADMIN_E_COMP_MOVE_PRIVATE", "A private component cannot be moved (or copied) to a library application or to the base partition");

        /// <summary>
        /// The Base Application Partition exists in all partition sets and cannot be removed.
        /// </summary>
        public static HRESULT COMADMIN_E_BASEPARTITION_REQUIRED_IN_SET = new HRESULT("0x8011081F", "COMADMIN_E_BASEPARTITION_REQUIRED_IN_SET", "The Base Application Partition exists in all partition sets and cannot be removed.");

        /// <summary>
        /// Event Class components cannot be aliased.
        /// </summary>
        public static HRESULT COMADMIN_E_CANNOT_ALIAS_EVENTCLASS = new HRESULT("0x80110820", "COMADMIN_E_CANNOT_ALIAS_EVENTCLASS", "Event Class components cannot be aliased.");

        /// <summary>
        /// Access is denied because the component is private.
        /// </summary>
        public static HRESULT COMADMIN_E_PRIVATE_ACCESSDENIED = new HRESULT("0x80110821", "COMADMIN_E_PRIVATE_ACCESSDENIED", "Access is denied because the component is private.");

        /// <summary>
        /// The specified SAFER level is invalid.
        /// </summary>
        public static HRESULT COMADMIN_E_SAFERINVALID = new HRESULT("0x80110822", "COMADMIN_E_SAFERINVALID", "The specified SAFER level is invalid.");

        /// <summary>
        /// The specified user cannot write to the system registry
        /// </summary>
        public static HRESULT COMADMIN_E_REGISTRY_ACCESSDENIED = new HRESULT("0x80110823", "COMADMIN_E_REGISTRY_ACCESSDENIED", "The specified user cannot write to the system registry");

        /// <summary>
        /// COM+ partitions are currently disabled.
        /// </summary>
        public static HRESULT COMADMIN_E_PARTITIONS_DISABLED = new HRESULT("0x80110824", "COMADMIN_E_PARTITIONS_DISABLED", "COM+ partitions are currently disabled.");

        /// <summary>
        /// The IO was completed by a filter.
        /// </summary>
        public static HRESULT ERROR_FLT_IO_COMPLETE = new HRESULT("0x001F0001", "ERROR_FLT_IO_COMPLETE", "The IO was completed by a filter.");

        /// <summary>
        /// A handler was not defined by the filter for this operation.
        /// </summary>
        public static HRESULT ERROR_FLT_NO_HANDLER_DEFINED = new HRESULT("0x801F0001", "ERROR_FLT_NO_HANDLER_DEFINED", "A handler was not defined by the filter for this operation.");

        /// <summary>
        /// A context is already defined for this object.
        /// </summary>
        public static HRESULT ERROR_FLT_CONTEXT_ALREADY_DEFINED = new HRESULT("0x801F0002", "ERROR_FLT_CONTEXT_ALREADY_DEFINED", "A context is already defined for this object.");

        /// <summary>
        /// Asynchronous requests are not valid for this operation.
        /// </summary>
        public static HRESULT ERROR_FLT_INVALID_ASYNCHRONOUS_REQUEST = new HRESULT("0x801F0003", "ERROR_FLT_INVALID_ASYNCHRONOUS_REQUEST", "Asynchronous requests are not valid for this operation.");

        /// <summary>
        /// Disallow the Fast IO path for this operation.
        /// </summary>
        public static HRESULT ERROR_FLT_DISALLOW_FAST_IO = new HRESULT("0x801F0004", "ERROR_FLT_DISALLOW_FAST_IO", "Disallow the Fast IO path for this operation.");

        /// <summary>
        /// An invalid name request was made. The name requested cannot be retrieved at this time.
        /// </summary>
        public static HRESULT ERROR_FLT_INVALID_NAME_REQUEST = new HRESULT("0x801F0005", "ERROR_FLT_INVALID_NAME_REQUEST", "An invalid name request was made. The name requested cannot be retrieved at this time.");

        /// <summary>
        /// Posting this operation to a worker thread for further processing is not safe at this time because it could lead to a system deadlock.
        /// </summary>
        public static HRESULT ERROR_FLT_NOT_SAFE_TO_POST_OPERATION = new HRESULT("0x801F0006", "ERROR_FLT_NOT_SAFE_TO_POST_OPERATION", "Posting this operation to a worker thread for further processing is not safe at this time because it could lead to a system deadlock.");

        /// <summary>
        /// The Filter Manager was not initialized when a filter tried to register. Make sure that the Filter Manager is getting loaded as a driver.
        /// </summary>
        public static HRESULT ERROR_FLT_NOT_INITIALIZED = new HRESULT("0x801F0007", "ERROR_FLT_NOT_INITIALIZED", "The Filter Manager was not initialized when a filter tried to register. Make sure that the Filter Manager is getting loaded as a driver.");

        /// <summary>
        /// The filter is not ready for attachment to volumes because it has not finished initializing (FltStartFiltering has not been called).
        /// </summary>
        public static HRESULT ERROR_FLT_FILTER_NOT_READY = new HRESULT("0x801F0008", "ERROR_FLT_FILTER_NOT_READY", "The filter is not ready for attachment to volumes because it has not finished initializing (FltStartFiltering has not been called).");

        /// <summary>
        /// The filter must cleanup any operation specific context at this time because it is being removed from the system before the operation is completed by the lower drivers.
        /// </summary>
        public static HRESULT ERROR_FLT_POST_OPERATION_CLEANUP = new HRESULT("0x801F0009", "ERROR_FLT_POST_OPERATION_CLEANUP", "The filter must cleanup any operation specific context at this time because it is being removed from the system before the operation is completed by the lower drivers.");

        /// <summary>
        /// The Filter Manager had an internal error from which it cannot recover, therefore the operation has been failed. This is usually the result of a filter returning an invalid value from a pre-operation callback.
        /// </summary>
        public static HRESULT ERROR_FLT_INTERNAL_ERROR = new HRESULT("0x801F000A", "ERROR_FLT_INTERNAL_ERROR", "The Filter Manager had an internal error from which it cannot recover, therefore the operation has been failed. This is usually the result of a filter returning an invalid value from a pre-operation callback.");

        /// <summary>
        /// The object specified for this action is in the process of being deleted, therefore the action requested cannot be completed at this time.
        /// </summary>
        public static HRESULT ERROR_FLT_DELETING_OBJECT = new HRESULT("0x801F000B", "ERROR_FLT_DELETING_OBJECT", "The object specified for this action is in the process of being deleted, therefore the action requested cannot be completed at this time.");

        /// <summary>
        /// Non-paged pool must be used for this type of context.
        /// </summary>
        public static HRESULT ERROR_FLT_MUST_BE_NONPAGED_POOL = new HRESULT("0x801F000C", "ERROR_FLT_MUST_BE_NONPAGED_POOL", "Non-paged pool must be used for this type of context.");

        /// <summary>
        /// A duplicate handler definition has been provided for an operation.
        /// </summary>
        public static HRESULT ERROR_FLT_DUPLICATE_ENTRY = new HRESULT("0x801F000D", "ERROR_FLT_DUPLICATE_ENTRY", "A duplicate handler definition has been provided for an operation.");

        /// <summary>
        /// The callback data queue has been disabled.
        /// </summary>
        public static HRESULT ERROR_FLT_CBDQ_DISABLED = new HRESULT("0x801F000E", "ERROR_FLT_CBDQ_DISABLED", "The callback data queue has been disabled.");

        /// <summary>
        /// Do not attach the filter to the volume at this time.
        /// </summary>
        public static HRESULT ERROR_FLT_DO_NOT_ATTACH = new HRESULT("0x801F000F", "ERROR_FLT_DO_NOT_ATTACH", "Do not attach the filter to the volume at this time.");

        /// <summary>
        /// Do not detach the filter from the volume at this time.
        /// </summary>
        public static HRESULT ERROR_FLT_DO_NOT_DETACH = new HRESULT("0x801F0010", "ERROR_FLT_DO_NOT_DETACH", "Do not detach the filter from the volume at this time.");

        /// <summary>
        /// An instance already exists at this altitude on the volume specified.
        /// </summary>
        public static HRESULT ERROR_FLT_INSTANCE_ALTITUDE_COLLISION = new HRESULT("0x801F0011", "ERROR_FLT_INSTANCE_ALTITUDE_COLLISION", "An instance already exists at this altitude on the volume specified.");

        /// <summary>
        /// An instance already exists with this name on the volume specified.
        /// </summary>
        public static HRESULT ERROR_FLT_INSTANCE_NAME_COLLISION = new HRESULT("0x801F0012", "ERROR_FLT_INSTANCE_NAME_COLLISION", "An instance already exists with this name on the volume specified.");

        /// <summary>
        /// The system could not find the filter specified.
        /// </summary>
        public static HRESULT ERROR_FLT_FILTER_NOT_FOUND = new HRESULT("0x801F0013", "ERROR_FLT_FILTER_NOT_FOUND", "The system could not find the filter specified.");

        /// <summary>
        /// The system could not find the volume specified.
        /// </summary>
        public static HRESULT ERROR_FLT_VOLUME_NOT_FOUND = new HRESULT("0x801F0014", "ERROR_FLT_VOLUME_NOT_FOUND", "The system could not find the volume specified.");

        /// <summary>
        /// The system could not find the instance specified.
        /// </summary>
        public static HRESULT ERROR_FLT_INSTANCE_NOT_FOUND = new HRESULT("0x801F0015", "ERROR_FLT_INSTANCE_NOT_FOUND", "The system could not find the instance specified.");

        /// <summary>
        /// No registered context allocation definition was found for the given request.
        /// </summary>
        public static HRESULT ERROR_FLT_CONTEXT_ALLOCATION_NOT_FOUND = new HRESULT("0x801F0016", "ERROR_FLT_CONTEXT_ALLOCATION_NOT_FOUND", "No registered context allocation definition was found for the given request.");

        /// <summary>
        /// An invalid parameter was specified during context registration.
        /// </summary>
        public static HRESULT ERROR_FLT_INVALID_CONTEXT_REGISTRATION = new HRESULT("0x801F0017", "ERROR_FLT_INVALID_CONTEXT_REGISTRATION", "An invalid parameter was specified during context registration.");

        /// <summary>
        /// The name requested was not found in Filter Manager's name cache and could not be retrieved from the file system.
        /// </summary>
        public static HRESULT ERROR_FLT_NAME_CACHE_MISS = new HRESULT("0x801F0018", "ERROR_FLT_NAME_CACHE_MISS", "The name requested was not found in Filter Manager's name cache and could not be retrieved from the file system.");

        /// <summary>
        /// The requested device object does not exist for the given volume.
        /// </summary>
        public static HRESULT ERROR_FLT_NO_DEVICE_OBJECT = new HRESULT("0x801F0019", "ERROR_FLT_NO_DEVICE_OBJECT", "The requested device object does not exist for the given volume.");

        /// <summary>
        /// The specified volume is already mounted.
        /// </summary>
        public static HRESULT ERROR_FLT_VOLUME_ALREADY_MOUNTED = new HRESULT("0x801F001A", "ERROR_FLT_VOLUME_ALREADY_MOUNTED", "The specified volume is already mounted.");

        /// <summary>
        /// The specified Transaction Context is already enlisted in a transaction
        /// </summary>
        public static HRESULT ERROR_FLT_ALREADY_ENLISTED = new HRESULT("0x801F001B", "ERROR_FLT_ALREADY_ENLISTED", "The specified Transaction Context is already enlisted in a transaction");

        /// <summary>
        /// The specified context is already attached to another object
        /// </summary>
        public static HRESULT ERROR_FLT_CONTEXT_ALREADY_LINKED = new HRESULT("0x801F001C", "ERROR_FLT_CONTEXT_ALREADY_LINKED", "The specified context is already attached to another object");

        /// <summary>
        /// No waiter is present for the filter's reply to this message.
        /// </summary>
        public static HRESULT ERROR_FLT_NO_WAITER_FOR_REPLY = new HRESULT("0x801F0020", "ERROR_FLT_NO_WAITER_FOR_REPLY", "No waiter is present for the filter's reply to this message.");

        /// <summary>
        /// The filesystem database resource is in use. Registration cannot complete at this time.
        /// </summary>
        public static HRESULT ERROR_FLT_REGISTRATION_BUSY = new HRESULT("0x801F0023", "ERROR_FLT_REGISTRATION_BUSY", "The filesystem database resource is in use. Registration cannot complete at this time.");

        /// <summary>
        /// Display Driver Stopped Responding} The %hs display driver has stopped working normally. Save your work and reboot the system to restore full display functionality. The next time you reboot the machine a dialog will be displayed giving you a chance to report this failure to Microsoft.
        /// </summary>
        public static HRESULT ERROR_HUNG_DISPLAY_DRIVER_THREAD = new HRESULT("0x80260001", "ERROR_HUNG_DISPLAY_DRIVER_THREAD", "Display Driver Stopped Responding} The %hs display driver has stopped working normally. Save your work and reboot the system to restore full display functionality. The next time you reboot the machine a dialog will be displayed giving you a chance to report this failure to Microsoft.");

        /// <summary>
        /// Desktop composition is disabled} The operation could not be completed because desktop composition is disabled.
        /// </summary>
        public static HRESULT DWM_E_COMPOSITIONDISABLED = new HRESULT("0x80263001", "DWM_E_COMPOSITIONDISABLED", "Desktop composition is disabled} The operation could not be completed because desktop composition is disabled.");

        /// <summary>
        /// Some desktop composition APIs are not supported while remoting} The operation is not supported while running in a remote session.
        /// </summary>
        public static HRESULT DWM_E_REMOTING_NOT_SUPPORTED = new HRESULT("0x80263002", "DWM_E_REMOTING_NOT_SUPPORTED", "Some desktop composition APIs are not supported while remoting} The operation is not supported while running in a remote session.");

        /// <summary>
        /// No DWM redirection surface is available} The DWM was unable to provide a redireciton surface to complete the DirectX present.
        /// </summary>
        public static HRESULT DWM_E_NO_REDIRECTION_SURFACE_AVAILABLE = new HRESULT("0x80263003", "DWM_E_NO_REDIRECTION_SURFACE_AVAILABLE", "No DWM redirection surface is available} The DWM was unable to provide a redireciton surface to complete the DirectX present.");

        /// <summary>
        /// DWM is not queuing presents for the specified window} The window specified is not currently using queued presents.
        /// </summary>
        public static HRESULT DWM_E_NOT_QUEUING_PRESENTS = new HRESULT("0x80263004", "DWM_E_NOT_QUEUING_PRESENTS", "DWM is not queuing presents for the specified window} The window specified is not currently using queued presents.");

        /// <summary>
        /// The adapter specified by the LUID is not found} DWM cannot find the adapter specified by the LUID.
        /// </summary>
        public static HRESULT DWM_E_ADAPTER_NOT_FOUND = new HRESULT("0x80263005", "DWM_E_ADAPTER_NOT_FOUND", "The adapter specified by the LUID is not found} DWM cannot find the adapter specified by the LUID.");

        /// <summary>
        /// GDI redirection surface was returned} GDI redirection surface of the top level window was returned.
        /// </summary>
        public static HRESULT DWM_S_GDI_REDIRECTION_SURFACE = new HRESULT("0x00263005", "DWM_S_GDI_REDIRECTION_SURFACE", "GDI redirection surface was returned} GDI redirection surface of the top level window was returned.");

        /// <summary>
        /// Redirection surface can not be created. The size of the surface is larger than what is supported on this machine} Redirection surface can not be created. The size of the surface is larger than what is supported on this machine.
        /// </summary>
        public static HRESULT DWM_E_TEXTURE_TOO_LARGE = new HRESULT("0x80263007", "DWM_E_TEXTURE_TOO_LARGE", "Redirection surface can not be created. The size of the surface is larger than what is supported on this machine} Redirection surface can not be created. The size of the surface is larger than what is supported on this machine.");

        /// <summary>
        /// Monitor descriptor could not be obtained.
        /// </summary>
        public static HRESULT ERROR_MONITOR_NO_DESCRIPTOR = new HRESULT("0x80261001", "ERROR_MONITOR_NO_DESCRIPTOR", "Monitor descriptor could not be obtained.");

        /// <summary>
        /// Format of the obtained monitor descriptor is not supported by this release.
        /// </summary>
        public static HRESULT ERROR_MONITOR_UNKNOWN_DESCRIPTOR_FORMAT = new HRESULT("0x80261002", "ERROR_MONITOR_UNKNOWN_DESCRIPTOR_FORMAT", "Format of the obtained monitor descriptor is not supported by this release.");

        /// <summary>
        /// Checksum of the obtained monitor descriptor is invalid.
        /// </summary>
        public static HRESULT ERROR_MONITOR_INVALID_DESCRIPTOR_CHECKSUM = new HRESULT("0xC0261003", "ERROR_MONITOR_INVALID_DESCRIPTOR_CHECKSUM", "Checksum of the obtained monitor descriptor is invalid.");

        /// <summary>
        /// Monitor descriptor contains an invalid standard timing block.
        /// </summary>
        public static HRESULT ERROR_MONITOR_INVALID_STANDARD_TIMING_BLOCK = new HRESULT("0xC0261004", "ERROR_MONITOR_INVALID_STANDARD_TIMING_BLOCK", "Monitor descriptor contains an invalid standard timing block.");

        /// <summary>
        /// WMI data block registration failed for one of the MSMonitorClass WMI subclasses.
        /// </summary>
        public static HRESULT ERROR_MONITOR_WMI_DATABLOCK_REGISTRATION_FAILED = new HRESULT("0xC0261005", "ERROR_MONITOR_WMI_DATABLOCK_REGISTRATION_FAILED", "WMI data block registration failed for one of the MSMonitorClass WMI subclasses.");

        /// <summary>
        /// Provided monitor descriptor block is either corrupted or does not contain monitor's detailed serial number.
        /// </summary>
        public static HRESULT ERROR_MONITOR_INVALID_SERIAL_NUMBER_MONDSC_BLOCK = new HRESULT("0xC0261006", "ERROR_MONITOR_INVALID_SERIAL_NUMBER_MONDSC_BLOCK", "Provided monitor descriptor block is either corrupted or does not contain monitor's detailed serial number.");

        /// <summary>
        /// Provided monitor descriptor block is either corrupted or does not contain monitor's user friendly name.
        /// </summary>
        public static HRESULT ERROR_MONITOR_INVALID_USER_FRIENDLY_MONDSC_BLOCK = new HRESULT("0xC0261007", "ERROR_MONITOR_INVALID_USER_FRIENDLY_MONDSC_BLOCK", "Provided monitor descriptor block is either corrupted or does not contain monitor's user friendly name.");

        /// <summary>
        /// There is no monitor descriptor data at the specified (offset, size) region.
        /// </summary>
        public static HRESULT ERROR_MONITOR_NO_MORE_DESCRIPTOR_DATA = new HRESULT("0xC0261008", "ERROR_MONITOR_NO_MORE_DESCRIPTOR_DATA", "There is no monitor descriptor data at the specified (offset, size) region.");

        /// <summary>
        /// Monitor descriptor contains an invalid detailed timing block.
        /// </summary>
        public static HRESULT ERROR_MONITOR_INVALID_DETAILED_TIMING_BLOCK = new HRESULT("0xC0261009", "ERROR_MONITOR_INVALID_DETAILED_TIMING_BLOCK", "Monitor descriptor contains an invalid detailed timing block.");

        /// <summary>
        /// Monitor descriptor contains invalid manufacture date.
        /// </summary>
        public static HRESULT ERROR_MONITOR_INVALID_MANUFACTURE_DATE = new HRESULT("0xC026100A", "ERROR_MONITOR_INVALID_MANUFACTURE_DATE", "Monitor descriptor contains invalid manufacture date.");

        /// <summary>
        /// Exclusive mode ownership is needed to create unmanaged primary allocation.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NOT_EXCLUSIVE_MODE_OWNER = new HRESULT("0xC0262000", "ERROR_GRAPHICS_NOT_EXCLUSIVE_MODE_OWNER", "Exclusive mode ownership is needed to create unmanaged primary allocation.");

        /// <summary>
        /// The driver needs more DMA buffer space in order to complete the requested operation.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INSUFFICIENT_DMA_BUFFER = new HRESULT("0xC0262001", "ERROR_GRAPHICS_INSUFFICIENT_DMA_BUFFER", "The driver needs more DMA buffer space in order to complete the requested operation.");

        /// <summary>
        /// Specified display adapter handle is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_DISPLAY_ADAPTER = new HRESULT("0xC0262002", "ERROR_GRAPHICS_INVALID_DISPLAY_ADAPTER", "Specified display adapter handle is invalid.");

        /// <summary>
        /// Specified display adapter and all of its state has been reset.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ADAPTER_WAS_RESET = new HRESULT("0xC0262003", "ERROR_GRAPHICS_ADAPTER_WAS_RESET", "Specified display adapter and all of its state has been reset.");

        /// <summary>
        /// The driver stack doesn't match the expected driver model.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_DRIVER_MODEL = new HRESULT("0xC0262004", "ERROR_GRAPHICS_INVALID_DRIVER_MODEL", "The driver stack doesn't match the expected driver model.");

        /// <summary>
        /// Present happened but ended up in the changed desktop
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PRESENT_MODE_CHANGED = new HRESULT("0xC0262005", "ERROR_GRAPHICS_PRESENT_MODE_CHANGED", "Present happened but ended up in the changed desktop");

        /// <summary>
        /// Nothing to present due to desktop occlusion
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PRESENT_OCCLUDED = new HRESULT("0xC0262006", "ERROR_GRAPHICS_PRESENT_OCCLUDED", "Nothing to present due to desktop occlusion");

        /// <summary>
        /// Not able to present due to denial of desktop access
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PRESENT_DENIED = new HRESULT("0xC0262007", "ERROR_GRAPHICS_PRESENT_DENIED", "Not able to present due to denial of desktop access");

        /// <summary>
        /// Not able to present with color convertion
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CANNOTCOLORCONVERT = new HRESULT("0xC0262008", "ERROR_GRAPHICS_CANNOTCOLORCONVERT", "Not able to present with color convertion");

        /// <summary>
        /// The kernel driver detected a version mismatch between it and the user mode driver.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DRIVER_MISMATCH = new HRESULT("0xC0262009", "ERROR_GRAPHICS_DRIVER_MISMATCH", "The kernel driver detected a version mismatch between it and the user mode driver.");

        /// <summary>
        /// Specified buffer is not big enough to contain entire requested dataset. Partial data populated up to the size of the buffer. Caller needs to provide buffer of size as specified in the partially populated buffer's content (interface specific).
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PARTIAL_DATA_POPULATED = new HRESULT("0x4026200A", "ERROR_GRAPHICS_PARTIAL_DATA_POPULATED", "Specified buffer is not big enough to contain entire requested dataset. Partial data populated up to the size of the buffer. Caller needs to provide buffer of size as specified in the partially populated buffer's content (interface specific).");

        /// <summary>
        /// Present redirection is disabled (desktop windowing management subsystem is off).
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PRESENT_REDIRECTION_DISABLED = new HRESULT("0xC026200B", "ERROR_GRAPHICS_PRESENT_REDIRECTION_DISABLED", "Present redirection is disabled (desktop windowing management subsystem is off).");

        /// <summary>
        /// Previous exclusive VidPn source owner has released its ownership
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PRESENT_UNOCCLUDED = new HRESULT("0xC026200C", "ERROR_GRAPHICS_PRESENT_UNOCCLUDED", "Previous exclusive VidPn source owner has released its ownership");

        /// <summary>
        /// Window DC is not available for presentation
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_WINDOWDC_NOT_AVAILABLE = new HRESULT("0xC026200D", "ERROR_GRAPHICS_WINDOWDC_NOT_AVAILABLE", "Window DC is not available for presentation");

        /// <summary>
        /// Not enough video memory available to complete the operation.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_VIDEO_MEMORY = new HRESULT("0xC0262100", "ERROR_GRAPHICS_NO_VIDEO_MEMORY", "Not enough video memory available to complete the operation.");

        /// <summary>
        /// Couldn't probe and lock the underlying memory of an allocation.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CANT_LOCK_MEMORY = new HRESULT("0xC0262101", "ERROR_GRAPHICS_CANT_LOCK_MEMORY", "Couldn't probe and lock the underlying memory of an allocation.");

        /// <summary>
        /// The allocation is currently busy.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ALLOCATION_BUSY = new HRESULT("0xC0262102", "ERROR_GRAPHICS_ALLOCATION_BUSY", "The allocation is currently busy.");

        /// <summary>
        /// An object being referenced has reach the maximum reference count already and can't be reference further.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_TOO_MANY_REFERENCES = new HRESULT("0xC0262103", "ERROR_GRAPHICS_TOO_MANY_REFERENCES", "An object being referenced has reach the maximum reference count already and can't be reference further.");

        /// <summary>
        /// A problem couldn't be solved due to some currently existing condition. The problem should be tried again later.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_TRY_AGAIN_LATER = new HRESULT("0xC0262104", "ERROR_GRAPHICS_TRY_AGAIN_LATER", "A problem couldn't be solved due to some currently existing condition. The problem should be tried again later.");

        /// <summary>
        /// A problem couldn't be solved due to some currently existing condition. The problem should be tried again immediately.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_TRY_AGAIN_NOW = new HRESULT("0xC0262105", "ERROR_GRAPHICS_TRY_AGAIN_NOW", "A problem couldn't be solved due to some currently existing condition. The problem should be tried again immediately.");

        /// <summary>
        /// The allocation is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ALLOCATION_INVALID = new HRESULT("0xC0262106", "ERROR_GRAPHICS_ALLOCATION_INVALID", "The allocation is invalid.");

        /// <summary>
        /// No more unswizzling aperture are currently available.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_UNSWIZZLING_APERTURE_UNAVAILABLE = new HRESULT("0xC0262107", "ERROR_GRAPHICS_UNSWIZZLING_APERTURE_UNAVAILABLE", "No more unswizzling aperture are currently available.");

        /// <summary>
        /// The current allocation can't be unswizzled by an aperture.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_UNSWIZZLING_APERTURE_UNSUPPORTED = new HRESULT("0xC0262108", "ERROR_GRAPHICS_UNSWIZZLING_APERTURE_UNSUPPORTED", "The current allocation can't be unswizzled by an aperture.");

        /// <summary>
        /// The request failed because a pinned allocation can't be evicted.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CANT_EVICT_PINNED_ALLOCATION = new HRESULT("0xC0262109", "ERROR_GRAPHICS_CANT_EVICT_PINNED_ALLOCATION", "The request failed because a pinned allocation can't be evicted.");

        /// <summary>
        /// The allocation can't be used from its current segment location for the specified operation.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_ALLOCATION_USAGE = new HRESULT("0xC0262110", "ERROR_GRAPHICS_INVALID_ALLOCATION_USAGE", "The allocation can't be used from its current segment location for the specified operation.");

        /// <summary>
        /// A locked allocation can't be used in the current command buffer.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CANT_RENDER_LOCKED_ALLOCATION = new HRESULT("0xC0262111", "ERROR_GRAPHICS_CANT_RENDER_LOCKED_ALLOCATION", "A locked allocation can't be used in the current command buffer.");

        /// <summary>
        /// The allocation being referenced has been closed permanently.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ALLOCATION_CLOSED = new HRESULT("0xC0262112", "ERROR_GRAPHICS_ALLOCATION_CLOSED", "The allocation being referenced has been closed permanently.");

        /// <summary>
        /// An invalid allocation instance is being referenced.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_ALLOCATION_INSTANCE = new HRESULT("0xC0262113", "ERROR_GRAPHICS_INVALID_ALLOCATION_INSTANCE", "An invalid allocation instance is being referenced.");

        /// <summary>
        /// An invalid allocation handle is being referenced.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_ALLOCATION_HANDLE = new HRESULT("0xC0262114", "ERROR_GRAPHICS_INVALID_ALLOCATION_HANDLE", "An invalid allocation handle is being referenced.");

        /// <summary>
        /// The allocation being referenced doesn't belong to the current device.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_WRONG_ALLOCATION_DEVICE = new HRESULT("0xC0262115", "ERROR_GRAPHICS_WRONG_ALLOCATION_DEVICE", "The allocation being referenced doesn't belong to the current device.");

        /// <summary>
        /// The specified allocation lost its content.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ALLOCATION_CONTENT_LOST = new HRESULT("0xC0262116", "ERROR_GRAPHICS_ALLOCATION_CONTENT_LOST", "The specified allocation lost its content.");

        /// <summary>
        /// GPU exception is detected on the given device. The device is not able to be scheduled.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_GPU_EXCEPTION_ON_DEVICE = new HRESULT("0xC0262200", "ERROR_GRAPHICS_GPU_EXCEPTION_ON_DEVICE", "GPU exception is detected on the given device. The device is not able to be scheduled.");

        /// <summary>
        /// Skip preparation of allocations referenced by the DMA buffer.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_SKIP_ALLOCATION_PREPARATION = new HRESULT("0x40262201", "ERROR_GRAPHICS_SKIP_ALLOCATION_PREPARATION", "Skip preparation of allocations referenced by the DMA buffer.");

        /// <summary>
        /// Specified VidPN topology is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDPN_TOPOLOGY = new HRESULT("0xC0262300", "ERROR_GRAPHICS_INVALID_VIDPN_TOPOLOGY", "Specified VidPN topology is invalid.");

        /// <summary>
        /// Specified VidPN topology is valid but is not supported by this model of the display adapter.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_VIDPN_TOPOLOGY_NOT_SUPPORTED = new HRESULT("0xC0262301", "ERROR_GRAPHICS_VIDPN_TOPOLOGY_NOT_SUPPORTED", "Specified VidPN topology is valid but is not supported by this model of the display adapter.");

        /// <summary>
        /// Specified VidPN topology is valid but is not supported by the display adapter at this time, due to current allocation of its resources.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_VIDPN_TOPOLOGY_CURRENTLY_NOT_SUPPORTED = new HRESULT("0xC0262302", "ERROR_GRAPHICS_VIDPN_TOPOLOGY_CURRENTLY_NOT_SUPPORTED", "Specified VidPN topology is valid but is not supported by the display adapter at this time, due to current allocation of its resources.");

        /// <summary>
        /// Specified VidPN handle is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDPN = new HRESULT("0xC0262303", "ERROR_GRAPHICS_INVALID_VIDPN", "Specified VidPN handle is invalid.");

        /// <summary>
        /// Specified video present source is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE = new HRESULT("0xC0262304", "ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE", "Specified video present source is invalid.");

        /// <summary>
        /// Specified video present target is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET = new HRESULT("0xC0262305", "ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET", "Specified video present target is invalid.");

        /// <summary>
        /// Specified VidPN modality is not supported (e.g. at least two of the pinned modes are not cofunctional).
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_VIDPN_MODALITY_NOT_SUPPORTED = new HRESULT("0xC0262306", "ERROR_GRAPHICS_VIDPN_MODALITY_NOT_SUPPORTED", "Specified VidPN modality is not supported (e.g. at least two of the pinned modes are not cofunctional).");

        /// <summary>
        /// No mode is pinned on the specified VidPN source/target.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MODE_NOT_PINNED = new HRESULT("0x00262307", "ERROR_GRAPHICS_MODE_NOT_PINNED", "No mode is pinned on the specified VidPN source/target.");

        /// <summary>
        /// Specified VidPN source mode set is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDPN_SOURCEMODESET = new HRESULT("0xC0262308", "ERROR_GRAPHICS_INVALID_VIDPN_SOURCEMODESET", "Specified VidPN source mode set is invalid.");

        /// <summary>
        /// Specified VidPN target mode set is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDPN_TARGETMODESET = new HRESULT("0xC0262309", "ERROR_GRAPHICS_INVALID_VIDPN_TARGETMODESET", "Specified VidPN target mode set is invalid.");

        /// <summary>
        /// Specified video signal frequency is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_FREQUENCY = new HRESULT("0xC026230A", "ERROR_GRAPHICS_INVALID_FREQUENCY", "Specified video signal frequency is invalid.");

        /// <summary>
        /// Specified video signal active region is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_ACTIVE_REGION = new HRESULT("0xC026230B", "ERROR_GRAPHICS_INVALID_ACTIVE_REGION", "Specified video signal active region is invalid.");

        /// <summary>
        /// Specified video signal total region is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_TOTAL_REGION = new HRESULT("0xC026230C", "ERROR_GRAPHICS_INVALID_TOTAL_REGION", "Specified video signal total region is invalid.");

        /// <summary>
        /// Specified video present source mode is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE_MODE = new HRESULT("0xC0262310", "ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE_MODE", "Specified video present source mode is invalid.");

        /// <summary>
        /// Specified video present target mode is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET_MODE = new HRESULT("0xC0262311", "ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET_MODE", "Specified video present target mode is invalid.");

        /// <summary>
        /// Pinned mode must remain in the set on VidPN's cofunctional modality enumeration.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PINNED_MODE_MUST_REMAIN_IN_SET = new HRESULT("0xC0262312", "ERROR_GRAPHICS_PINNED_MODE_MUST_REMAIN_IN_SET", "Pinned mode must remain in the set on VidPN's cofunctional modality enumeration.");

        /// <summary>
        /// Specified video present path is already in VidPN's topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PATH_ALREADY_IN_TOPOLOGY = new HRESULT("0xC0262313", "ERROR_GRAPHICS_PATH_ALREADY_IN_TOPOLOGY", "Specified video present path is already in VidPN's topology.");

        /// <summary>
        /// Specified mode is already in the mode set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MODE_ALREADY_IN_MODESET = new HRESULT("0xC0262314", "ERROR_GRAPHICS_MODE_ALREADY_IN_MODESET", "Specified mode is already in the mode set.");

        /// <summary>
        /// Specified video present source set is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDEOPRESENTSOURCESET = new HRESULT("0xC0262315", "ERROR_GRAPHICS_INVALID_VIDEOPRESENTSOURCESET", "Specified video present source set is invalid.");

        /// <summary>
        /// Specified video present target set is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDEOPRESENTTARGETSET = new HRESULT("0xC0262316", "ERROR_GRAPHICS_INVALID_VIDEOPRESENTTARGETSET", "Specified video present target set is invalid.");

        /// <summary>
        /// Specified video present source is already in the video present source set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_SOURCE_ALREADY_IN_SET = new HRESULT("0xC0262317", "ERROR_GRAPHICS_SOURCE_ALREADY_IN_SET", "Specified video present source is already in the video present source set.");

        /// <summary>
        /// Specified video present target is already in the video present target set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_TARGET_ALREADY_IN_SET = new HRESULT("0xC0262318", "ERROR_GRAPHICS_TARGET_ALREADY_IN_SET", "Specified video present target is already in the video present target set.");

        /// <summary>
        /// Specified VidPN present path is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDPN_PRESENT_PATH = new HRESULT("0xC0262319", "ERROR_GRAPHICS_INVALID_VIDPN_PRESENT_PATH", "Specified VidPN present path is invalid.");

        /// <summary>
        /// Miniport has no recommendation for augmentation of the specified VidPN's topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_RECOMMENDED_VIDPN_TOPOLOGY = new HRESULT("0xC026231A", "ERROR_GRAPHICS_NO_RECOMMENDED_VIDPN_TOPOLOGY", "Miniport has no recommendation for augmentation of the specified VidPN's topology.");

        /// <summary>
        /// Specified monitor frequency range set is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGESET = new HRESULT("0xC026231B", "ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGESET", "Specified monitor frequency range set is invalid.");

        /// <summary>
        /// Specified monitor frequency range is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGE = new HRESULT("0xC026231C", "ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGE", "Specified monitor frequency range is invalid.");

        /// <summary>
        /// Specified frequency range is not in the specified monitor frequency range set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_FREQUENCYRANGE_NOT_IN_SET = new HRESULT("0xC026231D", "ERROR_GRAPHICS_FREQUENCYRANGE_NOT_IN_SET", "Specified frequency range is not in the specified monitor frequency range set.");

        /// <summary>
        /// Specified mode set does not specify preference for one of its modes.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_PREFERRED_MODE = new HRESULT("0x0026231E", "ERROR_GRAPHICS_NO_PREFERRED_MODE", "Specified mode set does not specify preference for one of its modes.");

        /// <summary>
        /// Specified frequency range is already in the specified monitor frequency range set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_FREQUENCYRANGE_ALREADY_IN_SET = new HRESULT("0xC026231F", "ERROR_GRAPHICS_FREQUENCYRANGE_ALREADY_IN_SET", "Specified frequency range is already in the specified monitor frequency range set.");

        /// <summary>
        /// Specified mode set is stale. Please reacquire the new mode set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_STALE_MODESET = new HRESULT("0xC0262320", "ERROR_GRAPHICS_STALE_MODESET", "Specified mode set is stale. Please reacquire the new mode set.");

        /// <summary>
        /// Specified monitor source mode set is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITOR_SOURCEMODESET = new HRESULT("0xC0262321", "ERROR_GRAPHICS_INVALID_MONITOR_SOURCEMODESET", "Specified monitor source mode set is invalid.");

        /// <summary>
        /// Specified monitor source mode is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITOR_SOURCE_MODE = new HRESULT("0xC0262322", "ERROR_GRAPHICS_INVALID_MONITOR_SOURCE_MODE", "Specified monitor source mode is invalid.");

        /// <summary>
        /// Miniport does not have any recommendation regarding the request to provide a functional VidPN given the current display adapter configuration.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_RECOMMENDED_FUNCTIONAL_VIDPN = new HRESULT("0xC0262323", "ERROR_GRAPHICS_NO_RECOMMENDED_FUNCTIONAL_VIDPN", "Miniport does not have any recommendation regarding the request to provide a functional VidPN given the current display adapter configuration.");

        /// <summary>
        /// ID of the specified mode is already used by another mode in the set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MODE_ID_MUST_BE_UNIQUE = new HRESULT("0xC0262324", "ERROR_GRAPHICS_MODE_ID_MUST_BE_UNIQUE", "ID of the specified mode is already used by another mode in the set.");

        /// <summary>
        /// System failed to determine a mode that is supported by both the display adapter and the monitor connected to it.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_EMPTY_ADAPTER_MONITOR_MODE_SUPPORT_INTERSECTION = new HRESULT("0xC0262325", "ERROR_GRAPHICS_EMPTY_ADAPTER_MONITOR_MODE_SUPPORT_INTERSECTION", "System failed to determine a mode that is supported by both the display adapter and the monitor connected to it.");

        /// <summary>
        /// Number of video present targets must be greater than or equal to the number of video present sources.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_VIDEO_PRESENT_TARGETS_LESS_THAN_SOURCES = new HRESULT("0xC0262326", "ERROR_GRAPHICS_VIDEO_PRESENT_TARGETS_LESS_THAN_SOURCES", "Number of video present targets must be greater than or equal to the number of video present sources.");

        /// <summary>
        /// Specified present path is not in VidPN's topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PATH_NOT_IN_TOPOLOGY = new HRESULT("0xC0262327", "ERROR_GRAPHICS_PATH_NOT_IN_TOPOLOGY", "Specified present path is not in VidPN's topology.");

        /// <summary>
        /// Display adapter must have at least one video present source.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_SOURCE = new HRESULT("0xC0262328", "ERROR_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_SOURCE", "Display adapter must have at least one video present source.");

        /// <summary>
        /// Display adapter must have at least one video present target.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_TARGET = new HRESULT("0xC0262329", "ERROR_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_TARGET", "Display adapter must have at least one video present target.");

        /// <summary>
        /// Specified monitor descriptor set is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITORDESCRIPTORSET = new HRESULT("0xC026232A", "ERROR_GRAPHICS_INVALID_MONITORDESCRIPTORSET", "Specified monitor descriptor set is invalid.");

        /// <summary>
        /// Specified monitor descriptor is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITORDESCRIPTOR = new HRESULT("0xC026232B", "ERROR_GRAPHICS_INVALID_MONITORDESCRIPTOR", "Specified monitor descriptor is invalid.");

        /// <summary>
        /// Specified descriptor is not in the specified monitor descriptor set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MONITORDESCRIPTOR_NOT_IN_SET = new HRESULT("0xC026232C", "ERROR_GRAPHICS_MONITORDESCRIPTOR_NOT_IN_SET", "Specified descriptor is not in the specified monitor descriptor set.");

        /// <summary>
        /// Specified descriptor is already in the specified monitor descriptor set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MONITORDESCRIPTOR_ALREADY_IN_SET = new HRESULT("0xC026232D", "ERROR_GRAPHICS_MONITORDESCRIPTOR_ALREADY_IN_SET", "Specified descriptor is already in the specified monitor descriptor set.");

        /// <summary>
        /// ID of the specified monitor descriptor is already used by another descriptor in the set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MONITORDESCRIPTOR_ID_MUST_BE_UNIQUE = new HRESULT("0xC026232E", "ERROR_GRAPHICS_MONITORDESCRIPTOR_ID_MUST_BE_UNIQUE", "ID of the specified monitor descriptor is already used by another descriptor in the set.");

        /// <summary>
        /// Specified video present target subset type is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDPN_TARGET_SUBSET_TYPE = new HRESULT("0xC026232F", "ERROR_GRAPHICS_INVALID_VIDPN_TARGET_SUBSET_TYPE", "Specified video present target subset type is invalid.");

        /// <summary>
        /// Two or more of the specified resources are not related to each other, as defined by the interface semantics.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_RESOURCES_NOT_RELATED = new HRESULT("0xC0262330", "ERROR_GRAPHICS_RESOURCES_NOT_RELATED", "Two or more of the specified resources are not related to each other, as defined by the interface semantics.");

        /// <summary>
        /// ID of the specified video present source is already used by another source in the set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_SOURCE_ID_MUST_BE_UNIQUE = new HRESULT("0xC0262331", "ERROR_GRAPHICS_SOURCE_ID_MUST_BE_UNIQUE", "ID of the specified video present source is already used by another source in the set.");

        /// <summary>
        /// ID of the specified video present target is already used by another target in the set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_TARGET_ID_MUST_BE_UNIQUE = new HRESULT("0xC0262332", "ERROR_GRAPHICS_TARGET_ID_MUST_BE_UNIQUE", "ID of the specified video present target is already used by another target in the set.");

        /// <summary>
        /// Specified VidPN source cannot be used because there is no available VidPN target to connect it to.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_AVAILABLE_VIDPN_TARGET = new HRESULT("0xC0262333", "ERROR_GRAPHICS_NO_AVAILABLE_VIDPN_TARGET", "Specified VidPN source cannot be used because there is no available VidPN target to connect it to.");

        /// <summary>
        /// Newly arrived monitor could not be associated with a display adapter.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MONITOR_COULD_NOT_BE_ASSOCIATED_WITH_ADAPTER = new HRESULT("0xC0262334", "ERROR_GRAPHICS_MONITOR_COULD_NOT_BE_ASSOCIATED_WITH_ADAPTER", "Newly arrived monitor could not be associated with a display adapter.");

        /// <summary>
        /// Display adapter in question does not have an associated VidPN manager.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_VIDPNMGR = new HRESULT("0xC0262335", "ERROR_GRAPHICS_NO_VIDPNMGR", "Display adapter in question does not have an associated VidPN manager.");

        /// <summary>
        /// VidPN manager of the display adapter in question does not have an active VidPN.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_ACTIVE_VIDPN = new HRESULT("0xC0262336", "ERROR_GRAPHICS_NO_ACTIVE_VIDPN", "VidPN manager of the display adapter in question does not have an active VidPN.");

        /// <summary>
        /// Specified VidPN topology is stale. Please reacquire the new topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_STALE_VIDPN_TOPOLOGY = new HRESULT("0xC0262337", "ERROR_GRAPHICS_STALE_VIDPN_TOPOLOGY", "Specified VidPN topology is stale. Please reacquire the new topology.");

        /// <summary>
        /// There is no monitor connected on the specified video present target.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MONITOR_NOT_CONNECTED = new HRESULT("0xC0262338", "ERROR_GRAPHICS_MONITOR_NOT_CONNECTED", "There is no monitor connected on the specified video present target.");

        /// <summary>
        /// Specified source is not part of the specified VidPN's topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_SOURCE_NOT_IN_TOPOLOGY = new HRESULT("0xC0262339", "ERROR_GRAPHICS_SOURCE_NOT_IN_TOPOLOGY", "Specified source is not part of the specified VidPN's topology.");

        /// <summary>
        /// Specified primary surface size is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_PRIMARYSURFACE_SIZE = new HRESULT("0xC026233A", "ERROR_GRAPHICS_INVALID_PRIMARYSURFACE_SIZE", "Specified primary surface size is invalid.");

        /// <summary>
        /// Specified visible region size is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VISIBLEREGION_SIZE = new HRESULT("0xC026233B", "ERROR_GRAPHICS_INVALID_VISIBLEREGION_SIZE", "Specified visible region size is invalid.");

        /// <summary>
        /// Specified stride is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_STRIDE = new HRESULT("0xC026233C", "ERROR_GRAPHICS_INVALID_STRIDE", "Specified stride is invalid.");

        /// <summary>
        /// Specified pixel format is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_PIXELFORMAT = new HRESULT("0xC026233D", "ERROR_GRAPHICS_INVALID_PIXELFORMAT", "Specified pixel format is invalid.");

        /// <summary>
        /// Specified color basis is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_COLORBASIS = new HRESULT("0xC026233E", "ERROR_GRAPHICS_INVALID_COLORBASIS", "Specified color basis is invalid.");

        /// <summary>
        /// Specified pixel value access mode is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_PIXELVALUEACCESSMODE = new HRESULT("0xC026233F", "ERROR_GRAPHICS_INVALID_PIXELVALUEACCESSMODE", "Specified pixel value access mode is invalid.");

        /// <summary>
        /// Specified target is not part of the specified VidPN's topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_TARGET_NOT_IN_TOPOLOGY = new HRESULT("0xC0262340", "ERROR_GRAPHICS_TARGET_NOT_IN_TOPOLOGY", "Specified target is not part of the specified VidPN's topology.");

        /// <summary>
        /// Failed to acquire display mode management interface.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_DISPLAY_MODE_MANAGEMENT_SUPPORT = new HRESULT("0xC0262341", "ERROR_GRAPHICS_NO_DISPLAY_MODE_MANAGEMENT_SUPPORT", "Failed to acquire display mode management interface.");

        /// <summary>
        /// Specified VidPN source is already owned by a DMM client and cannot be used until that client releases it.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE = new HRESULT("0xC0262342", "ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE", "Specified VidPN source is already owned by a DMM client and cannot be used until that client releases it.");

        /// <summary>
        /// Specified VidPN is active and cannot be accessed.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CANT_ACCESS_ACTIVE_VIDPN = new HRESULT("0xC0262343", "ERROR_GRAPHICS_CANT_ACCESS_ACTIVE_VIDPN", "Specified VidPN is active and cannot be accessed.");

        /// <summary>
        /// Specified VidPN present path importance ordinal is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_PATH_IMPORTANCE_ORDINAL = new HRESULT("0xC0262344", "ERROR_GRAPHICS_INVALID_PATH_IMPORTANCE_ORDINAL", "Specified VidPN present path importance ordinal is invalid.");

        /// <summary>
        /// Specified VidPN present path content geometry transformation is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_PATH_CONTENT_GEOMETRY_TRANSFORMATION = new HRESULT("0xC0262345", "ERROR_GRAPHICS_INVALID_PATH_CONTENT_GEOMETRY_TRANSFORMATION", "Specified VidPN present path content geometry transformation is invalid.");

        /// <summary>
        /// Specified content geometry transformation is not supported on the respective VidPN present path.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_SUPPORTED = new HRESULT("0xC0262346", "ERROR_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_SUPPORTED", "Specified content geometry transformation is not supported on the respective VidPN present path.");

        /// <summary>
        /// Specified gamma ramp is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_GAMMA_RAMP = new HRESULT("0xC0262347", "ERROR_GRAPHICS_INVALID_GAMMA_RAMP", "Specified gamma ramp is invalid.");

        /// <summary>
        /// Specified gamma ramp is not supported on the respective VidPN present path.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_GAMMA_RAMP_NOT_SUPPORTED = new HRESULT("0xC0262348", "ERROR_GRAPHICS_GAMMA_RAMP_NOT_SUPPORTED", "Specified gamma ramp is not supported on the respective VidPN present path.");

        /// <summary>
        /// Multi-sampling is not supported on the respective VidPN present path.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MULTISAMPLING_NOT_SUPPORTED = new HRESULT("0xC0262349", "ERROR_GRAPHICS_MULTISAMPLING_NOT_SUPPORTED", "Multi-sampling is not supported on the respective VidPN present path.");

        /// <summary>
        /// Specified mode is not in the specified mode set.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MODE_NOT_IN_MODESET = new HRESULT("0xC026234A", "ERROR_GRAPHICS_MODE_NOT_IN_MODESET", "Specified mode is not in the specified mode set.");

        /// <summary>
        /// Specified data set (e.g. mode set, frequency range set, descriptor set, topology, etc.) is empty.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DATASET_IS_EMPTY = new HRESULT("0x0026234B", "ERROR_GRAPHICS_DATASET_IS_EMPTY", "Specified data set (e.g. mode set, frequency range set, descriptor set, topology, etc.) is empty.");

        /// <summary>
        /// Specified data set (e.g. mode set, frequency range set, descriptor set, topology, etc.) does not contain any more elements.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_MORE_ELEMENTS_IN_DATASET = new HRESULT("0x0026234C", "ERROR_GRAPHICS_NO_MORE_ELEMENTS_IN_DATASET", "Specified data set (e.g. mode set, frequency range set, descriptor set, topology, etc.) does not contain any more elements.");

        /// <summary>
        /// Specified VidPN topology recommendation reason is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_VIDPN_TOPOLOGY_RECOMMENDATION_REASON = new HRESULT("0xC026234D", "ERROR_GRAPHICS_INVALID_VIDPN_TOPOLOGY_RECOMMENDATION_REASON", "Specified VidPN topology recommendation reason is invalid.");

        /// <summary>
        /// Specified VidPN present path content type is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_PATH_CONTENT_TYPE = new HRESULT("0xC026234E", "ERROR_GRAPHICS_INVALID_PATH_CONTENT_TYPE", "Specified VidPN present path content type is invalid.");

        /// <summary>
        /// Specified VidPN present path copy protection type is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_COPYPROTECTION_TYPE = new HRESULT("0xC026234F", "ERROR_GRAPHICS_INVALID_COPYPROTECTION_TYPE", "Specified VidPN present path copy protection type is invalid.");

        /// <summary>
        /// No more than one unassigned mode set can exist at any given time for a given VidPN source/target.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_UNASSIGNED_MODESET_ALREADY_EXISTS = new HRESULT("0xC0262350", "ERROR_GRAPHICS_UNASSIGNED_MODESET_ALREADY_EXISTS", "No more than one unassigned mode set can exist at any given time for a given VidPN source/target.");

        /// <summary>
        /// Specified content transformation is not pinned on the specified VidPN present path.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_PINNED = new HRESULT("0x00262351", "ERROR_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_PINNED", "Specified content transformation is not pinned on the specified VidPN present path.");

        /// <summary>
        /// Specified scanline ordering type is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_SCANLINE_ORDERING = new HRESULT("0xC0262352", "ERROR_GRAPHICS_INVALID_SCANLINE_ORDERING", "Specified scanline ordering type is invalid.");

        /// <summary>
        /// Topology changes are not allowed for the specified VidPN.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_TOPOLOGY_CHANGES_NOT_ALLOWED = new HRESULT("0xC0262353", "ERROR_GRAPHICS_TOPOLOGY_CHANGES_NOT_ALLOWED", "Topology changes are not allowed for the specified VidPN.");

        /// <summary>
        /// All available importance ordinals are already used in specified topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_AVAILABLE_IMPORTANCE_ORDINALS = new HRESULT("0xC0262354", "ERROR_GRAPHICS_NO_AVAILABLE_IMPORTANCE_ORDINALS", "All available importance ordinals are already used in specified topology.");

        /// <summary>
        /// Specified primary surface has a different private format attribute than the current primary surface
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INCOMPATIBLE_PRIVATE_FORMAT = new HRESULT("0xC0262355", "ERROR_GRAPHICS_INCOMPATIBLE_PRIVATE_FORMAT", "Specified primary surface has a different private format attribute than the current primary surface");

        /// <summary>
        /// Specified mode pruning algorithm is invalid
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MODE_PRUNING_ALGORITHM = new HRESULT("0xC0262356", "ERROR_GRAPHICS_INVALID_MODE_PRUNING_ALGORITHM", "Specified mode pruning algorithm is invalid");

        /// <summary>
        /// Specified monitor capability origin is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITOR_CAPABILITY_ORIGIN = new HRESULT("0xC0262357", "ERROR_GRAPHICS_INVALID_MONITOR_CAPABILITY_ORIGIN", "Specified monitor capability origin is invalid.");

        /// <summary>
        /// Specified monitor frequency range constraint is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGE_CONSTRAINT = new HRESULT("0xC0262358", "ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGE_CONSTRAINT", "Specified monitor frequency range constraint is invalid.");

        /// <summary>
        /// Maximum supported number of present paths has been reached.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MAX_NUM_PATHS_REACHED = new HRESULT("0xC0262359", "ERROR_GRAPHICS_MAX_NUM_PATHS_REACHED", "Maximum supported number of present paths has been reached.");

        /// <summary>
        /// Miniport requested that augmentation be canceled for the specified source of the specified VidPN's topology.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CANCEL_VIDPN_TOPOLOGY_AUGMENTATION = new HRESULT("0xC026235A", "ERROR_GRAPHICS_CANCEL_VIDPN_TOPOLOGY_AUGMENTATION", "Miniport requested that augmentation be canceled for the specified source of the specified VidPN's topology.");

        /// <summary>
        /// Specified client type was not recognized.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_CLIENT_TYPE = new HRESULT("0xC026235B", "ERROR_GRAPHICS_INVALID_CLIENT_TYPE", "Specified client type was not recognized.");

        /// <summary>
        /// Client VidPN is not set on this adapter (e.g. no user mode initiated mode changes took place on this adapter yet).
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CLIENTVIDPN_NOT_SET = new HRESULT("0xC026235C", "ERROR_GRAPHICS_CLIENTVIDPN_NOT_SET", "Client VidPN is not set on this adapter (e.g. no user mode initiated mode changes took place on this adapter yet).");

        /// <summary>
        /// Specified display adapter child device already has an external device connected to it.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_SPECIFIED_CHILD_ALREADY_CONNECTED = new HRESULT("0xC0262400", "ERROR_GRAPHICS_SPECIFIED_CHILD_ALREADY_CONNECTED", "Specified display adapter child device already has an external device connected to it.");

        /// <summary>
        /// Specified display adapter child device does not support descriptor exposure.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CHILD_DESCRIPTOR_NOT_SUPPORTED = new HRESULT("0xC0262401", "ERROR_GRAPHICS_CHILD_DESCRIPTOR_NOT_SUPPORTED", "Specified display adapter child device does not support descriptor exposure.");

        /// <summary>
        /// Child device presence was not reliably detected.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_UNKNOWN_CHILD_STATUS = new HRESULT("0x4026242F", "ERROR_GRAPHICS_UNKNOWN_CHILD_STATUS", "Child device presence was not reliably detected.");

        /// <summary>
        /// The display adapter is not linked to any other adapters.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NOT_A_LINKED_ADAPTER = new HRESULT("0xC0262430", "ERROR_GRAPHICS_NOT_A_LINKED_ADAPTER", "The display adapter is not linked to any other adapters.");

        /// <summary>
        /// Lead adapter in a linked configuration was not enumerated yet.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_LEADLINK_NOT_ENUMERATED = new HRESULT("0xC0262431", "ERROR_GRAPHICS_LEADLINK_NOT_ENUMERATED", "Lead adapter in a linked configuration was not enumerated yet.");

        /// <summary>
        /// Some chain adapters in a linked configuration were not enumerated yet.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CHAINLINKS_NOT_ENUMERATED = new HRESULT("0xC0262432", "ERROR_GRAPHICS_CHAINLINKS_NOT_ENUMERATED", "Some chain adapters in a linked configuration were not enumerated yet.");

        /// <summary>
        /// The chain of linked adapters is not ready to start because of an unknown failure.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ADAPTER_CHAIN_NOT_READY = new HRESULT("0xC0262433", "ERROR_GRAPHICS_ADAPTER_CHAIN_NOT_READY", "The chain of linked adapters is not ready to start because of an unknown failure.");

        /// <summary>
        /// An attempt was made to start a lead link display adapter when the chain links were not started yet.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CHAINLINKS_NOT_STARTED = new HRESULT("0xC0262434", "ERROR_GRAPHICS_CHAINLINKS_NOT_STARTED", "An attempt was made to start a lead link display adapter when the chain links were not started yet.");

        /// <summary>
        /// An attempt was made to power up a lead link display adapter when the chain links were powered down.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_CHAINLINKS_NOT_POWERED_ON = new HRESULT("0xC0262435", "ERROR_GRAPHICS_CHAINLINKS_NOT_POWERED_ON", "An attempt was made to power up a lead link display adapter when the chain links were powered down.");

        /// <summary>
        /// The adapter link was found to be in an inconsistent state. Not all adapters are in an expected PNP/Power state.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INCONSISTENT_DEVICE_LINK_STATE = new HRESULT("0xC0262436", "ERROR_GRAPHICS_INCONSISTENT_DEVICE_LINK_STATE", "The adapter link was found to be in an inconsistent state. Not all adapters are in an expected PNP/Power state.");

        /// <summary>
        /// Starting the leadlink adapter has been deferred temporarily.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_LEADLINK_START_DEFERRED = new HRESULT("0x40262437", "ERROR_GRAPHICS_LEADLINK_START_DEFERRED", "Starting the leadlink adapter has been deferred temporarily.");

        /// <summary>
        /// The driver trying to start is not the same as the driver for the POSTed display adapter.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NOT_POST_DEVICE_DRIVER = new HRESULT("0xC0262438", "ERROR_GRAPHICS_NOT_POST_DEVICE_DRIVER", "The driver trying to start is not the same as the driver for the POSTed display adapter.");

        /// <summary>
        /// The display adapter is being polled for children too frequently at the same polling level.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_POLLING_TOO_FREQUENTLY = new HRESULT("0x40262439", "ERROR_GRAPHICS_POLLING_TOO_FREQUENTLY", "The display adapter is being polled for children too frequently at the same polling level.");

        /// <summary>
        /// Starting the adapter has been deferred temporarily.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_START_DEFERRED = new HRESULT("0x4026243A", "ERROR_GRAPHICS_START_DEFERRED", "Starting the adapter has been deferred temporarily.");

        /// <summary>
        /// An operation is being attempted that requires the display adapter to be in a quiescent state.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ADAPTER_ACCESS_NOT_EXCLUDED = new HRESULT("0xC026243B", "ERROR_GRAPHICS_ADAPTER_ACCESS_NOT_EXCLUDED", "An operation is being attempted that requires the display adapter to be in a quiescent state.");

        /// <summary>
        /// The driver does not support OPM.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_NOT_SUPPORTED = new HRESULT("0xC0262500", "ERROR_GRAPHICS_OPM_NOT_SUPPORTED", "The driver does not support OPM.");

        /// <summary>
        /// The driver does not support COPP.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_COPP_NOT_SUPPORTED = new HRESULT("0xC0262501", "ERROR_GRAPHICS_COPP_NOT_SUPPORTED", "The driver does not support COPP.");

        /// <summary>
        /// The driver does not support UAB.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_UAB_NOT_SUPPORTED = new HRESULT("0xC0262502", "ERROR_GRAPHICS_UAB_NOT_SUPPORTED", "The driver does not support UAB.");

        /// <summary>
        /// The specified encrypted parameters are invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_INVALID_ENCRYPTED_PARAMETERS = new HRESULT("0xC0262503", "ERROR_GRAPHICS_OPM_INVALID_ENCRYPTED_PARAMETERS", "The specified encrypted parameters are invalid.");

        /// <summary>
        /// The GDI display device passed to this function does not have any active video outputs.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_NO_VIDEO_OUTPUTS_EXIST = new HRESULT("0xC0262505", "ERROR_GRAPHICS_OPM_NO_VIDEO_OUTPUTS_EXIST", "The GDI display device passed to this function does not have any active video outputs.");

        /// <summary>
        /// An internal error caused this operation to fail.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_INTERNAL_ERROR = new HRESULT("0xC026250B", "ERROR_GRAPHICS_OPM_INTERNAL_ERROR", "An internal error caused this operation to fail.");

        /// <summary>
        /// The function failed because the caller passed in an invalid OPM user mode handle.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_INVALID_HANDLE = new HRESULT("0xC026250C", "ERROR_GRAPHICS_OPM_INVALID_HANDLE", "The function failed because the caller passed in an invalid OPM user mode handle.");

        /// <summary>
        /// A certificate could not be returned because the certificate buffer passed to the function was too small.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PVP_INVALID_CERTIFICATE_LENGTH = new HRESULT("0xC026250E", "ERROR_GRAPHICS_PVP_INVALID_CERTIFICATE_LENGTH", "A certificate could not be returned because the certificate buffer passed to the function was too small.");

        /// <summary>
        /// A video output could not be created because the frame buffer is in spanning mode.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_SPANNING_MODE_ENABLED = new HRESULT("0xC026250F", "ERROR_GRAPHICS_OPM_SPANNING_MODE_ENABLED", "A video output could not be created because the frame buffer is in spanning mode.");

        /// <summary>
        /// A video output could not be created because the frame buffer is in theater mode.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_THEATER_MODE_ENABLED = new HRESULT("0xC0262510", "ERROR_GRAPHICS_OPM_THEATER_MODE_ENABLED", "A video output could not be created because the frame buffer is in theater mode.");

        /// <summary>
        /// The function failed because the display adapter's Hardware Functionality Scan failed to validate the graphics hardware.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PVP_HFS_FAILED = new HRESULT("0xC0262511", "ERROR_GRAPHICS_PVP_HFS_FAILED", "The function failed because the display adapter's Hardware Functionality Scan failed to validate the graphics hardware.");

        /// <summary>
        /// The HDCP System Renewability Message passed to this function did not comply with section 5 of the HDCP 1.1 specification.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_INVALID_SRM = new HRESULT("0xC0262512", "ERROR_GRAPHICS_OPM_INVALID_SRM", "The HDCP System Renewability Message passed to this function did not comply with section 5 of the HDCP 1.1 specification.");

        /// <summary>
        /// The video output cannot enable the High-bandwidth Digital Content Protection (HDCP) System because it does not support HDCP.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_HDCP = new HRESULT("0xC0262513", "ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_HDCP", "The video output cannot enable the High-bandwidth Digital Content Protection (HDCP) System because it does not support HDCP.");

        /// <summary>
        /// The video output cannot enable Analog Copy Protection (ACP) because it does not support ACP.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_ACP = new HRESULT("0xC0262514", "ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_ACP", "The video output cannot enable Analog Copy Protection (ACP) because it does not support ACP.");

        /// <summary>
        /// The video output cannot enable the Content Generation Management System Analog (CGMS-A) protection technology because it does not support CGMS-A.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_CGMSA = new HRESULT("0xC0262515", "ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_CGMSA", "The video output cannot enable the Content Generation Management System Analog (CGMS-A) protection technology because it does not support CGMS-A.");

        /// <summary>
        /// The IOPMVideoOutput::GetInformation method cannot return the version of the SRM being used because the application never successfully passed an SRM to the video output.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_HDCP_SRM_NEVER_SET = new HRESULT("0xC0262516", "ERROR_GRAPHICS_OPM_HDCP_SRM_NEVER_SET", "The IOPMVideoOutput::GetInformation method cannot return the version of the SRM being used because the application never successfully passed an SRM to the video output.");

        /// <summary>
        /// The IOPMVideoOutput::Configure method cannot enable the specified output protection technology because the output's screen resolution is too high.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_RESOLUTION_TOO_HIGH = new HRESULT("0xC0262517", "ERROR_GRAPHICS_OPM_RESOLUTION_TOO_HIGH", "The IOPMVideoOutput::Configure method cannot enable the specified output protection technology because the output's screen resolution is too high.");

        /// <summary>
        /// The IOPMVideoOutput::Configure method cannot enable HDCP because the display adapter's HDCP hardware is already being used by other physical outputs.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_ALL_HDCP_HARDWARE_ALREADY_IN_USE = new HRESULT("0xC0262518", "ERROR_GRAPHICS_OPM_ALL_HDCP_HARDWARE_ALREADY_IN_USE", "The IOPMVideoOutput::Configure method cannot enable HDCP because the display adapter's HDCP hardware is already being used by other physical outputs.");

        /// <summary>
        /// The operating system asynchronously destroyed this OPM video output because the operating system's state changed. This error typically occurs because the monitor PDO associated with this video output was removed, the monitor PDO associated with this video output was stopped, the video output's session became a non-console session or the video output's desktop became an inactive desktop.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_VIDEO_OUTPUT_NO_LONGER_EXISTS = new HRESULT("0xC026251A", "ERROR_GRAPHICS_OPM_VIDEO_OUTPUT_NO_LONGER_EXISTS", "The operating system asynchronously destroyed this OPM video output because the operating system's state changed. This error typically occurs because the monitor PDO associated with this video output was removed, the monitor PDO associated with this video output was stopped, the video output's session became a non-console session or the video output's desktop became an inactive desktop.");

        /// <summary>
        /// The method failed because the session is changing its type. No IOPMVideoOutput methods can be called when a session is changing its type. There are currently three types of sessions: console, disconnected and remote.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_SESSION_TYPE_CHANGE_IN_PROGRESS = new HRESULT("0xC026251B", "ERROR_GRAPHICS_OPM_SESSION_TYPE_CHANGE_IN_PROGRESS", "The method failed because the session is changing its type. No IOPMVideoOutput methods can be called when a session is changing its type. There are currently three types of sessions: console, disconnected and remote.");

        /// <summary>
        /// Either the IOPMVideoOutput::COPPCompatibleGetInformation, IOPMVideoOutput::GetInformation, or IOPMVideoOutput::Configure method failed. This error is returned when the caller tries to use a COPP specific command while the video output has OPM semantics only.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_VIDEO_OUTPUT_DOES_NOT_HAVE_COPP_SEMANTICS = new HRESULT("0xC026251C", "ERROR_GRAPHICS_OPM_VIDEO_OUTPUT_DOES_NOT_HAVE_COPP_SEMANTICS", "Either the IOPMVideoOutput::COPPCompatibleGetInformation, IOPMVideoOutput::GetInformation, or IOPMVideoOutput::Configure method failed. This error is returned when the caller tries to use a COPP specific command while the video output has OPM semantics only.");

        /// <summary>
        /// The IOPMVideoOutput::GetInformation and IOPMVideoOutput::COPPCompatibleGetInformation methods return this error if the passed in sequence number is not the expected sequence number or the passed in OMAC value is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_INVALID_INFORMATION_REQUEST = new HRESULT("0xC026251D", "ERROR_GRAPHICS_OPM_INVALID_INFORMATION_REQUEST", "The IOPMVideoOutput::GetInformation and IOPMVideoOutput::COPPCompatibleGetInformation methods return this error if the passed in sequence number is not the expected sequence number or the passed in OMAC value is invalid.");

        /// <summary>
        /// The method failed because an unexpected error occurred inside of a display driver.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_DRIVER_INTERNAL_ERROR = new HRESULT("0xC026251E", "ERROR_GRAPHICS_OPM_DRIVER_INTERNAL_ERROR", "The method failed because an unexpected error occurred inside of a display driver.");

        /// <summary>
        /// Either the IOPMVideoOutput::COPPCompatibleGetInformation, IOPMVideoOutput::GetInformation, or IOPMVideoOutput::Configure method failed. This error is returned when the caller tries to use an OPM specific command while the video output has COPP semantics only.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_VIDEO_OUTPUT_DOES_NOT_HAVE_OPM_SEMANTICS = new HRESULT("0xC026251F", "ERROR_GRAPHICS_OPM_VIDEO_OUTPUT_DOES_NOT_HAVE_OPM_SEMANTICS", "Either the IOPMVideoOutput::COPPCompatibleGetInformation, IOPMVideoOutput::GetInformation, or IOPMVideoOutput::Configure method failed. This error is returned when the caller tries to use an OPM specific command while the video output has COPP semantics only.");

        /// <summary>
        /// The IOPMVideoOutput::COPPCompatibleGetInformation or IOPMVideoOutput::Configure method failed because the display driver does not support the OPM_GET_ACP_AND_CGMSA_SIGNALING and OPM_SET_ACP_AND_CGMSA_SIGNALING GUIDs.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_SIGNALING_NOT_SUPPORTED = new HRESULT("0xC0262520", "ERROR_GRAPHICS_OPM_SIGNALING_NOT_SUPPORTED", "The IOPMVideoOutput::COPPCompatibleGetInformation or IOPMVideoOutput::Configure method failed because the display driver does not support the OPM_GET_ACP_AND_CGMSA_SIGNALING and OPM_SET_ACP_AND_CGMSA_SIGNALING GUIDs.");

        /// <summary>
        /// The IOPMVideoOutput::Configure function returns this error code if the passed in sequence number is not the expected sequence number or the passed in OMAC value is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_OPM_INVALID_CONFIGURATION_REQUEST = new HRESULT("0xC0262521", "ERROR_GRAPHICS_OPM_INVALID_CONFIGURATION_REQUEST", "The IOPMVideoOutput::Configure function returns this error code if the passed in sequence number is not the expected sequence number or the passed in OMAC value is invalid.");

        /// <summary>
        /// The monitor connected to the specified video output does not have an I2C bus.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_I2C_NOT_SUPPORTED = new HRESULT("0xC0262580", "ERROR_GRAPHICS_I2C_NOT_SUPPORTED", "The monitor connected to the specified video output does not have an I2C bus.");

        /// <summary>
        /// No device on the I2C bus has the specified address.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_I2C_DEVICE_DOES_NOT_EXIST = new HRESULT("0xC0262581", "ERROR_GRAPHICS_I2C_DEVICE_DOES_NOT_EXIST", "No device on the I2C bus has the specified address.");

        /// <summary>
        /// An error occurred while transmitting data to the device on the I2C bus.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_I2C_ERROR_TRANSMITTING_DATA = new HRESULT("0xC0262582", "ERROR_GRAPHICS_I2C_ERROR_TRANSMITTING_DATA", "An error occurred while transmitting data to the device on the I2C bus.");

        /// <summary>
        /// An error occurred while receiving data from the device on the I2C bus.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_I2C_ERROR_RECEIVING_DATA = new HRESULT("0xC0262583", "ERROR_GRAPHICS_I2C_ERROR_RECEIVING_DATA", "An error occurred while receiving data from the device on the I2C bus.");

        /// <summary>
        /// The monitor does not support the specified VCP code.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DDCCI_VCP_NOT_SUPPORTED = new HRESULT("0xC0262584", "ERROR_GRAPHICS_DDCCI_VCP_NOT_SUPPORTED", "The monitor does not support the specified VCP code.");

        /// <summary>
        /// The data received from the monitor is invalid.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DDCCI_INVALID_DATA = new HRESULT("0xC0262585", "ERROR_GRAPHICS_DDCCI_INVALID_DATA", "The data received from the monitor is invalid.");

        /// <summary>
        /// The function failed because a monitor returned an invalid Timing Status byte when the operating system used the DDC/CI Get Timing Report & Timing Message command to get a timing report from a monitor.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DDCCI_MONITOR_RETURNED_INVALID_TIMING_STATUS_BYTE = new HRESULT("0xC0262586", "ERROR_GRAPHICS_DDCCI_MONITOR_RETURNED_INVALID_TIMING_STATUS_BYTE", "The function failed because a monitor returned an invalid Timing Status byte when the operating system used the DDC/CI Get Timing Report & Timing Message command to get a timing report from a monitor.");

        /// <summary>
        /// The monitor returned a DDC/CI capabilities string which did not comply with the ACCESS.bus 3.0, DDC/CI 1.1, or MCCS 2 Revision 1 specification.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_INVALID_CAPABILITIES_STRING = new HRESULT("0xC0262587", "ERROR_GRAPHICS_MCA_INVALID_CAPABILITIES_STRING", "The monitor returned a DDC/CI capabilities string which did not comply with the ACCESS.bus 3.0, DDC/CI 1.1, or MCCS 2 Revision 1 specification.");

        /// <summary>
        /// An internal Monitor Configuration API error occurred.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_INTERNAL_ERROR = new HRESULT("0xC0262588", "ERROR_GRAPHICS_MCA_INTERNAL_ERROR", "An internal Monitor Configuration API error occurred.");

        /// <summary>
        /// An operation failed because a DDC/CI message had an invalid value in its command field.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_COMMAND = new HRESULT("0xC0262589", "ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_COMMAND", "An operation failed because a DDC/CI message had an invalid value in its command field.");

        /// <summary>
        /// An error occurred because the field length of a DDC/CI message contained an invalid value.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_LENGTH = new HRESULT("0xC026258A", "ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_LENGTH", "An error occurred because the field length of a DDC/CI message contained an invalid value.");

        /// <summary>
        /// An error occurred because the checksum field in a DDC/CI message did not match the message's computed checksum value. This error implies that the data was corrupted while it was being transmitted from a monitor to a computer.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_CHECKSUM = new HRESULT("0xC026258B", "ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_CHECKSUM", "An error occurred because the checksum field in a DDC/CI message did not match the message's computed checksum value. This error implies that the data was corrupted while it was being transmitted from a monitor to a computer.");

        /// <summary>
        /// This function failed because an invalid monitor handle was passed to it.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_PHYSICAL_MONITOR_HANDLE = new HRESULT("0xC026258C", "ERROR_GRAPHICS_INVALID_PHYSICAL_MONITOR_HANDLE", "This function failed because an invalid monitor handle was passed to it.");

        /// <summary>
        /// The operating system asynchronously destroyed the monitor which corresponds to this handle because the operating system's state changed. This error typically occurs because the monitor PDO associated with this handle was removed, the monitor PDO associated with this handle was stopped, or a display mode change occurred. A display mode change occurs when windows sends a WM_DISPLAYCHANGE windows message to applications.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MONITOR_NO_LONGER_EXISTS = new HRESULT("0xC026258D", "ERROR_GRAPHICS_MONITOR_NO_LONGER_EXISTS", "The operating system asynchronously destroyed the monitor which corresponds to this handle because the operating system's state changed. This error typically occurs because the monitor PDO associated with this handle was removed, the monitor PDO associated with this handle was stopped, or a display mode change occurred. A display mode change occurs when windows sends a WM_DISPLAYCHANGE windows message to applications.");

        /// <summary>
        /// A continuous VCP code's current value is greater than its maximum value. This error code indicates that a monitor returned an invalid value.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DDCCI_CURRENT_CURRENT_VALUE_GREATER_THAN_MAXIMUM_VALUE = new HRESULT("0xC02625D8", "ERROR_GRAPHICS_DDCCI_CURRENT_CURRENT_VALUE_GREATER_THAN_MAXIMUM_VALUE", "A continuous VCP code's current value is greater than its maximum value. This error code indicates that a monitor returned an invalid value.");

        /// <summary>
        /// The monitor's VCP Version (0xDF) VCP code returned an invalid version value.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_INVALID_VCP_VERSION = new HRESULT("0xC02625D9", "ERROR_GRAPHICS_MCA_INVALID_VCP_VERSION", "The monitor's VCP Version (0xDF) VCP code returned an invalid version value.");

        /// <summary>
        /// The monitor does not comply with the MCCS specification it claims to support.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_MONITOR_VIOLATES_MCCS_SPECIFICATION = new HRESULT("0xC02625DA", "ERROR_GRAPHICS_MCA_MONITOR_VIOLATES_MCCS_SPECIFICATION", "The monitor does not comply with the MCCS specification it claims to support.");

        /// <summary>
        /// The MCCS version in a monitor's mccs_ver capability does not match the MCCS version the monitor reports when the VCP Version (0xDF) VCP code is used.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_MCCS_VERSION_MISMATCH = new HRESULT("0xC02625DB", "ERROR_GRAPHICS_MCA_MCCS_VERSION_MISMATCH", "The MCCS version in a monitor's mccs_ver capability does not match the MCCS version the monitor reports when the VCP Version (0xDF) VCP code is used.");

        /// <summary>
        /// The Monitor Configuration API only works with monitors which support the MCCS 1.0 specification, MCCS 2.0 specification or the MCCS 2.0 Revision 1 specification.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_UNSUPPORTED_MCCS_VERSION = new HRESULT("0xC02625DC", "ERROR_GRAPHICS_MCA_UNSUPPORTED_MCCS_VERSION", "The Monitor Configuration API only works with monitors which support the MCCS 1.0 specification, MCCS 2.0 specification or the MCCS 2.0 Revision 1 specification.");

        /// <summary>
        /// The monitor returned an invalid monitor technology type. CRT, Plasma and LCD (TFT) are examples of monitor technology types. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_INVALID_TECHNOLOGY_TYPE_RETURNED = new HRESULT("0xC02625DE", "ERROR_GRAPHICS_MCA_INVALID_TECHNOLOGY_TYPE_RETURNED", "The monitor returned an invalid monitor technology type. CRT, Plasma and LCD (TFT) are examples of monitor technology types. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.");

        /// <summary>
        /// SetMonitorColorTemperature()'s caller passed a color temperature to it which the current monitor did not support. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MCA_UNSUPPORTED_COLOR_TEMPERATURE = new HRESULT("0xC02625DF", "ERROR_GRAPHICS_MCA_UNSUPPORTED_COLOR_TEMPERATURE", "SetMonitorColorTemperature()'s caller passed a color temperature to it which the current monitor did not support. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.");

        /// <summary>
        /// This function can only be used if a program is running in the local console session. It cannot be used if the program is running on a remote desktop session or on a terminal server session.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_ONLY_CONSOLE_SESSION_SUPPORTED = new HRESULT("0xC02625E0", "ERROR_GRAPHICS_ONLY_CONSOLE_SESSION_SUPPORTED", "This function can only be used if a program is running in the local console session. It cannot be used if the program is running on a remote desktop session or on a terminal server session.");

        /// <summary>
        /// This function cannot find an actual GDI display device which corresponds to the specified GDI display device name.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_DISPLAY_DEVICE_CORRESPONDS_TO_NAME = new HRESULT("0xC02625E1", "ERROR_GRAPHICS_NO_DISPLAY_DEVICE_CORRESPONDS_TO_NAME", "This function cannot find an actual GDI display device which corresponds to the specified GDI display device name.");

        /// <summary>
        /// The function failed because the specified GDI display device was not attached to the Windows desktop.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_DISPLAY_DEVICE_NOT_ATTACHED_TO_DESKTOP = new HRESULT("0xC02625E2", "ERROR_GRAPHICS_DISPLAY_DEVICE_NOT_ATTACHED_TO_DESKTOP", "The function failed because the specified GDI display device was not attached to the Windows desktop.");

        /// <summary>
        /// This function does not support GDI mirroring display devices because GDI mirroring display devices do not have any physical monitors associated with them.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_MIRRORING_DEVICES_NOT_SUPPORTED = new HRESULT("0xC02625E3", "ERROR_GRAPHICS_MIRRORING_DEVICES_NOT_SUPPORTED", "This function does not support GDI mirroring display devices because GDI mirroring display devices do not have any physical monitors associated with them.");

        /// <summary>
        /// The function failed because an invalid pointer parameter was passed to it. A pointer parameter is invalid if it is NULL, points to an invalid address, points to a kernel mode address, or is not correctly aligned.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INVALID_POINTER = new HRESULT("0xC02625E4", "ERROR_GRAPHICS_INVALID_POINTER", "The function failed because an invalid pointer parameter was passed to it. A pointer parameter is invalid if it is NULL, points to an invalid address, points to a kernel mode address, or is not correctly aligned.");

        /// <summary>
        /// The function failed because the specified GDI device did not have any monitors associated with it.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_NO_MONITORS_CORRESPOND_TO_DISPLAY_DEVICE = new HRESULT("0xC02625E5", "ERROR_GRAPHICS_NO_MONITORS_CORRESPOND_TO_DISPLAY_DEVICE", "The function failed because the specified GDI device did not have any monitors associated with it.");

        /// <summary>
        /// An array passed to the function cannot hold all of the data that the function must copy into the array.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_PARAMETER_ARRAY_TOO_SMALL = new HRESULT("0xC02625E6", "ERROR_GRAPHICS_PARAMETER_ARRAY_TOO_SMALL", "An array passed to the function cannot hold all of the data that the function must copy into the array.");

        /// <summary>
        /// An internal error caused an operation to fail.
        /// </summary>
        public static HRESULT ERROR_GRAPHICS_INTERNAL_ERROR = new HRESULT("0xC02625E7", "ERROR_GRAPHICS_INTERNAL_ERROR", "An internal error caused an operation to fail.");


        #endregion

        #region COM Error Codes (TPM, PLA, FVE)
        /// <summary>
        /// This is an error mask to convert TPM hardware errors to win errors.
        /// </summary>
        public static HRESULT TPM_E_ERROR_MASK = new HRESULT("0x80280000", "TPM_E_ERROR_MASK", "This is an error mask to convert TPM hardware errors to win errors.");

        /// <summary>
        /// Authentication failed.
        /// </summary>
        public static HRESULT TPM_E_AUTHFAIL = new HRESULT("0x80280001", "TPM_E_AUTHFAIL", "Authentication failed.");

        /// <summary>
        /// The index to a PCR, DIR or other register is incorrect.
        /// </summary>
        public static HRESULT TPM_E_BADINDEX = new HRESULT("0x80280002", "TPM_E_BADINDEX", "The index to a PCR, DIR or other register is incorrect.");

        /// <summary>
        /// One or more parameter is bad.
        /// </summary>
        public static HRESULT TPM_E_BAD_PARAMETER = new HRESULT("0x80280003", "TPM_E_BAD_PARAMETER", "One or more parameter is bad.");

        /// <summary>
        /// An operation completed successfully but the auditing of that operation failed.
        /// </summary>
        public static HRESULT TPM_E_AUDITFAILURE = new HRESULT("0x80280004", "TPM_E_AUDITFAILURE", "An operation completed successfully but the auditing of that operation failed.");

        /// <summary>
        /// The clear disable flag is set and all clear operations now require physical access.
        /// </summary>
        public static HRESULT TPM_E_CLEAR_DISABLED = new HRESULT("0x80280005", "TPM_E_CLEAR_DISABLED", "The clear disable flag is set and all clear operations now require physical access.");

        /// <summary>
        /// Activate the Trusted Platform Module (TPM).
        /// </summary>
        public static HRESULT TPM_E_DEACTIVATED = new HRESULT("0x80280006", "TPM_E_DEACTIVATED", "Activate the Trusted Platform Module (TPM).");

        /// <summary>
        /// Enable the Trusted Platform Module (TPM).
        /// </summary>
        public static HRESULT TPM_E_DISABLED = new HRESULT("0x80280007", "TPM_E_DISABLED", "Enable the Trusted Platform Module (TPM).");

        /// <summary>
        /// The target command has been disabled.
        /// </summary>
        public static HRESULT TPM_E_DISABLED_CMD = new HRESULT("0x80280008", "TPM_E_DISABLED_CMD", "The target command has been disabled.");

        /// <summary>
        /// The operation failed.
        /// </summary>
        public static HRESULT TPM_E_FAIL = new HRESULT("0x80280009", "TPM_E_FAIL", "The operation failed.");

        /// <summary>
        /// The ordinal was unknown or inconsistent.
        /// </summary>
        public static HRESULT TPM_E_BAD_ORDINAL = new HRESULT("0x8028000A", "TPM_E_BAD_ORDINAL", "The ordinal was unknown or inconsistent.");

        /// <summary>
        /// The ability to install an owner is disabled.
        /// </summary>
        public static HRESULT TPM_E_INSTALL_DISABLED = new HRESULT("0x8028000B", "TPM_E_INSTALL_DISABLED", "The ability to install an owner is disabled.");

        /// <summary>
        /// The key handle cannot be interpreted.
        /// </summary>
        public static HRESULT TPM_E_INVALID_KEYHANDLE = new HRESULT("0x8028000C", "TPM_E_INVALID_KEYHANDLE", "The key handle cannot be interpreted.");

        /// <summary>
        /// The key handle points to an invalid key.
        /// </summary>
        public static HRESULT TPM_E_KEYNOTFOUND = new HRESULT("0x8028000D", "TPM_E_KEYNOTFOUND", "The key handle points to an invalid key.");

        /// <summary>
        /// Unacceptable encryption scheme.
        /// </summary>
        public static HRESULT TPM_E_INAPPROPRIATE_ENC = new HRESULT("0x8028000E", "TPM_E_INAPPROPRIATE_ENC", "Unacceptable encryption scheme.");

        /// <summary>
        /// Migration authorization failed.
        /// </summary>
        public static HRESULT TPM_E_MIGRATEFAIL = new HRESULT("0x8028000F", "TPM_E_MIGRATEFAIL", "Migration authorization failed.");

        /// <summary>
        /// PCR information could not be interpreted.
        /// </summary>
        public static HRESULT TPM_E_INVALID_PCR_INFO = new HRESULT("0x80280010", "TPM_E_INVALID_PCR_INFO", "PCR information could not be interpreted.");

        /// <summary>
        /// No room to load key.
        /// </summary>
        public static HRESULT TPM_E_NOSPACE = new HRESULT("0x80280011", "TPM_E_NOSPACE", "No room to load key.");

        /// <summary>
        /// There is no Storage Root Key (SRK) set.
        /// </summary>
        public static HRESULT TPM_E_NOSRK = new HRESULT("0x80280012", "TPM_E_NOSRK", "There is no Storage Root Key (SRK) set.");

        /// <summary>
        /// An encrypted blob is invalid or was not created by this TPM.
        /// </summary>
        public static HRESULT TPM_E_NOTSEALED_BLOB = new HRESULT("0x80280013", "TPM_E_NOTSEALED_BLOB", "An encrypted blob is invalid or was not created by this TPM.");

        /// <summary>
        /// The Trusted Platform Module (TPM) already has an owner.
        /// </summary>
        public static HRESULT TPM_E_OWNER_SET = new HRESULT("0x80280014", "TPM_E_OWNER_SET", "The Trusted Platform Module (TPM) already has an owner.");

        /// <summary>
        /// The TPM has insufficient internal resources to perform the requested action.
        /// </summary>
        public static HRESULT TPM_E_RESOURCES = new HRESULT("0x80280015", "TPM_E_RESOURCES", "The TPM has insufficient internal resources to perform the requested action.");

        /// <summary>
        /// A random string was too short.
        /// </summary>
        public static HRESULT TPM_E_SHORTRANDOM = new HRESULT("0x80280016", "TPM_E_SHORTRANDOM", "A random string was too short.");

        /// <summary>
        /// The TPM does not have the space to perform the operation.
        /// </summary>
        public static HRESULT TPM_E_SIZE = new HRESULT("0x80280017", "TPM_E_SIZE", "The TPM does not have the space to perform the operation.");

        /// <summary>
        /// The named PCR value does not match the current PCR value.
        /// </summary>
        public static HRESULT TPM_E_WRONGPCRVAL = new HRESULT("0x80280018", "TPM_E_WRONGPCRVAL", "The named PCR value does not match the current PCR value.");

        /// <summary>
        /// The paramSize argument to the command has the incorrect value .
        /// </summary>
        public static HRESULT TPM_E_BAD_PARAM_SIZE = new HRESULT("0x80280019", "TPM_E_BAD_PARAM_SIZE", "The paramSize argument to the command has the incorrect value .");

        /// <summary>
        /// There is no existing SHA-1 thread.
        /// </summary>
        public static HRESULT TPM_E_SHA_THREAD = new HRESULT("0x8028001A", "TPM_E_SHA_THREAD", "There is no existing SHA-1 thread.");

        /// <summary>
        /// The calculation is unable to proceed because the existing SHA-1 thread has already encountered an error.
        /// </summary>
        public static HRESULT TPM_E_SHA_ERROR = new HRESULT("0x8028001B", "TPM_E_SHA_ERROR", "The calculation is unable to proceed because the existing SHA-1 thread has already encountered an error.");

        /// <summary>
        /// The TPM hardware device reported a failure during its internal self test. Try restarting the computer to resolve the problem. If the problem continues, you might need to replace your TPM hardware or motherboard.
        /// </summary>
        public static HRESULT TPM_E_FAILEDSELFTEST = new HRESULT("0x8028001C", "TPM_E_FAILEDSELFTEST", "The TPM hardware device reported a failure during its internal self test. Try restarting the computer to resolve the problem. If the problem continues, you might need to replace your TPM hardware or motherboard.");

        /// <summary>
        /// The authorization for the second key in a 2 key function failed authorization.
        /// </summary>
        public static HRESULT TPM_E_AUTH2FAIL = new HRESULT("0x8028001D", "TPM_E_AUTH2FAIL", "The authorization for the second key in a 2 key function failed authorization.");

        /// <summary>
        /// The tag value sent to for a command is invalid.
        /// </summary>
        public static HRESULT TPM_E_BADTAG = new HRESULT("0x8028001E", "TPM_E_BADTAG", "The tag value sent to for a command is invalid.");

        /// <summary>
        /// An IO error occurred transmitting information to the TPM.
        /// </summary>
        public static HRESULT TPM_E_IOERROR = new HRESULT("0x8028001F", "TPM_E_IOERROR", "An IO error occurred transmitting information to the TPM.");

        /// <summary>
        /// The encryption process had a problem.
        /// </summary>
        public static HRESULT TPM_E_ENCRYPT_ERROR = new HRESULT("0x80280020", "TPM_E_ENCRYPT_ERROR", "The encryption process had a problem.");

        /// <summary>
        /// The decryption process did not complete.
        /// </summary>
        public static HRESULT TPM_E_DECRYPT_ERROR = new HRESULT("0x80280021", "TPM_E_DECRYPT_ERROR", "The decryption process did not complete.");

        /// <summary>
        /// An invalid handle was used.
        /// </summary>
        public static HRESULT TPM_E_INVALID_AUTHHANDLE = new HRESULT("0x80280022", "TPM_E_INVALID_AUTHHANDLE", "An invalid handle was used.");

        /// <summary>
        /// The TPM does not have an Endorsement Key (EK) installed.
        /// </summary>
        public static HRESULT TPM_E_NO_ENDORSEMENT = new HRESULT("0x80280023", "TPM_E_NO_ENDORSEMENT", "The TPM does not have an Endorsement Key (EK) installed.");

        /// <summary>
        /// The usage of a key is not allowed.
        /// </summary>
        public static HRESULT TPM_E_INVALID_KEYUSAGE = new HRESULT("0x80280024", "TPM_E_INVALID_KEYUSAGE", "The usage of a key is not allowed.");

        /// <summary>
        /// The submitted entity type is not allowed.
        /// </summary>
        public static HRESULT TPM_E_WRONG_ENTITYTYPE = new HRESULT("0x80280025", "TPM_E_WRONG_ENTITYTYPE", "The submitted entity type is not allowed.");

        /// <summary>
        /// The command was received in the wrong sequence relative to TPM_Init and a subsequent TPM_Startup.
        /// </summary>
        public static HRESULT TPM_E_INVALID_POSTINIT = new HRESULT("0x80280026", "TPM_E_INVALID_POSTINIT", "The command was received in the wrong sequence relative to TPM_Init and a subsequent TPM_Startup.");

        /// <summary>
        /// Signed data cannot include additional DER information.
        /// </summary>
        public static HRESULT TPM_E_INAPPROPRIATE_SIG = new HRESULT("0x80280027", "TPM_E_INAPPROPRIATE_SIG", "Signed data cannot include additional DER information.");

        /// <summary>
        /// The key properties in TPM_KEY_PARMs are not supported by this TPM.
        /// </summary>
        public static HRESULT TPM_E_BAD_KEY_PROPERTY = new HRESULT("0x80280028", "TPM_E_BAD_KEY_PROPERTY", "The key properties in TPM_KEY_PARMs are not supported by this TPM.");

        /// <summary>
        /// The migration properties of this key are incorrect.
        /// </summary>
        public static HRESULT TPM_E_BAD_MIGRATION = new HRESULT("0x80280029", "TPM_E_BAD_MIGRATION", "The migration properties of this key are incorrect.");

        /// <summary>
        /// The signature or encryption scheme for this key is incorrect or not permitted in this situation.
        /// </summary>
        public static HRESULT TPM_E_BAD_SCHEME = new HRESULT("0x8028002A", "TPM_E_BAD_SCHEME", "The signature or encryption scheme for this key is incorrect or not permitted in this situation.");

        /// <summary>
        /// The size of the data (or blob) parameter is bad or inconsistent with the referenced key.
        /// </summary>
        public static HRESULT TPM_E_BAD_DATASIZE = new HRESULT("0x8028002B", "TPM_E_BAD_DATASIZE", "The size of the data (or blob) parameter is bad or inconsistent with the referenced key.");

        /// <summary>
        /// A mode parameter is bad, such as capArea or subCapArea for TPM_GetCapability, phsicalPresence parameter for TPM_PhysicalPresence, or migrationType for TPM_CreateMigrationBlob.
        /// </summary>
        public static HRESULT TPM_E_BAD_MODE = new HRESULT("0x8028002C", "TPM_E_BAD_MODE", "A mode parameter is bad, such as capArea or subCapArea for TPM_GetCapability, phsicalPresence parameter for TPM_PhysicalPresence, or migrationType for TPM_CreateMigrationBlob.");

        /// <summary>
        /// Either the physicalPresence or physicalPresenceLock bits have the wrong value.
        /// </summary>
        public static HRESULT TPM_E_BAD_PRESENCE = new HRESULT("0x8028002D", "TPM_E_BAD_PRESENCE", "Either the physicalPresence or physicalPresenceLock bits have the wrong value.");

        /// <summary>
        /// The TPM cannot perform this version of the capability.
        /// </summary>
        public static HRESULT TPM_E_BAD_VERSION = new HRESULT("0x8028002E", "TPM_E_BAD_VERSION", "The TPM cannot perform this version of the capability.");

        /// <summary>
        /// The TPM does not allow for wrapped transport sessions.
        /// </summary>
        public static HRESULT TPM_E_NO_WRAP_TRANSPORT = new HRESULT("0x8028002F", "TPM_E_NO_WRAP_TRANSPORT", "The TPM does not allow for wrapped transport sessions.");

        /// <summary>
        /// TPM audit construction failed and the underlying command was returning a failure code also.
        /// </summary>
        public static HRESULT TPM_E_AUDITFAIL_UNSUCCESSFUL = new HRESULT("0x80280030", "TPM_E_AUDITFAIL_UNSUCCESSFUL", "TPM audit construction failed and the underlying command was returning a failure code also.");

        /// <summary>
        /// TPM audit construction failed and the underlying command was returning success.
        /// </summary>
        public static HRESULT TPM_E_AUDITFAIL_SUCCESSFUL = new HRESULT("0x80280031", "TPM_E_AUDITFAIL_SUCCESSFUL", "TPM audit construction failed and the underlying command was returning success.");

        /// <summary>
        /// Attempt to reset a PCR register that does not have the resettable attribute.
        /// </summary>
        public static HRESULT TPM_E_NOTRESETABLE = new HRESULT("0x80280032", "TPM_E_NOTRESETABLE", "Attempt to reset a PCR register that does not have the resettable attribute.");

        /// <summary>
        /// Attempt to reset a PCR register that requires locality and locality modifier not part of command transport.
        /// </summary>
        public static HRESULT TPM_E_NOTLOCAL = new HRESULT("0x80280033", "TPM_E_NOTLOCAL", "Attempt to reset a PCR register that requires locality and locality modifier not part of command transport.");

        /// <summary>
        /// Make identity blob not properly typed.
        /// </summary>
        public static HRESULT TPM_E_BAD_TYPE = new HRESULT("0x80280034", "TPM_E_BAD_TYPE", "Make identity blob not properly typed.");

        /// <summary>
        /// When saving context identified resource type does not match actual resource.
        /// </summary>
        public static HRESULT TPM_E_INVALID_RESOURCE = new HRESULT("0x80280035", "TPM_E_INVALID_RESOURCE", "When saving context identified resource type does not match actual resource.");

        /// <summary>
        /// The TPM is attempting to execute a command only available when in FIPS mode.
        /// </summary>
        public static HRESULT TPM_E_NOTFIPS = new HRESULT("0x80280036", "TPM_E_NOTFIPS", "The TPM is attempting to execute a command only available when in FIPS mode.");

        /// <summary>
        /// The command is attempting to use an invalid family ID.
        /// </summary>
        public static HRESULT TPM_E_INVALID_FAMILY = new HRESULT("0x80280037", "TPM_E_INVALID_FAMILY", "The command is attempting to use an invalid family ID.");

        /// <summary>
        /// The permission to manipulate the NV storage is not available.
        /// </summary>
        public static HRESULT TPM_E_NO_NV_PERMISSION = new HRESULT("0x80280038", "TPM_E_NO_NV_PERMISSION", "The permission to manipulate the NV storage is not available.");

        /// <summary>
        /// The operation requires a signed command.
        /// </summary>
        public static HRESULT TPM_E_REQUIRES_SIGN = new HRESULT("0x80280039", "TPM_E_REQUIRES_SIGN", "The operation requires a signed command.");

        /// <summary>
        /// Wrong operation to load an NV key.
        /// </summary>
        public static HRESULT TPM_E_KEY_NOTSUPPORTED = new HRESULT("0x8028003A", "TPM_E_KEY_NOTSUPPORTED", "Wrong operation to load an NV key.");

        /// <summary>
        /// NV_LoadKey blob requires both owner and blob authorization.
        /// </summary>
        public static HRESULT TPM_E_AUTH_CONFLICT = new HRESULT("0x8028003B", "TPM_E_AUTH_CONFLICT", "NV_LoadKey blob requires both owner and blob authorization.");

        /// <summary>
        /// The NV area is locked and not writtable.
        /// </summary>
        public static HRESULT TPM_E_AREA_LOCKED = new HRESULT("0x8028003C", "TPM_E_AREA_LOCKED", "The NV area is locked and not writtable.");

        /// <summary>
        /// The locality is incorrect for the attempted operation.
        /// </summary>
        public static HRESULT TPM_E_BAD_LOCALITY = new HRESULT("0x8028003D", "TPM_E_BAD_LOCALITY", "The locality is incorrect for the attempted operation.");

        /// <summary>
        /// The NV area is read only and can't be written to.
        /// </summary>
        public static HRESULT TPM_E_READ_ONLY = new HRESULT("0x8028003E", "TPM_E_READ_ONLY", "The NV area is read only and can't be written to.");

        /// <summary>
        /// There is no protection on the write to the NV area.
        /// </summary>
        public static HRESULT TPM_E_PER_NOWRITE = new HRESULT("0x8028003F", "TPM_E_PER_NOWRITE", "There is no protection on the write to the NV area.");

        /// <summary>
        /// The family count value does not match.
        /// </summary>
        public static HRESULT TPM_E_FAMILYCOUNT = new HRESULT("0x80280040", "TPM_E_FAMILYCOUNT", "The family count value does not match.");

        /// <summary>
        /// The NV area has already been written to.
        /// </summary>
        public static HRESULT TPM_E_WRITE_LOCKED = new HRESULT("0x80280041", "TPM_E_WRITE_LOCKED", "The NV area has already been written to.");

        /// <summary>
        /// The NV area attributes conflict.
        /// </summary>
        public static HRESULT TPM_E_BAD_ATTRIBUTES = new HRESULT("0x80280042", "TPM_E_BAD_ATTRIBUTES", "The NV area attributes conflict.");

        /// <summary>
        /// The structure tag and version are invalid or inconsistent.
        /// </summary>
        public static HRESULT TPM_E_INVALID_STRUCTURE = new HRESULT("0x80280043", "TPM_E_INVALID_STRUCTURE", "The structure tag and version are invalid or inconsistent.");

        /// <summary>
        /// The key is under control of the TPM Owner and can only be evicted by the TPM Owner.
        /// </summary>
        public static HRESULT TPM_E_KEY_OWNER_CONTROL = new HRESULT("0x80280044", "TPM_E_KEY_OWNER_CONTROL", "The key is under control of the TPM Owner and can only be evicted by the TPM Owner.");

        /// <summary>
        /// The counter handle is incorrect.
        /// </summary>
        public static HRESULT TPM_E_BAD_COUNTER = new HRESULT("0x80280045", "TPM_E_BAD_COUNTER", "The counter handle is incorrect.");

        /// <summary>
        /// The write is not a complete write of the area.
        /// </summary>
        public static HRESULT TPM_E_NOT_FULLWRITE = new HRESULT("0x80280046", "TPM_E_NOT_FULLWRITE", "The write is not a complete write of the area.");

        /// <summary>
        /// The gap between saved context counts is too large.
        /// </summary>
        public static HRESULT TPM_E_CONTEXT_GAP = new HRESULT("0x80280047", "TPM_E_CONTEXT_GAP", "The gap between saved context counts is too large.");

        /// <summary>
        /// The maximum number of NV writes without an owner has been exceeded.
        /// </summary>
        public static HRESULT TPM_E_MAXNVWRITES = new HRESULT("0x80280048", "TPM_E_MAXNVWRITES", "The maximum number of NV writes without an owner has been exceeded.");

        /// <summary>
        /// No operator AuthData value is set.
        /// </summary>
        public static HRESULT TPM_E_NOOPERATOR = new HRESULT("0x80280049", "TPM_E_NOOPERATOR", "No operator AuthData value is set.");

        /// <summary>
        /// The resource pointed to by context is not loaded.
        /// </summary>
        public static HRESULT TPM_E_RESOURCEMISSING = new HRESULT("0x8028004A", "TPM_E_RESOURCEMISSING", "The resource pointed to by context is not loaded.");

        /// <summary>
        /// The delegate administration is locked.
        /// </summary>
        public static HRESULT TPM_E_DELEGATE_LOCK = new HRESULT("0x8028004B", "TPM_E_DELEGATE_LOCK", "The delegate administration is locked.");

        /// <summary>
        /// Attempt to manage a family other than the delegated family.
        /// </summary>
        public static HRESULT TPM_E_DELEGATE_FAMILY = new HRESULT("0x8028004C", "TPM_E_DELEGATE_FAMILY", "Attempt to manage a family other than the delegated family.");

        /// <summary>
        /// Delegation table management not enabled.
        /// </summary>
        public static HRESULT TPM_E_DELEGATE_ADMIN = new HRESULT("0x8028004D", "TPM_E_DELEGATE_ADMIN", "Delegation table management not enabled.");

        /// <summary>
        /// There was a command executed outside of an exclusive transport session.
        /// </summary>
        public static HRESULT TPM_E_TRANSPORT_NOTEXCLUSIVE = new HRESULT("0x8028004E", "TPM_E_TRANSPORT_NOTEXCLUSIVE", "There was a command executed outside of an exclusive transport session.");

        /// <summary>
        /// Attempt to context save a owner evict controlled key.
        /// </summary>
        public static HRESULT TPM_E_OWNER_CONTROL = new HRESULT("0x8028004F", "TPM_E_OWNER_CONTROL", "Attempt to context save a owner evict controlled key.");

        /// <summary>
        /// The DAA command has no resources availble to execute the command.
        /// </summary>
        public static HRESULT TPM_E_DAA_RESOURCES = new HRESULT("0x80280050", "TPM_E_DAA_RESOURCES", "The DAA command has no resources availble to execute the command.");

        /// <summary>
        /// The consistency check on DAA parameter inputData0 has failed.
        /// </summary>
        public static HRESULT TPM_E_DAA_INPUT_DATA0 = new HRESULT("0x80280051", "TPM_E_DAA_INPUT_DATA0", "The consistency check on DAA parameter inputData0 has failed.");

        /// <summary>
        /// The consistency check on DAA parameter inputData1 has failed.
        /// </summary>
        public static HRESULT TPM_E_DAA_INPUT_DATA1 = new HRESULT("0x80280052", "TPM_E_DAA_INPUT_DATA1", "The consistency check on DAA parameter inputData1 has failed.");

        /// <summary>
        /// The consistency check on DAA_issuerSettings has failed.
        /// </summary>
        public static HRESULT TPM_E_DAA_ISSUER_SETTINGS = new HRESULT("0x80280053", "TPM_E_DAA_ISSUER_SETTINGS", "The consistency check on DAA_issuerSettings has failed.");

        /// <summary>
        /// The consistency check on DAA_tpmSpecific has failed.
        /// </summary>
        public static HRESULT TPM_E_DAA_TPM_SETTINGS = new HRESULT("0x80280054", "TPM_E_DAA_TPM_SETTINGS", "The consistency check on DAA_tpmSpecific has failed.");

        /// <summary>
        /// The atomic process indicated by the submitted DAA command is not the expected process.
        /// </summary>
        public static HRESULT TPM_E_DAA_STAGE = new HRESULT("0x80280055", "TPM_E_DAA_STAGE", "The atomic process indicated by the submitted DAA command is not the expected process.");

        /// <summary>
        /// The issuer's validity check has detected an inconsistency.
        /// </summary>
        public static HRESULT TPM_E_DAA_ISSUER_VALIDITY = new HRESULT("0x80280056", "TPM_E_DAA_ISSUER_VALIDITY", "The issuer's validity check has detected an inconsistency.");

        /// <summary>
        /// The consistency check on w has failed.
        /// </summary>
        public static HRESULT TPM_E_DAA_WRONG_W = new HRESULT("0x80280057", "TPM_E_DAA_WRONG_W", "The consistency check on w has failed.");

        /// <summary>
        /// The handle is incorrect.
        /// </summary>
        public static HRESULT TPM_E_BAD_HANDLE = new HRESULT("0x80280058", "TPM_E_BAD_HANDLE", "The handle is incorrect.");

        /// <summary>
        /// Delegation is not correct.
        /// </summary>
        public static HRESULT TPM_E_BAD_DELEGATE = new HRESULT("0x80280059", "TPM_E_BAD_DELEGATE", "Delegation is not correct.");

        /// <summary>
        /// The context blob is invalid.
        /// </summary>
        public static HRESULT TPM_E_BADCONTEXT = new HRESULT("0x8028005A", "TPM_E_BADCONTEXT", "The context blob is invalid.");

        /// <summary>
        /// Too many contexts held by the TPM.
        /// </summary>
        public static HRESULT TPM_E_TOOMANYCONTEXTS = new HRESULT("0x8028005B", "TPM_E_TOOMANYCONTEXTS", "Too many contexts held by the TPM.");

        /// <summary>
        /// Migration authority signature validation failure.
        /// </summary>
        public static HRESULT TPM_E_MA_TICKET_SIGNATURE = new HRESULT("0x8028005C", "TPM_E_MA_TICKET_SIGNATURE", "Migration authority signature validation failure.");

        /// <summary>
        /// Migration destination not authenticated.
        /// </summary>
        public static HRESULT TPM_E_MA_DESTINATION = new HRESULT("0x8028005D", "TPM_E_MA_DESTINATION", "Migration destination not authenticated.");

        /// <summary>
        /// Migration source incorrect.
        /// </summary>
        public static HRESULT TPM_E_MA_SOURCE = new HRESULT("0x8028005E", "TPM_E_MA_SOURCE", "Migration source incorrect.");

        /// <summary>
        /// Incorrect migration authority.
        /// </summary>
        public static HRESULT TPM_E_MA_AUTHORITY = new HRESULT("0x8028005F", "TPM_E_MA_AUTHORITY", "Incorrect migration authority.");

        /// <summary>
        /// Attempt to revoke the EK and the EK is not revocable.
        /// </summary>
        public static HRESULT TPM_E_PERMANENTEK = new HRESULT("0x80280061", "TPM_E_PERMANENTEK", "Attempt to revoke the EK and the EK is not revocable.");

        /// <summary>
        /// Bad signature of CMK ticket.
        /// </summary>
        public static HRESULT TPM_E_BAD_SIGNATURE = new HRESULT("0x80280062", "TPM_E_BAD_SIGNATURE", "Bad signature of CMK ticket.");

        /// <summary>
        /// There is no room in the context list for additional contexts.
        /// </summary>
        public static HRESULT TPM_E_NOCONTEXTSPACE = new HRESULT("0x80280063", "TPM_E_NOCONTEXTSPACE", "There is no room in the context list for additional contexts.");

        /// <summary>
        /// The command was blocked.
        /// </summary>
        public static HRESULT TPM_E_COMMAND_BLOCKED = new HRESULT("0x80280400", "TPM_E_COMMAND_BLOCKED", "The command was blocked.");

        /// <summary>
        /// The specified handle was not found.
        /// </summary>
        public static HRESULT TPM_E_INVALID_HANDLE = new HRESULT("0x80280401", "TPM_E_INVALID_HANDLE", "The specified handle was not found.");

        /// <summary>
        /// The TPM returned a duplicate handle and the command needs to be resubmitted.
        /// </summary>
        public static HRESULT TPM_E_DUPLICATE_VHANDLE = new HRESULT("0x80280402", "TPM_E_DUPLICATE_VHANDLE", "The TPM returned a duplicate handle and the command needs to be resubmitted.");

        /// <summary>
        /// The command within the transport was blocked.
        /// </summary>
        public static HRESULT TPM_E_EMBEDDED_COMMAND_BLOCKED = new HRESULT("0x80280403", "TPM_E_EMBEDDED_COMMAND_BLOCKED", "The command within the transport was blocked.");

        /// <summary>
        /// The command within the transport is not supported.
        /// </summary>
        public static HRESULT TPM_E_EMBEDDED_COMMAND_UNSUPPORTED = new HRESULT("0x80280404", "TPM_E_EMBEDDED_COMMAND_UNSUPPORTED", "The command within the transport is not supported.");

        /// <summary>
        /// The TPM is too busy to respond to the command immediately, but the command could be resubmitted at a later time.
        /// </summary>
        public static HRESULT TPM_E_RETRY = new HRESULT("0x80280800", "TPM_E_RETRY", "The TPM is too busy to respond to the command immediately, but the command could be resubmitted at a later time.");

        /// <summary>
        /// SelfTestFull has not been run.
        /// </summary>
        public static HRESULT TPM_E_NEEDS_SELFTEST = new HRESULT("0x80280801", "TPM_E_NEEDS_SELFTEST", "SelfTestFull has not been run.");

        /// <summary>
        /// The TPM is currently executing a full selftest.
        /// </summary>
        public static HRESULT TPM_E_DOING_SELFTEST = new HRESULT("0x80280802", "TPM_E_DOING_SELFTEST", "The TPM is currently executing a full selftest.");

        /// <summary>
        /// The TPM is defending against dictionary attacks and is in a time-out period.
        /// </summary>
        public static HRESULT TPM_E_DEFEND_LOCK_RUNNING = new HRESULT("0x80280803", "TPM_E_DEFEND_LOCK_RUNNING", "The TPM is defending against dictionary attacks and is in a time-out period.");

        /// <summary>
        /// TPM 2.0: Inconsistent attributes.
        /// </summary>
        public static HRESULT TPM_20_E_ATTRIBUTES = new HRESULT("0x80280082", "TPM_20_E_ATTRIBUTES", "TPM 2.0: Inconsistent attributes.");

        /// <summary>
        /// TPM 2.0: Hash algorithm not supported or not appropriate.
        /// </summary>
        public static HRESULT TPM_20_E_HASH = new HRESULT("0x80280083", "TPM_20_E_HASH", "TPM 2.0: Hash algorithm not supported or not appropriate.");

        /// <summary>
        /// TPM 2.0: Value is out of range or is not correct for the context.
        /// </summary>
        public static HRESULT TPM_20_E_VALUE = new HRESULT("0x80280084", "TPM_20_E_VALUE", "TPM 2.0: Value is out of range or is not correct for the context.");

        /// <summary>
        /// TPM 2.0: Hierarchy is not enabled or is not correct for the use.
        /// </summary>
        public static HRESULT TPM_20_E_HIERARCHY = new HRESULT("0x80280085", "TPM_20_E_HIERARCHY", "TPM 2.0: Hierarchy is not enabled or is not correct for the use.");

        /// <summary>
        /// TPM 2.0: Key size is not supported.
        /// </summary>
        public static HRESULT TPM_20_E_KEY_SIZE = new HRESULT("0x80280086", "TPM_20_E_KEY_SIZE", "TPM 2.0: Key size is not supported.");

        /// <summary>
        /// TPM_20_E_MGF = TPM 2.0: Mask generation function not supported. 
        /// OR 
        /// TPM_20_E_KEY = TPM 2.0: Key fields are not compatible with the selected use.
        /// </summary>
        public static HRESULT TPM_20_E_MGF = new HRESULT("0x80280087", "TPM_20_E_MGF", "TPM_20_E_MGF = TPM 2.0: Mask generation function not supported. OR TPM_20_E_KEY = TPM 2.0: Key fields are not compatible with the selected use.");

        /// <summary>
        /// TPM 2.0: Mode of operation not supported.
        /// </summary>
        public static HRESULT TPM_20_E_MODE = new HRESULT("0x80280089", "TPM_20_E_MODE", "TPM 2.0: Mode of operation not supported.");

        /// <summary>
        /// TPM 2.0: The type of the value is not appropriate for the use.
        /// </summary>
        public static HRESULT TPM_20_E_TYPE = new HRESULT("0x8028008A", "TPM_20_E_TYPE", "TPM 2.0: The type of the value is not appropriate for the use.");

        /// <summary>
        /// TPM 2.0: The Handle is not correct for the use.
        /// </summary>
        public static HRESULT TPM_20_E_HANDLE = new HRESULT("0x8028008B", "TPM_20_E_HANDLE", "TPM 2.0: The Handle is not correct for the use.");

        /// <summary>
        /// TPM 2.0: Unsupported key derivation function or function not appropriate for use.
        /// </summary>
        public static HRESULT TPM_20_E_KDF = new HRESULT("0x8028008C", "TPM_20_E_KDF", "TPM 2.0: Unsupported key derivation function or function not appropriate for use.");

        /// <summary>
        /// TPM 2.0: Value was out of allowed range.
        /// </summary>
        public static HRESULT TPM_20_E_RANGE = new HRESULT("0x8028008D", "TPM_20_E_RANGE", "TPM 2.0: Value was out of allowed range.");

        /// <summary>
        /// TPM 2.0: The authorization HMAC check failed and DA counter incremented.
        /// </summary>
        public static HRESULT TPM_20_E_AUTH_FAIL = new HRESULT("0x8028008E", "TPM_20_E_AUTH_FAIL", "TPM 2.0: The authorization HMAC check failed and DA counter incremented.");

        /// <summary>
        /// TPM 2.0: Invalid nonce size.
        /// </summary>
        public static HRESULT TPM_20_E_NONCE = new HRESULT("0x8028008F", "TPM_20_E_NONCE", "TPM 2.0: Invalid nonce size.");

        /// <summary>
        /// TPM 2.0: Unsupported or incompatible scheme.
        /// </summary>
        public static HRESULT TPM_20_E_SCHEME = new HRESULT("0x80280092", "TPM_20_E_SCHEME", "TPM 2.0: Unsupported or incompatible scheme.");

        /// <summary>
        /// TPM 2.0: Structure is wrong size..
        /// </summary>
        public static HRESULT TPM_20_E_SIZE = new HRESULT("0x80280095", "TPM_20_E_SIZE", "TPM 2.0: Structure is wrong size..");

        /// <summary>
        /// TPM 2.0: Unsupported symmetric algorithm or key size, or not appropriate for instance.
        /// </summary>
        public static HRESULT TPM_20_E_SYMMETRIC = new HRESULT("0x80280096", "TPM_20_E_SYMMETRIC", "TPM 2.0: Unsupported symmetric algorithm or key size, or not appropriate for instance.");

        /// <summary>
        /// TPM 2.0: Incorrect structure tag.
        /// </summary>
        public static HRESULT TPM_20_E_TAG = new HRESULT("0x80280097", "TPM_20_E_TAG", "TPM 2.0: Incorrect structure tag.");

        /// <summary>
        /// TPM 2.0: Union selector is incorrect.
        /// </summary>
        public static HRESULT TPM_20_E_SELECTOR = new HRESULT("0x80280098", "TPM_20_E_SELECTOR", "TPM 2.0: Union selector is incorrect.");

        /// <summary>
        /// TPM 2.0: The TPM was unable to unmarshal a value because there were not enough octets in the input buffer.
        /// </summary>
        public static HRESULT TPM_20_E_INSUFFICIENT = new HRESULT("0x8028009A", ">TPM_20_E_INSUFFICIENT", "TPM 2.0: The TPM was unable to unmarshal a value because there were not enough octets in the input buffer.");

        /// <summary>
        /// TPM 2.0: The signature is not valid.
        /// </summary>
        public static HRESULT TPM_20_E_SIGNATURE = new HRESULT("0x8028009B", "TPM_20_E_SIGNATURE", "TPM 2.0: The signature is not valid.");

        ///// <summary>
        ///// TPM 2.0: Key fields are not compatible with the selected use.  --> ambiguous value of: TPM_20_E_MGF
        ///// </summary>
        //public static HRESULT TPM_20_E_KEY = new HRESULT("0x80280087", "TPM_20_E_KEY", "TPM 2.0: Key fields are not compatible with the selected use.");

        /// <summary>
        /// TPM 2.0: A policy check failed.
        /// </summary>
        public static HRESULT TPM_20_E_POLICY_FAIL = new HRESULT("0x8028009D", "TPM_20_E_POLICY_FAIL", "TPM 2.0: A policy check failed.");

        /// <summary>
        /// TPM 2.0: Integrity check failed.
        /// </summary>
        public static HRESULT TPM_20_E_INTEGRITY = new HRESULT("0x8028009F", "TPM_20_E_INTEGRITY", "TPM 2.0: Integrity check failed.");

        /// <summary>
        /// TPM 2.0: Invalid ticket.
        /// </summary>
        public static HRESULT TPM_20_E_TICKET = new HRESULT("0x802800A0", "TPM_20_E_TICKET", "TPM 2.0: Invalid ticket.");

        /// <summary>
        /// TPM 2.0: Reserved bits not set to zero as required.
        /// </summary>
        public static HRESULT TPM_20_E_RESERVED_BITS = new HRESULT("0x802800A1", "TPM_20_E_RESERVED_BITS", "TPM 2.0: Reserved bits not set to zero as required.");

        /// <summary>
        /// TPM 2.0: Authorization failure without DA implications.
        /// </summary>
        public static HRESULT TPM_20_E_BAD_AUTH = new HRESULT("0x802800A2", "TPM_20_E_BAD_AUTH", "TPM 2.0: Authorization failure without DA implications.");

        /// <summary>
        /// TPM 2.0: The policy has expired.
        /// </summary>
        public static HRESULT TPM_20_E_EXPIRED = new HRESULT("0x802800A3", "TPM_20_E_EXPIRED", "TPM 2.0: The policy has expired.");

        /// <summary>
        /// TPM 2.0: The command code in the policy is not the command code of the command or the command code in a policy command references a command that is not implemented.
        /// </summary>
        public static HRESULT TPM_20_E_POLICY_CC = new HRESULT("0x802800A4", "TPM_20_E_POLICY_CC", "TPM 2.0: The command code in the policy is not the command code of the command or the command code in a policy command references a command that is not implemented.");

        /// <summary>
        /// TPM 2.0: Public and sensitive portions of an object are not cryptographically bound.
        /// </summary>
        public static HRESULT TPM_20_E_BINDING = new HRESULT("0x802800A5", "TPM_20_E_BINDING", "TPM 2.0: Public and sensitive portions of an object are not cryptographically bound.");

        /// <summary>
        /// TPM 2.0: Curve not supported.
        /// </summary>
        public static HRESULT TPM_20_E_CURVE = new HRESULT("0x802800A6", "TPM_20_E_CURVE", "TPM 2.0: Curve not supported.");

        /// <summary>
        /// TPM 2.0: Point is not on the required curve.
        /// </summary>
        public static HRESULT TPM_20_E_ECC_POINT = new HRESULT("0x802800A7", "TPM_20_E_ECC_POINT", "TPM 2.0: Point is not on the required curve.");

        /// <summary>
        /// TPM 2.0: TPM not initialized.
        /// </summary>
        public static HRESULT TPM_20_E_INITIALIZE = new HRESULT("0x80280100", "TPM_20_E_INITIALIZE", "TPM 2.0: TPM not initialized.");

        /// <summary>
        /// TPM 2.0: Commands not being accepted because of a TPM failure.
        /// </summary>
        public static HRESULT TPM_20_E_FAILURE = new HRESULT("0x80280101", "TPM_20_E_FAILURE", "TPM 2.0: Commands not being accepted because of a TPM failure.");

        /// <summary>
        /// TPM 2.0: Improper use of a sequence handle.
        /// </summary>
        public static HRESULT TPM_20_E_SEQUENCE = new HRESULT("0x80280103", "TPM_20_E_SEQUENCE", "TPM 2.0: Improper use of a sequence handle.");

        /// <summary>
        /// TPM 2.0: TPM_RC_PRIVATE error.
        /// </summary>
        public static HRESULT TPM_20_E_PRIVATE = new HRESULT("0x80280010B", "TPM_20_E_PRIVATE", "TPM 2.0: TPM_RC_PRIVATE error.");

        /// <summary>
        /// TPM 2.0: TPM_RC_HMAC.
        /// </summary>
        public static HRESULT TPM_20_E_HMAC = new HRESULT("0x80280119", "TPM_20_E_HMAC", "TPM 2.0: TPM_RC_HMAC.");

        /// <summary>
        /// TPM 2.0: TPM_RC_DISABLED.
        /// </summary>
        public static HRESULT TPM_20_E_DISABLED = new HRESULT("0x80280120", "TPM_20_E_DISABLED", "TPM 2.0: TPM_RC_DISABLED.");

        /// <summary>
        /// TPM 2.0: Command failed because audit sequence required exclusivity.
        /// </summary>
        public static HRESULT TPM_20_E_EXCLUSIVE = new HRESULT("0x80280121", "TPM_20_E_EXCLUSIVE", "TPM 2.0: Command failed because audit sequence required exclusivity.");

        /// <summary>
        /// TPM 2.0: Unsupported ECC curve.
        /// </summary>
        public static HRESULT TPM_20_E_ECC_CURVE = new HRESULT("0x80280123", "TPM_20_E_ECC_CURVE", "TPM 2.0: Unsupported ECC curve.");

        /// <summary>
        /// TPM 2.0: Authorization handle is not correct for command.
        /// </summary>
        public static HRESULT TPM_20_E_AUTH_TYPE = new HRESULT("0x80280124", "TPM_20_E_AUTH_TYPE", "TPM 2.0: Authorization handle is not correct for command.");

        /// <summary>
        /// TPM 2.0: Command requires an authorization session for handle and is not present.
        /// </summary>
        public static HRESULT TPM_20_E_AUTH_MISSING = new HRESULT("0x80280125", "TPM_20_E_AUTH_MISSING", "TPM 2.0: Command requires an authorization session for handle and is not present.");

        /// <summary>
        /// TPM 2.0: Policy failure in Math Operation or an invalid authPolicy value.
        /// </summary>
        public static HRESULT TPM_20_E_POLICY = new HRESULT("0x80280126", "TPM_20_E_POLICY", "TPM 2.0: Policy failure in Math Operation or an invalid authPolicy value.");

        /// <summary>
        /// TPM 2.0: PCR check fail.
        /// </summary>
        public static HRESULT TPM_20_E_PCR = new HRESULT("0x80280127", "TPM_20_E_PCR", "TPM 2.0: PCR check fail.");

        /// <summary>
        /// TPM 2.0: PCR have changed since checked.
        /// </summary>
        public static HRESULT TPM_20_E_PCR_CHANGED = new HRESULT("0x80280128", "TPM_20_E_PCR_CHANGED", "TPM 2.0: PCR have changed since checked.");

        /// <summary>
        /// TPM 2.0: The TPM is not in the right mode for upgrade.
        /// </summary>
        public static HRESULT TPM_20_E_UPGRADE = new HRESULT("0x8028012D", "TPM_20_E_UPGRADE", "TPM 2.0: The TPM is not in the right mode for upgrade.");

        /// <summary>
        /// TPM 2.0: Context ID counter is at maximum.
        /// </summary>
        public static HRESULT TPM_20_E_TOO_MANY_CONTEXTS = new HRESULT("0x8028012E", "TPM_20_E_TOO_MANY_CONTEXTS", "TPM 2.0: Context ID counter is at maximum.");

        /// <summary>
        /// TPM 2.0: authValue or authPolicy is not available for selected entity.
        /// </summary>
        public static HRESULT TPM_20_E_AUTH_UNAVAILABLE = new HRESULT("0x8028012F", "TPM_20_E_AUTH_UNAVAILABLE", "TPM 2.0: authValue or authPolicy is not available for selected entity.");

        /// <summary>
        /// TPM 2.0: A _TPM_Init and Startup(CLEAR) is required before the TPM can resume operation.
        /// </summary>
        public static HRESULT TPM_20_E_REBOOT = new HRESULT("0x80280130", "TPM_20_E_REBOOT", "TPM 2.0: A _TPM_Init and Startup(CLEAR) is required before the TPM can resume operation.");

        /// <summary>
        /// TPM 2.0: The protection algorithms (hash and symmetric) are not reasonably balanced. The digest size of the hash must be larger than the key size of the symmetric algorithm.
        /// </summary>
        public static HRESULT TPM_20_E_UNBALANCED = new HRESULT("0x80280131", "TPM_20_E_UNBALANCED", "TPM 2.0: The protection algorithms (hash and symmetric) are not reasonably balanced. The digest size of the hash must be larger than the key size of the symmetric algorithm.");

        /// <summary>
        /// TPM 2.0: The TPM command's commandSize value is inconsistent with contents of the command buffer; either the size is not the same as the bytes loaded by the hardware interface layer or the value is not large enough to hold a command header.
        /// </summary>
        public static HRESULT TPM_20_E_COMMAND_SIZE = new HRESULT("0x80280142", "TPM_20_E_COMMAND_SIZE", "TPM 2.0: The TPM command's commandSize value is inconsistent with contents of the command buffer; either the size is not the same as the bytes loaded by the hardware interface layer or the value is not large enough to hold a command header.");

        /// <summary>
        /// TPM 2.0: Command code not supported.
        /// </summary>
        public static HRESULT TPM_20_E_COMMAND_CODE = new HRESULT("0x80280143", "TPM_20_E_COMMAND_CODE", "TPM 2.0: Command code not supported.");

        /// <summary>
        /// TPM 2.0: The value of authorizationSize is out of range or the number of octets in the authorization Area is greater than required.
        /// </summary>
        public static HRESULT TPM_20_E_AUTHSIZE = new HRESULT("0x80280144", "TPM_20_E_AUTHSIZE", "TPM 2.0: The value of authorizationSize is out of range or the number of octets in the authorization Area is greater than required.");

        /// <summary>
        /// TPM 2.0: Use of an authorization session with a context command or another command that cannot have an authorization session.
        /// </summary>
        public static HRESULT TPM_20_E_AUTH_CONTEXT = new HRESULT("0x80280145", "TPM_20_E_AUTH_CONTEXT", "TPM 2.0: Use of an authorization session with a context command or another command that cannot have an authorization session.");

        /// <summary>
        /// TPM 2.0: NV offset+size is out of range.
        /// </summary>
        public static HRESULT TPM_20_E_NV_RANGE = new HRESULT("0x80280146", "TPM_20_E_NV_RANGE", "TPM 2.0: NV offset+size is out of range.");

        /// <summary>
        /// TPM 2.0: Requested allocation size is larger than allowed.
        /// </summary>
        public static HRESULT TPM_20_E_NV_SIZE = new HRESULT("0x80280147", "TPM_20_E_NV_SIZE", "TPM 2.0: Requested allocation size is larger than allowed.");

        /// <summary>
        /// TPM 2.0: NV access locked.
        /// </summary>
        public static HRESULT TPM_20_E_NV_LOCKED = new HRESULT("0x80280148", "TPM_20_E_NV_LOCKED", "TPM 2.0: NV access locked.");

        /// <summary>
        /// TPM 2.0: NV access authorization fails in command actions
        /// </summary>
        public static HRESULT TPM_20_E_NV_AUTHORIZATION = new HRESULT("0x80280149", "TPM_20_E_NV_AUTHORIZATION", "TPM 2.0: NV access authorization fails in command actions");

        /// <summary>
        /// TPM 2.0: An NV index is used before being initialized or the state saved by TPM2_Shutdown(STATE) could not be restored.
        /// </summary>
        public static HRESULT TPM_20_E_NV_UNINITIALIZED = new HRESULT("0x8028014A", "TPM_20_E_NV_UNINITIALIZED", "TPM 2.0: An NV index is used before being initialized or the state saved by TPM2_Shutdown(STATE) could not be restored.");

        /// <summary>
        /// TPM 2.0: Insufficient space for NV allocation.
        /// </summary>
        public static HRESULT TPM_20_E_NV_SPACE = new HRESULT("0x8028014B", "TPM_20_E_NV_SPACE", "TPM 2.0: Insufficient space for NV allocation.");

        /// <summary>
        /// TPM 2.0: NV index or persistent object already defined.
        /// </summary>
        public static HRESULT TPM_20_E_NV_DEFINED = new HRESULT("0x8028014C", "TPM_20_E_NV_DEFINED", "TPM 2.0: NV index or persistent object already defined.");

        /// <summary>
        /// TPM 2.0: Context in TPM2_ContextLoad() is not valid.
        /// </summary>
        public static HRESULT TPM_20_E_BAD_CONTEXT = new HRESULT("0x80280150", "TPM_20_E_BAD_CONTEXT", "TPM 2.0: Context in TPM2_ContextLoad() is not valid.");

        /// <summary>
        /// TPM 2.0: chHash value already set or not correct for use.
        /// </summary>
        public static HRESULT TPM_20_E_CPHASH = new HRESULT("0x80280151", "TPM_20_E_CPHASH", "TPM 2.0: chHash value already set or not correct for use.");

        /// <summary>
        /// TPM 2.0: Handle for parent is not a valid parent.
        /// </summary>
        public static HRESULT TPM_20_E_PARENT = new HRESULT("0x80280152", "TPM_20_E_PARENT", "TPM 2.0: Handle for parent is not a valid parent.");

        /// <summary>
        /// TPM 2.0: Some function needs testing.
        /// </summary>
        public static HRESULT TPM_20_E_NEEDS_TEST = new HRESULT("0x80280153", "TPM_20_E_NEEDS_TEST", "TPM 2.0: Some function needs testing.");

        /// <summary>
        /// TPM 2.0: returned when an internal function cannot process a request due to an unspecified problem. This code is usually related to invalid parameters that are not properly filtered by the input unmarshaling code.
        /// </summary>
        public static HRESULT TPM_20_E_NO_RESULT = new HRESULT("0x80280154", "TPM_20_E_NO_RESULT", "TPM 2.0: returned when an internal function cannot process a request due to an unspecified problem. This code is usually related to invalid parameters that are not properly filtered by the input unmarshaling code.");

        /// <summary>
        /// TPM 2.0: The sensitive area did not unmarshal correctly after decryption - this code is used in lieu of the other unmarshaling errors so that an attacker cannot determine where the unmarshaling error occurred.
        /// </summary>
        public static HRESULT TPM_20_E_SENSITIVE = new HRESULT("0x80280155", "TPM_20_E_SENSITIVE", "TPM 2.0: The sensitive area did not unmarshal correctly after decryption - this code is used in lieu of the other unmarshaling errors so that an attacker cannot determine where the unmarshaling error occurred.");

        /// <summary>
        /// TPM 2.0: Gap for context ID is too large.
        /// </summary>
        public static HRESULT TPM_20_E_CONTEXT_GAP = new HRESULT("0x80280901", "TPM_20_E_CONTEXT_GAP", "TPM 2.0: Gap for context ID is too large.");

        /// <summary>
        /// TPM 2.0: Out of memory for object contexts.
        /// </summary>
        public static HRESULT TPM_20_E_OBJECT_MEMORY = new HRESULT("0x80280902", "TPM_20_E_OBJECT_MEMORY", "TPM 2.0: Out of memory for object contexts.");

        /// <summary>
        /// TPM 2.0: Out of memory for session contexts.
        /// </summary>
        public static HRESULT TPM_20_E_SESSION_MEMORY = new HRESULT("0x80280903", "TPM_20_E_SESSION_MEMORY", "TPM 2.0: Out of memory for session contexts.");

        /// <summary>
        /// TPM 2.0: Out of shared object/session memory or need space for internal operations.
        /// </summary>
        public static HRESULT TPM_20_E_MEMORY = new HRESULT("0x80280904", "TPM_20_E_MEMORY", "TPM 2.0: Out of shared object/session memory or need space for internal operations.");

        /// <summary>
        /// TPM 2.0: Out of session handles - a session must be flushed before a new session may be created.
        /// </summary>
        public static HRESULT TPM_20_E_SESSION_HANDLES = new HRESULT("0x80280905", "TPM_20_E_SESSION_HANDLES", "TPM 2.0: Out of session handles - a session must be flushed before a new session may be created.");

        /// <summary>
        /// TPM 2.0: Out of object handles - the handle space for objects is depleted and a reboot is required.
        /// </summary>
        public static HRESULT TPM_20_E_OBJECT_HANDLES = new HRESULT("0x80280906", "TPM_20_E_OBJECT_HANDLES", "TPM 2.0: Out of object handles - the handle space for objects is depleted and a reboot is required.");

        /// <summary>
        /// TPM 2.0: Bad locality.
        /// </summary>
        public static HRESULT TPM_20_E_LOCALITY = new HRESULT("0x80280907", "TPM_20_E_LOCALITY", "TPM 2.0: Bad locality.");

        /// <summary>
        /// TPM 2.0: The TPM has suspended operation on the command; forward progress was made and the command may be retried.
        /// </summary>
        public static HRESULT TPM_20_E_YIELDED = new HRESULT("0x80280908", "TPM_20_E_YIELDED", "TPM 2.0: The TPM has suspended operation on the command; forward progress was made and the command may be retried.");

        /// <summary>
        /// TPM 2.0: The command was canceled.
        /// </summary>
        public static HRESULT TPM_20_E_CANCELED = new HRESULT("0x80280909", "TPM_20_E_CANCELED", "TPM 2.0: The command was canceled.");

        /// <summary>
        /// TPM 2.0: TPM is performing self-tests.
        /// </summary>
        public static HRESULT TPM_20_E_TESTING = new HRESULT("0x8028090A", "TPM_20_E_TESTING", "TPM 2.0: TPM is performing self-tests.");

        /// <summary>
        /// TPM 2.0: The TPM is rate-limiting accesses to prevent wearout of NV.
        /// </summary>
        public static HRESULT TPM_20_E_NV_RATE = new HRESULT("0x80280920", "TPM_20_E_NV_RATE", "TPM 2.0: The TPM is rate-limiting accesses to prevent wearout of NV.");

        /// <summary>
        /// TPM 2.0: Authorization for objects subject to DA protection are not allowed at this time because the TPM is in DA lockout mode.
        /// </summary>
        public static HRESULT TPM_20_E_LOCKOUT = new HRESULT("0x80280921", "TPM_20_E_LOCKOUT", "TPM 2.0: Authorization for objects subject to DA protection are not allowed at this time because the TPM is in DA lockout mode.");

        /// <summary>
        /// TPM 2.0: The TPM was not able to start the command.
        /// </summary>
        public static HRESULT TPM_20_E_RETRY = new HRESULT("0x80280922", "TPM_20_E_RETRY", "TPM 2.0: The TPM was not able to start the command.");

        /// <summary>
        /// TPM 2.0: the command may require writing of NV and NV is not current accessible..
        /// </summary>
        public static HRESULT TPM_20_E_NV_UNAVAILABLE = new HRESULT("0x80280923", "TPM_20_E_NV_UNAVAILABLE", "TPM 2.0: the command may require writing of NV and NV is not current accessible..");

        /// <summary>
        /// An internal software error has been detected.
        /// </summary>
        public static HRESULT TBS_E_INTERNAL_ERROR = new HRESULT("0x80284001", "TBS_E_INTERNAL_ERROR", "An internal software error has been detected.");

        /// <summary>
        /// One or more input parameters is bad.
        /// </summary>
        public static HRESULT TBS_E_BAD_PARAMETER = new HRESULT("0x80284002", "TBS_E_BAD_PARAMETER", "One or more input parameters is bad.");

        /// <summary>
        /// A specified output pointer is bad.
        /// </summary>
        public static HRESULT TBS_E_INVALID_OUTPUT_POINTER = new HRESULT("0x80284003", "TBS_E_INVALID_OUTPUT_POINTER", "A specified output pointer is bad.");

        /// <summary>
        /// The specified context handle does not refer to a valid context.
        /// </summary>
        public static HRESULT TBS_E_INVALID_CONTEXT = new HRESULT("0x80284004", "TBS_E_INVALID_CONTEXT", "The specified context handle does not refer to a valid context.");

        /// <summary>
        /// A specified output buffer is too small.
        /// </summary>
        public static HRESULT TBS_E_INSUFFICIENT_BUFFER = new HRESULT("0x80284005", "TBS_E_INSUFFICIENT_BUFFER", "A specified output buffer is too small.");

        /// <summary>
        /// An error occurred while communicating with the TPM.
        /// </summary>
        public static HRESULT TBS_E_IOERROR = new HRESULT("0x80284006", "TBS_E_IOERROR", "An error occurred while communicating with the TPM.");

        /// <summary>
        /// One or more context parameters is invalid.
        /// </summary>
        public static HRESULT TBS_E_INVALID_CONTEXT_PARAM = new HRESULT("0x80284007", "TBS_E_INVALID_CONTEXT_PARAM", "One or more context parameters is invalid.");

        /// <summary>
        /// The TBS service is not running and could not be started.
        /// </summary>
        public static HRESULT TBS_E_SERVICE_NOT_RUNNING = new HRESULT("0x80284008", "TBS_E_SERVICE_NOT_RUNNING", "The TBS service is not running and could not be started.");

        /// <summary>
        /// A new context could not be created because there are too many open contexts.
        /// </summary>
        public static HRESULT TBS_E_TOO_MANY_TBS_CONTEXTS = new HRESULT("0x80284009", "TBS_E_TOO_MANY_TBS_CONTEXTS", "A new context could not be created because there are too many open contexts.");

        /// <summary>
        /// A new virtual resource could not be created because there are too many open virtual resources.
        /// </summary>
        public static HRESULT TBS_E_TOO_MANY_RESOURCES = new HRESULT("0x8028400A", "TBS_E_TOO_MANY_RESOURCES", "A new virtual resource could not be created because there are too many open virtual resources.");

        /// <summary>
        /// The TBS service has been started but is not yet running.
        /// </summary>
        public static HRESULT TBS_E_SERVICE_START_PENDING = new HRESULT("0x8028400B", "TBS_E_SERVICE_START_PENDING", "The TBS service has been started but is not yet running.");

        /// <summary>
        /// The physical presence interface is not supported.
        /// </summary>
        public static HRESULT TBS_E_PPI_NOT_SUPPORTED = new HRESULT("0x8028400C", "TBS_E_PPI_NOT_SUPPORTED", "The physical presence interface is not supported.");

        /// <summary>
        /// The command was canceled.
        /// </summary>
        public static HRESULT TBS_E_COMMAND_CANCELED = new HRESULT("0x8028400D", "TBS_E_COMMAND_CANCELED", "The command was canceled.");

        /// <summary>
        /// The input or output buffer is too large.
        /// </summary>
        public static HRESULT TBS_E_BUFFER_TOO_LARGE = new HRESULT("0x8028400E", "TBS_E_BUFFER_TOO_LARGE", "The input or output buffer is too large.");

        /// <summary>
        /// A compatible Trusted Platform Module (TPM) Security Device cannot be found on this computer.
        /// </summary>
        public static HRESULT TBS_E_TPM_NOT_FOUND = new HRESULT("0x8028400F", "TBS_E_TPM_NOT_FOUND", "A compatible Trusted Platform Module (TPM) Security Device cannot be found on this computer.");

        /// <summary>
        /// The TBS service has been disabled.
        /// </summary>
        public static HRESULT TBS_E_SERVICE_DISABLED = new HRESULT("0x80284010", "TBS_E_SERVICE_DISABLED", "The TBS service has been disabled.");

        /// <summary>
        /// No TCG event log is available.
        /// </summary>
        public static HRESULT TBS_E_NO_EVENT_LOG = new HRESULT("0x80284011", "TBS_E_NO_EVENT_LOG", "No TCG event log is available.");

        /// <summary>
        /// The caller does not have the appropriate rights to perform the requested operation.
        /// </summary>
        public static HRESULT TBS_E_ACCESS_DENIED = new HRESULT("0x80284012", "TBS_E_ACCESS_DENIED", "The caller does not have the appropriate rights to perform the requested operation.");

        /// <summary>
        /// The TPM provisioning action is not allowed by the specified flags. For provisioning to be successful, one of several actions may be required. The TPM management console (tpm.msc) action to make the TPM Ready may help. For further information, see the documentation for the Win32_Tpm WMI method 'Provision'. (The actions that may be required include importing the TPM Owner Authorization value into the system, calling the Win32_Tpm WMI method for provisioning the TPM and specifying TRUE for either 'ForceClear_Allowed' or 'PhysicalPresencePrompts_Allowed' (as indicated by the value returned in the Additional Information), or enabling the TPM in the system BIOS.)
        /// </summary>
        public static HRESULT TBS_E_PROVISIONING_NOT_ALLOWED = new HRESULT("0x80284013", "TBS_E_PROVISIONING_NOT_ALLOWED", "The TPM provisioning action is not allowed by the specified flags. For provisioning to be successful, one of several actions may be required. The TPM management console (tpm.msc) action to make the TPM Ready may help. For further information, see the documentation for the Win32_Tpm WMI method 'Provision'. (The actions that may be required include importing the TPM Owner Authorization value into the system, calling the Win32_Tpm WMI method for provisioning the TPM and specifying TRUE for either 'ForceClear_Allowed' or 'PhysicalPresencePrompts_Allowed' (as indicated by the value returned in the Additional Information), or enabling the TPM in the system BIOS.)");

        /// <summary>
        /// The Physical Presence Interface of this firmware does not support the requested method.
        /// </summary>
        public static HRESULT TBS_E_PPI_FUNCTION_UNSUPPORTED = new HRESULT("0x80284014", "TBS_E_PPI_FUNCTION_UNSUPPORTED", "The Physical Presence Interface of this firmware does not support the requested method.");

        /// <summary>
        /// The requested TPM OwnerAuth value was not found.
        /// </summary>
        public static HRESULT TBS_E_OWNERAUTH_NOT_FOUND = new HRESULT("0x80284015", "TBS_E_OWNERAUTH_NOT_FOUND", "The requested TPM OwnerAuth value was not found.");

        /// <summary>
        /// The TPM provisioning did not complete. For more information on completing the provisioning, call the Win32_Tpm WMI method for provisioning the TPM ('Provision') and check the returned Information.
        /// </summary>
        public static HRESULT TBS_E_PROVISIONING_INCOMPLETE = new HRESULT("0x80284016", "TBS_E_PROVISIONING_INCOMPLETE", "The TPM provisioning did not complete. For more information on completing the provisioning, call the Win32_Tpm WMI method for provisioning the TPM ('Provision') and check the returned Information.");

        /// <summary>
        /// The command buffer is not in the correct state.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_STATE = new HRESULT("0x80290100", "TPMAPI_E_INVALID_STATE", "The command buffer is not in the correct state.");

        /// <summary>
        /// The command buffer does not contain enough data to satisfy the request.
        /// </summary>
        public static HRESULT TPMAPI_E_NOT_ENOUGH_DATA = new HRESULT("0x80290101", "TPMAPI_E_NOT_ENOUGH_DATA", "The command buffer does not contain enough data to satisfy the request.");

        /// <summary>
        /// The command buffer cannot contain any more data.
        /// </summary>
        public static HRESULT TPMAPI_E_TOO_MUCH_DATA = new HRESULT("0x80290102", "TPMAPI_E_TOO_MUCH_DATA", "The command buffer cannot contain any more data.");

        /// <summary>
        /// One or more output parameters was NULL or invalid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_OUTPUT_POINTER = new HRESULT("0x80290103", "TPMAPI_E_INVALID_OUTPUT_POINTER", "One or more output parameters was NULL or invalid.");

        /// <summary>
        /// One or more input parameters is invalid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_PARAMETER = new HRESULT("0x80290104", "TPMAPI_E_INVALID_PARAMETER", "One or more input parameters is invalid.");

        /// <summary>
        /// Not enough memory was available to satisfy the request.
        /// </summary>
        public static HRESULT TPMAPI_E_OUT_OF_MEMORY = new HRESULT("0x80290105", "TPMAPI_E_OUT_OF_MEMORY", "Not enough memory was available to satisfy the request.");

        /// <summary>
        /// The specified buffer was too small.
        /// </summary>
        public static HRESULT TPMAPI_E_BUFFER_TOO_SMALL = new HRESULT("0x80290106", "TPMAPI_E_BUFFER_TOO_SMALL", "The specified buffer was too small.");

        /// <summary>
        /// An internal error was detected.
        /// </summary>
        public static HRESULT TPMAPI_E_INTERNAL_ERROR = new HRESULT("0x80290107", "TPMAPI_E_INTERNAL_ERROR", "An internal error was detected.");

        /// <summary>
        /// The caller does not have the appropriate rights to perform the requested operation.
        /// </summary>
        public static HRESULT TPMAPI_E_ACCESS_DENIED = new HRESULT("0x80290108", "TPMAPI_E_ACCESS_DENIED", "The caller does not have the appropriate rights to perform the requested operation.");

        /// <summary>
        /// The specified authorization information was invalid.
        /// </summary>
        public static HRESULT TPMAPI_E_AUTHORIZATION_FAILED = new HRESULT("0x80290109", "TPMAPI_E_AUTHORIZATION_FAILED", "The specified authorization information was invalid.");

        /// <summary>
        /// The specified context handle was not valid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_CONTEXT_HANDLE = new HRESULT("0x8029010A", "TPMAPI_E_INVALID_CONTEXT_HANDLE", "The specified context handle was not valid.");

        /// <summary>
        /// An error occurred while communicating with the TBS.
        /// </summary>
        public static HRESULT TPMAPI_E_TBS_COMMUNICATION_ERROR = new HRESULT("0x8029010B", "TPMAPI_E_TBS_COMMUNICATION_ERROR", "An error occurred while communicating with the TBS.");

        /// <summary>
        /// The TPM returned an unexpected result.
        /// </summary>
        public static HRESULT TPMAPI_E_TPM_COMMAND_ERROR = new HRESULT("0x8029010C", "TPMAPI_E_TPM_COMMAND_ERROR", "The TPM returned an unexpected result.");

        /// <summary>
        /// The message was too large for the encoding scheme.
        /// </summary>
        public static HRESULT TPMAPI_E_MESSAGE_TOO_LARGE = new HRESULT("0x8029010D", "TPMAPI_E_MESSAGE_TOO_LARGE", "The message was too large for the encoding scheme.");

        /// <summary>
        /// The encoding in the blob was not recognized.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_ENCODING = new HRESULT("0x8029010E", "TPMAPI_E_INVALID_ENCODING", "The encoding in the blob was not recognized.");

        /// <summary>
        /// The key size is not valid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_KEY_SIZE = new HRESULT("0x8029010F", "TPMAPI_E_INVALID_KEY_SIZE", "The key size is not valid.");

        /// <summary>
        /// The encryption operation failed.
        /// </summary>
        public static HRESULT TPMAPI_E_ENCRYPTION_FAILED = new HRESULT("0x80290110", "TPMAPI_E_ENCRYPTION_FAILED", "The encryption operation failed.");

        /// <summary>
        /// The key parameters structure was not valid
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_KEY_PARAMS = new HRESULT("0x80290111", "TPMAPI_E_INVALID_KEY_PARAMS", "The key parameters structure was not valid");

        /// <summary>
        /// The requested supplied data does not appear to be a valid migration authorization blob.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_MIGRATION_AUTHORIZATION_BLOB = new HRESULT("0x80290112", "TPMAPI_E_INVALID_MIGRATION_AUTHORIZATION_BLOB", "The requested supplied data does not appear to be a valid migration authorization blob.");

        /// <summary>
        /// The specified PCR index was invalid
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_PCR_INDEX = new HRESULT("0x80290113", "TPMAPI_E_INVALID_PCR_INDEX", "The specified PCR index was invalid");

        /// <summary>
        /// The data given does not appear to be a valid delegate blob.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_DELEGATE_BLOB = new HRESULT("0x80290114", "TPMAPI_E_INVALID_DELEGATE_BLOB", "The data given does not appear to be a valid delegate blob.");

        /// <summary>
        /// One or more of the specified context parameters was not valid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_CONTEXT_PARAMS = new HRESULT("0x80290115", "TPMAPI_E_INVALID_CONTEXT_PARAMS", "One or more of the specified context parameters was not valid.");

        /// <summary>
        /// The data given does not appear to be a valid key blob
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_KEY_BLOB = new HRESULT("0x80290116", "TPMAPI_E_INVALID_KEY_BLOB", "The data given does not appear to be a valid key blob");

        /// <summary>
        /// The specified PCR data was invalid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_PCR_DATA = new HRESULT("0x80290117", "TPMAPI_E_INVALID_PCR_DATA", "The specified PCR data was invalid.");

        /// <summary>
        /// The format of the owner auth data was invalid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_OWNER_AUTH = new HRESULT("0x80290118", "TPMAPI_E_INVALID_OWNER_AUTH", "The format of the owner auth data was invalid.");

        /// <summary>
        /// The random number generated did not pass FIPS RNG check.
        /// </summary>
        public static HRESULT TPMAPI_E_FIPS_RNG_CHECK_FAILED = new HRESULT("0x80290119", "TPMAPI_E_FIPS_RNG_CHECK_FAILED", "The random number generated did not pass FIPS RNG check.");

        /// <summary>
        /// The TCG Event Log does not contain any data.
        /// </summary>
        public static HRESULT TPMAPI_E_EMPTY_TCG_LOG = new HRESULT("0x8029011A", "TPMAPI_E_EMPTY_TCG_LOG", "The TCG Event Log does not contain any data.");

        /// <summary>
        /// An entry in the TCG Event Log was invalid.
        /// </summary>
        public static HRESULT TPMAPI_E_INVALID_TCG_LOG_ENTRY = new HRESULT("0x8029011B", "TPMAPI_E_INVALID_TCG_LOG_ENTRY", "An entry in the TCG Event Log was invalid.");

        /// <summary>
        /// A TCG Separator was not found.
        /// </summary>
        public static HRESULT TPMAPI_E_TCG_SEPARATOR_ABSENT = new HRESULT("0x8029011C", "TPMAPI_E_TCG_SEPARATOR_ABSENT", "A TCG Separator was not found.");

        /// <summary>
        /// A digest value in a TCG Log entry did not match hashed data.
        /// </summary>
        public static HRESULT TPMAPI_E_TCG_INVALID_DIGEST_ENTRY = new HRESULT("0x8029011D", "TPMAPI_E_TCG_INVALID_DIGEST_ENTRY", "A digest value in a TCG Log entry did not match hashed data.");

        /// <summary>
        /// The requested operation was blocked by current TPM policy. Please contact your system administrator for assistance.
        /// </summary>
        public static HRESULT TPMAPI_E_POLICY_DENIES_OPERATION = new HRESULT("0x8029011E", "TPMAPI_E_POLICY_DENIES_OPERATION", "The requested operation was blocked by current TPM policy. Please contact your system administrator for assistance.");

        /// <summary>
        /// The specified buffer was too small.
        /// </summary>
        public static HRESULT TBSIMP_E_BUFFER_TOO_SMALL = new HRESULT("0x80290200", "TBSIMP_E_BUFFER_TOO_SMALL", "The specified buffer was too small.");

        /// <summary>
        /// The context could not be cleaned up.
        /// </summary>
        public static HRESULT TBSIMP_E_CLEANUP_FAILED = new HRESULT("0x80290201", "TBSIMP_E_CLEANUP_FAILED", "The context could not be cleaned up.");

        /// <summary>
        /// The specified context handle is invalid.
        /// </summary>
        public static HRESULT TBSIMP_E_INVALID_CONTEXT_HANDLE = new HRESULT("0x80290202", "TBSIMP_E_INVALID_CONTEXT_HANDLE", "The specified context handle is invalid.");

        /// <summary>
        /// An invalid context parameter was specified.
        /// </summary>
        public static HRESULT TBSIMP_E_INVALID_CONTEXT_PARAM = new HRESULT("0x80290203", "TBSIMP_E_INVALID_CONTEXT_PARAM", "An invalid context parameter was specified.");

        /// <summary>
        /// An error occurred while communicating with the TPM
        /// </summary>
        public static HRESULT TBSIMP_E_TPM_ERROR = new HRESULT("0x80290204", "TBSIMP_E_TPM_ERROR", "An error occurred while communicating with the TPM");

        /// <summary>
        /// No entry with the specified key was found.
        /// </summary>
        public static HRESULT TBSIMP_E_HASH_BAD_KEY = new HRESULT("0x80290205", "TBSIMP_E_HASH_BAD_KEY", "No entry with the specified key was found.");

        /// <summary>
        /// The specified virtual handle matches a virtual handle already in use.
        /// </summary>
        public static HRESULT TBSIMP_E_DUPLICATE_VHANDLE = new HRESULT("0x80290206", "TBSIMP_E_DUPLICATE_VHANDLE", "The specified virtual handle matches a virtual handle already in use.");

        /// <summary>
        /// The pointer to the returned handle location was NULL or invalid
        /// </summary>
        public static HRESULT TBSIMP_E_INVALID_OUTPUT_POINTER = new HRESULT("0x80290207", "TBSIMP_E_INVALID_OUTPUT_POINTER", "The pointer to the returned handle location was NULL or invalid");

        /// <summary>
        /// One or more parameters is invalid
        /// </summary>
        public static HRESULT TBSIMP_E_INVALID_PARAMETER = new HRESULT("0x80290208", "TBSIMP_E_INVALID_PARAMETER", "One or more parameters is invalid");

        /// <summary>
        /// The RPC subsystem could not be initialized.
        /// </summary>
        public static HRESULT TBSIMP_E_RPC_INIT_FAILED = new HRESULT("0x80290209", "TBSIMP_E_RPC_INIT_FAILED", "The RPC subsystem could not be initialized.");

        /// <summary>
        /// The TBS scheduler is not running.
        /// </summary>
        public static HRESULT TBSIMP_E_SCHEDULER_NOT_RUNNING = new HRESULT("0x8029020A", "TBSIMP_E_SCHEDULER_NOT_RUNNING", "The TBS scheduler is not running.");

        /// <summary>
        /// The command was canceled.
        /// </summary>
        public static HRESULT TBSIMP_E_COMMAND_CANCELED = new HRESULT("0x8029020B", "TBSIMP_E_COMMAND_CANCELED", "The command was canceled.");

        /// <summary>
        /// There was not enough memory to fulfill the request
        /// </summary>
        public static HRESULT TBSIMP_E_OUT_OF_MEMORY = new HRESULT("0x8029020C", "TBSIMP_E_OUT_OF_MEMORY", "There was not enough memory to fulfill the request");

        /// <summary>
        /// The specified list is empty, or the iteration has reached the end of the list.
        /// </summary>
        public static HRESULT TBSIMP_E_LIST_NO_MORE_ITEMS = new HRESULT("0x8029020D", "TBSIMP_E_LIST_NO_MORE_ITEMS", "The specified list is empty, or the iteration has reached the end of the list.");

        /// <summary>
        /// The specified item was not found in the list.
        /// </summary>
        public static HRESULT TBSIMP_E_LIST_NOT_FOUND = new HRESULT("0x8029020E", "TBSIMP_E_LIST_NOT_FOUND", "The specified item was not found in the list.");

        /// <summary>
        /// The TPM does not have enough space to load the requested resource.
        /// </summary>
        public static HRESULT TBSIMP_E_NOT_ENOUGH_SPACE = new HRESULT("0x8029020F", "TBSIMP_E_NOT_ENOUGH_SPACE", "The TPM does not have enough space to load the requested resource.");

        /// <summary>
        /// There are too many TPM contexts in use.
        /// </summary>
        public static HRESULT TBSIMP_E_NOT_ENOUGH_TPM_CONTEXTS = new HRESULT("0x80290210", "TBSIMP_E_NOT_ENOUGH_TPM_CONTEXTS", "There are too many TPM contexts in use.");

        /// <summary>
        /// The TPM command failed.
        /// </summary>
        public static HRESULT TBSIMP_E_COMMAND_FAILED = new HRESULT("0x80290211", "TBSIMP_E_COMMAND_FAILED", "The TPM command failed.");

        /// <summary>
        /// The TBS does not recognize the specified ordinal.
        /// </summary>
        public static HRESULT TBSIMP_E_UNKNOWN_ORDINAL = new HRESULT("0x80290212", "TBSIMP_E_UNKNOWN_ORDINAL", "The TBS does not recognize the specified ordinal.");

        /// <summary>
        /// The requested resource is no longer available.
        /// </summary>
        public static HRESULT TBSIMP_E_RESOURCE_EXPIRED = new HRESULT("0x80290213", "TBSIMP_E_RESOURCE_EXPIRED", "The requested resource is no longer available.");

        /// <summary>
        /// The resource type did not match.
        /// </summary>
        public static HRESULT TBSIMP_E_INVALID_RESOURCE = new HRESULT("0x80290214", "TBSIMP_E_INVALID_RESOURCE", "The resource type did not match.");

        /// <summary>
        /// No resources can be unloaded.
        /// </summary>
        public static HRESULT TBSIMP_E_NOTHING_TO_UNLOAD = new HRESULT("0x80290215", "TBSIMP_E_NOTHING_TO_UNLOAD", "No resources can be unloaded.");

        /// <summary>
        /// No new entries can be added to the hash table.
        /// </summary>
        public static HRESULT TBSIMP_E_HASH_TABLE_FULL = new HRESULT("0x80290216", "TBSIMP_E_HASH_TABLE_FULL", "No new entries can be added to the hash table.");

        /// <summary>
        /// A new TBS context could not be created because there are too many open contexts.
        /// </summary>
        public static HRESULT TBSIMP_E_TOO_MANY_TBS_CONTEXTS = new HRESULT("0x80290217", "TBSIMP_E_TOO_MANY_TBS_CONTEXTS", "A new TBS context could not be created because there are too many open contexts.");

        /// <summary>
        /// A new virtual resource could not be created because there are too many open virtual resources.
        /// </summary>
        public static HRESULT TBSIMP_E_TOO_MANY_RESOURCES = new HRESULT("0x80290218", "TBSIMP_E_TOO_MANY_RESOURCES", "A new virtual resource could not be created because there are too many open virtual resources.");

        /// <summary>
        /// The physical presence interface is not supported.
        /// </summary>
        public static HRESULT TBSIMP_E_PPI_NOT_SUPPORTED = new HRESULT("0x80290219", "TBSIMP_E_PPI_NOT_SUPPORTED", "The physical presence interface is not supported.");

        /// <summary>
        /// TBS is not compatible with the version of TPM found on the system.
        /// </summary>
        public static HRESULT TBSIMP_E_TPM_INCOMPATIBLE = new HRESULT("0x8029021A", "TBSIMP_E_TPM_INCOMPATIBLE", "TBS is not compatible with the version of TPM found on the system.");

        /// <summary>
        /// No TCG event log is available.
        /// </summary>
        public static HRESULT TBSIMP_E_NO_EVENT_LOG = new HRESULT("0x8029021B", "TBSIMP_E_NO_EVENT_LOG", "No TCG event log is available.");

        /// <summary>
        /// A general error was detected when attempting to acquire the BIOS's response to a Physical Presence command.
        /// </summary>
        public static HRESULT TPM_E_PPI_ACPI_FAILURE = new HRESULT("0x80290300", "TPM_E_PPI_ACPI_FAILURE", "A general error was detected when attempting to acquire the BIOS's response to a Physical Presence command.");

        /// <summary>
        /// The user failed to confirm the TPM operation request.
        /// </summary>
        public static HRESULT TPM_E_PPI_USER_ABORT = new HRESULT("0x80290301", "TPM_E_PPI_USER_ABORT", "The user failed to confirm the TPM operation request.");

        /// <summary>
        /// The BIOS failure prevented the successful execution of the requested TPM operation (e.g. invalid TPM operation request, BIOS communication error with the TPM).
        /// </summary>
        public static HRESULT TPM_E_PPI_BIOS_FAILURE = new HRESULT("0x80290302", "TPM_E_PPI_BIOS_FAILURE", "The BIOS failure prevented the successful execution of the requested TPM operation (e.g. invalid TPM operation request, BIOS communication error with the TPM).");

        /// <summary>
        /// The BIOS does not support the physical presence interface.
        /// </summary>
        public static HRESULT TPM_E_PPI_NOT_SUPPORTED = new HRESULT("0x80290303", "TPM_E_PPI_NOT_SUPPORTED", "The BIOS does not support the physical presence interface.");

        /// <summary>
        /// The Physical Presence command was blocked by current BIOS settings. The system owner may be able to reconfigure the BIOS settings to allow the command.
        /// </summary>
        public static HRESULT TPM_E_PPI_BLOCKED_IN_BIOS = new HRESULT("0x80290304", "TPM_E_PPI_BLOCKED_IN_BIOS", "The Physical Presence command was blocked by current BIOS settings. The system owner may be able to reconfigure the BIOS settings to allow the command.");

        /// <summary>
        /// This is an error mask to convert Platform Crypto Provider errors to win errors.
        /// </summary>
        public static HRESULT TPM_E_PCP_ERROR_MASK = new HRESULT("0x80290400", "TPM_E_PCP_ERROR_MASK", "This is an error mask to convert Platform Crypto Provider errors to win errors.");

        /// <summary>
        /// The Platform Crypto Device is currently not ready. It needs to be fully provisioned to be operational.
        /// </summary>
        public static HRESULT TPM_E_PCP_DEVICE_NOT_READY = new HRESULT("0x80290401", "TPM_E_PCP_DEVICE_NOT_READY", "The Platform Crypto Device is currently not ready. It needs to be fully provisioned to be operational.");

        /// <summary>
        /// The handle provided to the Platform Crypto Provider is invalid.
        /// </summary>
        public static HRESULT TPM_E_PCP_INVALID_HANDLE = new HRESULT("0x80290402", "TPM_E_PCP_INVALID_HANDLE", "The handle provided to the Platform Crypto Provider is invalid.");

        /// <summary>
        /// A parameter provided to the Platform Crypto Provider is invalid.
        /// </summary>
        public static HRESULT TPM_E_PCP_INVALID_PARAMETER = new HRESULT("0x80290403", "TPM_E_PCP_INVALID_PARAMETER", "A parameter provided to the Platform Crypto Provider is invalid.");

        /// <summary>
        /// A provided flag to the Platform Crypto Provider is not supported.
        /// </summary>
        public static HRESULT TPM_E_PCP_FLAG_NOT_SUPPORTED = new HRESULT("0x80290404", "TPM_E_PCP_FLAG_NOT_SUPPORTED", "A provided flag to the Platform Crypto Provider is not supported.");

        /// <summary>
        /// The requested operation is not supported by this Platform Crypto Provider.
        /// </summary>
        public static HRESULT TPM_E_PCP_NOT_SUPPORTED = new HRESULT("0x80290405", "TPM_E_PCP_NOT_SUPPORTED", "The requested operation is not supported by this Platform Crypto Provider.");

        /// <summary>
        /// The buffer is too small to contain all data. No information has been written to the buffer.
        /// </summary>
        public static HRESULT TPM_E_PCP_BUFFER_TOO_SMALL = new HRESULT("0x80290406", "TPM_E_PCP_BUFFER_TOO_SMALL", "The buffer is too small to contain all data. No information has been written to the buffer.");

        /// <summary>
        /// An unexpected internal error has occurred in the Platform Crypto Provider.
        /// </summary>
        public static HRESULT TPM_E_PCP_INTERNAL_ERROR = new HRESULT("0x80290407", "TPM_E_PCP_INTERNAL_ERROR", "An unexpected internal error has occurred in the Platform Crypto Provider.");

        /// <summary>
        /// The authorization to use a provider object has failed.
        /// </summary>
        public static HRESULT TPM_E_PCP_AUTHENTICATION_FAILED = new HRESULT("0x80290408", "TPM_E_PCP_AUTHENTICATION_FAILED", "The authorization to use a provider object has failed.");

        /// <summary>
        /// The Platform Crypto Device has ignored the authorization for the provider object, to mitigate against a dictionary attack.
        /// </summary>
        public static HRESULT TPM_E_PCP_AUTHENTICATION_IGNORED = new HRESULT("0x80290409", "TPM_E_PCP_AUTHENTICATION_IGNORED", "The Platform Crypto Device has ignored the authorization for the provider object, to mitigate against a dictionary attack.");

        /// <summary>
        /// The referenced policy was not found.
        /// </summary>
        public static HRESULT TPM_E_PCP_POLICY_NOT_FOUND = new HRESULT("0x8029040A", "TPM_E_PCP_POLICY_NOT_FOUND", "The referenced policy was not found.");

        /// <summary>
        /// The referenced profile was not found.
        /// </summary>
        public static HRESULT TPM_E_PCP_PROFILE_NOT_FOUND = new HRESULT("0x8029040B", "TPM_E_PCP_PROFILE_NOT_FOUND", "The referenced profile was not found.");

        /// <summary>
        /// The validation was not successful.
        /// </summary>
        public static HRESULT TPM_E_PCP_VALIDATION_FAILED = new HRESULT("0x8029040C", "TPM_E_PCP_VALIDATION_FAILED", "The validation was not successful.");

        /// <summary>
        /// An attempt was made to import or load a key under an incorrect storage parent.
        /// </summary>
        public static HRESULT TPM_E_PCP_WRONG_PARENT = new HRESULT("0x8029040E", "TPM_E_PCP_WRONG_PARENT", "An attempt was made to import or load a key under an incorrect storage parent.");

        /// <summary>
        /// The TPM key is not loaded.
        /// </summary>
        public static HRESULT TPM_E_KEY_NOT_LOADED = new HRESULT("0x8029040F", "TPM_E_KEY_NOT_LOADED", "The TPM key is not loaded.");

        /// <summary>
        /// The TPM key certification has not been generated.
        /// </summary>
        public static HRESULT TPM_E_NO_KEY_CERTIFICATION = new HRESULT("0x80290410", "TPM_E_NO_KEY_CERTIFICATION", "The TPM key certification has not been generated.");

        /// <summary>
        /// The TPM key is not yet finalized.
        /// </summary>
        public static HRESULT TPM_E_KEY_NOT_FINALIZED = new HRESULT("0x80290411", "TPM_E_KEY_NOT_FINALIZED", "The TPM key is not yet finalized.");

        /// <summary>
        /// The TPM attestation challenge is not set.
        /// </summary>
        public static HRESULT TPM_E_ATTESTATION_CHALLENGE_NOT_SET = new HRESULT("0x80290412", "TPM_E_ATTESTATION_CHALLENGE_NOT_SET", "The TPM attestation challenge is not set.");

        /// <summary>
        /// The TPM PCR info is not available.
        /// </summary>
        public static HRESULT TPM_E_NOT_PCR_BOUND = new HRESULT("0x80290413", "TPM_E_NOT_PCR_BOUND", "The TPM PCR info is not available.");

        /// <summary>
        /// The TPM key is already finalized.
        /// </summary>
        public static HRESULT TPM_E_KEY_ALREADY_FINALIZED = new HRESULT("0x80290414", "TPM_E_KEY_ALREADY_FINALIZED", "The TPM key is already finalized.");

        /// <summary>
        /// The TPM key usage policy is not supported.
        /// </summary>
        public static HRESULT TPM_E_KEY_USAGE_POLICY_NOT_SUPPORTED = new HRESULT("0x80290415", "TPM_E_KEY_USAGE_POLICY_NOT_SUPPORTED", "The TPM key usage policy is not supported.");

        /// <summary>
        /// The TPM key usage policy is invalid.
        /// </summary>
        public static HRESULT TPM_E_KEY_USAGE_POLICY_INVALID = new HRESULT("0x80290416", "TPM_E_KEY_USAGE_POLICY_INVALID", "The TPM key usage policy is invalid.");

        /// <summary>
        /// There was a problem with the software key being imported into the TPM.
        /// </summary>
        public static HRESULT TPM_E_SOFT_KEY_ERROR = new HRESULT("0x80290417", "TPM_E_SOFT_KEY_ERROR", "There was a problem with the software key being imported into the TPM.");

        /// <summary>
        /// The TPM key is not authenticated.
        /// </summary>
        public static HRESULT TPM_E_KEY_NOT_AUTHENTICATED = new HRESULT("0x80290418", "TPM_E_KEY_NOT_AUTHENTICATED", "The TPM key is not authenticated.");

        /// <summary>
        /// The TPM key is not an AIK.
        /// </summary>
        public static HRESULT TPM_E_PCP_KEY_NOT_AIK = new HRESULT("0x80290419", "TPM_E_PCP_KEY_NOT_AIK", "The TPM key is not an AIK.");

        /// <summary>
        /// The TPM key is not a signing key.
        /// </summary>
        public static HRESULT TPM_E_KEY_NOT_SIGNING_KEY = new HRESULT("0x8029041A", "TPM_E_KEY_NOT_SIGNING_KEY", "The TPM key is not a signing key.");

        /// <summary>
        /// The TPM is locked out.
        /// </summary>
        public static HRESULT TPM_E_LOCKED_OUT = new HRESULT("0x8029041B", "TPM_E_LOCKED_OUT", "The TPM is locked out.");

        /// <summary>
        /// The claim type requested is not supported.
        /// </summary>
        public static HRESULT TPM_E_CLAIM_TYPE_NOT_SUPPORTED = new HRESULT("0x8029041C", "TPM_E_CLAIM_TYPE_NOT_SUPPORTED", "The claim type requested is not supported.");

        /// <summary>
        /// TPM version is not supported.
        /// </summary>
        public static HRESULT TPM_E_VERSION_NOT_SUPPORTED = new HRESULT("0x8029041D", "TPM_E_VERSION_NOT_SUPPORTED", "TPM version is not supported.");

        /// <summary>
        /// The buffer lengths do not match.
        /// </summary>
        public static HRESULT TPM_E_BUFFER_LENGTH_MISMATCH = new HRESULT("0x8029041E", "TPM_E_BUFFER_LENGTH_MISMATCH", "The buffer lengths do not match.");

        /// <summary>
        /// The RSA key creation is blocked on this TPM due to known security vulnerabilities.
        /// </summary>
        public static HRESULT TPM_E_PCP_IFX_RSA_KEY_CREATION_BLOCKED = new HRESULT("0x8029041F", "TPM_E_PCP_IFX_RSA_KEY_CREATION_BLOCKED", "The RSA key creation is blocked on this TPM due to known security vulnerabilities.");

        /// <summary>
        /// A ticket required to use a key was not provided.
        /// </summary>
        public static HRESULT TPM_E_PCP_TICKET_MISSING = new HRESULT("0x80290420", "TPM_E_PCP_TICKET_MISSING", "A ticket required to use a key was not provided.");

        /// <summary>
        /// This key has a raw policy so the KSP can't authenticate against it.
        /// </summary>
        public static HRESULT TPM_E_PCP_RAW_POLICY_NOT_SUPPORTED = new HRESULT("0x80290421", "TPM_E_PCP_RAW_POLICY_NOT_SUPPORTED", "This key has a raw policy so the KSP can't authenticate against it.");

        /// <summary>
        /// The TPM key's handle was unexpectedly invalidated due to a hardware or firmware issue.
        /// </summary>
        public static HRESULT TPM_E_PCP_KEY_HANDLE_INVALIDATED = new HRESULT("0x80290422", "TPM_E_PCP_KEY_HANDLE_INVALIDATED", "The TPM key's handle was unexpectedly invalidated due to a hardware or firmware issue.");

        /// <summary>
        /// The requested salt size for signing with RSAPSS does not match what the TPM uses.
        /// </summary>
        public static HRESULT TPM_E_PCP_UNSUPPORTED_PSS_SALT = new HRESULT("0x40290423", "TPM_E_PCP_UNSUPPORTED_PSS_SALT", "The requested salt size for signing with RSAPSS does not match what the TPM uses.");

        /// <summary>
        /// Validation of the platform claim failed.
        /// </summary>
        public static HRESULT TPM_E_PCP_PLATFORM_CLAIM_MAY_BE_OUTDATED = new HRESULT("0x40290424", "TPM_E_PCP_PLATFORM_CLAIM_MAY_BE_OUTDATED", "Validation of the platform claim failed.");

        /// <summary>
        /// The requested platform claim is for a previous boot.
        /// </summary>
        public static HRESULT TPM_E_PCP_PLATFORM_CLAIM_OUTDATED = new HRESULT("0x40290425", "TPM_E_PCP_PLATFORM_CLAIM_OUTDATED", "The requested platform claim is for a previous boot.");

        /// <summary>
        /// The platform claim is for a previous boot, and cannot be created without reboot.
        /// </summary>
        public static HRESULT TPM_E_PCP_PLATFORM_CLAIM_REBOOT = new HRESULT("0x40290426", "TPM_E_PCP_PLATFORM_CLAIM_REBOOT", "The platform claim is for a previous boot, and cannot be created without reboot.");

        /// <summary>
        /// TPM related network operations are blocked as Zero Exhaust mode is enabled on client.
        /// </summary>
        public static HRESULT TPM_E_EXHAUST_ENABLED = new HRESULT("0x80290500", "TPM_E_EXHAUST_ENABLED", "TPM related network operations are blocked as Zero Exhaust mode is enabled on client.");

        /// <summary>
        /// TPM provisioning did not run to completion.
        /// </summary>
        public static HRESULT TPM_E_PROVISIONING_INCOMPLETE = new HRESULT("0x80290600", "TPM_E_PROVISIONING_INCOMPLETE", "TPM provisioning did not run to completion.");

        /// <summary>
        /// An invalid owner authorization value was specified.
        /// </summary>
        public static HRESULT TPM_E_INVALID_OWNER_AUTH = new HRESULT("0x80290601", "TPM_E_INVALID_OWNER_AUTH", "An invalid owner authorization value was specified.");

        /// <summary>
        /// TPM command returned too much data.
        /// </summary>
        public static HRESULT TPM_E_TOO_MUCH_DATA = new HRESULT("0x80290602", "TPM_E_TOO_MUCH_DATA", "TPM command returned too much data.");

        /// <summary>
        /// Data Collector Set was not found.
        /// </summary>
        public static HRESULT PLA_E_DCS_NOT_FOUND = new HRESULT("0x80300002", "PLA_E_DCS_NOT_FOUND", "Data Collector Set was not found.");

        /// <summary>
        /// The Data Collector Set or one of its dependencies is already in use.
        /// </summary>
        public static HRESULT PLA_E_DCS_IN_USE = new HRESULT("0x803000AA", "PLA_E_DCS_IN_USE", "The Data Collector Set or one of its dependencies is already in use.");

        /// <summary>
        /// Unable to start Data Collector Set because there are too many folders.
        /// </summary>
        public static HRESULT PLA_E_TOO_MANY_FOLDERS = new HRESULT("0x80300045", "PLA_E_TOO_MANY_FOLDERS", "Unable to start Data Collector Set because there are too many folders.");

        /// <summary>
        /// Not enough free disk space to start Data Collector Set.
        /// </summary>
        public static HRESULT PLA_E_NO_MIN_DISK = new HRESULT("0x80300070", "PLA_E_NO_MIN_DISK", "Not enough free disk space to start Data Collector Set.");

        /// <summary>
        /// Data Collector Set already exists.
        /// </summary>
        public static HRESULT PLA_E_DCS_ALREADY_EXISTS = new HRESULT("0x803000B7", "PLA_E_DCS_ALREADY_EXISTS", "Data Collector Set already exists.");

        /// <summary>
        /// Property value will be ignored.
        /// </summary>
        public static HRESULT PLA_S_PROPERTY_IGNORED = new HRESULT("0x00300100", "PLA_S_PROPERTY_IGNORED", "Property value will be ignored.");

        /// <summary>
        /// Property value conflict.
        /// </summary>
        public static HRESULT PLA_E_PROPERTY_CONFLICT = new HRESULT("0x80300101", "PLA_E_PROPERTY_CONFLICT", "Property value conflict.");

        /// <summary>
        /// The current configuration for this Data Collector Set requires that it contain exactly one Data Collector.
        /// </summary>
        public static HRESULT PLA_E_DCS_SINGLETON_REQUIRED = new HRESULT("0x80300102", "PLA_E_DCS_SINGLETON_REQUIRED", "The current configuration for this Data Collector Set requires that it contain exactly one Data Collector.");

        /// <summary>
        /// A user account is required in order to commit the current Data Collector Set properties.
        /// </summary>
        public static HRESULT PLA_E_CREDENTIALS_REQUIRED = new HRESULT("0x80300103", "PLA_E_CREDENTIALS_REQUIRED", "A user account is required in order to commit the current Data Collector Set properties.");

        /// <summary>
        /// Data Collector Set is not running.
        /// </summary>
        public static HRESULT PLA_E_DCS_NOT_RUNNING = new HRESULT("0x80300104", "PLA_E_DCS_NOT_RUNNING", "Data Collector Set is not running.");

        /// <summary>
        /// A conflict was detected in the list of include/exclude APIs. Do not specify the same API in both the include list and the exclude list.
        /// </summary>
        public static HRESULT PLA_E_CONFLICT_INCL_EXCL_API = new HRESULT("0x80300105", "PLA_E_CONFLICT_INCL_EXCL_API", "A conflict was detected in the list of include/exclude APIs. Do not specify the same API in both the include list and the exclude list.");

        /// <summary>
        /// The executable path you have specified refers to a network share or UNC path.
        /// </summary>
        public static HRESULT PLA_E_NETWORK_EXE_NOT_VALID = new HRESULT("0x80300106", "PLA_E_NETWORK_EXE_NOT_VALID", "The executable path you have specified refers to a network share or UNC path.");

        /// <summary>
        /// The executable path you have specified is already configured for API tracing.
        /// </summary>
        public static HRESULT PLA_E_EXE_ALREADY_CONFIGURED = new HRESULT("0x80300107", "PLA_E_EXE_ALREADY_CONFIGURED", "The executable path you have specified is already configured for API tracing.");

        /// <summary>
        /// The executable path you have specified does not exist. Verify that the specified path is correct.
        /// </summary>
        public static HRESULT PLA_E_EXE_PATH_NOT_VALID = new HRESULT("0x80300108", "PLA_E_EXE_PATH_NOT_VALID", "The executable path you have specified does not exist. Verify that the specified path is correct.");

        /// <summary>
        /// Data Collector already exists.
        /// </summary>
        public static HRESULT PLA_E_DC_ALREADY_EXISTS = new HRESULT("0x80300109", "PLA_E_DC_ALREADY_EXISTS", "Data Collector already exists.");

        /// <summary>
        /// The wait for the Data Collector Set start notification has timed out.
        /// </summary>
        public static HRESULT PLA_E_DCS_START_WAIT_TIMEOUT = new HRESULT("0x8030010A", "PLA_E_DCS_START_WAIT_TIMEOUT", "The wait for the Data Collector Set start notification has timed out.");

        /// <summary>
        /// The wait for the Data Collector to start has timed out.
        /// </summary>
        public static HRESULT PLA_E_DC_START_WAIT_TIMEOUT = new HRESULT("0x8030010B", "PLA_E_DC_START_WAIT_TIMEOUT", "The wait for the Data Collector to start has timed out.");

        /// <summary>
        /// The wait for the report generation tool to finish has timed out.
        /// </summary>
        public static HRESULT PLA_E_REPORT_WAIT_TIMEOUT = new HRESULT("0x8030010C", "PLA_E_REPORT_WAIT_TIMEOUT", "The wait for the report generation tool to finish has timed out.");

        /// <summary>
        /// Duplicate items are not allowed.
        /// </summary>
        public static HRESULT PLA_E_NO_DUPLICATES = new HRESULT("0x8030010D", "PLA_E_NO_DUPLICATES", "Duplicate items are not allowed.");

        /// <summary>
        /// When specifying the executable that you want to trace, you must specify a full path to the executable and not just a filename.
        /// </summary>
        public static HRESULT PLA_E_EXE_FULL_PATH_REQUIRED = new HRESULT("0x8030010E", "PLA_E_EXE_FULL_PATH_REQUIRED", "When specifying the executable that you want to trace, you must specify a full path to the executable and not just a filename.");

        /// <summary>
        /// The session name provided is invalid.
        /// </summary>
        public static HRESULT PLA_E_INVALID_SESSION_NAME = new HRESULT("0x8030010F", "PLA_E_INVALID_SESSION_NAME", "The session name provided is invalid.");

        /// <summary>
        /// The Event Log channel Microsoft-Windows-Diagnosis-PLA/Operational must be enabled to perform this operation.
        /// </summary>
        public static HRESULT PLA_E_PLA_CHANNEL_NOT_ENABLED = new HRESULT("0x80300110", "PLA_E_PLA_CHANNEL_NOT_ENABLED", "The Event Log channel Microsoft-Windows-Diagnosis-PLA/Operational must be enabled to perform this operation.");

        /// <summary>
        /// The Event Log channel Microsoft-Windows-TaskScheduler must be enabled to perform this operation.
        /// </summary>
        public static HRESULT PLA_E_TASKSCHED_CHANNEL_NOT_ENABLED = new HRESULT("0x80300111", "PLA_E_TASKSCHED_CHANNEL_NOT_ENABLED", "The Event Log channel Microsoft-Windows-TaskScheduler must be enabled to perform this operation.");

        /// <summary>
        /// The execution of the Rules Manager failed.
        /// </summary>
        public static HRESULT PLA_E_RULES_MANAGER_FAILED = new HRESULT("0x80300112", "PLA_E_RULES_MANAGER_FAILED", "The execution of the Rules Manager failed.");

        /// <summary>
        /// An error occurred while attempting to compress or extract the data.
        /// </summary>
        public static HRESULT PLA_E_CABAPI_FAILURE = new HRESULT("0x80300113", "PLA_E_CABAPI_FAILURE", "An error occurred while attempting to compress or extract the data.");

        /// <summary>
        /// This drive is locked by BitLocker Drive Encryption. You must unlock this drive from Control Panel.
        /// </summary>
        public static HRESULT FVE_E_LOCKED_VOLUME = new HRESULT("0x80310000", "FVE_E_LOCKED_VOLUME", "This drive is locked by BitLocker Drive Encryption. You must unlock this drive from Control Panel.");

        /// <summary>
        /// The drive is not encrypted.
        /// </summary>
        public static HRESULT FVE_E_NOT_ENCRYPTED = new HRESULT("0x80310001", "FVE_E_NOT_ENCRYPTED", "The drive is not encrypted.");

        /// <summary>
        /// The BIOS did not correctly communicate with the Trusted Platform Module (TPM). Contact the computer manufacturer for BIOS upgrade instructions.
        /// </summary>
        public static HRESULT FVE_E_NO_TPM_BIOS = new HRESULT("0x80310002", "FVE_E_NO_TPM_BIOS", "The BIOS did not correctly communicate with the Trusted Platform Module (TPM). Contact the computer manufacturer for BIOS upgrade instructions.");

        /// <summary>
        /// The BIOS did not correctly communicate with the master boot record (MBR). Contact the computer manufacturer for BIOS upgrade instructions.
        /// </summary>
        public static HRESULT FVE_E_NO_MBR_METRIC = new HRESULT("0x80310003", "FVE_E_NO_MBR_METRIC", "The BIOS did not correctly communicate with the master boot record (MBR). Contact the computer manufacturer for BIOS upgrade instructions.");

        /// <summary>
        /// A required TPM measurement is missing. If there is a bootable CD or DVD in your computer, remove it, restart the computer, and turn on BitLocker again. If the problem persists, ensure the master boot record is up to date.
        /// </summary>
        public static HRESULT FVE_E_NO_BOOTSECTOR_METRIC = new HRESULT("0x80310004", "FVE_E_NO_BOOTSECTOR_METRIC", "A required TPM measurement is missing. If there is a bootable CD or DVD in your computer, remove it, restart the computer, and turn on BitLocker again. If the problem persists, ensure the master boot record is up to date.");

        /// <summary>
        /// The boot sector of this drive is not compatible with BitLocker Drive Encryption. Use the Bootrec.exe tool in the Windows Recovery Environment to update or repair the boot manager (BOOTMGR).
        /// </summary>
        public static HRESULT FVE_E_NO_BOOTMGR_METRIC = new HRESULT("0x80310005", "FVE_E_NO_BOOTMGR_METRIC", "The boot sector of this drive is not compatible with BitLocker Drive Encryption. Use the Bootrec.exe tool in the Windows Recovery Environment to update or repair the boot manager (BOOTMGR).");

        /// <summary>
        /// The boot manager of this operating system is not compatible with BitLocker Drive Encryption. Use the Bootrec.exe tool in the Windows Recovery Environment to update or repair the boot manager (BOOTMGR).
        /// </summary>
        public static HRESULT FVE_E_WRONG_BOOTMGR = new HRESULT("0x80310006", "FVE_E_WRONG_BOOTMGR", "The boot manager of this operating system is not compatible with BitLocker Drive Encryption. Use the Bootrec.exe tool in the Windows Recovery Environment to update or repair the boot manager (BOOTMGR).");

        /// <summary>
        /// At least one secure key protector is required for this operation to be performed.
        /// </summary>
        public static HRESULT FVE_E_SECURE_KEY_REQUIRED = new HRESULT("0x80310007", "FVE_E_SECURE_KEY_REQUIRED", "At least one secure key protector is required for this operation to be performed.");

        /// <summary>
        /// BitLocker Drive Encryption is not enabled on this drive. Turn on BitLocker.
        /// </summary>
        public static HRESULT FVE_E_NOT_ACTIVATED = new HRESULT("0x80310008", "FVE_E_NOT_ACTIVATED", "BitLocker Drive Encryption is not enabled on this drive. Turn on BitLocker.");

        /// <summary>
        /// BitLocker Drive Encryption cannot perform requested action. This condition may occur when two requests are issued at the same time. Wait a few moments and then try the action again.
        /// </summary>
        public static HRESULT FVE_E_ACTION_NOT_ALLOWED = new HRESULT("0x80310009", "FVE_E_ACTION_NOT_ALLOWED", "BitLocker Drive Encryption cannot perform requested action. This condition may occur when two requests are issued at the same time. Wait a few moments and then try the action again.");

        /// <summary>
        /// The Active Directory Domain Services forest does not contain the required attributes and classes to host BitLocker Drive Encryption or Trusted Platform Module information. Contact your domain administrator to verify that any required BitLocker Active Directory schema extensions have been installed.
        /// </summary>
        public static HRESULT FVE_E_AD_SCHEMA_NOT_INSTALLED = new HRESULT("0x8031000A", "FVE_E_AD_SCHEMA_NOT_INSTALLED", "The Active Directory Domain Services forest does not contain the required attributes and classes to host BitLocker Drive Encryption or Trusted Platform Module information. Contact your domain administrator to verify that any required BitLocker Active Directory schema extensions have been installed.");

        /// <summary>
        /// The type of the data obtained from Active Directory was not expected. The BitLocker recovery information may be missing or corrupted.
        /// </summary>
        public static HRESULT FVE_E_AD_INVALID_DATATYPE = new HRESULT("0x8031000B", "FVE_E_AD_INVALID_DATATYPE", "The type of the data obtained from Active Directory was not expected. The BitLocker recovery information may be missing or corrupted.");

        /// <summary>
        /// The size of the data obtained from Active Directory was not expected. The BitLocker recovery information may be missing or corrupted.
        /// </summary>
        public static HRESULT FVE_E_AD_INVALID_DATASIZE = new HRESULT("0x8031000C", "FVE_E_AD_INVALID_DATASIZE", "The size of the data obtained from Active Directory was not expected. The BitLocker recovery information may be missing or corrupted.");

        /// <summary>
        /// The attribute read from Active Directory does not contain any values. The BitLocker recovery information may be missing or corrupted.
        /// </summary>
        public static HRESULT FVE_E_AD_NO_VALUES = new HRESULT("0x8031000D", "FVE_E_AD_NO_VALUES", "The attribute read from Active Directory does not contain any values. The BitLocker recovery information may be missing or corrupted.");

        /// <summary>
        /// The attribute was not set. Verify that you are logged on with a domain account that has the ability to write information to Active Directory objects.
        /// </summary>
        public static HRESULT FVE_E_AD_ATTR_NOT_SET = new HRESULT("0x8031000E", "FVE_E_AD_ATTR_NOT_SET", "The attribute was not set. Verify that you are logged on with a domain account that has the ability to write information to Active Directory objects.");

        /// <summary>
        /// The specified attribute cannot be found in Active Directory Domain Services. Contact your domain administrator to verify that any required BitLocker Active Directory schema extensions have been installed.
        /// </summary>
        public static HRESULT FVE_E_AD_GUID_NOT_FOUND = new HRESULT("0x8031000F", "FVE_E_AD_GUID_NOT_FOUND", "The specified attribute cannot be found in Active Directory Domain Services. Contact your domain administrator to verify that any required BitLocker Active Directory schema extensions have been installed.");

        /// <summary>
        /// The BitLocker metadata for the encrypted drive is not valid. You can attempt to repair the drive to restore access.
        /// </summary>
        public static HRESULT FVE_E_BAD_INFORMATION = new HRESULT("0x80310010", "FVE_E_BAD_INFORMATION", "The BitLocker metadata for the encrypted drive is not valid. You can attempt to repair the drive to restore access.");

        /// <summary>
        /// The drive cannot be encrypted because it does not have enough free space. Delete any unnecessary data on the drive to create additional free space and then try again.
        /// </summary>
        public static HRESULT FVE_E_TOO_SMALL = new HRESULT("0x80310011", "FVE_E_TOO_SMALL", "The drive cannot be encrypted because it does not have enough free space. Delete any unnecessary data on the drive to create additional free space and then try again.");

        /// <summary>
        /// The drive cannot be encrypted because it contains system boot information. Create a separate partition for use as the system drive that contains the boot information and a second partition for use as the operating system drive and then encrypt the operating system drive.
        /// </summary>
        public static HRESULT FVE_E_SYSTEM_VOLUME = new HRESULT("0x80310012", "FVE_E_SYSTEM_VOLUME", "The drive cannot be encrypted because it contains system boot information. Create a separate partition for use as the system drive that contains the boot information and a second partition for use as the operating system drive and then encrypt the operating system drive.");

        /// <summary>
        /// The drive cannot be encrypted because the file system is not supported.
        /// </summary>
        public static HRESULT FVE_E_FAILED_WRONG_FS = new HRESULT("0x80310013", "FVE_E_FAILED_WRONG_FS", "The drive cannot be encrypted because the file system is not supported.");

        /// <summary>
        /// The file system size is larger than the partition size in the partition table. This drive may be corrupt or may have been tampered with. To use it with BitLocker, you must reformat the partition.
        /// </summary>
        public static HRESULT FVE_E_BAD_PARTITION_SIZE = new HRESULT("0x80310014", "FVE_E_BAD_PARTITION_SIZE", "The file system size is larger than the partition size in the partition table. This drive may be corrupt or may have been tampered with. To use it with BitLocker, you must reformat the partition.");

        /// <summary>
        /// This drive cannot be encrypted.
        /// </summary>
        public static HRESULT FVE_E_NOT_SUPPORTED = new HRESULT("0x80310015", "FVE_E_NOT_SUPPORTED", "This drive cannot be encrypted.");

        /// <summary>
        /// The data is not valid.
        /// </summary>
        public static HRESULT FVE_E_BAD_DATA = new HRESULT("0x80310016", "FVE_E_BAD_DATA", "The data is not valid.");

        /// <summary>
        /// The data drive specified is not set to automatically unlock on the current computer and cannot be unlocked automatically.
        /// </summary>
        public static HRESULT FVE_E_VOLUME_NOT_BOUND = new HRESULT("0x80310017", "FVE_E_VOLUME_NOT_BOUND", "The data drive specified is not set to automatically unlock on the current computer and cannot be unlocked automatically.");

        /// <summary>
        /// You must initialize the Trusted Platform Module (TPM) before you can use BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_TPM_NOT_OWNED = new HRESULT("0x80310018", "FVE_E_TPM_NOT_OWNED", "You must initialize the Trusted Platform Module (TPM) before you can use BitLocker Drive Encryption.");

        /// <summary>
        /// The operation attempted cannot be performed on an operating system drive. 
        /// </summary>
        public static HRESULT FVE_E_NOT_DATA_VOLUME = new HRESULT("0x80310019", "FVE_E_NOT_DATA_VOLUME", "The operation attempted cannot be performed on an operating system drive. ");

        /// <summary>
        /// The buffer supplied to a function was insufficient to contain the returned data. Increase the buffer size before running the function again.
        /// </summary>
        public static HRESULT FVE_E_AD_INSUFFICIENT_BUFFER = new HRESULT("0x8031001A", "FVE_E_AD_INSUFFICIENT_BUFFER", "The buffer supplied to a function was insufficient to contain the returned data. Increase the buffer size before running the function again.");

        /// <summary>
        /// A read operation failed while converting the drive. The drive was not converted. Please re-enable BitLocker.
        /// </summary>
        public static HRESULT FVE_E_CONV_READ = new HRESULT("0x8031001B", "FVE_E_CONV_READ", "A read operation failed while converting the drive. The drive was not converted. Please re-enable BitLocker.");

        /// <summary>
        /// A write operation failed while converting the drive. The drive was not converted. Please re-enable BitLocker.
        /// </summary>
        public static HRESULT FVE_E_CONV_WRITE = new HRESULT("0x8031001C", "FVE_E_CONV_WRITE", "A write operation failed while converting the drive. The drive was not converted. Please re-enable BitLocker.");

        /// <summary>
        /// One or more BitLocker key protectors are required. You cannot delete the last key on this drive.
        /// </summary>
        public static HRESULT FVE_E_KEY_REQUIRED = new HRESULT("0x8031001D", "FVE_E_KEY_REQUIRED", "One or more BitLocker key protectors are required. You cannot delete the last key on this drive.");

        /// <summary>
        /// Cluster configurations are not supported by BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_CLUSTERING_NOT_SUPPORTED = new HRESULT("0x8031001E", "FVE_E_CLUSTERING_NOT_SUPPORTED", "Cluster configurations are not supported by BitLocker Drive Encryption.");

        /// <summary>
        /// The drive specified is already configured to be automatically unlocked on the current computer.
        /// </summary>
        public static HRESULT FVE_E_VOLUME_BOUND_ALREADY = new HRESULT("0x8031001F", "FVE_E_VOLUME_BOUND_ALREADY", "The drive specified is already configured to be automatically unlocked on the current computer.");

        /// <summary>
        /// The operating system drive is not protected by BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_OS_NOT_PROTECTED = new HRESULT("0x80310020", "FVE_E_OS_NOT_PROTECTED", "The operating system drive is not protected by BitLocker Drive Encryption.");

        /// <summary>
        /// BitLocker Drive Encryption has been suspended on this drive. All BitLocker key protectors configured for this drive are effectively disabled, and the drive will be automatically unlocked using an unencrypted (clear) key.
        /// </summary>
        public static HRESULT FVE_E_PROTECTION_DISABLED = new HRESULT("0x80310021", "FVE_E_PROTECTION_DISABLED", "BitLocker Drive Encryption has been suspended on this drive. All BitLocker key protectors configured for this drive are effectively disabled, and the drive will be automatically unlocked using an unencrypted (clear) key.");

        /// <summary>
        /// The drive you are attempting to lock does not have any key protectors available for encryption because BitLocker protection is currently suspended. Re-enable BitLocker to lock this drive.
        /// </summary>
        public static HRESULT FVE_E_RECOVERY_KEY_REQUIRED = new HRESULT("0x80310022", "FVE_E_RECOVERY_KEY_REQUIRED", "The drive you are attempting to lock does not have any key protectors available for encryption because BitLocker protection is currently suspended. Re-enable BitLocker to lock this drive.");

        /// <summary>
        /// BitLocker cannot use the Trusted Platform Module (TPM) to protect a data drive. TPM protection can only be used with the operating system drive.
        /// </summary>
        public static HRESULT FVE_E_FOREIGN_VOLUME = new HRESULT("0x80310023", "FVE_E_FOREIGN_VOLUME", "BitLocker cannot use the Trusted Platform Module (TPM) to protect a data drive. TPM protection can only be used with the operating system drive.");

        /// <summary>
        /// The BitLocker metadata for the encrypted drive cannot be updated because it was locked for updating by another process. Please try this process again.
        /// </summary>
        public static HRESULT FVE_E_OVERLAPPED_UPDATE = new HRESULT("0x80310024", "FVE_E_OVERLAPPED_UPDATE", "The BitLocker metadata for the encrypted drive cannot be updated because it was locked for updating by another process. Please try this process again.");

        /// <summary>
        /// The authorization data for the storage root key (SRK) of the Trusted Platform Module (TPM) is not zero and is therefore incompatible with BitLocker. Please initialize the TPM before attempting to use it with BitLocker.
        /// </summary>
        public static HRESULT FVE_E_TPM_SRK_AUTH_NOT_ZERO = new HRESULT("0x80310025", "FVE_E_TPM_SRK_AUTH_NOT_ZERO", "The authorization data for the storage root key (SRK) of the Trusted Platform Module (TPM) is not zero and is therefore incompatible with BitLocker. Please initialize the TPM before attempting to use it with BitLocker.");

        /// <summary>
        /// The drive encryption algorithm cannot be used on this sector size.
        /// </summary>
        public static HRESULT FVE_E_FAILED_SECTOR_SIZE = new HRESULT("0x80310026", "FVE_E_FAILED_SECTOR_SIZE", "The drive encryption algorithm cannot be used on this sector size.");

        /// <summary>
        /// The drive cannot be unlocked with the key provided. Confirm that you have provided the correct key and try again.
        /// </summary>
        public static HRESULT FVE_E_FAILED_AUTHENTICATION = new HRESULT("0x80310027", "FVE_E_FAILED_AUTHENTICATION", "The drive cannot be unlocked with the key provided. Confirm that you have provided the correct key and try again.");

        /// <summary>
        /// The drive specified is not the operating system drive.
        /// </summary>
        public static HRESULT FVE_E_NOT_OS_VOLUME = new HRESULT("0x80310028", "FVE_E_NOT_OS_VOLUME", "The drive specified is not the operating system drive.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be turned off on the operating system drive until the auto unlock feature has been disabled for the fixed data drives and removable data drives associated with this computer.
        /// </summary>
        public static HRESULT FVE_E_AUTOUNLOCK_ENABLED = new HRESULT("0x80310029", "FVE_E_AUTOUNLOCK_ENABLED", "BitLocker Drive Encryption cannot be turned off on the operating system drive until the auto unlock feature has been disabled for the fixed data drives and removable data drives associated with this computer.");

        /// <summary>
        /// The system partition boot sector does not perform Trusted Platform Module (TPM) measurements. Use the Bootrec.exe tool in the Windows Recovery Environment to update or repair the boot sector.
        /// </summary>
        public static HRESULT FVE_E_WRONG_BOOTSECTOR = new HRESULT("0x8031002A", "FVE_E_WRONG_BOOTSECTOR", "The system partition boot sector does not perform Trusted Platform Module (TPM) measurements. Use the Bootrec.exe tool in the Windows Recovery Environment to update or repair the boot sector.");

        /// <summary>
        /// BitLocker Drive Encryption operating system drives must be formatted with the NTFS file system in order to be encrypted. Convert the drive to NTFS, and then turn on BitLocker.
        /// </summary>
        public static HRESULT FVE_E_WRONG_SYSTEM_FS = new HRESULT("0x8031002B", "FVE_E_WRONG_SYSTEM_FS", "BitLocker Drive Encryption operating system drives must be formatted with the NTFS file system in order to be encrypted. Convert the drive to NTFS, and then turn on BitLocker.");

        /// <summary>
        /// Group Policy settings require that a recovery password be specified before encrypting the drive.
        /// </summary>
        public static HRESULT FVE_E_POLICY_PASSWORD_REQUIRED = new HRESULT("0x8031002C", "FVE_E_POLICY_PASSWORD_REQUIRED", "Group Policy settings require that a recovery password be specified before encrypting the drive.");

        /// <summary>
        /// The drive encryption algorithm and key cannot be set on a previously encrypted drive. To encrypt this drive with BitLocker Drive Encryption, remove the previous encryption and then turn on BitLocker.
        /// </summary>
        public static HRESULT FVE_E_CANNOT_SET_FVEK_ENCRYPTED = new HRESULT("0x8031002D", "FVE_E_CANNOT_SET_FVEK_ENCRYPTED", "The drive encryption algorithm and key cannot be set on a previously encrypted drive. To encrypt this drive with BitLocker Drive Encryption, remove the previous encryption and then turn on BitLocker.");

        /// <summary>
        /// BitLocker Drive Encryption cannot encrypt the specified drive because an encryption key is not available. Add a key protector to encrypt this drive.
        /// </summary>
        public static HRESULT FVE_E_CANNOT_ENCRYPT_NO_KEY = new HRESULT("0x8031002E", "FVE_E_CANNOT_ENCRYPT_NO_KEY", "BitLocker Drive Encryption cannot encrypt the specified drive because an encryption key is not available. Add a key protector to encrypt this drive.");

        /// <summary>
        /// BitLocker Drive Encryption detected bootable media (CD or DVD) in the computer. Remove the media and restart the computer before configuring BitLocker.
        /// </summary>
        public static HRESULT FVE_E_BOOTABLE_CDDVD = new HRESULT("0x80310030", "FVE_E_BOOTABLE_CDDVD", "BitLocker Drive Encryption detected bootable media (CD or DVD) in the computer. Remove the media and restart the computer before configuring BitLocker.");

        /// <summary>
        /// This key protector cannot be added. Only one key protector of this type is allowed for this drive.
        /// </summary>
        public static HRESULT FVE_E_PROTECTOR_EXISTS = new HRESULT("0x80310031", "FVE_E_PROTECTOR_EXISTS", "This key protector cannot be added. Only one key protector of this type is allowed for this drive.");

        /// <summary>
        /// The recovery password file was not found because a relative path was specified. Recovery passwords must be saved to a fully qualified path. Environment variables configured on the computer can be used in the path.
        /// </summary>
        public static HRESULT FVE_E_RELATIVE_PATH = new HRESULT("0x80310032", "FVE_E_RELATIVE_PATH", "The recovery password file was not found because a relative path was specified. Recovery passwords must be saved to a fully qualified path. Environment variables configured on the computer can be used in the path.");

        /// <summary>
        /// The specified key protector was not found on the drive. Try another key protector.
        /// </summary>
        public static HRESULT FVE_E_PROTECTOR_NOT_FOUND = new HRESULT("0x80310033", "FVE_E_PROTECTOR_NOT_FOUND", "The specified key protector was not found on the drive. Try another key protector.");

        /// <summary>
        /// The recovery key provided is corrupt and cannot be used to access the drive. An alternative recovery method, such as recovery password, a data recovery agent, or a backup version of the recovery key must be used to recover access to the drive.
        /// </summary>
        public static HRESULT FVE_E_INVALID_KEY_FORMAT = new HRESULT("0x80310034", "FVE_E_INVALID_KEY_FORMAT", "The recovery key provided is corrupt and cannot be used to access the drive. An alternative recovery method, such as recovery password, a data recovery agent, or a backup version of the recovery key must be used to recover access to the drive.");

        /// <summary>
        /// The format of the recovery password provided is invalid. BitLocker recovery passwords are 48 digits. Verify that the recovery password is in the correct format and then try again.
        /// </summary>
        public static HRESULT FVE_E_INVALID_PASSWORD_FORMAT = new HRESULT("0x80310035", "FVE_E_INVALID_PASSWORD_FORMAT", "The format of the recovery password provided is invalid. BitLocker recovery passwords are 48 digits. Verify that the recovery password is in the correct format and then try again.");

        /// <summary>
        /// The random number generator check test failed.
        /// </summary>
        public static HRESULT FVE_E_FIPS_RNG_CHECK_FAILED = new HRESULT("0x80310036", "FVE_E_FIPS_RNG_CHECK_FAILED", "The random number generator check test failed.");

        /// <summary>
        /// The Group Policy setting requiring FIPS compliance prevents a local recovery password from being generated or used by BitLocker Drive Encryption. When operating in FIPS-compliant mode, BitLocker recovery options can be either a recovery key stored on a USB drive or recovery through a data recovery agent.
        /// </summary>
        public static HRESULT FVE_E_FIPS_PREVENTS_RECOVERY_PASSWORD = new HRESULT("0x80310037", "FVE_E_FIPS_PREVENTS_RECOVERY_PASSWORD", "The Group Policy setting requiring FIPS compliance prevents a local recovery password from being generated or used by BitLocker Drive Encryption. When operating in FIPS-compliant mode, BitLocker recovery options can be either a recovery key stored on a USB drive or recovery through a data recovery agent.");

        /// <summary>
        /// The Group Policy setting requiring FIPS compliance prevents the recovery password from being saved to Active Directory. When operating in FIPS-compliant mode, BitLocker recovery options can be either a recovery key stored on a USB drive or recovery through a data recovery agent. Check your Group Policy settings configuration.
        /// </summary>
        public static HRESULT FVE_E_FIPS_PREVENTS_EXTERNAL_KEY_EXPORT = new HRESULT("0x80310038", "FVE_E_FIPS_PREVENTS_EXTERNAL_KEY_EXPORT", "The Group Policy setting requiring FIPS compliance prevents the recovery password from being saved to Active Directory. When operating in FIPS-compliant mode, BitLocker recovery options can be either a recovery key stored on a USB drive or recovery through a data recovery agent. Check your Group Policy settings configuration.");

        /// <summary>
        /// The drive must be fully decrypted to complete this operation.
        /// </summary>
        public static HRESULT FVE_E_NOT_DECRYPTED = new HRESULT("0x80310039", "FVE_E_NOT_DECRYPTED", "The drive must be fully decrypted to complete this operation.");

        /// <summary>
        /// The key protector specified cannot be used for this operation.
        /// </summary>
        public static HRESULT FVE_E_INVALID_PROTECTOR_TYPE = new HRESULT("0x8031003A", "FVE_E_INVALID_PROTECTOR_TYPE", "The key protector specified cannot be used for this operation.");

        /// <summary>
        /// No key protectors exist on the drive to perform the hardware test.
        /// </summary>
        public static HRESULT FVE_E_NO_PROTECTORS_TO_TEST = new HRESULT("0x8031003B", "FVE_E_NO_PROTECTORS_TO_TEST", "No key protectors exist on the drive to perform the hardware test.");

        /// <summary>
        /// The BitLocker startup key or recovery password cannot be found on the USB device. Verify that you have the correct USB device, that the USB device is plugged into the computer on an active USB port, restart the computer, and then try again. If the problem persists, contact the computer manufacturer for BIOS upgrade instructions.
        /// </summary>
        public static HRESULT FVE_E_KEYFILE_NOT_FOUND = new HRESULT("0x8031003C", "FVE_E_KEYFILE_NOT_FOUND", "The BitLocker startup key or recovery password cannot be found on the USB device. Verify that you have the correct USB device, that the USB device is plugged into the computer on an active USB port, restart the computer, and then try again. If the problem persists, contact the computer manufacturer for BIOS upgrade instructions.");

        /// <summary>
        /// The BitLocker startup key or recovery password file provided is corrupt or invalid. Verify that you have the correct startup key or recovery password file and try again.
        /// </summary>
        public static HRESULT FVE_E_KEYFILE_INVALID = new HRESULT("0x8031003D", "FVE_E_KEYFILE_INVALID", "The BitLocker startup key or recovery password file provided is corrupt or invalid. Verify that you have the correct startup key or recovery password file and try again.");

        /// <summary>
        /// The BitLocker encryption key cannot be obtained from the startup key or recovery password. Verify that you have the correct startup key or recovery password and try again.
        /// </summary>
        public static HRESULT FVE_E_KEYFILE_NO_VMK = new HRESULT("0x8031003E", "FVE_E_KEYFILE_NO_VMK", "The BitLocker encryption key cannot be obtained from the startup key or recovery password. Verify that you have the correct startup key or recovery password and try again.");

        /// <summary>
        /// The Trusted Platform Module (TPM) is disabled. The TPM must be enabled, initialized, and have valid ownership before it can be used with BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_TPM_DISABLED = new HRESULT("0x8031003F", "FVE_E_TPM_DISABLED", "The Trusted Platform Module (TPM) is disabled. The TPM must be enabled, initialized, and have valid ownership before it can be used with BitLocker Drive Encryption.");

        /// <summary>
        /// The BitLocker configuration of the specified drive cannot be managed because this computer is currently operating in Safe Mode. While in Safe Mode, BitLocker Drive Encryption can only be used for recovery purposes.
        /// </summary>
        public static HRESULT FVE_E_NOT_ALLOWED_IN_SAFE_MODE = new HRESULT("0x80310040", "FVE_E_NOT_ALLOWED_IN_SAFE_MODE", "The BitLocker configuration of the specified drive cannot be managed because this computer is currently operating in Safe Mode. While in Safe Mode, BitLocker Drive Encryption can only be used for recovery purposes.");

        /// <summary>
        /// The Trusted Platform Module (TPM) was not able to unlock the drive because the system boot information has changed or a PIN was not provided correctly. Verify that the drive has not been tampered with and that changes to the system boot information were caused by a trusted source. After verifying that the drive is safe to access, use the BitLocker recovery console to unlock the drive and then suspend and resume BitLocker to update system boot information that BitLocker associates with this drive.
        /// </summary>
        public static HRESULT FVE_E_TPM_INVALID_PCR = new HRESULT("0x80310041", "FVE_E_TPM_INVALID_PCR", "The Trusted Platform Module (TPM) was not able to unlock the drive because the system boot information has changed or a PIN was not provided correctly. Verify that the drive has not been tampered with and that changes to the system boot information were caused by a trusted source. After verifying that the drive is safe to access, use the BitLocker recovery console to unlock the drive and then suspend and resume BitLocker to update system boot information that BitLocker associates with this drive.");

        /// <summary>
        /// The BitLocker encryption key cannot be obtained from the Trusted Platform Module (TPM).
        /// </summary>
        public static HRESULT FVE_E_TPM_NO_VMK = new HRESULT("0x80310042", "FVE_E_TPM_NO_VMK", "The BitLocker encryption key cannot be obtained from the Trusted Platform Module (TPM).");

        /// <summary>
        /// The BitLocker encryption key cannot be obtained from the Trusted Platform Module (TPM) and PIN.
        /// </summary>
        public static HRESULT FVE_E_PIN_INVALID = new HRESULT("0x80310043", "FVE_E_PIN_INVALID", "The BitLocker encryption key cannot be obtained from the Trusted Platform Module (TPM) and PIN.");

        /// <summary>
        /// A boot application has changed since BitLocker Drive Encryption was enabled.
        /// </summary>
        public static HRESULT FVE_E_AUTH_INVALID_APPLICATION = new HRESULT("0x80310044", "FVE_E_AUTH_INVALID_APPLICATION", "A boot application has changed since BitLocker Drive Encryption was enabled.");

        /// <summary>
        /// The Boot Configuration Data (BCD) settings have changed since BitLocker Drive Encryption was enabled.
        /// </summary>
        public static HRESULT FVE_E_AUTH_INVALID_CONFIG = new HRESULT("0x80310045", "FVE_E_AUTH_INVALID_CONFIG", "The Boot Configuration Data (BCD) settings have changed since BitLocker Drive Encryption was enabled.");

        /// <summary>
        /// The Group Policy setting requiring FIPS compliance prohibits the use of unencrypted keys, which prevents BitLocker from being suspended on this drive. Please contact your domain administrator for more information.
        /// </summary>
        public static HRESULT FVE_E_FIPS_DISABLE_PROTECTION_NOT_ALLOWED = new HRESULT("0x80310046", "FVE_E_FIPS_DISABLE_PROTECTION_NOT_ALLOWED", "The Group Policy setting requiring FIPS compliance prohibits the use of unencrypted keys, which prevents BitLocker from being suspended on this drive. Please contact your domain administrator for more information.");

        /// <summary>
        /// This drive cannot be encrypted by BitLocker Drive Encryption because the file system does not extend to the end of the drive. Repartition this drive and then try again.
        /// </summary>
        public static HRESULT FVE_E_FS_NOT_EXTENDED = new HRESULT("0x80310047", "FVE_E_FS_NOT_EXTENDED", "This drive cannot be encrypted by BitLocker Drive Encryption because the file system does not extend to the end of the drive. Repartition this drive and then try again.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be enabled on the operating system drive. Contact the computer manufacturer for BIOS upgrade instructions.
        /// </summary>
        public static HRESULT FVE_E_FIRMWARE_TYPE_NOT_SUPPORTED = new HRESULT("0x80310048", "FVE_E_FIRMWARE_TYPE_NOT_SUPPORTED", "BitLocker Drive Encryption cannot be enabled on the operating system drive. Contact the computer manufacturer for BIOS upgrade instructions.");

        /// <summary>
        /// This version of Windows does not include BitLocker Drive Encryption. To use BitLocker Drive Encryption, please upgrade the operating system.
        /// </summary>
        public static HRESULT FVE_E_NO_LICENSE = new HRESULT("0x80310049", "FVE_E_NO_LICENSE", "This version of Windows does not include BitLocker Drive Encryption. To use BitLocker Drive Encryption, please upgrade the operating system.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be used because critical BitLocker system files are missing or corrupted. Use Windows Startup Repair to restore these files to your computer.
        /// </summary>
        public static HRESULT FVE_E_NOT_ON_STACK = new HRESULT("0x8031004A", "FVE_E_NOT_ON_STACK", "BitLocker Drive Encryption cannot be used because critical BitLocker system files are missing or corrupted. Use Windows Startup Repair to restore these files to your computer.");

        /// <summary>
        /// The drive cannot be locked when the drive is in use.
        /// </summary>
        public static HRESULT FVE_E_FS_MOUNTED = new HRESULT("0x8031004B", "FVE_E_FS_MOUNTED", "The drive cannot be locked when the drive is in use.");

        /// <summary>
        /// The access token associated with the current thread is not an impersonated token.
        /// </summary>
        public static HRESULT FVE_E_TOKEN_NOT_IMPERSONATED = new HRESULT("0x8031004C", "FVE_E_TOKEN_NOT_IMPERSONATED", "The access token associated with the current thread is not an impersonated token.");

        /// <summary>
        /// The BitLocker encryption key cannot be obtained. Verify that the Trusted Platform Module (TPM) is enabled and ownership has been taken. If this computer does not have a TPM, verify that the USB drive is inserted and available.
        /// </summary>
        public static HRESULT FVE_E_DRY_RUN_FAILED = new HRESULT("0x8031004D", "FVE_E_DRY_RUN_FAILED", "The BitLocker encryption key cannot be obtained. Verify that the Trusted Platform Module (TPM) is enabled and ownership has been taken. If this computer does not have a TPM, verify that the USB drive is inserted and available.");

        /// <summary>
        /// You must restart your computer before continuing with BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_REBOOT_REQUIRED = new HRESULT("0x8031004E", "FVE_E_REBOOT_REQUIRED", "You must restart your computer before continuing with BitLocker Drive Encryption.");

        /// <summary>
        /// Drive encryption cannot occur while boot debugging is enabled. Use the bcdedit command-line tool to turn off boot debugging.
        /// </summary>
        public static HRESULT FVE_E_DEBUGGER_ENABLED = new HRESULT("0x8031004F", "FVE_E_DEBUGGER_ENABLED", "Drive encryption cannot occur while boot debugging is enabled. Use the bcdedit command-line tool to turn off boot debugging.");

        /// <summary>
        /// No action was taken as BitLocker Drive Encryption is in raw access mode.
        /// </summary>
        public static HRESULT FVE_E_RAW_ACCESS = new HRESULT("0x80310050", "FVE_E_RAW_ACCESS", "No action was taken as BitLocker Drive Encryption is in raw access mode.");

        /// <summary>
        /// BitLocker Drive Encryption cannot enter raw access mode for this drive because the drive is currently in use.
        /// </summary>
        public static HRESULT FVE_E_RAW_BLOCKED = new HRESULT("0x80310051", "FVE_E_RAW_BLOCKED", "BitLocker Drive Encryption cannot enter raw access mode for this drive because the drive is currently in use.");

        /// <summary>
        /// The path specified in the Boot Configuration Data (BCD) for a BitLocker Drive Encryption integrity-protected application is incorrect. Please verify and correct your BCD settings and try again.
        /// </summary>
        public static HRESULT FVE_E_BCD_APPLICATIONS_PATH_INCORRECT = new HRESULT("0x80310052", "FVE_E_BCD_APPLICATIONS_PATH_INCORRECT", "The path specified in the Boot Configuration Data (BCD) for a BitLocker Drive Encryption integrity-protected application is incorrect. Please verify and correct your BCD settings and try again.");

        /// <summary>
        /// BitLocker Drive Encryption can only be used for limited provisioning or recovery purposes when the computer is running in pre-installation or recovery environments.
        /// </summary>
        public static HRESULT FVE_E_NOT_ALLOWED_IN_VERSION = new HRESULT("0x80310053", "FVE_E_NOT_ALLOWED_IN_VERSION", "BitLocker Drive Encryption can only be used for limited provisioning or recovery purposes when the computer is running in pre-installation or recovery environments.");

        /// <summary>
        /// The auto-unlock master key was not available from the operating system drive.
        /// </summary>
        public static HRESULT FVE_E_NO_AUTOUNLOCK_MASTER_KEY = new HRESULT("0x80310054", "FVE_E_NO_AUTOUNLOCK_MASTER_KEY", "The auto-unlock master key was not available from the operating system drive.");

        /// <summary>
        /// The system firmware failed to enable clearing of system memory when the computer was restarted.
        /// </summary>
        public static HRESULT FVE_E_MOR_FAILED = new HRESULT("0x80310055", "FVE_E_MOR_FAILED", "The system firmware failed to enable clearing of system memory when the computer was restarted.");

        /// <summary>
        /// The hidden drive cannot be encrypted.
        /// </summary>
        public static HRESULT FVE_E_HIDDEN_VOLUME = new HRESULT("0x80310056", "FVE_E_HIDDEN_VOLUME", "The hidden drive cannot be encrypted.");

        /// <summary>
        /// BitLocker encryption keys were ignored because the drive was in a transient state.
        /// </summary>
        public static HRESULT FVE_E_TRANSIENT_STATE = new HRESULT("0x80310057", "FVE_E_TRANSIENT_STATE", "BitLocker encryption keys were ignored because the drive was in a transient state.");

        /// <summary>
        /// Public key based protectors are not allowed on this drive.
        /// </summary>
        public static HRESULT FVE_E_PUBKEY_NOT_ALLOWED = new HRESULT("0x80310058", "FVE_E_PUBKEY_NOT_ALLOWED", "Public key based protectors are not allowed on this drive.");

        /// <summary>
        /// BitLocker Drive Encryption is already performing an operation on this drive. Please complete all operations before continuing.
        /// </summary>
        public static HRESULT FVE_E_VOLUME_HANDLE_OPEN = new HRESULT("0x80310059", "FVE_E_VOLUME_HANDLE_OPEN", "BitLocker Drive Encryption is already performing an operation on this drive. Please complete all operations before continuing.");

        /// <summary>
        /// This version of Windows does not support this feature of BitLocker Drive Encryption. To use this feature, upgrade the operating system.
        /// </summary>
        public static HRESULT FVE_E_NO_FEATURE_LICENSE = new HRESULT("0x8031005A", "FVE_E_NO_FEATURE_LICENSE", "This version of Windows does not support this feature of BitLocker Drive Encryption. To use this feature, upgrade the operating system.");

        /// <summary>
        /// The Group Policy settings for BitLocker startup options are in conflict and cannot be applied. Contact your system administrator for more information.
        /// </summary>
        public static HRESULT FVE_E_INVALID_STARTUP_OPTIONS = new HRESULT("0x8031005B", "FVE_E_INVALID_STARTUP_OPTIONS", "The Group Policy settings for BitLocker startup options are in conflict and cannot be applied. Contact your system administrator for more information.");

        /// <summary>
        /// Group policy settings do not permit the creation of a recovery password.
        /// </summary>
        public static HRESULT FVE_E_POLICY_RECOVERY_PASSWORD_NOT_ALLOWED = new HRESULT("0x8031005C", "FVE_E_POLICY_RECOVERY_PASSWORD_NOT_ALLOWED", "Group policy settings do not permit the creation of a recovery password.");

        /// <summary>
        /// Group policy settings require the creation of a recovery password.
        /// </summary>
        public static HRESULT FVE_E_POLICY_RECOVERY_PASSWORD_REQUIRED = new HRESULT("0x8031005D", "FVE_E_POLICY_RECOVERY_PASSWORD_REQUIRED", "Group policy settings require the creation of a recovery password.");

        /// <summary>
        /// Group policy settings do not permit the creation of a recovery key.
        /// </summary>
        public static HRESULT FVE_E_POLICY_RECOVERY_KEY_NOT_ALLOWED = new HRESULT("0x8031005E", "FVE_E_POLICY_RECOVERY_KEY_NOT_ALLOWED", "Group policy settings do not permit the creation of a recovery key.");

        /// <summary>
        /// Group policy settings require the creation of a recovery key.
        /// </summary>
        public static HRESULT FVE_E_POLICY_RECOVERY_KEY_REQUIRED = new HRESULT("0x8031005F", "FVE_E_POLICY_RECOVERY_KEY_REQUIRED", "Group policy settings require the creation of a recovery key.");

        /// <summary>
        /// Group policy settings do not permit the use of a PIN at startup. Please choose a different BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_PIN_NOT_ALLOWED = new HRESULT("0x80310060", "FVE_E_POLICY_STARTUP_PIN_NOT_ALLOWED", "Group policy settings do not permit the use of a PIN at startup. Please choose a different BitLocker startup option.");

        /// <summary>
        /// Group policy settings require the use of a PIN at startup. Please choose this BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_PIN_REQUIRED = new HRESULT("0x80310061", "FVE_E_POLICY_STARTUP_PIN_REQUIRED", "Group policy settings require the use of a PIN at startup. Please choose this BitLocker startup option.");

        /// <summary>
        /// Group policy settings do not permit the use of a startup key. Please choose a different BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_KEY_NOT_ALLOWED = new HRESULT("0x80310062", "FVE_E_POLICY_STARTUP_KEY_NOT_ALLOWED", "Group policy settings do not permit the use of a startup key. Please choose a different BitLocker startup option.");

        /// <summary>
        /// Group policy settings require the use of a startup key. Please choose this BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_KEY_REQUIRED = new HRESULT("0x80310063", "FVE_E_POLICY_STARTUP_KEY_REQUIRED", "Group policy settings require the use of a startup key. Please choose this BitLocker startup option.");

        /// <summary>
        /// Group policy settings do not permit the use of a startup key and PIN. Please choose a different BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_PIN_KEY_NOT_ALLOWED = new HRESULT("0x80310064", "FVE_E_POLICY_STARTUP_PIN_KEY_NOT_ALLOWED", "Group policy settings do not permit the use of a startup key and PIN. Please choose a different BitLocker startup option.");

        /// <summary>
        /// Group policy settings require the use of a startup key and PIN. Please choose this BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_PIN_KEY_REQUIRED = new HRESULT("0x80310065", "FVE_E_POLICY_STARTUP_PIN_KEY_REQUIRED", "Group policy settings require the use of a startup key and PIN. Please choose this BitLocker startup option.");

        /// <summary>
        /// Group policy does not permit the use of TPM-only at startup. Please choose a different BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_TPM_NOT_ALLOWED = new HRESULT("0x80310066", "FVE_E_POLICY_STARTUP_TPM_NOT_ALLOWED", "Group policy does not permit the use of TPM-only at startup. Please choose a different BitLocker startup option.");

        /// <summary>
        /// Group policy settings require the use of TPM-only at startup. Please choose this BitLocker startup option.
        /// </summary>
        public static HRESULT FVE_E_POLICY_STARTUP_TPM_REQUIRED = new HRESULT("0x80310067", "FVE_E_POLICY_STARTUP_TPM_REQUIRED", "Group policy settings require the use of TPM-only at startup. Please choose this BitLocker startup option.");

        /// <summary>
        /// The PIN provided does not meet minimum or maximum length requirements.
        /// </summary>
        public static HRESULT FVE_E_POLICY_INVALID_PIN_LENGTH = new HRESULT("0x80310068", "FVE_E_POLICY_INVALID_PIN_LENGTH", "The PIN provided does not meet minimum or maximum length requirements.");

        /// <summary>
        /// The key protector is not supported by the version of BitLocker Drive Encryption currently on the drive. Upgrade the drive to add the key protector.
        /// </summary>
        public static HRESULT FVE_E_KEY_PROTECTOR_NOT_SUPPORTED = new HRESULT("0x80310069", "FVE_E_KEY_PROTECTOR_NOT_SUPPORTED", "The key protector is not supported by the version of BitLocker Drive Encryption currently on the drive. Upgrade the drive to add the key protector.");

        /// <summary>
        /// Group policy settings do not permit the creation of a password.
        /// </summary>
        public static HRESULT FVE_E_POLICY_PASSPHRASE_NOT_ALLOWED = new HRESULT("0x8031006A", "FVE_E_POLICY_PASSPHRASE_NOT_ALLOWED", "Group policy settings do not permit the creation of a password.");

        /// <summary>
        /// Group policy settings require the creation of a password.
        /// </summary>
        public static HRESULT FVE_E_POLICY_PASSPHRASE_REQUIRED = new HRESULT("0x8031006B", "FVE_E_POLICY_PASSPHRASE_REQUIRED", "Group policy settings require the creation of a password.");

        /// <summary>
        /// The group policy setting requiring FIPS compliance prevented the password from being generated or used. Please contact your domain administrator for more information.
        /// </summary>
        public static HRESULT FVE_E_FIPS_PREVENTS_PASSPHRASE = new HRESULT("0x8031006C", "FVE_E_FIPS_PREVENTS_PASSPHRASE", "The group policy setting requiring FIPS compliance prevented the password from being generated or used. Please contact your domain administrator for more information.");

        /// <summary>
        /// A password cannot be added to the operating system drive.
        /// </summary>
        public static HRESULT FVE_E_OS_VOLUME_PASSPHRASE_NOT_ALLOWED = new HRESULT("0x8031006D", "FVE_E_OS_VOLUME_PASSPHRASE_NOT_ALLOWED", "A password cannot be added to the operating system drive.");

        /// <summary>
        /// The BitLocker object identifier (OID) on the drive appears to be invalid or corrupt. Use manage-BDE to reset the OID on this drive.
        /// </summary>
        public static HRESULT FVE_E_INVALID_BITLOCKER_OID = new HRESULT("0x8031006E", "FVE_E_INVALID_BITLOCKER_OID", "The BitLocker object identifier (OID) on the drive appears to be invalid or corrupt. Use manage-BDE to reset the OID on this drive.");

        /// <summary>
        /// The drive is too small to be protected using BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_VOLUME_TOO_SMALL = new HRESULT("0x8031006F", "FVE_E_VOLUME_TOO_SMALL", "The drive is too small to be protected using BitLocker Drive Encryption.");

        /// <summary>
        /// The selected discovery drive type is incompatible with the file system on the drive. BitLocker To Go discovery drives must be created on FAT formatted drives.
        /// </summary>
        public static HRESULT FVE_E_DV_NOT_SUPPORTED_ON_FS = new HRESULT("0x80310070", "FVE_E_DV_NOT_SUPPORTED_ON_FS", "The selected discovery drive type is incompatible with the file system on the drive. BitLocker To Go discovery drives must be created on FAT formatted drives.");

        /// <summary>
        /// The selected discovery drive type is not allowed by the computer's Group Policy settings. Verify that Group Policy settings allow the creation of discovery drives for use with BitLocker To Go.
        /// </summary>
        public static HRESULT FVE_E_DV_NOT_ALLOWED_BY_GP = new HRESULT("0x80310071", "FVE_E_DV_NOT_ALLOWED_BY_GP", "The selected discovery drive type is not allowed by the computer's Group Policy settings. Verify that Group Policy settings allow the creation of discovery drives for use with BitLocker To Go.");

        /// <summary>
        /// Group Policy settings do not permit user certificates such as smart cards to be used with BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_CERTIFICATE_NOT_ALLOWED = new HRESULT("0x80310072", "FVE_E_POLICY_USER_CERTIFICATE_NOT_ALLOWED", "Group Policy settings do not permit user certificates such as smart cards to be used with BitLocker Drive Encryption.");

        /// <summary>
        /// Group Policy settings require that you have a valid user certificate, such as a smart card, to be used with BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_CERTIFICATE_REQUIRED = new HRESULT("0x80310073", "FVE_E_POLICY_USER_CERTIFICATE_REQUIRED", "Group Policy settings require that you have a valid user certificate, such as a smart card, to be used with BitLocker Drive Encryption.");

        /// <summary>
        /// Group Policy settings requires that you use a smart card-based key protector with BitLocker Drive Encryption.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_CERT_MUST_BE_HW = new HRESULT("0x80310074", "FVE_E_POLICY_USER_CERT_MUST_BE_HW", "Group Policy settings requires that you use a smart card-based key protector with BitLocker Drive Encryption.");

        /// <summary>
        /// Group Policy settings do not permit BitLocker-protected fixed data drives to be automatically unlocked.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_CONFIGURE_FDV_AUTOUNLOCK_NOT_ALLOWED = new HRESULT("0x80310075", "FVE_E_POLICY_USER_CONFIGURE_FDV_AUTOUNLOCK_NOT_ALLOWED", "Group Policy settings do not permit BitLocker-protected fixed data drives to be automatically unlocked.");

        /// <summary>
        /// Group Policy settings do not permit BitLocker-protected removable data drives to be automatically unlocked.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_CONFIGURE_RDV_AUTOUNLOCK_NOT_ALLOWED = new HRESULT("0x80310076", "FVE_E_POLICY_USER_CONFIGURE_RDV_AUTOUNLOCK_NOT_ALLOWED", "Group Policy settings do not permit BitLocker-protected removable data drives to be automatically unlocked.");

        /// <summary>
        /// Group Policy settings do not permit you to configure BitLocker Drive Encryption on removable data drives.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_CONFIGURE_RDV_NOT_ALLOWED = new HRESULT("0x80310077", "FVE_E_POLICY_USER_CONFIGURE_RDV_NOT_ALLOWED", "Group Policy settings do not permit you to configure BitLocker Drive Encryption on removable data drives.");

        /// <summary>
        /// Group Policy settings do not permit you to turn on BitLocker Drive Encryption on removable data drives. Please contact your system administrator if you need to turn on BitLocker.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_ENABLE_RDV_NOT_ALLOWED = new HRESULT("0x80310078", "FVE_E_POLICY_USER_ENABLE_RDV_NOT_ALLOWED", "Group Policy settings do not permit you to turn on BitLocker Drive Encryption on removable data drives. Please contact your system administrator if you need to turn on BitLocker.");

        /// <summary>
        /// Group Policy settings do not permit turning off BitLocker Drive Encryption on removable data drives. Please contact your system administrator if you need to turn off BitLocker.
        /// </summary>
        public static HRESULT FVE_E_POLICY_USER_DISABLE_RDV_NOT_ALLOWED = new HRESULT("0x80310079", "FVE_E_POLICY_USER_DISABLE_RDV_NOT_ALLOWED", "Group Policy settings do not permit turning off BitLocker Drive Encryption on removable data drives. Please contact your system administrator if you need to turn off BitLocker.");

        /// <summary>
        /// Your password does not meet minimum password length requirements. By default, passwords must be at least 8 characters in length. Check with your system administrator for the password length requirement in your organization.
        /// </summary>
        public static HRESULT FVE_E_POLICY_INVALID_PASSPHRASE_LENGTH = new HRESULT("0x80310080", "FVE_E_POLICY_INVALID_PASSPHRASE_LENGTH", "Your password does not meet minimum password length requirements. By default, passwords must be at least 8 characters in length. Check with your system administrator for the password length requirement in your organization.");

        /// <summary>
        /// Your password does not meet the complexity requirements set by your system administrator. Try adding upper and lowercase characters, numbers, and symbols.
        /// </summary>
        public static HRESULT FVE_E_POLICY_PASSPHRASE_TOO_SIMPLE = new HRESULT("0x80310081", "FVE_E_POLICY_PASSPHRASE_TOO_SIMPLE", "Your password does not meet the complexity requirements set by your system administrator. Try adding upper and lowercase characters, numbers, and symbols.");

        /// <summary>
        /// This drive cannot be encrypted because it is reserved for Windows System Recovery Options.
        /// </summary>
        public static HRESULT FVE_E_RECOVERY_PARTITION = new HRESULT("0x80310082", "FVE_E_RECOVERY_PARTITION", "This drive cannot be encrypted because it is reserved for Windows System Recovery Options.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive because of conflicting Group Policy settings. BitLocker cannot be configured to automatically unlock fixed data drives when user recovery options are disabled. If you want BitLocker-protected fixed data drives to be automatically unlocked after key validation has occurred, please ask your system administrator to resolve the settings conflict before enabling BitLocker.
        /// </summary>
        public static HRESULT FVE_E_POLICY_CONFLICT_FDV_RK_OFF_AUK_ON = new HRESULT("0x80310083", "FVE_E_POLICY_CONFLICT_FDV_RK_OFF_AUK_ON", "BitLocker Drive Encryption cannot be applied to this drive because of conflicting Group Policy settings. BitLocker cannot be configured to automatically unlock fixed data drives when user recovery options are disabled. If you want BitLocker-protected fixed data drives to be automatically unlocked after key validation has occurred, please ask your system administrator to resolve the settings conflict before enabling BitLocker.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive because of conflicting Group Policy settings. BitLocker cannot be configured to automatically unlock removable data drives when user recovery option are disabled. If you want BitLocker-protected removable data drives to be automatically unlocked after key validation has occured, please ask your system administrator to resolve the settings conflict before enabling BitLocker.
        /// </summary>
        public static HRESULT FVE_E_POLICY_CONFLICT_RDV_RK_OFF_AUK_ON = new HRESULT("0x80310084", "FVE_E_POLICY_CONFLICT_RDV_RK_OFF_AUK_ON", "BitLocker Drive Encryption cannot be applied to this drive because of conflicting Group Policy settings. BitLocker cannot be configured to automatically unlock removable data drives when user recovery option are disabled. If you want BitLocker-protected removable data drives to be automatically unlocked after key validation has occured, please ask your system administrator to resolve the settings conflict before enabling BitLocker.");

        /// <summary>
        /// The Enhanced Key Usage (EKU) attribute of the specified certificate does not permit it to be used for BitLocker Drive Encryption. BitLocker does not require that a certificate have an EKU attribute, but if one is configured it must be set to an object identifier (OID) that matches the OID configured for BitLocker.
        /// </summary>
        public static HRESULT FVE_E_NON_BITLOCKER_OID = new HRESULT("0x80310085", "FVE_E_NON_BITLOCKER_OID", "The Enhanced Key Usage (EKU) attribute of the specified certificate does not permit it to be used for BitLocker Drive Encryption. BitLocker does not require that a certificate have an EKU attribute, but if one is configured it must be set to an object identifier (OID) that matches the OID configured for BitLocker.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive as currently configured because of Group Policy settings. The certificate you provided for drive encryption is self-signed. Current Group Policy settings do not permit the use of self-signed certificates. Obtain a new certificate from your certification authority before attempting to enable BitLocker. 
        /// </summary>
        public static HRESULT FVE_E_POLICY_PROHIBITS_SELFSIGNED = new HRESULT("0x80310086", "FVE_E_POLICY_PROHIBITS_SELFSIGNED", "BitLocker Drive Encryption cannot be applied to this drive as currently configured because of Group Policy settings. The certificate you provided for drive encryption is self-signed. Current Group Policy settings do not permit the use of self-signed certificates. Obtain a new certificate from your certification authority before attempting to enable BitLocker. ");

        /// <summary>
        /// BitLocker Encryption cannot be applied to this drive because of conflicting Group Policy settings. When write access to drives not protected by BitLocker is denied, the use of a USB startup key cannot be required. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker. 
        /// </summary>
        public static HRESULT FVE_E_POLICY_CONFLICT_RO_AND_STARTUP_KEY_REQUIRED = new HRESULT("0x80310087", "FVE_E_POLICY_CONFLICT_RO_AND_STARTUP_KEY_REQUIRED", "BitLocker Encryption cannot be applied to this drive because of conflicting Group Policy settings. When write access to drives not protected by BitLocker is denied, the use of a USB startup key cannot be required. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker. ");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on operating system drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker. 
        /// </summary>
        public static HRESULT FVE_E_CONV_RECOVERY_FAILED = new HRESULT("0x80310088", "FVE_E_CONV_RECOVERY_FAILED", "BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on operating system drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker. ");

        /// <summary>
        /// The requested virtualization size is too big.
        /// </summary>
        public static HRESULT FVE_E_VIRTUALIZED_SPACE_TOO_BIG = new HRESULT("0x80310089", "FVE_E_VIRTUALIZED_SPACE_TOO_BIG", "The requested virtualization size is too big.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on operating system drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker.
        /// </summary>
        public static HRESULT FVE_E_POLICY_CONFLICT_OSV_RP_OFF_ADB_ON = new HRESULT("0x80310090", "FVE_E_POLICY_CONFLICT_OSV_RP_OFF_ADB_ON", "BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on operating system drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on fixed data drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker.
        /// </summary>
        public static HRESULT FVE_E_POLICY_CONFLICT_FDV_RP_OFF_ADB_ON = new HRESULT("0x80310091", "FVE_E_POLICY_CONFLICT_FDV_RP_OFF_ADB_ON", "BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on fixed data drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on removable data drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker. 
        /// </summary>
        public static HRESULT FVE_E_POLICY_CONFLICT_RDV_RP_OFF_ADB_ON = new HRESULT("0x80310092", "FVE_E_POLICY_CONFLICT_RDV_RP_OFF_ADB_ON", "BitLocker Drive Encryption cannot be applied to this drive because there are conflicting Group Policy settings for recovery options on removable data drives. Storing recovery information to Active Directory Domain Services cannot be required when the generation of recovery passwords is not permitted. Please have your system administrator resolve these policy conflicts before attempting to enable BitLocker. ");

        /// <summary>
        /// The Key Usage (KU) attribute of the specified certificate does not permit it to be used for BitLocker Drive Encryption. BitLocker does not require that a certificate have a KU attribute, but if one is configured it must be set to either Key Encipherment or Key Agreement.
        /// </summary>
        public static HRESULT FVE_E_NON_BITLOCKER_KU = new HRESULT("0x80310093", "FVE_E_NON_BITLOCKER_KU", "The Key Usage (KU) attribute of the specified certificate does not permit it to be used for BitLocker Drive Encryption. BitLocker does not require that a certificate have a KU attribute, but if one is configured it must be set to either Key Encipherment or Key Agreement.");

        /// <summary>
        /// The private key associated with the specified certificate cannot be authorized. The private key authorization was either not provided or the provided authorization was invalid.
        /// </summary>
        public static HRESULT FVE_E_PRIVATEKEY_AUTH_FAILED = new HRESULT("0x80310094", "FVE_E_PRIVATEKEY_AUTH_FAILED", "The private key associated with the specified certificate cannot be authorized. The private key authorization was either not provided or the provided authorization was invalid.");

        /// <summary>
        /// Removal of the data recovery agent certificate must be done using the Certificates snap-in.
        /// </summary>
        public static HRESULT FVE_E_REMOVAL_OF_DRA_FAILED = new HRESULT("0x80310095", "FVE_E_REMOVAL_OF_DRA_FAILED", "Removal of the data recovery agent certificate must be done using the Certificates snap-in.");

        /// <summary>
        /// This drive was encrypted using the version of BitLocker Drive Encryption included with Windows Vista and Windows Server 2008 which does not support organizational identifiers. To specify organizational identifiers for this drive upgrade the drive encryption to the latest version using the &quot;manage-bde -upgrade&quot; command.
        /// </summary>
        public static HRESULT FVE_E_OPERATION_NOT_SUPPORTED_ON_VISTA_VOLUME = new HRESULT("0x80310096", "FVE_E_OPERATION_NOT_SUPPORTED_ON_VISTA_VOLUME", "This drive was encrypted using the version of BitLocker Drive Encryption included with Windows Vista and Windows Server 2008 which does not support organizational identifiers. To specify organizational identifiers for this drive upgrade the drive encryption to the latest version using the \"manage-bde -upgrade\" command.");

        /// <summary>
        /// The drive cannot be locked because it is automatically unlocked on this computer. Remove the automatic unlock protector to lock this drive.
        /// </summary>
        public static HRESULT FVE_E_CANT_LOCK_AUTOUNLOCK_ENABLED_VOLUME = new HRESULT("0x80310097", "FVE_E_CANT_LOCK_AUTOUNLOCK_ENABLED_VOLUME", "The drive cannot be locked because it is automatically unlocked on this computer. Remove the automatic unlock protector to lock this drive.");

        /// <summary>
        /// The default Bitlocker Key Derivation Function SP800-56A for ECC smart cards is not supported by your smart card. The Group Policy setting requiring FIPS-compliance prevents BitLocker from using any other key derivation function for encryption. You have to use a FIPS compliant smart card in FIPS restricted environments.
        /// </summary>
        public static HRESULT FVE_E_FIPS_HASH_KDF_NOT_ALLOWED = new HRESULT("0x80310098", "FVE_E_FIPS_HASH_KDF_NOT_ALLOWED", "The default Bitlocker Key Derivation Function SP800-56A for ECC smart cards is not supported by your smart card. The Group Policy setting requiring FIPS-compliance prevents BitLocker from using any other key derivation function for encryption. You have to use a FIPS compliant smart card in FIPS restricted environments.");

        /// <summary>
        /// The BitLocker encryption key could not be obtained from the Trusted Platform Module (TPM) and enhanced PIN. Try using a PIN containing only numerals.
        /// </summary>
        public static HRESULT FVE_E_ENH_PIN_INVALID = new HRESULT("0x80310099", "FVE_E_ENH_PIN_INVALID", "The BitLocker encryption key could not be obtained from the Trusted Platform Module (TPM) and enhanced PIN. Try using a PIN containing only numerals.");

        /// <summary>
        /// The requested TPM PIN contains invalid characters.
        /// </summary>
        public static HRESULT FVE_E_INVALID_PIN_CHARS = new HRESULT("0x8031009A", "FVE_E_INVALID_PIN_CHARS", "The requested TPM PIN contains invalid characters.");

        /// <summary>
        /// The management information stored on the drive contained an unknown type. If you are using an old version of Windows, try accessing the drive from the latest version.
        /// </summary>
        public static HRESULT FVE_E_INVALID_DATUM_TYPE = new HRESULT("0x8031009B", "FVE_E_INVALID_DATUM_TYPE", "The management information stored on the drive contained an unknown type. If you are using an old version of Windows, try accessing the drive from the latest version.");

        /// <summary>
        /// The feature is only supported on EFI systems.
        /// </summary>
        public static HRESULT FVE_E_EFI_ONLY = new HRESULT("0x8031009C", "FVE_E_EFI_ONLY", "The feature is only supported on EFI systems.");

        /// <summary>
        /// More than one Network Key Protector certificate has been found on the system.
        /// </summary>
        public static HRESULT FVE_E_MULTIPLE_NKP_CERTS = new HRESULT("0x8031009D", "FVE_E_MULTIPLE_NKP_CERTS", "More than one Network Key Protector certificate has been found on the system.");

        /// <summary>
        /// Removal of the Network Key Protector certificate must be done using the Certificates snap-in.
        /// </summary>
        public static HRESULT FVE_E_REMOVAL_OF_NKP_FAILED = new HRESULT("0x8031009E", "FVE_E_REMOVAL_OF_NKP_FAILED", "Removal of the Network Key Protector certificate must be done using the Certificates snap-in.");

        /// <summary>
        /// An invalid certificate has been found in the Network Key Protector certificate store.
        /// </summary>
        public static HRESULT FVE_E_INVALID_NKP_CERT = new HRESULT("0x8031009F", "FVE_E_INVALID_NKP_CERT", "An invalid certificate has been found in the Network Key Protector certificate store.");

        /// <summary>
        /// This drive isn't protected with a PIN.
        /// </summary>
        public static HRESULT FVE_E_NO_EXISTING_PIN = new HRESULT("0x803100A0", "FVE_E_NO_EXISTING_PIN", "This drive isn't protected with a PIN.");

        /// <summary>
        /// Please enter the correct current PIN.
        /// </summary>
        public static HRESULT FVE_E_PROTECTOR_CHANGE_PIN_MISMATCH = new HRESULT("0x803100A1", "FVE_E_PROTECTOR_CHANGE_PIN_MISMATCH", "Please enter the correct current PIN.");

        /// <summary>
        /// You must be logged on with an administrator account to change the PIN or password. Click the link to reset the PIN or password as an administrator.
        /// </summary>
        public static HRESULT FVE_E_PROTECTOR_CHANGE_BY_STD_USER_DISALLOWED = new HRESULT("0x803100A2", "FVE_E_PROTECTOR_CHANGE_BY_STD_USER_DISALLOWED", "You must be logged on with an administrator account to change the PIN or password. Click the link to reset the PIN or password as an administrator.");

        /// <summary>
        /// BitLocker has disabled PIN and password changes after too many failed requests. Click the link to reset the PIN or password as an administrator.
        /// </summary>
        public static HRESULT FVE_E_PROTECTOR_CHANGE_MAX_PIN_CHANGE_ATTEMPTS_REACHED = new HRESULT("0x803100A3", "FVE_E_PROTECTOR_CHANGE_MAX_PIN_CHANGE_ATTEMPTS_REACHED", "BitLocker has disabled PIN and password changes after too many failed requests. Click the link to reset the PIN or password as an administrator.");

        /// <summary>
        /// Your system administrator requires that passwords contain only printable ASCII characters. This includes unaccented letters (A-Z, a-z), numbers (0-9), space, arithmetic signs, common punctuation, separators, and the following symbols: # $ & @ ^ _ ~ .
        /// </summary>
        public static HRESULT FVE_E_POLICY_PASSPHRASE_REQUIRES_ASCII = new HRESULT("0x803100A4", "FVE_E_POLICY_PASSPHRASE_REQUIRES_ASCII", "Your system administrator requires that passwords contain only printable ASCII characters. This includes unaccented letters (A-Z, a-z), numbers (0-9), space, arithmetic signs, common punctuation, separators, and the following symbols: # $ & @ ^ _ ~ .");

        /// <summary>
        /// BitLocker Drive Encryption only supports used space only encryption on thin provisioned storage.
        /// </summary>
        public static HRESULT FVE_E_FULL_ENCRYPTION_NOT_ALLOWED_ON_TP_STORAGE = new HRESULT("0x803100A5", "FVE_E_FULL_ENCRYPTION_NOT_ALLOWED_ON_TP_STORAGE", "BitLocker Drive Encryption only supports used space only encryption on thin provisioned storage.");

        /// <summary>
        /// BitLocker Drive Encryption does not support wiping free space on thin provisioned storage.
        /// </summary>
        public static HRESULT FVE_E_WIPE_NOT_ALLOWED_ON_TP_STORAGE = new HRESULT("0x803100A6", "FVE_E_WIPE_NOT_ALLOWED_ON_TP_STORAGE", "BitLocker Drive Encryption does not support wiping free space on thin provisioned storage.");

        /// <summary>
        /// The required authentication key length is not supported by the drive.
        /// </summary>
        public static HRESULT FVE_E_KEY_LENGTH_NOT_SUPPORTED_BY_EDRIVE = new HRESULT("0x803100A7", "FVE_E_KEY_LENGTH_NOT_SUPPORTED_BY_EDRIVE", "The required authentication key length is not supported by the drive.");

        /// <summary>
        /// This drive isn't protected with a password.
        /// </summary>
        public static HRESULT FVE_E_NO_EXISTING_PASSPHRASE = new HRESULT("0x803100A8", "FVE_E_NO_EXISTING_PASSPHRASE", "This drive isn't protected with a password.");

        /// <summary>
        /// Please enter the correct current password.
        /// </summary>
        public static HRESULT FVE_E_PROTECTOR_CHANGE_PASSPHRASE_MISMATCH = new HRESULT("0x803100A9", "FVE_E_PROTECTOR_CHANGE_PASSPHRASE_MISMATCH", "Please enter the correct current password.");

        /// <summary>
        /// The password cannot exceed 256 characters.
        /// </summary>
        public static HRESULT FVE_E_PASSPHRASE_TOO_LONG = new HRESULT("0x803100AA", "FVE_E_PASSPHRASE_TOO_LONG", "The password cannot exceed 256 characters.");

        /// <summary>
        /// A password key protector cannot be added because a TPM protector exists on the drive.
        /// </summary>
        public static HRESULT FVE_E_NO_PASSPHRASE_WITH_TPM = new HRESULT("0x803100AB", "FVE_E_NO_PASSPHRASE_WITH_TPM", "A password key protector cannot be added because a TPM protector exists on the drive.");

        /// <summary>
        /// A TPM key protector cannot be added because a password protector exists on the drive.
        /// </summary>
        public static HRESULT FVE_E_NO_TPM_WITH_PASSPHRASE = new HRESULT("0x803100AC", "FVE_E_NO_TPM_WITH_PASSPHRASE", "A TPM key protector cannot be added because a password protector exists on the drive.");

        /// <summary>
        /// This command can only be performed from the coordinator node for the specified CSV volume.
        /// </summary>
        public static HRESULT FVE_E_NOT_ALLOWED_ON_CSV_STACK = new HRESULT("0x803100AD", "FVE_E_NOT_ALLOWED_ON_CSV_STACK", "This command can only be performed from the coordinator node for the specified CSV volume.");

        /// <summary>
        /// This command cannot be performed on a volume when it is part of a cluster.
        /// </summary>
        public static HRESULT FVE_E_NOT_ALLOWED_ON_CLUSTER = new HRESULT("0x803100AE", "FVE_E_NOT_ALLOWED_ON_CLUSTER", "This command cannot be performed on a volume when it is part of a cluster.");

        /// <summary>
        /// BitLocker did not revert to using BitLocker software encryption due to group policy configuration.
        /// </summary>
        public static HRESULT FVE_E_EDRIVE_NO_FAILOVER_TO_SW = new HRESULT("0x803100AF", "FVE_E_EDRIVE_NO_FAILOVER_TO_SW", "BitLocker did not revert to using BitLocker software encryption due to group policy configuration.");

        /// <summary>
        /// The drive cannot be managed by BitLocker because the drive's hardware encryption feature is already in use.
        /// </summary>
        public static HRESULT FVE_E_EDRIVE_BAND_IN_USE = new HRESULT("0x803100B0", "FVE_E_EDRIVE_BAND_IN_USE", "The drive cannot be managed by BitLocker because the drive's hardware encryption feature is already in use.");

        /// <summary>
        /// Group Policy settings do not allow the use of hardware-based encryption.
        /// </summary>
        public static HRESULT FVE_E_EDRIVE_DISALLOWED_BY_GP = new HRESULT("0x803100B1", "FVE_E_EDRIVE_DISALLOWED_BY_GP", "Group Policy settings do not allow the use of hardware-based encryption.");

        /// <summary>
        /// The drive specified does not support hardware-based encryption.
        /// </summary>
        public static HRESULT FVE_E_EDRIVE_INCOMPATIBLE_VOLUME = new HRESULT("0x803100B2", "FVE_E_EDRIVE_INCOMPATIBLE_VOLUME", "The drive specified does not support hardware-based encryption.");

        /// <summary>
        /// BitLocker cannot be upgraded during disk encryption or decryption.
        /// </summary>
        public static HRESULT FVE_E_NOT_ALLOWED_TO_UPGRADE_WHILE_CONVERTING = new HRESULT("0x803100B3", "FVE_E_NOT_ALLOWED_TO_UPGRADE_WHILE_CONVERTING", "BitLocker cannot be upgraded during disk encryption or decryption.");

        /// <summary>
        /// Discovery Volumes are not supported for volumes using hardware encryption.
        /// </summary>
        public static HRESULT FVE_E_EDRIVE_DV_NOT_SUPPORTED = new HRESULT("0x803100B4", "FVE_E_EDRIVE_DV_NOT_SUPPORTED", "Discovery Volumes are not supported for volumes using hardware encryption.");

        /// <summary>
        /// No pre-boot keyboard detected. The user may not be able to provide required input to unlock the volume.
        /// </summary>
        public static HRESULT FVE_E_NO_PREBOOT_KEYBOARD_DETECTED = new HRESULT("0x803100B5", "FVE_E_NO_PREBOOT_KEYBOARD_DETECTED", "No pre-boot keyboard detected. The user may not be able to provide required input to unlock the volume.");

        /// <summary>
        /// No pre-boot keyboard or Windows Recovery Environment detected. The user may not be able to provide required input to unlock the volume.
        /// </summary>
        public static HRESULT FVE_E_NO_PREBOOT_KEYBOARD_OR_WINRE_DETECTED = new HRESULT("0x803100B6", "FVE_E_NO_PREBOOT_KEYBOARD_OR_WINRE_DETECTED", "No pre-boot keyboard or Windows Recovery Environment detected. The user may not be able to provide required input to unlock the volume.");

        /// <summary>
        /// Group Policy settings require the creation of a startup PIN, but a pre-boot keyboard is not available on this device. The user may not be able to provide required input to unlock the volume.
        /// </summary>
        public static HRESULT FVE_E_POLICY_REQUIRES_STARTUP_PIN_ON_TOUCH_DEVICE = new HRESULT("0x803100B7", "FVE_E_POLICY_REQUIRES_STARTUP_PIN_ON_TOUCH_DEVICE", "Group Policy settings require the creation of a startup PIN, but a pre-boot keyboard is not available on this device. The user may not be able to provide required input to unlock the volume.");

        /// <summary>
        /// Group Policy settings require the creation of a recovery password, but neither a pre-boot keyboard nor Windows Recovery Environment is available on this device. The user may not be able to provide required input to unlock the volume.
        /// </summary>
        public static HRESULT FVE_E_POLICY_REQUIRES_RECOVERY_PASSWORD_ON_TOUCH_DEVICE = new HRESULT("0x803100B8", "FVE_E_POLICY_REQUIRES_RECOVERY_PASSWORD_ON_TOUCH_DEVICE", "Group Policy settings require the creation of a recovery password, but neither a pre-boot keyboard nor Windows Recovery Environment is available on this device. The user may not be able to provide required input to unlock the volume.");

        /// <summary>
        /// Wipe of free space is not currently taking place.
        /// </summary>
        public static HRESULT FVE_E_WIPE_CANCEL_NOT_APPLICABLE = new HRESULT("0x803100B9", "FVE_E_WIPE_CANCEL_NOT_APPLICABLE", "Wipe of free space is not currently taking place.");

        /// <summary>
        /// BitLocker cannot use Secure Boot for platform integrity because Secure Boot has been disabled.
        /// </summary>
        public static HRESULT FVE_E_SECUREBOOT_DISABLED = new HRESULT("0x803100BA", "FVE_E_SECUREBOOT_DISABLED", "BitLocker cannot use Secure Boot for platform integrity because Secure Boot has been disabled.");

        /// <summary>
        /// BitLocker cannot use Secure Boot for platform integrity because the Secure Boot configuration does not meet the requirements for BitLocker.
        /// </summary>
        public static HRESULT FVE_E_SECUREBOOT_CONFIGURATION_INVALID = new HRESULT("0x803100BB", "FVE_E_SECUREBOOT_CONFIGURATION_INVALID", "BitLocker cannot use Secure Boot for platform integrity because the Secure Boot configuration does not meet the requirements for BitLocker.");

        /// <summary>
        /// Your computer doesn't support BitLocker hardware-based encryption. Check with your computer manufacturer for firmware updates.
        /// </summary>
        public static HRESULT FVE_E_EDRIVE_DRY_RUN_FAILED = new HRESULT("0x803100BC", "FVE_E_EDRIVE_DRY_RUN_FAILED", "Your computer doesn't support BitLocker hardware-based encryption. Check with your computer manufacturer for firmware updates.");

        /// <summary>
        /// BitLocker cannot be enabled on the volume because it contains a Volume Shadow Copy. Remove all Volume Shadow Copies before encrypting the volume.
        /// </summary>
        public static HRESULT FVE_E_SHADOW_COPY_PRESENT = new HRESULT("0x803100BD", "FVE_E_SHADOW_COPY_PRESENT", "BitLocker cannot be enabled on the volume because it contains a Volume Shadow Copy. Remove all Volume Shadow Copies before encrypting the volume.");

        /// <summary>
        /// BitLocker Drive Encryption cannot be applied to this drive because the Group Policy setting for Enhanced Boot Configuration Data contains invalid data. Please have your system administrator resolve this invalid configuration before attempting to enable BitLocker.
        /// </summary>
        public static HRESULT FVE_E_POLICY_INVALID_ENHANCED_BCD_SETTINGS = new HRESULT("0x803100BE", "FVE_E_POLICY_INVALID_ENHANCED_BCD_SETTINGS", "BitLocker Drive Encryption cannot be applied to this drive because the Group Policy setting for Enhanced Boot Configuration Data contains invalid data. Please have your system administrator resolve this invalid configuration before attempting to enable BitLocker.");

        /// <summary>
        /// This PC's firmware is not capable of supporting hardware encryption.
        /// </summary>
        public static HRESULT FVE_E_EDRIVE_INCOMPATIBLE_FIRMWARE = new HRESULT("0x803100BF", "FVE_E_EDRIVE_INCOMPATIBLE_FIRMWARE", "This PC's firmware is not capable of supporting hardware encryption.");

        /// <summary>
        /// BitLocker has disabled password changes after too many failed requests. Click the link to reset the password as an administrator.
        /// </summary>
        public static HRESULT FVE_E_PROTECTOR_CHANGE_MAX_PASSPHRASE_CHANGE_ATTEMPTS_REACHED = new HRESULT("0x803100C0", "FVE_E_PROTECTOR_CHANGE_MAX_PASSPHRASE_CHANGE_ATTEMPTS_REACHED", "BitLocker has disabled password changes after too many failed requests. Click the link to reset the password as an administrator.");

        /// <summary>
        /// You must be logged on with an administrator account to change the password. Click the link to reset the password as an administrator.
        /// </summary>
        public static HRESULT FVE_E_PASSPHRASE_PROTECTOR_CHANGE_BY_STD_USER_DISALLOWED = new HRESULT("0x803100C1", "FVE_E_PASSPHRASE_PROTECTOR_CHANGE_BY_STD_USER_DISALLOWED", "You must be logged on with an administrator account to change the password. Click the link to reset the password as an administrator.");

        /// <summary>
        /// BitLocker cannot save the recovery password because the specified Microsoft account is Suspended.
        /// </summary>
        public static HRESULT FVE_E_LIVEID_ACCOUNT_SUSPENDED = new HRESULT("0x803100C2", "FVE_E_LIVEID_ACCOUNT_SUSPENDED", "BitLocker cannot save the recovery password because the specified Microsoft account is Suspended.");

        /// <summary>
        /// BitLocker cannot save the recovery password because the specified MIcrosoft account is Blocked.
        /// </summary>
        public static HRESULT FVE_E_LIVEID_ACCOUNT_BLOCKED = new HRESULT("0x803100C3", "FVE_E_LIVEID_ACCOUNT_BLOCKED", "BitLocker cannot save the recovery password because the specified MIcrosoft account is Blocked.");

        /// <summary>
        /// This PC is not provisioned to support device encryption. Please enable BitLocker on all volumes to comply with device encryption policy.
        /// </summary>
        public static HRESULT FVE_E_NOT_PROVISIONED_ON_ALL_VOLUMES = new HRESULT("0x803100C4", "FVE_E_NOT_PROVISIONED_ON_ALL_VOLUMES", "This PC is not provisioned to support device encryption. Please enable BitLocker on all volumes to comply with device encryption policy.");

        /// <summary>
        /// This PC cannot support device encryption because unencrypted fixed data volumes are present.
        /// </summary>
        public static HRESULT FVE_E_DE_FIXED_DATA_NOT_SUPPORTED = new HRESULT("0x803100C5", "FVE_E_DE_FIXED_DATA_NOT_SUPPORTED", "This PC cannot support device encryption because unencrypted fixed data volumes are present.");

        /// <summary>
        /// This PC does not meet the hardware requirements to support device encryption.
        /// </summary>
        public static HRESULT FVE_E_DE_HARDWARE_NOT_COMPLIANT = new HRESULT("0x803100C6", "FVE_E_DE_HARDWARE_NOT_COMPLIANT", "This PC does not meet the hardware requirements to support device encryption.");

        /// <summary>
        /// This PC cannot support device encryption because WinRE is not properly configured.
        /// </summary>
        public static HRESULT FVE_E_DE_WINRE_NOT_CONFIGURED = new HRESULT("0x803100C7", "FVE_E_DE_WINRE_NOT_CONFIGURED", "This PC cannot support device encryption because WinRE is not properly configured.");

        /// <summary>
        /// Protection is enabled on the volume but has been suspended. This is likely to have happened due to an update being applied to your system. Please try again after a reboot.
        /// </summary>
        public static HRESULT FVE_E_DE_PROTECTION_SUSPENDED = new HRESULT("0x803100C8", "FVE_E_DE_PROTECTION_SUSPENDED", "Protection is enabled on the volume but has been suspended. This is likely to have happened due to an update being applied to your system. Please try again after a reboot.");

        /// <summary>
        /// This PC is not provisioned to support device encryption.
        /// </summary>
        public static HRESULT FVE_E_DE_OS_VOLUME_NOT_PROTECTED = new HRESULT("0x803100C9", "FVE_E_DE_OS_VOLUME_NOT_PROTECTED", "This PC is not provisioned to support device encryption.");

        /// <summary>
        /// Device Lock has been triggered due to too many incorrect password attempts.
        /// </summary>
        public static HRESULT FVE_E_DE_DEVICE_LOCKEDOUT = new HRESULT("0x803100CA", "FVE_E_DE_DEVICE_LOCKEDOUT", "Device Lock has been triggered due to too many incorrect password attempts.");

        /// <summary>
        /// Protection has not been enabled on the volume. Enabling protection requires a connected account. If you already have a connected account and are seeing this error, please refer to the event log for more information.
        /// </summary>
        public static HRESULT FVE_E_DE_PROTECTION_NOT_YET_ENABLED = new HRESULT("0x803100CB", "FVE_E_DE_PROTECTION_NOT_YET_ENABLED", "Protection has not been enabled on the volume. Enabling protection requires a connected account. If you already have a connected account and are seeing this error, please refer to the event log for more information.");

        /// <summary>
        /// Your PIN can only contain numbers from 0 to 9.
        /// </summary>
        public static HRESULT FVE_E_INVALID_PIN_CHARS_DETAILED = new HRESULT("0x803100CC", "FVE_E_INVALID_PIN_CHARS_DETAILED", "Your PIN can only contain numbers from 0 to 9.");

        /// <summary>
        /// BitLocker cannot use hardware replay protection because no counter is available on your PC.
        /// </summary>
        public static HRESULT FVE_E_DEVICE_LOCKOUT_COUNTER_UNAVAILABLE = new HRESULT("0x803100CD", "FVE_E_DEVICE_LOCKOUT_COUNTER_UNAVAILABLE", "BitLocker cannot use hardware replay protection because no counter is available on your PC.");

        /// <summary>
        /// Device Lockout state validation failed due to counter mismatch.
        /// </summary>
        public static HRESULT FVE_E_DEVICELOCKOUT_COUNTER_MISMATCH = new HRESULT("0x803100CE", "FVE_E_DEVICELOCKOUT_COUNTER_MISMATCH", "Device Lockout state validation failed due to counter mismatch.");
        #endregion

        #region COM Error Codes (FWP, WS, NDIS, HyperV)
        /// <summary>
        /// The callout does not exist.
        /// </summary>
        public static HRESULT FWP_E_CALLOUT_NOT_FOUND = new HRESULT("0x80320001", "FWP_E_CALLOUT_NOT_FOUND", "The callout does not exist.");

        /// <summary>
        /// The filter condition does not exist.
        /// </summary>
        public static HRESULT FWP_E_CONDITION_NOT_FOUND = new HRESULT("0x80320002", "FWP_E_CONDITION_NOT_FOUND", "The filter condition does not exist.");

        /// <summary>
        /// The filter does not exist.
        /// </summary>
        public static HRESULT FWP_E_FILTER_NOT_FOUND = new HRESULT("0x80320003", "FWP_E_FILTER_NOT_FOUND", "The filter does not exist.");

        /// <summary>
        /// The layer does not exist.
        /// </summary>
        public static HRESULT FWP_E_LAYER_NOT_FOUND = new HRESULT("0x80320004", "FWP_E_LAYER_NOT_FOUND", "The layer does not exist.");

        /// <summary>
        /// The provider does not exist.
        /// </summary>
        public static HRESULT FWP_E_PROVIDER_NOT_FOUND = new HRESULT("0x80320005", "FWP_E_PROVIDER_NOT_FOUND", "The provider does not exist.");

        /// <summary>
        /// The provider context does not exist.
        /// </summary>
        public static HRESULT FWP_E_PROVIDER_CONTEXT_NOT_FOUND = new HRESULT("0x80320006", "FWP_E_PROVIDER_CONTEXT_NOT_FOUND", "The provider context does not exist.");

        /// <summary>
        /// The sublayer does not exist.
        /// </summary>
        public static HRESULT FWP_E_SUBLAYER_NOT_FOUND = new HRESULT("0x80320007", "FWP_E_SUBLAYER_NOT_FOUND", "The sublayer does not exist.");

        /// <summary>
        /// The object does not exist.
        /// </summary>
        public static HRESULT FWP_E_NOT_FOUND = new HRESULT("0x80320008", "FWP_E_NOT_FOUND", "The object does not exist.");

        /// <summary>
        /// An object with that GUID or LUID already exists.
        /// </summary>
        public static HRESULT FWP_E_ALREADY_EXISTS = new HRESULT("0x80320009", "FWP_E_ALREADY_EXISTS", "An object with that GUID or LUID already exists.");

        /// <summary>
        /// The object is referenced by other objects so cannot be deleted.
        /// </summary>
        public static HRESULT FWP_E_IN_USE = new HRESULT("0x8032000A", "FWP_E_IN_USE", "The object is referenced by other objects so cannot be deleted.");

        /// <summary>
        /// The call is not allowed from within a dynamic session.
        /// </summary>
        public static HRESULT FWP_E_DYNAMIC_SESSION_IN_PROGRESS = new HRESULT("0x8032000B", "FWP_E_DYNAMIC_SESSION_IN_PROGRESS", "The call is not allowed from within a dynamic session.");

        /// <summary>
        /// The call was made from the wrong session so cannot be completed.
        /// </summary>
        public static HRESULT FWP_E_WRONG_SESSION = new HRESULT("0x8032000C", "FWP_E_WRONG_SESSION", "The call was made from the wrong session so cannot be completed.");

        /// <summary>
        /// The call must be made from within an explicit transaction.
        /// </summary>
        public static HRESULT FWP_E_NO_TXN_IN_PROGRESS = new HRESULT("0x8032000D", "FWP_E_NO_TXN_IN_PROGRESS", "The call must be made from within an explicit transaction.");

        /// <summary>
        /// The call is not allowed from within an explicit transaction.
        /// </summary>
        public static HRESULT FWP_E_TXN_IN_PROGRESS = new HRESULT("0x8032000E", "FWP_E_TXN_IN_PROGRESS", "The call is not allowed from within an explicit transaction.");

        /// <summary>
        /// The explicit transaction has been forcibly canceled.
        /// </summary>
        public static HRESULT FWP_E_TXN_ABORTED = new HRESULT("0x8032000F", "FWP_E_TXN_ABORTED", "The explicit transaction has been forcibly canceled.");

        /// <summary>
        /// The session has been canceled.
        /// </summary>
        public static HRESULT FWP_E_SESSION_ABORTED = new HRESULT("0x80320010", "FWP_E_SESSION_ABORTED", "The session has been canceled.");

        /// <summary>
        /// The call is not allowed from within a read-only transaction.
        /// </summary>
        public static HRESULT FWP_E_INCOMPATIBLE_TXN = new HRESULT("0x80320011", "FWP_E_INCOMPATIBLE_TXN", "The call is not allowed from within a read-only transaction.");

        /// <summary>
        /// The call timed out while waiting to acquire the transaction lock.
        /// </summary>
        public static HRESULT FWP_E_TIMEOUT = new HRESULT("0x80320012", "FWP_E_TIMEOUT", "The call timed out while waiting to acquire the transaction lock.");

        /// <summary>
        /// Collection of network diagnostic events is disabled.
        /// </summary>
        public static HRESULT FWP_E_NET_EVENTS_DISABLED = new HRESULT("0x80320013", "FWP_E_NET_EVENTS_DISABLED", "Collection of network diagnostic events is disabled.");

        /// <summary>
        /// The operation is not supported by the specified layer.
        /// </summary>
        public static HRESULT FWP_E_INCOMPATIBLE_LAYER = new HRESULT("0x80320014", "FWP_E_INCOMPATIBLE_LAYER", "The operation is not supported by the specified layer.");

        /// <summary>
        /// The call is allowed for kernel-mode callers only.
        /// </summary>
        public static HRESULT FWP_E_KM_CLIENTS_ONLY = new HRESULT("0x80320015", "FWP_E_KM_CLIENTS_ONLY", "The call is allowed for kernel-mode callers only.");

        /// <summary>
        /// The call tried to associate two objects with incompatible lifetimes.
        /// </summary>
        public static HRESULT FWP_E_LIFETIME_MISMATCH = new HRESULT("0x80320016", "FWP_E_LIFETIME_MISMATCH", "The call tried to associate two objects with incompatible lifetimes.");

        /// <summary>
        /// The object is built in so cannot be deleted.
        /// </summary>
        public static HRESULT FWP_E_BUILTIN_OBJECT = new HRESULT("0x80320017", "FWP_E_BUILTIN_OBJECT", "The object is built in so cannot be deleted.");

        /// <summary>
        /// The maximum number of callouts has been reached.
        /// </summary>
        public static HRESULT FWP_E_TOO_MANY_CALLOUTS = new HRESULT("0x80320018", "FWP_E_TOO_MANY_CALLOUTS", "The maximum number of callouts has been reached.");

        /// <summary>
        /// A notification could not be delivered because a message queue is at its maximum capacity.
        /// </summary>
        public static HRESULT FWP_E_NOTIFICATION_DROPPED = new HRESULT("0x80320019", "FWP_E_NOTIFICATION_DROPPED", "A notification could not be delivered because a message queue is at its maximum capacity.");

        /// <summary>
        /// The traffic parameters do not match those for the security association context.
        /// </summary>
        public static HRESULT FWP_E_TRAFFIC_MISMATCH = new HRESULT("0x8032001A", "FWP_E_TRAFFIC_MISMATCH", "The traffic parameters do not match those for the security association context.");

        /// <summary>
        /// The call is not allowed for the current security association state.
        /// </summary>
        public static HRESULT FWP_E_INCOMPATIBLE_SA_STATE = new HRESULT("0x8032001B", "FWP_E_INCOMPATIBLE_SA_STATE", "The call is not allowed for the current security association state.");

        /// <summary>
        /// A required pointer is null.
        /// </summary>
        public static HRESULT FWP_E_NULL_POINTER = new HRESULT("0x8032001C", "FWP_E_NULL_POINTER", "A required pointer is null.");

        /// <summary>
        /// An enumerator is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_ENUMERATOR = new HRESULT("0x8032001D", "FWP_E_INVALID_ENUMERATOR", "An enumerator is not valid.");

        /// <summary>
        /// The flags field contains an invalid value.
        /// </summary>
        public static HRESULT FWP_E_INVALID_FLAGS = new HRESULT("0x8032001E", "FWP_E_INVALID_FLAGS", "The flags field contains an invalid value.");

        /// <summary>
        /// A network mask is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_NET_MASK = new HRESULT("0x8032001F", "FWP_E_INVALID_NET_MASK", "A network mask is not valid.");

        /// <summary>
        /// An FWP_RANGE is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_RANGE = new HRESULT("0x80320020", "FWP_E_INVALID_RANGE", "An FWP_RANGE is not valid.");

        /// <summary>
        /// The time interval is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_INTERVAL = new HRESULT("0x80320021", "FWP_E_INVALID_INTERVAL", "The time interval is not valid.");

        /// <summary>
        /// An array that must contain at least one element is zero length.
        /// </summary>
        public static HRESULT FWP_E_ZERO_LENGTH_ARRAY = new HRESULT("0x80320022", "FWP_E_ZERO_LENGTH_ARRAY", "An array that must contain at least one element is zero length.");

        /// <summary>
        /// The displayData.name field cannot be null.
        /// </summary>
        public static HRESULT FWP_E_NULL_DISPLAY_NAME = new HRESULT("0x80320023", "FWP_E_NULL_DISPLAY_NAME", "The displayData.name field cannot be null.");

        /// <summary>
        /// The action type is not one of the allowed action types for a filter.
        /// </summary>
        public static HRESULT FWP_E_INVALID_ACTION_TYPE = new HRESULT("0x80320024", "FWP_E_INVALID_ACTION_TYPE", "The action type is not one of the allowed action types for a filter.");

        /// <summary>
        /// The filter weight is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_WEIGHT = new HRESULT("0x80320025", "FWP_E_INVALID_WEIGHT", "The filter weight is not valid.");

        /// <summary>
        /// A filter condition contains a match type that is not compatible with the operands.
        /// </summary>
        public static HRESULT FWP_E_MATCH_TYPE_MISMATCH = new HRESULT("0x80320026", "FWP_E_MATCH_TYPE_MISMATCH", "A filter condition contains a match type that is not compatible with the operands.");

        /// <summary>
        /// An FWP_VALUE or FWPM_CONDITION_VALUE is of the wrong type.
        /// </summary>
        public static HRESULT FWP_E_TYPE_MISMATCH = new HRESULT("0x80320027", "FWP_E_TYPE_MISMATCH", "An FWP_VALUE or FWPM_CONDITION_VALUE is of the wrong type.");

        /// <summary>
        /// An integer value is outside the allowed range.
        /// </summary>
        public static HRESULT FWP_E_OUT_OF_BOUNDS = new HRESULT("0x80320028", "FWP_E_OUT_OF_BOUNDS", "An integer value is outside the allowed range.");

        /// <summary>
        /// A reserved field is nonzero.
        /// </summary>
        public static HRESULT FWP_E_RESERVED = new HRESULT("0x80320029", "FWP_E_RESERVED", "A reserved field is nonzero.");

        /// <summary>
        /// A filter cannot contain multiple conditions operating on a single field.
        /// </summary>
        public static HRESULT FWP_E_DUPLICATE_CONDITION = new HRESULT("0x8032002A", "FWP_E_DUPLICATE_CONDITION", "A filter cannot contain multiple conditions operating on a single field.");

        /// <summary>
        /// A policy cannot contain the same keying module more than once.
        /// </summary>
        public static HRESULT FWP_E_DUPLICATE_KEYMOD = new HRESULT("0x8032002B", "FWP_E_DUPLICATE_KEYMOD", "A policy cannot contain the same keying module more than once.");

        /// <summary>
        /// The action type is not compatible with the layer.
        /// </summary>
        public static HRESULT FWP_E_ACTION_INCOMPATIBLE_WITH_LAYER = new HRESULT("0x8032002C", "FWP_E_ACTION_INCOMPATIBLE_WITH_LAYER", "The action type is not compatible with the layer.");

        /// <summary>
        /// The action type is not compatible with the sublayer.
        /// </summary>
        public static HRESULT FWP_E_ACTION_INCOMPATIBLE_WITH_SUBLAYER = new HRESULT("0x8032002D", "FWP_E_ACTION_INCOMPATIBLE_WITH_SUBLAYER", "The action type is not compatible with the sublayer.");

        /// <summary>
        /// The raw context or the provider context is not compatible with the layer.
        /// </summary>
        public static HRESULT FWP_E_CONTEXT_INCOMPATIBLE_WITH_LAYER = new HRESULT("0x8032002E", "FWP_E_CONTEXT_INCOMPATIBLE_WITH_LAYER", "The raw context or the provider context is not compatible with the layer.");

        /// <summary>
        /// The raw context or the provider context is not compatible with the callout.
        /// </summary>
        public static HRESULT FWP_E_CONTEXT_INCOMPATIBLE_WITH_CALLOUT = new HRESULT("0x8032002F", "FWP_E_CONTEXT_INCOMPATIBLE_WITH_CALLOUT", "The raw context or the provider context is not compatible with the callout.");

        /// <summary>
        /// The authentication method is not compatible with the policy type.
        /// </summary>
        public static HRESULT FWP_E_INCOMPATIBLE_AUTH_METHOD = new HRESULT("0x80320030", "FWP_E_INCOMPATIBLE_AUTH_METHOD", "The authentication method is not compatible with the policy type.");

        /// <summary>
        /// The Diffie-Hellman group is not compatible with the policy type.
        /// </summary>
        public static HRESULT FWP_E_INCOMPATIBLE_DH_GROUP = new HRESULT("0x80320031", "FWP_E_INCOMPATIBLE_DH_GROUP", "The Diffie-Hellman group is not compatible with the policy type.");

        /// <summary>
        /// An IKE policy cannot contain an Extended Mode policy.
        /// </summary>
        public static HRESULT FWP_E_EM_NOT_SUPPORTED = new HRESULT("0x80320032", "FWP_E_EM_NOT_SUPPORTED", "An IKE policy cannot contain an Extended Mode policy.");

        /// <summary>
        /// The enumeration template or subscription will never match any objects.
        /// </summary>
        public static HRESULT FWP_E_NEVER_MATCH = new HRESULT("0x80320033", "FWP_E_NEVER_MATCH", "The enumeration template or subscription will never match any objects.");

        /// <summary>
        /// The provider context is of the wrong type.
        /// </summary>
        public static HRESULT FWP_E_PROVIDER_CONTEXT_MISMATCH = new HRESULT("0x80320034", "FWP_E_PROVIDER_CONTEXT_MISMATCH", "The provider context is of the wrong type.");

        /// <summary>
        /// The parameter is incorrect.
        /// </summary>
        public static HRESULT FWP_E_INVALID_PARAMETER = new HRESULT("0x80320035", "FWP_E_INVALID_PARAMETER", "The parameter is incorrect.");

        /// <summary>
        /// The maximum number of sublayers has been reached.
        /// </summary>
        public static HRESULT FWP_E_TOO_MANY_SUBLAYERS = new HRESULT("0x80320036", "FWP_E_TOO_MANY_SUBLAYERS", "The maximum number of sublayers has been reached.");

        /// <summary>
        /// The notification function for a callout returned an error.
        /// </summary>
        public static HRESULT FWP_E_CALLOUT_NOTIFICATION_FAILED = new HRESULT("0x80320037", "FWP_E_CALLOUT_NOTIFICATION_FAILED", "The notification function for a callout returned an error.");

        /// <summary>
        /// The IPsec authentication transform is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_AUTH_TRANSFORM = new HRESULT("0x80320038", "FWP_E_INVALID_AUTH_TRANSFORM", "The IPsec authentication transform is not valid.");

        /// <summary>
        /// The IPsec cipher transform is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_CIPHER_TRANSFORM = new HRESULT("0x80320039", "FWP_E_INVALID_CIPHER_TRANSFORM", "The IPsec cipher transform is not valid.");

        /// <summary>
        /// The IPsec cipher transform is not compatible with the policy.
        /// </summary>
        public static HRESULT FWP_E_INCOMPATIBLE_CIPHER_TRANSFORM = new HRESULT("0x8032003A", "FWP_E_INCOMPATIBLE_CIPHER_TRANSFORM", "The IPsec cipher transform is not compatible with the policy.");

        /// <summary>
        /// The combination of IPsec transform types is not valid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_TRANSFORM_COMBINATION = new HRESULT("0x8032003B", "FWP_E_INVALID_TRANSFORM_COMBINATION", "The combination of IPsec transform types is not valid.");

        /// <summary>
        /// A policy cannot contain the same auth method more than once.
        /// </summary>
        public static HRESULT FWP_E_DUPLICATE_AUTH_METHOD = new HRESULT("0x8032003C", "FWP_E_DUPLICATE_AUTH_METHOD", "A policy cannot contain the same auth method more than once.");

        /// <summary>
        /// A tunnel endpoint configuration is invalid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_TUNNEL_ENDPOINT = new HRESULT("0x8032003D", "FWP_E_INVALID_TUNNEL_ENDPOINT", "A tunnel endpoint configuration is invalid.");

        /// <summary>
        /// The WFP MAC Layers are not ready.
        /// </summary>
        public static HRESULT FWP_E_L2_DRIVER_NOT_READY = new HRESULT("0x8032003E", "FWP_E_L2_DRIVER_NOT_READY", "The WFP MAC Layers are not ready.");

        /// <summary>
        /// A key manager capable of key dictation is already registered
        /// </summary>
        public static HRESULT FWP_E_KEY_DICTATOR_ALREADY_REGISTERED = new HRESULT("0x8032003F", "FWP_E_KEY_DICTATOR_ALREADY_REGISTERED", "A key manager capable of key dictation is already registered");

        /// <summary>
        /// A key manager dictated invalid keys
        /// </summary>
        public static HRESULT FWP_E_KEY_DICTATION_INVALID_KEYING_MATERIAL = new HRESULT("0x80320040", "FWP_E_KEY_DICTATION_INVALID_KEYING_MATERIAL", "A key manager dictated invalid keys");

        /// <summary>
        /// The BFE IPsec Connection Tracking is disabled.
        /// </summary>
        public static HRESULT FWP_E_CONNECTIONS_DISABLED = new HRESULT("0x80320041", "FWP_E_CONNECTIONS_DISABLED", "The BFE IPsec Connection Tracking is disabled.");

        /// <summary>
        /// The DNS name is invalid.
        /// </summary>
        public static HRESULT FWP_E_INVALID_DNS_NAME = new HRESULT("0x80320042", "FWP_E_INVALID_DNS_NAME", "The DNS name is invalid.");

        /// <summary>
        /// The engine option is still enabled due to other configuration settings.
        /// </summary>
        public static HRESULT FWP_E_STILL_ON = new HRESULT("0x80320043", "FWP_E_STILL_ON", "The engine option is still enabled due to other configuration settings.");

        /// <summary>
        /// The IKEEXT service is not running. This service only runs when there is IPsec policy applied to the machine.
        /// </summary>
        public static HRESULT FWP_E_IKEEXT_NOT_RUNNING = new HRESULT("0x80320044", "FWP_E_IKEEXT_NOT_RUNNING", "The IKEEXT service is not running. This service only runs when there is IPsec policy applied to the machine.");

        /// <summary>
        /// The packet should be dropped, no ICMP should be sent.
        /// </summary>
        public static HRESULT FWP_E_DROP_NOICMP = new HRESULT("0x80320104", "FWP_E_DROP_NOICMP", "The packet should be dropped, no ICMP should be sent.");

        /// <summary>
        /// The function call is completing asynchronously.
        /// </summary>
        public static HRESULT WS_S_ASYNC = new HRESULT("0x003D0000", "WS_S_ASYNC", "The function call is completing asynchronously.");

        /// <summary>
        /// There are no more messages available on the channel.
        /// </summary>
        public static HRESULT WS_S_END = new HRESULT("0x003D0001", "WS_S_END", "There are no more messages available on the channel.");

        /// <summary>
        /// The input data was not in the expected format or did not have the expected value.
        /// </summary>
        public static HRESULT WS_E_INVALID_FORMAT = new HRESULT("0x803D0000", "WS_E_INVALID_FORMAT", "The input data was not in the expected format or did not have the expected value.");

        /// <summary>
        /// The operation could not be completed because the object is in a faulted state due to a previous error.
        /// </summary>
        public static HRESULT WS_E_OBJECT_FAULTED = new HRESULT("0x803D0001", "WS_E_OBJECT_FAULTED", "The operation could not be completed because the object is in a faulted state due to a previous error.");

        /// <summary>
        /// The operation could not be completed because it would lead to numeric overflow.
        /// </summary>
        public static HRESULT WS_E_NUMERIC_OVERFLOW = new HRESULT("0x803D0002", "WS_E_NUMERIC_OVERFLOW", "The operation could not be completed because it would lead to numeric overflow.");

        /// <summary>
        /// The operation is not allowed due to the current state of the object.
        /// </summary>
        public static HRESULT WS_E_INVALID_OPERATION = new HRESULT("0x803D0003", "WS_E_INVALID_OPERATION", "The operation is not allowed due to the current state of the object.");

        /// <summary>
        /// The operation was aborted.
        /// </summary>
        public static HRESULT WS_E_OPERATION_ABORTED = new HRESULT("0x803D0004", "WS_E_OPERATION_ABORTED", "The operation was aborted.");

        /// <summary>
        /// Access was denied by the remote endpoint.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_ACCESS_DENIED = new HRESULT("0x803D0005", "WS_E_ENDPOINT_ACCESS_DENIED", "Access was denied by the remote endpoint.");

        /// <summary>
        /// The operation did not complete within the time allotted.
        /// </summary>
        public static HRESULT WS_E_OPERATION_TIMED_OUT = new HRESULT("0x803D0006", "WS_E_OPERATION_TIMED_OUT", "The operation did not complete within the time allotted.");

        /// <summary>
        /// The operation was abandoned.
        /// </summary>
        public static HRESULT WS_E_OPERATION_ABANDONED = new HRESULT("0x803D0007", "WS_E_OPERATION_ABANDONED", "The operation was abandoned.");

        /// <summary>
        /// A quota was exceeded.
        /// </summary>
        public static HRESULT WS_E_QUOTA_EXCEEDED = new HRESULT("0x803D0008", "WS_E_QUOTA_EXCEEDED", "A quota was exceeded.");

        /// <summary>
        /// The information was not available in the specified language.
        /// </summary>
        public static HRESULT WS_E_NO_TRANSLATION_AVAILABLE = new HRESULT("0x803D0009", "WS_E_NO_TRANSLATION_AVAILABLE", "The information was not available in the specified language.");

        /// <summary>
        /// Security verification was not successful for the received data.
        /// </summary>
        public static HRESULT WS_E_SECURITY_VERIFICATION_FAILURE = new HRESULT("0x803D000A", "WS_E_SECURITY_VERIFICATION_FAILURE", "Security verification was not successful for the received data.");

        /// <summary>
        /// The address is already being used.
        /// </summary>
        public static HRESULT WS_E_ADDRESS_IN_USE = new HRESULT("0x803D000B", "WS_E_ADDRESS_IN_USE", "The address is already being used.");

        /// <summary>
        /// The address is not valid for this context.
        /// </summary>
        public static HRESULT WS_E_ADDRESS_NOT_AVAILABLE = new HRESULT("0x803D000C", "WS_E_ADDRESS_NOT_AVAILABLE", "The address is not valid for this context.");

        /// <summary>
        /// The remote endpoint does not exist or could not be located.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_NOT_FOUND = new HRESULT("0x803D000D", "WS_E_ENDPOINT_NOT_FOUND", "The remote endpoint does not exist or could not be located.");

        /// <summary>
        /// The remote endpoint is not currently in service at this location.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_NOT_AVAILABLE = new HRESULT("0x803D000E", "WS_E_ENDPOINT_NOT_AVAILABLE", "The remote endpoint is not currently in service at this location.");

        /// <summary>
        /// The remote endpoint could not process the request.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_FAILURE = new HRESULT("0x803D000F", "WS_E_ENDPOINT_FAILURE", "The remote endpoint could not process the request.");

        /// <summary>
        /// The remote endpoint was not reachable.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_UNREACHABLE = new HRESULT("0x803D0010", "WS_E_ENDPOINT_UNREACHABLE", "The remote endpoint was not reachable.");

        /// <summary>
        /// The operation was not supported by the remote endpoint.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_ACTION_NOT_SUPPORTED = new HRESULT("0x803D0011", "WS_E_ENDPOINT_ACTION_NOT_SUPPORTED", "The operation was not supported by the remote endpoint.");

        /// <summary>
        /// The remote endpoint is unable to process the request due to being overloaded.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_TOO_BUSY = new HRESULT("0x803D0012", "WS_E_ENDPOINT_TOO_BUSY", "The remote endpoint is unable to process the request due to being overloaded.");

        /// <summary>
        /// A message containing a fault was received from the remote endpoint.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_FAULT_RECEIVED = new HRESULT("0x803D0013", "WS_E_ENDPOINT_FAULT_RECEIVED", "A message containing a fault was received from the remote endpoint.");

        /// <summary>
        /// The connection with the remote endpoint was terminated.
        /// </summary>
        public static HRESULT WS_E_ENDPOINT_DISCONNECTED = new HRESULT("0x803D0014", "WS_E_ENDPOINT_DISCONNECTED", "The connection with the remote endpoint was terminated.");

        /// <summary>
        /// The HTTP proxy server could not process the request.
        /// </summary>
        public static HRESULT WS_E_PROXY_FAILURE = new HRESULT("0x803D0015", "WS_E_PROXY_FAILURE", "The HTTP proxy server could not process the request.");

        /// <summary>
        /// Access was denied by the HTTP proxy server.
        /// </summary>
        public static HRESULT WS_E_PROXY_ACCESS_DENIED = new HRESULT("0x803D0016", "WS_E_PROXY_ACCESS_DENIED", "Access was denied by the HTTP proxy server.");

        /// <summary>
        /// The requested feature is not available on this platform.
        /// </summary>
        public static HRESULT WS_E_NOT_SUPPORTED = new HRESULT("0x803D0017", "WS_E_NOT_SUPPORTED", "The requested feature is not available on this platform.");

        /// <summary>
        /// The HTTP proxy server requires HTTP authentication scheme 'basic'.
        /// </summary>
        public static HRESULT WS_E_PROXY_REQUIRES_BASIC_AUTH = new HRESULT("0x803D0018", "WS_E_PROXY_REQUIRES_BASIC_AUTH", "The HTTP proxy server requires HTTP authentication scheme 'basic'.");

        /// <summary>
        /// The HTTP proxy server requires HTTP authentication scheme 'digest'.
        /// </summary>
        public static HRESULT WS_E_PROXY_REQUIRES_DIGEST_AUTH = new HRESULT("0x803D0019", "WS_E_PROXY_REQUIRES_DIGEST_AUTH", "The HTTP proxy server requires HTTP authentication scheme 'digest'.");

        /// <summary>
        /// The HTTP proxy server requires HTTP authentication scheme 'NTLM'.
        /// </summary>
        public static HRESULT WS_E_PROXY_REQUIRES_NTLM_AUTH = new HRESULT("0x803D001A", "WS_E_PROXY_REQUIRES_NTLM_AUTH", "The HTTP proxy server requires HTTP authentication scheme 'NTLM'.");

        /// <summary>
        /// The HTTP proxy server requires HTTP authentication scheme 'negotiate'.
        /// </summary>
        public static HRESULT WS_E_PROXY_REQUIRES_NEGOTIATE_AUTH = new HRESULT("0x803D001B", "WS_E_PROXY_REQUIRES_NEGOTIATE_AUTH", "The HTTP proxy server requires HTTP authentication scheme 'negotiate'.");

        /// <summary>
        /// The remote endpoint requires HTTP authentication scheme 'basic'.
        /// </summary>
        public static HRESULT WS_E_SERVER_REQUIRES_BASIC_AUTH = new HRESULT("0x803D001C", "WS_E_SERVER_REQUIRES_BASIC_AUTH", "The remote endpoint requires HTTP authentication scheme 'basic'.");

        /// <summary>
        /// The remote endpoint requires HTTP authentication scheme 'digest'.
        /// </summary>
        public static HRESULT WS_E_SERVER_REQUIRES_DIGEST_AUTH = new HRESULT("0x803D001D", "WS_E_SERVER_REQUIRES_DIGEST_AUTH", "The remote endpoint requires HTTP authentication scheme 'digest'.");

        /// <summary>
        /// The remote endpoint requires HTTP authentication scheme 'NTLM'.
        /// </summary>
        public static HRESULT WS_E_SERVER_REQUIRES_NTLM_AUTH = new HRESULT("0x803D001E", "WS_E_SERVER_REQUIRES_NTLM_AUTH", "The remote endpoint requires HTTP authentication scheme 'NTLM'.");

        /// <summary>
        /// The remote endpoint requires HTTP authentication scheme 'negotiate'.
        /// </summary>
        public static HRESULT WS_E_SERVER_REQUIRES_NEGOTIATE_AUTH = new HRESULT("0x803D001F", "WS_E_SERVER_REQUIRES_NEGOTIATE_AUTH", "The remote endpoint requires HTTP authentication scheme 'negotiate'.");

        /// <summary>
        /// The endpoint address URL is invalid.
        /// </summary>
        public static HRESULT WS_E_INVALID_ENDPOINT_URL = new HRESULT("0x803D0020", "WS_E_INVALID_ENDPOINT_URL", "The endpoint address URL is invalid.");

        /// <summary>
        /// Unrecognized error occurred in the Windows Web Services framework.
        /// </summary>
        public static HRESULT WS_E_OTHER = new HRESULT("0x803D0021", "WS_E_OTHER", "Unrecognized error occurred in the Windows Web Services framework.");

        /// <summary>
        /// A security token was rejected by the server because it has expired.
        /// </summary>
        public static HRESULT WS_E_SECURITY_TOKEN_EXPIRED = new HRESULT("0x803D0022", "WS_E_SECURITY_TOKEN_EXPIRED", "A security token was rejected by the server because it has expired.");

        /// <summary>
        /// A security operation failed in the Windows Web Services framework.
        /// </summary>
        public static HRESULT WS_E_SECURITY_SYSTEM_FAILURE = new HRESULT("0x803D0023", "WS_E_SECURITY_SYSTEM_FAILURE", "A security operation failed in the Windows Web Services framework.");

        /// <summary>
        /// The binding to the network interface is being closed.
        /// </summary>
        public static HRESULT ERROR_NDIS_INTERFACE_CLOSING = new HRESULT("0x80340002", "ERROR_NDIS_INTERFACE_CLOSING", "The binding to the network interface is being closed.");

        /// <summary>
        /// An invalid version was specified.
        /// </summary>
        public static HRESULT ERROR_NDIS_BAD_VERSION = new HRESULT("0x80340004", "ERROR_NDIS_BAD_VERSION", "An invalid version was specified.");

        /// <summary>
        /// An invalid characteristics table was used.
        /// </summary>
        public static HRESULT ERROR_NDIS_BAD_CHARACTERISTICS = new HRESULT("0x80340005", "ERROR_NDIS_BAD_CHARACTERISTICS", "An invalid characteristics table was used.");

        /// <summary>
        /// Failed to find the network interface or network interface is not ready.
        /// </summary>
        public static HRESULT ERROR_NDIS_ADAPTER_NOT_FOUND = new HRESULT("0x80340006", "ERROR_NDIS_ADAPTER_NOT_FOUND", "Failed to find the network interface or network interface is not ready.");

        /// <summary>
        /// Failed to open the network interface.
        /// </summary>
        public static HRESULT ERROR_NDIS_OPEN_FAILED = new HRESULT("0x80340007", "ERROR_NDIS_OPEN_FAILED", "Failed to open the network interface.");

        /// <summary>
        /// Network interface has encountered an internal unrecoverable failure.
        /// </summary>
        public static HRESULT ERROR_NDIS_DEVICE_FAILED = new HRESULT("0x80340008", "ERROR_NDIS_DEVICE_FAILED", "Network interface has encountered an internal unrecoverable failure.");

        /// <summary>
        /// The multicast list on the network interface is full.
        /// </summary>
        public static HRESULT ERROR_NDIS_MULTICAST_FULL = new HRESULT("0x80340009", "ERROR_NDIS_MULTICAST_FULL", "The multicast list on the network interface is full.");

        /// <summary>
        /// An attempt was made to add a duplicate multicast address to the list.
        /// </summary>
        public static HRESULT ERROR_NDIS_MULTICAST_EXISTS = new HRESULT("0x8034000A", "ERROR_NDIS_MULTICAST_EXISTS", "An attempt was made to add a duplicate multicast address to the list.");

        /// <summary>
        /// At attempt was made to remove a multicast address that was never added.
        /// </summary>
        public static HRESULT ERROR_NDIS_MULTICAST_NOT_FOUND = new HRESULT("0x8034000B", "ERROR_NDIS_MULTICAST_NOT_FOUND", "At attempt was made to remove a multicast address that was never added.");

        /// <summary>
        /// Netowork interface aborted the request.
        /// </summary>
        public static HRESULT ERROR_NDIS_REQUEST_ABORTED = new HRESULT("0x8034000C", "ERROR_NDIS_REQUEST_ABORTED", "Netowork interface aborted the request.");

        /// <summary>
        /// Network interface cannot process the request because it is being reset.
        /// </summary>
        public static HRESULT ERROR_NDIS_RESET_IN_PROGRESS = new HRESULT("0x8034000D", "ERROR_NDIS_RESET_IN_PROGRESS", "Network interface cannot process the request because it is being reset.");

        /// <summary>
        /// Netword interface does not support this request.
        /// </summary>
        public static HRESULT ERROR_NDIS_NOT_SUPPORTED = new HRESULT("0x803400BB", "ERROR_NDIS_NOT_SUPPORTED", "Netword interface does not support this request.");

        /// <summary>
        /// An attempt was made to send an invalid packet on a network interface.
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_PACKET = new HRESULT("0x8034000F", "ERROR_NDIS_INVALID_PACKET", "An attempt was made to send an invalid packet on a network interface.");

        /// <summary>
        /// Network interface is not ready to complete this operation.
        /// </summary>
        public static HRESULT ERROR_NDIS_ADAPTER_NOT_READY = new HRESULT("0x80340011", "ERROR_NDIS_ADAPTER_NOT_READY", "Network interface is not ready to complete this operation.");

        /// <summary>
        /// The length of the buffer submitted for this operation is not valid.
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_LENGTH = new HRESULT("0x80340014", "ERROR_NDIS_INVALID_LENGTH", "The length of the buffer submitted for this operation is not valid.");

        /// <summary>
        /// The data used for this operation is not valid.
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_DATA = new HRESULT("0x80340015", "ERROR_NDIS_INVALID_DATA", "The data used for this operation is not valid.");

        /// <summary>
        /// The length of buffer submitted for this operation is too small.
        /// </summary>
        public static HRESULT ERROR_NDIS_BUFFER_TOO_SHORT = new HRESULT("0x80340016", "ERROR_NDIS_BUFFER_TOO_SHORT", "The length of buffer submitted for this operation is too small.");

        /// <summary>
        /// Network interface does not support this OID (Object Identifier)
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_OID = new HRESULT("0x80340017", "ERROR_NDIS_INVALID_OID", "Network interface does not support this OID (Object Identifier)");

        /// <summary>
        /// The network interface has been removed.
        /// </summary>
        public static HRESULT ERROR_NDIS_ADAPTER_REMOVED = new HRESULT("0x80340018", "ERROR_NDIS_ADAPTER_REMOVED", "The network interface has been removed.");

        /// <summary>
        /// Network interface does not support this media type.
        /// </summary>
        public static HRESULT ERROR_NDIS_UNSUPPORTED_MEDIA = new HRESULT("0x80340019", "ERROR_NDIS_UNSUPPORTED_MEDIA", "Network interface does not support this media type.");

        /// <summary>
        /// An attempt was made to remove a token ring group address that is in use by other components.
        /// </summary>
        public static HRESULT ERROR_NDIS_GROUP_ADDRESS_IN_USE = new HRESULT("0x8034001A", "ERROR_NDIS_GROUP_ADDRESS_IN_USE", "An attempt was made to remove a token ring group address that is in use by other components.");

        /// <summary>
        /// An attempt was made to map a file that cannot be found.
        /// </summary>
        public static HRESULT ERROR_NDIS_FILE_NOT_FOUND = new HRESULT("0x8034001B", "ERROR_NDIS_FILE_NOT_FOUND", "An attempt was made to map a file that cannot be found.");

        /// <summary>
        /// An error occurred while NDIS tried to map the file.
        /// </summary>
        public static HRESULT ERROR_NDIS_ERROR_READING_FILE = new HRESULT("0x8034001C", "ERROR_NDIS_ERROR_READING_FILE", "An error occurred while NDIS tried to map the file.");

        /// <summary>
        /// An attempt was made to map a file that is alreay mapped.
        /// </summary>
        public static HRESULT ERROR_NDIS_ALREADY_MAPPED = new HRESULT("0x8034001D", "ERROR_NDIS_ALREADY_MAPPED", "An attempt was made to map a file that is alreay mapped.");

        /// <summary>
        /// An attempt to allocate a hardware resource failed because the resource is used by another component.
        /// </summary>
        public static HRESULT ERROR_NDIS_RESOURCE_CONFLICT = new HRESULT("0x8034001E", "ERROR_NDIS_RESOURCE_CONFLICT", "An attempt to allocate a hardware resource failed because the resource is used by another component.");

        /// <summary>
        /// The I/O operation failed because network media is disconnected or wireless access point is out of range.
        /// </summary>
        public static HRESULT ERROR_NDIS_MEDIA_DISCONNECTED = new HRESULT("0x8034001F", "ERROR_NDIS_MEDIA_DISCONNECTED", "The I/O operation failed because network media is disconnected or wireless access point is out of range.");

        /// <summary>
        /// The network address used in the request is invalid.
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_ADDRESS = new HRESULT("0x80340022", "ERROR_NDIS_INVALID_ADDRESS", "The network address used in the request is invalid.");

        /// <summary>
        /// The specified request is not a valid operation for the target device.
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_DEVICE_REQUEST = new HRESULT("0x80340010", "ERROR_NDIS_INVALID_DEVICE_REQUEST", "The specified request is not a valid operation for the target device.");

        /// <summary>
        /// The offload operation on the network interface has been paused.
        /// </summary>
        public static HRESULT ERROR_NDIS_PAUSED = new HRESULT("0x8034002A", "ERROR_NDIS_PAUSED", "The offload operation on the network interface has been paused.");

        /// <summary>
        /// Network interface was not found.
        /// </summary>
        public static HRESULT ERROR_NDIS_INTERFACE_NOT_FOUND = new HRESULT("0x8034002B", "ERROR_NDIS_INTERFACE_NOT_FOUND", "Network interface was not found.");

        /// <summary>
        /// The revision number specified in the structure is not supported.
        /// </summary>
        public static HRESULT ERROR_NDIS_UNSUPPORTED_REVISION = new HRESULT("0x8034002C", "ERROR_NDIS_UNSUPPORTED_REVISION", "The revision number specified in the structure is not supported.");

        /// <summary>
        /// The specified port does not exist on this network interface.
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_PORT = new HRESULT("0x8034002D", "ERROR_NDIS_INVALID_PORT", "The specified port does not exist on this network interface.");

        /// <summary>
        /// The current state of the specified port on this network interface does not support the requested operation.
        /// </summary>
        public static HRESULT ERROR_NDIS_INVALID_PORT_STATE = new HRESULT("0x8034002E", "ERROR_NDIS_INVALID_PORT_STATE", "The current state of the specified port on this network interface does not support the requested operation.");

        /// <summary>
        /// The miniport adapter is in low power state.
        /// </summary>
        public static HRESULT ERROR_NDIS_LOW_POWER_STATE = new HRESULT("0x8034002F", "ERROR_NDIS_LOW_POWER_STATE", "The miniport adapter is in low power state.");

        /// <summary>
        /// This operation requires the miniport adapter to be reinitialized.
        /// </summary>
        public static HRESULT ERROR_NDIS_REINIT_REQUIRED = new HRESULT("0x80340030", "ERROR_NDIS_REINIT_REQUIRED", "This operation requires the miniport adapter to be reinitialized.");

        /// <summary>
        /// The wireless local area network interface is in auto configuration mode and doesn't support the requested parameter change operation.
        /// </summary>
        public static HRESULT ERROR_NDIS_DOT11_AUTO_CONFIG_ENABLED = new HRESULT("0x80342000", "ERROR_NDIS_DOT11_AUTO_CONFIG_ENABLED", "The wireless local area network interface is in auto configuration mode and doesn't support the requested parameter change operation.");

        /// <summary>
        /// The wireless local area network interface is busy and cannot perform the requested operation.
        /// </summary>
        public static HRESULT ERROR_NDIS_DOT11_MEDIA_IN_USE = new HRESULT("0x80342001", "ERROR_NDIS_DOT11_MEDIA_IN_USE", "The wireless local area network interface is busy and cannot perform the requested operation.");

        /// <summary>
        /// The wireless local area network interface is powered down and doesn't support the requested operation.
        /// </summary>
        public static HRESULT ERROR_NDIS_DOT11_POWER_STATE_INVALID = new HRESULT("0x80342002", "ERROR_NDIS_DOT11_POWER_STATE_INVALID", "The wireless local area network interface is powered down and doesn't support the requested operation.");

        /// <summary>
        /// The list of wake on LAN patterns is full.
        /// </summary>
        public static HRESULT ERROR_NDIS_PM_WOL_PATTERN_LIST_FULL = new HRESULT("0x80342003", "ERROR_NDIS_PM_WOL_PATTERN_LIST_FULL", "The list of wake on LAN patterns is full.");

        /// <summary>
        /// The list of low power protocol offloads is full.
        /// </summary>
        public static HRESULT ERROR_NDIS_PM_PROTOCOL_OFFLOAD_LIST_FULL = new HRESULT("0x80342004", "ERROR_NDIS_PM_PROTOCOL_OFFLOAD_LIST_FULL", "The list of low power protocol offloads is full.");

        /// <summary>
        /// The request will be completed later by NDIS status indication.
        /// </summary>
        public static HRESULT ERROR_NDIS_INDICATION_REQUIRED = new HRESULT("0x00340001", "ERROR_NDIS_INDICATION_REQUIRED", "The request will be completed later by NDIS status indication.");

        /// <summary>
        /// The TCP connection is not offloadable because of a local policy setting.
        /// </summary>
        public static HRESULT ERROR_NDIS_OFFLOAD_POLICY = new HRESULT("0xC034100F", "ERROR_NDIS_OFFLOAD_POLICY", "The TCP connection is not offloadable because of a local policy setting.");

        /// <summary>
        /// The TCP connection is not offloadable by the Chimney Offload target.
        /// </summary>
        public static HRESULT ERROR_NDIS_OFFLOAD_CONNECTION_REJECTED = new HRESULT("0xC0341012", "ERROR_NDIS_OFFLOAD_CONNECTION_REJECTED", "The TCP connection is not offloadable by the Chimney Offload target.");

        /// <summary>
        /// The IP Path object is not in an offloadable state.
        /// </summary>
        public static HRESULT ERROR_NDIS_OFFLOAD_PATH_REJECTED = new HRESULT("0xC0341013", "ERROR_NDIS_OFFLOAD_PATH_REJECTED", "The IP Path object is not in an offloadable state.");

        /// <summary>
        /// The hypervisor does not support the operation because the specified hypercall code is not supported.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_HYPERCALL_CODE = new HRESULT("0xC0350002", "ERROR_HV_INVALID_HYPERCALL_CODE", "The hypervisor does not support the operation because the specified hypercall code is not supported.");

        /// <summary>
        /// The hypervisor does not support the operation because the encoding for the hypercall input register is not supported.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_HYPERCALL_INPUT = new HRESULT("0xC0350003", "ERROR_HV_INVALID_HYPERCALL_INPUT", "The hypervisor does not support the operation because the encoding for the hypercall input register is not supported.");

        /// <summary>
        /// The hypervisor could not perform the operation because a parameter has an invalid alignment.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_ALIGNMENT = new HRESULT("0xC0350004", "ERROR_HV_INVALID_ALIGNMENT", "The hypervisor could not perform the operation because a parameter has an invalid alignment.");

        /// <summary>
        /// The hypervisor could not perform the operation because an invalid parameter was specified.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_PARAMETER = new HRESULT("0xC0350005", "ERROR_HV_INVALID_PARAMETER", "The hypervisor could not perform the operation because an invalid parameter was specified.");

        /// <summary>
        /// Access to the specified object was denied.
        /// </summary>
        public static HRESULT ERROR_HV_ACCESS_DENIED = new HRESULT("0xC0350006", "ERROR_HV_ACCESS_DENIED", "Access to the specified object was denied.");

        /// <summary>
        /// The hypervisor could not perform the operation because the partition is entering or in an invalid state.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_PARTITION_STATE = new HRESULT("0xC0350007", "ERROR_HV_INVALID_PARTITION_STATE", "The hypervisor could not perform the operation because the partition is entering or in an invalid state.");

        /// <summary>
        /// The operation is not allowed in the current state.
        /// </summary>
        public static HRESULT ERROR_HV_OPERATION_DENIED = new HRESULT("0xC0350008", "ERROR_HV_OPERATION_DENIED", "The operation is not allowed in the current state.");

        /// <summary>
        /// The hypervisor does not recognize the specified partition property.
        /// </summary>
        public static HRESULT ERROR_HV_UNKNOWN_PROPERTY = new HRESULT("0xC0350009", "ERROR_HV_UNKNOWN_PROPERTY", "The hypervisor does not recognize the specified partition property.");

        /// <summary>
        /// The specified value of a partition property is out of range or violates an invariant.
        /// </summary>
        public static HRESULT ERROR_HV_PROPERTY_VALUE_OUT_OF_RANGE = new HRESULT("0xC035000A", "ERROR_HV_PROPERTY_VALUE_OUT_OF_RANGE", "The specified value of a partition property is out of range or violates an invariant.");

        /// <summary>
        /// There is not enough memory in the hypervisor pool to complete the operation.
        /// </summary>
        public static HRESULT ERROR_HV_INSUFFICIENT_MEMORY = new HRESULT("0xC035000B", "ERROR_HV_INSUFFICIENT_MEMORY", "There is not enough memory in the hypervisor pool to complete the operation.");

        /// <summary>
        /// The maximum partition depth has been exceeded for the partition hierarchy.
        /// </summary>
        public static HRESULT ERROR_HV_PARTITION_TOO_DEEP = new HRESULT("0xC035000C", "ERROR_HV_PARTITION_TOO_DEEP", "The maximum partition depth has been exceeded for the partition hierarchy.");

        /// <summary>
        /// A partition with the specified partition Id does not exist.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_PARTITION_ID = new HRESULT("0xC035000D", "ERROR_HV_INVALID_PARTITION_ID", "A partition with the specified partition Id does not exist.");

        /// <summary>
        /// The hypervisor could not perform the operation because the specified VP index is invalid.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_VP_INDEX = new HRESULT("0xC035000E", "ERROR_HV_INVALID_VP_INDEX", "The hypervisor could not perform the operation because the specified VP index is invalid.");

        /// <summary>
        /// The hypervisor could not perform the operation because the specified port identifier is invalid.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_PORT_ID = new HRESULT("0xC0350011", "ERROR_HV_INVALID_PORT_ID", "The hypervisor could not perform the operation because the specified port identifier is invalid.");

        /// <summary>
        /// The hypervisor could not perform the operation because the specified connection identifier is invalid.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_CONNECTION_ID = new HRESULT("0xC0350012", "ERROR_HV_INVALID_CONNECTION_ID", "The hypervisor could not perform the operation because the specified connection identifier is invalid.");

        /// <summary>
        /// Not enough buffers were supplied to send a message.
        /// </summary>
        public static HRESULT ERROR_HV_INSUFFICIENT_BUFFERS = new HRESULT("0xC0350013", "ERROR_HV_INSUFFICIENT_BUFFERS", "Not enough buffers were supplied to send a message.");

        /// <summary>
        /// The previous virtual interrupt has not been acknowledged.
        /// </summary>
        public static HRESULT ERROR_HV_NOT_ACKNOWLEDGED = new HRESULT("0xC0350014", "ERROR_HV_NOT_ACKNOWLEDGED", "The previous virtual interrupt has not been acknowledged.");

        /// <summary>
        /// The previous virtual interrupt has already been acknowledged.
        /// </summary>
        public static HRESULT ERROR_HV_ACKNOWLEDGED = new HRESULT("0xC0350016", "ERROR_HV_ACKNOWLEDGED", "The previous virtual interrupt has already been acknowledged.");

        /// <summary>
        /// The indicated partition is not in a valid state for saving or restoring.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_SAVE_RESTORE_STATE = new HRESULT("0xC0350017", "ERROR_HV_INVALID_SAVE_RESTORE_STATE", "The indicated partition is not in a valid state for saving or restoring.");

        /// <summary>
        /// The hypervisor could not complete the operation because a required feature of the synthetic interrupt controller (SynIC) was disabled.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_SYNIC_STATE = new HRESULT("0xC0350018", "ERROR_HV_INVALID_SYNIC_STATE", "The hypervisor could not complete the operation because a required feature of the synthetic interrupt controller (SynIC) was disabled.");

        /// <summary>
        /// The hypervisor could not perform the operation because the object or value was either already in use or being used for a purpose that would not permit completing the operation.
        /// </summary>
        public static HRESULT ERROR_HV_OBJECT_IN_USE = new HRESULT("0xC0350019", "ERROR_HV_OBJECT_IN_USE", "The hypervisor could not perform the operation because the object or value was either already in use or being used for a purpose that would not permit completing the operation.");

        /// <summary>
        /// The proximity domain information is invalid.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_PROXIMITY_DOMAIN_INFO = new HRESULT("0xC035001A", "ERROR_HV_INVALID_PROXIMITY_DOMAIN_INFO", "The proximity domain information is invalid.");

        /// <summary>
        /// An attempt to retrieve debugging data failed because none was available.
        /// </summary>
        public static HRESULT ERROR_HV_NO_DATA = new HRESULT("0xC035001B", "ERROR_HV_NO_DATA", "An attempt to retrieve debugging data failed because none was available.");

        /// <summary>
        /// The physical connection being used for debuggging has not recorded any receive activity since the last operation.
        /// </summary>
        public static HRESULT ERROR_HV_INACTIVE = new HRESULT("0xC035001C", "ERROR_HV_INACTIVE", "The physical connection being used for debuggging has not recorded any receive activity since the last operation.");

        /// <summary>
        /// There are not enough resources to complete the operation.
        /// </summary>
        public static HRESULT ERROR_HV_NO_RESOURCES = new HRESULT("0xC035001D", "ERROR_HV_NO_RESOURCES", "There are not enough resources to complete the operation.");

        /// <summary>
        /// A hypervisor feature is not available to the user.
        /// </summary>
        public static HRESULT ERROR_HV_FEATURE_UNAVAILABLE = new HRESULT("0xC035001E", "ERROR_HV_FEATURE_UNAVAILABLE", "A hypervisor feature is not available to the user.");

        /// <summary>
        /// The maximum number of domains supported by the platform I/O remapping hardware is currently in use. No domains are available to assign this device to this partition.
        /// </summary>
        public static HRESULT ERROR_HV_INSUFFICIENT_DEVICE_DOMAINS = new HRESULT("0xC0350038", "ERROR_HV_INSUFFICIENT_DEVICE_DOMAINS", "The maximum number of domains supported by the platform I/O remapping hardware is currently in use. No domains are available to assign this device to this partition.");

        /// <summary>
        /// The hypervisor could not perform the operation because the specified LP index is invalid.
        /// </summary>
        public static HRESULT ERROR_HV_INVALID_LP_INDEX = new HRESULT("0xC0350041", "ERROR_HV_INVALID_LP_INDEX", "The hypervisor could not perform the operation because the specified LP index is invalid.");

        /// <summary>
        /// No hypervisor is present on this system.
        /// </summary>
        public static HRESULT ERROR_HV_NOT_PRESENT = new HRESULT("0xC0351000", "ERROR_HV_NOT_PRESENT", "No hypervisor is present on this system.");

        /// <summary>
        /// The handler for the virtualization infrastructure driver is already registered. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_DUPLICATE_HANDLER = new HRESULT("0xC0370001", "ERROR_VID_DUPLICATE_HANDLER", "The handler for the virtualization infrastructure driver is already registered. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The number of registered handlers for the virtualization infrastructure driver exceeded the maximum. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_TOO_MANY_HANDLERS = new HRESULT("0xC0370002", "ERROR_VID_TOO_MANY_HANDLERS", "The number of registered handlers for the virtualization infrastructure driver exceeded the maximum. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The message queue for the virtualization infrastructure driver is full and cannot accept new messages. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_QUEUE_FULL = new HRESULT("0xC0370003", "ERROR_VID_QUEUE_FULL", "The message queue for the virtualization infrastructure driver is full and cannot accept new messages. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// No handler exists to handle the message for the virtualization infrastructure driver. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_HANDLER_NOT_PRESENT = new HRESULT("0xC0370004", "ERROR_VID_HANDLER_NOT_PRESENT", "No handler exists to handle the message for the virtualization infrastructure driver. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The name of the partition or message queue for the virtualization infrastructure driver is invalid. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_OBJECT_NAME = new HRESULT("0xC0370005", "ERROR_VID_INVALID_OBJECT_NAME", "The name of the partition or message queue for the virtualization infrastructure driver is invalid. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The partition name of the virtualization infrastructure driver exceeds the maximum.
        /// </summary>
        public static HRESULT ERROR_VID_PARTITION_NAME_TOO_LONG = new HRESULT("0xC0370006", "ERROR_VID_PARTITION_NAME_TOO_LONG", "The partition name of the virtualization infrastructure driver exceeds the maximum.");

        /// <summary>
        /// The message queue name of the virtualization infrastructure driver exceeds the maximum.
        /// </summary>
        public static HRESULT ERROR_VID_MESSAGE_QUEUE_NAME_TOO_LONG = new HRESULT("0xC0370007", "ERROR_VID_MESSAGE_QUEUE_NAME_TOO_LONG", "The message queue name of the virtualization infrastructure driver exceeds the maximum.");

        /// <summary>
        /// Cannot create the partition for the virtualization infrastructure driver because another partition with the same name already exists.
        /// </summary>
        public static HRESULT ERROR_VID_PARTITION_ALREADY_EXISTS = new HRESULT("0xC0370008", "ERROR_VID_PARTITION_ALREADY_EXISTS", "Cannot create the partition for the virtualization infrastructure driver because another partition with the same name already exists.");

        /// <summary>
        /// The virtualization infrastructure driver has encountered an error. The requested partition does not exist. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_PARTITION_DOES_NOT_EXIST = new HRESULT("0xC0370009", "ERROR_VID_PARTITION_DOES_NOT_EXIST", "The virtualization infrastructure driver has encountered an error. The requested partition does not exist. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The virtualization infrastructure driver has encountered an error. Could not find the requested partition. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_PARTITION_NAME_NOT_FOUND = new HRESULT("0xC037000A", "ERROR_VID_PARTITION_NAME_NOT_FOUND", "The virtualization infrastructure driver has encountered an error. Could not find the requested partition. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// A message queue with the same name already exists for the virtualization infrastructure driver.
        /// </summary>
        public static HRESULT ERROR_VID_MESSAGE_QUEUE_ALREADY_EXISTS = new HRESULT("0xC037000B", "ERROR_VID_MESSAGE_QUEUE_ALREADY_EXISTS", "A message queue with the same name already exists for the virtualization infrastructure driver.");

        /// <summary>
        /// The memory block page for the virtualization infrastructure driver cannot be mapped because the page map limit has been reached. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_EXCEEDED_MBP_ENTRY_MAP_LIMIT = new HRESULT("0xC037000C", "ERROR_VID_EXCEEDED_MBP_ENTRY_MAP_LIMIT", "The memory block page for the virtualization infrastructure driver cannot be mapped because the page map limit has been reached. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The memory block for the virtualization infrastructure driver is still being used and cannot be destroyed.
        /// </summary>
        public static HRESULT ERROR_VID_MB_STILL_REFERENCED = new HRESULT("0xC037000D", "ERROR_VID_MB_STILL_REFERENCED", "The memory block for the virtualization infrastructure driver is still being used and cannot be destroyed.");

        /// <summary>
        /// Cannot unlock the page array for the guest operating system memory address because it does not match a previous lock request. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_CHILD_GPA_PAGE_SET_CORRUPTED = new HRESULT("0xC037000E", "ERROR_VID_CHILD_GPA_PAGE_SET_CORRUPTED", "Cannot unlock the page array for the guest operating system memory address because it does not match a previous lock request. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The non-uniform memory access (NUMA) node settings do not match the system NUMA topology. In order to start the virtual machine, you will need to modify the NUMA configuration.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_NUMA_SETTINGS = new HRESULT("0xC037000F", "ERROR_VID_INVALID_NUMA_SETTINGS", "The non-uniform memory access (NUMA) node settings do not match the system NUMA topology. In order to start the virtual machine, you will need to modify the NUMA configuration.");

        /// <summary>
        /// The non-uniform memory access (NUMA) node index does not match a valid index in the system NUMA topology.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_NUMA_NODE_INDEX = new HRESULT("0xC0370010", "ERROR_VID_INVALID_NUMA_NODE_INDEX", "The non-uniform memory access (NUMA) node index does not match a valid index in the system NUMA topology.");

        /// <summary>
        /// The memory block for the virtualization infrastructure driver is already associated with a message queue.
        /// </summary>
        public static HRESULT ERROR_VID_NOTIFICATION_QUEUE_ALREADY_ASSOCIATED = new HRESULT("0xC0370011", "ERROR_VID_NOTIFICATION_QUEUE_ALREADY_ASSOCIATED", "The memory block for the virtualization infrastructure driver is already associated with a message queue.");

        /// <summary>
        /// The handle is not a valid memory block handle for the virtualization infrastructure driver.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_MEMORY_BLOCK_HANDLE = new HRESULT("0xC0370012", "ERROR_VID_INVALID_MEMORY_BLOCK_HANDLE", "The handle is not a valid memory block handle for the virtualization infrastructure driver.");

        /// <summary>
        /// The request exceeded the memory block page limit for the virtualization infrastructure driver. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_PAGE_RANGE_OVERFLOW = new HRESULT("0xC0370013", "ERROR_VID_PAGE_RANGE_OVERFLOW", "The request exceeded the memory block page limit for the virtualization infrastructure driver. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The handle is not a valid message queue handle for the virtualization infrastructure driver.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_MESSAGE_QUEUE_HANDLE = new HRESULT("0xC0370014", "ERROR_VID_INVALID_MESSAGE_QUEUE_HANDLE", "The handle is not a valid message queue handle for the virtualization infrastructure driver.");

        /// <summary>
        /// The handle is not a valid page range handle for the virtualization infrastructure driver.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_GPA_RANGE_HANDLE = new HRESULT("0xC0370015", "ERROR_VID_INVALID_GPA_RANGE_HANDLE", "The handle is not a valid page range handle for the virtualization infrastructure driver.");

        /// <summary>
        /// Cannot install client notifications because no message queue for the virtualization infrastructure driver is associated with the memory block.
        /// </summary>
        public static HRESULT ERROR_VID_NO_MEMORY_BLOCK_NOTIFICATION_QUEUE = new HRESULT("0xC0370016", "ERROR_VID_NO_MEMORY_BLOCK_NOTIFICATION_QUEUE", "Cannot install client notifications because no message queue for the virtualization infrastructure driver is associated with the memory block.");

        /// <summary>
        /// The request to lock or map a memory block page failed because the virtualization infrastructure driver memory block limit has been reached. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_MEMORY_BLOCK_LOCK_COUNT_EXCEEDED = new HRESULT("0xC0370017", "ERROR_VID_MEMORY_BLOCK_LOCK_COUNT_EXCEEDED", "The request to lock or map a memory block page failed because the virtualization infrastructure driver memory block limit has been reached. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The handle is not a valid parent partition mapping handle for the virtualization infrastructure driver.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_PPM_HANDLE = new HRESULT("0xC0370018", "ERROR_VID_INVALID_PPM_HANDLE", "The handle is not a valid parent partition mapping handle for the virtualization infrastructure driver.");

        /// <summary>
        /// Notifications cannot be created on the memory block because it is use.
        /// </summary>
        public static HRESULT ERROR_VID_MBPS_ARE_LOCKED = new HRESULT("0xC0370019", "ERROR_VID_MBPS_ARE_LOCKED", "Notifications cannot be created on the memory block because it is use.");

        /// <summary>
        /// The message queue for the virtualization infrastructure driver has been closed. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_MESSAGE_QUEUE_CLOSED = new HRESULT("0xC037001A", "ERROR_VID_MESSAGE_QUEUE_CLOSED", "The message queue for the virtualization infrastructure driver has been closed. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// Cannot add a virtual processor to the partition because the maximum has been reached.
        /// </summary>
        public static HRESULT ERROR_VID_VIRTUAL_PROCESSOR_LIMIT_EXCEEDED = new HRESULT("0xC037001B", "ERROR_VID_VIRTUAL_PROCESSOR_LIMIT_EXCEEDED", "Cannot add a virtual processor to the partition because the maximum has been reached.");

        /// <summary>
        /// Cannot stop the virtual processor immediately because of a pending intercept.
        /// </summary>
        public static HRESULT ERROR_VID_STOP_PENDING = new HRESULT("0xC037001C", "ERROR_VID_STOP_PENDING", "Cannot stop the virtual processor immediately because of a pending intercept.");

        /// <summary>
        /// Invalid state for the virtual processor. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_PROCESSOR_STATE = new HRESULT("0xC037001D", "ERROR_VID_INVALID_PROCESSOR_STATE", "Invalid state for the virtual processor. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The maximum number of kernel mode clients for the virtualization infrastructure driver has been reached. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_EXCEEDED_KM_CONTEXT_COUNT_LIMIT = new HRESULT("0xC037001E", "ERROR_VID_EXCEEDED_KM_CONTEXT_COUNT_LIMIT", "The maximum number of kernel mode clients for the virtualization infrastructure driver has been reached. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// This kernel mode interface for the virtualization infrastructure driver has already been initialized. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_KM_INTERFACE_ALREADY_INITIALIZED = new HRESULT("0xC037001F", "ERROR_VID_KM_INTERFACE_ALREADY_INITIALIZED", "This kernel mode interface for the virtualization infrastructure driver has already been initialized. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// Cannot set or reset the memory block property more than once for the virtualization infrastructure driver. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_MB_PROPERTY_ALREADY_SET_RESET = new HRESULT("0xC0370020", "ERROR_VID_MB_PROPERTY_ALREADY_SET_RESET", "Cannot set or reset the memory block property more than once for the virtualization infrastructure driver. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The memory mapped I/O for this page range no longer exists. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_MMIO_RANGE_DESTROYED = new HRESULT("0xC0370021", "ERROR_VID_MMIO_RANGE_DESTROYED", "The memory mapped I/O for this page range no longer exists. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The lock or unlock request uses an invalid guest operating system memory address. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_INVALID_CHILD_GPA_PAGE_SET = new HRESULT("0xC0370022", "ERROR_VID_INVALID_CHILD_GPA_PAGE_SET", "The lock or unlock request uses an invalid guest operating system memory address. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// Cannot destroy or reuse the reserve page set for the virtualization infrastructure driver because it is in use. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_RESERVE_PAGE_SET_IS_BEING_USED = new HRESULT("0xC0370023", "ERROR_VID_RESERVE_PAGE_SET_IS_BEING_USED", "Cannot destroy or reuse the reserve page set for the virtualization infrastructure driver because it is in use. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// The reserve page set for the virtualization infrastructure driver is too small to use in the lock request. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_RESERVE_PAGE_SET_TOO_SMALL = new HRESULT("0xC0370024", "ERROR_VID_RESERVE_PAGE_SET_TOO_SMALL", "The reserve page set for the virtualization infrastructure driver is too small to use in the lock request. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// Cannot lock or map the memory block page for the virtualization infrastructure driver because it has already been locked using a reserve page set page. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_MBP_ALREADY_LOCKED_USING_RESERVED_PAGE = new HRESULT("0xC0370025", "ERROR_VID_MBP_ALREADY_LOCKED_USING_RESERVED_PAGE", "Cannot lock or map the memory block page for the virtualization infrastructure driver because it has already been locked using a reserve page set page. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// Cannot create the memory block for the virtualization infrastructure driver because the requested number of pages exceeded the limit. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.
        /// </summary>
        public static HRESULT ERROR_VID_MBP_COUNT_EXCEEDED_LIMIT = new HRESULT("0xC0370026", "ERROR_VID_MBP_COUNT_EXCEEDED_LIMIT", "Cannot create the memory block for the virtualization infrastructure driver because the requested number of pages exceeded the limit. Restarting the virtual machine may fix the problem. If the problem persists, try restarting the physical computer.");

        /// <summary>
        /// Cannot restore this virtual machine because the saved state data cannot be read. Delete the saved state data and then try to start the virtual machine.
        /// </summary>
        public static HRESULT ERROR_VID_SAVED_STATE_CORRUPT = new HRESULT("0xC0370027", "ERROR_VID_SAVED_STATE_CORRUPT", "Cannot restore this virtual machine because the saved state data cannot be read. Delete the saved state data and then try to start the virtual machine.");

        /// <summary>
        /// Cannot restore this virtual machine because an item read from the saved state data is not recognized. Delete the saved state data and then try to start the virtual machine.
        /// </summary>
        public static HRESULT ERROR_VID_SAVED_STATE_UNRECOGNIZED_ITEM = new HRESULT("0xC0370028", "ERROR_VID_SAVED_STATE_UNRECOGNIZED_ITEM", "Cannot restore this virtual machine because an item read from the saved state data is not recognized. Delete the saved state data and then try to start the virtual machine.");

        /// <summary>
        /// Cannot restore this virtual machine to the saved state because of hypervisor incompatibility. Delete the saved state data and then try to start the virtual machine.
        /// </summary>
        public static HRESULT ERROR_VID_SAVED_STATE_INCOMPATIBLE = new HRESULT("0xC0370029", "ERROR_VID_SAVED_STATE_INCOMPATIBLE", "Cannot restore this virtual machine to the saved state because of hypervisor incompatibility. Delete the saved state data and then try to start the virtual machine.");
        #endregion

        #region COM Error Codes (VOLMGR, BCD, VHD, SDIAG)
        /// <summary>
        /// The regeneration operation was not able to copy all data from the active plexes due to bad sectors.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_INCOMPLETE_REGENERATION = new HRESULT("0x80380001", "ERROR_VOLMGR_INCOMPLETE_REGENERATION", "The regeneration operation was not able to copy all data from the active plexes due to bad sectors.");

        /// <summary>
        /// One or more disks were not fully migrated to the target pack. They may or may not require reimport after fixing the hardware problems.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_INCOMPLETE_DISK_MIGRATION = new HRESULT("0x80380002", "ERROR_VOLMGR_INCOMPLETE_DISK_MIGRATION", "One or more disks were not fully migrated to the target pack. They may or may not require reimport after fixing the hardware problems.");

        /// <summary>
        /// The configuration database is full.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DATABASE_FULL = new HRESULT("0xC0380001", "ERROR_VOLMGR_DATABASE_FULL", "The configuration database is full.");

        /// <summary>
        /// The configuration data on the disk is corrupted.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_CONFIGURATION_CORRUPTED = new HRESULT("0xC0380002", "ERROR_VOLMGR_DISK_CONFIGURATION_CORRUPTED", "The configuration data on the disk is corrupted.");

        /// <summary>
        /// The configuration on the disk is not insync with the in-memory configuration.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_CONFIGURATION_NOT_IN_SYNC = new HRESULT("0xC0380003", "ERROR_VOLMGR_DISK_CONFIGURATION_NOT_IN_SYNC", "The configuration on the disk is not insync with the in-memory configuration.");

        /// <summary>
        /// A majority of disks failed to be updated with the new configuration.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_CONFIG_UPDATE_FAILED = new HRESULT("0xC0380004", "ERROR_VOLMGR_PACK_CONFIG_UPDATE_FAILED", "A majority of disks failed to be updated with the new configuration.");

        /// <summary>
        /// The disk contains non-simple volumes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_CONTAINS_NON_SIMPLE_VOLUME = new HRESULT("0xC0380005", "ERROR_VOLMGR_DISK_CONTAINS_NON_SIMPLE_VOLUME", "The disk contains non-simple volumes.");

        /// <summary>
        /// The same disk was specified more than once in the migration list.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_DUPLICATE = new HRESULT("0xC0380006", "ERROR_VOLMGR_DISK_DUPLICATE", "The same disk was specified more than once in the migration list.");

        /// <summary>
        /// The disk is already dynamic.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_DYNAMIC = new HRESULT("0xC0380007", "ERROR_VOLMGR_DISK_DYNAMIC", "The disk is already dynamic.");

        /// <summary>
        /// The specified disk id is invalid. There are no disks with the specified disk id.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_ID_INVALID = new HRESULT("0xC0380008", "ERROR_VOLMGR_DISK_ID_INVALID", "The specified disk id is invalid. There are no disks with the specified disk id.");

        /// <summary>
        /// The specified disk is an invalid disk. Operation cannot complete on an invalid disk.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_INVALID = new HRESULT("0xC0380009", "ERROR_VOLMGR_DISK_INVALID", "The specified disk is an invalid disk. Operation cannot complete on an invalid disk.");

        /// <summary>
        /// The specified disk(s) cannot be removed since it is the last remaining voter.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_LAST_VOTER = new HRESULT("0xC038000A", "ERROR_VOLMGR_DISK_LAST_VOTER", "The specified disk(s) cannot be removed since it is the last remaining voter.");

        /// <summary>
        /// The specified disk has an invalid disk layout.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_LAYOUT_INVALID = new HRESULT("0xC038000B", "ERROR_VOLMGR_DISK_LAYOUT_INVALID", "The specified disk has an invalid disk layout.");

        /// <summary>
        /// The disk layout contains non-basic partitions which appear after basic paritions. This is an invalid disk layout.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_LAYOUT_NON_BASIC_BETWEEN_BASIC_PARTITIONS = new HRESULT("0xC038000C", "ERROR_VOLMGR_DISK_LAYOUT_NON_BASIC_BETWEEN_BASIC_PARTITIONS", "The disk layout contains non-basic partitions which appear after basic paritions. This is an invalid disk layout.");

        /// <summary>
        /// The disk layout contains partitions which are not cylinder aligned.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_LAYOUT_NOT_CYLINDER_ALIGNED = new HRESULT("0xC038000D", "ERROR_VOLMGR_DISK_LAYOUT_NOT_CYLINDER_ALIGNED", "The disk layout contains partitions which are not cylinder aligned.");

        /// <summary>
        /// The disk layout contains partitions which are samller than the minimum size.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_LAYOUT_PARTITIONS_TOO_SMALL = new HRESULT("0xC038000E", "ERROR_VOLMGR_DISK_LAYOUT_PARTITIONS_TOO_SMALL", "The disk layout contains partitions which are samller than the minimum size.");

        /// <summary>
        /// The disk layout contains primary partitions in between logical drives. This is an invalid disk layout.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_LAYOUT_PRIMARY_BETWEEN_LOGICAL_PARTITIONS = new HRESULT("0xC038000F", "ERROR_VOLMGR_DISK_LAYOUT_PRIMARY_BETWEEN_LOGICAL_PARTITIONS", "The disk layout contains primary partitions in between logical drives. This is an invalid disk layout.");

        /// <summary>
        /// The disk layout contains more than the maximum number of supported partitions.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_LAYOUT_TOO_MANY_PARTITIONS = new HRESULT("0xC0380010", "ERROR_VOLMGR_DISK_LAYOUT_TOO_MANY_PARTITIONS", "The disk layout contains more than the maximum number of supported partitions.");

        /// <summary>
        /// The specified disk is missing. The operation cannot complete on a missing disk.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_MISSING = new HRESULT("0xC0380011", "ERROR_VOLMGR_DISK_MISSING", "The specified disk is missing. The operation cannot complete on a missing disk.");

        /// <summary>
        /// The specified disk is not empty.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_NOT_EMPTY = new HRESULT("0xC0380012", "ERROR_VOLMGR_DISK_NOT_EMPTY", "The specified disk is not empty.");

        /// <summary>
        /// There is not enough usable space for this operation.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_NOT_ENOUGH_SPACE = new HRESULT("0xC0380013", "ERROR_VOLMGR_DISK_NOT_ENOUGH_SPACE", "There is not enough usable space for this operation.");

        /// <summary>
        /// The force revectoring of bad sectors failed.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_REVECTORING_FAILED = new HRESULT("0xC0380014", "ERROR_VOLMGR_DISK_REVECTORING_FAILED", "The force revectoring of bad sectors failed.");

        /// <summary>
        /// The specified disk has an invalid sector size.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_SECTOR_SIZE_INVALID = new HRESULT("0xC0380015", "ERROR_VOLMGR_DISK_SECTOR_SIZE_INVALID", "The specified disk has an invalid sector size.");

        /// <summary>
        /// The specified disk set contains volumes which exist on disks outside of the set.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_SET_NOT_CONTAINED = new HRESULT("0xC0380016", "ERROR_VOLMGR_DISK_SET_NOT_CONTAINED", "The specified disk set contains volumes which exist on disks outside of the set.");

        /// <summary>
        /// A disk in the volume layout provides extents to more than one member of a plex.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_USED_BY_MULTIPLE_MEMBERS = new HRESULT("0xC0380017", "ERROR_VOLMGR_DISK_USED_BY_MULTIPLE_MEMBERS", "A disk in the volume layout provides extents to more than one member of a plex.");

        /// <summary>
        /// A disk in the volume layout provides extents to more than one plex.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DISK_USED_BY_MULTIPLE_PLEXES = new HRESULT("0xC0380018", "ERROR_VOLMGR_DISK_USED_BY_MULTIPLE_PLEXES", "A disk in the volume layout provides extents to more than one plex.");

        /// <summary>
        /// Dynamic disks are not supported on this system.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DYNAMIC_DISK_NOT_SUPPORTED = new HRESULT("0xC0380019", "ERROR_VOLMGR_DYNAMIC_DISK_NOT_SUPPORTED", "Dynamic disks are not supported on this system.");

        /// <summary>
        /// The specified extent is already used by other volumes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_EXTENT_ALREADY_USED = new HRESULT("0xC038001A", "ERROR_VOLMGR_EXTENT_ALREADY_USED", "The specified extent is already used by other volumes.");

        /// <summary>
        /// The specified volume is retained and can only be extended into a contiguous extent. The specified extent to grow the volume is not contiguous with the specified volume.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_EXTENT_NOT_CONTIGUOUS = new HRESULT("0xC038001B", "ERROR_VOLMGR_EXTENT_NOT_CONTIGUOUS", "The specified volume is retained and can only be extended into a contiguous extent. The specified extent to grow the volume is not contiguous with the specified volume.");

        /// <summary>
        /// The specified volume extent is not within the public region of the disk.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_EXTENT_NOT_IN_PUBLIC_REGION = new HRESULT("0xC038001C", "ERROR_VOLMGR_EXTENT_NOT_IN_PUBLIC_REGION", "The specified volume extent is not within the public region of the disk.");

        /// <summary>
        /// The specified volume extent is not sector aligned.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_EXTENT_NOT_SECTOR_ALIGNED = new HRESULT("0xC038001D", "ERROR_VOLMGR_EXTENT_NOT_SECTOR_ALIGNED", "The specified volume extent is not sector aligned.");

        /// <summary>
        /// The specified parition overlaps an EBR (the first track of an extended partition on a MBR disks).
        /// </summary>
        public static HRESULT ERROR_VOLMGR_EXTENT_OVERLAPS_EBR_PARTITION = new HRESULT("0xC038001E", "ERROR_VOLMGR_EXTENT_OVERLAPS_EBR_PARTITION", "The specified parition overlaps an EBR (the first track of an extended partition on a MBR disks).");

        /// <summary>
        /// The specified extent lengths cannot be used to construct a volume with specified length.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_EXTENT_VOLUME_LENGTHS_DO_NOT_MATCH = new HRESULT("0xC038001F", "ERROR_VOLMGR_EXTENT_VOLUME_LENGTHS_DO_NOT_MATCH", "The specified extent lengths cannot be used to construct a volume with specified length.");

        /// <summary>
        /// The system does not support fault tolerant volumes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_FAULT_TOLERANT_NOT_SUPPORTED = new HRESULT("0xC0380020", "ERROR_VOLMGR_FAULT_TOLERANT_NOT_SUPPORTED", "The system does not support fault tolerant volumes.");

        /// <summary>
        /// The specified interleave length is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_INTERLEAVE_LENGTH_INVALID = new HRESULT("0xC0380021", "ERROR_VOLMGR_INTERLEAVE_LENGTH_INVALID", "The specified interleave length is invalid.");

        /// <summary>
        /// There is already a maximum number of registered users.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MAXIMUM_REGISTERED_USERS = new HRESULT("0xC0380022", "ERROR_VOLMGR_MAXIMUM_REGISTERED_USERS", "There is already a maximum number of registered users.");

        /// <summary>
        /// The specified member is already in-sync with the other active members. It does not need to be regenerated.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MEMBER_IN_SYNC = new HRESULT("0xC0380023", "ERROR_VOLMGR_MEMBER_IN_SYNC", "The specified member is already in-sync with the other active members. It does not need to be regenerated.");

        /// <summary>
        /// The same member index was specified more than once.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MEMBER_INDEX_DUPLICATE = new HRESULT("0xC0380024", "ERROR_VOLMGR_MEMBER_INDEX_DUPLICATE", "The same member index was specified more than once.");

        /// <summary>
        /// The specified member index is greater or equal than the number of members in the volume plex.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MEMBER_INDEX_INVALID = new HRESULT("0xC0380025", "ERROR_VOLMGR_MEMBER_INDEX_INVALID", "The specified member index is greater or equal than the number of members in the volume plex.");

        /// <summary>
        /// The specified member is missing. It cannot be regenerated.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MEMBER_MISSING = new HRESULT("0xC0380026", "ERROR_VOLMGR_MEMBER_MISSING", "The specified member is missing. It cannot be regenerated.");

        /// <summary>
        /// The specified member is not detached. Cannot replace a member which is not detached.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MEMBER_NOT_DETACHED = new HRESULT("0xC0380027", "ERROR_VOLMGR_MEMBER_NOT_DETACHED", "The specified member is not detached. Cannot replace a member which is not detached.");

        /// <summary>
        /// The specified member is already regenerating.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MEMBER_REGENERATING = new HRESULT("0xC0380028", "ERROR_VOLMGR_MEMBER_REGENERATING", "The specified member is already regenerating.");

        /// <summary>
        /// All disks belonging to the pack failed.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_ALL_DISKS_FAILED = new HRESULT("0xC0380029", "ERROR_VOLMGR_ALL_DISKS_FAILED", "All disks belonging to the pack failed.");

        /// <summary>
        /// There are currently no registered users for notifications. The task number is irrelevant unless there are registered users.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NO_REGISTERED_USERS = new HRESULT("0xC038002A", "ERROR_VOLMGR_NO_REGISTERED_USERS", "There are currently no registered users for notifications. The task number is irrelevant unless there are registered users.");

        /// <summary>
        /// The specified notification user does not exist. Failed to unregister user for notifications.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NO_SUCH_USER = new HRESULT("0xC038002B", "ERROR_VOLMGR_NO_SUCH_USER", "The specified notification user does not exist. Failed to unregister user for notifications.");

        /// <summary>
        /// The notifications have been reset. Notifications for the current user are invalid. Unregister and re-register for notifications.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NOTIFICATION_RESET = new HRESULT("0xC038002C", "ERROR_VOLMGR_NOTIFICATION_RESET", "The notifications have been reset. Notifications for the current user are invalid. Unregister and re-register for notifications.");

        /// <summary>
        /// The specified number of members is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NUMBER_OF_MEMBERS_INVALID = new HRESULT("0xC038002D", "ERROR_VOLMGR_NUMBER_OF_MEMBERS_INVALID", "The specified number of members is invalid.");

        /// <summary>
        /// The specified number of plexes is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NUMBER_OF_PLEXES_INVALID = new HRESULT("0xC038002E", "ERROR_VOLMGR_NUMBER_OF_PLEXES_INVALID", "The specified number of plexes is invalid.");

        /// <summary>
        /// The specified source and target packs are identical.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_DUPLICATE = new HRESULT("0xC038002F", "ERROR_VOLMGR_PACK_DUPLICATE", "The specified source and target packs are identical.");

        /// <summary>
        /// The specified pack id is invalid. There are no packs with the specified pack id.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_ID_INVALID = new HRESULT("0xC0380030", "ERROR_VOLMGR_PACK_ID_INVALID", "The specified pack id is invalid. There are no packs with the specified pack id.");

        /// <summary>
        /// The specified pack is the invalid pack. The operation cannot complete with the invalid pack.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_INVALID = new HRESULT("0xC0380031", "ERROR_VOLMGR_PACK_INVALID", "The specified pack is the invalid pack. The operation cannot complete with the invalid pack.");

        /// <summary>
        /// The specified pack name is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_NAME_INVALID = new HRESULT("0xC0380032", "ERROR_VOLMGR_PACK_NAME_INVALID", "The specified pack name is invalid.");

        /// <summary>
        /// The specified pack is offline.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_OFFLINE = new HRESULT("0xC0380033", "ERROR_VOLMGR_PACK_OFFLINE", "The specified pack is offline.");

        /// <summary>
        /// The specified pack already has a quorum of healthy disks.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_HAS_QUORUM = new HRESULT("0xC0380034", "ERROR_VOLMGR_PACK_HAS_QUORUM", "The specified pack already has a quorum of healthy disks.");

        /// <summary>
        /// The pack does not have a quorum of healthy disks.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_WITHOUT_QUORUM = new HRESULT("0xC0380035", "ERROR_VOLMGR_PACK_WITHOUT_QUORUM", "The pack does not have a quorum of healthy disks.");

        /// <summary>
        /// The specified disk has an unsupported partition style. Only MBR and GPT partition styles are supported.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PARTITION_STYLE_INVALID = new HRESULT("0xC0380036", "ERROR_VOLMGR_PARTITION_STYLE_INVALID", "The specified disk has an unsupported partition style. Only MBR and GPT partition styles are supported.");

        /// <summary>
        /// Failed to update the disk's partition layout.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PARTITION_UPDATE_FAILED = new HRESULT("0xC0380037", "ERROR_VOLMGR_PARTITION_UPDATE_FAILED", "Failed to update the disk's partition layout.");

        /// <summary>
        /// The specified plex is already in-sync with the other active plexes. It does not need to be regenerated.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_IN_SYNC = new HRESULT("0xC0380038", "ERROR_VOLMGR_PLEX_IN_SYNC", "The specified plex is already in-sync with the other active plexes. It does not need to be regenerated.");

        /// <summary>
        /// The same plex index was specified more than once.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_INDEX_DUPLICATE = new HRESULT("0xC0380039", "ERROR_VOLMGR_PLEX_INDEX_DUPLICATE", "The same plex index was specified more than once.");

        /// <summary>
        /// The specified plex index is greater or equal than the number of plexes in the volume.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_INDEX_INVALID = new HRESULT("0xC038003A", "ERROR_VOLMGR_PLEX_INDEX_INVALID", "The specified plex index is greater or equal than the number of plexes in the volume.");

        /// <summary>
        /// The specified plex is the last active plex in the volume. The plex cannot be removed or else the volume will go offline.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_LAST_ACTIVE = new HRESULT("0xC038003B", "ERROR_VOLMGR_PLEX_LAST_ACTIVE", "The specified plex is the last active plex in the volume. The plex cannot be removed or else the volume will go offline.");

        /// <summary>
        /// The specified plex is missing.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_MISSING = new HRESULT("0xC038003C", "ERROR_VOLMGR_PLEX_MISSING", "The specified plex is missing.");

        /// <summary>
        /// The specified plex is currently regenerating.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_REGENERATING = new HRESULT("0xC038003D", "ERROR_VOLMGR_PLEX_REGENERATING", "The specified plex is currently regenerating.");

        /// <summary>
        /// The specified plex type is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_TYPE_INVALID = new HRESULT("0xC038003E", "ERROR_VOLMGR_PLEX_TYPE_INVALID", "The specified plex type is invalid.");

        /// <summary>
        /// The operation is only supported on RAID-5 plexes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_NOT_RAID5 = new HRESULT("0xC038003F", "ERROR_VOLMGR_PLEX_NOT_RAID5", "The operation is only supported on RAID-5 plexes.");

        /// <summary>
        /// The operation is only supported on simple plexes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_NOT_SIMPLE = new HRESULT("0xC0380040", "ERROR_VOLMGR_PLEX_NOT_SIMPLE", "The operation is only supported on simple plexes.");

        /// <summary>
        /// The Size fields in the VM_VOLUME_LAYOUT input structure are incorrectly set.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_STRUCTURE_SIZE_INVALID = new HRESULT("0xC0380041", "ERROR_VOLMGR_STRUCTURE_SIZE_INVALID", "The Size fields in the VM_VOLUME_LAYOUT input structure are incorrectly set.");

        /// <summary>
        /// There is already a pending request for notifications. Wait for the existing request to return before requesting for more notifications.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_TOO_MANY_NOTIFICATION_REQUESTS = new HRESULT("0xC0380042", "ERROR_VOLMGR_TOO_MANY_NOTIFICATION_REQUESTS", "There is already a pending request for notifications. Wait for the existing request to return before requesting for more notifications.");

        /// <summary>
        /// There is currently a transaction in process.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_TRANSACTION_IN_PROGRESS = new HRESULT("0xC0380043", "ERROR_VOLMGR_TRANSACTION_IN_PROGRESS", "There is currently a transaction in process.");

        /// <summary>
        /// An unexpected layout change occurred outside of the volume manager.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_UNEXPECTED_DISK_LAYOUT_CHANGE = new HRESULT("0xC0380044", "ERROR_VOLMGR_UNEXPECTED_DISK_LAYOUT_CHANGE", "An unexpected layout change occurred outside of the volume manager.");

        /// <summary>
        /// The specified volume contains a missing disk.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_CONTAINS_MISSING_DISK = new HRESULT("0xC0380045", "ERROR_VOLMGR_VOLUME_CONTAINS_MISSING_DISK", "The specified volume contains a missing disk.");

        /// <summary>
        /// The specified volume id is invalid. There are no volumes with the specified volume id.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_ID_INVALID = new HRESULT("0xC0380046", "ERROR_VOLMGR_VOLUME_ID_INVALID", "The specified volume id is invalid. There are no volumes with the specified volume id.");

        /// <summary>
        /// The specified volume length is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_LENGTH_INVALID = new HRESULT("0xC0380047", "ERROR_VOLMGR_VOLUME_LENGTH_INVALID", "The specified volume length is invalid.");

        /// <summary>
        /// The specified size for the volume is not a multiple of the sector size.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_LENGTH_NOT_SECTOR_SIZE_MULTIPLE = new HRESULT("0xC0380048", "ERROR_VOLMGR_VOLUME_LENGTH_NOT_SECTOR_SIZE_MULTIPLE", "The specified size for the volume is not a multiple of the sector size.");

        /// <summary>
        /// The operation is only supported on mirrored volumes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_NOT_MIRRORED = new HRESULT("0xC0380049", "ERROR_VOLMGR_VOLUME_NOT_MIRRORED", "The operation is only supported on mirrored volumes.");

        /// <summary>
        /// The specified volume does not have a retain partition.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_NOT_RETAINED = new HRESULT("0xC038004A", "ERROR_VOLMGR_VOLUME_NOT_RETAINED", "The specified volume does not have a retain partition.");

        /// <summary>
        /// The specified volume is offline.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_OFFLINE = new HRESULT("0xC038004B", "ERROR_VOLMGR_VOLUME_OFFLINE", "The specified volume is offline.");

        /// <summary>
        /// The specified volume already has a retain partition.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_RETAINED = new HRESULT("0xC038004C", "ERROR_VOLMGR_VOLUME_RETAINED", "The specified volume already has a retain partition.");

        /// <summary>
        /// The specified number of extents is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NUMBER_OF_EXTENTS_INVALID = new HRESULT("0xC038004D", "ERROR_VOLMGR_NUMBER_OF_EXTENTS_INVALID", "The specified number of extents is invalid.");

        /// <summary>
        /// All disks participating to the volume must have the same sector size.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_DIFFERENT_SECTOR_SIZE = new HRESULT("0xC038004E", "ERROR_VOLMGR_DIFFERENT_SECTOR_SIZE", "All disks participating to the volume must have the same sector size.");

        /// <summary>
        /// The boot disk experienced failures.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_BAD_BOOT_DISK = new HRESULT("0xC038004F", "ERROR_VOLMGR_BAD_BOOT_DISK", "The boot disk experienced failures.");

        /// <summary>
        /// The configuration of the pack is offline.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_CONFIG_OFFLINE = new HRESULT("0xC0380050", "ERROR_VOLMGR_PACK_CONFIG_OFFLINE", "The configuration of the pack is offline.");

        /// <summary>
        /// The configuration of the pack is online.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_CONFIG_ONLINE = new HRESULT("0xC0380051", "ERROR_VOLMGR_PACK_CONFIG_ONLINE", "The configuration of the pack is online.");

        /// <summary>
        /// The specified pack is not the primary pack.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NOT_PRIMARY_PACK = new HRESULT("0xC0380052", "ERROR_VOLMGR_NOT_PRIMARY_PACK", "The specified pack is not the primary pack.");

        /// <summary>
        /// All disks failed to be updated with the new content of the log.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PACK_LOG_UPDATE_FAILED = new HRESULT("0xC0380053", "ERROR_VOLMGR_PACK_LOG_UPDATE_FAILED", "All disks failed to be updated with the new content of the log.");

        /// <summary>
        /// The specified number of disks in a plex is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NUMBER_OF_DISKS_IN_PLEX_INVALID = new HRESULT("0xC0380054", "ERROR_VOLMGR_NUMBER_OF_DISKS_IN_PLEX_INVALID", "The specified number of disks in a plex is invalid.");

        /// <summary>
        /// The specified number of disks in a plex member is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NUMBER_OF_DISKS_IN_MEMBER_INVALID = new HRESULT("0xC0380055", "ERROR_VOLMGR_NUMBER_OF_DISKS_IN_MEMBER_INVALID", "The specified number of disks in a plex member is invalid.");

        /// <summary>
        /// The operation is not supported on mirrored volumes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_VOLUME_MIRRORED = new HRESULT("0xC0380056", "ERROR_VOLMGR_VOLUME_MIRRORED", "The operation is not supported on mirrored volumes.");

        /// <summary>
        /// The operation is only supported on simple and spanned plexes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PLEX_NOT_SIMPLE_SPANNED = new HRESULT("0xC0380057", "ERROR_VOLMGR_PLEX_NOT_SIMPLE_SPANNED", "The operation is only supported on simple and spanned plexes.");

        /// <summary>
        /// The pack has no valid log copies.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NO_VALID_LOG_COPIES = new HRESULT("0xC0380058", "ERROR_VOLMGR_NO_VALID_LOG_COPIES", "The pack has no valid log copies.");

        /// <summary>
        /// A primary pack is already present.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_PRIMARY_PACK_PRESENT = new HRESULT("0xC0380059", "ERROR_VOLMGR_PRIMARY_PACK_PRESENT", "A primary pack is already present.");

        /// <summary>
        /// The specified number of disks is invalid.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_NUMBER_OF_DISKS_INVALID = new HRESULT("0xC038005A", "ERROR_VOLMGR_NUMBER_OF_DISKS_INVALID", "The specified number of disks is invalid.");

        /// <summary>
        /// The system does not support mirrored volumes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_MIRROR_NOT_SUPPORTED = new HRESULT("0xC038005B", "ERROR_VOLMGR_MIRROR_NOT_SUPPORTED", "The system does not support mirrored volumes.");

        /// <summary>
        /// The system does not support RAID-5 volumes.
        /// </summary>
        public static HRESULT ERROR_VOLMGR_RAID5_NOT_SUPPORTED = new HRESULT("0xC038005C", "ERROR_VOLMGR_RAID5_NOT_SUPPORTED", "The system does not support RAID-5 volumes.");

        /// <summary>
        /// Some BCD entries were not imported correctly from the BCD store.
        /// </summary>
        public static HRESULT ERROR_BCD_NOT_ALL_ENTRIES_IMPORTED = new HRESULT("0x80390001", "ERROR_BCD_NOT_ALL_ENTRIES_IMPORTED", "Some BCD entries were not imported correctly from the BCD store.");

        /// <summary>
        /// Entries enumerated have exceeded the allowed threshold.
        /// </summary>
        public static HRESULT ERROR_BCD_TOO_MANY_ELEMENTS = new HRESULT("0xC0390002", "ERROR_BCD_TOO_MANY_ELEMENTS", "Entries enumerated have exceeded the allowed threshold.");

        /// <summary>
        /// Some BCD entries were not synchronized correctly with the firmware.
        /// </summary>
        public static HRESULT ERROR_BCD_NOT_ALL_ENTRIES_SYNCHRONIZED = new HRESULT("0x80390003", "ERROR_BCD_NOT_ALL_ENTRIES_SYNCHRONIZED", "Some BCD entries were not synchronized correctly with the firmware.");

        /// <summary>
        /// The virtual hard disk is corrupted. The virtual hard disk drive footer is missing.
        /// </summary>
        public static HRESULT ERROR_VHD_DRIVE_FOOTER_MISSING = new HRESULT("0xC03A0001", "ERROR_VHD_DRIVE_FOOTER_MISSING", "The virtual hard disk is corrupted. The virtual hard disk drive footer is missing.");

        /// <summary>
        /// The virtual hard disk is corrupted. The virtual hard disk drive footer checksum does not match the on-disk checksum.
        /// </summary>
        public static HRESULT ERROR_VHD_DRIVE_FOOTER_CHECKSUM_MISMATCH = new HRESULT("0xC03A0002", "ERROR_VHD_DRIVE_FOOTER_CHECKSUM_MISMATCH", "The virtual hard disk is corrupted. The virtual hard disk drive footer checksum does not match the on-disk checksum.");

        /// <summary>
        /// The virtual hard disk is corrupted. The virtual hard disk drive footer in the virtual hard disk is corrupted.
        /// </summary>
        public static HRESULT ERROR_VHD_DRIVE_FOOTER_CORRUPT = new HRESULT("0xC03A0003", "ERROR_VHD_DRIVE_FOOTER_CORRUPT", "The virtual hard disk is corrupted. The virtual hard disk drive footer in the virtual hard disk is corrupted.");

        /// <summary>
        /// The system does not recognize the file format of this virtual hard disk.
        /// </summary>
        public static HRESULT ERROR_VHD_FORMAT_UNKNOWN = new HRESULT("0xC03A0004", "ERROR_VHD_FORMAT_UNKNOWN", "The system does not recognize the file format of this virtual hard disk.");

        /// <summary>
        /// The version does not support this version of the file format.
        /// </summary>
        public static HRESULT ERROR_VHD_FORMAT_UNSUPPORTED_VERSION = new HRESULT("0xC03A0005", "ERROR_VHD_FORMAT_UNSUPPORTED_VERSION", "The version does not support this version of the file format.");

        /// <summary>
        /// The virtual hard disk is corrupted. The sparse header checksum does not match the on-disk checksum.
        /// </summary>
        public static HRESULT ERROR_VHD_SPARSE_HEADER_CHECKSUM_MISMATCH = new HRESULT("0xC03A0006", "ERROR_VHD_SPARSE_HEADER_CHECKSUM_MISMATCH", "The virtual hard disk is corrupted. The sparse header checksum does not match the on-disk checksum.");

        /// <summary>
        /// The system does not support this version of the virtual hard disk.This version of the sparse header is not supported.
        /// </summary>
        public static HRESULT ERROR_VHD_SPARSE_HEADER_UNSUPPORTED_VERSION = new HRESULT("0xC03A0007", "ERROR_VHD_SPARSE_HEADER_UNSUPPORTED_VERSION", "The system does not support this version of the virtual hard disk.This version of the sparse header is not supported.");

        /// <summary>
        /// The virtual hard disk is corrupted. The sparse header in the virtual hard disk is corrupt.
        /// </summary>
        public static HRESULT ERROR_VHD_SPARSE_HEADER_CORRUPT = new HRESULT("0xC03A0008", "ERROR_VHD_SPARSE_HEADER_CORRUPT", "The virtual hard disk is corrupted. The sparse header in the virtual hard disk is corrupt.");

        /// <summary>
        /// Failed to write to the virtual hard disk failed because the system failed to allocate a new block in the virtual hard disk.
        /// </summary>
        public static HRESULT ERROR_VHD_BLOCK_ALLOCATION_FAILURE = new HRESULT("0xC03A0009", "ERROR_VHD_BLOCK_ALLOCATION_FAILURE", "Failed to write to the virtual hard disk failed because the system failed to allocate a new block in the virtual hard disk.");

        /// <summary>
        /// The virtual hard disk is corrupted. The block allocation table in the virtual hard disk is corrupt.
        /// </summary>
        public static HRESULT ERROR_VHD_BLOCK_ALLOCATION_TABLE_CORRUPT = new HRESULT("0xC03A000A", "ERROR_VHD_BLOCK_ALLOCATION_TABLE_CORRUPT", "The virtual hard disk is corrupted. The block allocation table in the virtual hard disk is corrupt.");

        /// <summary>
        /// The system does not support this version of the virtual hard disk. The block size is invalid.
        /// </summary>
        public static HRESULT ERROR_VHD_INVALID_BLOCK_SIZE = new HRESULT("0xC03A000B", "ERROR_VHD_INVALID_BLOCK_SIZE", "The system does not support this version of the virtual hard disk. The block size is invalid.");

        /// <summary>
        /// The virtual hard disk is corrupted. The block bitmap does not match with the block data present in the virtual hard disk.
        /// </summary>
        public static HRESULT ERROR_VHD_BITMAP_MISMATCH = new HRESULT("0xC03A000C", "ERROR_VHD_BITMAP_MISMATCH", "The virtual hard disk is corrupted. The block bitmap does not match with the block data present in the virtual hard disk.");

        /// <summary>
        /// The chain of virtual hard disks is broken. The system cannot locate the parent virtual hard disk for the differencing disk.
        /// </summary>
        public static HRESULT ERROR_VHD_PARENT_VHD_NOT_FOUND = new HRESULT("0xC03A000D", "ERROR_VHD_PARENT_VHD_NOT_FOUND", "The chain of virtual hard disks is broken. The system cannot locate the parent virtual hard disk for the differencing disk.");

        /// <summary>
        /// The chain of virtual hard disks is corrupted. There is a mismatch in the identifiers of the parent virtual hard disk and differencing disk.
        /// </summary>
        public static HRESULT ERROR_VHD_CHILD_PARENT_ID_MISMATCH = new HRESULT("0xC03A000E", "ERROR_VHD_CHILD_PARENT_ID_MISMATCH", "The chain of virtual hard disks is corrupted. There is a mismatch in the identifiers of the parent virtual hard disk and differencing disk.");

        /// <summary>
        /// The chain of virtual hard disks is corrupted. The time stamp of the parent virtual hard disk does not match the time stamp of the differencing disk.
        /// </summary>
        public static HRESULT ERROR_VHD_CHILD_PARENT_TIMESTAMP_MISMATCH = new HRESULT("0xC03A000F", "ERROR_VHD_CHILD_PARENT_TIMESTAMP_MISMATCH", "The chain of virtual hard disks is corrupted. The time stamp of the parent virtual hard disk does not match the time stamp of the differencing disk.");

        /// <summary>
        /// Failed to read the metadata of the virtual hard disk.
        /// </summary>
        public static HRESULT ERROR_VHD_METADATA_READ_FAILURE = new HRESULT("0xC03A0010", "ERROR_VHD_METADATA_READ_FAILURE", "Failed to read the metadata of the virtual hard disk.");

        /// <summary>
        /// Failed to write to the metadata of the virtual hard disk.
        /// </summary>
        public static HRESULT ERROR_VHD_METADATA_WRITE_FAILURE = new HRESULT("0xC03A0011", "ERROR_VHD_METADATA_WRITE_FAILURE", "Failed to write to the metadata of the virtual hard disk.");

        /// <summary>
        /// The size of the virtual hard disk is not valid.
        /// </summary>
        public static HRESULT ERROR_VHD_INVALID_SIZE = new HRESULT("0xC03A0012", "ERROR_VHD_INVALID_SIZE", "The size of the virtual hard disk is not valid.");

        /// <summary>
        /// The file size of this virtual hard disk is not valid.
        /// </summary>
        public static HRESULT ERROR_VHD_INVALID_FILE_SIZE = new HRESULT("0xC03A0013", "ERROR_VHD_INVALID_FILE_SIZE", "The file size of this virtual hard disk is not valid.");

        /// <summary>
        /// A virtual disk support provider for the specified file was not found.
        /// </summary>
        public static HRESULT ERROR_VIRTDISK_PROVIDER_NOT_FOUND = new HRESULT("0xC03A0014", "ERROR_VIRTDISK_PROVIDER_NOT_FOUND", "A virtual disk support provider for the specified file was not found.");

        /// <summary>
        /// The specified disk is not a virtual disk.
        /// </summary>
        public static HRESULT ERROR_VIRTDISK_NOT_VIRTUAL_DISK = new HRESULT("0xC03A0015", "ERROR_VIRTDISK_NOT_VIRTUAL_DISK", "The specified disk is not a virtual disk.");

        /// <summary>
        /// The chain of virtual hard disks is inaccessible. The process has not been granted access rights to the parent virtual hard disk for the differencing disk.
        /// </summary>
        public static HRESULT ERROR_VHD_PARENT_VHD_ACCESS_DENIED = new HRESULT("0xC03A0016", "ERROR_VHD_PARENT_VHD_ACCESS_DENIED", "The chain of virtual hard disks is inaccessible. The process has not been granted access rights to the parent virtual hard disk for the differencing disk.");

        /// <summary>
        /// The chain of virtual hard disks is corrupted. There is a mismatch in the virtual sizes of the parent virtual hard disk and differencing disk.
        /// </summary>
        public static HRESULT ERROR_VHD_CHILD_PARENT_SIZE_MISMATCH = new HRESULT("0xC03A0017", "ERROR_VHD_CHILD_PARENT_SIZE_MISMATCH", "The chain of virtual hard disks is corrupted. There is a mismatch in the virtual sizes of the parent virtual hard disk and differencing disk.");

        /// <summary>
        /// The chain of virtual hard disks is corrupted. A differencing disk is indicated in its own parent chain.
        /// </summary>
        public static HRESULT ERROR_VHD_DIFFERENCING_CHAIN_CYCLE_DETECTED = new HRESULT("0xC03A0018", "ERROR_VHD_DIFFERENCING_CHAIN_CYCLE_DETECTED", "The chain of virtual hard disks is corrupted. A differencing disk is indicated in its own parent chain.");

        /// <summary>
        /// The chain of virtual hard disks is inaccessible. There was an error opening a virtual hard disk further up the chain.
        /// </summary>
        public static HRESULT ERROR_VHD_DIFFERENCING_CHAIN_ERROR_IN_PARENT = new HRESULT("0xC03A0019", "ERROR_VHD_DIFFERENCING_CHAIN_ERROR_IN_PARENT", "The chain of virtual hard disks is inaccessible. There was an error opening a virtual hard disk further up the chain.");

        /// <summary>
        /// The requested operation could not be completed due to a virtual disk system limitation. On NTFS, virtual hard disk files must be uncompressed and unencrypted. On ReFS, virtual hard disk files must not have the integrity bit set.
        /// </summary>
        public static HRESULT ERROR_VIRTUAL_DISK_LIMITATION = new HRESULT("0xC03A001A", "ERROR_VIRTUAL_DISK_LIMITATION", "The requested operation could not be completed due to a virtual disk system limitation. On NTFS, virtual hard disk files must be uncompressed and unencrypted. On ReFS, virtual hard disk files must not have the integrity bit set.");

        /// <summary>
        /// The requested operation cannot be performed on a virtual disk of this type.
        /// </summary>
        public static HRESULT ERROR_VHD_INVALID_TYPE = new HRESULT("0xC03A001B", "ERROR_VHD_INVALID_TYPE", "The requested operation cannot be performed on a virtual disk of this type.");

        /// <summary>
        /// The requested operation cannot be performed on the virtual disk in its current state.
        /// </summary>
        public static HRESULT ERROR_VHD_INVALID_STATE = new HRESULT("0xC03A001C", "ERROR_VHD_INVALID_STATE", "The requested operation cannot be performed on the virtual disk in its current state.");

        /// <summary>
        /// The sector size of the physical disk on which the virtual disk resides is not supported.
        /// </summary>
        public static HRESULT ERROR_VIRTDISK_UNSUPPORTED_DISK_SECTOR_SIZE = new HRESULT("0xC03A001D", "ERROR_VIRTDISK_UNSUPPORTED_DISK_SECTOR_SIZE", "The sector size of the physical disk on which the virtual disk resides is not supported.");

        /// <summary>
        /// The disk is already owned by a different owner.
        /// </summary>
        public static HRESULT ERROR_VIRTDISK_DISK_ALREADY_OWNED = new HRESULT("0xC03A001E", "ERROR_VIRTDISK_DISK_ALREADY_OWNED", "The disk is already owned by a different owner.");

        /// <summary>
        /// The disk must be offline or read-only.
        /// </summary>
        public static HRESULT ERROR_VIRTDISK_DISK_ONLINE_AND_WRITABLE = new HRESULT("0xC03A001F", "ERROR_VIRTDISK_DISK_ONLINE_AND_WRITABLE", "The disk must be offline or read-only.");

        /// <summary>
        /// Change Tracking is not initialized for this Virtual Disk.
        /// </summary>
        public static HRESULT ERROR_CTLOG_TRACKING_NOT_INITIALIZED = new HRESULT("0xC03A0020", "ERROR_CTLOG_TRACKING_NOT_INITIALIZED", "Change Tracking is not initialized for this Virtual Disk.");

        /// <summary>
        /// Size of change tracking file exceeded the maximum size limit
        /// </summary>
        public static HRESULT ERROR_CTLOG_LOGFILE_SIZE_EXCEEDED_MAXSIZE = new HRESULT("0xC03A0021", "ERROR_CTLOG_LOGFILE_SIZE_EXCEEDED_MAXSIZE", "Size of change tracking file exceeded the maximum size limit");

        /// <summary>
        /// VHD file is changed due to compaction, expansion or offline patching
        /// </summary>
        public static HRESULT ERROR_CTLOG_VHD_CHANGED_OFFLINE = new HRESULT("0xC03A0022", "ERROR_CTLOG_VHD_CHANGED_OFFLINE", "VHD file is changed due to compaction, expansion or offline patching");

        /// <summary>
        /// Change Tracking for the virtual disk is not in a valid state to perform this request. Change tracking could be discontinued or already in the requested state.
        /// </summary>
        public static HRESULT ERROR_CTLOG_INVALID_TRACKING_STATE = new HRESULT("0xC03A0023", "ERROR_CTLOG_INVALID_TRACKING_STATE", "Change Tracking for the virtual disk is not in a valid state to perform this request. Change tracking could be discontinued or already in the requested state.");

        /// <summary>
        /// Change Tracking file for the virtual disk is not in a valid state.
        /// </summary>
        public static HRESULT ERROR_CTLOG_INCONSISTANT_TRACKING_FILE = new HRESULT("0xC03A0024", "ERROR_CTLOG_INCONSISTANT_TRACKING_FILE", "Change Tracking file for the virtual disk is not in a valid state.");

        /// <summary>
        /// The requested resize operation could not be completed because it might truncate user data residing on the virtual disk.
        /// </summary>
        public static HRESULT ERROR_VHD_RESIZE_WOULD_TRUNCATE_DATA = new HRESULT("0xC03A0025", "ERROR_VHD_RESIZE_WOULD_TRUNCATE_DATA", "The requested resize operation could not be completed because it might truncate user data residing on the virtual disk.");

        /// <summary>
        /// The requested operation could not be completed because the virtual disk's minimum safe size could not be determined. This may be due to a missing or corrupt partition table.
        /// </summary>
        public static HRESULT ERROR_VHD_COULD_NOT_COMPUTE_MINIMUM_VIRTUAL_SIZE = new HRESULT("0xC03A0026", "ERROR_VHD_COULD_NOT_COMPUTE_MINIMUM_VIRTUAL_SIZE", "The requested operation could not be completed because the virtual disk's minimum safe size could not be determined. This may be due to a missing or corrupt partition table.");

        /// <summary>
        /// The requested operation could not be completed because the virtual disk's size cannot be safely reduced further.
        /// </summary>
        public static HRESULT ERROR_VHD_ALREADY_AT_OR_BELOW_MINIMUM_VIRTUAL_SIZE = new HRESULT("0xC03A0027", "ERROR_VHD_ALREADY_AT_OR_BELOW_MINIMUM_VIRTUAL_SIZE", "The requested operation could not be completed because the virtual disk's size cannot be safely reduced further.");

        /// <summary>
        /// There is not enough space in the virtual disk file for the provided metadata item.
        /// </summary>
        public static HRESULT ERROR_VHD_METADATA_FULL = new HRESULT("0xC03A0028", "ERROR_VHD_METADATA_FULL", "There is not enough space in the virtual disk file for the provided metadata item.");

        /// <summary>
        /// The virtualization storage subsystem has generated an error.
        /// </summary>
        public static HRESULT ERROR_QUERY_STORAGE_ERROR = new HRESULT("0x803A0001", "ERROR_QUERY_STORAGE_ERROR", "The virtualization storage subsystem has generated an error.");

        /// <summary>
        /// The operation was canceled.
        /// </summary>
        public static HRESULT SDIAG_E_CANCELLED = new HRESULT("0x803C0100", "SDIAG_E_CANCELLED", "The operation was canceled.");

        /// <summary>
        /// An error occurred when running a PowerShell script.
        /// </summary>
        public static HRESULT SDIAG_E_SCRIPT = new HRESULT("0x803C0101", "SDIAG_E_SCRIPT", "An error occurred when running a PowerShell script.");

        /// <summary>
        /// An error occurred when interacting with PowerShell runtime.
        /// </summary>
        public static HRESULT SDIAG_E_POWERSHELL = new HRESULT("0x803C0102", "SDIAG_E_POWERSHELL", "An error occurred when interacting with PowerShell runtime.");

        /// <summary>
        /// An error occurred in the Scripted Diagnostic Managed Host.
        /// </summary>
        public static HRESULT SDIAG_E_MANAGEDHOST = new HRESULT("0x803C0103", "SDIAG_E_MANAGEDHOST", "An error occurred in the Scripted Diagnostic Managed Host.");

        /// <summary>
        /// The troubleshooting pack does not contain a required verifier to complete the verification.
        /// </summary>
        public static HRESULT SDIAG_E_NOVERIFIER = new HRESULT("0x803C0104", "SDIAG_E_NOVERIFIER", "The troubleshooting pack does not contain a required verifier to complete the verification.");

        /// <summary>
        /// The troubleshooting pack cannot be executed on this system.
        /// </summary>
        public static HRESULT SDIAG_S_CANNOTRUN = new HRESULT("0x003C0105", "SDIAG_S_CANNOTRUN", "The troubleshooting pack cannot be executed on this system.");

        /// <summary>
        /// Scripted diagnostics is disabled by group policy.
        /// </summary>
        public static HRESULT SDIAG_E_DISABLED = new HRESULT("0x803C0106", "SDIAG_E_DISABLED", "Scripted diagnostics is disabled by group policy.");

        /// <summary>
        /// Trust validation of the diagnostic package failed.
        /// </summary>
        public static HRESULT SDIAG_E_TRUST = new HRESULT("0x803C0107", "SDIAG_E_TRUST", "Trust validation of the diagnostic package failed.");

        /// <summary>
        /// The troubleshooting pack cannot be executed on this system.
        /// </summary>
        public static HRESULT SDIAG_E_CANNOTRUN = new HRESULT("0x803C0108", "SDIAG_E_CANNOTRUN", "The troubleshooting pack cannot be executed on this system.");

        /// <summary>
        /// This version of the troubleshooting pack is not supported.
        /// </summary>
        public static HRESULT SDIAG_E_VERSION = new HRESULT("0x803C0109", "SDIAG_E_VERSION", "This version of the troubleshooting pack is not supported.");

        /// <summary>
        /// A required resource cannot be loaded.
        /// </summary>
        public static HRESULT SDIAG_E_RESOURCE = new HRESULT("0x803C010A", "SDIAG_E_RESOURCE", "A required resource cannot be loaded.");
        #endregion

        #region COM Error Codes (WPN, MBN, P2P, Bluetooth)
        /// <summary>
        /// The notification channel has already been closed.
        /// </summary>
        public static HRESULT WPN_E_CHANNEL_CLOSED = new HRESULT("0x803E0100", "WPN_E_CHANNEL_CLOSED", "The notification channel has already been closed.");

        /// <summary>
        /// The notification channel request did not complete successfully.
        /// </summary>
        public static HRESULT WPN_E_CHANNEL_REQUEST_NOT_COMPLETE = new HRESULT("0x803E0101", "WPN_E_CHANNEL_REQUEST_NOT_COMPLETE", "The notification channel request did not complete successfully.");

        /// <summary>
        /// The application identifier provided is invalid.
        /// </summary>
        public static HRESULT WPN_E_INVALID_APP = new HRESULT("0x803E0102", "WPN_E_INVALID_APP", "The application identifier provided is invalid.");

        /// <summary>
        /// A notification channel request for the provided application identifier is in progress.
        /// </summary>
        public static HRESULT WPN_E_OUTSTANDING_CHANNEL_REQUEST = new HRESULT("0x803E0103", "WPN_E_OUTSTANDING_CHANNEL_REQUEST", "A notification channel request for the provided application identifier is in progress.");

        /// <summary>
        /// The channel identifier is already tied to another application endpoint.
        /// </summary>
        public static HRESULT WPN_E_DUPLICATE_CHANNEL = new HRESULT("0x803E0104", "WPN_E_DUPLICATE_CHANNEL", "The channel identifier is already tied to another application endpoint.");

        /// <summary>
        /// The notification platform is unavailable.
        /// </summary>
        public static HRESULT WPN_E_PLATFORM_UNAVAILABLE = new HRESULT("0x803E0105", "WPN_E_PLATFORM_UNAVAILABLE", "The notification platform is unavailable.");

        /// <summary>
        /// The notification has already been posted.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_POSTED = new HRESULT("0x803E0106", "WPN_E_NOTIFICATION_POSTED", "The notification has already been posted.");

        /// <summary>
        /// The notification has already been hidden.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_HIDDEN = new HRESULT("0x803E0107", "WPN_E_NOTIFICATION_HIDDEN", "The notification has already been hidden.");

        /// <summary>
        /// The notification cannot be hidden until it has been shown.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_NOT_POSTED = new HRESULT("0x803E0108", "WPN_E_NOTIFICATION_NOT_POSTED", "The notification cannot be hidden until it has been shown.");

        /// <summary>
        /// Cloud notifications have been turned off.
        /// </summary>
        public static HRESULT WPN_E_CLOUD_DISABLED = new HRESULT("0x803E0109", "WPN_E_CLOUD_DISABLED", "Cloud notifications have been turned off.");

        /// <summary>
        /// The application does not have the cloud notification capability.
        /// </summary>
        public static HRESULT WPN_E_CLOUD_INCAPABLE = new HRESULT("0x803E0110", "WPN_E_CLOUD_INCAPABLE", "The application does not have the cloud notification capability.");

        /// <summary>
        /// Settings prevent the notification from being delivered.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_DISABLED = new HRESULT("0x803E0111", "WPN_E_NOTIFICATION_DISABLED", "Settings prevent the notification from being delivered.");

        /// <summary>
        /// Application capabilities prevent the notification from being delivered.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_INCAPABLE = new HRESULT("0x803E0112", "WPN_E_NOTIFICATION_INCAPABLE", "Application capabilities prevent the notification from being delivered.");

        /// <summary>
        /// The application does not have the internet access capability.
        /// </summary>
        public static HRESULT WPN_E_INTERNET_INCAPABLE = new HRESULT("0x803E0113", "WPN_E_INTERNET_INCAPABLE", "The application does not have the internet access capability.");

        /// <summary>
        /// Settings prevent the notification type from being delivered.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_TYPE_DISABLED = new HRESULT("0x803E0114", "WPN_E_NOTIFICATION_TYPE_DISABLED", "Settings prevent the notification type from being delivered.");

        /// <summary>
        /// The size of the notification content is too large.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_SIZE = new HRESULT("0x803E0115", "WPN_E_NOTIFICATION_SIZE", "The size of the notification content is too large.");

        /// <summary>
        /// The size of the notification tag is too large.
        /// </summary>
        public static HRESULT WPN_E_TAG_SIZE = new HRESULT("0x803E0116", "WPN_E_TAG_SIZE", "The size of the notification tag is too large.");

        /// <summary>
        /// The notification platform doesn't have appropriate privilege on resources.
        /// </summary>
        public static HRESULT WPN_E_ACCESS_DENIED = new HRESULT("0x803E0117", "WPN_E_ACCESS_DENIED", "The notification platform doesn't have appropriate privilege on resources.");

        /// <summary>
        /// The notification platform found application is already registered.
        /// </summary>
        public static HRESULT WPN_E_DUPLICATE_REGISTRATION = new HRESULT("0x803E0118", "WPN_E_DUPLICATE_REGISTRATION", "The notification platform found application is already registered.");

        /// <summary>
        /// The notification platform has run out of presentation layer sessions.
        /// </summary>
        public static HRESULT WPN_E_OUT_OF_SESSION = new HRESULT("0x803E0200", "WPN_E_OUT_OF_SESSION", "The notification platform has run out of presentation layer sessions.");

        /// <summary>
        /// The notification platform rejects image download request due to system in power save mode.
        /// </summary>
        public static HRESULT WPN_E_POWER_SAVE = new HRESULT("0x803E0201", "WPN_E_POWER_SAVE", "The notification platform rejects image download request due to system in power save mode.");

        /// <summary>
        /// The notification platform doesn't have the requested image in its cache.
        /// </summary>
        public static HRESULT WPN_E_IMAGE_NOT_FOUND_IN_CACHE = new HRESULT("0x803E0202", "WPN_E_IMAGE_NOT_FOUND_IN_CACHE", "The notification platform doesn't have the requested image in its cache.");

        /// <summary>
        /// The notification platform cannot complete all of requested image.
        /// </summary>
        public static HRESULT WPN_E_ALL_URL_NOT_COMPLETED = new HRESULT("0x803E0203", "WPN_E_ALL_URL_NOT_COMPLETED", "The notification platform cannot complete all of requested image.");

        /// <summary>
        /// A cloud image downloaded from the notification platform is invalid.
        /// </summary>
        public static HRESULT WPN_E_INVALID_CLOUD_IMAGE = new HRESULT("0x803E0204", "WPN_E_INVALID_CLOUD_IMAGE", "A cloud image downloaded from the notification platform is invalid.");

        /// <summary>
        /// Notification Id provided as filter is matched with what the notification platform maintains.
        /// </summary>
        public static HRESULT WPN_E_NOTIFICATION_ID_MATCHED = new HRESULT("0x803E0205", "WPN_E_NOTIFICATION_ID_MATCHED", "Notification Id provided as filter is matched with what the notification platform maintains.");

        /// <summary>
        /// Notification callback interface is already registered.
        /// </summary>
        public static HRESULT WPN_E_CALLBACK_ALREADY_REGISTERED = new HRESULT("0x803E0206", "WPN_E_CALLBACK_ALREADY_REGISTERED", "Notification callback interface is already registered.");

        /// <summary>
        /// Toast Notification was dropped without being displayed to the user.
        /// </summary>
        public static HRESULT WPN_E_TOAST_NOTIFICATION_DROPPED = new HRESULT("0x803E0207", "WPN_E_TOAST_NOTIFICATION_DROPPED", "Toast Notification was dropped without being displayed to the user.");

        /// <summary>
        /// The notification platform does not have the proper privileges to complete the request.
        /// </summary>
        public static HRESULT WPN_E_STORAGE_LOCKED = new HRESULT("0x803E0208", "WPN_E_STORAGE_LOCKED", "The notification platform does not have the proper privileges to complete the request.");

        /// <summary>
        /// Context is not activated.
        /// </summary>
        public static HRESULT E_MBN_CONTEXT_NOT_ACTIVATED = new HRESULT("0x80548201", "E_MBN_CONTEXT_NOT_ACTIVATED", "Context is not activated.");

        /// <summary>
        /// Bad SIM is inserted.
        /// </summary>
        public static HRESULT E_MBN_BAD_SIM = new HRESULT("0x80548202", "E_MBN_BAD_SIM", "Bad SIM is inserted.");

        /// <summary>
        /// Requested data class is not available.
        /// </summary>
        public static HRESULT E_MBN_DATA_CLASS_NOT_AVAILABLE = new HRESULT("0x80548203", "E_MBN_DATA_CLASS_NOT_AVAILABLE", "Requested data class is not available.");

        /// <summary>
        /// Access point name (APN) or Access string is incorrect.
        /// </summary>
        public static HRESULT E_MBN_INVALID_ACCESS_STRING = new HRESULT("0x80548204", "E_MBN_INVALID_ACCESS_STRING", "Access point name (APN) or Access string is incorrect.");

        /// <summary>
        /// Max activated contexts have reached.
        /// </summary>
        public static HRESULT E_MBN_MAX_ACTIVATED_CONTEXTS = new HRESULT("0x80548205", "E_MBN_MAX_ACTIVATED_CONTEXTS", "Max activated contexts have reached.");

        /// <summary>
        /// Device is in packet detach state.
        /// </summary>
        public static HRESULT E_MBN_PACKET_SVC_DETACHED = new HRESULT("0x80548206", "E_MBN_PACKET_SVC_DETACHED", "Device is in packet detach state.");

        /// <summary>
        /// Provider is not visible.
        /// </summary>
        public static HRESULT E_MBN_PROVIDER_NOT_VISIBLE = new HRESULT("0x80548207", "E_MBN_PROVIDER_NOT_VISIBLE", "Provider is not visible.");

        /// <summary>
        /// Radio is powered off.
        /// </summary>
        public static HRESULT E_MBN_RADIO_POWER_OFF = new HRESULT("0x80548208", "E_MBN_RADIO_POWER_OFF", "Radio is powered off.");

        /// <summary>
        /// MBN subscription is not activated.
        /// </summary>
        public static HRESULT E_MBN_SERVICE_NOT_ACTIVATED = new HRESULT("0x80548209", "E_MBN_SERVICE_NOT_ACTIVATED", "MBN subscription is not activated.");

        /// <summary>
        /// SIM is not inserted.
        /// </summary>
        public static HRESULT E_MBN_SIM_NOT_INSERTED = new HRESULT("0x8054820A", "E_MBN_SIM_NOT_INSERTED", "SIM is not inserted.");

        /// <summary>
        /// Voice call in progress.
        /// </summary>
        public static HRESULT E_MBN_VOICE_CALL_IN_PROGRESS = new HRESULT("0x8054820B", "E_MBN_VOICE_CALL_IN_PROGRESS", "Voice call in progress.");

        /// <summary>
        /// Visible provider cache is invalid.
        /// </summary>
        public static HRESULT E_MBN_INVALID_CACHE = new HRESULT("0x8054820C", "E_MBN_INVALID_CACHE", "Visible provider cache is invalid.");

        /// <summary>
        /// Device is not registered.
        /// </summary>
        public static HRESULT E_MBN_NOT_REGISTERED = new HRESULT("0x8054820D", "E_MBN_NOT_REGISTERED", "Device is not registered.");

        /// <summary>
        /// Providers not found.
        /// </summary>
        public static HRESULT E_MBN_PROVIDERS_NOT_FOUND = new HRESULT("0x8054820E", "E_MBN_PROVIDERS_NOT_FOUND", "Providers not found.");

        /// <summary>
        /// Pin is not supported.
        /// </summary>
        public static HRESULT E_MBN_PIN_NOT_SUPPORTED = new HRESULT("0x8054820F", "E_MBN_PIN_NOT_SUPPORTED", "Pin is not supported.");

        /// <summary>
        /// Pin is required.
        /// </summary>
        public static HRESULT E_MBN_PIN_REQUIRED = new HRESULT("0x80548210", "E_MBN_PIN_REQUIRED", "Pin is required.");

        /// <summary>
        /// PIN is disabled.
        /// </summary>
        public static HRESULT E_MBN_PIN_DISABLED = new HRESULT("0x80548211", "E_MBN_PIN_DISABLED", "PIN is disabled.");

        /// <summary>
        /// Generic Failure.
        /// </summary>
        public static HRESULT E_MBN_FAILURE = new HRESULT("0x80548212", "E_MBN_FAILURE", "Generic Failure.");

        /// <summary>
        /// Profile is invalid.
        /// </summary>
        public static HRESULT E_MBN_INVALID_PROFILE = new HRESULT("0x80548218", "E_MBN_INVALID_PROFILE", "Profile is invalid.");

        /// <summary>
        /// Default profile exist.
        /// </summary>
        public static HRESULT E_MBN_DEFAULT_PROFILE_EXIST = new HRESULT("0x80548219", "E_MBN_DEFAULT_PROFILE_EXIST", "Default profile exist.");

        /// <summary>
        /// SMS encoding is not supported.
        /// </summary>
        public static HRESULT E_MBN_SMS_ENCODING_NOT_SUPPORTED = new HRESULT("0x80548220", "E_MBN_SMS_ENCODING_NOT_SUPPORTED", "SMS encoding is not supported.");

        /// <summary>
        /// SMS filter is not supported.
        /// </summary>
        public static HRESULT E_MBN_SMS_FILTER_NOT_SUPPORTED = new HRESULT("0x80548221", "E_MBN_SMS_FILTER_NOT_SUPPORTED", "SMS filter is not supported.");

        /// <summary>
        /// Invalid SMS memory index is used.
        /// </summary>
        public static HRESULT E_MBN_SMS_INVALID_MEMORY_INDEX = new HRESULT("0x80548222", "E_MBN_SMS_INVALID_MEMORY_INDEX", "Invalid SMS memory index is used.");

        /// <summary>
        /// SMS language is not supported.
        /// </summary>
        public static HRESULT E_MBN_SMS_LANG_NOT_SUPPORTED = new HRESULT("0x80548223", "E_MBN_SMS_LANG_NOT_SUPPORTED", "SMS language is not supported.");

        /// <summary>
        /// SMS memory failure occurred.
        /// </summary>
        public static HRESULT E_MBN_SMS_MEMORY_FAILURE = new HRESULT("0x80548224", "E_MBN_SMS_MEMORY_FAILURE", "SMS memory failure occurred.");

        /// <summary>
        /// SMS network timeout happened.
        /// </summary>
        public static HRESULT E_MBN_SMS_NETWORK_TIMEOUT = new HRESULT("0x80548225", "E_MBN_SMS_NETWORK_TIMEOUT", "SMS network timeout happened.");

        /// <summary>
        /// Unknown SMSC address is used.
        /// </summary>
        public static HRESULT E_MBN_SMS_UNKNOWN_SMSC_ADDRESS = new HRESULT("0x80548226", "E_MBN_SMS_UNKNOWN_SMSC_ADDRESS", "Unknown SMSC address is used.");

        /// <summary>
        /// SMS format is not supported.
        /// </summary>
        public static HRESULT E_MBN_SMS_FORMAT_NOT_SUPPORTED = new HRESULT("0x80548227", "E_MBN_SMS_FORMAT_NOT_SUPPORTED", "SMS format is not supported.");

        /// <summary>
        /// SMS operation is not allowed.
        /// </summary>
        public static HRESULT E_MBN_SMS_OPERATION_NOT_ALLOWED = new HRESULT("0x80548228", "E_MBN_SMS_OPERATION_NOT_ALLOWED", "SMS operation is not allowed.");

        /// <summary>
        /// Device SMS memory is full.
        /// </summary>
        public static HRESULT E_MBN_SMS_MEMORY_FULL = new HRESULT("0x80548229", "E_MBN_SMS_MEMORY_FULL", "Device SMS memory is full.");

        /// <summary>
        /// The IPv6 protocol is not installed.
        /// </summary>
        public static HRESULT PEER_E_IPV6_NOT_INSTALLED = new HRESULT("0x80630001", "PEER_E_IPV6_NOT_INSTALLED", "The IPv6 protocol is not installed.");

        /// <summary>
        /// The compoment has not been initialized.
        /// </summary>
        public static HRESULT PEER_E_NOT_INITIALIZED = new HRESULT("0x80630002", "PEER_E_NOT_INITIALIZED", "The compoment has not been initialized.");

        /// <summary>
        /// The required service canot be started.
        /// </summary>
        public static HRESULT PEER_E_CANNOT_START_SERVICE = new HRESULT("0x80630003", "PEER_E_CANNOT_START_SERVICE", "The required service canot be started.");

        /// <summary>
        /// The P2P protocol is not licensed to run on this OS.
        /// </summary>
        public static HRESULT PEER_E_NOT_LICENSED = new HRESULT("0x80630004", "PEER_E_NOT_LICENSED", "The P2P protocol is not licensed to run on this OS.");

        /// <summary>
        /// The graph handle is invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_GRAPH = new HRESULT("0x80630010", "PEER_E_INVALID_GRAPH", "The graph handle is invalid.");

        /// <summary>
        /// The GRaphing database name has changed.
        /// </summary>
        public static HRESULT PEER_E_DBNAME_CHANGED = new HRESULT("0x80630011", "PEER_E_DBNAME_CHANGED", "The GRaphing database name has changed.");

        /// <summary>
        /// A graph with the same ID already exists.
        /// </summary>
        public static HRESULT PEER_E_DUPLICATE_GRAPH = new HRESULT("0x80630012", "PEER_E_DUPLICATE_GRAPH", "A graph with the same ID already exists.");

        /// <summary>
        /// The graph is not ready.
        /// </summary>
        public static HRESULT PEER_E_GRAPH_NOT_READY = new HRESULT("0x80630013", "PEER_E_GRAPH_NOT_READY", "The graph is not ready.");

        /// <summary>
        /// The graph is shutting down.
        /// </summary>
        public static HRESULT PEER_E_GRAPH_SHUTTING_DOWN = new HRESULT("0x80630014", "PEER_E_GRAPH_SHUTTING_DOWN", "The graph is shutting down.");

        /// <summary>
        /// The graph is still in use.
        /// </summary>
        public static HRESULT PEER_E_GRAPH_IN_USE = new HRESULT("0x80630015", "PEER_E_GRAPH_IN_USE", "The graph is still in use.");

        /// <summary>
        /// The graph database is corrupt.
        /// </summary>
        public static HRESULT PEER_E_INVALID_DATABASE = new HRESULT("0x80630016", "PEER_E_INVALID_DATABASE", "The graph database is corrupt.");

        /// <summary>
        /// Too many attributes have been used.
        /// </summary>
        public static HRESULT PEER_E_TOO_MANY_ATTRIBUTES = new HRESULT("0x80630017", "PEER_E_TOO_MANY_ATTRIBUTES", "Too many attributes have been used.");

        /// <summary>
        /// The connection can not be found.
        /// </summary>
        public static HRESULT PEER_E_CONNECTION_NOT_FOUND = new HRESULT("0x80630103", "PEER_E_CONNECTION_NOT_FOUND", "The connection can not be found.");

        /// <summary>
        /// The peer attempted to connect to itself.
        /// </summary>
        public static HRESULT PEER_E_CONNECT_SELF = new HRESULT("0x80630106", "PEER_E_CONNECT_SELF", "The peer attempted to connect to itself.");

        /// <summary>
        /// The peer is already listening for connections.
        /// </summary>
        public static HRESULT PEER_E_ALREADY_LISTENING = new HRESULT("0x80630107", "PEER_E_ALREADY_LISTENING", "The peer is already listening for connections.");

        /// <summary>
        /// The node was not found.
        /// </summary>
        public static HRESULT PEER_E_NODE_NOT_FOUND = new HRESULT("0x80630108", "PEER_E_NODE_NOT_FOUND", "The node was not found.");

        /// <summary>
        /// The Connection attempt failed.
        /// </summary>
        public static HRESULT PEER_E_CONNECTION_FAILED = new HRESULT("0x80630109", "PEER_E_CONNECTION_FAILED", "The Connection attempt failed.");

        /// <summary>
        /// The peer connection could not be authenticated.
        /// </summary>
        public static HRESULT PEER_E_CONNECTION_NOT_AUTHENTICATED = new HRESULT("0x8063010A", "PEER_E_CONNECTION_NOT_AUTHENTICATED", "The peer connection could not be authenticated.");

        /// <summary>
        /// The connection was refused.
        /// </summary>
        public static HRESULT PEER_E_CONNECTION_REFUSED = new HRESULT("0x8063010B", "PEER_E_CONNECTION_REFUSED", "The connection was refused.");

        /// <summary>
        /// The peer name classifer is too long.
        /// </summary>
        public static HRESULT PEER_E_CLASSIFIER_TOO_LONG = new HRESULT("0x80630201", "PEER_E_CLASSIFIER_TOO_LONG", "The peer name classifer is too long.");

        /// <summary>
        /// The maximum number of identies have been created.
        /// </summary>
        public static HRESULT PEER_E_TOO_MANY_IDENTITIES = new HRESULT("0x80630202", "PEER_E_TOO_MANY_IDENTITIES", "The maximum number of identies have been created.");

        /// <summary>
        /// Unable to access a key.
        /// </summary>
        public static HRESULT PEER_E_NO_KEY_ACCESS = new HRESULT("0x80630203", "PEER_E_NO_KEY_ACCESS", "Unable to access a key.");

        /// <summary>
        /// The group already exists.
        /// </summary>
        public static HRESULT PEER_E_GROUPS_EXIST = new HRESULT("0x80630204", "PEER_E_GROUPS_EXIST", "The group already exists.");

        /// <summary>
        /// The requested record could not be found.
        /// </summary>
        public static HRESULT PEER_E_RECORD_NOT_FOUND = new HRESULT("0x80630301", "PEER_E_RECORD_NOT_FOUND", "The requested record could not be found.");

        /// <summary>
        /// Access to the database was denied.
        /// </summary>
        public static HRESULT PEER_E_DATABASE_ACCESSDENIED = new HRESULT("0x80630302", "PEER_E_DATABASE_ACCESSDENIED", "Access to the database was denied.");

        /// <summary>
        /// The Database could not be initialized.
        /// </summary>
        public static HRESULT PEER_E_DBINITIALIZATION_FAILED = new HRESULT("0x80630303", "PEER_E_DBINITIALIZATION_FAILED", "The Database could not be initialized.");

        /// <summary>
        /// The record is too big.
        /// </summary>
        public static HRESULT PEER_E_MAX_RECORD_SIZE_EXCEEDED = new HRESULT("0x80630304", "PEER_E_MAX_RECORD_SIZE_EXCEEDED", "The record is too big.");

        /// <summary>
        /// The database already exists.
        /// </summary>
        public static HRESULT PEER_E_DATABASE_ALREADY_PRESENT = new HRESULT("0x80630305", "PEER_E_DATABASE_ALREADY_PRESENT", "The database already exists.");

        /// <summary>
        /// The database could not be found.
        /// </summary>
        public static HRESULT PEER_E_DATABASE_NOT_PRESENT = new HRESULT("0x80630306", "PEER_E_DATABASE_NOT_PRESENT", "The database could not be found.");

        /// <summary>
        /// The identity could not be found.
        /// </summary>
        public static HRESULT PEER_E_IDENTITY_NOT_FOUND = new HRESULT("0x80630401", "PEER_E_IDENTITY_NOT_FOUND", "The identity could not be found.");

        /// <summary>
        /// The event handle could not be found.
        /// </summary>
        public static HRESULT PEER_E_EVENT_HANDLE_NOT_FOUND = new HRESULT("0x80630501", "PEER_E_EVENT_HANDLE_NOT_FOUND", "The event handle could not be found.");

        /// <summary>
        /// Invalid search.
        /// </summary>
        public static HRESULT PEER_E_INVALID_SEARCH = new HRESULT("0x80630601", "PEER_E_INVALID_SEARCH", "Invalid search.");

        /// <summary>
        /// The search atributes are invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_ATTRIBUTES = new HRESULT("0x80630602", "PEER_E_INVALID_ATTRIBUTES", "The search atributes are invalid.");

        /// <summary>
        /// The invitiation is not trusted.
        /// </summary>
        public static HRESULT PEER_E_INVITATION_NOT_TRUSTED = new HRESULT("0x80630701", "PEER_E_INVITATION_NOT_TRUSTED", "The invitiation is not trusted.");

        /// <summary>
        /// The certchain is too long.
        /// </summary>
        public static HRESULT PEER_E_CHAIN_TOO_LONG = new HRESULT("0x80630703", "PEER_E_CHAIN_TOO_LONG", "The certchain is too long.");

        /// <summary>
        /// The time period is invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_TIME_PERIOD = new HRESULT("0x80630705", "PEER_E_INVALID_TIME_PERIOD", "The time period is invalid.");

        /// <summary>
        /// A circular cert chain was detected.
        /// </summary>
        public static HRESULT PEER_E_CIRCULAR_CHAIN_DETECTED = new HRESULT("0x80630706", "PEER_E_CIRCULAR_CHAIN_DETECTED", "A circular cert chain was detected.");

        /// <summary>
        /// The certstore is corrupted.
        /// </summary>
        public static HRESULT PEER_E_CERT_STORE_CORRUPTED = new HRESULT("0x80630801", "PEER_E_CERT_STORE_CORRUPTED", "The certstore is corrupted.");

        /// <summary>
        /// The specified PNRP cloud deos not exist.
        /// </summary>
        public static HRESULT PEER_E_NO_CLOUD = new HRESULT("0x80631001", "PEER_E_NO_CLOUD", "The specified PNRP cloud deos not exist.");

        /// <summary>
        /// The cloud name is ambiguous.
        /// </summary>
        public static HRESULT PEER_E_CLOUD_NAME_AMBIGUOUS = new HRESULT("0x80631005", "PEER_E_CLOUD_NAME_AMBIGUOUS", "The cloud name is ambiguous.");

        /// <summary>
        /// The record is invlaid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_RECORD = new HRESULT("0x80632010", "PEER_E_INVALID_RECORD", "The record is invlaid.");

        /// <summary>
        /// Not authorized.
        /// </summary>
        public static HRESULT PEER_E_NOT_AUTHORIZED = new HRESULT("0x80632020", "PEER_E_NOT_AUTHORIZED", "Not authorized.");

        /// <summary>
        /// The password does not meet policy requirements.
        /// </summary>
        public static HRESULT PEER_E_PASSWORD_DOES_NOT_MEET_POLICY = new HRESULT("0x80632021", "PEER_E_PASSWORD_DOES_NOT_MEET_POLICY", "The password does not meet policy requirements.");

        /// <summary>
        /// The record validation has been defered.
        /// </summary>
        public static HRESULT PEER_E_DEFERRED_VALIDATION = new HRESULT("0x80632030", "PEER_E_DEFERRED_VALIDATION", "The record validation has been defered.");

        /// <summary>
        /// The group properies are invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_GROUP_PROPERTIES = new HRESULT("0x80632040", "PEER_E_INVALID_GROUP_PROPERTIES", "The group properies are invalid.");

        /// <summary>
        /// The peername is invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_PEER_NAME = new HRESULT("0x80632050", "PEER_E_INVALID_PEER_NAME", "The peername is invalid.");

        /// <summary>
        /// The classifier is invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_CLASSIFIER = new HRESULT("0x80632060", "PEER_E_INVALID_CLASSIFIER", "The classifier is invalid.");

        /// <summary>
        /// The friendly name is invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_FRIENDLY_NAME = new HRESULT("0x80632070", "PEER_E_INVALID_FRIENDLY_NAME", "The friendly name is invalid.");

        /// <summary>
        /// Invalid role property.
        /// </summary>
        public static HRESULT PEER_E_INVALID_ROLE_PROPERTY = new HRESULT("0x80632071", "PEER_E_INVALID_ROLE_PROPERTY", "Invalid role property.");

        /// <summary>
        /// Invalid classifer protopery.
        /// </summary>
        public static HRESULT PEER_E_INVALID_CLASSIFIER_PROPERTY = new HRESULT("0x80632072", "PEER_E_INVALID_CLASSIFIER_PROPERTY", "Invalid classifer protopery.");

        /// <summary>
        /// Invlaid record expiration.
        /// </summary>
        public static HRESULT PEER_E_INVALID_RECORD_EXPIRATION = new HRESULT("0x80632080", "PEER_E_INVALID_RECORD_EXPIRATION", "Invlaid record expiration.");

        /// <summary>
        /// Invlaid credential info.
        /// </summary>
        public static HRESULT PEER_E_INVALID_CREDENTIAL_INFO = new HRESULT("0x80632081", "PEER_E_INVALID_CREDENTIAL_INFO", "Invlaid credential info.");

        /// <summary>
        /// Invalid credential.
        /// </summary>
        public static HRESULT PEER_E_INVALID_CREDENTIAL = new HRESULT("0x80632082", "PEER_E_INVALID_CREDENTIAL", "Invalid credential.");

        /// <summary>
        /// Invalid record size.
        /// </summary>
        public static HRESULT PEER_E_INVALID_RECORD_SIZE = new HRESULT("0x80632083", "PEER_E_INVALID_RECORD_SIZE", "Invalid record size.");

        /// <summary>
        /// Unsupported version.
        /// </summary>
        public static HRESULT PEER_E_UNSUPPORTED_VERSION = new HRESULT("0x80632090", "PEER_E_UNSUPPORTED_VERSION", "Unsupported version.");

        /// <summary>
        /// The group is not ready.
        /// </summary>
        public static HRESULT PEER_E_GROUP_NOT_READY = new HRESULT("0x80632091", "PEER_E_GROUP_NOT_READY", "The group is not ready.");

        /// <summary>
        /// The group is still in use.
        /// </summary>
        public static HRESULT PEER_E_GROUP_IN_USE = new HRESULT("0x80632092", "PEER_E_GROUP_IN_USE", "The group is still in use.");

        /// <summary>
        /// The group is invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_GROUP = new HRESULT("0x80632093", "PEER_E_INVALID_GROUP", "The group is invalid.");

        /// <summary>
        /// No members were found.
        /// </summary>
        public static HRESULT PEER_E_NO_MEMBERS_FOUND = new HRESULT("0x80632094", "PEER_E_NO_MEMBERS_FOUND", "No members were found.");

        /// <summary>
        /// There are no member connections.
        /// </summary>
        public static HRESULT PEER_E_NO_MEMBER_CONNECTIONS = new HRESULT("0x80632095", "PEER_E_NO_MEMBER_CONNECTIONS", "There are no member connections.");

        /// <summary>
        /// Unable to listen.
        /// </summary>
        public static HRESULT PEER_E_UNABLE_TO_LISTEN = new HRESULT("0x80632096", "PEER_E_UNABLE_TO_LISTEN", "Unable to listen.");

        /// <summary>
        /// The identity does not exist.
        /// </summary>
        public static HRESULT PEER_E_IDENTITY_DELETED = new HRESULT("0x806320A0", "PEER_E_IDENTITY_DELETED", "The identity does not exist.");

        /// <summary>
        /// The service is not availible.
        /// </summary>
        public static HRESULT PEER_E_SERVICE_NOT_AVAILABLE = new HRESULT("0x806320A1", "PEER_E_SERVICE_NOT_AVAILABLE", "The service is not availible.");

        /// <summary>
        /// THe contact could not be found.
        /// </summary>
        public static HRESULT PEER_E_CONTACT_NOT_FOUND = new HRESULT("0x80636001", "PEER_E_CONTACT_NOT_FOUND", "THe contact could not be found.");

        /// <summary>
        /// The graph data was created.
        /// </summary>
        public static HRESULT PEER_S_GRAPH_DATA_CREATED = new HRESULT("0x00630001", "PEER_S_GRAPH_DATA_CREATED", "The graph data was created.");

        /// <summary>
        /// There is not more event data.
        /// </summary>
        public static HRESULT PEER_S_NO_EVENT_DATA = new HRESULT("0x00630002", "PEER_S_NO_EVENT_DATA", "There is not more event data.");

        /// <summary>
        /// The graph is already connect.
        /// </summary>
        public static HRESULT PEER_S_ALREADY_CONNECTED = new HRESULT("0x00632000", "PEER_S_ALREADY_CONNECTED", "The graph is already connect.");

        /// <summary>
        /// The subscription already exists.
        /// </summary>
        public static HRESULT PEER_S_SUBSCRIPTION_EXISTS = new HRESULT("0x00636000", "PEER_S_SUBSCRIPTION_EXISTS", "The subscription already exists.");

        /// <summary>
        /// No connectivity.
        /// </summary>
        public static HRESULT PEER_S_NO_CONNECTIVITY = new HRESULT("0x00630005", "PEER_S_NO_CONNECTIVITY", "No connectivity.");

        /// <summary>
        /// Already a member.
        /// </summary>
        public static HRESULT PEER_S_ALREADY_A_MEMBER = new HRESULT("0x00630006", "PEER_S_ALREADY_A_MEMBER", "Already a member.");

        /// <summary>
        /// The peername could not be converted to a DNS pnrp name.
        /// </summary>
        public static HRESULT PEER_E_CANNOT_CONVERT_PEER_NAME = new HRESULT("0x80634001", "PEER_E_CANNOT_CONVERT_PEER_NAME", "The peername could not be converted to a DNS pnrp name.");

        /// <summary>
        /// Invalid peer host name.
        /// </summary>
        public static HRESULT PEER_E_INVALID_PEER_HOST_NAME = new HRESULT("0x80634002", "PEER_E_INVALID_PEER_HOST_NAME", "Invalid peer host name.");

        /// <summary>
        /// No more data could be found.
        /// </summary>
        public static HRESULT PEER_E_NO_MORE = new HRESULT("0x80634003", "PEER_E_NO_MORE", "No more data could be found.");

        /// <summary>
        /// The existing peer name is already registered.
        /// </summary>
        public static HRESULT PEER_E_PNRP_DUPLICATE_PEER_NAME = new HRESULT("0x80634005", "PEER_E_PNRP_DUPLICATE_PEER_NAME", "The existing peer name is already registered.");

        /// <summary>
        /// The app invite request was canceld by the user.
        /// </summary>
        public static HRESULT PEER_E_INVITE_CANCELLED = new HRESULT("0x80637000", "PEER_E_INVITE_CANCELLED", "The app invite request was canceld by the user.");

        /// <summary>
        /// No respose ot the invite was received.
        /// </summary>
        public static HRESULT PEER_E_INVITE_RESPONSE_NOT_AVAILABLE = new HRESULT("0x80637001", "PEER_E_INVITE_RESPONSE_NOT_AVAILABLE", "No respose ot the invite was received.");

        /// <summary>
        /// User is not siged into serverless presence.
        /// </summary>
        public static HRESULT PEER_E_NOT_SIGNED_IN = new HRESULT("0x80637003", "PEER_E_NOT_SIGNED_IN", "User is not siged into serverless presence.");

        /// <summary>
        /// The user declinded the privacy policy prompt.
        /// </summary>
        public static HRESULT PEER_E_PRIVACY_DECLINED = new HRESULT("0x80637004", "PEER_E_PRIVACY_DECLINED", "The user declinded the privacy policy prompt.");

        /// <summary>
        /// A timeout occured.
        /// </summary>
        public static HRESULT PEER_E_TIMEOUT = new HRESULT("0x80637005", "PEER_E_TIMEOUT", "A timeout occured.");

        /// <summary>
        /// The address is invalid.
        /// </summary>
        public static HRESULT PEER_E_INVALID_ADDRESS = new HRESULT("0x80637007", "PEER_E_INVALID_ADDRESS", "The address is invalid.");

        /// <summary>
        /// A required firewall exception is disabled.
        /// </summary>
        public static HRESULT PEER_E_FW_EXCEPTION_DISABLED = new HRESULT("0x80637008", "PEER_E_FW_EXCEPTION_DISABLED", "A required firewall exception is disabled.");

        /// <summary>
        /// The service is block by a firewall policy.
        /// </summary>
        public static HRESULT PEER_E_FW_BLOCKED_BY_POLICY = new HRESULT("0x80637009", "PEER_E_FW_BLOCKED_BY_POLICY", "The service is block by a firewall policy.");

        /// <summary>
        /// Firewall exceptions are disabled.
        /// </summary>
        public static HRESULT PEER_E_FW_BLOCKED_BY_SHIELDS_UP = new HRESULT("0x8063700A", "PEER_E_FW_BLOCKED_BY_SHIELDS_UP", "Firewall exceptions are disabled.");

        /// <summary>
        /// THe user declinded to enable the firewall exceptions.
        /// </summary>
        public static HRESULT PEER_E_FW_DECLINED = new HRESULT("0x8063700B", "PEER_E_FW_DECLINED", "THe user declinded to enable the firewall exceptions.");

        /// <summary>
        /// The attribute handle given was not valid on this server.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INVALID_HANDLE = new HRESULT("0x80650001", "E_BLUETOOTH_ATT_INVALID_HANDLE", "The attribute handle given was not valid on this server.");

        /// <summary>
        /// The attribute cannot be read.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_READ_NOT_PERMITTED = new HRESULT("0x80650002", "E_BLUETOOTH_ATT_READ_NOT_PERMITTED", "The attribute cannot be read.");

        /// <summary>
        /// The attribute cannot be written.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_WRITE_NOT_PERMITTED = new HRESULT("0x80650003", "E_BLUETOOTH_ATT_WRITE_NOT_PERMITTED", "The attribute cannot be written.");

        /// <summary>
        /// The attribute PDU was invalid.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INVALID_PDU = new HRESULT("0x80650004", "E_BLUETOOTH_ATT_INVALID_PDU", "The attribute PDU was invalid.");

        /// <summary>
        /// The attribute requires authentication before it can be read or written.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INSUFFICIENT_AUTHENTICATION = new HRESULT("0x80650005", "E_BLUETOOTH_ATT_INSUFFICIENT_AUTHENTICATION", "The attribute requires authentication before it can be read or written.");

        /// <summary>
        /// Attribute server does not support the request received from the client.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_REQUEST_NOT_SUPPORTED = new HRESULT("0x80650006", "E_BLUETOOTH_ATT_REQUEST_NOT_SUPPORTED", "Attribute server does not support the request received from the client.");

        /// <summary>
        /// Offset specified was past the end of the attribute.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INVALID_OFFSET = new HRESULT("0x80650007", "E_BLUETOOTH_ATT_INVALID_OFFSET", "Offset specified was past the end of the attribute.");

        /// <summary>
        /// The attribute requires authorization before it can be read or written.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INSUFFICIENT_AUTHORIZATION = new HRESULT("0x80650008", "E_BLUETOOTH_ATT_INSUFFICIENT_AUTHORIZATION", "The attribute requires authorization before it can be read or written.");

        /// <summary>
        /// Too many prepare writes have been queued.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_PREPARE_QUEUE_FULL = new HRESULT("0x80650009", "E_BLUETOOTH_ATT_PREPARE_QUEUE_FULL", "Too many prepare writes have been queued.");

        /// <summary>
        /// No attribute found within the given attribute handle range.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_ATTRIBUTE_NOT_FOUND = new HRESULT("0x8065000A", "E_BLUETOOTH_ATT_ATTRIBUTE_NOT_FOUND", "No attribute found within the given attribute handle range.");

        /// <summary>
        /// The attribute cannot be read or written using the Read Blob Request.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_ATTRIBUTE_NOT_LONG = new HRESULT("0x8065000B", "E_BLUETOOTH_ATT_ATTRIBUTE_NOT_LONG", "The attribute cannot be read or written using the Read Blob Request.");

        /// <summary>
        /// The Encryption Key Size used for encrypting this link is insufficient.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INSUFFICIENT_ENCRYPTION_KEY_SIZE = new HRESULT("0x8065000C", "E_BLUETOOTH_ATT_INSUFFICIENT_ENCRYPTION_KEY_SIZE", "The Encryption Key Size used for encrypting this link is insufficient.");

        /// <summary>
        /// The attribute value length is invalid for the operation.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INVALID_ATTRIBUTE_VALUE_LENGTH = new HRESULT("0x8065000D", "E_BLUETOOTH_ATT_INVALID_ATTRIBUTE_VALUE_LENGTH", "The attribute value length is invalid for the operation.");

        /// <summary>
        /// The attribute request that was requested has encountered an error that was unlikely, and therefore could not be completed as requested.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_UNLIKELY = new HRESULT("0x8065000E", "E_BLUETOOTH_ATT_UNLIKELY", "The attribute request that was requested has encountered an error that was unlikely, and therefore could not be completed as requested.");

        /// <summary>
        /// The attribute requires encryption before it can be read or written.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INSUFFICIENT_ENCRYPTION = new HRESULT("0x8065000F", "E_BLUETOOTH_ATT_INSUFFICIENT_ENCRYPTION", "The attribute requires encryption before it can be read or written.");

        /// <summary>
        /// The attribute type is not a supported grouping attribute as defined by a higher layer specification.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_UNSUPPORTED_GROUP_TYPE = new HRESULT("0x80650010", "E_BLUETOOTH_ATT_UNSUPPORTED_GROUP_TYPE", "The attribute type is not a supported grouping attribute as defined by a higher layer specification.");

        /// <summary>
        /// Insufficient Resources to complete the request.
        /// </summary>
        public static HRESULT E_BLUETOOTH_ATT_INSUFFICIENT_RESOURCES = new HRESULT("0x80650011", "E_BLUETOOTH_ATT_INSUFFICIENT_RESOURCES", "Insufficient Resources to complete the request.");
        #endregion

        #region COM Error Codes (UI, Audio, DirectX, Codec)
        /// <summary>
        /// The object could not be created.
        /// </summary>
        public static HRESULT UI_E_CREATE_FAILED = new HRESULT("0x802A0001", "UI_E_CREATE_FAILED", "The object could not be created.");

        /// <summary>
        /// Shutdown was already called on this object or the object that owns it.
        /// </summary>
        public static HRESULT UI_E_SHUTDOWN_CALLED = new HRESULT("0x802A0002", "UI_E_SHUTDOWN_CALLED", "Shutdown was already called on this object or the object that owns it.");

        /// <summary>
        /// This method cannot be called during this type of callback.
        /// </summary>
        public static HRESULT UI_E_ILLEGAL_REENTRANCY = new HRESULT("0x802A0003", "UI_E_ILLEGAL_REENTRANCY", "This method cannot be called during this type of callback.");

        /// <summary>
        /// This object has been sealed, so this change is no longer allowed.
        /// </summary>
        public static HRESULT UI_E_OBJECT_SEALED = new HRESULT("0x802A0004", "UI_E_OBJECT_SEALED", "This object has been sealed, so this change is no longer allowed.");

        /// <summary>
        /// The requested value was never set.
        /// </summary>
        public static HRESULT UI_E_VALUE_NOT_SET = new HRESULT("0x802A0005", "UI_E_VALUE_NOT_SET", "The requested value was never set.");

        /// <summary>
        /// The requested value cannot be determined.
        /// </summary>
        public static HRESULT UI_E_VALUE_NOT_DETERMINED = new HRESULT("0x802A0006", "UI_E_VALUE_NOT_DETERMINED", "The requested value cannot be determined.");

        /// <summary>
        /// A callback returned an invalid output parameter.
        /// </summary>
        public static HRESULT UI_E_INVALID_OUTPUT = new HRESULT("0x802A0007", "UI_E_INVALID_OUTPUT", "A callback returned an invalid output parameter.");

        /// <summary>
        /// A callback returned a success code other than S_OK or S_FALSE.
        /// </summary>
        public static HRESULT UI_E_BOOLEAN_EXPECTED = new HRESULT("0x802A0008", "UI_E_BOOLEAN_EXPECTED", "A callback returned a success code other than S_OK or S_FALSE.");

        /// <summary>
        /// A parameter that should be owned by this object is owned by a different object.
        /// </summary>
        public static HRESULT UI_E_DIFFERENT_OWNER = new HRESULT("0x802A0009", "UI_E_DIFFERENT_OWNER", "A parameter that should be owned by this object is owned by a different object.");

        /// <summary>
        /// More than one item matched the search criteria.
        /// </summary>
        public static HRESULT UI_E_AMBIGUOUS_MATCH = new HRESULT("0x802A000A", "UI_E_AMBIGUOUS_MATCH", "More than one item matched the search criteria.");

        /// <summary>
        /// A floating-point overflow occurred.
        /// </summary>
        public static HRESULT UI_E_FP_OVERFLOW = new HRESULT("0x802A000B", "UI_E_FP_OVERFLOW", "A floating-point overflow occurred.");

        /// <summary>
        /// This method can only be called from the thread that created the object.
        /// </summary>
        public static HRESULT UI_E_WRONG_THREAD = new HRESULT("0x802A000C", "UI_E_WRONG_THREAD", "This method can only be called from the thread that created the object.");

        /// <summary>
        /// The storyboard is currently in the schedule.
        /// </summary>
        public static HRESULT UI_E_STORYBOARD_ACTIVE = new HRESULT("0x802A0101", "UI_E_STORYBOARD_ACTIVE", "The storyboard is currently in the schedule.");

        /// <summary>
        /// The storyboard is not playing.
        /// </summary>
        public static HRESULT UI_E_STORYBOARD_NOT_PLAYING = new HRESULT("0x802A0102", "UI_E_STORYBOARD_NOT_PLAYING", "The storyboard is not playing.");

        /// <summary>
        /// The start keyframe might occur after the end keyframe.
        /// </summary>
        public static HRESULT UI_E_START_KEYFRAME_AFTER_END = new HRESULT("0x802A0103", "UI_E_START_KEYFRAME_AFTER_END", "The start keyframe might occur after the end keyframe.");

        /// <summary>
        /// It might not be possible to determine the end keyframe time when the start keyframe is reached.
        /// </summary>
        public static HRESULT UI_E_END_KEYFRAME_NOT_DETERMINED = new HRESULT("0x802A0104", "UI_E_END_KEYFRAME_NOT_DETERMINED", "It might not be possible to determine the end keyframe time when the start keyframe is reached.");

        /// <summary>
        /// Two repeated portions of a storyboard might overlap.
        /// </summary>
        public static HRESULT UI_E_LOOPS_OVERLAP = new HRESULT("0x802A0105", "UI_E_LOOPS_OVERLAP", "Two repeated portions of a storyboard might overlap.");

        /// <summary>
        /// The transition has already been added to a storyboard.
        /// </summary>
        public static HRESULT UI_E_TRANSITION_ALREADY_USED = new HRESULT("0x802A0106", "UI_E_TRANSITION_ALREADY_USED", "The transition has already been added to a storyboard.");

        /// <summary>
        /// The transition has not been added to a storyboard.
        /// </summary>
        public static HRESULT UI_E_TRANSITION_NOT_IN_STORYBOARD = new HRESULT("0x802A0107", "UI_E_TRANSITION_NOT_IN_STORYBOARD", "The transition has not been added to a storyboard.");

        /// <summary>
        /// The transition might eclipse the beginning of another transition in the storyboard.
        /// </summary>
        public static HRESULT UI_E_TRANSITION_ECLIPSED = new HRESULT("0x802A0108", "UI_E_TRANSITION_ECLIPSED", "The transition might eclipse the beginning of another transition in the storyboard.");

        /// <summary>
        /// The given time is earlier than the time passed to the last update.
        /// </summary>
        public static HRESULT UI_E_TIME_BEFORE_LAST_UPDATE = new HRESULT("0x802A0109", "UI_E_TIME_BEFORE_LAST_UPDATE", "The given time is earlier than the time passed to the last update.");

        /// <summary>
        /// This client is already connected to a timer.
        /// </summary>
        public static HRESULT UI_E_TIMER_CLIENT_ALREADY_CONNECTED = new HRESULT("0x802A010A", "UI_E_TIMER_CLIENT_ALREADY_CONNECTED", "This client is already connected to a timer.");

        /// <summary>
        /// The passed dimension is invalid or does not match the object's dimension.
        /// </summary>
        public static HRESULT UI_E_INVALID_DIMENSION = new HRESULT("0x802A010B", "UI_E_INVALID_DIMENSION", "The passed dimension is invalid or does not match the object's dimension.");

        /// <summary>
        /// The added primitive begins at or beyond the duration of the interpolator.
        /// </summary>
        public static HRESULT UI_E_PRIMITIVE_OUT_OF_BOUNDS = new HRESULT("0x802A010C", "UI_E_PRIMITIVE_OUT_OF_BOUNDS", "The added primitive begins at or beyond the duration of the interpolator.");

        /// <summary>
        /// The operation cannot be completed because the window is being closed.
        /// </summary>
        public static HRESULT UI_E_WINDOW_CLOSED = new HRESULT("0x802A0201", "UI_E_WINDOW_CLOSED", "The operation cannot be completed because the window is being closed.");

        /// <summary>
        /// PortCls could not find an audio engine node exposed by a miniport driver claiming support for IMiniportAudioEngineNode.
        /// </summary>
        public static HRESULT E_AUDIO_ENGINE_NODE_NOT_FOUND = new HRESULT("0x80660001", "E_AUDIO_ENGINE_NODE_NOT_FOUND", "PortCls could not find an audio engine node exposed by a miniport driver claiming support for IMiniportAudioEngineNode.");

        /// <summary>
        /// The Present operation was invisible to the user.
        /// </summary>
        public static HRESULT DXGI_STATUS_OCCLUDED = new HRESULT("0x087A0001", "DXGI_STATUS_OCCLUDED", "The Present operation was invisible to the user.");

        /// <summary>
        /// The Present operation was partially invisible to the user.
        /// </summary>
        public static HRESULT DXGI_STATUS_CLIPPED = new HRESULT("0x087A0002", "DXGI_STATUS_CLIPPED", "The Present operation was partially invisible to the user.");

        /// <summary>
        /// The driver is requesting that the DXGI runtime not use shared resources to communicate with the Desktop Window Manager.
        /// </summary>
        public static HRESULT DXGI_STATUS_NO_REDIRECTION = new HRESULT("0x087A0004", "DXGI_STATUS_NO_REDIRECTION", "The driver is requesting that the DXGI runtime not use shared resources to communicate with the Desktop Window Manager.");

        /// <summary>
        /// The Present operation was not visible because the Windows session has switched to another desktop (for example, ctrl-alt-del).
        /// </summary>
        public static HRESULT DXGI_STATUS_NO_DESKTOP_ACCESS = new HRESULT("0x087A0005", "DXGI_STATUS_NO_DESKTOP_ACCESS", "The Present operation was not visible because the Windows session has switched to another desktop (for example, ctrl-alt-del).");

        /// <summary>
        /// The Present operation was not visible because the target monitor was being used for some other purpose.
        /// </summary>
        public static HRESULT DXGI_STATUS_GRAPHICS_VIDPN_SOURCE_IN_USE = new HRESULT("0x087A0006", "DXGI_STATUS_GRAPHICS_VIDPN_SOURCE_IN_USE", "The Present operation was not visible because the target monitor was being used for some other purpose.");

        /// <summary>
        /// The Present operation was not visible because the display mode changed. DXGI will have re-attempted the presentation.
        /// </summary>
        public static HRESULT DXGI_STATUS_MODE_CHANGED = new HRESULT("0x087A0007", "DXGI_STATUS_MODE_CHANGED", "The Present operation was not visible because the display mode changed. DXGI will have re-attempted the presentation.");

        /// <summary>
        /// The Present operation was not visible because another Direct3D device was attempting to take fullscreen mode at the time.
        /// </summary>
        public static HRESULT DXGI_STATUS_MODE_CHANGE_IN_PROGRESS = new HRESULT("0x087A0008", "DXGI_STATUS_MODE_CHANGE_IN_PROGRESS", "The Present operation was not visible because another Direct3D device was attempting to take fullscreen mode at the time.");

        /// <summary>
        /// The application made a call that is invalid. Either the parameters of the call or the state of some object was incorrect. Enable the D3D debug layer in order to see details via debug messages.
        /// </summary>
        public static HRESULT DXGI_ERROR_INVALID_CALL = new HRESULT("0x887A0001", "DXGI_ERROR_INVALID_CALL", "The application made a call that is invalid. Either the parameters of the call or the state of some object was incorrect. Enable the D3D debug layer in order to see details via debug messages.");

        /// <summary>
        /// The object was not found. If calling IDXGIFactory::EnumAdaptes, there is no adapter with the specified ordinal.
        /// </summary>
        public static HRESULT DXGI_ERROR_NOT_FOUND = new HRESULT("0x887A0002", "DXGI_ERROR_NOT_FOUND", "The object was not found. If calling IDXGIFactory::EnumAdaptes, there is no adapter with the specified ordinal.");

        /// <summary>
        /// The caller did not supply a sufficiently large buffer.
        /// </summary>
        public static HRESULT DXGI_ERROR_MORE_DATA = new HRESULT("0x887A0003", "DXGI_ERROR_MORE_DATA", "The caller did not supply a sufficiently large buffer.");

        /// <summary>
        /// The specified device interface or feature level is not supported on this system.
        /// </summary>
        public static HRESULT DXGI_ERROR_UNSUPPORTED = new HRESULT("0x887A0004", "DXGI_ERROR_UNSUPPORTED", "The specified device interface or feature level is not supported on this system.");

        /// <summary>
        /// The GPU device instance has been suspended. Use GetDeviceRemovedReason to determine the appropriate action.
        /// </summary>
        public static HRESULT DXGI_ERROR_DEVICE_REMOVED = new HRESULT("0x887A0005", "DXGI_ERROR_DEVICE_REMOVED", "The GPU device instance has been suspended. Use GetDeviceRemovedReason to determine the appropriate action.");

        /// <summary>
        /// The GPU will not respond to more commands, most likely because of an invalid command passed by the calling application.
        /// </summary>
        public static HRESULT DXGI_ERROR_DEVICE_HUNG = new HRESULT("0x887A0006", "DXGI_ERROR_DEVICE_HUNG", "The GPU will not respond to more commands, most likely because of an invalid command passed by the calling application.");

        /// <summary>
        /// The GPU will not respond to more commands, most likely because some other application submitted invalid commands. The calling application should re-create the device and continue.
        /// </summary>
        public static HRESULT DXGI_ERROR_DEVICE_RESET = new HRESULT("0x887A0007", "DXGI_ERROR_DEVICE_RESET", "The GPU will not respond to more commands, most likely because some other application submitted invalid commands. The calling application should re-create the device and continue.");

        /// <summary>
        /// The GPU was busy at the moment when the call was made, and the call was neither executed nor scheduled.
        /// </summary>
        public static HRESULT DXGI_ERROR_WAS_STILL_DRAWING = new HRESULT("0x887A000A", "DXGI_ERROR_WAS_STILL_DRAWING", "The GPU was busy at the moment when the call was made, and the call was neither executed nor scheduled.");

        /// <summary>
        /// An event (such as power cycle) interrupted the gathering of presentation statistics. Any previous statistics should be considered invalid.
        /// </summary>
        public static HRESULT DXGI_ERROR_FRAME_STATISTICS_DISJOINT = new HRESULT("0x887A000B", "DXGI_ERROR_FRAME_STATISTICS_DISJOINT", "An event (such as power cycle) interrupted the gathering of presentation statistics. Any previous statistics should be considered invalid.");

        /// <summary>
        /// Fullscreen mode could not be achieved because the specified output was already in use.
        /// </summary>
        public static HRESULT DXGI_ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE = new HRESULT("0x887A000C", "DXGI_ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE", "Fullscreen mode could not be achieved because the specified output was already in use.");

        /// <summary>
        /// An internal issue prevented the driver from carrying out the specified operation. The driver's state is probably suspect, and the application should not continue.
        /// </summary>
        public static HRESULT DXGI_ERROR_DRIVER_INTERNAL_ERROR = new HRESULT("0x887A0020", "DXGI_ERROR_DRIVER_INTERNAL_ERROR", "An internal issue prevented the driver from carrying out the specified operation. The driver's state is probably suspect, and the application should not continue.");

        /// <summary>
        /// A global counter resource was in use, and the specified counter cannot be used by this Direct3D device at this time.
        /// </summary>
        public static HRESULT DXGI_ERROR_NONEXCLUSIVE = new HRESULT("0x887A0021", "DXGI_ERROR_NONEXCLUSIVE", "A global counter resource was in use, and the specified counter cannot be used by this Direct3D device at this time.");

        /// <summary>
        /// A resource is not available at the time of the call, but may become available later.
        /// </summary>
        public static HRESULT DXGI_ERROR_NOT_CURRENTLY_AVAILABLE = new HRESULT("0x887A0022", "DXGI_ERROR_NOT_CURRENTLY_AVAILABLE", "A resource is not available at the time of the call, but may become available later.");

        /// <summary>
        /// The application's remote device has been removed due to session disconnect or network disconnect. The application should call IDXGIFactory1::IsCurrent to find out when the remote device becomes available again.
        /// </summary>
        public static HRESULT DXGI_ERROR_REMOTE_CLIENT_DISCONNECTED = new HRESULT("0x887A0023", "DXGI_ERROR_REMOTE_CLIENT_DISCONNECTED", "The application's remote device has been removed due to session disconnect or network disconnect. The application should call IDXGIFactory1::IsCurrent to find out when the remote device becomes available again.");

        /// <summary>
        /// The device has been removed during a remote session because the remote computer ran out of memory.
        /// </summary>
        public static HRESULT DXGI_ERROR_REMOTE_OUTOFMEMORY = new HRESULT("0x887A0024", "DXGI_ERROR_REMOTE_OUTOFMEMORY", "The device has been removed during a remote session because the remote computer ran out of memory.");

        /// <summary>
        /// The keyed mutex was abandoned.
        /// </summary>
        public static HRESULT DXGI_ERROR_ACCESS_LOST = new HRESULT("0x887A0026", "DXGI_ERROR_ACCESS_LOST", "The keyed mutex was abandoned.");

        /// <summary>
        /// The timeout value has elapsed and the resource is not yet available.
        /// </summary>
        public static HRESULT DXGI_ERROR_WAIT_TIMEOUT = new HRESULT("0x887A0027", "DXGI_ERROR_WAIT_TIMEOUT", "The timeout value has elapsed and the resource is not yet available.");

        /// <summary>
        /// The output duplication has been turned off because the Windows session ended or was disconnected. This happens when a remote user disconnects, or when &quot;switch user&quot; is used locally.
        /// </summary>
        public static HRESULT DXGI_ERROR_SESSION_DISCONNECTED = new HRESULT("0x887A0028", "DXGI_ERROR_SESSION_DISCONNECTED", "The output duplication has been turned off because the Windows session ended or was disconnected. This happens when a remote user disconnects, or when \"switch user\" is used locally.");

        /// <summary>
        /// The DXGI outuput (monitor) to which the swapchain content was restricted, has been disconnected or changed.
        /// </summary>
        public static HRESULT DXGI_ERROR_RESTRICT_TO_OUTPUT_STALE = new HRESULT("0x887A0029", "DXGI_ERROR_RESTRICT_TO_OUTPUT_STALE", "The DXGI outuput (monitor) to which the swapchain content was restricted, has been disconnected or changed.");

        /// <summary>
        /// DXGI is unable to provide content protection on the swapchain. This is typically caused by an older driver, or by the application using a swapchain that is incompatible with content protection.
        /// </summary>
        public static HRESULT DXGI_ERROR_CANNOT_PROTECT_CONTENT = new HRESULT("0x887A002A", "DXGI_ERROR_CANNOT_PROTECT_CONTENT", "DXGI is unable to provide content protection on the swapchain. This is typically caused by an older driver, or by the application using a swapchain that is incompatible with content protection.");

        /// <summary>
        /// The application is trying to use a resource to which it does not have the required access privileges. This is most commonly caused by writing to a shared resource with read-only access.
        /// </summary>
        public static HRESULT DXGI_ERROR_ACCESS_DENIED = new HRESULT("0x887A002B", "DXGI_ERROR_ACCESS_DENIED", "The application is trying to use a resource to which it does not have the required access privileges. This is most commonly caused by writing to a shared resource with read-only access.");

        /// <summary>
        /// The swapchain has become unoccluded.
        /// </summary>
        public static HRESULT DXGI_STATUS_UNOCCLUDED = new HRESULT("0x087A0009", "DXGI_STATUS_UNOCCLUDED", "The swapchain has become unoccluded.");

        /// <summary>
        /// The adapter did not have access to the required resources to complete the Desktop Duplication Present() call, the Present() call needs to be made again.
        /// </summary>
        public static HRESULT DXGI_STATUS_DDA_WAS_STILL_DRAWING = new HRESULT("0x087A000A", "DXGI_STATUS_DDA_WAS_STILL_DRAWING", "The adapter did not have access to the required resources to complete the Desktop Duplication Present() call, the Present() call needs to be made again.");

        /// <summary>
        /// An on-going mode change prevented completion of the call. The call may succeed if attempted later.
        /// </summary>
        public static HRESULT DXGI_ERROR_MODE_CHANGE_IN_PROGRESS = new HRESULT("0x887A0025", "DXGI_ERROR_MODE_CHANGE_IN_PROGRESS", "An on-going mode change prevented completion of the call. The call may succeed if attempted later.");

        /// <summary>
        /// The GPU was busy when the operation was requested.
        /// </summary>
        public static HRESULT DXGI_DDI_ERR_WASSTILLDRAWING = new HRESULT("0x887B0001", "DXGI_DDI_ERR_WASSTILLDRAWING", "The GPU was busy when the operation was requested.");

        /// <summary>
        /// The driver has rejected the creation of this resource.
        /// </summary>
        public static HRESULT DXGI_DDI_ERR_UNSUPPORTED = new HRESULT("0x887B0002", "DXGI_DDI_ERR_UNSUPPORTED", "The driver has rejected the creation of this resource.");

        /// <summary>
        /// The GPU counter was in use by another process or d3d device when application requested access to it.
        /// </summary>
        public static HRESULT DXGI_DDI_ERR_NONEXCLUSIVE = new HRESULT("0x887B0003", "DXGI_DDI_ERR_NONEXCLUSIVE", "The GPU counter was in use by another process or d3d device when application requested access to it.");

        /// <summary>
        /// The application has exceeded the maximum number of unique state objects per Direct3D device. The limit is 4096 for feature levels up to 11.1.
        /// </summary>
        public static HRESULT D3D10_ERROR_TOO_MANY_UNIQUE_STATE_OBJECTS = new HRESULT("0x88790001", "D3D10_ERROR_TOO_MANY_UNIQUE_STATE_OBJECTS", "The application has exceeded the maximum number of unique state objects per Direct3D device. The limit is 4096 for feature levels up to 11.1.");

        /// <summary>
        /// The specified file was not found.
        /// </summary>
        public static HRESULT D3D10_ERROR_FILE_NOT_FOUND = new HRESULT("0x88790002", "D3D10_ERROR_FILE_NOT_FOUND", "The specified file was not found.");

        /// <summary>
        /// The application has exceeded the maximum number of unique state objects per Direct3D device. The limit is 4096 for feature levels up to 11.1.
        /// </summary>
        public static HRESULT D3D11_ERROR_TOO_MANY_UNIQUE_STATE_OBJECTS = new HRESULT("0x887C0001", "D3D11_ERROR_TOO_MANY_UNIQUE_STATE_OBJECTS", "The application has exceeded the maximum number of unique state objects per Direct3D device. The limit is 4096 for feature levels up to 11.1.");

        /// <summary>
        /// The specified file was not found.
        /// </summary>
        public static HRESULT D3D11_ERROR_FILE_NOT_FOUND = new HRESULT("0x887C0002", "D3D11_ERROR_FILE_NOT_FOUND", "The specified file was not found.");

        /// <summary>
        /// The application has exceeded the maximum number of unique view objects per Direct3D device. The limit is 2^20 for feature levels up to 11.1.
        /// </summary>
        public static HRESULT D3D11_ERROR_TOO_MANY_UNIQUE_VIEW_OBJECTS = new HRESULT("0x887C0003", "D3D11_ERROR_TOO_MANY_UNIQUE_VIEW_OBJECTS", "The application has exceeded the maximum number of unique view objects per Direct3D device. The limit is 2^20 for feature levels up to 11.1.");

        /// <summary>
        /// The application's first call per command list to Map on a deferred context did not use D3D11_MAP_WRITE_DISCARD.
        /// </summary>
        public static HRESULT D3D11_ERROR_DEFERRED_CONTEXT_MAP_WITHOUT_INITIAL_DISCARD = new HRESULT("0x887C0004", "D3D11_ERROR_DEFERRED_CONTEXT_MAP_WITHOUT_INITIAL_DISCARD", "The application's first call per command list to Map on a deferred context did not use D3D11_MAP_WRITE_DISCARD.");

        /// <summary>
        /// The object was not in the correct state to process the method.
        /// </summary>
        public static HRESULT D2DERR_WRONG_STATE = new HRESULT("0x88990001", "D2DERR_WRONG_STATE", "The object was not in the correct state to process the method.");

        /// <summary>
        /// The object has not yet been initialized.
        /// </summary>
        public static HRESULT D2DERR_NOT_INITIALIZED = new HRESULT("0x88990002", "D2DERR_NOT_INITIALIZED", "The object has not yet been initialized.");

        /// <summary>
        /// The requested operation is not supported.
        /// </summary>
        public static HRESULT D2DERR_UNSUPPORTED_OPERATION = new HRESULT("0x88990003", "D2DERR_UNSUPPORTED_OPERATION", "The requested operation is not supported.");

        /// <summary>
        /// The geometry scanner failed to process the data.
        /// </summary>
        public static HRESULT D2DERR_SCANNER_FAILED = new HRESULT("0x88990004", "D2DERR_SCANNER_FAILED", "The geometry scanner failed to process the data.");

        /// <summary>
        /// Direct2D could not access the screen.
        /// </summary>
        public static HRESULT D2DERR_SCREEN_ACCESS_DENIED = new HRESULT("0x88990005", "D2DERR_SCREEN_ACCESS_DENIED", "Direct2D could not access the screen.");

        /// <summary>
        /// A valid display state could not be determined.
        /// </summary>
        public static HRESULT D2DERR_DISPLAY_STATE_INVALID = new HRESULT("0x88990006", "D2DERR_DISPLAY_STATE_INVALID", "A valid display state could not be determined.");

        /// <summary>
        /// The supplied vector is zero.
        /// </summary>
        public static HRESULT D2DERR_ZERO_VECTOR = new HRESULT("0x88990007", "D2DERR_ZERO_VECTOR", "The supplied vector is zero.");

        /// <summary>
        /// An internal error (Direct2D bug) occurred. On checked builds, we would assert. The application should close this instance of Direct2D and should consider restarting its process.
        /// </summary>
        public static HRESULT D2DERR_INTERNAL_ERROR = new HRESULT("0x88990008", "D2DERR_INTERNAL_ERROR", "An internal error (Direct2D bug) occurred. On checked builds, we would assert. The application should close this instance of Direct2D and should consider restarting its process.");

        /// <summary>
        /// The display format Direct2D needs to render is not supported by the hardware device.
        /// </summary>
        public static HRESULT D2DERR_DISPLAY_FORMAT_NOT_SUPPORTED = new HRESULT("0x88990009", "D2DERR_DISPLAY_FORMAT_NOT_SUPPORTED", "The display format Direct2D needs to render is not supported by the hardware device.");

        /// <summary>
        /// A call to this method is invalid.
        /// </summary>
        public static HRESULT D2DERR_INVALID_CALL = new HRESULT("0x8899000A", "D2DERR_INVALID_CALL", "A call to this method is invalid.");

        /// <summary>
        /// No hardware rendering device is available for this operation.
        /// </summary>
        public static HRESULT D2DERR_NO_HARDWARE_DEVICE = new HRESULT("0x8899000B", "D2DERR_NO_HARDWARE_DEVICE", "No hardware rendering device is available for this operation.");

        /// <summary>
        /// There has been a presentation error that may be recoverable. The caller needs to recreate, rerender the entire frame, and reattempt present.
        /// </summary>
        public static HRESULT D2DERR_RECREATE_TARGET = new HRESULT("0x8899000C", "D2DERR_RECREATE_TARGET", "There has been a presentation error that may be recoverable. The caller needs to recreate, rerender the entire frame, and reattempt present.");

        /// <summary>
        /// Shader construction failed because it was too complex.
        /// </summary>
        public static HRESULT D2DERR_TOO_MANY_SHADER_ELEMENTS = new HRESULT("0x8899000D", "D2DERR_TOO_MANY_SHADER_ELEMENTS", "Shader construction failed because it was too complex.");

        /// <summary>
        /// Shader compilation failed.
        /// </summary>
        public static HRESULT D2DERR_SHADER_COMPILE_FAILED = new HRESULT("0x8899000E", "D2DERR_SHADER_COMPILE_FAILED", "Shader compilation failed.");

        /// <summary>
        /// Requested DirectX surface size exceeded maximum texture size.
        /// </summary>
        public static HRESULT D2DERR_MAX_TEXTURE_SIZE_EXCEEDED = new HRESULT("0x8899000F", "D2DERR_MAX_TEXTURE_SIZE_EXCEEDED", "Requested DirectX surface size exceeded maximum texture size.");

        /// <summary>
        /// The requested Direct2D version is not supported.
        /// </summary>
        public static HRESULT D2DERR_UNSUPPORTED_VERSION = new HRESULT("0x88990010", "D2DERR_UNSUPPORTED_VERSION", "The requested Direct2D version is not supported.");

        /// <summary>
        /// Invalid number.
        /// </summary>
        public static HRESULT D2DERR_BAD_NUMBER = new HRESULT("0x88990011", "D2DERR_BAD_NUMBER", "Invalid number.");

        /// <summary>
        /// Objects used together must be created from the same factory instance.
        /// </summary>
        public static HRESULT D2DERR_WRONG_FACTORY = new HRESULT("0x88990012", "D2DERR_WRONG_FACTORY", "Objects used together must be created from the same factory instance.");

        /// <summary>
        /// A layer resource can only be in use once at any point in time.
        /// </summary>
        public static HRESULT D2DERR_LAYER_ALREADY_IN_USE = new HRESULT("0x88990013", "D2DERR_LAYER_ALREADY_IN_USE", "A layer resource can only be in use once at any point in time.");

        /// <summary>
        /// The pop call did not match the corresponding push call.
        /// </summary>
        public static HRESULT D2DERR_POP_CALL_DID_NOT_MATCH_PUSH = new HRESULT("0x88990014", "D2DERR_POP_CALL_DID_NOT_MATCH_PUSH", "The pop call did not match the corresponding push call.");

        /// <summary>
        /// The resource was realized on the wrong render target.
        /// </summary>
        public static HRESULT D2DERR_WRONG_RESOURCE_DOMAIN = new HRESULT("0x88990015", "D2DERR_WRONG_RESOURCE_DOMAIN", "The resource was realized on the wrong render target.");

        /// <summary>
        /// The push and pop calls were unbalanced.
        /// </summary>
        public static HRESULT D2DERR_PUSH_POP_UNBALANCED = new HRESULT("0x88990016", "D2DERR_PUSH_POP_UNBALANCED", "The push and pop calls were unbalanced.");

        /// <summary>
        /// Attempt to copy from a render target while a layer or clip rect is applied.
        /// </summary>
        public static HRESULT D2DERR_RENDER_TARGET_HAS_LAYER_OR_CLIPRECT = new HRESULT("0x88990017", "D2DERR_RENDER_TARGET_HAS_LAYER_OR_CLIPRECT", "Attempt to copy from a render target while a layer or clip rect is applied.");

        /// <summary>
        /// The brush types are incompatible for the call.
        /// </summary>
        public static HRESULT D2DERR_INCOMPATIBLE_BRUSH_TYPES = new HRESULT("0x88990018", "D2DERR_INCOMPATIBLE_BRUSH_TYPES", "The brush types are incompatible for the call.");

        /// <summary>
        /// An unknown win32 failure occurred.
        /// </summary>
        public static HRESULT D2DERR_WIN32_ERROR = new HRESULT("0x88990019", "D2DERR_WIN32_ERROR", "An unknown win32 failure occurred.");

        /// <summary>
        /// The render target is not compatible with GDI.
        /// </summary>
        public static HRESULT D2DERR_TARGET_NOT_GDI_COMPATIBLE = new HRESULT("0x8899001A", "D2DERR_TARGET_NOT_GDI_COMPATIBLE", "The render target is not compatible with GDI.");

        /// <summary>
        /// A text client drawing effect object is of the wrong type.
        /// </summary>
        public static HRESULT D2DERR_TEXT_EFFECT_IS_WRONG_TYPE = new HRESULT("0x8899001B", "D2DERR_TEXT_EFFECT_IS_WRONG_TYPE", "A text client drawing effect object is of the wrong type.");

        /// <summary>
        /// The application is holding a reference to the IDWriteTextRenderer interface after the corresponding DrawText or DrawTextLayout call has returned. The IDWriteTextRenderer instance will be invalid.
        /// </summary>
        public static HRESULT D2DERR_TEXT_RENDERER_NOT_RELEASED = new HRESULT("0x8899001C", "D2DERR_TEXT_RENDERER_NOT_RELEASED", "The application is holding a reference to the IDWriteTextRenderer interface after the corresponding DrawText or DrawTextLayout call has returned. The IDWriteTextRenderer instance will be invalid.");

        /// <summary>
        /// The requested size is larger than the guaranteed supported texture size at the Direct3D device's current feature level.
        /// </summary>
        public static HRESULT D2DERR_EXCEEDS_MAX_BITMAP_SIZE = new HRESULT("0x8899001D", "D2DERR_EXCEEDS_MAX_BITMAP_SIZE", "The requested size is larger than the guaranteed supported texture size at the Direct3D device's current feature level.");

        /// <summary>
        /// There was a configuration error in the graph.
        /// </summary>
        public static HRESULT D2DERR_INVALID_GRAPH_CONFIGURATION = new HRESULT("0x8899001E", "D2DERR_INVALID_GRAPH_CONFIGURATION", "There was a configuration error in the graph.");

        /// <summary>
        /// There was a internal configuration error in the graph.
        /// </summary>
        public static HRESULT D2DERR_INVALID_INTERNAL_GRAPH_CONFIGURATION = new HRESULT("0x8899001F", "D2DERR_INVALID_INTERNAL_GRAPH_CONFIGURATION", "There was a internal configuration error in the graph.");

        /// <summary>
        /// There was a cycle in the graph.
        /// </summary>
        public static HRESULT D2DERR_CYCLIC_GRAPH = new HRESULT("0x88990020", "D2DERR_CYCLIC_GRAPH", "There was a cycle in the graph.");

        /// <summary>
        /// Cannot draw with a bitmap that has the D2D1_BITMAP_OPTIONS_CANNOT_DRAW option.
        /// </summary>
        public static HRESULT D2DERR_BITMAP_CANNOT_DRAW = new HRESULT("0x88990021", "D2DERR_BITMAP_CANNOT_DRAW", "Cannot draw with a bitmap that has the D2D1_BITMAP_OPTIONS_CANNOT_DRAW option.");

        /// <summary>
        /// The operation cannot complete while there are outstanding references to the target bitmap.
        /// </summary>
        public static HRESULT D2DERR_OUTSTANDING_BITMAP_REFERENCES = new HRESULT("0x88990022", "D2DERR_OUTSTANDING_BITMAP_REFERENCES", "The operation cannot complete while there are outstanding references to the target bitmap.");

        /// <summary>
        /// The operation failed because the original target is not currently bound as a target.
        /// </summary>
        public static HRESULT D2DERR_ORIGINAL_TARGET_NOT_BOUND = new HRESULT("0x88990023", "D2DERR_ORIGINAL_TARGET_NOT_BOUND", "The operation failed because the original target is not currently bound as a target.");

        /// <summary>
        /// Cannot set the image as a target because it is either an effect or is a bitmap that does not have the D2D1_BITMAP_OPTIONS_TARGET flag set.
        /// </summary>
        public static HRESULT D2DERR_INVALID_TARGET = new HRESULT("0x88990024", "D2DERR_INVALID_TARGET", "Cannot set the image as a target because it is either an effect or is a bitmap that does not have the D2D1_BITMAP_OPTIONS_TARGET flag set.");

        /// <summary>
        /// Cannot draw with a bitmap that is currently bound as the target bitmap.
        /// </summary>
        public static HRESULT D2DERR_BITMAP_BOUND_AS_TARGET = new HRESULT("0x88990025", "D2DERR_BITMAP_BOUND_AS_TARGET", "Cannot draw with a bitmap that is currently bound as the target bitmap.");

        /// <summary>
        /// D3D Device does not have sufficient capabilities to perform the requested action.
        /// </summary>
        public static HRESULT D2DERR_INSUFFICIENT_DEVICE_CAPABILITIES = new HRESULT("0x88990026", "D2DERR_INSUFFICIENT_DEVICE_CAPABILITIES", "D3D Device does not have sufficient capabilities to perform the requested action.");

        /// <summary>
        /// The graph could not be rendered with the context's current tiling settings.
        /// </summary>
        public static HRESULT D2DERR_INTERMEDIATE_TOO_LARGE = new HRESULT("0x88990027", "D2DERR_INTERMEDIATE_TOO_LARGE", "The graph could not be rendered with the context's current tiling settings.");

        /// <summary>
        /// The CLSID provided to Unregister did not correspond to a registered effect.
        /// </summary>
        public static HRESULT D2DERR_EFFECT_IS_NOT_REGISTERED = new HRESULT("0x88990028", "D2DERR_EFFECT_IS_NOT_REGISTERED", "The CLSID provided to Unregister did not correspond to a registered effect.");

        /// <summary>
        /// The specified property does not exist.
        /// </summary>
        public static HRESULT D2DERR_INVALID_PROPERTY = new HRESULT("0x88990029", "D2DERR_INVALID_PROPERTY", "The specified property does not exist.");

        /// <summary>
        /// The specified sub-property does not exist.
        /// </summary>
        public static HRESULT D2DERR_NO_SUBPROPERTIES = new HRESULT("0x8899002A", "D2DERR_NO_SUBPROPERTIES", "The specified sub-property does not exist.");

        /// <summary>
        /// AddPage or Close called after print job is already closed.
        /// </summary>
        public static HRESULT D2DERR_PRINT_JOB_CLOSED = new HRESULT("0x8899002B", "D2DERR_PRINT_JOB_CLOSED", "AddPage or Close called after print job is already closed.");

        /// <summary>
        /// Error during print control creation. Indicates that none of the package target types (representing printer formats) are supported by Direct2D print control.
        /// </summary>
        public static HRESULT D2DERR_PRINT_FORMAT_NOT_SUPPORTED = new HRESULT("0x8899002C", "D2DERR_PRINT_FORMAT_NOT_SUPPORTED", "Error during print control creation. Indicates that none of the package target types (representing printer formats) are supported by Direct2D print control.");

        /// <summary>
        /// An effect attempted to use a transform with too many inputs.
        /// </summary>
        public static HRESULT D2DERR_TOO_MANY_TRANSFORM_INPUTS = new HRESULT("0x8899002D", "D2DERR_TOO_MANY_TRANSFORM_INPUTS", "An effect attempted to use a transform with too many inputs.");

        /// <summary>
        /// Indicates an error in an input file such as a font file.
        /// </summary>
        public static HRESULT DWRITE_E_FILEFORMAT = new HRESULT("0x88985000", "DWRITE_E_FILEFORMAT", "Indicates an error in an input file such as a font file.");

        /// <summary>
        /// Indicates an error originating in DirectWrite code, which is not expected to occur but is safe to recover from.
        /// </summary>
        public static HRESULT DWRITE_E_UNEXPECTED = new HRESULT("0x88985001", "DWRITE_E_UNEXPECTED", "Indicates an error originating in DirectWrite code, which is not expected to occur but is safe to recover from.");

        /// <summary>
        /// Indicates the specified font does not exist.
        /// </summary>
        public static HRESULT DWRITE_E_NOFONT = new HRESULT("0x88985002", "DWRITE_E_NOFONT", "Indicates the specified font does not exist.");

        /// <summary>
        /// A font file could not be opened because the file, directory, network location, drive, or other storage location does not exist or is unavailable.
        /// </summary>
        public static HRESULT DWRITE_E_FILENOTFOUND = new HRESULT("0x88985003", "DWRITE_E_FILENOTFOUND", "A font file could not be opened because the file, directory, network location, drive, or other storage location does not exist or is unavailable.");

        /// <summary>
        /// A font file exists but could not be opened due to access denied, sharing violation, or similar error.
        /// </summary>
        public static HRESULT DWRITE_E_FILEACCESS = new HRESULT("0x88985004", "DWRITE_E_FILEACCESS", "A font file exists but could not be opened due to access denied, sharing violation, or similar error.");

        /// <summary>
        /// A font collection is obsolete due to changes in the system.
        /// </summary>
        public static HRESULT DWRITE_E_FONTCOLLECTIONOBSOLETE = new HRESULT("0x88985005", "DWRITE_E_FONTCOLLECTIONOBSOLETE", "A font collection is obsolete due to changes in the system.");

        /// <summary>
        /// The given interface is already registered.
        /// </summary>
        public static HRESULT DWRITE_E_ALREADYREGISTERED = new HRESULT("0x88985006", "DWRITE_E_ALREADYREGISTERED", "The given interface is already registered.");

        /// <summary>
        /// The font cache contains invalid data.
        /// </summary>
        public static HRESULT DWRITE_E_CACHEFORMAT = new HRESULT("0x88985007", "DWRITE_E_CACHEFORMAT", "The font cache contains invalid data.");

        /// <summary>
        /// A font cache file corresponds to a different version of DirectWrite.
        /// </summary>
        public static HRESULT DWRITE_E_CACHEVERSION = new HRESULT("0x88985008", "DWRITE_E_CACHEVERSION", "A font cache file corresponds to a different version of DirectWrite.");

        /// <summary>
        /// The operation is not supported for this type of font.
        /// </summary>
        public static HRESULT DWRITE_E_UNSUPPORTEDOPERATION = new HRESULT("0x88985009", "DWRITE_E_UNSUPPORTEDOPERATION", "The operation is not supported for this type of font.");

        /// <summary>
        /// The codec is in the wrong state.
        /// </summary>
        public static HRESULT WINCODEC_ERR_WRONGSTATE = new HRESULT("0x88982F04", "WINCODEC_ERR_WRONGSTATE", "The codec is in the wrong state.");

        /// <summary>
        /// The value is out of range.
        /// </summary>
        public static HRESULT WINCODEC_ERR_VALUEOUTOFRANGE = new HRESULT("0x88982F05", "WINCODEC_ERR_VALUEOUTOFRANGE", "The value is out of range.");

        /// <summary>
        /// The image format is unknown.
        /// </summary>
        public static HRESULT WINCODEC_ERR_UNKNOWNIMAGEFORMAT = new HRESULT("0x88982F07", "WINCODEC_ERR_UNKNOWNIMAGEFORMAT", "The image format is unknown.");

        /// <summary>
        /// The SDK version is unsupported.
        /// </summary>
        public static HRESULT WINCODEC_ERR_UNSUPPORTEDVERSION = new HRESULT("0x88982F0B", "WINCODEC_ERR_UNSUPPORTEDVERSION", "The SDK version is unsupported.");

        /// <summary>
        /// The component is not initialized.
        /// </summary>
        public static HRESULT WINCODEC_ERR_NOTINITIALIZED = new HRESULT("0x88982F0C", "WINCODEC_ERR_NOTINITIALIZED", "The component is not initialized.");

        /// <summary>
        /// There is already an outstanding read or write lock.
        /// </summary>
        public static HRESULT WINCODEC_ERR_ALREADYLOCKED = new HRESULT("0x88982F0D", "WINCODEC_ERR_ALREADYLOCKED", "There is already an outstanding read or write lock.");

        /// <summary>
        /// The specified bitmap property cannot be found.
        /// </summary>
        public static HRESULT WINCODEC_ERR_PROPERTYNOTFOUND = new HRESULT("0x88982F40", "WINCODEC_ERR_PROPERTYNOTFOUND", "The specified bitmap property cannot be found.");

        /// <summary>
        /// The bitmap codec does not support the bitmap property.
        /// </summary>
        public static HRESULT WINCODEC_ERR_PROPERTYNOTSUPPORTED = new HRESULT("0x88982F41", "WINCODEC_ERR_PROPERTYNOTSUPPORTED", "The bitmap codec does not support the bitmap property.");

        /// <summary>
        /// The bitmap property size is invalid.
        /// </summary>
        public static HRESULT WINCODEC_ERR_PROPERTYSIZE = new HRESULT("0x88982F42", "WINCODEC_ERR_PROPERTYSIZE", "The bitmap property size is invalid.");

        /// <summary>
        /// An unknown error has occurred.
        /// </summary>
        public static HRESULT WINCODEC_ERR_CODECPRESENT = new HRESULT("0x88982F43", "WINCODEC_ERR_CODECPRESENT", "An unknown error has occurred.");

        /// <summary>
        /// The bitmap codec does not support a thumbnail.
        /// </summary>
        public static HRESULT WINCODEC_ERR_CODECNOTHUMBNAIL = new HRESULT("0x88982F44", "WINCODEC_ERR_CODECNOTHUMBNAIL", "The bitmap codec does not support a thumbnail.");

        /// <summary>
        /// The bitmap palette is unavailable.
        /// </summary>
        public static HRESULT WINCODEC_ERR_PALETTEUNAVAILABLE = new HRESULT("0x88982F45", "WINCODEC_ERR_PALETTEUNAVAILABLE", "The bitmap palette is unavailable.");

        /// <summary>
        /// Too many scanlines were requested.
        /// </summary>
        public static HRESULT WINCODEC_ERR_CODECTOOMANYSCANLINES = new HRESULT("0x88982F46", "WINCODEC_ERR_CODECTOOMANYSCANLINES", "Too many scanlines were requested.");

        /// <summary>
        /// An internal error occurred.
        /// </summary>
        public static HRESULT WINCODEC_ERR_INTERNALERROR = new HRESULT("0x88982F48", "WINCODEC_ERR_INTERNALERROR", "An internal error occurred.");

        /// <summary>
        /// The bitmap bounds do not match the bitmap dimensions.
        /// </summary>
        public static HRESULT WINCODEC_ERR_SOURCERECTDOESNOTMATCHDIMENSIONS = new HRESULT("0x88982F49", "WINCODEC_ERR_SOURCERECTDOESNOTMATCHDIMENSIONS", "The bitmap bounds do not match the bitmap dimensions.");

        /// <summary>
        /// The component cannot be found.
        /// </summary>
        public static HRESULT WINCODEC_ERR_COMPONENTNOTFOUND = new HRESULT("0x88982F50", "WINCODEC_ERR_COMPONENTNOTFOUND", "The component cannot be found.");

        /// <summary>
        /// The bitmap size is outside the valid range.
        /// </summary>
        public static HRESULT WINCODEC_ERR_IMAGESIZEOUTOFRANGE = new HRESULT("0x88982F51", "WINCODEC_ERR_IMAGESIZEOUTOFRANGE", "The bitmap size is outside the valid range.");

        /// <summary>
        /// There is too much metadata to be written to the bitmap.
        /// </summary>
        public static HRESULT WINCODEC_ERR_TOOMUCHMETADATA = new HRESULT("0x88982F52", "WINCODEC_ERR_TOOMUCHMETADATA", "There is too much metadata to be written to the bitmap.");

        /// <summary>
        /// The image is unrecognized.
        /// </summary>
        public static HRESULT WINCODEC_ERR_BADIMAGE = new HRESULT("0x88982F60", "WINCODEC_ERR_BADIMAGE", "The image is unrecognized.");

        /// <summary>
        /// The image header is unrecognized.
        /// </summary>
        public static HRESULT WINCODEC_ERR_BADHEADER = new HRESULT("0x88982F61", "WINCODEC_ERR_BADHEADER", "The image header is unrecognized.");

        /// <summary>
        /// The bitmap frame is missing.
        /// </summary>
        public static HRESULT WINCODEC_ERR_FRAMEMISSING = new HRESULT("0x88982F62", "WINCODEC_ERR_FRAMEMISSING", "The bitmap frame is missing.");

        /// <summary>
        /// The image metadata header is unrecognized.
        /// </summary>
        public static HRESULT WINCODEC_ERR_BADMETADATAHEADER = new HRESULT("0x88982F63", "WINCODEC_ERR_BADMETADATAHEADER", "The image metadata header is unrecognized.");

        /// <summary>
        /// The stream data is unrecognized.
        /// </summary>
        public static HRESULT WINCODEC_ERR_BADSTREAMDATA = new HRESULT("0x88982F70", "WINCODEC_ERR_BADSTREAMDATA", "The stream data is unrecognized.");

        /// <summary>
        /// Failed to write to the stream.
        /// </summary>
        public static HRESULT WINCODEC_ERR_STREAMWRITE = new HRESULT("0x88982F71", "WINCODEC_ERR_STREAMWRITE", "Failed to write to the stream.");

        /// <summary>
        /// Failed to read from the stream.
        /// </summary>
        public static HRESULT WINCODEC_ERR_STREAMREAD = new HRESULT("0x88982F72", "WINCODEC_ERR_STREAMREAD", "Failed to read from the stream.");

        /// <summary>
        /// The stream is not available.
        /// </summary>
        public static HRESULT WINCODEC_ERR_STREAMNOTAVAILABLE = new HRESULT("0x88982F73", "WINCODEC_ERR_STREAMNOTAVAILABLE", "The stream is not available.");

        /// <summary>
        /// The bitmap pixel format is unsupported.
        /// </summary>
        public static HRESULT WINCODEC_ERR_UNSUPPORTEDPIXELFORMAT = new HRESULT("0x88982F80", "WINCODEC_ERR_UNSUPPORTEDPIXELFORMAT", "The bitmap pixel format is unsupported.");

        /// <summary>
        /// The operation is unsupported.
        /// </summary>
        public static HRESULT WINCODEC_ERR_UNSUPPORTEDOPERATION = new HRESULT("0x88982F81", "WINCODEC_ERR_UNSUPPORTEDOPERATION", "The operation is unsupported.");

        /// <summary>
        /// The component registration is invalid.
        /// </summary>
        public static HRESULT WINCODEC_ERR_INVALIDREGISTRATION = new HRESULT("0x88982F8A", "WINCODEC_ERR_INVALIDREGISTRATION", "The component registration is invalid.");

        /// <summary>
        /// The component initialization has failed.
        /// </summary>
        public static HRESULT WINCODEC_ERR_COMPONENTINITIALIZEFAILURE = new HRESULT("0x88982F8B", "WINCODEC_ERR_COMPONENTINITIALIZEFAILURE", "The component initialization has failed.");

        /// <summary>
        /// The buffer allocated is insufficient.
        /// </summary>
        public static HRESULT WINCODEC_ERR_INSUFFICIENTBUFFER = new HRESULT("0x88982F8C", "WINCODEC_ERR_INSUFFICIENTBUFFER", "The buffer allocated is insufficient.");

        /// <summary>
        /// Duplicate metadata is present.
        /// </summary>
        public static HRESULT WINCODEC_ERR_DUPLICATEMETADATAPRESENT = new HRESULT("0x88982F8D", "WINCODEC_ERR_DUPLICATEMETADATAPRESENT", "Duplicate metadata is present.");

        /// <summary>
        /// The bitmap property type is unexpected.
        /// </summary>
        public static HRESULT WINCODEC_ERR_PROPERTYUNEXPECTEDTYPE = new HRESULT("0x88982F8E", "WINCODEC_ERR_PROPERTYUNEXPECTEDTYPE", "The bitmap property type is unexpected.");

        /// <summary>
        /// The size is unexpected.
        /// </summary>
        public static HRESULT WINCODEC_ERR_UNEXPECTEDSIZE = new HRESULT("0x88982F8F", "WINCODEC_ERR_UNEXPECTEDSIZE", "The size is unexpected.");

        /// <summary>
        /// The property query is invalid.
        /// </summary>
        public static HRESULT WINCODEC_ERR_INVALIDQUERYREQUEST = new HRESULT("0x88982F90", "WINCODEC_ERR_INVALIDQUERYREQUEST", "The property query is invalid.");

        /// <summary>
        /// The metadata type is unexpected.
        /// </summary>
        public static HRESULT WINCODEC_ERR_UNEXPECTEDMETADATATYPE = new HRESULT("0x88982F91", "WINCODEC_ERR_UNEXPECTEDMETADATATYPE", "The metadata type is unexpected.");

        /// <summary>
        /// The specified bitmap property is only valid at root level.
        /// </summary>
        public static HRESULT WINCODEC_ERR_REQUESTONLYVALIDATMETADATAROOT = new HRESULT("0x88982F92", "WINCODEC_ERR_REQUESTONLYVALIDATMETADATAROOT", "The specified bitmap property is only valid at root level.");

        /// <summary>
        /// The query string contains an invalid character.
        /// </summary>
        public static HRESULT WINCODEC_ERR_INVALIDQUERYCHARACTER = new HRESULT("0x88982F93", "WINCODEC_ERR_INVALIDQUERYCHARACTER", "The query string contains an invalid character.");

        /// <summary>
        /// Windows Codecs received an error from the Win32 system.
        /// </summary>
        public static HRESULT WINCODEC_ERR_WIN32ERROR = new HRESULT("0x88982F94", "WINCODEC_ERR_WIN32ERROR", "Windows Codecs received an error from the Win32 system.");
        #endregion

        // ************************************************************************** \\
        #endregion

        private HRESULT(int value, string name, string description) : this((long)value, name, description)
        {
        }

        private HRESULT(long value, string name, string description)
        {
            this._value = value;
            this._hex = string.Concat(0, "x", value.ToString("X8"));
            this._name = name;
            this._desc = description;
            this._isSet = true;
            if (!results.Contains(this))
                results.Add(this);
            else
                throw new InvalidOperationException(string.Format("{0} already exists", value));
        }

        private HRESULT(string hex, string name, string description)
        {
            //Console.WriteLine(hex);
            if (!string.IsNullOrEmpty(hex) && !hex.Trim().Equals(string.Empty))
            {
                this._value = Convert.ToInt64(hex, 16); // int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
                this._hex = string.Concat(0, "x", this._value.ToString("X8")); 
            }
            else
            {
                this._value = -1;
                this._hex = null;
            }
            this._name = name;
            this._desc = description;
            this._isSet = true;
            if (!results.Contains(this))
                results.Add(this);
            else
                throw new InvalidOperationException(string.Format("{0} already exists", hex));
        }

        public long Value
        {
            get
            {
                if (!this._isSet)
                    return NONE._value;
                return this._value;
            }
        }

        public string Hex
        {
            get
            {
                if (!this._isSet)
                    return NONE._hex;
                return this._hex;
            }
        }

        public string Name
        {
            get
            {
                if (!this._isSet)
                    return NONE._name;
                return this._name;
            }
        }

        public string Description
        {
            get
            {
                if (!this._isSet)
                    return NONE._desc;
                return this._desc;
            }
        }

        public bool Equals(HRESULT other)
        {
            return this.Value == other.Value && this.Hex == other.Hex;
        }

        public bool Equals(int other)
        {
            return this.Value == other;
        }

        public bool Equals(long other)
        {
            return this.Value == other;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Hex;
        }

        //public static bool operator ==(HRESULT h1, HRESULT h2)
        //{

        //}

        //     public static bool operator !=(HRESULT h1, HRESULT h2)
        //{
        //    return !(b1 == b2);
        //}

        public static implicit operator long(HRESULT h)
        {
            return h.Value;
        }

        public static implicit operator HRESULT(int i)
        {
            HRESULT result = results.FirstOrDefault(r => r.Value == (long)i);

            if (result != NONE)
                return result;

            throw new InvalidCastException();
        }

        public static implicit operator HRESULT(long i)
        {
            HRESULT result = results.FirstOrDefault(r => r.Value == i && r != NONE);

            if (result != NONE)
                return result;

            throw new InvalidCastException();
        }
    }




    internal static class Win32Enums
    {
        internal const int CS_DROPSHADOW = 131072;
        internal const int WM_NCPAINT = 133;
        internal const int WM_NCHITTEST = 132;
        internal const int WM_ACTIVATEAPP = 28;
        internal const int HTCAPTION = 2;
        internal const int HTCLIENT = 1;

        internal const uint SHGFI_SYSICONINDEX = 16384;
        internal const uint SHGFI_TYPENAME = 1024;
        internal const uint SHGFI_ICON = 256;
        internal const uint SHGFI_USEFILEATTRIBUTES = 16;
        internal const uint SHGFI_SMALLICON = 1;
        internal const uint SHGFI_LARGEICON = 0;

        internal const int MAX_PATH = 260;
        internal const int FILE_ATTRIBUTE_NORMAL = 128;
        internal const int FILE_ATTRIBUTE_DIRECTORY = 16;
        internal const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
        internal const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
        internal const int FORMAT_MESSAGE_FROM_HMODULE = 2048;
        internal const int FORMAT_MESSAGE_FROM_STRING = 1024;
        internal const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
        internal const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
        internal const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 255;

        internal const int ATTACH_PARENT_PROCESS = -1;
    }
}
