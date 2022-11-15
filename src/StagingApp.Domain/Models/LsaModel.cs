namespace StagingApp.Domain.Models;

public class LsaModel
{
    [StructLayout(LayoutKind.Sequential)]
    private struct LSA_UNICODE_STRING
    {
        public ushort Length;
        public ushort MaximumLength;
        public IntPtr Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct LSA_OBJECT_ATTRIBUTES
    {
        public int Length;
        public IntPtr RootDirectory;
        public LSA_UNICODE_STRING ObjectName;
        public uint Attributes;
        public IntPtr SecurityDescriptor;
        public IntPtr SecurityQualityOfService;
    }

    private enum LSA_AccessPolicy : long
    {
        POLICY_VIEW_LOCAL_INFORMATION = 0x00000001L,
        POLICY_VIEW_AUDIT_INFORMATION = 0x00000002L,
        POLICY_GET_PRIVATE_INFORMATION = 0x00000004L,
        POLICY_TRUST_ADMIN = 0x00000008L,
        POLICY_CREATE_ACCOUNT = 0x00000010L,
        POLICY_CREATE_SECRET = 0x00000020L,
        POLICY_CREATE_PRIVILEGE = 0x00000040L,
        POLICY_SET_DEFAULT_QUOTA_LIMITS = 0x00000080L,
        POLICY_SET_AUDIT_REQUIREMENTS = 0x00000100L,
        POLICY_AUDIT_LOG_ADMIN = 0x00000200L,
        POLICY_SERVER_ADMIN = 0x00000400L,
        POLICY_LOOKUP_NAMES = 0x00000800L,
        POLICY_NOTIFICATION = 0x00001000L
    }

    [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]

    private static extern uint LsaRetrievePrivateData(IntPtr PolicyHandle,
                                                      ref LSA_UNICODE_STRING KeyName,
                                                      out IntPtr PrivateData);

    [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]
    private static extern uint LsaStorePrivateData(IntPtr policyHandle,
                                                   ref LSA_UNICODE_STRING KeyName,
                                                   ref LSA_UNICODE_STRING PrivateData);

    [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]
    private static extern uint LsaOpenPolicy(ref LSA_UNICODE_STRING SystemName,
                                             ref LSA_OBJECT_ATTRIBUTES ObjectAttributes,
                                             uint DesiredAccess,
                                             out IntPtr PolicyHandle);

    [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]
    private static extern uint LsaNtStatusToWinError(uint status);

    [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]
    private static extern uint LsaClose(IntPtr policyHandle);

    [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]
    private static extern uint LsaFreeMemory(IntPtr buffer);

    private LSA_OBJECT_ATTRIBUTES _objectAttributes;
    private LSA_UNICODE_STRING _localsystem;
    private LSA_UNICODE_STRING _secretName;

    public LsaModel(string key)
    {
        if (key.Length == 0)
        {
            throw new Exception("Key lenght zero");
        }

        _objectAttributes = new LSA_OBJECT_ATTRIBUTES
        {
            Length = 0,
            RootDirectory = IntPtr.Zero,
            Attributes = 0,
            SecurityDescriptor = IntPtr.Zero,
            SecurityQualityOfService = IntPtr.Zero
        };

        _localsystem = new LSA_UNICODE_STRING
        {
            Buffer = IntPtr.Zero,
            Length = 0,
            MaximumLength = 0
        };

        _secretName = new LSA_UNICODE_STRING
        {
            Buffer = Marshal.StringToHGlobalUni(key),
            Length = (ushort)(key.Length * UnicodeEncoding.CharSize),
            MaximumLength = (ushort)((key.Length + 1) * UnicodeEncoding.CharSize)
        };
    }

    private IntPtr GetLsaPolicy(LSA_AccessPolicy access)
    {

        uint ntsResult = LsaOpenPolicy(ref _localsystem, ref _objectAttributes, (uint)access, out IntPtr LsaPolicyHandle);

        uint winErrorCode = LsaNtStatusToWinError(ntsResult);
        return winErrorCode != 0 ? throw new Exception("LsaOpenPolicy failed: " + winErrorCode) : LsaPolicyHandle;
    }

    private static void ReleaseLsaPolicy(IntPtr LsaPolicyHandle)
    {
        uint ntsResult = LsaClose(LsaPolicyHandle);
        uint winErrorCode = LsaNtStatusToWinError(ntsResult);
        if (winErrorCode != 0)
        {
            throw new Exception("LsaClose failed: " + winErrorCode);
        }
    }

    public void SetSecret(string value)
    {
        LSA_UNICODE_STRING lusSecretData = new();

        if (value.Length > 0)
        {
            //Create data and key
            lusSecretData.Buffer = Marshal.StringToHGlobalUni(value);
            lusSecretData.Length = (ushort)(value.Length * UnicodeEncoding.CharSize);
            lusSecretData.MaximumLength = (ushort)((value.Length + 1) * UnicodeEncoding.CharSize);
        }
        else
        {
            //Delete data and key
            lusSecretData.Buffer = IntPtr.Zero;
            lusSecretData.Length = 0;
            lusSecretData.MaximumLength = 0;
        }

        IntPtr LsaPolicyHandle = GetLsaPolicy(LSA_AccessPolicy.POLICY_CREATE_SECRET);
        uint result = LsaStorePrivateData(LsaPolicyHandle, ref _secretName, ref lusSecretData);
        ReleaseLsaPolicy(LsaPolicyHandle);

        uint winErrorCode = LsaNtStatusToWinError(result);
        if (winErrorCode != 0)
        {
            throw new Exception("StorePrivateData failed: " + winErrorCode);
        }

    }
}
