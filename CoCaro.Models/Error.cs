using System;


namespace CoCaro.Models
{
    [Serializable]
    public class Error
    {
        public static ErrorObject SUCCESS = new ErrorObject { Code = 0, Message = "Thành công" };
        public static ErrorObject FAILED = new ErrorObject { Code = 1, Message = "Thất bại" };
        public static ErrorObject SYSTEM = new ErrorObject { Code = 2, Message = "Lỗi hệ thống" };
        public static ErrorObject DATABASE = new ErrorObject { Code = 3, Message = "Lỗi database" };
        public static ErrorObject AUTH = new ErrorObject { Code = 4, Message = "Lỗi chứng thực" };
        public static ErrorObject INVALID_DATA = new ErrorObject { Code = 5, Message = "Dữ liệu vào không chính xác" };

        //Các lỗi về tài khoản
        public static ErrorObject USER_INVALID = new ErrorObject { Code = 100, Message = "Tài khoản hoặc mật khẩu không chính xác." };
        public static ErrorObject USER_LOCKED = new ErrorObject { Code = 101, Message = "Tài khoản đã bị khóa." };
        public static ErrorObject USER_DELETED = new ErrorObject { Code = 102, Message = "Tài khoản đã bị xóa." };
        public static ErrorObject USER_EXISTED = new ErrorObject { Code = 103, Message = "Tài khoản đã tồn tại." };
        public static ErrorObject USER_INACTIVE = new ErrorObject { Code = 104, Message = "Tài khoản chưa được phép sử dụng." };
        public static ErrorObject USER_LICENSE_EXPIRED = new ErrorObject { Code = 105, Message = "Tài khoản đã hết hạn sử dụng phần mềm." };
        public static ErrorObject USER_NOT_EXISTED = new ErrorObject { Code = 106, Message = "Tài khoản không tồn tại." };

        //Các lỗi về Agency
        public static ErrorObject AGENCY_NAME_EXISTED = new ErrorObject { Code = 107, Message = "Agency name already exists." };
        public static ErrorObject AGENCY_CODE_EXISTED = new ErrorObject { Code = 108, Message = "Agency code already exists." };

        //Các lỗi về Customer
        public static ErrorObject CUSTOMER_USERNAME_EXISTED = new ErrorObject { Code = 109, Message = "Username already exists." };
        public static ErrorObject CUSTOMER_EMAIL_EXISTED = new ErrorObject { Code = 110, Message = "Email already exists." };
        public static ErrorObject CUSTOMER_INVALID = new ErrorObject { Code = 111, Message = "Tài khoản hoặc mật khẩu không chính xác." };
        public static ErrorObject CUSTOMER_LOCKED = new ErrorObject { Code = 112, Message = "Tài khoản đã bị khóa." };
        public static ErrorObject CUSTOMER_DELETED = new ErrorObject { Code = 113, Message = "Tài khoản đã bị xóa." };
        public static ErrorObject PASSWORD_INCORRECT = new ErrorObject { Code = 120, Message = "Mật khẩu cũ không đúng" };

        //vì các biến trên là static nên dẫn đến bị setData chồng lên nhau
        //=> tạo ra các function khác vùng nhớ
        //Dùng khi có kết quả trả về 
        public static ErrorObject Get(ErrorObject ErrorObj, object Data = null)
        {
            return new ErrorObject(ErrorObj, Data);
        }

        public static ErrorObject Success()
        {
            return new ErrorObject(SUCCESS);
        }

    }

    [Serializable]
    public class ErrorObject
    {
        public int Code { get; set; } 
        public string Message { get; set; }
        public object Data { get; set; }
        public ErrorObject(int Code, string Message, object Data = null)
        {
            this.Code = Code;
            this.Message = Message;
            this.Data = Data;
        }

        //set mặc định thành công
        public ErrorObject()
        {
            Code = 0;
            Message = "Thành công";
        }

        public ErrorObject(ErrorObject Obj)
        {
            this.Code = Obj.Code;
            this.Message = Obj.Message;
            this.Data = Obj.Data;
        }

        public ErrorObject(ErrorObject Obj, object Data)
        {
            this.Code = Obj.Code;
            this.Message = Obj.Message;
            this.Data = Data;
        }

        public ErrorObject SetCode(ErrorObject Obj)
        {
            this.Code = Obj.Code;
            this.Message = Obj.Message;
            return this;
        }

        public ErrorObject SetCode(ErrorObject Obj, object Data)
        {
            SetCode(Obj).Data = Data;
            return this;
        }

        public ErrorObject SetMessage(string Message)
        {
            this.Message = Message;
            return this;
        }

        public ErrorObject SetData(object Data)
        {
            this.Data = Data;
            return this;
        }

        public object GetData()
        {
            return this.Data;
        }

        public ErrorObject Success(object Data)
        {
            return SetCode(Error.SUCCESS, Data);
        }

        public ErrorObject Failed(object Data)
        {
            return SetCode(Error.FAILED, Data);
        }

        public ErrorObject Failed(string Message)
        {
            return SetCode(Error.FAILED).SetMessage(Message);
        }

        public ErrorObject System(object Data)
        {
            return SetCode(Error.SYSTEM, Data);
        }

        public T GetData<T>()
        {
            return (T)Data;

        }
    }
}
