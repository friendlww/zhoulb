using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zhoulb.DLL
{
    public static class UpdateSQL
    {
        /// <summary>
        /// 更新客户数据
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="clientPhone"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static string UpdateClient(string clientName, string clientPhone,int clientId)
        {
            return $"update ClientInfo set  ClientName='{clientName}',ClientPhone='{clientPhone}' where ClientID='{clientId}'  ";
        }

        /// <summary>
        /// 更新商品数据
        /// </summary>
        /// <returns></returns>
        public static string UpdateProduct(string productName, string inPrice, string outPrice, string productCode,string productNum, string remark, int clientInfo, string pictureInfo,int productId)
        {
            return
                $"update ProductInfo set ProductName='{productName}',InPrice='{inPrice}',OutPrice='{outPrice}',ProductCode='{productCode}',ProductNum='{productNum}',Remark='{remark}',ClientInfo={clientInfo},PictureInfo='{pictureInfo}' where ProductID='{productId}'";
        }
    }
}
