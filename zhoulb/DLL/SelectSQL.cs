

namespace zhoulb.DLL
{
    public static class SelectSQL
    {
        /// <summary>
        /// 搜索首页统计数据
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="clientPhone"></param>
        /// <param name="productName"></param>
        /// <param name="productCode"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static string SelectStatisticInfo(string clientName, string clientPhone,string productName,string productCode ,string remark)
        {
            var sql = "SELECT  b.ClientName,a.ProductName,a.ProductCode, a.InPrice, a.OutPrice,ifnull(a.ProductNum,0) ProductNum, a.Remark, c.PictureID  FROM ProductInfo a LEFT JOIN ClientInfo b ON a.ClientInfo=b.ClientID LEFT JOIN PictureInfo c ON a.PictureInfo=c.PictureID";
            var sqlpara = string.Empty;
            if (string.IsNullOrEmpty(clientName) == false)
            {
                sqlpara += $" and ClientName like '%{clientName}%' ";
            }
            if (string.IsNullOrEmpty(clientPhone) == false)
            {
                sqlpara += $" and ClientPhone like '%{clientPhone}%' ";
            }
            if (string.IsNullOrEmpty(productName) == false)
            {
                sqlpara += $" and ProductName like '%{productName}%' ";
            }
            if (string.IsNullOrEmpty(productCode) == false)
            {
                sqlpara += $" and ProductCode like '%{productCode}%' ";
            }
            if (string.IsNullOrEmpty(remark) == false)
            {
                sqlpara += $" and Remark like '%{remark}%' ";
            }
            if (string.IsNullOrEmpty(sqlpara) == false)
            {
                sql += " where ProductID is not null " + sqlpara;
            }
            return sql;
        }

        /// <summary>
        /// 搜索客户
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="clientPhone"></param>
        /// <returns></returns>
        public static string SelectClient(string clientName, string clientPhone)
        {
            var sql = "select ClientName,ClientPhone,ClientID from ClientInfo ";
            var sqlpara = string.Empty;
            if (string.IsNullOrEmpty(clientName) == false)
            {
                sqlpara += $" and ClientName  like '%{clientName}%' ";
            }
            if (string.IsNullOrEmpty(clientPhone) == false)
            {
                sqlpara += $" and ClientPhone like '%{clientPhone}%' ";
            }
            if (string.IsNullOrEmpty(sqlpara) == false)
            {
                sql += " where ClientID is not null " + sqlpara;
            }
            return sql;
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productCode"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static string SelectProduct(string productName,string productCode,string remark)
        {
            var sql = "SELECT ProductID, ProductName, ProductCode , InPrice, OutPrice,ifnull(ProductNum,0) ProductNum ,Remark from ProductInfo ";
            var sqlpara = string.Empty;
            if (string.IsNullOrEmpty(productName) == false)
            {
                sqlpara += $" and ProductName like '%{productName}%' ";
            }
            if (string.IsNullOrEmpty(productCode) == false)
            {
                sqlpara += $" and ProductCode like '%{productCode}%' ";
            }
            if (string.IsNullOrEmpty(remark) == false)
            {
                sqlpara += $" and Remark like '%{remark}%' ";
            }
            if (string.IsNullOrEmpty(sqlpara) == false)
            {
                sql += " where  ProductID is not null " + sqlpara;
            }
            return sql;
        }

        /// <summary>
        /// 根据主键查找客户
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static string SelectClientWithId(int clientId)
        {
            return $"select ClientName,ClientPhone,ClientID from ClientInfo where ClientID={clientId}";
        }

        /// <summary>
        /// 根据主键查找图片
        /// </summary>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        public static string SelectPictureWithId(string pictureId)
        {
            return $"SELECT PictureID, PictureName, PicturePath FROM PictureInfo WHERE PictureID='{pictureId}'";
        }

        /// <summary>
        /// 根据主键查找商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static string SelectProductWithId(int productId)
        {
            return
                $"SELECT ProductID, ProductName, InPrice, OutPrice, ProductCode,ifnull(ProductNum,0) ProductNum, Remark, ClientInfo,PictureInfo  FROM ProductInfo where ProductID={productId}";
        }
    }
}
