using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace CoCaro.Utilities
{
    /// <summary>
    /// Author: BinhNQ
    /// </summary>
    public static class CommonExtensions
    {
        public static Dictionary<string, object> ToDictionary(this object myObj)
        {
            return myObj.GetType()
                .GetProperties()
                .Select(pi => new { Name = pi.Name, Value = pi.GetValue(myObj, null) })
                .Union(
                    myObj.GetType()
                        .GetFields()
                        .Select(fi => new { Name = fi.Name, Value = fi.GetValue(myObj) })
                )
                .ToDictionary(ks => ks.Name, vs => vs.Value);
        }

        public static List<Claim> ToClaim(this object myObj)
        {
            return myObj.GetType()
                .GetProperties()
                .Select(pi => new Claim(pi.Name, pi.GetValue(myObj, new[] { "" }).ToString()))
                .Union(
                    myObj.GetType()
                        .GetFields()
                        .Select(fi => new Claim(fi.Name, fi.GetValue(myObj).ToString()))
                )
                .ToList();
        }

        public static string ToJson(this object Obj)
        {
            return JsonConvert.SerializeObject(Obj);
        }

        public static T JsonToObject<T>(this string Data)
        {
            return JsonConvert.DeserializeObject<T>(Data);
        }

        public static string EncryptMd5(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            byte[] valueArray = Encoding.ASCII.GetBytes(value);
            valueArray = md5.ComputeHash(valueArray);
            var sb = new StringBuilder();
            for (int i = 0; i < valueArray.Length; i++)
                sb.Append(valueArray[i].ToString("x2").ToLower());
            return sb.ToString();
        }

        public static string Decode(this string strString, string strKeyPhrase)
        {
            Byte[] byt = Convert.FromBase64String(strString);
            strString = System.Text.Encoding.UTF8.GetString(byt);
            strString = KeyED(strString, strKeyPhrase);
            return strString;
        }
        public static string Encode(this string strString, string strKeyPhrase)
        {

            strString = KeyED(strString, strKeyPhrase);
            Byte[] byt = System.Text.Encoding.UTF8.GetBytes(strString);
            strString = Convert.ToBase64String(byt);
            return strString;
        }

        private static string KeyED(string strString, string strKeyphrase)
        {
            int strStringLength = strString.Length;
            int strKeyPhraseLength = strKeyphrase.Length;

            System.Text.StringBuilder builder = new System.Text.StringBuilder(strString);

            for (int i = 0; i < strStringLength; i++)
            {
                int pos = i % strKeyPhraseLength;
                int xorCurrPos = (int)(strString[i]) ^ (int)(strKeyphrase[pos]);
                builder[i] = Convert.ToChar(xorCurrPos);
            }

            return builder.ToString();
        }

        #region Compare object extension 
        //Source: https://toidicodedao.com/2015/06/16/series-c-hay-ho-so-sanh-2-object-trong-c-deep-compare/

        public static bool DeepEquals(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            //Nếu property không phải class, chỉ là int, double, DateTime v...v
            //Gọi hàm equal thông thường
            if (!obj.GetType().IsClass) return obj.Equals(another);

            var result = true;
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                //Tiếp tục đệ quy
                if (!objValue.DeepEquals(anotherValue)) result = false;
            }
            return result;
        }

        public static bool DeepEquals<T>(this IEnumerable<T> obj, IEnumerable<T> another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;

            bool result = true;
            //Duyệt từng phần tử trong 2 list đưa vào
            using (IEnumerator<T> enumerator1 = obj.GetEnumerator())
            using (IEnumerator<T> enumerator2 = another.GetEnumerator())
            {
                while (true)
                {
                    bool hasNext1 = enumerator1.MoveNext();
                    bool hasNext2 = enumerator2.MoveNext();

                    //Nếu có 1 list hết, hoặc 2 phần tử khác nhau, thoát khoải vòng lặp
                    if (hasNext1 != hasNext2 || !enumerator1.Current.DeepEquals(enumerator2.Current))
                    {
                        result = false;
                        break;
                    }

                    //Dừng vòng lặp khi 2 list đều hết
                    if (!hasNext1) break;
                }
            }

            return result;
        }

        public static bool JsonEquals(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);

            return objJson == anotherJson;
        }

        #endregion


    }
}