using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zhoulb.DLL
{
    public static class DeleteSQL
    {
        /// <summary>
        /// 根据主键查找客户
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static string DeleteClientWithId(int clientId)
        {
            return $"delete from ClientInfo where ClientID={clientId}";
        }

        /// <summary>
        /// 根据主键查找图片
        /// </summary>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        public static string DeletePictureWithId(string pictureId)
        {
            return $"delete FROM PictureInfo WHERE PictureID='{pictureId}'";
        }

        /// <summary>
        /// 根据主键查找商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static string DeleteProductWithId(int productId)
        {
            return
                $"delete FROM ProductInfo where ProductID={productId}";
        }
    }
}
