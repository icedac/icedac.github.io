﻿/****************************************************************************
 * 	
 *  generated by PacketCompiler.
 *  do not modify this.
 * 
 *  time: 2018-07-12 09:58:00.475
 *  
 */
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cl_ms {
    /// start of RawCode
    
    public enum ERoomType
    {
        RT_ROOM_DEFAULT = 0,
        RT_ROOM_PVE,
        RT_ROOM_PVP,
        RT_ROOM_END,
        RT_ROOM_TEST = 0xffff,
    }
    
    /// endof RawCode

    /// class Util
    /// <summary>
    /// 
    /// </summary>
    public class Util
    {
        public const UInt64 VERSION = 0x0000000000000020; // 32
        public const UInt64 BUILD_VERSION = 0x01d4197b614d6180; // 131758306804720000
		
        public const UInt64 SALT_UINT64 = 0x27a73e105959299f;		
        public static byte[] COMPILE_TIME_PASS_BYTES = new byte[] { 0x5a,0x09,0xcd,0x17,0x01,0x65,0x29,0x1a,0x16,0x51,0x10,0x63,0x15,0x1b,0x9c,0x32,0xf7,0x50,0x04,0x1c,0xb0,0x09,0xa0,0x50,0xa7,0x16,0xfc,0x53,0xf0,0x0d,0xaf,0x69,0x4c,0x5b,0xd8,0x52,0x3e,0x36,0x5e,0x60,0xb7,0x0d,0x71,0x48,0x23,0x3e,0xdf,0x60,0xde,0x4c,0x37,0x2d,0x5d,0x7c,0xcb,0x66,0x8d,0x66,0x18,0x16,0x33,0x77,0x1e,0x50 }; // do not modify

        public delegate void runLater();

		public static Encoding MBCS = Encoding.GetEncoding("euc-kr");
        
        public static System.String ReadCString(System.IO.BinaryReader reader)
        {
            List<byte> byteList = new List<byte>();
            byte b;
            while ((b = reader.ReadByte()) != 0)
            {
                byteList.Add(b);
            }
            return MBCS.GetString(byteList.ToArray());
        }
        public static System.String ReadCStringAsASCII(System.IO.BinaryReader reader)
        {
            List<byte> byteList = new List<byte>();
            byte b;
            while ((b = reader.ReadByte()) != 0)
            {
                byteList.Add(b);
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        public static System.String ReadUnicodeCString(System.IO.BinaryReader reader)
        {
            List<byte> byteList = new List<byte>();
            byte b1, b2;
            while (true)
            {
                if (-1 == reader.PeekChar()) return "[ERR] STREAM_ERROR";
                b1 = reader.ReadByte();
                if (-1 == reader.PeekChar()) return "[ERR] STREAM_ERROR";
                b2 = reader.ReadByte();

                if (b1 == 0 && b2 == 0) break;

                byteList.Add(b1);
                byteList.Add(b2);
            }
            return Encoding.Unicode.GetString(byteList.ToArray());
        }

        // Fixed-Size C-String
        public static System.String ReadFixedCString(int fixed_size, System.IO.BinaryReader reader)
        {
            List<byte> byteList = new List<byte>();
            byte b;
            for (int i = 0; i < fixed_size; ++i)
            {
                if (-1 == reader.PeekChar()) return "[ERR] STREAM_ERROR";
                byteList.Add(reader.ReadByte());
            }
            return MBCS.GetString(byteList.ToArray());
        }
        public static System.String ReadFixedCStringAsASCII(int fixed_size, System.IO.BinaryReader reader)
        {
            List<byte> byteList = new List<byte>();
            byte b;
            for (int i = 0; i < fixed_size; ++i)
            {
                if (-1 == reader.PeekChar()) return "[ERR] STREAM_ERROR";
                byteList.Add(reader.ReadByte());
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        public static System.String ReadFixedUnicodeCString(int fixed_size, System.IO.BinaryReader reader)
        {
            List<byte> byteList = new List<byte>();
            for (int i = 0; i < fixed_size; ++i)
            {
                if (-1 == reader.PeekChar()) return "[ERR] STREAM_ERROR";
                byteList.Add(reader.ReadByte());
                if (-1 == reader.PeekChar()) return "[ERR] STREAM_ERROR";
                byteList.Add(reader.ReadByte());
            }
            return Encoding.Unicode.GetString(byteList.ToArray());
        }

    }

    /// class Header
    /// <summary>
    /// 
    /// </summary>
    public class Header
    {
        public System.UInt16 sizeWhole = 0;
        public System.UInt16 typeBody = 0;

        public static byte[] GetBytes( System.UInt16 typeBody, byte[] body = null )
        {
            System.UInt16 sizeWhole = 4;
            if (body != null)
                sizeWhole += (ushort)body.Length;

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
            writer.Write(sizeWhole);
            writer.Write(typeBody);
            writer.Write(body);
            return stream.ToArray();
        }

        public bool Read( byte[] data, int size )
        {
            if (size < 4) return false;
            System.IO.MemoryStream stream = new System.IO.MemoryStream(data);
            System.IO.BinaryReader reader = new System.IO.BinaryReader(stream);
            sizeWhole = reader.ReadUInt16();
            typeBody = reader.ReadUInt16();
            if (size < sizeWhole) return false;
            // okay
            return true;
        }
    }

	public interface ISend {
		System.UInt16 GetPacketId();
		byte[] GetBytes();
	}

	public enum Error {
		SUCCESS = 0, // success
		ERR_GENERAL = 1, // error
		ERR_NOT_IMPLEMENTED = 2, // not implemented
		ERR_SESSION_SERVER = 3, // session error
		ERR_HAS_PENDING_REQUEST = 4, // client should wait until last response receive
		ERR_ALREADY_LOGGED_IN = 100, // already logged in
		ERR_WRONG_VERSION = 101, // wrong version
		ERR_FORCE_DISCONNECT_INACTIVE_ACCOUNT = 102, // disconnect inactive account, try to logged in again
		ERR_SESSION_NOT_FOUND = 103, // ERR_SESSION_NOT_FOUND
		ERR_WRONG_PARAMETER_ACCT_NAME = 200, // wrong parameter
		ERR_WRONG_PARAMETER_1 = 201, // wrong parameter: 1st
		ERR_WRONG_PARAMETER_2 = 202, // wrong parameter: 2nd
		ERR_WRONG_PARAMETER_3 = 203, // wrong parameter: 3rd
		ERR_WRONG_PARAMETER_4 = 204, // wrong parameter: 4th
		ERR_WRONG_PARAMETER_5 = 205, // wrong parameter: 5th
		ERR_WRONG_PARAMETER_COST = 206, // 
		ERR_WRONG_PARAMETER = 299, // 
		ERR_RESOURCE_NOT_FOUND = 300, // resource not found
		ERR_RESOURCE_NOT_FOUND_SOURCE = 301, // resource not found(source)
		ERR_RESOURCE_NOT_FOUND_TARGET = 302, // resource not found(target)
		ERR_ACCESS_DENIED = 400, // acess denied
		ERR_ACCESS_DENIED_PROTECTED = 401, // acess denied: protected
		ERR_ACCESS_DENIED_SOURCE = 402, // 
		ERR_ACCESS_DENIED_TARGET = 403, // 
		ERR_DB_SERVER = 500, // DB ERROR
		ERR_DB_SERVER_SQLEXCEPTION = 501, // db error
		ERR_DB_SERVER_SQLWARNING = 502, // db error
		ERR_INSURFFICIENT_RESOURCE = 1100, // 
		ERR_INSURFFICIENT_GAME_MONEY = 1101, // 
		ERR_INSURFFICIENT_GAME_CASH = 1102, // 
		ERR_INSURFFICIENT_HONOR_POINT = 1103, // 
		ERR_INSURFFICIENT_AP = 1104, // 
		ERR_INSURFFICIENT_ITEM_MATERIAL = 1105, // 
		ERR_INSURFFICIENT_INVENTORY = 1106, // 
		ERR_OPERATION_NOT_READY = 1200, // need to wait cool time
		ERR_ACCOUNT_EVENT_RAISED = 1300, // AccountEvent should be resolved by ResolveAccountEventReq
		ERR_ACCOUNT_EVENT_NOT_FOUND = 1301, // 
	}
	
   // send packet list
   public enum Send {
       CM_VERSION_CHECK_REQ = 1000,
       CM_LOGIN_REQ = 1001,
       CM_KEEP_ALIVE_REQ = 1002,
       CM_LIST_CHARACTER_REQ = 1003,
       CM_ROOM_CREATE_OR_JOIN_REQ = 1004,
       CM_ROOM_JOIN_REQ = 1005,
       CM_ROOM_LEAVE_REQ = 1006,
       CM_ROOM_CHAT_REQ = 1007,
       CM_ROOM_READY_REQ = 1008,
       CM_ROOM_KICK_REQ = 1009,
       CM_ROOM_ADD_NPCREQ = 1010,
   }

   // recv packet list
   public enum Recv {
       MC_VERSION_CHECK_RES = 1500,
       MC_LOGIN_RES = 1501,
       MC_KEEP_ALIVE_RES = 1502,
       MC_LIST_CHARACTER_RES = 1503,
       MC_ROOM_CREATE_OR_JOIN_RES = 1504,
       MC_ROOM_JOIN_RES = 1505,
       MC_ROOM_LEAVE_RES = 1506,
       MC_ROOM_ENTER_NTF = 1507,
       MC_ROOM_LEAVE_NTF = 1508,
       MC_ROOM_CHAT_NTF = 1509,
       MC_ROOM_READY_NTF = 1510,
       MC_ROOM_TEAM_CHANGED_NTF = 1511,
       MC_ROOM_OP_CHANGED_NTF = 1512,
       MC_ROOM_START_NTF = 1513,
       MC_ROOM_RESULT_NTF = 1514,
       MC_ROOM_CHAT_RES = 1515,
       MC_ROOM_READY_RES = 1516,
       MC_ROOM_KICK_RES = 1517,
       MC_ROOM_ADD_NPCRES = 1518,
   }

   /****************************************************************************
    *
    * Packet Classes for Send/Recv
    */
    // '버전 체크'
    public partial class Send_CM_VersionCheckReq : ISend
    {
        public static System.UInt16 Type = (System.UInt16)Send.CM_VERSION_CHECK_REQ;
        public static System.String GetPacketName() { return "CM_VERSION_CHECK_REQ"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        public virtual System.UInt16 GetPacketId() { return _Type; }
        // public byte[] GetBytes()
        public UInt64 version; // '버전; 다르면 요청 실패'
        public UInt64 build_version; // '빌드버전; 달라도 성공. 진행 여부는 클라이언트에서 판단'
        public UInt64 client_key; // 'reserved'
    }

    public partial class Send_CM_LoginReq : ISend
    {
        public static System.UInt16 Type = (System.UInt16)Send.CM_LOGIN_REQ;
        public static System.String GetPacketName() { return "CM_LOGIN_REQ"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        public virtual System.UInt16 GetPacketId() { return _Type; }
        // public byte[] GetBytes()
        public UInt64 version; // '버전; 다르면 요청 실패'
        public UInt64 build_version; // '빌드버전; 달라도 성공. 진행 여부는 클라이언트에서 판단'
        public const UInt32 acct_name__constrain_max_size = 50; // will assert if the size exceeds 50
        public String acct_name = null; 
        public const UInt32 acct_passwd__constrain_max_size = 16; // will assert if the size exceeds 16
        public String acct_passwd = null; 
    }

    // '버전 체크'
    public partial class Send_CM_KeepAliveReq : ISend
    {
        public static System.UInt16 Type = (System.UInt16)Send.CM_KEEP_ALIVE_REQ;
        public static System.String GetPacketName() { return "CM_KEEP_ALIVE_REQ"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        public virtual System.UInt16 GetPacketId() { return _Type; }
        // public byte[] GetBytes()
        public System.DateTime client_time; 
    }

    public partial class Send_CM_ListCharacterReq : ISend
    {
        public static System.UInt16 Type = (System.UInt16)Send.CM_LIST_CHARACTER_REQ;
        public static System.String GetPacketName() { return "CM_LIST_CHARACTER_REQ"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        public virtual System.UInt16 GetPacketId() { return _Type; }
        // public byte[] GetBytes()
    }








    public partial class Recv_MC_VersionCheckRes
    {
        public static System.UInt16 Type = (System.UInt16)Recv.MC_VERSION_CHECK_RES;
        public static System.String GetPacketName() { return "MC_VERSION_CHECK_RES"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        // public void Read(byte[] data, int size);
        public UInt32 result; // 'see @Error'
        public UInt64 required_version; // '버전; 다르면 요청 실패'
        public UInt64 required_build_version; // '빌드버전; 달라도 성공. 진행 여부는 클라이언트에서 판단'
        public const UInt32 redirection_url__constrain_max_size = 256; // will assert if the size exceeds 256
        public String redirection_url = null; // '버전 실패시 패치할 수 있는 URL 명시'
    }

    public partial class Recv_MC_LoginRes
    {
        public static System.UInt16 Type = (System.UInt16)Recv.MC_LOGIN_RES;
        public static System.String GetPacketName() { return "MC_LOGIN_RES"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        // public void Read(byte[] data, int size);
        public UInt32 result; 
        public UInt64 required_version; 
        public UInt64 required_build_version; 
        public const UInt32 acct_name__constrain_max_size = 50; // will assert if the size exceeds 50
        public String acct_name = null; 
        public UInt64 session_id; // 'Unique ID in Server'
        public UInt64 acct_id; // '계정 Unique no'
        public UInt64 acct_id_external; 
        public const UInt32 nickname__constrain_max_size = 50; // will assert if the size exceeds 50
        public String nickname = null; 
        public System.DateTime creation_time; // '(UTC+0[GMT])'
        public UInt32 char_count; 
        public UInt64 curr_char_id; 
        public UInt32 gold; 
        public UInt32 ruby; 
        public UInt32 honor; // '월계꽌'
        public UInt32 max_ap; // '최대 행동력'
        public System.DateTime time_to_max_ap; // '(UTC+0[GMT]) ap가 최대치가 될 시간'
        public UInt32 ap_cool_time; // 'ap 쿨타임(1차는데 걸리는 초)'
        public System.DateTime last_logged_in; // '(UTC+0[GMT])'
        public UInt32 acct_level; 
        public UInt32 acct_exp; 
        public UInt32 max_inventory_count; 
        public UInt32 last_processed_system_mail_id; 
        public System.DateTime[] time_to_free_product = new System.DateTime[10]; 
        public UInt32 max_box_inventory_count; 
    }

    public partial class Recv_MC_KeepAliveRes
    {
        public static System.UInt16 Type = (System.UInt16)Recv.MC_KEEP_ALIVE_RES;
        public static System.String GetPacketName() { return "MC_KEEP_ALIVE_RES"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        // public void Read(byte[] data, int size);
        public UInt32 result; // 'see @Error'
        public System.DateTime client_time; 
        public System.DateTime server_time; // '서버 시간'
    }

    public partial class Recv_MC_ListCharacterRes
    {
        public static System.UInt16 Type = (System.UInt16)Recv.MC_LIST_CHARACTER_RES;
        public static System.String GetPacketName() { return "MC_LIST_CHARACTER_RES"; }
        public virtual System.UInt16 _Type { get { return Type; } }
        public virtual System.String _Name { get { return GetPacketName(); } }
        // public void Read(byte[] data, int size);
        public UInt32 result; 
        public UInt64 curr_char_id; // '0: no selected. happen when the account just created.'
        public class Struct_list 
        {
            public UInt64 char_id; 
            public UInt32 char_index; 
            public UInt32 char_level; 
            public UInt32 char_exp; 
            public UInt32[] char_attr_pt = new UInt32[4]; // '0:att,def,health,skillcap'
            public const UInt32 char_spec__constrain_max_size = 20; // will assert if the size exceeds 20
            public String char_spec = null; // 'sepc string: 010021304201102'
            public UInt64[] slot_item_id = new UInt64[10]; // '0: char_skin 1:weapon 2:armor 3:acc1 4:acc2'
            public UInt64[] slot_item_skill_id = new UInt64[10]; 
            public UInt32 max_skill_slot; // '스킬 슬롯 갯수'
            public UInt32[] slot_vanity_item_index = new UInt32[5]; 
            public UInt32[] slot_vanity_item_skill_index = new UInt32[10]; 
        };
        public const UInt32 list__constrain_max_size = 8; // will assert if the size exceeds 8
        // public System.UInt32 list_count;
        public Struct_list[] list = null; // 'list of characters [TODO] add more properties.'
    }
















   public enum Result {
       RESULT_NOT_IMPLEMENTED = -1,
       RESULT_FALSE = 0,
       RESULT_TRUE = 1,
   }

   /****************************************************************************
    *
    * Handler Interface
    */
   public interface IMSToCLHandler
   {
       Result On_MC_VersionCheckRes( Recv_MC_VersionCheckRes packet );
       Result On_MC_LoginRes( Recv_MC_LoginRes packet );
       Result On_MC_KeepAliveRes( Recv_MC_KeepAliveRes packet );
       Result On_MC_ListCharacterRes( Recv_MC_ListCharacterRes packet );
   }

   /****************************************************************************
    *
    * Handler Adaptor
    */
   public class AMSToCLHandler : IMSToCLHandler
   {
       public virtual Result On_MC_VersionCheckRes( Recv_MC_VersionCheckRes packet ) { return Result.RESULT_NOT_IMPLEMENTED; }
       public virtual Result On_MC_LoginRes( Recv_MC_LoginRes packet ) { return Result.RESULT_NOT_IMPLEMENTED; }
       public virtual Result On_MC_KeepAliveRes( Recv_MC_KeepAliveRes packet ) { return Result.RESULT_NOT_IMPLEMENTED; }
       public virtual Result On_MC_ListCharacterRes( Recv_MC_ListCharacterRes packet ) { return Result.RESULT_NOT_IMPLEMENTED; }
       public static Result Dispatch ( IMSToCLHandler this_, System.UInt16 type, byte[] data, int size )
       {
           switch ( type )
           {
               case (System.UInt16)Recv.MC_VERSION_CHECK_RES:
               {
                   Recv_MC_VersionCheckRes packet = new Recv_MC_VersionCheckRes();
                   try {
						packet.Read(data, size);
                   } catch (System.Exception e) {
						System.Console.WriteLine("---- parsing error ----" );
						System.Console.WriteLine(e.Message);
						System.Console.WriteLine(e.StackTrace);
						System.Console.WriteLine("---- ------------- ----" );
						packet.result = (System.UInt32)Error.ERR_PARSING_ERROR;
                   }
                   return this_.On_MC_VersionCheckRes(packet);
               }
               case (System.UInt16)Recv.MC_LOGIN_RES:
               {
                   Recv_MC_LoginRes packet = new Recv_MC_LoginRes();
                   try {
						packet.Read(data, size);
                   } catch (System.Exception e) {
						System.Console.WriteLine("---- parsing error ----" );
						System.Console.WriteLine(e.Message);
						System.Console.WriteLine(e.StackTrace);
						System.Console.WriteLine("---- ------------- ----" );
						packet.result = (System.UInt32)Error.ERR_PARSING_ERROR;
                   }
                   return this_.On_MC_LoginRes(packet);
               }
               case (System.UInt16)Recv.MC_KEEP_ALIVE_RES:
               {
                   Recv_MC_KeepAliveRes packet = new Recv_MC_KeepAliveRes();
                   try {
						packet.Read(data, size);
                   } catch (System.Exception e) {
						System.Console.WriteLine("---- parsing error ----" );
						System.Console.WriteLine(e.Message);
						System.Console.WriteLine(e.StackTrace);
						System.Console.WriteLine("---- ------------- ----" );
						packet.result = (System.UInt32)Error.ERR_PARSING_ERROR;
                   }
                   return this_.On_MC_KeepAliveRes(packet);
               }
               case (System.UInt16)Recv.MC_LIST_CHARACTER_RES:
               {
                   Recv_MC_ListCharacterRes packet = new Recv_MC_ListCharacterRes();
                   try {
						packet.Read(data, size);
                   } catch (System.Exception e) {
						System.Console.WriteLine("---- parsing error ----" );
						System.Console.WriteLine(e.Message);
						System.Console.WriteLine(e.StackTrace);
						System.Console.WriteLine("---- ------------- ----" );
						packet.result = (System.UInt32)Error.ERR_PARSING_ERROR;
                   }
                   return this_.On_MC_ListCharacterRes(packet);
               }
           }
           return Result.RESULT_FALSE;
       }
   }

   /****************************************************************************
    *
    * Serializer for Send/Recv
    */
    // C# Send Serializer
    public partial class Send_CM_VersionCheckReq : ISend
    {
        public virtual byte[] GetBytes()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
            System.IO.MemoryStream tail_stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter tail_writer = new System.IO.BinaryWriter(tail_stream);
            int offset = 0;
            
            
            // UInt64 version
            writer.Write( this.version );
            
            // UInt64 build_version
            writer.Write( this.build_version );
            
            // UInt64 client_key
            writer.Write( this.client_key );
            
            if ( offset > 0 )
                writer.Write(tail_stream.ToArray());
            return stream.ToArray();
        }
    }
    // C# Send Serializer
    public partial class Send_CM_LoginReq : ISend
    {
        public virtual byte[] GetBytes()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
            System.IO.MemoryStream tail_stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter tail_writer = new System.IO.BinaryWriter(tail_stream);
            int offset = 0;
            
            
            // UInt64 version
            writer.Write( this.version );
            
            // UInt64 build_version
            writer.Write( this.build_version );
            
            // String acct_name
            if ( this.acct_name == null ) this.acct_name = "";
            Trace.Assert(this.acct_name.Length <= Send_CM_LoginReq.acct_name__constrain_max_size ); 
            writer.Write( (System.UInt64)offset );
            byte[] acct_name_buf = Util.MBCS.GetBytes(this.acct_name);
            tail_writer.Write(acct_name_buf);
            tail_writer.Write((byte)0);
            offset += acct_name_buf.Length + 1;
            
            // String acct_passwd
            if ( this.acct_passwd == null ) this.acct_passwd = "";
            Trace.Assert(this.acct_passwd.Length <= Send_CM_LoginReq.acct_passwd__constrain_max_size ); 
            writer.Write( (System.UInt64)offset );
            byte[] acct_passwd_buf = Util.MBCS.GetBytes(this.acct_passwd);
            tail_writer.Write(acct_passwd_buf);
            tail_writer.Write((byte)0);
            offset += acct_passwd_buf.Length + 1;
            
            if ( offset > 0 )
                writer.Write(tail_stream.ToArray());
            return stream.ToArray();
        }
    }
    // C# Send Serializer
    public partial class Send_CM_KeepAliveReq : ISend
    {
        public virtual byte[] GetBytes()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
            System.IO.MemoryStream tail_stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter tail_writer = new System.IO.BinaryWriter(tail_stream);
            int offset = 0;
            
            
            // System.DateTime client_time
            writer.Write( this.client_time.ToFileTime() );
            
            if ( offset > 0 )
                writer.Write(tail_stream.ToArray());
            return stream.ToArray();
        }
    }
    // C# Send Serializer
    public partial class Send_CM_ListCharacterReq : ISend
    {
        public virtual byte[] GetBytes()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
            System.IO.MemoryStream tail_stream = new System.IO.MemoryStream();
            System.IO.BinaryWriter tail_writer = new System.IO.BinaryWriter(tail_stream);
            int offset = 0;
            
            
            if ( offset > 0 )
                writer.Write(tail_stream.ToArray());
            return stream.ToArray();
        }
    }
    // C# Recv Serializer
    public partial class Recv_MC_VersionCheckRes
    {
        public void Read(byte[] data, int size)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream(data);
            System.IO.BinaryReader reader = new System.IO.BinaryReader(stream);
            List<Util.runLater> runLaterList = new List<Util.runLater>();
            long begin_offset = 0;
            
            
            // UInt32 result
            this.result = reader.ReadUInt32();
            
            // UInt64 required_version
            this.required_version = reader.ReadUInt64();
            
            // UInt64 required_build_version
            this.required_build_version = reader.ReadUInt64();
            
            // String redirection_url
            long redirection_url_offset = (long)reader.ReadUInt64();
            runLaterList.Add(() => {
                stream.Position = begin_offset + redirection_url_offset;
                this.redirection_url = Util.ReadCString(reader);
            });
            
            // run all runLaters
            begin_offset = stream.Position;
            foreach (Util.runLater run in runLaterList)
                run();
        }
    }
    // C# Recv Serializer
    public partial class Recv_MC_LoginRes
    {
        public void Read(byte[] data, int size)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream(data);
            System.IO.BinaryReader reader = new System.IO.BinaryReader(stream);
            List<Util.runLater> runLaterList = new List<Util.runLater>();
            long begin_offset = 0;
            
            
            // UInt32 result
            this.result = reader.ReadUInt32();
            
            // UInt64 required_version
            this.required_version = reader.ReadUInt64();
            
            // UInt64 required_build_version
            this.required_build_version = reader.ReadUInt64();
            
            // String acct_name
            long acct_name_offset = (long)reader.ReadUInt64();
            runLaterList.Add(() => {
                stream.Position = begin_offset + acct_name_offset;
                this.acct_name = Util.ReadCString(reader);
            });
            
            // UInt64 session_id
            this.session_id = reader.ReadUInt64();
            
            // UInt64 acct_id
            this.acct_id = reader.ReadUInt64();
            
            // UInt64 acct_id_external
            this.acct_id_external = reader.ReadUInt64();
            
            // String nickname
            long nickname_offset = (long)reader.ReadUInt64();
            runLaterList.Add(() => {
                stream.Position = begin_offset + nickname_offset;
                this.nickname = Util.ReadCString(reader);
            });
            
            // System.DateTime creation_time
            this.creation_time = System.DateTime.FromFileTime( 0 );
            try { this.creation_time = System.DateTime.FromFileTime( reader.ReadInt64() ); } catch ( System.Exception e ) {}
            
            // UInt32 char_count
            this.char_count = reader.ReadUInt32();
            
            // UInt64 curr_char_id
            this.curr_char_id = reader.ReadUInt64();
            
            // UInt32 gold
            this.gold = reader.ReadUInt32();
            
            // UInt32 ruby
            this.ruby = reader.ReadUInt32();
            
            // UInt32 honor
            this.honor = reader.ReadUInt32();
            
            // UInt32 max_ap
            this.max_ap = reader.ReadUInt32();
            
            // System.DateTime time_to_max_ap
            this.time_to_max_ap = System.DateTime.FromFileTime( 0 );
            try { this.time_to_max_ap = System.DateTime.FromFileTime( reader.ReadInt64() ); } catch ( System.Exception e ) {}
            
            // UInt32 ap_cool_time
            this.ap_cool_time = reader.ReadUInt32();
            
            // System.DateTime last_logged_in
            this.last_logged_in = System.DateTime.FromFileTime( 0 );
            try { this.last_logged_in = System.DateTime.FromFileTime( reader.ReadInt64() ); } catch ( System.Exception e ) {}
            
            // UInt32 acct_level
            this.acct_level = reader.ReadUInt32();
            
            // UInt32 acct_exp
            this.acct_exp = reader.ReadUInt32();
            
            // UInt32 max_inventory_count
            this.max_inventory_count = reader.ReadUInt32();
            
            // UInt32 last_processed_system_mail_id
            this.last_processed_system_mail_id = reader.ReadUInt32();
            
            // System.DateTime time_to_free_product[10]
            this.time_to_free_product = new System.DateTime[10];
            for ( int i = 0; i < 10; ++i )
            {
                this.time_to_free_product[i] = System.DateTime.FromFileTime( 0 );
        		try { this.time_to_free_product[i] = System.DateTime.FromFileTime( reader.ReadInt64() ); } catch ( System.Exception e ) {}
            }
            
            
            // UInt32 max_box_inventory_count
            this.max_box_inventory_count = reader.ReadUInt32();
            
            // run all runLaters
            begin_offset = stream.Position;
            foreach (Util.runLater run in runLaterList)
                run();
        }
    }
    // C# Recv Serializer
    public partial class Recv_MC_KeepAliveRes
    {
        public void Read(byte[] data, int size)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream(data);
            System.IO.BinaryReader reader = new System.IO.BinaryReader(stream);
            List<Util.runLater> runLaterList = new List<Util.runLater>();
            long begin_offset = 0;
            
            
            // UInt32 result
            this.result = reader.ReadUInt32();
            
            // System.DateTime client_time
            this.client_time = System.DateTime.FromFileTime( 0 );
            try { this.client_time = System.DateTime.FromFileTime( reader.ReadInt64() ); } catch ( System.Exception e ) {}
            
            // System.DateTime server_time
            this.server_time = System.DateTime.FromFileTime( 0 );
            try { this.server_time = System.DateTime.FromFileTime( reader.ReadInt64() ); } catch ( System.Exception e ) {}
            
            // run all runLaters
            begin_offset = stream.Position;
            foreach (Util.runLater run in runLaterList)
                run();
        }
    }
    // C# Recv Serializer
    public partial class Recv_MC_ListCharacterRes
    {
        public void Read(byte[] data, int size)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream(data);
            System.IO.BinaryReader reader = new System.IO.BinaryReader(stream);
            List<Util.runLater> runLaterList = new List<Util.runLater>();
            long begin_offset = 0;
            
            
            // UInt32 result
            this.result = reader.ReadUInt32();
            
            // UInt64 curr_char_id
            this.curr_char_id = reader.ReadUInt64();
            
            // Struct_list list
            int list_size = (int)reader.ReadUInt16();
            long list_offset = (long)reader.ReadUInt64();
            this.list = new Recv_MC_ListCharacterRes.Struct_list[list_size];
            runLaterList.Add(() => {
                stream.Position = begin_offset + list_offset;
                List<Util.runLater> list_runLaterList = new List<Util.runLater>();
                for ( int list_i = 0; list_i < this.list.Length; ++list_i )
                {
                    this.list[list_i] = new Recv_MC_ListCharacterRes.Struct_list();
                    int list_copy_i = list_i;
                
                // UInt64 char_id
                this.list[list_copy_i].char_id = reader.ReadUInt64();
                
                // UInt32 char_index
                this.list[list_copy_i].char_index = reader.ReadUInt32();
                
                // UInt32 char_level
                this.list[list_copy_i].char_level = reader.ReadUInt32();
                
                // UInt32 char_exp
                this.list[list_copy_i].char_exp = reader.ReadUInt32();
                
                // UInt32 char_attr_pt[4]
                this.list[list_copy_i].char_attr_pt = new UInt32[4];
                for ( int i = 0; i < 4; ++i )
                    this.list[list_copy_i].char_attr_pt[i] = reader.ReadUInt32();
                
                
                // String char_spec
                long list_char_spec_offset = (long)reader.ReadUInt64();
                list_runLaterList.Add(() => {
                    stream.Position = begin_offset + list_char_spec_offset;
                    this.list[list_copy_i].char_spec = Util.ReadCString(reader);
                });
                
                // UInt64 slot_item_id[10]
                this.list[list_copy_i].slot_item_id = new UInt64[10];
                for ( int i = 0; i < 10; ++i )
                    this.list[list_copy_i].slot_item_id[i] = reader.ReadUInt64();
                
                
                // UInt64 slot_item_skill_id[10]
                this.list[list_copy_i].slot_item_skill_id = new UInt64[10];
                for ( int i = 0; i < 10; ++i )
                    this.list[list_copy_i].slot_item_skill_id[i] = reader.ReadUInt64();
                
                
                // UInt32 max_skill_slot
                this.list[list_copy_i].max_skill_slot = reader.ReadUInt32();
                
                // UInt32 slot_vanity_item_index[5]
                this.list[list_copy_i].slot_vanity_item_index = new UInt32[5];
                for ( int i = 0; i < 5; ++i )
                    this.list[list_copy_i].slot_vanity_item_index[i] = reader.ReadUInt32();
                
                
                // UInt32 slot_vanity_item_skill_index[10]
                this.list[list_copy_i].slot_vanity_item_skill_index = new UInt32[10];
                for ( int i = 0; i < 10; ++i )
                    this.list[list_copy_i].slot_vanity_item_skill_index[i] = reader.ReadUInt32();
                
                }
                
                // run all list_runLaters
                foreach (Util.runLater run in list_runLaterList)
                    run();
            });
            
            
            // run all runLaters
            begin_offset = stream.Position;
            foreach (Util.runLater run in runLaterList)
                run();
        }
    }

} // end of cl_ms
