DataPacket.cs

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
struct DataPacket
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    public string Name;
 
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    public string Subject;
 
    [MarshalAs(UnmanagedType.I4)]
    public int Grade;
 
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string Memo;
 
    // Calling this method will return a byte array with the contents
    // of the struct ready to be sent via the tcp socket.
    public byte[] Serialize()
    {
        // Allocate a byte array for the struct data
        var buffer = new byte[Marshal.SizeOf(typeof(DataPacket))];
 
        // Allocate a GCHandle and get the array pointer
        var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var pBuffer = gch.AddrOfPinnedObject();
 
        // copy data from struct to array and unpin the gc pointer
        Marshal.StructureToPtr(this, pBuffer, false);
        gch.Free();
 
        return buffer;
    }
 
    // this method will deserialize a byte array into the struct.
    public void Deserialize(ref byte[] data)
    {
        var gch = GCHandle.Alloc(data, GCHandleType.Pinned);
        this = (DataPacket)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(DataPacket));
        gch.Free();
    }
}

    // 패킷 헤더 클래스
    [StructLayout(LayoutKind.Sequential)]//[StructLayout(LayoutKind.Sequential, Pack=1)]
    public class HEADER
    {
        public ushort a1;
        public ushort a2;
        public ushort a3;
        public ushort a4;
    }
   
    // 로그인 요청 클래스
    // GetBuffer을 부모 클래스에서 정의하고 여기서는 상속 받지 않은 이유는 그렇게 하면 클래스의
    // 데이타를 복사 할 때 부모클래스의 크기(4바이트) 만큼을 앞에 계산 해버린다
    [StructLayout(LayoutKind.Sequential)]
    public class LoginAuthorRet
    {
        public LoginAuthorRet()
       {
            Header = new HEADER();
            acID = new byte[21];
            acPasswd = new byte[31];
        }

        // 클래스의 있는 데이타를 메모리에 담아서 리턴 한다.
        public void GetBuffer( byte[] outBuffer )
        {
            if( 0 == outBuffer.Length )
                outBuffer = new byte[ MAX_PACKET_DATA ];

            unsafe
           {
                fixed(byte* fixed_buffer = outBuffer)
               {
                    Marshal.StructureToPtr(this, (IntPtr)fixed_buffer, false);
                }
            }
        }

        public HEADER Header;    // 헤더
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=21)] public byte[] acID;          // 아이디
       [MarshalAs(UnmanagedType.ByValArray, SizeConst=31)] public byte[] acPasswd;    // 패스워드
    }

// 사용 예
LoginAuthorRet LoginPacket = new LoginAuthorRet();
LoginPacket.Header.a2 = PK_LOGIN_AUTHOR_REQ;
LoginPacket.Header.a3 = (ushort)(Marshal.SizeOf(LoginPacket)-Marshal.SizeOf(LoginPacket.Header));
               
int iIDLen = strID.Length;
int iPWLen = strPass.Length;
Buffer.BlockCopy( Encoding.ASCII.GetBytes(strID), 0, LoginPacket.acID, 0, iIDLen );
Buffer.BlockCopy( Encoding.ASCII.GetBytes(strPass ), 0, LoginPacket.acPasswd, 0, iPWLen );

byte[] packet1 = new byte[ MAX_PACKET_DATA ];
LoginPacket.GetBuffer( packet1 );
SendPacket( packet1, Marshal.SizeOf(LoginPacket) );
