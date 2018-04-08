using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zhoulb.DLL
{
    public class InsertSQL
    {
        /// <summary>
        /// 插入客户数据
        /// </summary>
        /// <returns></returns>
        public static string InsertClient(string clientName,string clientPhone)
        {
            return $"insert into ClientInfo(ClientName, ClientPhone) values('{clientName}', '{clientPhone}')";
        }

        /// <summary>
        /// 插入商品数据
        /// </summary>
        /// <returns></returns>
        public static string InsertProduct(string productName, string inPrice, string outPrice, string productCode,string productNum ,string remark, int clientInfo,string pictureInfo)
        {
            return
                $"insert into ProductInfo(ProductName,InPrice,OutPrice,ProductCode,ProductNum,Remark,ClientInfo,PictureInfo) values('{productName}','{inPrice}','{outPrice}','{productCode}','{productNum}','{remark}',{clientInfo},'{pictureInfo}')";
        }
        /// <summary>
        /// 插入图片数据
        /// </summary>
        /// <param name="pictureId"></param>
        /// <param name="pictureName"></param>
        /// <param name="picturePath"></param>
        /// <returns></returns>
        public static string InsertPicture(string pictureId,string pictureName,string picturePath)
        {
            return
                $"insert into PictureInfo(PictureID,PictureName,PicturePath) values('{pictureId}','{pictureName}','{picturePath}')";
        }
    }
}
