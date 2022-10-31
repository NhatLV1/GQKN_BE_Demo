namespace PVI.GQKN.API.Services.Auth;


/// <summary>
/// ACL generic operations
/// </summary>
public static class ACL_OPS
{
    public const ulong CRUD_LIST    = 1L << 0;
    public const ulong CRUD_DETAIL  = 1L << 1;
    public const ulong CRUD_CREATE  = 1L << 2;
    public const ulong CRUD_UPDATE  = 1L << 3;
    /// <summary>
    /// bit 4
    /// </summary>
    public const ulong CRUD_DELETE  = 1L << 4;

    // File actions
    public const ulong FILE_CREATE  = 1L << 11;
    public const ulong FILE_READ    = 1L << 12;
    //public const long U = 1 << 13;
    public const ulong FILE_DELETE = 1L << 14;
    public const ulong FILE_EXPORT = 1L << 15;
    
    // Folder
    public const ulong FOLDER_CREATE = 1L << 16;
    public const ulong FOLDER_UPDATE = 1L << 17;
    public const ulong FOLDER_DELETE = 1L << 18;

    // Flows
    //public const ulong FLOW_PROCESS = 1L << 21; // chuyển xử lý
    public const ulong FLOW_APPROVE = 1L << 22; // duyệt 
    public const ulong FLOW_FOWARD =  1L << 23; // chuyển xử lý 
    public const ulong FLOW_CANCEL =  1L << 24; // hủy
    public const ulong FLOW_REJECT =  1L << 25; // trả 

    // Notifications
    public const ulong NOTIFICATION_HOTLINE = 1L << 31; // Hotline
    public const ulong NOTIFICATION_EMAIL = 1L << 32; // Gửi email 
    public const ulong NOTIFICATION_WEB = 1L << 33; // Gửi Notif Web (web)
    public const ulong NOTIFICATION_APP = 1L << 34; // Gửi Notif App (push)

    public const int FLOW_START_BIT = 21;
    public const int FLOW_END_BIT = 25;

    public const int FILE_END_BIT = 15;

    public const int NOTIFICATION_START_BIT = 31;
}

/// <summary>
/// Tác vụ administrator
/// </summary>
public static class ADMIN_OPS
{
    public const string ACL_SCOPE = "ADMIN";

    public const string RES_VAITRO = "ADMIN-ROLE";
    public const string RES_ACCOUNT = "ADMIN-ACC";
    public const string RES_CHUCDANH = "ADMIN-POS";
    public const string RES_PHONGBAN = "ADMIN-DEP";

    // user
    public const ulong ACCOUNT_LIST         = 1L << 0;
    //public const ulong ACCOUNT_DETAIL       = 1L << 1;
    public const ulong ACCOUNT_CREATE       = 1L << 2;
    public const ulong ACCOUNT_UPDATE       = 1L << 3;
    public const ulong ACCOUNT_DEACTIVATE   = 1L << 4;

    // role
    public const ulong VAITRO_LIST      = 1L << 10;
    //public const ulong VAITRO_DETAIL    = 1L << 11;
    public const ulong VAITRO_CREATE    = 1L << 12;
    public const ulong VAITRO_UPDATE    = 1L << 13;
    public const ulong VAITRO_DELETE    = 1L << 14;

    // chức danh
    public const ulong CHUCDANH_LIST    = 1L << 20;
    //public const ulong CHUCDANH_DETAIL  = 1L << 21;
    public const ulong CHUCDANH_CREATE  = 1L << 22;
    public const ulong CHUCDANH_UPDATE  = 1L << 23;
    public const ulong CHUCDANH_DELETE  = 1L << 24;

    // phòng ban
    public const ulong PHONGBAN_LIST    = 1L << 30;
    //public const ulong PHONGBAN_DETAIL  = 1L << 31;
    public const ulong PHONGBAN_CREATE  = 1L << 32;
    public const ulong PHONGBAN_UPDATE  = 1L << 33;
    public const ulong PHONGBAN_DELETE  = 1L << 34;

}

/// <summary>
/// Khai báo tổn thất ACL Operations
/// </summary>
public static class KBTT_OPS
{
    public const string ACL_SCOPE = "KBTT";

    public const string RES_KBTT = "KBTT-CRUD";
    public const string RES_FILE = "KBTT-FILE";
    public const string RES_FOLDER = "KBTT-FOLDRE";

    // CRUD
    public const ulong LIST_KBTT    = ACL_OPS.CRUD_LIST;
    //public const ulong DETAIL_KBTT  = ACL_OPS.CRUD_DETAIL;
    public const ulong CREATE_KBTT  = ACL_OPS.CRUD_CREATE;
    public const ulong UPDATE_KBTT  = ACL_OPS.CRUD_UPDATE;
    public const ulong CANCEL_KBTT  = ACL_OPS.CRUD_DELETE;

    // FILE
    public const ulong UPLOAD_FILE = ACL_OPS.FILE_CREATE;
    public const ulong DELETE_FILE = ACL_OPS.FILE_DELETE;
    public const ulong EXPORT_FILE = ACL_OPS.FILE_EXPORT;

    // FILE
    public const ulong FOLDER_CREATE = ACL_OPS.FOLDER_CREATE;
    public const ulong FOLDER_UPDATE = ACL_OPS.FOLDER_UPDATE;
    public const ulong FOLDER_DELETE = ACL_OPS.FOLDER_DELETE;

}

/// <summary>
/// Báo cáo tổn thất ACL Operations
/// </summary>
public static class BCTT_OPS
{
    public const string ACL_SCOPE = "BCTT";

    public const string RES_BCTT = "BCTT-CRUD";
    public const string RES_FILE = "BCTT-FILE";
    public const string RES_FLOW = "BCTT-FLOW";
    public const string RES_FOLDER = "BCTT-FOLDER";

    // CRUD
    public const ulong BCTT_LIST        = ACL_OPS.CRUD_DETAIL;
    public const ulong BCTT_CREATE      = ACL_OPS.CRUD_CREATE; // tiếp nhận & lập
    public const ulong BCTT_UPDATE      = ACL_OPS.CRUD_UPDATE; //
    public const ulong BCTT_CANCEL      = ACL_OPS.CRUD_DELETE; // 4
    public const ulong BCTT_VERFIY      = (1L << 5);


    // FILE
    public const ulong FILE_UPLOAD = ACL_OPS.FILE_CREATE;
    public const ulong FILE_REMOVE = ACL_OPS.FILE_DELETE;
    public const ulong FILE_EXPORT = ACL_OPS.FILE_EXPORT;
    
    // Folder
    public const ulong FOLDER_CREATE = (1UL << (ACL_OPS.FILE_END_BIT + 1));
    public const ulong FOLDER_UPDATE = (1UL << (ACL_OPS.FILE_END_BIT + 2));
    public const ulong FOLDER_DELETE = (1UL << (ACL_OPS.FILE_END_BIT + 3));

    // FLOW
    public const ulong FLOW_APROVE                  = ACL_OPS.FLOW_APPROVE;
    public const ulong FLOW_FORWARD                 = ACL_OPS.FLOW_FOWARD;
    public const ulong FLOW_VALIDATE_BCTT           = (1UL << (ACL_OPS.FLOW_END_BIT + 1));
    public const ulong FLOW_VALIDATE_KTTC           = (1UL << (ACL_OPS.FLOW_END_BIT + 2));
    public const ulong FLOW_VALIDATE_TBH            = (1UL << (ACL_OPS.FLOW_END_BIT + 3));
  
}

public static class PAGD_OPS
{
    public const string ACL_SCOPE = "PAGD";

    public const string RES_BCTT = "PAGD-CRUD";
    public const string RES_FILE = "PAGD-FILE";
    public const string RES_FLOW = "PAGD-FLOW";

    // CRUD
    public const ulong PAGD_LIST    = ACL_OPS.CRUD_DETAIL;
    public const ulong PAGD_CREATE  = ACL_OPS.CRUD_CREATE; 
    public const ulong PAGD_UPDATE  = ACL_OPS.CRUD_UPDATE;
    public const ulong PAGD_CANCEL  = ACL_OPS.CRUD_DELETE;

    // Flow
    public const ulong FLOW_CANCEL = ACL_OPS.FLOW_CANCEL;   // Hủy  Phương án giám định
    public const ulong FLOW_REJECT = ACL_OPS.FLOW_REJECT;   // Trả lại BCTT
    public const ulong FLOW_APROVE = ACL_OPS.FLOW_APPROVE;  // Xác nhận Phương án giám định
    public const ulong FLOW_FOWARD = ACL_OPS.FLOW_FOWARD;   // Chuyển xử lý
    /*
        Duyệ phương án tự giám định 
        Duyệ phương án cty giám định
        Duyệ phương án Thuê chuyên gia
     */


    public const ulong FLOW_VALIDATE_BCTT = (1UL << (ACL_OPS.FLOW_END_BIT + 1));
    public const ulong FLOW_VALIDATE_KTTC = (1UL << (ACL_OPS.FLOW_END_BIT + 2));
    public const ulong FLOW_VALIDATE_TBH = (1UL << (ACL_OPS.FLOW_END_BIT + 3));
}
